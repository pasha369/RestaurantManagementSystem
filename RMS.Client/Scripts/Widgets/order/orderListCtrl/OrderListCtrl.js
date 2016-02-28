define(['knockout', 'jquery', 'jquery-ui', 'toastr',
        'text!Widgets/order/orderListCtrl/orderListCtrl.html'],
    function (ko, $) {
        var toastr = require('toastr');

        $.widget("cc.orderListCtrl", {

            options: {
                view: require('text!Widgets/order/orderListCtrl/orderListCtrl.html'),
                viewModel: null,
                restaurantId: null,
                eventTrigger: null
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);

                function OrderListVM() {
                    this.OrderDishes = ko.observableArray([]);

                    this.showMenu = function() {
                        $("#menu").show();
                        $("#order").hide();
                    }

                    this.removeDish = function(dish) {
                        self.options.viewModel.OrderDishes.remove(dish);
                        toastr.success(dish.Name + " was removed from order.");
                    }
                }
                this.options.viewModel = new OrderListVM();
                this.options.eventTrigger.attach(this._addDishToOrder, this);
                ko.applyBindings(self.options.viewModel, $("#order-list-ctrl")[0]);
            },
           
            _addDishToOrder: function(dish) {
                this.options.viewModel.OrderDishes.push(dish);
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