using Interact.Instance.Data.Interface;
using Interact.Instance.Data.Postgresql.InteractDomain;
using Interact.Library.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Transactions;

namespace Interact.Instance.Data.Postgresql.Data
{
    public class ConsumerDAL : IConsumerDAL
    {
        public void Dispose()
        {
            this.Dispose();
        }

        public Library.Structure.ConsumerStatus GetThreadGroupServerStatus(string threadGroup)
        {
            using (var context = new InteractContext())
            {
                return context.CloudInstance.Where(c => c.Threadgroup == threadGroup).Select(c=> (Library.Structure.ConsumerStatus)c.ConsumerStatusId).FirstOrDefault();
            }
        }

        public void SetConsumerThreadStatus(string threadGroup, string indentifier, Library.Structure.ConsumerStatus clientStatus)
        {
            using (var context = new InteractContext())
            {


            }
        }
    }
}
