define(['knockout', "knockout.validation", 'jquery', 'jquery-ui', 'datepicker', 'moment', 'toastr',
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


                var validationConfig = ({
                    insertMessages: true,
                    decorateElement: true,
                    errorElementClass: 'has-error',
                    errorMessageClass: 'help-block'
                });
                
                function bookingVM() {
                    this.AvailableTime = ko.observableArray(['11:00', '11:30', '12:00', '12:30', '13:00', '13:30']);
                    
                    this.From = ko.observable().extend({ required: true });
                    this.To = ko.observable().extend({ required: true });

                    this.Msg = ko.observable();

                    this.Date = ko.observable().extend({ required: true });
                    this.PeopleNum = ko.observable().extend({ required: true });
                    
                    this.bookTable = function () {
                        if (!self.options.viewModel.isValid()) {
                            this.errors.showAllMessages();
                        } else {
                            self._bookTable();
                        }
                    };
                    
                    this.errors = ko.validation.group(this);
                };

                self.options.viewModel = ko.validatedObservable(new bookingVM());

                ko.validation.init(validationConfig, true);
                ko.applyBindingsWithValidation(self.options.viewModel, $("#bookingctrl")[0]);
            },
            _bookTable: function () {
                var self = this;
                var vm = self.options.viewModel();

                var from = moment(vm.Date() + ' ' + vm.From(), "DD.MM.YYYY HH:mm").toDate();
                var to = moment(vm.Date() + ' ' + vm.To(), "DD.MM.YYYY HH:mm").toDate();

                var reservation = {
                    From: from,
                    To: to,

                    Date: vm.Date,
                    Msg: vm.Msg,
                    PeopleNum: vm.PeopleNum,

                    RestaurantId: self.options.restaurantId
                };

                $.ajax({
                    type: 'POST',
                    url: '/Restaurant/BookTable',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: ko.toJSON(reservation),
                    success: function () {
                        toastr.success('Reservation saved');
                    },
                    error: function (err) {
                        if (e.status == 401) {
                            window.location = "/Account/Login";
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