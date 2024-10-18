using BusinessObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class BookingDetail
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int BookingDetailId { get; set; }

		[Required]
		public int BookingReservationId { get; set; }

		[ForeignKey("BookingReservationId")]
		public BookingReservation BookingReservation { get; set; }

		[Required]
		public int RoomId { get; set; }

		[ForeignKey("RoomId")]
		public Room Room { get; set; }

		[Required]
		public DateOnly StartDate { get; set; }

		[Required]
		public DateOnly EndDate { get; set;}

		[Required]
		[Column(TypeName = "money")]
		public decimal ActualPrice { get; set; }
	}
}
