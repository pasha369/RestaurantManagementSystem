define(['knockout', 'jquery', 'jquery-ui',
        'text!Widgets/userProfileCtrl/userProfileCtrl.html'],
    function (ko, $) {

        $.widget("cc.userProfile", {

            options: {
                view: require('text!Widgets/userProfileCtrl/userProfileCtrl.html'),
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
                    this.Position = ko.observable();
                    this.PhotoUrl = ko.observable();
                };
                self.options.viewModel = new userVM();

                ko.applyBindings(self.options.viewModel, $('#user-profile-ctrl')[0]);
                self._loadProfile();
                
                $("#btnChangePhoto").on("click", function () {
                    var dialog = $("<div>").photopicker({
                        userId: self.options.viewModel.Id
                    });
                });
                
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
                        vm.Phone(data.Phone);
                        vm.Position(data.Position);
                        vm.PhotoUrl(data.PhotoUrl.replace('~', '')) ;
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