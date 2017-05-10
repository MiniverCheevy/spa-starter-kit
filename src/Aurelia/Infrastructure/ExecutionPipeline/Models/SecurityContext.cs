namespace Fernweh.Infrastructure.ExecutionPipeline.Models
{
  public class SecurityContext
  {
    public bool AllowAnonymouse { get; set; }
    public string[] Roles { get; set; }
  }
}
