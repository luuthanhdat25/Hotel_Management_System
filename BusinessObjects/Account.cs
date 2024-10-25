using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public AccountType accountType { get; set; }

        [Required]
        public AccountStatus AccountStatus { get; set; }
    }

    public enum AccountType
    {
        Admin,
        Customer
    }

    public enum AccountStatus
    {
        Active, 
        Disable
    }
}
