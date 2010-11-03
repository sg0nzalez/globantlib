var PAGE_HANDLER = (function () {

    /**
    * Changes the containers
    */
    function contents() {
        $("#w-devices").hide();
        $("#w-contents").show();
    }
    function devices() {
        $("#w-devices").show();
        $("#w-contents").hide();
    }

    /**
    * Contents
    */
    function contentDeactivate() {
        CONTENTS_DETAILS.hide();
        CONTENTS_LOADER.hide();
        CONTENTS_REQUESTS.hide();
        CONTENTS_LIST.hide();
    }
    function contentList(page, query) {
        contents();
        CONTENTS_LOADER.show("Loading book list...");
        CONTENTS_LIST.init(page, query, function () {
            contentDeactivate()
            CONTENTS_SEARCH.init(query);
            CONTENTS_REQUESTS.initSidebar();
        });
    }
    function contentDetails(id) {
        contents();
        CONTENTS_LOADER.show("Loading book details...");
        CONTENTS_DETAILS.init(id, function () {
            contentDeactivate()
            CONTENTS_SEARCH.init();
            CONTENTS_REQUESTS.initSidebar();
        });
    }
    function contentRequests() {
        contents();
        CONTENTS_LOADER.show("Loading book requests...");
        CONTENTS_REQUESTS.init(function () {
            contentDeactivate()
            CONTENTS_SEARCH.init();
        });
    }

    /**
    * Devices
    */
    function devicesDeactivate() {
        DEVICES_LOADER.hide();
        DEVICES_LIST.hide();
        DEVICES_CALENDAR.hide();
    }
    function deviceList(page) {
        devices();
        DEVICES_LOADER.show("Loading device list...");
        DEVICES_LIST.init(page, function () {
            //devicesDeactivate();
        });
    }
    function deviceCalendar(id) {
        devices();
        DEVICES_LOADER.show("Loading device calendar...");
        DEVICES_CALENDAR.init(id, function () {
            devicesDeactivate();
        });
    }

    return {
        "contentList": contentList,
        "contentDetails": contentDetails,
        "contentRequests": contentRequests,
        "deviceList": deviceList,
        "deviceCalendar": deviceCalendar
    }

} ());