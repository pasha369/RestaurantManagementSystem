define(['knockout', 'jquery', 'jquery-ui', 'toastr',
        'text!Widgets/order/orderListCtrl/orderListCtrl.html'],
    function (ko, $) {
        var toastr = require('toastr');

        $.widget("cc.orderListCtrl", {

            options: {
                view: require('text!Widgets/order/orderListCtrl/orderListCtrl.html'),
                viewModel: null,
                tableId: null,
                restaurantId: null,
                eventTrigger: null,
                orderHub: null,
                orderId: null
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);

                function OrderListVM() {
                    this.OrderDishes = ko.observableArray([]);

                    this.ActiveDishes = ko.observableArray([{Name: 'Test',Cost: 10}]);
                    this.WaitDishes = ko.observableArray([{ Name: 'WaitTest', Cost: 10 }]);
                    this.DoneDishes = ko.observableArray([{ Name: 'DoneTest', Cost: 10 }]);

                    this.showMenu = function () {
                        $("#menu").show();
                        $("#order").hide();
                    }

                    this.removeDish = function (dish) {
                        self.options.viewModel.OrderDishes.remove(dish);
                        toastr.success(dish.Name + " was removed from order.");
                    }

                    this.makeOrder = function () {
                        self._makeOrder();
                    }
                }

                this.options.viewModel = new OrderListVM();

                self._initAsyncOrder();
                self._getOpenOrder();

                this.options.eventTrigger.attach(this._addDishToOrder, this);
                ko.applyBindings(self.options.viewModel, $("#order-list-ctrl")[0]);
            },

            _addDishToOrder: function (dish) {
                this.options.viewModel.OrderDishes.push(dish);
            },

            /**
             * Initialize Signalr connection.
             * @returns {} 
             */
            _initAsyncOrder: function () {
                var self = this;
                self.options.orderHub = $.connection.orderHub;

                // Create a function that the hub can call back to display messages.
                self.options.orderHub.client.onChangeDishStatus = function (dish, status, prevStatus) {
                    var dish = {
                        Name: dish.Name,
                        Description: dish.Description,
                        Cost: 10
                    }
                    switch (prevStatus) {
                        case "Active":
                            $.each(self.options.viewModel.ActiveDishes(), function(k, v) {
                                if (v.Name == dish.Name) {
                                    self.options.viewModel.ActiveDishes.remove(v);
                                }
                            });
                            break;
                        case "Wait":
                            $.each(self.options.viewModel.WaitDishes(), function (k, v) {
                                if (v.Name == dish.Name) {
                                    self.options.viewModel.WaitDishes.remove(dish);
                                }
                            }); break;
                        case "Ready":
                            $.each(self.options.viewModel.DoneDishes(), function (k, v) {
                                if (v.Name == dish.Name) {
                                    self.options.viewModel.DoneDishes.remove(dish);
                                }
                            });
                            break;

                        default: break;
                    }
                    switch (status) {
                        case "Active":
                            self.options.viewModel.ActiveDishes.push(dish); break;
                        case "Wait":
                            self.options.viewModel.WaitDishes.push(dish); break;
                        case "Ready":
                            self.options.viewModel.DoneDishes.push(dish);
                            break;

                        default: break;
                    }
                };
                // Start the connection.
                $.connection.hub.start().done();
            },

            _getOpenOrder: function () {
                var self = this;
                $.ajax({
                    type: 'POST',
                    url: '/api/Order/GetOpenOrder/' + self.options.tableId,
                    success: function (data) {
                        self.options.orderId = data.OrderId;
                        $.each(data.Dishes, function(k, v) {
                            self.options.viewModel.OrderDishes.push(v);
                        });
                    }
                });
            },

            _makeOrder: function () {
                var self = this;
                var vm = this.options.viewModel;
                $.ajax({
                    type: 'POST',
                    url: '/api/Order/MakeOrder/',
                    data: {
                        Id: self.options.orderId,
                        Dishes: vm.OrderDishes(),
                        RestaurantId: self.options.restaurantId,
                        TableId: self.options.tableId
                    },
                    dataType: 'json',
                    success: function (orderId) {

                        self.options.orderHub.server.send({
                            OrderId: orderId,
                            Dishes: self.options.viewModel.OrderDishes(),
                            RestaurantId: self.options.restaurantId
                        }, self.options.tableId);
                        toastr.success("Order was created. Please wait your order in progress...");
                    }
                });
            },

            _orderHub: function () {

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