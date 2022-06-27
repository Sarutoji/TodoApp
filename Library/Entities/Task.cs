using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Task
    {
        public int taskId { get; set; }
        public int listId { get; set; }
        public string taskName { get; set; }
        public string taskDescription { get; set; }
        public bool taskStatus { get; set; }
        public DateTime dateTime { get; set; }
    }
}
