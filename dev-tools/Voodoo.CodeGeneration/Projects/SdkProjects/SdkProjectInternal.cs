using System;

namespace Voodoo.CodeGeneration.Projects.SdkProjects
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Project
    {
        private ProjectPropertyGroup[] propertyGroupField;

        private ProjectItemGroup[] itemGroupField;

        private string sdkField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PropertyGroup")]
        public ProjectPropertyGroup[] PropertyGroup
        {
            get { return this.propertyGroupField; }
            set { this.propertyGroupField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemGroup")]
        public ProjectItemGroup[] ItemGroup
        {
            get { return this.itemGroupField; }
            set { this.itemGroupField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Sdk
        {
            get { return this.sdkField; }
            set { this.sdkField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ProjectPropertyGroup
    {
        private string outputPathField;

        private string targetFrameworkField;

        private string assemblyNameField;

        private string rootNamespaceField;

        private object applicationIconField;

        private string outputTypeExField;

        private object startupObjectField;

        private string outputTypeField;

        private string labelField;

        private string conditionField;

        /// <remarks/>
        public string OutputPath
        {
            get { return this.outputPathField; }
            set { this.outputPathField = value; }
        }

        /// <remarks/>
        public string TargetFramework
        {
            get { return this.targetFrameworkField; }
            set { this.targetFrameworkField = value; }
        }

        /// <remarks/>
        public string AssemblyName
        {
            get { return this.assemblyNameField; }
            set { this.assemblyNameField = value; }
        }

        /// <remarks/>
        public string RootNamespace
        {
            get { return this.rootNamespaceField; }
            set { this.rootNamespaceField = value; }
        }

        /// <remarks/>
        public object ApplicationIcon
        {
            get { return this.applicationIconField; }
            set { this.applicationIconField = value; }
        }

        /// <remarks/>
        public string OutputTypeEx
        {
            get { return this.outputTypeExField; }
            set { this.outputTypeExField = value; }
        }

        /// <remarks/>
        public object StartupObject
        {
            get { return this.startupObjectField; }
            set { this.startupObjectField = value; }
        }

        /// <remarks/>
        public string OutputType
        {
            get { return this.outputTypeField; }
            set { this.outputTypeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Label
        {
            get { return this.labelField; }
            set { this.labelField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition
        {
            get { return this.conditionField; }
            set { this.conditionField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ProjectItemGroup
    {
        private ProjectItemGroupReference[] referenceField;

        private ProjectItemGroupPackageReference[] packageReferenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Reference")]
        public ProjectItemGroupReference[] Reference
        {
            get { return this.referenceField; }
            set { this.referenceField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PackageReference")]
        public ProjectItemGroupPackageReference[] PackageReference
        {
            get { return this.packageReferenceField; }
            set { this.packageReferenceField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ProjectItemGroupReference
    {
        private string includeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get { return this.includeField; }
            set { this.includeField = value; }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ProjectItemGroupPackageReference
    {
        private string includeField;

        private string versionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get { return this.includeField; }
            set { this.includeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Version
        {
            get { return this.versionField; }
            set { this.versionField = value; }
        }
    }
}