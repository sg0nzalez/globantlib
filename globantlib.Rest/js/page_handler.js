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
    function contentCalendar(type, month, id) {
        var params = {
            type: type,
            id: id,
            month: month,
            routes: {
                get: 'data/leases.xml',
                submit: 'asd',
                prefix: '#devices/calendar/' + type + '/',
                postback: '#devices'
            }
        };
        calendar();
        LOADER.show("Loading device booking calendar...");
        CALENDAR.init(params, function () {
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
        devices();
        LOADER.show("Loading device list...");
        DEVICES_LIST.init(function () {
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
                get: 'data/leases.xml',
                submit: 'asd',
                prefix: '#devices/calendar/' + type,
                postback: '#devices'
            }
        };
        calendar();
        LOADER.show("Loading device booking calendar...");
        CALENDAR.init(params, function () {
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