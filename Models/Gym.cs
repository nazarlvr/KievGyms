using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace KievGyms
{
    public partial class Gym
    {
        public Gym()
        {
            GymMemberships = new HashSet<GymMembership>();
            Trainers = new HashSet<Trainer>();
        }

        public int GymId { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Gym")]
        public string GymName { get; set; }
        public int DistrictId { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Information about Gym")]
        public string GymInfo { get; set; }
        public double GeoLong { get; set; }
        public double GeoLat { get; set; }

        public virtual District District { get; set; }
        public virtual ICollection<GymMembership> GymMemberships { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}
