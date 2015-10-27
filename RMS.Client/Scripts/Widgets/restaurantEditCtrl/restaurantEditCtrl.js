define(['knockout',
        'jquery',
        'jquery-ui',
        'toastr',
        'Custom/PhotoUpload',
        'text!Widgets/restaurantEditCtrl/restaurantEditCtrl.html'],
    function (ko, $, photoUpload) {
        var toastr = require('toastr');
        $.widget("cc.restaurantEdit", {

            // These options will be used as defaults
            options: {
                view: require('text!Widgets/restaurantEditCtrl/restaurantEditCtrl.html'),
                viewModel: null,
                restaurantId: "-1",
            },

            // Set up the widget
            _create: function () {
                var self = this;
                this.element.html(this.options.view);


                function restaurantVM() {
                    this.Id = ko.observable();
                    this.Title = ko.observable();
                    this.PhoneNumber = ko.observable();
                    this.Description = ko.observable();
                    this.PhotoUrl = ko.observable();

                    this.saveRestaurant = function() {
                        self._saveRestaurant();
                    };
                };

                self.options.viewModel = new restaurantVM();
                
                PhotoUpload("#uploadImg", "/api/Image/UploadRstPic/" + self.options.restaurantId);
                
                ko.applyBindings(self.options.viewModel, $('#restaurant-edit')[0]);
                self._loadRestaurant();
            },

            _loadRestaurant: function () {
                var self = this;
                var vm = self.options.viewModel;
                var url = '/Restaurant/GetById/' + self.options.restaurantId;

                $.ajax({
                    type: "GET",
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        vm.Id(data.Id);
                        vm.Title(data.Name);
                        vm.PhoneNumber(data.PhoneNumber);
                        vm.Description(data.Description);
                        if (data.PhotoUrl) {
                            vm.PhotoUrl(data.PhotoUrl.replace("~", ""));
                        }

                    },
                    error: function (err) {
                        alert(err.status + " : " + err.statusText);
                    }
                });

            },

            _saveRestaurant: function () {
                var self = this;
                var url = '/Restaurant/RestaurantEdit/';
                var data = {
                    Id: self.options.viewModel.Id(),
                    Name: self.options.viewModel.Title(),
                    PhoneNumber: self.options.viewModel.PhoneNumber(),
                    Description: self.options.viewModel.Description(),
                };

                $.ajax({
                    type: "POST",
                    url: url,
                    data: ko.toJSON(
                        data
                    ),
                    contentType: "application/json; charset=utf-8",

                    success: function (data) {
                        toastr.success("Saved successful");
                    },
                    error: function (err) {
                        alert(err.status + " : " + err.statusText);
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