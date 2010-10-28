var CONTENTS_LOADER = (function () {

    /**
    * Shows overlay when loading
    */
    function show() {
        hide();
        var width, height, x, y, search_height, overlay_back, overlay_swirling;
        width = $('#w-contents-left').width();
        height = $('#w-contents-left').height();
        x = $('#w-contents-left').position().left;
        y = $('#w-contents-left').position().top;
        search_height = $("#w-contents-search").height();
        overlay_back = $("<div>").attr("id", "overlay_back").css({ "width": width, "height": height - search_height, "top": y + search_height, "left": x });
        overlay_swirling = $("<img>").attr({ "src": "/img/loading.gif", "alt": "loading" }).css({ "width": "100px", "height": "100px", "margin-left": (width - 100) / 2, "margin-top": (height - 100) / 2 });
        overlay_back.append(overlay_swirling).prependTo($('#w-contents-left'));
    }

    /**
    * Hides overlay after loading
    */
    function hide() {
        $("#overlay_back").remove();
    }

    /**
    * Public interface
    */
    return {
        "show": show,
        "hide": hide
    };

} ());