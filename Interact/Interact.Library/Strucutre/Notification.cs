using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interact.Library.Structure
{
    public abstract class Notification
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NotificationType Type { get; set; }
        public string ThreadGroup { get; set; } = $"DefaultGroup";


        public Notification(string threadGroup, NotificationType type)
        {
            Type = type;
            ThreadGroup = threadGroup;
        }

        public abstract void SendNotification(WorkerResult notifyItem);

        public virtual async Task Notify(WorkerResult notifyItem)
        {
            try
            {
                SendNotification(notifyItem);
            }
            catch(Exception ex)
            {
                Log.Error($"Notification of {this.Type.ToString()} from ThreadGroup '{this.ThreadGroup}' could not be sent.", exception: ex);
            }
        }
    }

    public enum NotificationType
    {
        SUCCESS,
        ERROR,
        FAILURE
    }
}
