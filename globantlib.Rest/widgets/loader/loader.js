var LOADER = (function () {

    var idleTimeout = null;

    function showIdle() {
        clearTimeout(idleTimeout);
        $("#loading-message")
            .text("Something weird is going on... Please try again");
    }

    /**
    * Shows overlay when loading
    */
    function show(message) {
        hide();
        $("#loading-message")
            .attr('class', 'loading')
            .text(message);
        $("#w-contents-left").css({
            "opacity": 0.5,
            "overflow": "hidden"
        });
        $("loading-message").text(message);
        document.title = message + " @ Globant Library";
        idleTimeout = setTimeout(showIdle, 7000);
    }

    /**
    * Hides overlay after loading
    */
    function hide() {
        clearTimeout(idleTimeout);
        $("#w-contents-left").css({
            "opacity": 1,
            "overflow": ""
        });
        $("#loading-message")
            .attr('class', 'ready')
            .text("Ready");
    }

    /**
    * Public interface
    */
    return {
        "show": show,
        "hide": hide
    };

} ());