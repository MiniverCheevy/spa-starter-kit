using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Infrastructure.Notations;

namespace Core.Infrastructure.Metadata
{
    //StringLengthAttribute
    //RequiredAttribute
    //CollectionMustHaveAtLeastOneItem
    //EnumIsRequired
    //GreaterThanZeroIntegerIsRequired

    //RangeAttribute
    //RequiredDateTime
    //RequiredInt
    //RequiredNonZeroInt
    //CompareAttribute
    //Create GreaterThan

    [Client]
    public class UIMetadata
    {
        public ValidationMetaData Email { get; set; }
        public ValidationMetaData Length { get; set; }
        public ValidationMetaData Date { get; set; }
        public ValidationMetaData Integer { get; set; }
        public ValidationMetaData Decimal { get; set; }
        public bool IsRequired { get; set; }
        public string PropertyName { get; set; }
        public string DisplayName { get; set; }
        public string Format { get; set; } = "Text";
        public bool IsReadOnly { get; set; }
        public bool IsHidden { get; set; }
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
