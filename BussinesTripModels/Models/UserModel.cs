using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BusinessTripModels.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(254)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(250)]
        public string Password { get; set; }

        public bool IsEmailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }

        public DateTime? ActivationCodeExpireDate { get; set; }

        public Role Role { get; set; }


        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public User()
        {
        }
    }
}
