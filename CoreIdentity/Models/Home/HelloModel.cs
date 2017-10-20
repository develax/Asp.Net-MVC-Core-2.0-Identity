using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Models.Home
{
    public class HelloModel
    {
        [Display(Name = "Time of day")]
        public string TimeOfDay { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Your name")]
        public string UserName { get; set; }
    }
}
