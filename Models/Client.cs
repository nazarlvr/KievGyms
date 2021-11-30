using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace KievGyms
{
    public partial class Client
    {
        public Client()
        {
            GymMemberships = new HashSet<GymMembership>();
        }

        public int ClientId { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display (Name = "Full Name")]
        public string ClientFullName { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Date of birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime ClientDateOfBirth { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Phone number")]
        public string ClientMobilePhone { get; set; }

        public virtual ICollection<GymMembership> GymMemberships { get; set; }
    }
}
