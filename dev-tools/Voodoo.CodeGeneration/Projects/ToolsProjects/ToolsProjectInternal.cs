﻿namespace Voodoo.CodeGeneration.Projects.ToolsProjects
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003", IsNullable = false)]
    public partial class Project
    {

        private object[] itemsField;

        private decimal toolsVersionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Import", typeof(ProjectImport))]
        [System.Xml.Serialization.XmlElementAttribute("ItemGroup", typeof(ProjectItemGroup))]
        [System.Xml.Serialization.XmlElementAttribute("PropertyGroup", typeof(ProjectPropertyGroup))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ToolsVersion
        {
            get
            {
                return this.toolsVersionField;
            }
            set
            {
                this.toolsVersionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectImport
    {

        private string projectField;

        private string conditionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Project
        {
            get
            {
                return this.projectField;
            }
            set
            {
                this.projectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroup
    {

        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Compile", typeof(ProjectItemGroupCompile))]
        [System.Xml.Serialization.XmlElementAttribute("Content", typeof(ProjectItemGroupContent))]
        [System.Xml.Serialization.XmlElementAttribute("EmbeddedResource", typeof(ProjectItemGroupEmbeddedResource))]
        [System.Xml.Serialization.XmlElementAttribute("None", typeof(ProjectItemGroupNone))]
        [System.Xml.Serialization.XmlElementAttribute("Reference", typeof(ProjectItemGroupReference))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroupCompile
    {

        private string dependentUponField;

        private string includeField;

        /// <remarks/>
        public string DependentUpon
        {
            get
            {
                return this.dependentUponField;
            }
            set
            {
                this.dependentUponField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroupContent
    {

        private string includeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroupEmbeddedResource
    {

        private string dependentUponField;

        private string includeField;

        /// <remarks/>
        public string DependentUpon
        {
            get
            {
                return this.dependentUponField;
            }
            set
            {
                this.dependentUponField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroupNone
    {

        private string includeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroupReference
    {

        private string hintPathField;

        private string includeField;

        /// <remarks/>
        public string HintPath
        {
            get
            {
                return this.hintPathField;
            }
            set
            {
                this.hintPathField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectPropertyGroup
    {

        private bool debugSymbolsField;

        private bool debugSymbolsFieldSpecified;

        private string debugTypeField;

        private bool optimizeField;

        private bool optimizeFieldSpecified;

        private string outputPathField;

        private string defineConstantsField;

        private string errorReportField;

        private byte warningLevelField;

        private bool warningLevelFieldSpecified;

        private ProjectPropertyGroupConfiguration configurationField;

        private ProjectPropertyGroupPlatform platformField;

        private string projectGuidField;

        private string outputTypeField;

        private string appDesignerFolderField;

        private string rootNamespaceField;

        private string assemblyNameField;

        private string targetFrameworkVersionField;

        private ushort fileAlignmentField;

        private bool fileAlignmentFieldSpecified;

        private object targetFrameworkProfileField;

        private string conditionField;

        /// <remarks/>
        public bool DebugSymbols
        {
            get
            {
                return this.debugSymbolsField;
            }
            set
            {
                this.debugSymbolsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DebugSymbolsSpecified
        {
            get
            {
                return this.debugSymbolsFieldSpecified;
            }
            set
            {
                this.debugSymbolsFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string DebugType
        {
            get
            {
                return this.debugTypeField;
            }
            set
            {
                this.debugTypeField = value;
            }
        }

        /// <remarks/>
        public bool Optimize
        {
            get
            {
                return this.optimizeField;
            }
            set
            {
                this.optimizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OptimizeSpecified
        {
            get
            {
                return this.optimizeFieldSpecified;
            }
            set
            {
                this.optimizeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string OutputPath
        {
            get
            {
                return this.outputPathField;
            }
            set
            {
                this.outputPathField = value;
            }
        }

        /// <remarks/>
        public string DefineConstants
        {
            get
            {
                return this.defineConstantsField;
            }
            set
            {
                this.defineConstantsField = value;
            }
        }

        /// <remarks/>
        public string ErrorReport
        {
            get
            {
                return this.errorReportField;
            }
            set
            {
                this.errorReportField = value;
            }
        }

        /// <remarks/>
        public byte WarningLevel
        {
            get
            {
                return this.warningLevelField;
            }
            set
            {
                this.warningLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool WarningLevelSpecified
        {
            get
            {
                return this.warningLevelFieldSpecified;
            }
            set
            {
                this.warningLevelFieldSpecified = value;
            }
        }

        /// <remarks/>
        public ProjectPropertyGroupConfiguration Configuration
        {
            get
            {
                return this.configurationField;
            }
            set
            {
                this.configurationField = value;
            }
        }

        /// <remarks/>
        public ProjectPropertyGroupPlatform Platform
        {
            get
            {
                return this.platformField;
            }
            set
            {
                this.platformField = value;
            }
        }

        /// <remarks/>
        public string ProjectGuid
        {
            get
            {
                return this.projectGuidField;
            }
            set
            {
                this.projectGuidField = value;
            }
        }

        /// <remarks/>
        public string OutputType
        {
            get
            {
                return this.outputTypeField;
            }
            set
            {
                this.outputTypeField = value;
            }
        }

        /// <remarks/>
        public string AppDesignerFolder
        {
            get
            {
                return this.appDesignerFolderField;
            }
            set
            {
                this.appDesignerFolderField = value;
            }
        }

        /// <remarks/>
        public string RootNamespace
        {
            get
            {
                return this.rootNamespaceField;
            }
            set
            {
                this.rootNamespaceField = value;
            }
        }

        /// <remarks/>
        public string AssemblyName
        {
            get
            {
                return this.assemblyNameField;
            }
            set
            {
                this.assemblyNameField = value;
            }
        }

        /// <remarks/>
        public string TargetFrameworkVersion
        {
            get
            {
                return this.targetFrameworkVersionField;
            }
            set
            {
                this.targetFrameworkVersionField = value;
            }
        }

        /// <remarks/>
        public ushort FileAlignment
        {
            get
            {
                return this.fileAlignmentField;
            }
            set
            {
                this.fileAlignmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FileAlignmentSpecified
        {
            get
            {
                return this.fileAlignmentFieldSpecified;
            }
            set
            {
                this.fileAlignmentFieldSpecified = value;
            }
        }

        /// <remarks/>
        public object TargetFrameworkProfile
        {
            get
            {
                return this.targetFrameworkProfileField;
            }
            set
            {
                this.targetFrameworkProfileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectPropertyGroupConfiguration
    {

        private string conditionField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectPropertyGroupPlatform
    {

        private string conditionField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}