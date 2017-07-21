using System;
using System.ComponentModel.DataAnnotations;

namespace ItlaSocial.Models
{
    public class PublicationMedia
    {
        [Key]
        public int Id { get; set; }

        public int PublicationId { get; set; }
        public virtual Publication Publication { get; set; }

        [Required]
        public string MediaUrl { get; set; }

        [Required]
        public PublicationMediaType MediaType { get; set; } = PublicationMediaType.Photo;

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public bool Reported { get; set; } = false;

        [Required]
        public bool Deleted { get; set; } = false;
    }
}