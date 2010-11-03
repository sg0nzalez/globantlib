var CALENDAR = (function () {

    function init(params, callback) {
        callback();
        document.title = "Calendar @ Globant Library";
    }

    /**
    * Public interface
    */
    return {
        "init": init
    };

} ());