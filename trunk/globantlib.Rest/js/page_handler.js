var PAGE_HANDLER = (function () {

    function contentList(page, query) {
        CONTENTS_LOADER.show();
        CONTENTS_LIST.init(page, query, function () {
            CONTENTS_LIST.show();
            CONTENTS_SEARCH.init(query);
            CONTENTS_DETAILS.hide();
            CONTENTS_LOADER.hide();
        });
    }

    function contentDetails(id) {
        CONTENTS_LOADER.show();
        CONTENTS_DETAILS.init(id, function () {
            CONTENTS_DETAILS.show();
            CONTENTS_SEARCH.hide();
            CONTENTS_LIST.hide();
            CONTENTS_LOADER.hide();
        });
    }

    function devicesList(page) {
        document.location.href = "/";
    }

    function devicesCalendar(id) {
    }

    return {
        "contentList": contentList,
        "contentDetails": contentDetails,
        "devicesList": devicesList,
        "devicesCalendar": devicesCalendar
    }

} ());