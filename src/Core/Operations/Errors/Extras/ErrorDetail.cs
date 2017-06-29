using System.Collections.Generic;
using Voodoo.Messages;

namespace Core.Operations.Errors.Extras
{
    public class ErrorDetail : ErrorRow
    {
        public string Details { get; set; }
        public string Host { get; set; }

        public string Url { get; set; }


        public List<Grouping<NameValuePair>> Items { get; set; }

        public ErrorDetail()
        {
            Items = new List<Grouping<NameValuePair>>();
        }
    }
}