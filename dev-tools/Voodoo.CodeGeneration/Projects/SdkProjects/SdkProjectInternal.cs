using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Voodoo.CodeGeneration.Projects.SdkProjects
{
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class Project
    {
        private ProjectItemGroup[] itemGroupField;
        private ProjectPropertyGroup[] propertyGroupField;

        private string sdkField;

        /// <remarks />
        [XmlElement("PropertyGroup")]
        public ProjectPropertyGroup[] PropertyGroup { get  => propertyGroupField; set  => propertyGroupField  = value; }

        /// <remarks />
        [XmlElement("ItemGroup")]
        public ProjectItemGroup[] ItemGroup { get  => itemGroupField; set  => itemGroupField  = value; }

        /// <remarks />
        [XmlAttribute]
        public string Sdk { get  => sdkField; set  => sdkField  = value; }
    }

    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class ProjectPropertyGroup
    {
        private object applicationIconField;

        private string assemblyNameField;

        private string conditionField;

        private string labelField;
        private string outputPathField;

        private string outputTypeExField;

        private string outputTypeField;

        private string rootNamespaceField;

        private object startupObjectField;

        private string targetFrameworkField;

        /// <remarks />
        public string OutputPath { get  => outputPathField; set  => outputPathField  = value; }

        /// <remarks />
        public string TargetFramework { get  => targetFrameworkField; set  => targetFrameworkField  = value; }

        /// <remarks />
        public string AssemblyName { get  => assemblyNameField; set  => assemblyNameField  = value; }

        /// <remarks />
        public string RootNamespace { get  => rootNamespaceField; set  => rootNamespaceField  = value; }

        /// <remarks />
        public object ApplicationIcon { get  => applicationIconField; set  => applicationIconField  = value; }

        /// <remarks />
        public string OutputTypeEx { get  => outputTypeExField; set  => outputTypeExField  = value; }

        /// <remarks />
        public object StartupObject { get  => startupObjectField; set  => startupObjectField  = value; }

        /// <remarks />
        public string OutputType { get  => outputTypeField; set  => outputTypeField  = value; }

        /// <remarks />
        [XmlAttribute]
        public string Label { get  => labelField; set  => labelField  = value; }

        /// <remarks />
        [XmlAttribute]
        public string Condition { get  => conditionField; set  => conditionField  = value; }
    }

    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class ProjectItemGroup
    {
        private ProjectItemGroupPackageReference[] packageReferenceField;
        private ProjectItemGroupReference[] referenceField;

        /// <remarks />
        [XmlElement("Reference")]
        public ProjectItemGroupReference[] Reference { get  => referenceField; set  => referenceField  = value; }

        /// <remarks />
        [XmlElement("PackageReference")]
        public ProjectItemGroupPackageReference[] PackageReference { get  => packageReferenceField; set  =>
            packageReferenceField  = value; }
    }

    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class ProjectItemGroupReference
    {
        private string includeField;

        /// <remarks />
        [XmlAttribute]
        public string Include { get  => includeField; set  => includeField  = value; }
    }

    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class ProjectItemGroupPackageReference
    {
        private string includeField;

        private string versionField;

        /// <remarks />
        [XmlAttribute]
        public string Include { get  => includeField; set  => includeField  = value; }

        /// <remarks />
        [XmlAttribute]
        public string Version { get  => versionField; set  => versionField  = value; }
    }
}