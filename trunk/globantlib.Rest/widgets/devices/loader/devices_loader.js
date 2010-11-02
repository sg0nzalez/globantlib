var DEVICES_LOADER = (function () {

    function show(message) {
        $("#loading-message")
            .attr("class", "loading")
            .text(message);
    }
    function hide() {
        $("#loading-message")
            .attr("class", "ready")
            .text("Ready");
    }

    return {
        "show": show,
        "hide": hide
    };

} ());