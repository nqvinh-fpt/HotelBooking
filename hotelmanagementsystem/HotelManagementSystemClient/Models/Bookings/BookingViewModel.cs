using BusinessObjects.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystemClient.Models.Bookings
{
	public class BookingViewModel
	{
		public int? RoomId { get; set; }
		public int? CustomerId { get; set; }
		public int Status { get; set; }

		public virtual Customer? Customer { get; set; }
		public virtual Room? Room { get; set; }

		[Required]
		[FutureDate(ErrorMessage = "Check-in date must be in the future.")]
		public DateTime? CheckInDate { get; set; }

		[Required]
		[LaterThan("CheckInDate", ErrorMessage = "Check-out date must be later than check-in date.")]
		public DateTime? CheckOutDate { get; set; }
	}
}
