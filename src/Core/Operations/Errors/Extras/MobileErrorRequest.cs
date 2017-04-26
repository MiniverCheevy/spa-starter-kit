namespace Fernweh.Core.Operations.Errors.Extras
{
    public class MobileErrorRequest
    {
        public string ErrorMsg { get; set; }
        public string Url { get; set; }
        public string LineNumber { get; set; }
        public string Column { get; set; }
        public string ErrorObject { get; set; }
    }
}