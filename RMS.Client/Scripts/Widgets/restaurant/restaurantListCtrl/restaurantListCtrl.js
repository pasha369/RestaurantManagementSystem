define(['knockout',
        'jquery',
        'jquery-ui',
        'toastr',
        'text!Widgets/restaurant/restaurantListCtrl/restaurantListCtrl.html',
       ],
    function (ko, $, photoUpload) {
        var toastr = require('toastr');

        $.widget("cc.restaurantList", {

            // These options will be used as defaults
            options: {
                view: require('text!Widgets/restaurant/restaurantListCtrl/restaurantListCtrl.html'),
                viewModel: null
            },

            loadtCss: function (url) {
                var link = document.createElement("link");
                link.type = "text/css";
                link.rel = "stylesheet";
                link.href = url;
                document.getElementsByTagName("head")[0].appendChild(link);
            },

            // Set up the widget
            _create: function () {
                var self = this;
                this.element.html(this.options.view);
                this.loadtCss('/Scripts/Widgets/restaurant/restaurantListCtrl/restaurantListCtrl.css');

                function restaurantVM() {
                    this.IsAjaxLoader = ko.observable(); 

                    this.RestaurantCount = ko.observable();
                    this.CityList = ko.observableArray([]);
                    this.CuisineList = ko.observableArray([]);
                    this.Cuisines = ko.observableArray([]);
                    this.Cities = ko.observableArray([]);
                    this.Restaurants = ko.observableArray([]);
                    this.ItemId = ko.observable(0);
                    this.SelectedCity = ko.observable();
                    this.SelectedCuisine = ko.observable();
                    this.Prev = function () {
                        var vm = self.options.viewModel;
                        if ((vm.ItemId() - 3) >= 0) {
                            vm.ItemId(vm.ItemId() - 3);
                        }
                        self._loadRestaurants(vm.ItemId());
                    }

                    this.Next = function () {
                        var vm = self.options.viewModel;
                        if ((vm.ItemId() + 3) <= vm.RestaurantCount()) {
                            vm.ItemId(vm.ItemId() + 3);
                        }
                        self._loadRestaurants(vm.ItemId());
                    }

                    this.SelectCity = function (item) {
                        var vm = self.options.viewModel;
                        vm.SelectedCity(item);
                        self._loadRestaurants(vm.ItemId(), vm.SelectedCity(), vm.SelectedCuisine());

                    }

                    this.SelectCuisine = function (item) {
                        var vm = self.options.viewModel;

                        vm.SelectedCuisine(item);
                        self._loadRestaurants(vm.ItemId(), vm.SelectedCity(), vm.SelectedCuisine());
                    }
                };

                self.options.viewModel = new restaurantVM();

                ko.applyBindings(self.options.viewModel, $('#restaurant-list-ctrl')[0]);
                self._loadRestaurants();
            },

            _loadRestaurants: function (itemId, city, cuisine) {
                var self = this;
                itemId = itemId > 0 ? itemId : null;
                city = !(typeof city === 'undefined') ? city : '';
                cuisine = !(typeof cuisine === 'undefined') ? cuisine : '';
                self.options.viewModel.IsAjaxLoader(true);
                
                $.ajax({
                    type: 'POST',
                    url: '/Restaurant/GetRestaurantPage/',
                    data: {
                        firstItemId: itemId,
                        city: city,
                        cuisine: cuisine
                    },
                    dataType: 'json',
                    success: function (restaurantLst) {
                        self.options.viewModel.Cuisines(restaurantLst.Cuisines);
                        self.options.viewModel.Cities(restaurantLst.Cities);

                        self.options.viewModel.CityList(restaurantLst.CityList);
                        self.options.viewModel.CuisineList(restaurantLst.CuisineList);
                        self.options.viewModel.RestaurantCount(restaurantLst.RestaurantCount);
                        self.options.viewModel.Restaurants([]);
                        var restaurants = [];
                        $.each(restaurantLst.RestaurantModels, function (k, value) {
                            value.PhotoUrl = value.PhotoUrl.replace("~", "");
                            value.Description = value.Description.length > 50 ? value.Description.substring(0, 50) + '...' : value.Description;
                            restaurants.push(value);
                        });
                        self.options.viewModel.Restaurants(restaurants);
                        self.options.viewModel.IsAjaxLoader(false);
                    }
                });
            },

            // Use the _setOption method to respond to changes to options
            _setOption: function (key, value) {
                $.Widget.prototype._setOption.apply(this, arguments);
                // In jQuery UI 1.9 and above, you use the _super method instead
                this._super("_setOption", key, value);
            },

            // Use the destroy method to clean up any modifications your widget has made to the DOM
            destroy: function () {
                // In jQuery UI 1.8, you must invoke the destroy method from the base widget
                $.Widget.prototype.destroy.call(this);
                // In jQuery UI 1.9 and above, you would define _destroy instead of destroy and not call the base method
            }
        });


    });