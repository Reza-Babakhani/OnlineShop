using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels.Profile
{
   public class ProfileIndexViewModel
    {
        public ProfileIndexViewModel()
        {
            ProfileInformations = new ProfileDetailsViewModel();

            SideBarViewModel = new ProfileSideBarViewModel();
        }

        public ProfileDetailsViewModel ProfileInformations { get; set; }

        public ProfileSideBarViewModel SideBarViewModel { get; set; }

    }
}
