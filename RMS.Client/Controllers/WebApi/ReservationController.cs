using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Services;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.WebApi
{
    public class ReservationController : ApiController
    {
        [WebMethod]
        public List<ReservedTable> GetByRestaurant(int Id)
        {
            var rstManager = new RestaurantManager();

            List<ReservedTable> lstReservation = rstManager.GetAllTable(Id)
                .Select(t => new ReservedTable()
                                 {
                                     Id = t.Id,
                                     Num = t.Number,
                                     Reservations = GetReservationByTable(t.Id),
                                 }).ToList();

            return lstReservation;
        }
        private List<BookingModel> GetReservationByTable(int Id)
        {
            var rsvManager = new ReservationManager();
            List<BookingModel> lstReservation = rsvManager.GetAll().
                Where(r => r.Table.Id == Id)
                                .Select(r => new BookingModel()
                                 {
                                     Id = r.Id,
                                     From = r.From,
                                     To = r.To,
                                     Email = r.User.Email,
                                     Fullname = r.User.Name,
                                     Msg = r.SpecialRequest,
                                     PeopleNum = r.PeopleCount,
                                     Phone = r.User.Phone.ToString()
                                 })
                                 .ToList();

            return lstReservation;
        }
        [WebMethod]
        public void RejectReservation(int Id)
        {
            var rsvManager = new ReservationManager();
            var reservarion = rsvManager.GetById(Id);
            reservarion.Status = ReservationStatus.Canceled;
        }
        public void RemoveReservation(int Id)
        {
            var rsvManager = new ReservationManager();
            var reservarion = rsvManager.GetById(Id);

            rsvManager.Delete(reservarion);
        }
        public void ApplyReservation(int Id)
        {
            var rsvManager = new ReservationManager();
            var reservarion = rsvManager.GetById(Id);
            reservarion.Status = ReservationStatus.Confirmed;
        }
    }
}
