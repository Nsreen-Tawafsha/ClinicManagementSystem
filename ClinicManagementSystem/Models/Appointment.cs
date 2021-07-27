using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }


        [Required (ErrorMessage = "Appointment's Doctor should be chosen")]
        [Display(Name = "Doctor")]
        public long DoctorId { get; set; }
        [ForeignKey(name: "DoctorId")]
        public Doctor Doctor { get; set; } //Property  //new objects to get the doctor id easely


        [Required (ErrorMessage = "Appointment's Patient should be chosen")]
        [Display(Name = "Patient")]
        public long PatientId { get; set; }
        [ForeignKey(name: "PatientId")]
        public Patient Patient { get; set; } //property


        [Required]
        [DataType(DataType.Date)]
        public DateTime Reservation { get; set; }


        [Required]
        [DataType(DataType.Time)]
        [Range(9,17, ErrorMessage = "Pay attention to the clinic opening hours")]
        public DateTime StartTime { get; set; }


        [Required]
        [DataType(DataType.Time)]
        [Range(9, 17, ErrorMessage = "Pay attention to the clinic opening hours")]
        public DateTime EndTime { get; set; }


        [Display(Name = "Appointment Type")]
        public long AppointmentTypeId { get; set; }
        [ForeignKey(name: "AppointmentTypeId")]
        public AppointmentType AppointmentType { get; set; }   // new appointment type object

    }
}
