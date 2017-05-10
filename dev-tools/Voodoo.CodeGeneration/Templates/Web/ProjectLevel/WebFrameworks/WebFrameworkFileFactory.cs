using System;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Angular;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks
{
    public class WebFrameworkFileFactory
    {
        public static string GetModuleName()
        {
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular1)
                return "app";
            if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
                return @"App";
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular2)
                return "app";
            if (Vs.Helper.Solution.WebFramework == WebFramework.React)
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


        public static CodeFile GetTypeScriptModelsFile(ProjectFacade web, Type[] types, string path)
        {
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular1)
            {
                var service = new TypeScriptModelsFile(web, types, path);
                return service;
            }
            if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
            {
                var service = new Aurelia.TypeScriptModelsFile(web, types, path);
                return service;
            }
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular2)
            {
                var service = new Angular2.TypeScriptModelsFile(web, types, path);
                return service;
            }
            if (Vs.Helper.Solution.WebFramework == WebFramework.React)
            {
                var service = new React.TypeScriptModelsFile(web, types, path);
                return service;
            }
            return null;
        }
    }
}