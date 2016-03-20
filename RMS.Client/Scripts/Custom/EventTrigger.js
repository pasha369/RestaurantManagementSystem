define([] , function () {

        this.EventTrigger =  function () {

            function Event(context, callback) {
                this._context = context;
                this._callback = callback;
            }
            
            this._listeners = [];


            this.attach = function(listener, context) {
                var event = new Event(context, listener);
                this._listeners.push(event);
            };

            this.notify = function() {
                var index;

                for (index = 0; index < this._listeners.length; index += 1) {
                    this._listeners[index]._callback.apply(this._listeners[index]._context, arguments);
                }
            };
        }

});
