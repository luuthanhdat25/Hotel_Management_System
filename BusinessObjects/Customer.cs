using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerFullName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(12)]
        public string Telephone { get; set; }

        [Required]
        [StringLength(50)]
        public string EmailAddress { get; set; }

        [Required]
        public DateOnly CustomerBirthday { get; set; }

        [Required]
        public CustomerStatus CustomerStatus { get; set; }
    }

    public enum CustomerStatus
    {
        Active, Disable
    }
}
