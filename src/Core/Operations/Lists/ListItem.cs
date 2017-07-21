using Voodoo.Infrastructure.Notations;

namespace Core.Operations.Lists
{
    [Client]
    public class ListItem : IListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string FilterData { get; set; }
    }

    public interface IListItem
    {
        int Id { get; set; }
        string Name { get; set; }
        string FilterData { get; set; }
    }
}