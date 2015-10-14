var urlPath = window.location.pathname;

$(function () {
    ko.applyBindings(ReviewVM);

});

var ReviewVM = {
    Reviews: ko.observableArray([]),
    
    Id: ko.observable(),
    Stars: ko.observable(),
    Food: ko.observable(),
    Service: ko.observable(),
    Ambience: ko.observable(),
    Comment: ko.observable(),
    Author: ko.observable(),
    RestaurantId: ko.observable(),


    loadReviews: function () {
        var self = this;
        var url = '/api/Review/GetAll/' + self.RestaurantId();
        
        $.ajax({
            type: "GET",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $.each(data, function(key, value) {
                    var review = new Review({
                        Food: value.Food,
                        Author: value.Author,
                        Comment: value.Comment,
                        
                    });
                    self.Reviews.push(review);
                });
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }
        });
    },


    saveReview: function () {
        var self = this;
        var data = new Review({
            Stars: self.Stars(),
            Food: self.Food(),
            Service: self.Service,
            Ambience: self.Ambience,
            Comment: self.Comment,
            Author: self.Author,
            RestaurantId: self.RestaurantId
        });
        $.ajax({
            type: "POST",
            url: '/api/Review/Save',
            data: ko.toJSON(
                data
            ),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log("Saved " + data);
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }
        });
    }
};


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