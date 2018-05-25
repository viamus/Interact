using Interact.Library.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interact.Library.Structure
{
    public abstract class Consumer<T> where T : IQueueObject
    {
        private readonly TimeSpan SCHEDULE_CONSUMER_SERVER_STATUS = new TimeSpan(0, 0, 30);
        private readonly TimeSpan SCHEDULE_CONSUMER_CLIENT_STATUS = new TimeSpan(0, 0, 30);
        private readonly TimeSpan SCHEDULE_BASE_IDLE_TIME = new TimeSpan(0, 0, 30);
        private readonly TimeSpan SCHEDULE_MAX_IDLE_TIME = new TimeSpan(0, 5, 0);

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private object _ConsumerStatusLocker { get; set; } = new object();
        protected ConsumerStatus _ConsumerStatus
        {
            get
            {
                return _ConsumerStatus;
            }
            set
            {
                lock (_ConsumerStatusLocker)
                {
                    _ConsumerStatus = value;
                }
            }
        }
        public ConcurrentQueue<T> MemoryQueue { get; set; } = new ConcurrentQueue<T>();
        public string ThreadGuid { get; set; } = Guid.NewGuid().ToString();
        public string ThreadGroup { get; set; } = $"DefaultGroup";
        public int MaxMemoryQueueObjects { get; set; } = 10;

        public Consumer(string ThreadGroup, int MaxMemoryQueueObjects)
        {
            if (!MaxMemoryQueueObjects.ValidRange(10, 1000))
            {
                throw new ArgumentOutOfRangeException("MaxMemoryQueueObjects must be a integer in the range of 10 to 1000.");
            }
        }

        public abstract ICollection<T> GetObjectsFromQueue(int maxObjects);

        public abstract void RemoveObjectFromQueue(T @object);

        public abstract ConsumerStatus GetConsumerServerStatus();

        protected abstract void NotifyConsumerClientStatus();

        protected virtual async Task LoadConsumerClientStatus()
        {
            await Task.Delay(1000);

            while (_ConsumerStatus != ConsumerStatus.DISPOSE)
            {
                await Task.Delay(SCHEDULE_CONSUMER_CLIENT_STATUS);
                try
                {
                    NotifyConsumerClientStatus();
                }
                catch (Exception ex)
                {
                    Log.Error($"Consumer from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid}  could not send the consumer client status to the server.", exception: ex);
                    _ConsumerStatus = ConsumerStatus.OFFLINE;
                }
            }
        }

        protected virtual async Task LoadConsumerServerStatus()
        {
            await Task.Delay(1000);

            while (_ConsumerStatus != ConsumerStatus.DISPOSE)
            {
                await Task.Delay(SCHEDULE_CONSUMER_SERVER_STATUS);
                try
                {
                    _ConsumerStatus = GetConsumerServerStatus();
                }
                catch (Exception ex)
                {
                    Log.Error($"Consumer from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid}  could not load the consumer server status.", exception: ex);
                    _ConsumerStatus = ConsumerStatus.OFFLINE;
                }
            }
        }

        protected virtual async Task LoadConsumer()
        {
            int idleCount = 0;

            await Task.Delay(1000);

            while (_ConsumerStatus != ConsumerStatus.DISPOSE)
            {
                try
                {
                    while (_ConsumerStatus == ConsumerStatus.OFFLINE) { await Task.Delay(1000); };

                    var objects = GetObjectsFromQueue(MaxMemoryQueueObjects - MemoryQueue.Count);

                    if (objects.NullOrEmpty())
                    {
                        idleCount++;

                        if (_ConsumerStatus == ConsumerStatus.ONLINE_IDLE)
                        {
                            var waitingTime = idleCount * SCHEDULE_BASE_IDLE_TIME;
                            if (waitingTime >= SCHEDULE_MAX_IDLE_TIME)
                            {
                                waitingTime = SCHEDULE_BASE_IDLE_TIME;
                            }
                            await Task.Delay(waitingTime);
                        }
                        else 
                        {
                            if (_ConsumerStatus == ConsumerStatus.ONLINE && idleCount >= 25 )
                            {
                                _ConsumerStatus = ConsumerStatus.ONLINE_IDLE;
                            }          
                        }
                    }
                    else
                    {
                        idleCount = 0;
                        _ConsumerStatus = ConsumerStatus.ONLINE;
                        MemoryQueue.EnqueueRange(objects);
                    }
                   
                }
                catch (Exception ex)
                {
                    Log.Error($"Consumer from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid}  could not get objects from queue.",exception: ex);
                    _ConsumerStatus = ConsumerStatus.ONLINE_IDLE;
                }
            }
        }

        public virtual async Task Run()
        {
            await Task.Delay(1000);

            try
            {
                LoadConsumerServerStatus();
                LoadConsumerClientStatus();
                LoadConsumer();

                while (_ConsumerStatus != ConsumerStatus.DISPOSE);

                Log.Info($"Consumer from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid} disposing...");
            }
            catch (Exception ex)
            {
                Log.Fatal($"Consumer from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid} had a fatal error and got closed", exception: ex);
                _ConsumerStatus = ConsumerStatus.DISPOSE;
            }
        }

    }

    public enum ConsumerStatus
    {
        DISPOSE = 0,
        OFFLINE = 1,
        ONLINE = 2,
        ONLINE_IDLE = 3,
    }
}
