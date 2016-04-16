define(['knockout', "knockout.validation", 'jquery', 'jquery-ui', 'datepicker', 'moment', 'toastr',
        'Custom/timeOperations',
        'text!Widgets/bookingCtrl/bookingCtrl.html'],
    function (ko, validation, $, datepicker) {
        var moment = require('moment');
        var toastr = require('toastr');
        
        $.widget("cc.booking", {

            options: {
                view: require('text!Widgets/bookingCtrl/bookingCtrl.html'),
                viewModel: null,
                restaurantId: null,
            },

            _create: function () {
                var self = this;

                self.element.html(self.options.view);
                Datepicker.initDatepicker();
                var timeOperator = new TimeOperator();

                var validationConfig = ({
                    insertMessages: true,
                    decorateElement: true,
                    errorElementClass: 'has-error',
                    errorMessageClass: 'help-block'
                });
                
                function bookingVM() {
                    this.FromTimes = ko.observableArray([]);
                    this.ToTimes = ko.observableArray([]);

                    this.From = ko.observable().extend({ required: true });
                    this.To = ko.observable().extend({ required: true });

                    this.Msg = ko.observable();

                    this.Date = ko.observable().extend({ required: true });
                    this.PeopleNum = ko.observable().extend({ required: true });

                    this.btnClass = ko.observable();
                    this.isAllReserved = ko.observable();

                    this.bookTable = function () {
                        if (!self.options.viewModel.isValid()) {
                            this.errors.showAllMessages();
                        } else {
                            self._bookTable();
                        }
                    };

                    this.errors = ko.validation.group(this);
                };

                var viewModel = new bookingVM();
                self.options.viewModel = ko.validatedObservable(viewModel);
                viewModel.From.subscribe(function (newValue) {
                    var date = moment(newValue, "HH:mm");
                    var hourDependFrom = [];
                    timeOperator.fill(hourDependFrom, date.hour(), 18);
                    viewModel.ToTimes(hourDependFrom);
                });

                timeOperator.fill(self.options.viewModel().FromTimes, 9, 18);
                timeOperator.fill(self.options.viewModel().ToTimes, 9, 18);

                ko.validation.init(validationConfig, true);
                ko.applyBindingsWithValidation(self.options.viewModel, $("#bookingctrl")[0]);
                self._isFreeTableExists(viewModel);
            },

            _bookTable: function () {
                var self = this;
                var vm = self.options.viewModel();

                var from = moment(vm.Date() + ' ' + vm.From(), "DD.MM.YYYY HH:mm");
                var to = moment(vm.Date() + ' ' + vm.To(), "DD.MM.YYYY HH:mm");

                var model = {
                    From: from,
                    FromHour: from.hour(),
                    FromMinutes: from.minutes(),
                    To: to,
                    ToHour: to.hour(),
                    ToMinutes: to.minutes(),

                    Date: vm.Date,
                    Msg: vm.Msg,
                    PeopleNum: vm.PeopleNum,

                    RestaurantId: self.options.restaurantId
                };

                $.ajax({
                    type: 'POST',
                    url: '/api/Reservation/ReserveTable',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: ko.toJSON(model),
                    success: function () {
                        toastr.success('Reservation has been made');
                        $('#book-now').modal('hide');
                    },
                    error: function (err) {
                        if (err.status == 401) {
                            window.location = "/Account/Login";
                        }
                    }

                });
            },

            _getFreeTimeStart: function () {
                // TODO: use when will be created method for getting date from to to time.
                var self = this;
                $.ajax({
                    type: 'POST',
                    url: "/Restaurant/GetFreeHours/",
                    data: {
                        date: moment('2016.4.10 00:00').format("DD.MM.YYYY HH:mm"),
                        restaurantId: self.options.restaurantId
                    },
                    success: function(result) {
                        console.log(result);
                    }
                });
            },

            _isFreeTableExists: function(viewModel) {
                var self = this;
                $.ajax({
                    type: 'POST',
                    url: '/Restaurant/IsFreeTableExist/',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: ko.toJSON({ restaurantId: self.options.restaurantId }),
                    success: function(response) {
                        viewModel.isAllReserved(response.isExist);
                        if (!response.isExist) {
                            viewModel.btnClass("btn-block btn btn-u btn-u-red disabled");
                        } else {
                            viewModel.btnClass("btn-block btn btn-u btn-u-red");
                        }
                    }
                });
            },
            _setOption: function (key, value) {
                switch (key) {
                    case "clear":
                        // handle changes to clear option
                        break;
                }

                $.Widget.prototype._setOption.apply(this, arguments);
                this._super("_setOption", key, value);
            },


            destroy: function () {
                $.Widget.prototype.destroy.call(this);
            }
        });


    });