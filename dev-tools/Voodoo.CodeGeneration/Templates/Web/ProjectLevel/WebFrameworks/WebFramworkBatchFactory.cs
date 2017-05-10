using System;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Aurelia;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks
{
    public class WebFramworkBatchFactory
    {
        public static string GetModuleName()
        {
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular1)
                return "app";
            if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
                return @"App";
            if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
                return "app";
            throw new NotImplementedException("No Module Name Configured");
        }

        public static string GetMessagesReferencePath()
        {
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular1)
                return "messages.ts";
            if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
                return @"../../src/messages.ts";
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular2)
                return "messages.ts";
            throw new NotImplementedException("No Message Reference Path Configured");
        }


        public static TypeScriptServiceBatchFileBase GetTypeServiceFile(ProjectFacade web, Resource[] items,
            string path)
        {
            //TODO:refactor
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular1)
                throw new NotImplementedException();
            if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
            {
                var service = new TsServiceBatchFile(web, items, path);
                return service;
            }
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular2)
            {
                throw new NotImplementedException();
                //var service = new Angular2.TsServiceFile(web, item, path);
                //service.Template = new Angular2.TsServiceTemplate { File = service };
                //return service;
            }
            if (Vs.Helper.Solution.WebFramework == WebFramework.React)
            {
                var service = new React.TsServiceBatchFile(web, items, path);
                return service;
            }
            Console.WriteLine("Web Framework unexpected, not supported or not configured.");
            return null;
        }
    }
}