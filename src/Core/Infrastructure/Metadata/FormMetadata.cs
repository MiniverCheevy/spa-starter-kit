using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Metadata
{

    
    //RangeAttribute
    //StringLengthAttribute
    //RequiredAttribute
    //CollectionMustHaveAtLeastOneItem
    //EnumIsRequired
    //GreaterThanZeroIntegerIsRequired
    //RequiredDateTime
    //RequiredGuid
    //RequiredInt
    //RequiredNonZeroInt

    public class FormMetadata:UiMetadata
    {
        public ValidationMetaData Date { get; set; }
        public ValidationMetaData Email { get; set; }
        public ValidationMetaData Length { get; set; }
        public ValidationMetaData Integer { get; set; }
        public ValidationMetaData Decimal { get; set; }
       

    }


    public class ValidationMetaData
    {
        public bool ShouldValidate { get; set; }
        public string Message { get; set; }
        public object Min { get; set; }
        public object Max { get; set; }
    }
}
