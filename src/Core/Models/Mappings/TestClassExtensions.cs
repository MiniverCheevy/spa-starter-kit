using Core;
using Voodoo.CodeGeneration.Models;
using Core.Context;
using Core.Operations.TestClasses.Extras;
namespace Core.Models.Mappings
{
    public static partial class TestClassExtensions
    {
        public static TestClassRepository TestClassRepository(this DatabaseContext context)
        {
            return new TestClassRepository(context);
        }
        public static TestClassRow ToTestClassRow(this TestClass model)
        {
            var message = toTestClassRow(model, new TestClassRow());
            return message;
        }
        public static TestClass UpdateFrom(this  TestClass model, TestClassRow message)
        {
            return updateFromTestClassRow(message, model);
        }
        public static TestClassDetail ToTestClassDetail(this TestClass model)
        {
            var message = toTestClassDetail(model, new TestClassDetail());
            return message;
        }
        public static TestClass UpdateFrom(this  TestClass model, TestClassDetail message)
        {
            return updateFromTestClassDetail(message, model);
        }
    }
}

