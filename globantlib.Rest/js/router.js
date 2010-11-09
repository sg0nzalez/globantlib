var ROUTER = (function () {

    var root;

    // main method
    root = function () {
        root.contents.list();
    };

    // devices list first page
    root.devices = function () {
        PAGE_HANDLER.deviceList();
    };

    // devices list page N
    root.devices.list = function (page) {
        PAGE_HANDLER.deviceList(page);
    };

    // content calendar
    root.devices.calendar = function (type, month, year, id) {
        PAGE_HANDLER.deviceCalendar(type, month, year, id);
    };

    // contents list first page
    root.contents = function () {
        PAGE_HANDLER.contentList();
    };

    // contents list page N
    root.contents.list = function (page, search) {
        PAGE_HANDLER.contentList(page, search);
    };

    // content detail page
    root.contents.details = function (id) {
        PAGE_HANDLER.contentDetails(id);
    };

    // content download
    root.contents.requests = function () {
        PAGE_HANDLER.contentRequests();
    };

    // content calendar
    root.contents.calendar = function (type, month, year, id) {
        PAGE_HANDLER.contentCalendar(type, month, year, id);
    };

    /**
    * Public interface
    */
    return root;

} ());