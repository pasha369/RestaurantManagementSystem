define(['knockout', 'jquery', 'jquery-ui',
        'text!Widgets/commentCtrl/commentCtrl.html'],
    function (ko, $) {

        $.widget("cc.comment", {

            options: {
                view: require('text!Widgets/commentCtrl/commentCtrl.html'),
                viewModel: null,
                restaurantId: null,
                eventTrigger: null,
            },

            _create: function () {
                var self = this;

                self.element.html(self.options.view);

                function commentVM() {
                    
                    this.Id = ko.observable();
                    this.Stars = ko.observable();
                    this.Food = ko.observable();
                    this.Service = ko.observable();
                    this.Ambience = ko.observable();
                    this.Comment = ko.observable();
                    this.Author = ko.observable();
                    this.RestaurantId = ko.observable(self.options.restaurantId);

                    this.saveReview = function () {
                       
                        self._saveReview();
                    };
                    
                };

                self.options.viewModel = new commentVM();
                
                ko.applyBindings(self.options.viewModel, $("#review")[0]);
            },

            _saveReview: function() {
                var self = this;
                var vm = self.options.viewModel;
                var review = ({
                    Stars: vm.Stars(),
                    Food: vm.Food(),
                    Service: vm.Service(),
                    Ambience: vm.Ambience(),
                    Comment: vm.Comment(),
                    RestaurantId: vm.RestaurantId()
                });
                
                $.ajax({
                    type:'POST',
                    url: '/api/Review/Save',
                    data: ko.toJSON(review),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        self.options.eventTrigger.notify(review);
                    },
                    error: function (e) {
                        if (e.status == 401) {
                            window.location = "/Account/Login";
                        }
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