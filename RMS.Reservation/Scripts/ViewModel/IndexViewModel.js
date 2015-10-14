var urlPath = window.location.pathname;

$(function () {
    ko.applyBindings(indexVM);
    indexVM.loadRst();
});

var indexVM = {
    Restaurants: ko.observableArray([]),

    loadRst: function () {
        var self = this;
        //Ajax Call Get All Restaurant
        $.ajax({
            type: "GET",
            url: 'Home/GetAll',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                
                self.Restaurants(JSON.parse(data)); //Put the response in ObservableArray
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }
        });

    }
};


function Restaurants(Restaurants) {
    this.Id = ko.observable(Restaurants.Id);
    this.Title = ko.observable(Restaurants.Name);
    this.Adress = ko.observable(Restaurants.Adress.Country);
    this.Content = ko.observable(Restaurants.Desciption);
    this.Content = ko.observable(Restaurants.Cuisines);
}