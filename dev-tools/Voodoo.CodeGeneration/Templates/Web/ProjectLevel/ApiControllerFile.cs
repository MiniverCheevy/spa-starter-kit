﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.Infrastructure;
using Voodoo.Messages;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel
{
    public class ApiControllerFile : TypedCodeFile
    {
        public IEnumerable<Resource> Resources { get; set; }

        public override string FileName => "Api.generated.cs";

        public ApiControllerFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Name = $"{Name}Controller";
        }

        public ApiControllerFile(ProjectFacade project, TypeFacade type, IEnumerable<Resource> resources)
            : base(project, type)
        {
            Resources = resources;
            OverwriteExistingFile = true;
            PageSpecificUsingStatements.Add("Voodoo.Messages");
            PageSpecificUsingStatements.Add($"{project.RootNamespace}.Infrastructure.ExecutionPipeline");
            PageSpecificUsingStatements.Add($"{project.RootNamespace}.Infrastructure.ExecutionPipeline.Models");
            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.Linq");
            PageSpecificUsingStatements.Add("System.Threading.Tasks");
            PageSpecificUsingStatements.Add("Microsoft.AspNetCore.Mvc");
            PageSpecificUsingStatements.Add("Microsoft.AspNetCore.Authorization");
            PageSpecificUsingStatements.Add("Microsoft.AspNetCore.Http");
            PageSpecificUsingStatements.Add("Voodoo");
            foreach (var resource in resources)
                foreach (var verb in resource.Verbs)
                {
                    addNamespaces(verb.OperationType);
                    addNamespaces(verb.RequestType);
                    addNamespaces(verb.ResponseType);
                }
        }

        public override string GetFolder()
        {
            return @"Controllers\Api\";
        }

        private void addNamespaces(Type type)
        {
            if (type == null)
                return;

            PageSpecificUsingStatements.Add(type.Namespace);
            if (type.GenericTypeArguments.Any())
                foreach (var arg in type.GenericTypeArguments)
                    addNamespaces(arg);
        }

        public override string GetFileContents()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"/***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
***************************************************************/
");
            foreach (var item in UsingStatements)
                builder.AppendLine($"using {item};");

            builder.AppendLine($"namespace {Namespace}");
            builder.AppendLine("{");
            foreach (var resource in Resources)
            {
                builder.AppendLine();
                builder.AppendLine("[Route(\"api/[controller]\")]");

                var isBinary = resource.Verbs.Any(c => c.ResponseType == typeof(BinaryResponse));

                if (isBinary)
                    builder.AppendLine($"public class {resource.Name}Controller : FileController");
                else
                    builder.AppendLine($"public class {resource.Name}Controller : ApiControllerBase");

                if (isBinary && (resource.Verbs.Count() > 1 || resource.Verbs.Any(c => c.Method != Verb.Get)))
                {
                    Vs.Helper.Log.Add(new LogEntry { Level = Logging.LogLevels.Info, Message = $"Typically a report resource should have only one get method, this is not the case for {resource.Name}, you may have some problems" });
                }

                builder.AppendLine("{");
                foreach (var verb in resource.Verbs)
                {
                    appendMethod(builder, verb, resource);
                }
                builder.AppendLine("}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
        private void appendMethod(StringBuilder builder, RestMethod verb, Resource resource)
        {
            if (verb.ResponseType == typeof(BinaryResponse))
            {
                appendBinaryMethod(builder, verb, resource);
            }
            else
            {
                appendRestMethod(builder, verb, resource);
            }
        }

        private void appendBinaryMethod(StringBuilder builder, RestMethod verb, Resource resource)
        {
            builder.AppendLine($"{verb.Attribute}");

            builder.AppendLine($"public async Task<ActionResult> {verb.Name}");
            builder.AppendLine($"({verb.Parameter} {verb.RequestTypeName} request)");
            builder.AppendLine("{");
            builder.AppendLine();

            builder.AppendLine($@" var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
                     <{verb.RequestTypeName}, {verb.ResponseTypeName}>
                    {{
                        Command = new {verb.OperationTypeName}(request),
                        Context = HttpContext,
                        ModelState = ModelState,
                        Request = request,
                        SecurityContext = new SecurityContext {{ AllowAnonymouse = {
                    verb.AllowAnonymous.ToString().ToLower()
                }, Roles=new string[] {{ {verb.RoleArrayString} }} }}
                    }};");

            builder.AppendLine(
                $"var pipeline = new ExcecutionPipeline<{verb.RequestTypeName}, {verb.ResponseTypeName}>");
            builder.AppendLine($" (state);");
            builder.AppendLine($"await pipeline.ExecuteAsync();");

            builder.AppendLine($"return HandleBinaryResponse(state.Response);");

            builder.AppendLine("}");
        }

        private void appendRestMethod(StringBuilder builder, RestMethod verb, Resource resource)
        {
            builder.AppendLine($"{verb.Attribute}");

            builder.AppendLine($"public async Task<{verb.ResponseTypeName}> {verb.Name}");
            builder.AppendLine($"({verb.Parameter} {verb.RequestTypeName} request)");
            builder.AppendLine("{");
            builder.AppendLine();

            builder.AppendLine($@" var state = new Infrastructure.ExecutionPipeline.Models.ExecutionState
                     <{verb.RequestTypeName}, {verb.ResponseTypeName}>
                    {{
                        Command = new {verb.OperationTypeName}(request),
                        Context = HttpContext,
                        ModelState = ModelState,
                        Request = request,
                        SecurityContext = new SecurityContext {{ AllowAnonymouse = {
                            verb.AllowAnonymous.ToString().ToLower()
                        }, Roles=new string[] {{ {verb.RoleArrayString} }} }}
                    }};");

            builder.AppendLine(
                $"var pipeline = new ExcecutionPipeline<{verb.RequestTypeName}, {verb.ResponseTypeName}>");
            builder.AppendLine($" (state);");
            builder.AppendLine($"await pipeline.ExecuteAsync();");

            builder.AppendLine($"return state.Response;");
            builder.AppendLine("}");
        }
    }
}