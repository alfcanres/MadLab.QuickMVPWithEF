using System;

namespace MadLab.QuickMVPWithEF.Entities
{
    public class TodoItem : IHasKey
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public int TodoListId { get; set; }
        public TodoList TodoList { get; set; }
    }
}