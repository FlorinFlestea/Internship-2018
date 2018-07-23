using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessTripApplication.Models
{
    public class Trip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string PmName { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string ProjectName { get; set; }

        [Required]
        public string ProjectNumber { get; set; }

        [Required]
        public string TaskNumber { get; set; }

        [Required]
        public string ClientLocation { get; set; }

        [Required]
        public string DepartureLocation { get; set; }

        [Required]
        public string Transportation { get; set; }

        [Required]
        public bool NeedOfPhone { get; set; }

        [Required]
        public bool NeedOfBankCard { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        [MaxLength]
        public string Accommodation { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        [MaxLength]
        public string Comments { get; set; }

        public bool Approved { get; set; }

        public User User { get; set; }
        public Area Area { get; set; }

        public Trip()
        {
            
        }

    }
}