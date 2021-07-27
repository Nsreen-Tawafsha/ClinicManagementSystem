using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Models
{
    public class PatientMedicalHistory
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }


        [Required (ErrorMessage = "Patient must be identified")]
        [Display(Name = "Patient")]
        public long PatientId { get; set; }
        [ForeignKey(name: "PatientId")]
        public Patient Patient { get; set; }


        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        /////
        [Required(ErrorMessage = "Date of Data entry should be entered")]
        [Display(Name = "Date of data entry")]
        [DataType(DataType.Date)]
        public DateTime DataEntry { get; set; }
    }
}
