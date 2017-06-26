using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fernweh.Tests.CodeGeneration.Helpers.ModelBuilders.TestClasses
{
    public class DateTest
    {
        public const string Error = "AAAAAHHHHHHHH";
    
        [Range( typeof(DateTime), "1/1/1980", "1/1/2050", ErrorMessage = DateTest.Error)]
        public DateTime SomeDate { get; set; }
    }
}
