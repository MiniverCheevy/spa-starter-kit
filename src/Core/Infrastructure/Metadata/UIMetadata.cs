using System.Collections.Generic;
using Voodoo.Infrastructure.Notations;

namespace Core.Infrastructure.Metadata
{

   
    [Client]
    public class UIMetadata
    {
        public string PropertyName { get; set; }
        public string JsName { get; set; }
        public string DisplayName { get; set; }
        public string DisplayFormat { get; set; } = "text";
        public ValidationMetaData Email { get; set; }
        public ValidationMetaData Length { get; set; }
        public ValidationMetaData Date { get; set; }
        public ValidationMetaData Int { get; set; }
        public ValidationMetaData Decimal { get; set; }
        public ValidationMetaData Required { get; set; }        
        public bool IsReadOnly { get; set; }
        public bool IsHidden { get; set; }
        public bool DoNotSort { get; set; }
        public object Control { get; set; }

        public bool IsValid { get; set; }
        public string ValidationMessage { get; set; }
    }

    [Client]
    public class ValidationMetaData
    {
        public bool ShouldValidate { get; set; }
        public string Message { get; set; }
        public object Min { get; set; }
        public object Max { get; set; }
    }
}