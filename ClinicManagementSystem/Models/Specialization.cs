using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Models
{
    public class Specialization
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }

        [Required (ErrorMessage ="Specialization should be identified")]
        [StringLength(50, ErrorMessage = "Maximum length is {1}")]
        [Display(Name = "Specialization Name ")]
        public string SpecializationName { get; set; }
    }
}
