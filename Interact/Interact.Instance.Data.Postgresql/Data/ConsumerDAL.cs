using Interact.Instance.Data.Interface;
using Interact.Instance.Data.Postgresql.InteractDomain;
using Interact.Library.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Transactions;
using Interact.Instance.Data.Exceptions;

namespace Interact.Instance.Data.Postgresql.Data
{
    public class ConsumerDAL : IConsumerDAL
    {
        public void Dispose()
        {
            this.Dispose();
        }

        private IServiceProvider _Services;

        public ConsumerDAL(IServiceProvider serivces)
        {
            _Services = serivces;
        }

        public TransactionScope CreateTransactionScope(IsolationLevel isolation = IsolationLevel.ReadCommitted, TimeSpan? timeout = null)
        {
            timeout = timeout ?? new TimeSpan(0, 0, 30);
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = timeout.Value });
        }

        public Library.Structure.ConsumerStatus GetThreadGroupServerStatus(string threadGroup)
        {
            using (var context = _Services.GetService(typeof(InteractContext)) as InteractContext)
            {
                return context.CloudInstance.Where(c => c.Threadgroup == threadGroup).Select(c=> (Library.Structure.ConsumerStatus)c.ConsumerStatusId).FirstOrDefault();
            }
        }

        public void SetConsumerThreadStatus(string threadGroup, string identifier, Library.Structure.ConsumerStatus clientStatus)
        {
            using (var context = _Services.GetService(typeof(InteractContext)) as InteractContext)
            {
                using (var scope = CreateTransactionScope())
                {
                    var clientInstance = context.CloudClientInstance.Where(c => c.Identifier == identifier).FirstOrDefault();

                    if(clientInstance == null)
                    {
                        var serverInstance = GetCloudInstance(threadGroup);

                        if (serverInstance == null)
                        {
                            throw new NotFoundException($"Consumer Instance not fount to the threadgroup = {threadGroup}");
                        }

                        context.CloudClientInstance.Add(new CloudClientInstance
                        {
                            Identifier = identifier,
                            CloudInstanceId = serverInstance.Id,
                            ConsumerStatusId = Convert.ToInt32(clientStatus),
                            Created = DateTime.Now,
                            Updated = DateTime.Now

                        });
                    }
                    else
                    {
                        clientInstance.ConsumerStatusId = Convert.ToInt32(clientStatus);
                        clientInstance.Updated = DateTime.Now;
                        context.CloudClientInstance.Update(clientInstance);
                       
                    }
                    context.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void GetConsumerInstance(string threadGroup)
        {
            using (var context = _Services.GetService(typeof(InteractContext)) as InteractContext)
            {
                var instance = context.CloudInstance.Where(c => c.Threadgroup == threadGroup).FirstOrDefault();
            }
        }

        private CloudInstance GetCloudInstance(string threadGroup)
        {
            using(var context = _Services.GetService(typeof(InteractContext)) as InteractContext)
            {
                return context.CloudInstance.Where(c => c.Threadgroup == threadGroup).FirstOrDefault();
            }
        }
    }
}
