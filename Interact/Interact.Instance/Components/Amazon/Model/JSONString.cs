using Interact.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Components.Amazon.Model
{
    public class JSONString: IQueueObject
    {
        public dynamic Message;
        public string Identifier { get; set; }

        public JSONString(dynamic message, string identifier)
        {
            this.Message = message;
            this.Identifier = identifier;
        }

        public string GetObjectIdentifier()
        {
            return this.Identifier;
        }
    }
}
