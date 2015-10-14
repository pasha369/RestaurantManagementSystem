define(['knockout', 'jquery', 'jquery-ui',
        'text!Widgets/favoritesCtrl/favoritesCtrl.html'],
    function (ko, $, preloader) {

        $.widget("cc.favorites", {

            options: {
                view: require('text!Widgets/favoritesCtrl/favoritesCtrl.html'),
                viewModel: null,
                restaurantId: null,
            },

            _create: function () {
                var self = this;

                self.element.html(self.options.view);
               
                function favoritesVM() {

                    this.favorites = ko.observableArray([]);
                };

                self.options.viewModel = new favoritesVM();

                ko.applyBindings(self.options.viewModel, $("#favorites")[0]);
                self._loadFavorites();
            },
            _loadFavorites: function () {
                var self = this;
                var vm = self.options.viewModel;
                
                $.ajax({
                    type: 'GET',
                    url: '/Profile/Favorites',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $.each(JSON.parse(data), function (key, value) {
                            value.PhotoUrl = value.PhotoUrl.replace("~", "");
                            vm.favorites.push(value);
                        });
                    },
                    error: function (err) {
                        console.log(err.status + " : " + err.statusText);
                        
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