using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Models
{
    public class AppointmentType
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }


        [StringLength(50, ErrorMessage = "Maximum length = {1}")]
        public string Type { get; set; }


        [Display(Name = "Appointment Type price")]
        [Range(20, 60)]
        public decimal Price { get; set; }
    }
}
