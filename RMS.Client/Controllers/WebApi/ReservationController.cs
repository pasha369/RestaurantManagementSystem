using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Services;
using AutoMapper;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Client.Models.View;
using System;
using System.Net;
using System.Net.Http;

namespace RMS.Client.Controllers.WebApi
{
    /// <summary>
    /// Represents reservation controller.
    /// </summary>
    public class ReservationController : ApiController
    {
        public IDataManager<Reservation> rsvManager;
        private IDataManager<UserInfo> _userManager;

        /// <summary>
        /// Initialize BookingModel instance.
        /// </summary>
        /// <param name="mng">Reservation manager</param>
        /// <param name="userManager">User manager</param>
        public ReservationController(IDataManager<Reservation> mng, IDataManager<UserInfo> userManager)
        {
            rsvManager = mng;
            _userManager = userManager;
        }

        /// <summary>
        /// Reserve table.
        /// </summary>
        /// <param name="model">Booking model</param>
        [HttpPost]
        public void ReserveTable(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                var from = new DateTime(model.From.Year, model.From.Month, model.From.Day, model.FromHour, model.FromMinutes, 0);
                var to = new DateTime(model.To.Year, model.To.Month, model.To.Day, model.ToHour, model.ToMinutes, 0);

                var rstManager = new RestaurantManager();
                var tblManager = new DinnerTableManager();

                var restaurant = rstManager.Get(model.RestaurantId);
                var table = restaurant
                    .DinnerTables
                    .FirstOrDefault(x => x.Reservations.Exists(r => (r.From <= from && r.To >= from) ||
                               (r.From <= to && r.To >= to)) == false);

                if (table != null)
                {
                    tblManager.Update(table);

                    var reservation = new Reservation();

                    reservation.User = GetUserByLogin();
                    reservation.PeopleCount = model.PeopleNum;
                    reservation.From = from;
                    reservation.To = to;
                    reservation.SpecialRequest = model.Msg;
                    reservation.Table = table;
                    rsvManager.Add(reservation);
                }
            }
        }

        /// <summary>
        /// Get user reserved restaurants where he 
        /// can enter after restaurateur confirm that user enter.
        /// </summary>
        /// <returns>User reserved restaurants</returns>
        [HttpGet]
        public List<TableViewModel> GetUserReservation()
        {
            var user = GetUserByLogin();
            var tableList = rsvManager.Get()
                .Where(r => r.User.Id == user.Id && r.Status == ReservationStatus.Confirmed)
                .Select(r => r.Table);
            var modelLst = Mapper.Map<List<TableViewModel>>(tableList);

            return modelLst;
        }

        /// <summary>
        /// Get restaurant reservation by date.
        /// </summary>
        /// <param name="Id">restaurant id</param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        public List<ReservedTable> GetByRestaurant(int Id, int day, int month, int year)
        {
            var date = new DateTime(year, month, day);
            var rstManager = new RestaurantManager();

            var restaurant = rstManager.Get(Id);
            List<ReservedTable> lstReservation = restaurant.DinnerTables
                ?.Select(t => new ReservedTable()
                {
                    Id = t.Id,
                    Num = t.Number,
                    Reservations = t.Reservations.Count() > 0 ? GetReservationByTable(t.Id, date) : new List<BookingModel>()
                })
                .ToList();

            return lstReservation;
        }

        private List<BookingModel> GetReservationByTable(int Id, DateTime date)
        {

            List<BookingModel> lstReservation = rsvManager.Get().
                Where(r => r.Table.Id == Id && r.From.Day == date.Day &&
                           r.From.Month == date.Month && r.From.Year == date.Year)
                .Select(r => new BookingModel()
                {
                    Id = r.Id,
                    From = r.From,
                    To = r.To,
                    Email = r.User.Email,
                    Fullname = r.User.Name,
                    Msg = r.SpecialRequest,
                    PeopleNum = r.PeopleCount,
                    Status = r.Status,
                    Phone = r.User.Phone.ToString()
                })
                .ToList();

            return lstReservation;

            return new List<BookingModel>();
        }

        [WebMethod]
        public void RejectReservation(int Id)
        {
            var reservarion = rsvManager.Get(Id);
            reservarion.Status = ReservationStatus.Canceled;
        }

        [HttpPost]
        public void RemoveReservation(int Id)
        {
            var reservarion = rsvManager.Get(Id);

            rsvManager.Delete(reservarion);
        }

        public void ApplyReservation(int Id)
        {
            var reservarion = rsvManager.Get(Id);
            reservarion.Status = ReservationStatus.Confirmed;
            rsvManager.Update(reservarion);
        }

        [HttpGet]
        public List<string> GetRsvStatus()
        {
            return Enum.GetNames(typeof(ReservationStatus)).ToList();
        }

        [HttpPost]
        public HttpResponseMessage ChangeStatus(RsvStatus rsvStatus)
        {
            var rsv = rsvManager.Get(rsvStatus.RstId);
            ReservationStatus status;
            Enum.TryParse(rsvStatus.ReserveStatus, out status);
            rsv.Status = status;
            rsvManager.Update(rsv);
            return this.Request.CreateResponse(HttpStatusCode.OK, new {});
        }

        private UserInfo GetUserByLogin()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _userManager.Get().FirstOrDefault(u => u.Login == login);
            return user;
        }
    }
}
