define(['knockout', 'jquery', 'jquery-ui',
        'Widgets/bookingCtrl/bookingCtrl',
        'text!Widgets/menu/menuUserCtrl/menuUserCtrl.html'],
    function (ko, $) {

        $.widget("cc.menuUser", {

            options: {
                view: require('text!Widgets/menu/menuUserCtrl/menuUserCtrl.html'),
                viewModel: null,
                restaurantId: null
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);

                function MenuUserVM() {
                    
                    this.Categories = ko.observableArray([]);
                    
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