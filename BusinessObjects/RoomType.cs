using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class RoomType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomTypeName { get; set; }

        [Required]
        [StringLength(250)]
        public string TypeDescription { get; set; }

        [StringLength(250)]
        public string TypeNote { get; set; }

		public override string ToString()
		{
			return $"{{{nameof(RoomTypeId)}={RoomTypeId.ToString()}, {nameof(RoomTypeName)}={RoomTypeName}, {nameof(TypeDescription)}={TypeDescription}, {nameof(TypeNote)}={TypeNote}}}";
		}
	}
}
