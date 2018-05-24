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

        public string ThreadGroup { get; set; } = $"DefaultGroup";
        private ICollection<HttpStatusCode> _SuccessHttpStatus { get; set; }

        public Worker(string threadGroup ,Endpoint endpoint, WorkerCompletedEvent callback, ICollection<Notification> notify = null, ICollection<HttpStatusCode> successHttpStatus = null)
        {
            ThreadGroup = threadGroup;
            _Endpoint = endpoint;
            _Notifications = notify;
            _Callback = callback;
            _SuccessHttpStatus = successHttpStatus;
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

        public virtual async Task Run(T payload)
        {
            _SuccessHttpStatus = _SuccessHttpStatus ?? new List<HttpStatusCode> { HttpStatusCode.OK };

            try
            {
                var result = DoWorkAsync(payload).Result;

                if (_SuccessHttpStatus.Contains(result.Status))
                {
                    await SendNotifications( NotificationType.SUCCESS, result);
                    await _Callback(payload.GetObjectIdentifier());

                }
                else
                {
                    await SendNotifications(NotificationType.FAILURE, result);
                }

                Log.Info($"Worker from ThreadGroup '{this.ThreadGroup}' message {payload.GetObjectIdentifier()} processed.");
            }
            catch(Exception ex)
            {
                Log.Fatal($"Worker from ThreadGroup '{this.ThreadGroup}' had a fatal error and was killed", exception: ex);
                await SendNotifications(NotificationType.ERROR, new WorkerResult { Exception = ex });
            }
        }
    }
}
