define(['knockout', 'jquery', 'jquery-ui',
        'Widgets/profileCtrl/profileCtrl',
        'Widgets/restaurantEditCtrl/restaurantEditCtrl',
        'Widgets/profileEditCtrl/profileEditCtrl',
        'Widgets/menu/menuEditCtrl/menuEditCtrl',
        'Widgets/reservations/reservationTblCtrl/reservationTblCtrl',
        'Widgets/order/orderManageCtrl/orderManageCtrl',
        'text!Widgets/clientCtrl/clientCtrl.html'],
    function (ko, $, profileCtrl, restaurantEditCtrl, profileEditCtrl, menuCtrl, reservationTblCtrl) {

        $.widget("cc.client", {

            options: {
                view: require('text!Widgets/clientCtrl/clientCtrl.html'),
                current: null,
                restaurant: null
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);
                self._loadRestaurant();
                
                self.options.current = $('.client-profile').userProfile();
                $('.orders').orderManageCtrl().hide();
                $('.restaurant').restaurantEdit({ restaurantId: self.options.restaurant.Id }).hide();
                $('.reserved-tbl').reservationTbl({ restaurantId: self.options.restaurant.Id }).hide();
                $('.restaurant-menu').menuEdit({ restaurantId: self.options.restaurant.Id }).hide();
                $('.client-settings').settings().hide();


                $('#btnProfile').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.client-profile').userProfile("LoadProfile");;
                    self.options.current.show();
                });
                $('#btnRestaurant').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.restaurant');
                    self.options.current.show();
                });
                $('#btnReservetions').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.reserved-tbl');
                    self.options.current.show();
                });
                $('#btnOrder').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.orders');
                    self.options.current.show();
                });
                $('#btnMenu').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.restaurant-menu');
                    self.options.current.show();
                });
                $('#btnSettings').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.client-settings');
                    self.options.current.show();
                });
            },
            _loadRestaurant: function () {
                var self = this;

                $.ajax({
                    type: 'GET',
                    url: '/Restaurant/GetRestaurantByClient',
                    datatype: 'json',
                    async: false,
                    success: function(data) {
                        self.options.restaurant = JSON.parse(data);
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