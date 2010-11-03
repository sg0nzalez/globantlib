var DEVICES_LIST = (function () {

    /**
    * Manage display state
    */
    function show() {
        $("#w-devices-list").show();
    }
    function hide() {
        $("#w-devices-list").hide();
    }

    /**
    * Initialize
    */
    function init(callback) {
        var service = './DeviceService.mvc/',
            target = document.getElementById('w-devices-list');
        XML.transformWithCallback(service, 'widgets/devices/list/list.xsl', target, function () {
            callback();
            show();
        });
    }

    return {
        "init": init,
        "show": show,
        "hide": hide
    };

} ());