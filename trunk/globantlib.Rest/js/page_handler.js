var PAGE_HANDLER = (function () {

    function contentDeactivate() {
        CONTENTS_DETAILS.hide();
        CONTENTS_LOADER.hide();
        CONTENTS_REQUESTS.hide();
        CONTENTS_LIST.hide();
    }


    function contentList(page, query) {
        CONTENTS_LOADER.show("Loading book list...");
        CONTENTS_LIST.init(page, query, function () {
            contentDeactivate()
            CONTENTS_SEARCH.init(query);
            CONTENTS_REQUESTS.initSidebar();
            CONTENTS_LIST.show();
        });
    }

    function contentDetails(id) {
        CONTENTS_LOADER.show("Loading book details...");
        CONTENTS_DETAILS.init(id, function () {
            contentDeactivate()
            CONTENTS_DETAILS.show();
            CONTENTS_SEARCH.init();
            CONTENTS_REQUESTS.initSidebar();
        });
    }

    function contentRequests() {
        CONTENTS_LOADER.show("Loading book requests...");
        CONTENTS_REQUESTS.init(function () {
            contentDeactivate()
            CONTENTS_REQUESTS.initSidebar();
            CONTENTS_SEARCH.init();
            CONTENTS_REQUESTS.show();
        });
    }

    function devicesList(page) {
        document.location.href = "/devices/ViewDevices.aspx";
    }

    function devicesCalendar(id) {
    }

    return {
        "contentList": contentList,
        "contentDetails": contentDetails,
        "contentRequests": contentRequests,
        "devicesList": devicesList,
        "devicesCalendar": devicesCalendar
    }

} ());