using System.Collections.Generic;

namespace Fernweh.Core.Operations.Lists
{
    public class ListsRequest
    {
        public bool IncludeInactive { get; set; }
        public List<Lists> Lists { get; set; }

        public ListsRequest()
        {
            Lists = new List<Lists>();
        }
    }
}