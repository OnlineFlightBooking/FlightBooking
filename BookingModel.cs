using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FLightBookingSystem.Models
{
    public class BookingModel
    {
        public int BookingID;
        public DateTime SelectedDate;
    }
    public class CancelationModel
    {
        public int BookingID;
        public int Payment;
    }
}