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
        [MaxLength(40, ErrorMessage = "Only 40 chars allowed")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Second name required to fill in.")]
        [MaxLength(40, ErrorMessage = "Only 40 chars allowed")]
        public string SecondName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email required to fill in.")]
        public string Email { get; set; }
        [Range(typeof(DateTime), "01/01/1910", "01/01/2020")]
        public DateTime Birthdate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number required to fill in.")]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address required to fill in.")]
        public string Address { get; set; }

        [MaxLength(300, ErrorMessage = "Only 300 chars allowed")]
        public string Description { get; set; }
    }
}
