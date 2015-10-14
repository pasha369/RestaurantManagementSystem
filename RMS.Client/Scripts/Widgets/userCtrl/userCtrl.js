define(['knockout', 'jquery', 'jquery-ui',
        'Widgets/userProfileCtrl/userProfileCtrl',
        'Widgets/favoritesCtrl/favoritesCtrl',
        'text!Widgets/userCtrl/userCtrl.html'],
    function (ko, $, userProfileCtrl, favoritesCtrl) {
  
        $.widget("cc.user", {
            
            options: {
                view: require('text!Widgets/userCtrl/userCtrl.html'),
                current: $('.user-info')
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);

                $('.user-profile').userProfile();
                $('.user-favorite').favorites().hide();

                $('#btnProfile').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.user-profile');
                    self.options.current.show();
                });
                $('#btnFavorite').on('click', function () {
                    self.options.current.hide();
                    self.options.current = $('.user-favorite');
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