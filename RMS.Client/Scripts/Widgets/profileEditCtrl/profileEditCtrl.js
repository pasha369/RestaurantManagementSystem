define(['knockout', 'jquery', 'jquery-ui', 'toastr',
        'Custom/PhotoUpload',
        'text!Widgets/profileEditCtrl/profileEditCtrl.html'],
    function (ko, $) {
        var toastr = require('toastr');
        
        $.widget("cc.settings", {

            options: {
                view: require('text!Widgets/profileEditCtrl/profileEditCtrl.html'),
                current: null,
                viewModel: null,
            },

            _create: function () {
                var self = this;
                self.element.html(self.options.view);

                function userVM() {
                    this.Id = ko.observable();
                    this.Name = ko.observable();
                    this.Phone = ko.observable();
                    this.Password = ko.observable();
                    this.Position = ko.observable();
                    this.PhotoUrl = ko.observable();
                    
                    this.Email = ko.observable();
                    this.Skype = ko.observable();
                    this.Facebook = ko.observable();
                    this.About = ko.observable();

                    this.saveProfile = function() {
                        self._saveProfile();
                    };
                };
                self.options.viewModel = new userVM();

                PhotoUpload("#uploadPhoto", "/api/Image/UploadPhoto/");


                ko.applyBindings(self.options.viewModel, $('#profile-edit-ctrl')[0]);
                self._loadProfile();


            },
            _loadProfile: function () {
                var self = this;
                var vm = self.options.viewModel;
                $.ajax({
                    type: 'GET',
                    url: '/Profile/GetUser',
                    datatype: 'json',
                    success: function (data) {
                        
                        vm.Id(data.Id);
                        vm.Name(data.Name);
                        vm.Password(data.Password);
                        vm.Phone(data.Phone);
                        vm.Position(data.Position);
                        vm.Email(data.Email);
                        vm.Facebook(data.Facebook);
                        vm.About(data.About);
                        vm.Skype(data.Skype);
                        
                        if (data.PhotoUrl) {
                            vm.PhotoUrl(data.PhotoUrl.replace('~', ''));
                        }
                    },
                    error: function (err) {
                        console.log(err.status + " : " + err.statusText);
                    },
                });
            },
            _saveProfile: function () {
                var self = this;
                var vm = self.options.viewModel;
                $.ajax({
                    type: 'POST',
                    url: '/Profile/ProfileEdit',
                    datatype: 'json',
                    data: {
                        Id: vm.Id,
                        Name: vm.Name,
                        Password: vm.Password,
                        Skype: vm.Skype,
                        Email: vm.Email,
                        Facebook: vm.Facebook,
                        About: vm.About,
                        Phone: vm.Phone
                    },
                    success: function () {
                        toastr.success("Saved successful");
                    },
                    error: function (err) {
                        console.log(err.status + " : " + err.statusText);
                    },
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