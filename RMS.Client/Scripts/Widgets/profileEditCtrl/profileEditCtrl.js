define(['knockout', 'jquery', 'jquery-ui',
        'text!Widgets/profileEditCtrl/profileEditCtrl.html'],
    function (ko, $) {

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

                    this.saveProfile = function() {
                        self._saveProfile();
                    };
                };
                self.options.viewModel = new userVM();

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
                        Phone: vm.Phone
                    },
                    success: function () {
                        
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