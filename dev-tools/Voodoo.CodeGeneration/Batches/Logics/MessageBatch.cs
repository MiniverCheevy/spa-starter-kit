using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras;
using Voodoo.CodeGeneration.Templates.Tests;

namespace Voodoo.CodeGeneration.Batches.Logics
{
    [Generates(Command = "message", Format = "message <entity>",
        Notes = "reads from Data, writes to Logic: message and helper files")]
    public class MessageBatch : Batch
    {
        public MessageBatch(string[] targetTypes = null)
            : base(targetTypes)
        {
            ThrowIfNotFound(Token.Data, Token.Logic, Token.Models);
            GetTargetFrom(Token.Data);
        }

        public override void Build()
        {
            data.AddFile(new DetailFile(data, type));
            data.AddFile(new RowFile(data, type));
            logic.AddFile(new RepositoryFile(logic, type));
            logic.AddFile(new MapperFile(logic, type, models));
            logic.AddFile(new ExtensionFile(logic, type));
            data.AddFile(new MessagesFile(data, type));

            tests?.AddFile(new MappingTestsFile(tests, type, logic));
            if (contextType != null)
                addNameValuePairs();
        }
    }
}