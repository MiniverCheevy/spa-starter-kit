using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Metadata
{
    //HiddenAttribute
    //DisplayFormat
    //DisplayAttribute



    public class UiMetadata
    {
        public string ColumnName { get; set; }
        public string DisplayName { get; set; }
        public string Format { get; set; } = "Text";
        public bool IsReadOnly { get; set; }
        public bool IsHidden { get; set; }
    }
}
