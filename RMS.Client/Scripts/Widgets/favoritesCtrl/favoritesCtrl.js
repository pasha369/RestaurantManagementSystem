define(['knockout', 'jquery', 'jquery-ui',
        'toastr','Widgets/bookingCtrl/bookingCtrl',
        'text!Widgets/favoritesCtrl/favoritesCtrl.html'],
    function (ko, $) {
        var toastr = require('toastr');

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

                    this.remove = function(item) {
                        self._removeFavorite(item);
                    };
                    this.reserve = function (item) {
                        $('#book-table-context').booking({ restaurantId: item.Id });
                    };
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
                            value.Description = value.Description.substring(0, 200) + '...';
                            if (value.PhotoUrl) {
                                value.PhotoUrl = value.PhotoUrl.replace("~", "");
                            }
                            vm.favorites.push(value);
                        });
                    },
                    error: function (err) {
                        console.log(err.status + " : " + err.statusText);
                        
                    }
                });
            },
            _removeFavorite: function (item) {
                var self = this;
                var vm = self.options.viewModel;
                
                $.ajax({
                    type: 'POST',
                    url: '/Profile/RemoveFavorite/' + item.Id,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    complete: function (data) {
                        var index = self.options.viewModel.favorites().indexOf(item);
                        if (index > -1) {
                            self.options.viewModel.favorites.splice(index, 1);
                        }
                        toastr.success(item.Name + ' was removed'); 
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