using Amazon;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Components.Amazon
{
    public class AWSQueueConfiguration
    {
        private AWSComponent _Cloud { get; set; }

        private string _QueueEndpoint { get; set; }

        private string _Region { get; set; }

        private int _MaxMemoryQueueObjects { get; set; }

        private int _VisibilityTimeout { get; set; }

        private int _WaitTimeSeconds { get; set; }

        public AWSQueueConfiguration(AWSComponent cloud,
            string queueEndpoint, string region, int visibilityTimeout,
            int maxNumberOfMessages, int waitTimeSeconds = 0)
        {
            _Cloud = cloud;
            _QueueEndpoint = queueEndpoint;
            _Region = region;
            _MaxMemoryQueueObjects = maxNumberOfMessages;
            _VisibilityTimeout = visibilityTimeout;
            _WaitTimeSeconds = waitTimeSeconds;
        }

        public ICollection<Message> RetriveQueueObjects()
        {
            return _Cloud.RetriveQueueObjects(_QueueEndpoint, RegionEndpoint.GetBySystemName(_Region),
                                        _VisibilityTimeout, _MaxMemoryQueueObjects, _WaitTimeSeconds);
        }

        public void RemoveQueueObjects(string receiptHandle)
        {
            _Cloud.RemoveQueueObjects(_QueueEndpoint, RegionEndpoint.GetBySystemName(_Region), receiptHandle);
        }
    }
}