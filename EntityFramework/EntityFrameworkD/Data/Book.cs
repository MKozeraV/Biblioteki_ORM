using System.ComponentModel.DataAnnotations;
namespace P7AppAPI.Data
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; } = null;
        [MaxLength(50)]
        public string Author { get; set; } = null;

    }
}
