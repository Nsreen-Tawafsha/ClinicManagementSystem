using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }              // primary key


        [Required(ErrorMessage ="Doctor First Name should be entered")]
        [StringLength(30, ErrorMessage = "Maximum length is {1}")]
        [RegularExpression(pattern: "^[A-Za-z]+$", ErrorMessage = "Invalid First Name format")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Doctor Last Name should be entered")]
        [StringLength(30, ErrorMessage = "Maximum length is {1}")]
        [RegularExpression(pattern: "^[A-Za-z]+$", ErrorMessage = "Invalid Last Name format")]
        public string LastName { get; set; }


        [StringLength(100, ErrorMessage = "Maximum length is {1}")]     // 1 refers to 100 (the length of the string that we have)
        public string Address { get; set; }


        [DataType(DataType.MultilineText)]                               // it is like a text filed (unlimited)
        [Column(TypeName = "text")]
        public string Notes { get; set; }


        [DataType(DataType.Currency)]
        public decimal? MonthlySalary { get; set; }            /// string maybe empty so we dont need to put "?" which means null, but in the case of the decimal and the int and so on we should put "?"


        [DataType(DataType.PhoneNumber, ErrorMessage = "You should write just numbers")]
        public string PhoneNumber { get; set; }


        [RegularExpression(pattern: "\b[A-Z]{2}[0-9]{2}(?:[ ]?[0-9]{4}){4}(?!(?:[ ]?[0-9]){3})(?:[ ]?[0-9]{1,2})?\b ")]
        public string IBAN { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        //-------
        public string Country { get; set; }
        //-------


        [NotMapped]  //doesnt go to the database
        public string DoctorName
        {
            get { return $"{FirstName} {LastName}"; }
        }

    //---------------------------------- for the relations
        public List<Appointment> Appointments { get; set; }


        //foreign key
        [Required]
        [Display(Name = "Specialization")]
        public long SpecializationId { get; set; }
        [ForeignKey(name: "SpecializationId")]
        public Specialization Specialization { get; set; }

    }
}
