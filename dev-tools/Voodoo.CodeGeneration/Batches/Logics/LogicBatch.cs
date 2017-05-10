using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Templates.Logic.ProjectLevel;

namespace Voodoo.CodeGeneration.Batches.Logics
{
    [Generates(Command = "logic", Format = "logic [entity]",
        Notes =
            "reads from Data, writes to Logic: all commands, queries and helper files, ListHelper will include all enums in the project with the context and any entity with and Id and Name"
    )]
    public class LogicBatch : Batch
    {
        public LogicBatch(string[] targetTypes = null)
            : base(targetTypes)
        {
            ThrowIfNotFound(Token.Data, Token.Logic);
            GetTargetFrom(Token.Data, targetTypes != null);
        }

        public override void Build()
        {
            logic.AddRestResource(type.Name);
            logic.AddRestResource(type.PluralName);
            logic.AddFile(new RestResourcesFile(logic));

            if (contextType != null)
                addNameValuePairs();

            if (type == null)
                return;

            new CommandBatch(allTargets).Build();
            new QueryBatch(allTargets).Build();
        }
    }
}