using System.ComponentModel.DataAnnotations;

namespace ItlaSocial.Models
{
    public class PublicationLike
    {
        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string PublicationId { get; set; }
        public virtual Publication Publication { get; set; }

        public bool DisLike { get; set; } = false;

        public bool UnLike { get; set; } = false;
    }
}