using Core;
using Voodoo.CodeGeneration.Models;
using Core.Context;
using Microsoft.EntityFrameworkCore;
namespace Core.Operations.TestClasses.Extras
{
    public class TestClassRepository
    {
        private DatabaseContext context;
        public TestClassRepository(DatabaseContext context)
        {
            this.context = context;
        }
    }
}

