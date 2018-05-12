using Amazon.Runtime;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Library.Components
{
    public class AWSComponent
    {
        private BasicAWSCredentials Credentials { get; set; }

        public AWSComponent(string accessKey, string secretKey)
        {
            Credentials = new BasicAWSCredentials(accessKey, secretKey);
        }

        public void RetriveSQSObjects(string queueUrl, int visibilityTimeout, int waitTimeSeconds, int MaxNumberOfMessages = 10)
        {
            var receiveMessageRequest = new ReceiveMessageRequest();
            receiveMessageRequest.QueueUrl = queueUrl;
        }
    }
}
