using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Interact.Library.Structure
{
    public class Endpoint
    {
        public string Url { get; set; }
        public ICollection<KeyValuePair<string, string>> Headers { get; set; }
        public HttpMethod RequestMethod { get; set; }
    }
}
