namespace Core.Models.Scratch
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}