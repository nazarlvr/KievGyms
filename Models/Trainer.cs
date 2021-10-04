using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace KievGyms
{
    public partial class Trainer
    {
        public int TrainerId { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Full name")]
        public string TrainerFullName { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Date of birth")]
        public DateTime TrainerDateOfBirth { get; set; }
        [Display(Name = "Gym Name")]
        public int GymId { get; set; }
        [Required(ErrorMessage = "The field cannot be empty")]
        [Display(Name = "Salary")]
        public decimal TrainerSalary { get; set; }
        [Display(Name = "Specialization Name")]
        public int SpecializationId { get; set; }

        public virtual Gym Gym { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}
