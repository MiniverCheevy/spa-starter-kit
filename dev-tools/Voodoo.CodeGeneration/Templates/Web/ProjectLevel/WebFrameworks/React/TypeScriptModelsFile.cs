﻿using System;
using System.Text;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.React
{

    public class TypeScriptModelsFile : TypeScriptModelsFileBase
    {
        public TypeScriptModelsFile(ProjectFacade project, Type[] types, string path) : base(project, types, path)
        {
            Builder.AddTypes(types);
            OverwriteExistingFile = true;
        }


        public override string GetFileContents()
        {
            var builder = new StringBuilder();
            builder.Append($@"//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************
            { Builder.GetOutput()} ");
            return builder.ToString();
        }
    }
}