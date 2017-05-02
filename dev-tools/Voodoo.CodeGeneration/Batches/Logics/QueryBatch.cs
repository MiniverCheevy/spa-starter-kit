using System;
using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Templates.Logic.OperationLevel;
using Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras;
using Voodoo.CodeGeneration.Templates.Logic.ProjectLevel;
using Voodoo.CodeGeneration.Templates.Tests;

namespace Voodoo.CodeGeneration.Batches.Logics
{
    [Generates(
            Command = "query",
            Format = "query <entity>",
            Notes =
                "reads from Data, writes to Logic: query, helper and message files.  If entity has int Id and string Name it will also be added to the ListsQuery."
        )
    ]
    public class QueryBatch : Batch
    {
        public QueryBatch(string[] targetTypes = null)
            : base(targetTypes)
        {
            ThrowIfNotFound(Token.Data, Token.Logic);
            GetTargetFrom(Token.Data);
        }

        public override void Build()
        {
            if (!type.HasId)
                Console.WriteLine("WARNING type has no id, detail query will be truncated");

            logic.AddRestResource(type.Name);
            logic.AddRestResource(type.PluralName);
            logic.AddFile(new ListQueryFile(logic, type));
            logic.AddFile(new DetailQueryFile(logic, type));
            data.AddFile(new QueryResponseFile(data, type));
            data.AddFile(new QueryRequestFile(data, type));
            logic.AddFile(new RestResourcesFile(logic));

            var message = new MessageBatch(allTargets);
            message.Build();

            if (tests == null)
                return;

            tests.AddFile(new QueryTestsFile(tests, type, logic));
            tests.AddFile(new MappingTestsFile(tests, type, logic));
            tests.AddFile(new TestHelperFile(tests, type, logic));
        }
    }
}