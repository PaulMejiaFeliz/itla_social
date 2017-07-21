using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItlaSocial.Models.ApplicationViewModels
{
    public class ApplicationUserInvitationViewModel
    {
        public string Id { get; set; }

        public String Name { get; set; }

        public string LastName { get; set; }

        public FriendshipStatus Status { get; set; } = FriendshipStatus.None;

        public bool Sent { get; set; } = false;

        public ProfilePhotoViewModel ProfilePhotos { get; set; } = new ProfilePhotoViewModel() { MediaUrl = "images\\default.png" };
    }
}
