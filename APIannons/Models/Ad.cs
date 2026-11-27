using System.ComponentModel.DataAnnotations;

namespace APIannons.Models
{
    public class Ad
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
                public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int Price { get; set; } = 0;
        
        [StringLength(50)]
        public string Location { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
