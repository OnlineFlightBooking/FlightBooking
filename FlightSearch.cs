using FLightBookingSystem.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FLightBookingSystem.Models
{
    public class FlightSearch : IValidatableObject
    {
        public int ID { get; set; }
        public string FlightName { get; set; }

        [Required(ErrorMessage = "The start date is required")]
        [Display(Name = "Start Date:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "The end date is required")]
        [Display(Name = "End Date:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime EndTime { get; set; }

        public int Seat { get; set; }

        [Required(ErrorMessage = "From City Required")]
        public string FromCity { get; set; }

        [Required(ErrorMessage = "To City Required")]
        public string ToCity { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (StartTime < DateTime.Now)
            {
                yield return new ValidationResult("Selected Older Date please select Proper Date");
            }
            if (EndTime < DateTime.Now)
            {
                yield return new ValidationResult("Selected Older Date please select Proper Date");
            }
            if (StartTime < EndTime)
            {
                yield return new ValidationResult("EndDate must be greater than StartDate");
            }
        }
    }
}