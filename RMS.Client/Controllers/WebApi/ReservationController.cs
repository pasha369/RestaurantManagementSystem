﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Services;
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

        public ReservationController(IDataManager<Reservation> mng)
        {
            rsvManager = mng;
        }

        [HttpGet]
        public List<ReservedTable> GetByRestaurant(int Id, int day, int month, int year)
        {
            var date = new DateTime(year, month, day);
            var rstManager = new RestaurantManager();

            List<ReservedTable> lstReservation = rstManager.GetAllTable(Id)
                .Select(t => new ReservedTable()
                                 {
                                     Id = t.Id,
                                     Num = t.Number,
                                     Reservations = GetReservationByTable(t.Id, date),
                                 }).ToList();

            return lstReservation;
        }

        private List<BookingModel> GetReservationByTable(int Id, DateTime date)
        {
            List<BookingModel> lstReservation = rsvManager.GetAll().
                Where(r => r.Table.Id == Id && r.From.Day == date.Day && 
                    r.From.Month == date.Month && r.From.Year == date.Year )
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

            var reservarion = rsvManager.GetById(Id);
            reservarion.Status = ReservationStatus.Canceled;
        }
        [HttpPost]
        public void RemoveReservation(int Id)
        {
            var reservarion = rsvManager.GetById(Id);

            rsvManager.Delete(reservarion);
        }
        public void ApplyReservation(int Id)
        {
            
            var reservarion = rsvManager.GetById(Id);

            reservarion.Status = ReservationStatus.Confirmed;

            rsvManager.Delete(reservarion);
        }
    }
}
