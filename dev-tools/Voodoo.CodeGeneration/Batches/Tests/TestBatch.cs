using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Templates.Tests;

namespace Voodoo.CodeGeneration.Batches.Tests
{
    [Generates(Command = "test", Format = "test <operation>",
        Notes = "generates a smoke test for the given operation")]
    public class TestBatch : Batch
    {
        public TestBatch(string[] targetTypes = null) : base(targetTypes)
        {
            ThrowIfNotFound(Token.Tests, Token.Logic);
            GetTargetFrom(Token.Logic);
        }

        public override void Build()
        {
            tests.AddFile(new TestFile(tests, type, logic));
        }
    }
}