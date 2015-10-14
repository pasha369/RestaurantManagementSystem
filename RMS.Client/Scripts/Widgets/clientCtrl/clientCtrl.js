define(['knockout', 'jquery', 'jquery-ui',
        'Widgets/userProfileCtrl/userProfileCtrl',
        'Widgets/reservationCtrl/reservationCtrl',
        'Widgets/restaurantEditCtrl/restaurantEditCtrl',
        'text!Widgets/clientCtrl/clientCtrl.html'],
    function (ko, $, userProfileCtrl, restaurantEditCtrl) {

        $.widget("cc.client", {

            options: {
                view: require('text!Widgets/clientCtrl/clientCtrl.html'),
                current: $('.client-info'),
                restaurant: null
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);
                self._loadRestaurant();
                
                //$('.client-profile').userProfile();
                $('.restaurant').restaurantEdit({ restaurantId: self.options.restaurant.Id }).hide();
                $('.reserved-tbl').reservations({ RestaurantId: self.options.restaurant.Id }).hide();



                $('#btnProfile').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.client-profile');
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