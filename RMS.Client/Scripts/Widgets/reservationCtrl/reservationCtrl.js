﻿define(['knockout',
        'jquery',
        'jquery-ui',
        'moment',
        'text!Widgets/reservationCtrl/reservationCtrl.html'],
    function (ko, $) {
        var moment = require('moment');
        $.widget("cc.reservations", {

            // These options will be used as defaults
            options: {
                view: require('text!Widgets/reservationCtrl/reservationCtrl.html'),
                reservationVM: null,
                RestaurantId: "-1",
            },

            // Set up the widget
            _create: function () {
                var self = this;

                self.element.html(this.options.view);

                function ReservationVM() {
                    this.Client = ko.observable();
                    
                    this.Tables = ko.observableArray([]);
                    this.Times = ko.observableArray([]);

                    this.Fullname = ko.observable();
                    this.Email = ko.observable();
                    this.Msg = ko.observable();
                    this.Date = ko.observable();
                    this.PeopleNum = ko.observable();



                    this.refresh = function() {
                        self._loadReservation(self.options.RestaurantId);
                    };
                    this.apply = function(item) {
                        self._applyReservation(item);
                    };
                    this.remove = function (item) {
                        self._removeReservation(item);
                    };
                    this.detail = function(item) {
                        self.options.reservationVM.Client(item);

                    };

                };

                self.options.reservationVM = new ReservationVM();
                self._fillTimes();

                ko.applyBindings(self.options.reservationVM, $("#reservations")[0]);
                self._loadReservation(this.options.RestaurantId);

            },
            _applyReservation: function (item) {
                var self = this;
                $.ajax({
                    type: "POST",
                    url: '/api/Reservation/ApplyReservation/' + item.Id(),
                    success: function() {
                        self._loadReservation(self.options.RestaurantId);
                    },
                    error: function(err) {
                        console.log(err.status);
                    },
                });
            },
            _removeReservation: function (item) {
                var self = this;
                $.ajax({
                    type: "POST",
                    url: '/api/Reservation/RemoveReservation/'+item.Id(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        self._loadReservation(self.options.RestaurantId);
                        
                    },
                    error: function (err) {
                        console.log(err.status);
                    },
                });
            },
            _loadReservation: function (restaurantId) {
                var url = '/api/Reservation/GetByRestaurant/' ;
                var self = this;

                var day = $('input[name=day]').val() ;
                var month = $('input[name=month]').val() ;
                var year = $('input[name=year]').val() ;

                var date = moment('01' + '-' + '01' + '-' + 2012, "MM-DD-YYYY").toDate();
                
                
                $.ajax({
                    type: "GET",
                    url: url,
                    data: {
                        Id: restaurantId,
                        day: day,
                        month: month,
                        year: year
                    },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        self.options.reservationVM.Tables([]);
                        $.each(data, function (key, value) {
                            var current = jQuery.extend({}, value);
                            current.Reservations = self._fillRow(current.Reservations);


                            $.each(value.Reservations, function (k, v) {
                                if (v != null) {
                                    var fromIdx = self._getDateIdx(
                                        v.From.replace("T", " ")
                                    );
                                    var toIdx = self._getDateIdx(
                                        v.To.replace("T", " ")
                                    );
                                    var reservation = {
                                        Id: ko.observable(v.Id),
                                        Fullname: ko.observable(v.Fullname),
                                        Email: ko.observable(v.Email),
                                        Msg: ko.observable(v.Msg),
                                        Date: ko.observable(v.From.replace("T", " ")),
                                        PeopleNum: ko.observable(v.PeopleNum),
                                        Status: ko.observable(v.Status),
                                        StatusCss : ko.computed(function () {
                                            if(this.Status == 0) {
                                                return "new";
                                            }
                                            if(this.Status == 1) {
                                                return "confirmed";
                                            }
                                        }, this),
                                        ColIdx: ko.observable(fromIdx),
                                        Colspan: ko.observable(toIdx - fromIdx)
                                    };

                                    self._insert(reservation.ColIdx(),
                                        reservation.Colspan(),
                                        current.Reservations,
                                        reservation);
                                }
                            });
                            
                            self.options.reservationVM.Tables.push(current);

                        });

                    },
                    error: function (err) {
                        alert(err.status + " : " + err.statusText);
                    }
                });
            },
            /* Clear space beetwen id and id + length. After 
            set in current position td with field colspan which contains 
            data about reservation.
            */
            _insert: function (id, length, data, item) {
                data.splice(id, length, item);
            },
            /* Fill every cell in table row open.
            */
            _fillRow: function (row) {
                row = [];
                var times = this.options.reservationVM.Times();
                for (var i = 0; i < times.length; i++) {
                    row.push(null);
                }
                return row;
            },
            
            /* Fill top header of table reservations (time) from begin working 
            day to end. Transition 30 minutes.
            */
            _fillTimes: function () {
                var self = this;

                var current = new Date();
                // begin working day.
                current.setHours(9);
                current.setMinutes(0);
                
                while (current.getHours() != 18) {
                    var time = moment(current).format('hh:mm');
                    
                    self.options.reservationVM.Times.push(time);
                    current = add(current, 30);
                }

                // add minutes to time.
                function add(time, min) {
                    return new Date(time.getTime() + min * 60000);
                }

            },
            /* Get index current time in array Times.
            For set cell in correct position.
            */
            _getDateIdx: function (date) {
                var times = this.options.reservationVM.Times();
                
                var curDate = moment(date, 'YYYY-MM-DD hh:mm:ss').toDate();
                var idx = 0;
                for (var i = 0; i < times.length; i++) {
                    var time = moment(times[i], 'hh:mm').toDate();
                    if (time.getHours() == curDate.getHours() ) {
                        idx = i;
                        break;
                    }
                }
                return idx;

            },

            _setOption: function (key, value) {
                $.Widget.prototype._setOption.apply(this, arguments);
                this._super("_setOption", key, value);
            },

            destroy: function () {
                $.Widget.prototype.destroy.call(this);
            }
        });


    });