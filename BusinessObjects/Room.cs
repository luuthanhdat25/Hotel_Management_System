using BusinessObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Table("RoomInformation")]
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomNumber { get; set; }

        [Required]
        [StringLength(220)]
        public string RoomDescription { get; set; }

        [Required]
        public int RoomMaxCapacity { get; set; }

        [Required]
        public RoomStatus RoomStatus { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal RoomPricePerDate { get; set; }
     
        [Required]
        public int RoomTypeId { get; set; }

        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; }

		public override string ToString()
		{
			return $"{{{nameof(RoomId)}={RoomId.ToString()}, {nameof(RoomNumber)}={RoomNumber}, {nameof(RoomDescription)}={RoomDescription}, {nameof(RoomMaxCapacity)}={RoomMaxCapacity.ToString()}, {nameof(RoomStatus)}={RoomStatus.ToString()}, {nameof(RoomPricePerDate)}={RoomPricePerDate.ToString()}, {nameof(RoomTypeId)}={RoomTypeId.ToString()}, {nameof(RoomType)}={RoomType}}}";
		}
	}

    public enum RoomStatus
    {
        Active, Deleted
    }
}
