using Interact.Library.Interfaces;
using Interact.Library.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interact.Instance.Components.Amazon.Model
{
    public class InstanceThreadPool<T>
        where T : IQueueObject
    {
        private Consumer<T> _ConsumerBluePrint { get; set; }
        private Worker<T> _WorkerBluePrint { get; set; }

        public InstanceThreadPool(Consumer<T> consumerBluePrint, Worker<T> workerBluePrint)
        {
            this._ConsumerBluePrint = consumerBluePrint;
            this._WorkerBluePrint = workerBluePrint;
        }

        public async Task Run(int maxWorkers = 10)
        {
            if(maxWorkers < 0)
            {
                throw new ArgumentOutOfRangeException("Max number of workers must be a integer more than one.");
            }

            Task consumer = _ConsumerBluePrint.Run();
            List<Task> workers = new List<Task>();
            while (true)
            {
                if(consumer.IsCompleted || consumer.IsFaulted || consumer.IsCanceled || consumer.IsCompletedSuccessfully)
                {
                    break;
                }

                while(workers.Count < maxWorkers)
                {
                    workers.RemoveAll(w => w.IsCompleted || w.IsFaulted || w.IsCanceled || w.IsCompletedSuccessfully);

                    T message;
                    if (_ConsumerBluePrint.MemoryQueue.TryDequeue(out message))
                    {
                        workers.Add(_WorkerBluePrint.Run(message));
                    }
                }
            }
        }
    }
}
