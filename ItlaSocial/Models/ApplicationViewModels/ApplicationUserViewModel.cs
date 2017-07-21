using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItlaSocial.Models.ApplicationViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        public String Name { get; set; }

        public string LastName { get; set; }

        public ICollection<ProfilePhotoViewModel> ProfilePhotos { get; set; }
    }
}
