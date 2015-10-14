var urlPath = window.location.pathname;

require(["knockout", "jquery"], function (ko, $) {
    $(function () {
        
        var RestaurantVM = {
            Restaurants: ko.observableArray([]),

            loadRst: function () {
                var self = this;
                //Ajax Call Get All Restaurant
                $.ajax({
                    type: "GET",
                    url: '/Restaurant/GetAll',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var source = $.each(JSON.parse(data), function(key, value) {
                   
                            value.PhotoUrl = value.PhotoUrl.replace('~', '    ');
                            value.Description = value.Description.substring(0, 120) + '...';
                            value.CommentCount = value.Reviews.length;
                        });
                        self.Restaurants(source); //Put the response in ObservableArray
                    },
                    error: function (err) {
                        alert(err.status + " : " + err.statusText);
                    }
                });

            }
        };


        function Restaurants(restaurants) {
            this.Id = ko.observable(restaurants.Id);
            this.Title = ko.observable(restaurants.Name);
            this.Adress = ko.observable(restaurants.Adress.Country);
            this.Description = ko.observable(restaurants.Desciption).extend({ maxlength: 128 });

            this.PhotoUrl = ko.observable(restaurants.PhotoUrl);
        }



        ko.applyBindings(RestaurantVM);
        RestaurantVM.loadRst();
    });
});

