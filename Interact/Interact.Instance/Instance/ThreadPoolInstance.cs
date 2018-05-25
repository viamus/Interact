using Interact.Library.Interfaces;
using Interact.Library.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interact.Instance
{
    public class ThreadPoolInstance<T>
        where T : IQueueObject
    {
        private Consumer<T> _ConsumerBluePrint { get; set; }
        private Worker<T> _WorkerBluePrint { get; set; }

        public ThreadPoolInstance(Consumer<T> consumerBluePrint, Worker<T> workerBluePrint)
        {
            this._ConsumerBluePrint = consumerBluePrint;
            this._WorkerBluePrint = workerBluePrint;
        }

        public async Task Run()
        {
            Task consumer = _ConsumerBluePrint.Run();
            List<Task> workers = new List<Task>();
            while (true)
            {
                if (consumer.IsCompleted || consumer.IsFaulted || consumer.IsCanceled || consumer.IsCompletedSuccessfully)
                {
                    break;
                }

                while (workers.Count < _ConsumerBluePrint.MemoryQueue.Count)
                {
                    T message;
                    if (_ConsumerBluePrint.MemoryQueue.TryDequeue(out message))
                    {
                        workers.Add(_WorkerBluePrint.Run(message));
                    }
                }

                workers.RemoveAll(w => w.IsCompleted || w.IsFaulted || w.IsCanceled || w.IsCompletedSuccessfully);
            }
        }
    }
}
