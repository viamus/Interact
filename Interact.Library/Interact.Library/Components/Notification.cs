using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interact.Library.Components
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

        public abstract void SendNotification();

        public virtual async Task Notify()
        {
            try
            {
                SendNotification();
            }
            catch(Exception ex)
            {
                Log.Error($"Notification of {this.Type.ToString()} from ThreadGroup '{this.ThreadGroup}' could not be sent.", exception: ex);
            }
        }

    }

    public enum NotificationType
    {
        SUCESS,
        ERROR,
        FAILURE
    }
}
