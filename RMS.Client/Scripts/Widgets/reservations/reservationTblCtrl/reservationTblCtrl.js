define(['knockout',
        'jquery',
        'jquery-ui',
        'datepicker',
        'moment',
        'text!Widgets/reservations/reservationTblCtrl/reservationTblCtrl.html'],
    function (ko, $, datepicker) {
        var moment = require('moment');
        $.widget("cc.reservationTbl", {

            // These options will be used as defaults
            options: {
                view: require('text!Widgets/reservations/reservationTblCtrl/reservationTblCtrl.html'),
                reservationVM: null,
                restaurantId: "-1",
            },

            // Set up the widget
            _create: function () {
                var self = this;

                self.element.html(this.options.view);
                Datepicker.initDatepicker();

                function ReservationVM() {
                    this.Client = ko.observable();
                    this.StatusTypes = ko.observableArray([]);

                    this.Tables = ko.observableArray([]);
                    this.Times = ko.observableArray([]);

                    this.Date = ko.observable();

                    this.Fullname = ko.observable();
                    this.Email = ko.observable();
                    this.Phone = ko.observable();
                    this.Msg = ko.observable();
                    this.Date = ko.observable();
                    this.PeopleNum = ko.observable();
                    this.Status = ko.observable();
                    this.CurStatus = ko.observable();

                    this.apply = function (item) {
                        self._applyReservation(item);
                    };
                    this.remove = function (item) {
                        self._removeReservation(item);
                    };
                    this.detail = function (item) {
                        var vm = self.options.reservationVM;

                        vm.Fullname(item.Fullname());
                        vm.Phone(item.Phone());
                        vm.PeopleNum(item.PeopleNum());
                    };
                    this.statusChanged = function (obj, event) {
                        if (event.originalEvent) { //user changed
                            self._changeStatus(obj);
                        }
                    }
                };

                self.options.reservationVM = new ReservationVM();

                self._fillTimes();
                self.options.reservationVM.Date.subscribe(function (newValue) {
                    self._loadReservation(self.options.restaurantId);
                });
                self.options.reservationVM.Date(moment(new Date()).format("DD.MM.YYYY"));
                ko.applyBindings(self.options.reservationVM, $("#reservations")[0]);
                self._loadReservation(self.options.restaurantId);
                self._getStatusLst();

            },
            _getStatusLst: function () {
                var self = this;
                $.ajax({
                    type: "GET",
                    url: '/api/Reservation/GetRsvStatus/',
                    success: function (data) {
                        $.each(data, function (key, value) {
                            self.options.reservationVM.StatusTypes.push(value);
                        });
                    },
                    error: function (err) {
                        console.log(err.status);
                    },
                });
            },
            _changeStatus: function (curReservation) {
                var self = this;

                $.ajax({
                    type: "POST",
                    url: '/api/Reservation/ChangeStatus/',
                    data: { RstId: curReservation.Id(), ReserveStatus: self.options.reservationVM.CurStatus() },
                    success: function () {
                        self._loadReservation(self.options.restaurantId);
                    },
                    error: function (err) {
                        console.log(err.status);
                    },
                });
            },
            _applyReservation: function (item) {
                var self = this;
                $.ajax({
                    type: "POST",
                    url: '/api/Reservation/ApplyReservation/' + item.Id(),
                    success: function () {
                        self._loadReservation(self.options.restaurantId);
                    },
                    error: function (err) {
                        console.log(err.status);
                    },
                });
            },
            _removeReservation: function (item) {
                var self = this;
                $.ajax({
                    type: "POST",
                    url: '/api/Reservation/RemoveReservation/' + item.Id(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        self._loadReservation(self.options.restaurantId);

                    },
                    error: function (err) {
                        console.log(err.status);
                    },
                });
            },
            _loadReservation: function (restaurantId) {
                var url = '/api/Reservation/GetByRestaurant/';
                var self = this;

                var day = $('input[name=day]').val();
                var month = $('input[name=month]').val();
                var year = $('input[name=year]').val();

                var date = moment(self.options.reservationVM.Date(), "DD.MM.YYYY").toDate();


                $.ajax({
                    type: "GET",
                    url: url,
                    data: {
                        Id: restaurantId,
                        day: date.getDate(),
                        month: date.getMonth() + 1,
                        year: date.getFullYear()
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
                                        Phone: ko.observable(v.Phone),
                                        Msg: ko.observable(v.Msg),
                                        Date: ko.observable(v.From.replace("T", " ")),
                                        PeopleNum: ko.observable(v.PeopleNum),
                                        Status: ko.observable(v.Status),
                                        StatusCss: ko.computed(function () {

                                            switch (this.Status) {
                                                case 0:
                                                    return "new";
                                                case 1:
                                                    return "confirmed";
                                                case 2:
                                                    return "canceled";
                                                case 3:
                                                    return "noshow";
                                                default:
                                                    return "";
                                            }
                                        }, this),
                                        SelectedStatus: ko.computed({
                                            read: function () {
                                                var statusTypes = self.options.reservationVM.StatusTypes();
                                                var current = "";
                                                for (var i = 0; i < statusTypes.length; i++) {
                                                    if (this.Status == i) {
                                                        current = statusTypes[i];
                                                        break;
                                                    }
                                                }
                                                return current;
                                            },
                                            write: function (value) {
                                                self.options.reservationVM.CurStatus(value);
                                            }

                                        }, this),

                                        ColIdx: ko.observable(fromIdx + 1),
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
                        console.log(err.status + " : " + err.statusText);
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
                    var time = moment(current).format('HH:mm');

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

                var curDate = moment(date, 'YYYY-MM-DD HH:mm:ss').toDate();
                var idx = 0;
                for (var i = 0; i < times.length; i++) {
                    var time = moment(times[i], 'HH:mm').toDate();
                    if (time.getHours() == curDate.getHours()) {
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