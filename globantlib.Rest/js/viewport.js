var VIEWPORT = (function () {

    function adapt() {
        var headerHeight = $("#header-wrap").outerHeight(),
            footerHeight = $("#footer-wrap").outerHeight(),
            windowHeight = $(window).height(),
            contentHeight = windowHeight - (headerHeight + footerHeight) - 100;
        $("#content").css({
            'height': contentHeight
        });
    }

    function init() {
        window.onresize = adapt;
        adapt();
    }

    return {
        "init": init
    };

} ());