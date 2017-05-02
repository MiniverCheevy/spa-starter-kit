﻿using System.Net.Http;
using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.React
{
    public class TsServiceBatchFile : TypeScriptServiceBatchFileBase
    {
        public TsServiceBatchFile(ProjectFacade project, Resource[] resources, string path)
            : base(project, resources, path)
        {

        }
        public override string FileName => "api.generated.ts";
        public override string GetFileContents()
        {
            var builder = new StringBuilder();

            builder.Append(@"//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************
import { CurrentUserService } from 'ClientApp/services/current-user-service';
import { MessengerService } from 'ClientApp/services/messenger-service';
import { AjaxService } from 'ClientApp/services/ajax-service';
import * as Models from './models.generated';");

            foreach (var resource in Resources)
            {
                builder.AppendLine();
                builder.Append($@" export class {resource.Name}    {{
                    url: string = 'api/{resource.Name}';");

                foreach (var verb in resource.Verbs)
                {
                    var declarations = Builder.AddTypes(verb.RequestType, verb.ResponseType);
                    builder.Append($@"   
			public async {verb.Name.ToLower()} (request: Models.{declarations.RequestDeclaration}):
											Promise<Models.{declarations.ResponseDeclaration}>
			{{
            try {{
			        MessengerService.incrementHttpRequestCounter();
                    var httpResponse = await AjaxService.build{verb.Name}Request(request, this.url);
                    var response = await httpResponse.json();
                    
                    var out = <Models.IResponse>response;
                    MessengerService.showResponseMessage(out);
                    MessengerService.decrementHttpRequestCounter();
                    return out;
            }}
            catch (err)
            {{
            AjaxService.logError(err, this.url, (<any>new Error()).stack);
           
            var result = {{
                isOk: false,
                message: err.statusText
            }};


            MessengerService.decrementHttpRequestCounter();
            MessengerService.showResponseMessage(result);
            return result;
        }}
}}");

                 
                }
                builder.AppendLine("}");
            }
            return builder.ToString();
        }
    }
}


