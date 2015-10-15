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
        public List<BookingModel> GetByRestaurant(int Id)
        {
            var rsvManager = new ReservationManager();

            List<BookingModel> lstReservation = rsvManager.GetAll()
                .Where(r => r.Table.Restaurant.Id == Id)
                .Select(r => new BookingModel()
                                 {
                                     Id = r.Id,
                                     Date = r.DateTime,
                                     Email = r.User.Email,
                                     Fullname = r.User.Name,
                                     Msg = r.SpecialRequest,
                                     PeopleNum = r.PeopleCount,
                                     Phone = r.User.Phone.ToString(),
                                 }).ToList();

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
