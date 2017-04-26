using System;
using Voodoo.CodeGeneration.Templates.Logic.OperationLevel;
using Voodoo.CodeGeneration.Templates.Logic.ProjectLevel;
using Voodoo.CodeGeneration.Templates.Tests;

//namespace Voodoo.CodeGeneration.Models.Batches.Logics

namespace Voodoo.CodeGeneration.Batches.Logics
{
    //[Generates(Command = "command", Format = "command <entity>",
    //    Notes =
    //        "reads from Data, writes to Logic: add, update and delete commands, messages and helper.  If entity has IsActive property delete will be soft."
    //    )]
    public class CommandBatch : Batch
    {
        public CommandBatch(string[] targetTypes = null) : base(targetTypes)
        {
            ThrowIfNotFound(Token.Data, Token.Logic);
            GetTargetFrom(Token.Data);
        }

        public override void Build()
        {
            if (!type.HasId)
                Console.WriteLine("WARNING type has no id, update and delete commands will be truncated");

            logic.AddRestResource($"{type.Name}Detail");
            logic.AddRestResource($"{type.Name}List");
            //logic.AddFile(new AddCommandFile(logic, type));
            //logic.AddFile(new UpdateCommandFile(logic, type));
            logic.AddFile(new SaveCommandFile(logic, type));
            logic.AddFile(new DeleteCommandFile(logic, type));
            logic.AddFile(new RestResourcesFile(logic));

            new MessageBatch(allTargets).Build();

            if (tests == null)
                return;

            tests.AddFile(new AddSaveCommandTestsFile(tests, type, logic));
            tests.AddFile(new UpdateSaveCommandTestsFile(tests, type, logic));
            tests.AddFile(new DeleteCommandTestsFile(tests, type, logic));
            tests.AddFile(new TestHelperSaveFile(tests, type, logic));
        }
    }
}