using System.ComponentModel.DataAnnotations;

namespace todoApp.Models
{
    public class TodoText
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }

        public int isCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
