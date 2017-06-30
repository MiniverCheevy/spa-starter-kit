using Voodoo.Infrastructure.Notations;

namespace Core.Infrastructure.Metadata
{
    [Client]
    public class UIMetadata
    {
        public ValidationMetaData Email { get; set; }
        public ValidationMetaData Length { get; set; }
        public ValidationMetaData Date { get; set; }
        public ValidationMetaData Integer { get; set; }
        public ValidationMetaData Decimal { get; set; }
        public ValidationMetaData Required { get; set; }
        public string PropertyName { get; set; }
        public string DisplayName { get; set; }
        public string Format { get; set; } = "Text";
        public bool IsReadOnly { get; set; }
        public bool IsHidden { get; set; }
        public bool DoNotSort { get; set; }
    }

    [Client]
    public class ValidationMetaData
    {
        public bool ShouldValidate { get; set; }
        public string Message { get; set; }
        public object Min { get; set; }
        public object Max { get; set; }
    }
}