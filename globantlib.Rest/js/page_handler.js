var PAGE_HANDLER = (function () {

    /**
    * Changes the containers
    */
    function contents() {
        $("#w-devices").hide();
        $("#w-contents").show();
        $("#w-calendar").hide();
    }
    function devices() {
        $("#w-devices").show();
        $("#w-contents").hide();
        $("#w-calendar").hide();
    }
    function calendar() {
        $("#w-devices").hide();
        $("#w-contents").hide();
        $("#w-calendar").show();
    }

    /**
    * Contents
    */
    function contentDeactivate() {
        CONTENTS_DETAILS.hide();
        LOADER.hide();
        CONTENTS_REQUESTS.hide();
        CONTENTS_LIST.hide();
    }
    function contentList(page, query) {
        contents();
        LOADER.show("Loading book list...");
        CONTENTS_LIST.init(page, query, function () {
            contentDeactivate()
            CONTENTS_SEARCH.init(query);
            CONTENTS_REQUESTS.initSidebar();
        });
    }
    function contentDetails(id) {
        contents();
        LOADER.show("Loading book details...");
        CONTENTS_DETAILS.init(id, function () {
            contentDeactivate()
            CONTENTS_SEARCH.init();
            CONTENTS_REQUESTS.initSidebar();
        });
    }
    function contentRequests() {
        contents();
        LOADER.show("Loading book requests...");
        CONTENTS_REQUESTS.init(function () {
            contentDeactivate()
            CONTENTS_SEARCH.init();
        });
    }
    function contentCalendar(deviceId) {
        calendar();
        LOADER.show("Loading device booking calendar...");
        CALENDAR.init({
            id: deviceId,
            service: {
                get: "/DeviceService.mvc/",
                book: "/DeviceService.mvc/"
            }
        }, function () {
            contentDeactivate()
        });
    }

    /**
    * Devices
    */
    function deviceDeactivate() {
        LOADER.hide();
        DEVICES_LIST.hide();
    }
    function deviceList() {
        devices();
        LOADER.show("Loading device list...");
        DEVICES_LIST.init(function () {
            deviceDeactivate();
        });
    }
    function deviceCalendar(deviceId) {
        calendar();
        LOADER.show("Loading device booking calendar...");
        CALENDAR.init({
            id: deviceId,
            service: {
                get: "/DeviceService.mvc/",
                book: "/DeviceService.mvc/"
            }
        }, function () {
            deviceDeactivate();
        });
    }

    return {
        "contentList": contentList,
        "contentDetails": contentDetails,
        "contentRequests": contentRequests,
        "contentCalendar": contentCalendar,
        "deviceList": deviceList,
        "deviceCalendar": deviceCalendar
    }

} ());