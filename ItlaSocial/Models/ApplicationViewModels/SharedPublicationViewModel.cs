using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItlaSocial.Models.ApplicationViewModels
{
    public class SharedPublicationViewModel: PublicationViewModel
    {
        public virtual SharedPublicationInfoViewModel SharedPublication { get; set; }
    }
}
