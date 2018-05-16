using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Library.Components
{
    public class JSONConsumer : Consumer<string>
    {
        public JSONConsumer(string threadGroup, int maxMemoryQueueObjects):base(threadGroup, maxMemoryQueueObjects)
        {

        }

        public override ConsumerStatus GetConsumerServerStatus()
        {
            throw new NotImplementedException();
        }

        public override ICollection<string> GetObjectsFromQueue(int maxObjects)
        {
            throw new NotImplementedException();
        }

        public override void LockQueue()
        {
            throw new NotImplementedException();
        }

        public override void ReleaseQueue()
        {
            throw new NotImplementedException();
        }

        public override void RemoveObjectFromQueue(string @object)
        {
            throw new NotImplementedException();
        }

        protected override void NotifyConsumerClientStatus()
        {
            throw new NotImplementedException();
        }
    }
}
