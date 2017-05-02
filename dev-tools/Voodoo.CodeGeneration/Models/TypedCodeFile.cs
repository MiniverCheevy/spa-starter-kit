﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models.TestingFramework;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo;
using Voodoo.CodeGeneration.Models.Reflection;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace Voodoo.CodeGeneration.Models
{
    public abstract class CodeFile
    {
        protected CodeFile(ProjectFacade project)
        {
            if (project == null) return;
            Project = project;

            if (Vs.Helper.Solution.ContextType == null) return;
            HasContext = true;
            ContextName = Vs.Helper.Solution.ContextType.Name;
            ContextNamespace = Vs.Helper.Solution.ContextType.Namespace;
        }

        public bool OverwriteExistingFile { get; set; }
        public string Name { get; set; }
        public ProjectFacade Project { get; set; }
        public List<string> PageSpecificUsingStatements { get; set; } = new List<string>();
        public bool HasContext { get; set; }
        public string ContextName { get; set; }
        public string ContextNamespace { get; set; }

        public virtual string FullPath => $@"{Project.Folder}\{PathToProject}";

        public List<string> UsingStatements
        {
            get
            {
                var statements =
                    Project.UsingStatements
                        .Union(PageSpecificUsingStatements)
                        .Where(c => c != Namespace && !string.IsNullOrWhiteSpace(c))
                        .Distinct()
                        .ToList();

                return statements;
            }
        }

        public string PathToProject => string.IsNullOrWhiteSpace(GetFolder())
            ? FileName
            : IoNic.PathCombineLocal(GetFolder(), FileName);

        public virtual string VisualStudioItemTypeNode => VisualStudioItemType.Compile.ToString();

        public virtual string Namespace => $"{Project.RootNamespace}.{GetFolder()}".Replace(@"\", ".").TrimEnd('.');

        public virtual string FileName => OverwriteExistingFile ? $"{Name}.generated.cs" : $"{Name}.cs";

        public virtual IEnumerable<KeyValuePair<string, string>> CustomVisualStudioMetaData()
        {
            return null;
        }

        public abstract string GetFileContents();
        public abstract string GetFolder();
    }

    public abstract class TypedTestFile : TypedCodeFile
    {
        protected TypedTestFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Tests = Vs.Helper.TestingFramework;
            Tests.RequiredNamespaces.AsEnumerable()
                .ForEach(c => PageSpecificUsingStatements.Add(c));
            PageSpecificUsingStatements.Add("FluentAssertions");
        }

        public ITestingFramework Tests { get; set; }
    }

    public abstract class TypedCodeFile : CodeFile
    {
        protected TypedCodeFile(ProjectFacade project, TypeFacade type) : base(project)
        {
            Type = type;
            var pluralizer = PluralizationService.CreateService(CultureInfo.CurrentCulture);
            if (type == null) return;
            Name = type.Name;
            PluralName = pluralizer.Pluralize(Name);
        }

        public TypeFacade Type { get; set; }
        public string PluralName { get; set; }

        public string Folder => PluralName;

        public string ExtrasFolder => $@"{Folder}\Extras";
    }

    public abstract class TypedUiScratchFile : ScratchFile
    {
        protected TypedUiScratchFile(TypeFacade project)
        {
        }
    }

    public abstract class ScratchFile : CodeFile
    {
        private readonly string fileName;

        protected ScratchFile() : base(null)
        {
            fileName = $"{Guid.NewGuid().ToString().Replace("-", "")}.cs";
        }

        public override string FullPath => IoNic.PathCombineLocal(Path.GetTempPath(), fileName);

        public override string FileName => fileName;

        public override string GetFolder()
        {
            return null;
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}