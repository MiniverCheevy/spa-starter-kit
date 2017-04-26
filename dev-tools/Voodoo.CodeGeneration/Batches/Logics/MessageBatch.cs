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
            ThrowIfNotFound(Token.Data, Token.Logic);
            GetTargetFrom(Token.Data);
        }

        public override void Build()
        {
            logic.AddFile(new MessageFile(logic, type));
            logic.AddFile(new RepositoryFile(logic, type));
            logic.AddFile(new MapperFile(logic, type));
            logic.AddFile(new ExtensionFile(logic, type));
            logic.AddFile(new MessagesFile(logic, type));

            tests?.AddFile(new MappingTestsFile(tests, type, logic));
            if (contextType != null)
                addNameValuePairs();
        }
    }
}