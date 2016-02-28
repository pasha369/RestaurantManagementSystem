define(['knockout', 'jquery', 'jquery-ui',
        'toastr',
        'Widgets/bookingCtrl/bookingCtrl',
        'text!Widgets/menu/menuUserCtrl/menuUserCtrl.html'],
    function (ko, $) {
        var toastr = require('toastr');

        $.widget("cc.menuUser", {

            options: {
                view: require('text!Widgets/menu/menuUserCtrl/menuUserCtrl.html'),
                viewModel: null,
                restaurantId: null,
                eventTrigger: null
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);

                function MenuUserVM() {
                    
                    this.Categories = ko.observableArray([]);
                    
                    this.showOrders = function() {
                        $("#menu-user-ctrl").hide();
                        $("#order-list-ctrl").show();
                    }
                    this.addToOrder = function(dish) {
                        self.options.eventTrigger.notify(dish);
                        toastr.success(dish.Name + " was added to order.");
                    }
                    this.showOrder = function() {
                        $('#order').show();
                        $('#menu').hide();
                    }
                }
                this.options.viewModel = new MenuUserVM();
                ko.applyBindings(self.options.viewModel, $("#menu-user-ctrl")[0]);
                self._loadMenu();
            },

            _loadMenu: function() {
                var self = this;
                var vm = self.options.viewModel;

                $.ajax({
                    type: 'Get',
                    url: '/api/Menu/GetCategories/',
                    contentType: "application/json; charset=utf-8",
                    data: { rstId: self.options.restaurantId },
                    dataType: "json",
                    success: function (data) {
                        $.each(data, function (key, value) {
                            vm.Categories.push(value);
                        });
                    },
                    error: function (err) {
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