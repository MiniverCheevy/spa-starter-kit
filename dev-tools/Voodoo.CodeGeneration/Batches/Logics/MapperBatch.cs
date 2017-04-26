using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras;
using Voodoo.CodeGeneration.Templates.Tests;

namespace Voodoo.CodeGeneration.Batches.Logics
{
    [Generates(Command = "mapper", Format = "mapper <entity>",
        Notes = "reads from Data and Logic, writes to Logic: regenerates mapper file to pick up new properties")]
    public class MapperBatch : Batch
    {
        public MapperBatch(string[] targetTypes = null)
            : base(targetTypes)
        {
            ThrowIfNotFound(Token.Data, Token.Logic);
            GetTargetFrom(Token.Data);
        }

        public override void Build()
        {
            logic.AddFile(new MapperFile(logic, type));

            if (tests == null) return;

            tests.AddFile(new MappingTestsFile(tests, type, logic));
        }
    }
}