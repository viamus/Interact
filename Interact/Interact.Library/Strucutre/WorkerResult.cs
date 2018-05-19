using System.Net;

namespace Interact.Library.Structure
{
    public class WorkerResult
    {
        public string RequestResult { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
