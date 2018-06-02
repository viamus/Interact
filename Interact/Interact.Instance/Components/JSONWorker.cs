using Interact.Instance.Components.Amazon.Model;
using Interact.Library.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Interact.Instance.Components
{
    public class JSONWorker : Worker<JSONString>
    {
        public JSONWorker(string threadGroup, Endpoint endpoint, WorkerCompletedEvent callback, ICollection<Notification> notifications = null)
            :base(threadGroup, endpoint, callback, notifications)
        {

        }

        public override async Task<WorkerResult> DoWorkAsync(JSONString request)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(request.Message, Encoding.UTF8, "application/json");

                var requestMessage = new HttpRequestMessage(_Endpoint.RequestMethod, _Endpoint.Url);

                _Endpoint.Headers?.ToList().ForEach(header =>
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                });

                var result = await client.SendAsync(requestMessage);

                return new WorkerResult { RequestResult = result.Content.ReadAsStringAsync().Result, Status = result.StatusCode };
            }
        }
    }
}
