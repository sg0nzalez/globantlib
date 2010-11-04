﻿var ROUTER = (function () {

    var devices, contents;

    // devices list first page
    devices = function () {
        PAGE_HANDLER.deviceList();
    };

    // devices list page N
    devices.list = function (page) {
        PAGE_HANDLER.deviceList(page);
    };

    // content calendar
    devices.calendar = function (type, month, year, id) {
        PAGE_HANDLER.deviceCalendar(type, month, year, id);
    };

    // contents list first page
    contents = function () {
        PAGE_HANDLER.contentList();
    };

    // contents list page N
    contents.list = function (page, search) {
        PAGE_HANDLER.contentList(page, search);
    };

    // content detail page
    contents.details = function (id) {
        PAGE_HANDLER.contentDetails(id);
    };

    // content download
    contents.requests = function () {
        PAGE_HANDLER.contentRequests();
    };

    // content calendar
    contents.calendar = function (type, month, year, id) {
        PAGE_HANDLER.contentCalendar(type, month, year, id);
    };

    /**
    * Public interface
    */
    return {
        "main": contents,
        "devices": devices,
        "contents": contents
    };

} ());