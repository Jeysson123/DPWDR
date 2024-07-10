using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Technical.Interview.Model.Dtos;

namespace Technical.Interview.Model.Entities
{
    /*class that represent a product*/
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [NotMapped]
        public Rating Rating { get; set; }
    }
}
