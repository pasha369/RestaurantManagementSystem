define(['knockout', 'jquery', 'jquery-ui', 'toastr',
        'Widgets/order/orderManageCtrl/orderManageCtrl',
        'text!Widgets/order/orderDetailCtrl/orderDetailCtrl.html'],
    function (ko, $) {
        var toastr = require('toastr');

        $.widget("cc.orderDetailCtrl", {

            options: {
                view: require('text!Widgets/order/orderDetailCtrl/orderDetailCtrl.html'),
                viewModel: null,
                restaurantId: null,
                orderHub: null,
                order: null,
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);

                function OrderDetailVM() {
                    this.StatusTypes = ko.observableArray(['Wait', 'Active', 'Ready']);
                    this.Dishes = ko.observableArray([]);
                    this.Total = ko.observable();
                    this.CloseOrder = function () {
                        self._closeOrder();
                    }

                    this.BackToOrders = function () {
                        $('.order-detail').hide();
                        $('.orders').show();
                    }

                    this.StatusChanged = function (item) {

                        self.options.orderHub.server.changeDishStatus(item, self.options.order.Id, item.Status);
                        console.log(item);
                    }
                }

                this.options.viewModel = new OrderDetailVM();

                self.options.orderHub = $.connection.orderHub;
                $.connection.hub.start().done();

                self._initOrderDetail();
                ko.applyBindings(self.options.viewModel, $("#order-detail-ctrl")[0]);
            },

            _initOrderDetail: function () {
                var self = this;
                var vm = self.options.viewModel;

                Array.prototype.sum = function (prop) {
                    var total = 0;
                    for (var i = 0; i < this.length; i++) {
                        total += this[i][prop];
                    }
                    return total;
                }

                var total = self.options.order.Dishes.sum("Cost");
                
                ko.utils.arrayPushAll(vm.Dishes, self.options.order.Dishes);
                vm.Dishes.valueHasMutated();
                vm.Total(total);
            },

            _closeOrder: function () {
                var self = this;
                $.ajax({
                    type: 'POST',
                    url: '/api/Order/CloseOrder/' + self.options.order.Id,
                    success: function (data) {
                        // TODO: implement close order.
                        // TODO: implement remove from list order after close.
                        // TODO: implement notification on close.
                    }
                })
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