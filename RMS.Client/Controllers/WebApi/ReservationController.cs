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

namespace RMS.Client.Controllers.WebApi
{
    public class ReservationController : ApiController
    {
        public IDataManager<Reservation> rsvManager;
        private IDataManager<UserInfo> _userManager;

        public ReservationController(IDataManager<Reservation> mng, IDataManager<UserInfo> userManager)
        {
            rsvManager = mng;
            _userManager = userManager;
        }

        [HttpPost]
        public void ReserveTable(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                var rstManager = new RestaurantManager();
                var tblManager = new DinnerTableManager();

                var table = rstManager.GetAllTable(model.RestaurantId)
                    .FirstOrDefault();

                if (table != null)
                {
                    tblManager.Update(table);

                    var reservation = new Reservation();

                    reservation.User = GetUserByLogin();
                    reservation.PeopleCount = model.PeopleNum;
                    reservation.From = model.From;
                    reservation.To = model.To;
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
                .Where(r => r.User.Id == user.Id)
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

            List<ReservedTable> lstReservation = rstManager.GetAllTable(Id)
                ?.Select(t => new ReservedTable(){
                    Id = t.Id,
                    Num = t.Number,
                    Reservations = GetReservationByTable(t.Id, date)
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
        public void ChangeStatus(RsvStatus rsvStatus)
        {
            var rsv = rsvManager.Get(rsvStatus.RstId);
            ReservationStatus status;
            Enum.TryParse(rsvStatus.ReserveStatus, out status);
            rsv.Status = status;
        }

        private UserInfo GetUserByLogin()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _userManager.Get().FirstOrDefault(u => u.Login == login);
            return user;
        }
    }
}
