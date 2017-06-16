using System;

namespace Core.Infrastructure.Metadata
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HiddenAttribute:Attribute
    {
        
    }
}
