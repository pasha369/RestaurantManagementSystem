define(['knockout', 'jquery', 'jquery-ui',
        'Widgets/profileCtrl/profileCtrl',
        'Widgets/favoritesCtrl/favoritesCtrl',
        'Widgets/profileEditCtrl/profileEditCtrl',
        'Widgets/reservations/reservationUserCtrl/reservationUserCtrl',
        'text!Widgets/userCtrl/userCtrl.html'],
    function (ko, $, profileCtrl, favoritesCtrl, profileEditCtrl, reservationUserCtrl) {
  
        $.widget("cc.user", {
            
            options: {
                view: require('text!Widgets/userCtrl/userCtrl.html'),
                current: null
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);

                self.options.current =  $('.user-profile').userProfile();
                $('.user-favorite').favorites().hide();
                $('.user-reservations').userReservations().hide();
                $('.user-settings').settings().hide();

                $('#btnProfile').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.user-profile').userProfile("LoadProfile");
                    self.options.current.show();
                });
                $('#btnFavorite').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.user-favorite');
                    self.options.current.show();
                });
                $('#btnReservations').on('click', function () {
                    // Destroy ctrl with menu.
                    $('.user-reservations').userReservations('destroy');
                    // Show ctrl with reservations.
                    $('.user-reservations').userReservations();
                    
                    self.options.current.hide();
                    self.options.current = $('.user-reservations');
                    self.options.current.show();
                });
                $('#btnSettings').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.user-settings');
                    self.options.current.show();
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