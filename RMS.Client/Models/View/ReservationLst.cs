using System.Collections.Generic;

namespace RMS.Client.Models.View
{
    public class ReservationLst
    {
        public int Id { get; set; }

        public List<ReservedTable> Tables { get; set; }
    }
}