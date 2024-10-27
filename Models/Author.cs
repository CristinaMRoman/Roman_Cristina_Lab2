namespace Roman_Cristina_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List <Book>? Books { get; set; } //navigation property
    }
}
