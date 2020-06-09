using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactWeb.Models
{
    public class ContactCreateViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required to fill in.")]
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
