using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MvcValidationExtensions.Attribute;

namespace BusinessTripModels.Models
{
    public class Trip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "PM name*")]
        [Required(ErrorMessage = "Please enter Project Manager name!")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Please enter a correct name!")]
        public string PmName { get; set; }

        [Display(Name = "Client name*")]
        [Required(ErrorMessage = "Please enter Client name!")]
        [RegularExpression(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Please enter a correct name!")]
        public string ClientName { get; set; }

        [Display(Name = "Starting Date*")]
        [Required(ErrorMessage = "Please enter starting date!")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [LessThanEqualTo("EndDate", ErrorMessage = "The date must be lower than end date!")]
        public Nullable<DateTime> StartingDate { get; set; }

        [Display(Name = "End Date*")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [GreaterThanEqualTo("StartingDate", ErrorMessage = "The date must be greater than starting date!")]
        public Nullable<DateTime> EndDate { get; set; }

        [Display(Name = "Project name")]
        [RegularExpression(@"^([a-zA-Z]*)([- ][A-Za-z]*)*$", ErrorMessage = "Alows only letters, spaces and -!")]
        public string ProjectName { get; set; }

        [Display(Name = "Project number*")]
        [Required(ErrorMessage = "Please enter project number!")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Alows only alphanumeric!")]
        public string ProjectNumber { get; set; }

        [Display(Name = "Task name")]
        [RegularExpression(@"^([a-zA-Z]*)([- ][A-Za-z]*)*$", ErrorMessage = "Alows only letters, spaces and -!")]
        public string TaskName { get; set; }

        [Display(Name = "Task number*")]
        [Required(ErrorMessage = "Please enter task number!")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Alows only alphanumeric!")]
        public string TaskNumber { get; set; }

        [Display(Name = "Client location*")]
        [Required(ErrorMessage = "Please enter client location!")]
        [RegularExpression(@"^([A-Za-z]*)([- ][A-Za-z]*)*$", ErrorMessage = "Alows only letters, spaces and -!")]
        public string ClientLocation { get; set; }

        [Display(Name = "Leaving location*")]
        [Required(ErrorMessage = "Please enter leaving location!")]
        [RegularExpression(@"^([A-Za-z]*)([- ][A-Za-z]*)*$", ErrorMessage = "Alows only letters, spaces and -!")]
        public string DepartureLocation { get; set; }

        [Display(Name = "Mean of transportation*")]
        [Required(ErrorMessage = "Please enter transport! (e.g. personal car)")]
        [RegularExpression(@"^[a-zA-Z0-9 .,;?!]*$", ErrorMessage = "Alows only numbers, letters and [.,;?!]")]
        public string Transportation { get; set; }

        [Display(Name = "Telephone")]
        public bool NeedOfPhone { get; set; }

        [Display(Name = "Bank card")]
        public bool NeedOfBankCard { get; set; }

        [Display(Name = "Accommodation*")]
        [Required(ErrorMessage = "Please enter something!")]
        [Column(TypeName = "nText")]
        [MaxLength(300)]
        [RegularExpression(@"^[a-zA-Z0-9 .,;?!]*$", ErrorMessage = "Alows only numbers, letters and [.,;?!]")]
        public string Accommodation { get; set; }

        [Display(Name = "Anything else to consider important*")]
        [Required(ErrorMessage = "Please enter something!")]
        [Column(TypeName = "nText")]
        [MaxLength(300)]
        [RegularExpression(@"^[a-zA-Z0-9 .,;?!]*$", ErrorMessage = "Alows only numbers, letters and [.,;?!]")]
        public string Comments { get; set; }

        public int Status { get; set; }

        public User User { get; set; }
        public Area Area { get; set; }
    }
}
