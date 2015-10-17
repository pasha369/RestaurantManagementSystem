using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Client.Models.View
{
    public class ReservationLst
    {
        public int Id { get; set; }
        public List<ReservedTable> Tables { get; set; }
    }
}