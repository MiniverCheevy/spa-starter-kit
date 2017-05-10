﻿using System.Text;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.React
{
    public class TsServiceBatchFile : TypeScriptServiceBatchFileBase
    {
        public override string FileName => "api.generated.ts";

        public TsServiceBatchFile(ProjectFacade project, Resource[] resources, string path)
            : base(project, resources, path)
        {
        }

        public override string GetFileContents()
        {
            var builder = new StringBuilder();

            builder.Append(@"//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************
import { CurrentUserService } from './services/current-user-service';
import { MessengerService } from './services/messenger-service';
import { AjaxService } from './services/ajax-service';
import * as Models from './models.generated';");

            foreach (var resource in Resources)
            {
                builder.AppendLine();
                builder.Append($@" export class {resource.Name}Prototype    {{
                    url: string = 'api/{resource.Name}';");

                foreach (var verb in resource.Verbs)
                {
                    var declarations = Builder.AddTypes(verb.RequestType, verb.ResponseType);
                    builder.Append($@"   
			public async {verb.Name.ToLower()} (request: Models.{declarations.RequestDeclaration}):
											Promise<Models.{declarations.ResponseDeclaration}>
			{{
            var error;
            try {{
			        MessengerService.incrementHttpRequestCounter();
                    var httpResponse = await AjaxService.build{verb.Name}Request(request, this.url)
                    .catch(function (err) {{
                                        error = err;
                                    }});
                    if(error == null)
                    {{
                        var response = await httpResponse.data;
                    
                        var out = <Models.IResponse>response;
                        MessengerService.showResponseMessage(out);
                        MessengerService.decrementHttpRequestCounter();
                        return out;
                    }}
            }}
            catch (err)
            {{
                if (err != null)
                    error = err;
            }};


             if (error != null)
            {{
                AjaxService.logError(error, this.url, (<any> new Error()).stack);

                var result = {{
                    isOk: false,
                    message: error.statusText
                }};

                MessengerService.decrementHttpRequestCounter();
                MessengerService.showResponseMessage(result);
                return result;
            }}
}}");
                }
                builder.AppendLine("}");
                builder.AppendLine($"export const {resource.Name} = new {resource.Name}Prototype();");
            }
            return builder.ToString();
        }
    }
}