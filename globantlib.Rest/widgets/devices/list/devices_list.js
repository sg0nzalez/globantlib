var DEVICES_LIST = (function () {

    /**
    * Manage display state
    */
    function show() {
        $("#w-devices-list-ph").show();
    }
    function hide() {
        $("#w-devices-list-ph").hide();
    }

    /**
    * Initialize
    */
    function init(callback) {
        var service = '/DeviceService.mvc/',
            target = document.getElementById('w-devices-list-ph');
        XML.transformWithCallback(service, 'widgets/devices/list/list.xsl', target, function () {
            callback();
            show();
            document.title = "Devices @ Globant Library";
        });
    }

    return {
        "init": init,
        "show": show,
        "hide": hide
    };

} ());