using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace KievGyms
{
    public partial class GymMembership
    {
        public int GymMembershipId { get; set; }
        public int GymId { get; set; }
        public int ClientId { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Price")]
        public decimal GymMembershipPrice { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Information")]
        public string GymMembershipInfo { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Expiration date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime GymMembershipExpiryDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual Gym Gym { get; set; }
    }
}
