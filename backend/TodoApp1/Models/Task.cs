using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Task
    {
        [Key]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set;} = DateTime.Now;

        public DateTime UpdatedDate { get; set;} = DateTime.Now;

    }
}
