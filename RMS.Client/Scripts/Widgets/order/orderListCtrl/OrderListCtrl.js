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
                    this.OrderId = ko.observable();

                    this.ActiveDishes = ko.observableArray([]);
                    this.WaitDishes = ko.observableArray([]);
                    this.DoneDishes = ko.observableArray([]);

                    this.IsCanOrder = ko.computed(function () {
                        return this.OrderDishes().length > 0;
                    }, this);

                    this.showMenu = function () {
                        $("#menu").show();
                        $("#order").hide();
                    }

                    this.removeDish = function (dish) {
                        self.options.viewModel.OrderDishes.remove(dish);
                        toastr.success(dish.Name + " was removed from order.");
                    }

                    this.payWithPaypal = function () {
                        window.location.href = '/Paypal/PaymentWithPaypal?orderId=' + self.options.orderId;
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
                   
                    switch (prevStatus) {
                        case "Active":
                            if (self.options.viewModel.ActiveDishes().length > 0) {
                                $.each(self.options.viewModel.ActiveDishes(), function (k, v) {
                                    if (v.Id === dish.Id) {
                                        self.options.viewModel.ActiveDishes.splice(k, 1);
                                    }
                                });
                            }
                            break;
                        case "Wait":
                            if (self.options.viewModel.WaitDishes().length > 0) {
                                $.each(self.options.viewModel.WaitDishes(), function (k, v) {
                                    if (v.Id === dish.Id) {
                                        self.options.viewModel.WaitDishes.splice(k, 1);
                                    }
                                });
                            }
                            break;
                        case "Ready":
                            if (self.options.viewModel.DoneDishes().length > 0) {
                                $.each(self.options.viewModel.DoneDishes(), function (k, v) {
                                    if (v.Id === dish.Id) {
                                        self.options.viewModel.DoneDishes.splice(k, 1);
                                    }
                                });
                            }
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
                var active = [];
                var wait = [];
                var done = [];

                $.ajax({
                    type: 'POST',
                    url: '/api/Order/GetOpenOrder/' + self.options.tableId,
                    success: function (order) {
                        self.options.orderId = order.Id;
                        self.options.viewModel.OrderId(order.Id);
                        $.each(order.Dishes, function (k, v) {
                            switch (v.Status) {
                                case "Active":
                                    active.push(v);
                                    break;
                                case "Wait":
                                    wait.push(v);
                                    break;
                                case "Ready":
                                    done.push(v);
                                    break;
                                default:
                                    break;
                            }
                            //self.options.viewModel.OrderDishes.push(v);
                        });
                        self._addDishesByStatus("Active", active);
                        self._addDishesByStatus("Wait", wait);
                        self._addDishesByStatus("Done", done);
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
                    success: function (order) {
                        self.options.orderId = order.Id;
                        self.options.orderHub.server.send(order, self.options.tableId);
                        self._moveToActive();
                        toastr.success("Order was created. Please wait your order in progress...");
                    }
                });
            },

            _orderHub: function () {

            },

            _addDishesByStatus: function (status, dishes) {
                var self = this;
                switch (status) {
                    case "Active":
                        self.options.viewModel.ActiveDishes(dishes);
                        break;
                    case "Wait":
                        self.options.viewModel.WaitDishes(dishes);
                        break;
                    case "Done":
                        self.options.viewModel.DoneDishes(dishes);
                        break;
                    default:
                        return false;
                }
                return true;
            },

            _moveToActive: function() {
                var vw = this.options.viewModel;
                var dishes = vw.OrderDishes().slice();

                ko.utils.arrayPushAll(vw.ActiveDishes, dishes);
                vw.ActiveDishes.valueHasMutated();
                vw.OrderDishes.removeAll();
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