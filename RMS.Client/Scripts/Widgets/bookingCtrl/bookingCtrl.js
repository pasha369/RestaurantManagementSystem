define(['knockout', 'jquery', 'jquery-ui', 'datepicker',
        'text!Widgets/bookingCtrl/bookingCtrl.html'],
    function (ko, $, datepicker) {

        $.widget("cc.booking", {

            options: {
                view: require('text!Widgets/bookingCtrl/bookingCtrl.html'),
                viewModel : null,
                restaurantId : null,
            },

            _create: function () {
                var self = this;
                
                self.element.html(self.options.view);
                Datepicker.initDatepicker();
                
                function bookingVM() {
                    
                    this.Date = ko.observable();
                    this.Msg = ko.observable();
                    this.PeopleNum = ko.observable();

                    this.bookTable = function() {
                        self._bookTable();
                    };
                };

                self.options.viewModel = new bookingVM();

                ko.applyBindings(self.options.viewModel, $("#bookingctrl")[0]);
            },
            _bookTable: function () {
                var self = this;
                var vm = self.options.viewModel;
                
                var reservation = {
                    Date: vm.Date,
                    Msg: vm.Msg,
                    PeopleNum: vm.PeopleNum,
                    RestaurantId : self.options.restaurantId
                };
                
                $.ajax({
                    type:'POST',
                    url: '/Restaurant/BookTable',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data : ko.toJSON(reservation),
                    success: function () {
                        console.log('bookTable: success');
                    },
                    error: function (err) {
                        console.log(err.status + " : " + err.statusText);
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