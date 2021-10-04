using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace KievGyms
{
    public partial class District
    {
        public District()
        {
            Gyms = new HashSet<Gym>();
        }

        public int DistrictId { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "District Name")]
        public string DistrictName { get; set; }

        public virtual ICollection<Gym> Gyms { get; set; }
    }
}
