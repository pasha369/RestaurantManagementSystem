define(['knockout', 'jquery', 'jquery-ui', 'toastr',
        'Widgets/order/orderDetailCtrl/orderDetailCtrl',
        'text!Widgets/order/orderManageCtrl/orderManageCtrl.html'],
    function (ko, $) {
        var toastr = require('toastr');

        $.widget("cc.orderManageCtrl", {

            options: {
                view: require('text!Widgets/order/orderManageCtrl/orderManageCtrl.html'),
                viewModel: null,
                restaurantId: null,
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);

                function OrderManageVM() {
                    this.Orders = ko.observableArray();

                    this.GetDetail = function (order) {
                        $(".orders").hide();
                        if ($(".order-detail").is(':cc-orderDetailCtrl')) {
                            $(".order-detail").orderDetailCtrl("destroy");
                        }
                        $(".order-detail").orderDetailCtrl({ order: order.Order });
                        $(".order-detail").show();
                    }
                }

                var order = $.connection.orderHub;
                // Create a function that the hub can call back to display messages.
                order.client.addOrderToPage = function (order, tableNumber) {
                    // Add the message to the page. 
                    self.options.viewModel.Orders.push({
                        Order: order,
                        TableNumber: tableNumber
                    });
                };
                $.connection.hub.start().done();
                this.options.viewModel = new OrderManageVM();
                ko.applyBindings(self.options.viewModel, $("#order-manage-ctrl")[0]);
                self._loadOrders();
            },

            _loadOrders: function () {
                var self = this;
                $.ajax({
                    type: 'POST',
                    url: '/api/Order/GetActiveOrders/',
                    success: function(data) {
                        $.each(data, function(k, v) {
                            self.options.viewModel.Orders.push({
                                Order: { OrderId: v.OrderId, Dishes: v.Dishes },
                                TableNumber: v.TableNumber,
                                ClientName: v.ClientName
                            });
                        });
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