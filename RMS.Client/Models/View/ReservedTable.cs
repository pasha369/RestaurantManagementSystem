using System.Collections.Generic;

namespace RMS.Client.Models.View
{
    public class ReservedTable
    {
        public int Id { get; set; }
        public int Num { get; set; }

        public List<BookingModel> Reservations { get; set; }
    }
}