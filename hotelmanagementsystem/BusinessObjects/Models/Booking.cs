using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int? RoomId { get; set; }
        public int? CustomerId { get; set; }
        public int Status { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Room? Room { get; set; }


        [Required]
        //[FutureDate(ErrorMessage = "Check-in date must be in the future.")]
        public DateTime? CheckInDate { get; set; }

        [Required]
        [LaterThan("CheckInDate", ErrorMessage = "Check-out date must be later than check-in date.")]
        public DateTime? CheckOutDate { get; set; }


    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var checkInDate = (DateTime)value;
            if (checkInDate <= DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }

    public class LaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public LaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var checkOutDate = (DateTime)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            var checkInDate = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (checkOutDate <= checkInDate)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}


