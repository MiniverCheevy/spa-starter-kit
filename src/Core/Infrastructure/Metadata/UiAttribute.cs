using System;

namespace Core.Infrastructure.Metadata
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UIAttribute:Attribute
    {
        public bool IsHidden { get; set; }
        public bool IsReadOnly { get; set; }
        public DisplayFormat DisplayFormat { get; set; } = DisplayFormat.Text;
    }
}
