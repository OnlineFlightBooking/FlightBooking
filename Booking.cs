//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FLightBookingSystem.DatabaseLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Booking
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public System.DateTime BookingDate { get; set; }
        public string FromCIty { get; set; }
        public string ToCity { get; set; }
        public decimal Price { get; set; }
        public int FlightID { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public int TotalSeat { get; set; }
    
        public virtual Flight Flight { get; set; }
        public virtual Customer Customer { get; set; }
       
    }
}
