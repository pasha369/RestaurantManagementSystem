define(['knockout', 'jquery', 'jquery-ui',
        'text!Widgets/searchCtrl/searchCtrl.html'],
    function (ko, $) {

        $.widget("cc.search", {

            options: {
                view: require('text!Widgets/searchCtrl/searchCtrl.html'),
                viewModel: null,
                restaurantId: null,
                name: null,
            },

            _create: function () {
                var self = this;

                self.element.html(self.options.view);

                function searchVM() {

                    this.Restaurants = ko.observableArray([]);

                    this.search = function() {
                        self._loadRestaurants();
                    };
                    this.reserve = function (item) {
                        $('#book-table-context').booking({ restaurantId: item.Id });
                    };
                };

                self.options.viewModel = new searchVM();
                $('#search-field').val(self.options.name);
                
                ko.applyBindings(self.options.viewModel, $("#search-ctrl")[0]);
                self._loadRestaurants();
            },

            _loadRestaurants: function () {
                var self = this;
                var vm = self.options.viewModel;

                $.ajax({
                    type: 'GET',
                    url: '/api/Search/FindByName',
                    contentType: "application/json; charset=utf-8",
                    data: { name: $('#search-field').val() },
                    dataType: "json",
                    success: function (data) {
                        vm.Restaurants([]);
                        $.each(data, function (key, value) {
                            if (value.PhotoUrl) {
                                value.PhotoUrl = value.PhotoUrl.replace('~', '');
                            }
                            value.Description = value.Description.substring(0, 200) + '...';
                            vm.Restaurants.push(value);
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