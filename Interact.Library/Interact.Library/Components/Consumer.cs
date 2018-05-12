using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interact.Library.Components
{
    public abstract class Consumer<T>
    {
        public string ThreadGuid { get; set; } = Guid.NewGuid().ToString();

        public string ThreadName { get; set; } = $"DefaultConsumer";

        public int MaxThreadPool { get; set; } = 1;

        public int MaxMemoryQueueObjects { get; set; } = 10;

        public Consumer(int ThreadName, int MaxThreadPool, int MaxMemoryQueueObjects)
        {
            if (!MaxThreadPool.ValidRange(1, 100))
            {
                throw new ArgumentOutOfRangeException("MaxThreadPool must be a integer in the range of 1 to 100.");
            }

            if(!MaxMemoryQueueObjects.ValidRange(10, 1000))
            {
                throw new ArgumentOutOfRangeException("MaxMemoryQueueObjects must be a integer in the range of 10 to 1000.");
            }

        }

        public abstract ICollection<T> GetObjectsFromQueue();

        public abstract void RemoveObjectFromQueue(T @object);

        public abstract void LockQueue();

        public abstract void ReleaseQueue();

        public async Task Run()
        {

        }

    }
}
