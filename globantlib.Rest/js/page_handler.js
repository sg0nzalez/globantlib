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
        LOADER.show("Loading book list...");
        CONTENTS_LIST.init(page, query, function () {
            contents();
            contentDeactivate()
            CONTENTS_SEARCH.init(query);
            CONTENTS_REQUESTS.initSidebar();
        });
    }
    function contentDetails(id) {
        LOADER.show("Loading book details...");
        CONTENTS_DETAILS.init(id, function () {
            contents();
            contentDeactivate()
            CONTENTS_SEARCH.init();
            CONTENTS_REQUESTS.initSidebar();
        });
    }
    function contentRequests() {
        LOADER.show("Loading book requests...");
        CONTENTS_REQUESTS.init(function () {
            contents();
            contentDeactivate()
            CONTENTS_SEARCH.init();
        });
    }
    function contentCalendar(type, month, year, id) {
        var params = {
            type: type,
            id: id,
            month: month,
            year: year,
            routes: {
                get: "/LibraryService.mvc/ContentCalendar",
                submit: '/LibraryService.mvc/LeaseSubmit',
                prefix: '#contents/calendar/' + type,
                postback: '#contents'
            }
        };
        LOADER.show("Loading device booking calendar...");
        CALENDAR.init(params, function () {
            calendar();
            deviceDeactivate();
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
        LOADER.show("Loading device list...");
        DEVICES_LIST.init(function () {
            devices();
            deviceDeactivate();
        });
    }
    function deviceCalendar(type, month, year, id) {
        var params = {
            type: type,
            id: id,
            month: month,
            year: year,
            routes: {
                get: "/DeviceService.mvc/DeviceCalendar",
                submit: '/DeviceService.mvc/LeaseSubmit',
                prefix: '#devices/calendar/' + type,
                postback: '#devices'
            }
        };
        LOADER.show("Loading device booking calendar...");
        CALENDAR.init(params, function () {
            calendar();
            deviceDeactivate();
        });
    }

    /**
    * Public interface
    */
    return {
        "contentList": contentList,
        "contentDetails": contentDetails,
        "contentRequests": contentRequests,
        "contentCalendar": contentCalendar,
        "deviceList": deviceList,
        "deviceCalendar": deviceCalendar
    }

} ());