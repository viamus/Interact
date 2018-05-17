using Interact.Instance.Data.Interface;
using Interact.Library.Structure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;
using Interact.Instance.Exceptions;

namespace Interact.Instance.Components.Amazon
{
    public class JSONConsumer : Consumer<string>
    {
        private AWSQueueConfiguration _QueueConfiguration;
        private IServiceProvider _Services;

        public JSONConsumer(IServiceProvider services, AWSQueueConfiguration queue, 
                            string threadGroup, int maxMemoryQueueObjects):base(threadGroup, maxMemoryQueueObjects)
        {
            _Services = services;
            _QueueConfiguration = queue;
        }

        public override ConsumerStatus GetConsumerServerStatus()
        {
            using (var consumerDal = _Services.GetService<IConsumerDAL>())
            {
                return consumerDal.GetThreadGroupServerStatus(this.ThreadGroup);
            }
        }

        public override ICollection<string> GetObjectsFromQueue(int maxObjects)
        {
            var messages = _QueueConfiguration.RetriveQueueObjects();
            return messages.Select(m => 
            {
                LockMessage(m.MessageId);
                return JsonConvert.SerializeObject(m);

           }).ToList();
        }

        private void LockMessage(string identity)
        {
            var redis = _Services.GetService<IDistributedCache>();
            if (redis.GetString(identity) != null)
            {
                DistributedCacheEntryOptions opcoesCache =
                       new DistributedCacheEntryOptions();
                        opcoesCache.SetAbsoluteExpiration(
                            TimeSpan.FromMinutes(1));

                redis.SetString(identity, "Locked", opcoesCache);
            }
            else
            {
                throw new RedisLockedKeyException("Message already locked.");
            }
        }

        private void ReleaseMessage(string identity)
        {
            var redis = _Services.GetService<IDistributedCache>();
            if (redis.GetString(identity) != null)
            {
                redis.Remove(identity);
            }
        }

        public override void RemoveObjectFromQueue(string identity)
        {
            _QueueConfiguration.RemoveQueueObjects(identity);
            ReleaseMessage(identity);
        }

        protected override void NotifyConsumerClientStatus()
        {
            throw new NotImplementedException();
        }
    }
}
