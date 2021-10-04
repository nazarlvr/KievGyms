using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace KievGyms
{
    public partial class Specialization
    {
        public Specialization()
        {
            Trainers = new HashSet<Trainer>();
        }

        public int SpecializationId { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Specialization")]
        public string SpecializationName { get; set; }

        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}
