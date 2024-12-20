﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int RoomId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Room? Room { get; set; } = null!;
    }
}
