define(['knockout', "knockout.validation", 'jquery', 'jquery-ui', 'datepicker', 'moment', 'toastr',
        'text!Widgets/menu/menuEditCtrl/menuEditCtrl.html'],
    function (ko, validation, $, datepicker) {
        var moment = require('moment');
        var toastr = require('toastr');

        $.widget("cc.menuEdit", {

            options: {
                view: require('text!Widgets/menu/menuEditCtrl/menuEditCtrl.html'),
                viewModel: null,
                restaurantId: null,
            },

            _create: function () {
                var self = this;

                self.element.html(self.options.view);

                function MenuVM() {
                    this.Categories = ko.observableArray([]);

                    this.CategoryName = ko.observable();
                    this.DishName = ko.observable();
                    this.DishCost = ko.observable();
                    
                    this.AddCategory = function () {
                        self._addCategory();
                    };
                    this.AddDish = function() {
                        self._addDish();
                    };
                }

                self.options.viewModel = new MenuVM();
                ko.applyBindings(self.options.viewModel, $("#menu-ctrl")[0]);
                self._loadCategories();
            },
            _loadCategories: function () {
                var self = this;
                var vm = self.options.viewModel;

                $.ajax({
                    type: 'Get',
                    url: '/api/Menu/GetCategories/',
                    contentType: "application/json; charset=utf-8",
                    data: {rstId: 1},
                    dataType: "json",
                    success: function (data) {
                        $.each(data, function(key, value) {
                            vm.Categories.push(value);
                        });
                        toastr.success('Add category saved');
                    },
                    error: function (err) {
                        toastr.warning('Something wrong');
                    }
                });

            },
            _addCategory: function () {
                var self = this;
                var vm = self.options.viewModel;
                var category = {
                    Name: vm.CategoryName(),
                    MenuId: 1
                };
                
                $.ajax({
                    type: 'POST',
                    url: '/api/Menu/AddCategory',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: ko.toJSON(category),
                    success: function () {
                        toastr.success('Add category saved');
                    },
                    error: function (err) {
                        toastr.warning('Something wrong');
                    }
                });
            },
            _addDish: function () {
                var self = this;
                var vm = self.options.viewModel;
                var dish = {
                    Name: vm.DishName,
                    Cost: vm.DishCost,
                    CategoryId: 1, //TODO: Category
                };
                
                $.ajax({
                    type: 'POST',
                    url: '/api/Dish/Add',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: ko.toJSON(dish),
                    success: function () {
                        toastr.success('Add category saved');
                    },
                    error: function (err) {
                        toastr.warning('Something wrong');
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