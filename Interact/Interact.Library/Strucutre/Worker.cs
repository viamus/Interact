using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Interact.Library.Interfaces;
using System.Net;

namespace Interact.Library.Structure
{
    public delegate Task WorkerCompletedEvent(object identifier);

    public abstract class Worker<T> where T:IQueueObject
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected Endpoint _Endpoint;
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

        public abstract Task<WorkerResult> DoWorkAsync(T payload);

        protected virtual async Task SendNotifications(NotificationType type, WorkerResult result)
        {
            if (!_Notifications.NullOrEmpty())
            {
                List<Task> sender = new List<Task>();

                _Notifications.Where(n => n.Type == NotificationType.FAILURE).ToList().ForEach((notification) =>
                {
                    sender.Add(notification.Notify(result));
                });

                Task.WaitAll(sender.ToArray());
            }
        }

        public virtual async Task Run(T payload, ICollection<HttpStatusCode> successHttpStatus)
        {
            successHttpStatus = successHttpStatus ?? new List<HttpStatusCode> { HttpStatusCode.OK };

            try
            {
                var result = DoWorkAsync().Result;

                if (successHttpStatus.Contains(result.Status))
                {
                    await SendNotifications( NotificationType.SUCCESS, result);
                    await _Callback(payload.GetObjectIdentifier());

                }
                else
                {
                    await SendNotifications(NotificationType.FAILURE, result);
                }

                Log.Info($"Worker from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid} message {payload.GetObjectIdentifier()} processed.");
            }
            catch(Exception ex)
            {
                Log.Fatal($"Worker from ThreadGroup '{this.ThreadGroup}' and ThreadGuid:'{this.ThreadGuid} had a fatal error and was killed", exception: ex);
                await SendNotifications(NotificationType.ERROR, new WorkerResult { Exception = ex });
            }
        }
    }
}
