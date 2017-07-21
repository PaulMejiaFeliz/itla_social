using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItlaSocial.Models.ApplicationViewModels
{
    public class MediaPublicationViewModel: PublicationViewModel
    {
        public virtual ICollection<PublicationMedia> PublicationMedia { get; set; }
    }
}
