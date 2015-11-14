define(['moment', ], function () {
    this.TimeOperator = function () {
        var moment = require('moment');
        /**
        * function for add minutes to time.
        * @returns { Date + 30 minutes. } 
        */
        function add(time, min) {
            return new Date(time.getTime() + min * 60000);
        }
        /**
         * fill time array by step 30 minutes.
        */
        this.fill = function (array) {
            var current = new Date();
            // begin working day.
            current.setHours(9);
            current.setMinutes(0);

            while (current.getHours() != 18) {
                var time = moment(current).format('HH:mm');
                array.push(time);
                current = add(current, 30);
            }
        }

    }
})