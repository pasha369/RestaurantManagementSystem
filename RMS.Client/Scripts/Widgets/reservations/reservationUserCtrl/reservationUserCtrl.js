define(['knockout', 'jquery', 'jquery-ui',
        "Custom/EventTrigger",
        'toastr', 'Widgets/bookingCtrl/bookingCtrl',
        'Widgets/menu/menuUserCtrl/menuUserCtrl',
        'Widgets/order/orderListCtrl/orderListCtrl',
        'text!Widgets/reservations/reservationUserCtrl/reservationUserCtrl.html'],
    function (ko, $) {
        var toastr = require('toastr');

        $.widget("cc.userReservations", {
            options: {
                view: require('text!Widgets/reservations/reservationUserCtrl/reservationUserCtrl.html'),
                viewModel: null,
                restaurantId: null,
            },

            _create: function () {
                var self = this;

                self.element.html(self.options.view);

                function UserRsvVM() {
                    this.Restaurants = ko.observableArray([]);

                    this.enter = function (item) {
                        self._enter(item);
                    };
                };

                self.options.viewModel = new UserRsvVM();

                ko.applyBindings(self.options.viewModel, $("#reservation-user-ctrl")[0]);
                self._loadRestaurants();
            },

            _loadRestaurants: function () {
                var self = this;
                var vm = self.options.viewModel;

                $.ajax({
                    type: 'GET',
                    url: '/api/Reservation/GetUserReservation',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $.each(data, function (key, value) {
                            var reservation = value.Table.Restaurant;
                            reservation.Description = reservation.Description.substring(0, 200) + '...';
                            if (reservation.PhotoUrl) {
                                reservation.PhotoUrl = reservation.PhotoUrl.replace("~", "");
                            }
                            reservation.TableId = value.Table.TableId;
                            reservation.Status = value.Status;
                            vm.Restaurants.push(reservation);
                        });
                    },
                    error: function (err) {
                        console.log(err.status + " : " + err.statusText);

                    }
                });
            },

            /* Load restaurant menu when user click on button enter.
            */
            _enter: function (item) {
                var self = this;
                $("#reservation-user-ctrl").hide();
                var eventTrigger = new EventTrigger();
                $(".menu-order").append($('<div id="menu"></div>'));
                $(".menu-order").append($('<div id="order"></div>'));
                $('#menu').menuUser({ restaurantId: item.Id, eventTrigger: eventTrigger });
                $('#order').orderListCtrl({ tableId: item.TableId, restaurantId: item.Id, eventTrigger: eventTrigger });
                $('#order').hide();
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