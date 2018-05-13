using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interact.Library.Components
{
    public abstract class Consumer<T>
    {
        private const int _ScheduleConsumerServerStatus = 30 * 1000;
        private const int _ScheduleConsumerClientStatus = 30 * 1000;

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ConcurrentBag<T> MemoryQueue { get; set; } = new ConcurrentBag<T>();

        private ConsumerStatus _ConsumerStatus { get; set; }

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

        public abstract void LockQueue();

        public abstract void ReleaseQueue();

        public abstract ConsumerStatus GetConsumerServerStatus();

        protected abstract void NotifyConsumerClientStatus();

        protected virtual async Task LoadConsumerServerStatus()
        {
            await Task.Delay(1000);

            while (_ConsumerStatus != ConsumerStatus.DISPOSE)
            {
                await Task.Delay(_ScheduleConsumerServerStatus);
                try
                {
                    _ConsumerStatus = GetConsumerServerStatus();
                }
                catch (Exception)
                {
                    Log.Error($"Consumer from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid}  could not load the consumer server status.");
                    _ConsumerStatus = ConsumerStatus.OFFLINE;
                }

                NotifyConsumerClientStatus();
            }
        }

        private async Task LoadConsumerClientStatus()
        {
            await Task.Delay(1000);

            while (_ConsumerStatus != ConsumerStatus.DISPOSE)
            {
                await Task.Delay(_ScheduleConsumerClientStatus);
                try
                {
                    NotifyConsumerClientStatus();
                }
                catch (Exception)
                {
                    Log.Error($"Consumer from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid}  could not send the consumer client status to the server.");
                    _ConsumerStatus = ConsumerStatus.OFFLINE;
                }
            }
        }

        protected virtual async Task LoadConsumer()
        {
            await Task.Delay(1000);

            while (_ConsumerStatus != ConsumerStatus.DISPOSE)
            {
                await Task.Delay(1000);
                try
                {
                    MemoryQueue.AddRange(GetObjectsFromQueue(MaxMemoryQueueObjects - MemoryQueue.Count));
                }
                catch (Exception)
                {
                    Log.Error($"Consumer from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid}  could not get objects from queue.");
                    _ConsumerStatus = ConsumerStatus.IDLE;
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
            catch(Exception ex)
            {
                Log.Fatal($"Consumer from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid} had a fatal error and got closed");
                _ConsumerStatus = ConsumerStatus.DISPOSE;
            }
        }

    }

    public enum ConsumerStatus
    {
        DISPOSE,
        OFFLINE,
        ONLINE,
        IDLE
    }
}
