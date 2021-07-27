using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }              // primary key
        

        [Required(ErrorMessage ="Patient First name should be entered")]
        [StringLength(30, ErrorMessage = "Maximum length is {1}")]
        [RegularExpression(pattern: "^[A-Za-z]+$", ErrorMessage = "Invalid First Name format")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Patient Last name should be entered")]
        [StringLength(30, ErrorMessage = "Maximum length is {1}")]
        [RegularExpression(pattern: "^[A-Za-z]+$", ErrorMessage = "Invalid Last Name format")]
        public string LastName { get; set; }


        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage ="Patient Gender should be selected")]
        public string Gender { get; set; }


        [DataType(DataType.PhoneNumber, ErrorMessage ="You should write just numbers")]
        public string PhoneNumber { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Registration date should be entered")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }


        //it is like the Identification Number
        [Required(ErrorMessage ="SSN should be entered")]
        [RegularExpression(pattern: "^\\d{9}$")]
        public string SSN { get; set; }


        [NotMapped]
        public string PatientName
        {
            get { return $"{FirstName} {LastName}"; }

        }


        //-------
        public string Country { get; set; }
        //-------


        //---------------------------------- for the relations
        public List<Appointment> Appointments { get; set; }
        public List<PatientMedicalHistory> MedicalHistories { get; set; }

        //---------------------------------- addition
        [NotMapped]
        [Display(Name = "Patient age")]
        public int Age
        {
            get
            {
                var now = DateTime.Today;
                var age = now.Year - Birthday.Year;
                if (Birthday > now.AddYears(-age)) age--;
                return age;
            }

        }
    }
}
