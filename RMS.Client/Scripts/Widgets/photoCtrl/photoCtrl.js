define(['knockout',
        'jquery',
        'jquery-ui',
        'text!Widgets/PhotoCtrl/PhotoCtrl.html'],
    function (ko, $) {

        $.widget("cc.photopicker", $.ui.dialog, {

            options: {
                view: require('text!Widgets/PhotoCtrl/PhotoCtrl.html'),
                url: '/api/Image/UploadPhoto'
            },
            open: function () {
                console.log("open");

                // Invoke the parent widget's open().
                return this._super();
            },

            // Set up the widget
            _create: function () {
                var self = this;

                self.element.html(self.options.view);
                self.element.dialog();

                $("#uploadFile").on("change", function () {
                    self._showImage(this);
                });
                $("#btnSavePhoto").on("click", function () { self._savePhoto();});

                $('#uploadFile').on('change', function (e) {
                    var files = e.target.files;
                    //var myID = 3; //uncomment this to make sure the ajax URL works
                    if (files.length > 0) {
                        if (window.FormData !== undefined) {
                            var data = new FormData();
                            for (var x = 0; x < files.length; x++) {
                                data.append("file" + x, files[x]);
                            }

                            $.ajax({
                                type: "POST",
                                url: self.options.url ,
                                contentType: false,
                                processData: false,
                                data: data,
                                success: function (result) {
                                    console.log(result);
                                },
                                error: function (xhr, status, p3, p4) {
                                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                                    if (xhr.responseText && xhr.responseText[0] == "{")
                                        err = JSON.parse(xhr.responseText).Message;
                                    console.log(err);
                                }
                            });
                        } else {
                            alert("This browser doesn't support HTML5 file uploads!");
                        }
                    }
                });

            },
            _savePhoto: function () {
                var self = this;
                var image = $("#blah").get(0);
                var formData = new FormData();
                formData.append("imageFile", image);
                
                $.ajax({
                    type: "POST",
                    url: "/api/Image/UploadPhoto",
                    processData: false,
                    contentType: false,
                    data: formData
                });
            },

            _showImage: function (input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#blah')
                            .attr('src', e.target.result);
                    };

                    reader.readAsDataURL(input.files[0]);
                }
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