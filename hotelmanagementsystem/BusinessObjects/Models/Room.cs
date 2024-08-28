using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Room
    {

        public int RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomType { get; set; }
        public string? Status { get; set; }
        public decimal? Price { get; set; }
        public string? Amenities { get; set; }
        public string? RoomImage { get; set; }
        public int? Floor { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }
        public virtual ICollection<Feedback>? Feedbacks { get; set; }
    }
}
