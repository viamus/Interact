using Interact.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Interact.Library.Components
{
    public delegate Task WorkerCompletedEvent(object identifier);

    public abstract class Worker<T> where T:IWorkerObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Endpoint _Endpoint;
        private ICollection<Notification> _Notifications;
        private WorkerCompletedEvent _Callback = null;

        public string ThreadGuid { get; set; } = Guid.NewGuid().ToString();
        private string ThreadGroup { get; set; } = $"DefaultGroup";

        public Worker(string threadGroup ,Endpoint endpoint, WorkerCompletedEvent callback, ICollection<Notification> notify = null)
        {
            ThreadGroup = threadGroup;
            _Endpoint = endpoint;
            _Notifications = notify;
            _Callback = callback;
        }

        /// <summary>
        /// Returns if the task was completed sucessfully
        /// </summary>
        /// <param name="payload">Message that will be send.</param>
        /// <returns></returns>
        public abstract Task<bool> DoWork(T payload, Endpoint endpoint, ICollection<Notification> notifications = null);

        public virtual async Task Run(T payload)
        {
            List<Task> sender = new List<Task>();

            try
            {
                if (DoWork(payload, _Endpoint, _Notifications).Result)
                {
                    await _Callback(payload.GetObjectIdentifier());
                }
                else
                {
                    if (!_Notifications.NullOrEmpty())
                    {
                        _Notifications.Where(n => n.Type == NotificationType.FAILURE).ToList().ForEach((notification) =>
                          {
                              sender.Add(notification.Notify());
                          });

                        Task.WaitAll(sender.ToArray());
                    }
                }

                Log.Info($"Worker from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid} message {payload.GetObjectIdentifier()} processed.");
            }
            catch(Exception ex)
            {
                Log.Fatal($"Worker from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid} had a fatal error and was killed", exception: ex);

                if (!_Notifications.NullOrEmpty())
                {
                    _Notifications.Where(n => n.Type == NotificationType.ERROR).ToList().ForEach((notification) =>
                    {
                        sender.Add(notification.Notify());
                    });

                    Task.WaitAll(sender.ToArray());
                }
            }
        }
    }
}
