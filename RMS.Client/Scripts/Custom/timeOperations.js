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
         * Fill top header of table reservations (time) from begin working 
         * day to end. Transition 30 minutes.
         * @param { Result array. } array 
         * @param { Hour when working day start.} startHour 
         * @param { Hour when working day ending. } endHour 
         */
        this.fill = function (array, startHour, endHour) {
            var current = new Date();

            current.setHours(startHour);
            current.setMinutes(0);

            while (current.getHours() != endHour) {
                var time = moment(current).format('HH:mm');
                array.push(time);
                current = add(current, 30);
            }
        }

    }
})