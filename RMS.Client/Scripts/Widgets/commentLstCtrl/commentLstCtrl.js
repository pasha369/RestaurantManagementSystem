define(['knockout',
        'jquery',
        'jquery-ui',
        'text!Widgets/commentLstCtrl/commentLstCtrl.html'],
    function (ko, $) {

        $.widget("cc.commentList", {

            // These options will be used as defaults
            options: {
                view: require('text!Widgets/commentLstCtrl/commentLstCtrl.html'),
                viewModel : null,
                RestaurantId: "-1",
                eventTrigger : null,
            },

            // Set up the widget
            _create: function () {
                this.element.html(this.options.view);

                var self = this;
                function commentListVM (){
                    
                    this.Reviews = ko.observableArray([]);
                };

                self.options.viewModel = new commentListVM();
                
                self.options.eventTrigger.attach(self._refresh, this);
                
                ko.applyBindings(self.options.viewModel, $("#comment-list")[0]);
                self._loadReviews(self.options.RestaurantId);
            },
            
            _loadReviews: function () {
                var self = this;
                var url = '/api/Review/GetAll/' + self.options.RestaurantId;

                function Review(review) {
                    this.Id = ko.observable(review.Id);
                    this.Stars = ko.observable(review.Stars);
                    this.Food = ko.observable(review.Food);
                    this.Service = ko.observable(review.Service);
                    this.Ambience = ko.observable(review.Ambience);
                    this.Comment = ko.observable(review.Comment);
                    this.Author = ko.observable(review.Author);
                    this.RestaurantId = ko.observable(review.RestaurantId);

                }

                $.ajax({
                    type: "GET",
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $.each(data, function (key, value) {
                            var review = new Review(value);
                            self.options.viewModel.Reviews.push(review);
                        });
                    },
                    error: function (err) {
                        alert(err.status + " : " + err.statusText);
                    }
                });
            },
            _refresh: function(review) {
                var self = this;
                self.options.viewModel.Reviews.push(review);
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