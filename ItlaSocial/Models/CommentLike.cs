using System.ComponentModel.DataAnnotations;

namespace ItlaSocial.Models
{
    public class CommentLike
    {
        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public bool DisLike { get; set; } = false;

        public bool UnLike { get; set; } = false;
    }
}