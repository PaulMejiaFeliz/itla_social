using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItlaSocial.Models
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }

        public string ApplicationUser1Id { get; set; }
        [ForeignKey("ApplicationUser1Id")]
        [InverseProperty("RecivedFriends")]
        public ApplicationUser ApplicationUser1 { get; set; }

        public string ApplicationUser2Id { get; set; }
        [ForeignKey("ApplicationUser2Id")]
        [InverseProperty("Friends")]
        public ApplicationUser ApplicationUser2 { get; set; }

        public FriendshipStatus Status { get; set; }

        public bool Sent { get; set; }
    }
}