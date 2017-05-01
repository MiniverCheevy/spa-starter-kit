﻿using System;
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
    public class WebFrameworkFileFactory
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
            else if (Vs.Helper.Solution.WebFramework == WebFramework.Angular2)
            {
                return "app";
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.React)
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

        
        public static CodeFile GetTypeScriptModelsFile(ProjectFacade web, Type[] types, string path)
        {
            if (Vs.Helper.Solution.WebFramework == WebFramework.Angular1)
            {
                var service = new Angular.TypeScriptModelsFile(web, types, path);
                return service;
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.Aurelia)
            {
                var service = new Aurelia.TypeScriptModelsFile(web, types, path);
                return service;
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.Angular2)
            {
                var service = new Angular2.TypeScriptModelsFile(web, types, path);
                return service;
            }
            else if (Vs.Helper.Solution.WebFramework == WebFramework.React)
            {
                var service = new React.TypeScriptModelsFile(web, types, path);
                return service;
            }
            return null;
        }
    }
}