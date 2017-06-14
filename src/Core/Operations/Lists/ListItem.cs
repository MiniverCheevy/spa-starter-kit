namespace Core.Operations.Lists
{
    public class ListItem : IListItem
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }

    public interface IListItem
    {
        int Value { get; set; }
        string Name { get; set; }
    }
}