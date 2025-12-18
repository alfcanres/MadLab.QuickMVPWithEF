namespace MadLab.QuickMVPWithEF.Entities
{
    public class TodoList : IHasKey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TodoItem> Items { get; set; }
    }
}