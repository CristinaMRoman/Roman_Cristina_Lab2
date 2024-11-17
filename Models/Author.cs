using System.ComponentModel.DataAnnotations;

namespace Roman_Cristina_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name ="AuthorName")]
        public string AuthorName => $"{FirstName} {LastName}";
        public ICollection<Book>? Books { get; set; } //navigation property
    }
}
