using Interact.Instance.Data.Interface;
using Interact.Library.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Components
{
    public class JSONConsumer : Consumer<string>
    {
        private AWSComponent _CloudComponent;
        private IServiceProvider _Services;

        public JSONConsumer(IServiceProvider services, string threadGroup, int maxMemoryQueueObjects):base(threadGroup, maxMemoryQueueObjects)
        {
            _Services = services;
        }

        public void LoadCloudComponent(string accessKey, string secretKey)
        {
            _CloudComponent = new AWSComponent(accessKey, secretKey);
        }

        public override ConsumerStatus GetConsumerServerStatus()
        {
            var consumerDal = _Services.GetService<IConsumerDAL>();

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
