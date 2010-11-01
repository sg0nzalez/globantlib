var CONTENTS_LOADER = (function () {

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
    }

    /**
    * Hides overlay after loading
    */
    function hide() {
        //$("#overlay_back").remove();
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