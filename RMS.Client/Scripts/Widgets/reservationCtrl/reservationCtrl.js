define(['knockout',
        'jquery',
        'jquery-ui',
        'text!Widgets/reservationCtrl/reservationCtrl.html'],
    function (ko, $) {

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
                
                function ReservationVM(){

                    this.Reservations = ko.observableArray([]);
                    this.Fullname = ko.observable();
                    this.Email = ko.observable();
                    this.Msg = ko.observable();
                    this.Date = ko.observable();
                    this.PeopleNum = ko.observable();
                };
                
                self.options.reservationVM = new ReservationVM();
                
                ko.applyBindings(self.options.reservationVM, $("#reservations")[0]);
                self._loadReservation(this.options.RestaurantId);
            },
            
            _loadReservation: function (restaurantId) {
                var url = '/api/Reservation/GetByRestaurant/' + restaurantId;
                var self = this;
                
                $.ajax({
                    type: "GET",
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $.each(data, function (key, value) {
                            var reservation = {
                                Fullname: ko.observable(value.Fullname),
                                Email: ko.observable(value.Email),
                                Msg: ko.observable(value.Msg),
                                Date: ko.observable(value.Date),
                                PeopleNum: ko.observable(value.PeopleNum),
                            };
                            self.options.reservationVM.Reservations.push(reservation);
                        });
                        
                    },
                    error: function (err) {
                        alert(err.status + " : " + err.statusText);
                    }
                });
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