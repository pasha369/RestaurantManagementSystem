define(['knockout', 'jquery', 'jquery-ui', 'toastr',
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
          
                }
                this.options.viewModel = new OrderManageVM();
                ko.applyBindings(self.options.viewModel, $("#order-manage-ctrl")[0]);
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