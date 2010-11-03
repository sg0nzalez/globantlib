var DEVICES = (function () {

    function hide() {
        $("#w-devices-list").hide();
    }
    function show() {
        $("#w-devices-list").show();
    }

    /**
    * Load and show the list of contents
    */
    function showList(page) {
        var xml = './DeviceService.mvc/',
            target = document.getElementById('w-devices-list');
        if (page) {
            xml += '?page=' + page;
        }
        target.className = 'loading';
        XML.transformWithCallback(xml, '/widgets/devices/xsl/DevicesHire.xsl', target, function () {
            target.className = '';
        });
    }

    function showCalendar(id) { 
        
    }
    return {
        "show": show,
        "hide" : hide,
        "showList": showList,
        "showCalendar": showCalendar
    };

} ());