using System;
using System.Collections.Generic;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.CodeGeneration.Templates.PCL;
using Aurelia = Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Aurelia;
using Angular = Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Angular;
using Angular2 = Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Angular2;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks
{
    public class WebFramworkBatchFactory
    {
        public static string GetModuleName()
        {
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular1)
            {
                return "app";
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
            {
                return @"App";
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
            {
                return "app";
            }
            throw new NotImplementedException("No Module Name Configured");
        }

        public static string GetMessagesReferencePath()
        {
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular1)
            {
                return "messages.ts";
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
            {
                return @"../../src/messages.ts";
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.Angular2)
            {
                return "messages.ts";
            }
            throw new NotImplementedException("No Message Reference Path Configured");
        }


        public static TypeScriptServiceBatchFileBase GetTypeServiceFile(ProjectFacade web, Resource[] items, string path)
        {
            //TODO:refactor
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular1)
            {
                throw new NotImplementedException();
                //	var service = new Angular.TsServiceFile(web, item, path);
                //service.Template = new Angular.TsServiceTemplate { File = service };
                //return service;
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
            {
                var service = new Aurelia.TsServiceBatchFile(web, items, path);
                return service;
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.Angular2)
            {
                throw new NotImplementedException();
                //var service = new Angular2.TsServiceFile(web, item, path);
                //service.Template = new Angular2.TsServiceTemplate { File = service };
                //return service;
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.React)
            {

                var service = new React.TsServiceBatchFile(web, items, path);
                return service;
            }
            Console.WriteLine("Web Framework unexpected, not supported or not configured.");
            return null;
        }
    }
}