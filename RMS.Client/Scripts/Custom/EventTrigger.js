define('Custom/EventTrigger', function () {
    
    function EventTrigger() {
        
        this._listeners = [];

        this.attach = function(listener) {
            this._listeners.push(listener);
        };
        this.notify = function(args) {
            var index;

            for (index = 0; index < this._listeners.length; index += 1) {
                this._listeners[index](args);
            }
        };
    }

    EventTrigger.prototype = {

    };
    
});
