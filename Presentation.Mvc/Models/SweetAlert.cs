using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Mvc.Models
{
    public class SweetAlert
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string ComfirmButtonText { get; set; }

        public string CancelButtonText { get; set; }

        public SweetAlertIcon Icon { get; set; }

        public bool ShowCancelButton { get; set; }

        public bool ShowCloseButton { get; set; }


    }

    public enum SweetAlertIcon
    {
        success,
        error,
        warning,
        info,
        question
    }
}
