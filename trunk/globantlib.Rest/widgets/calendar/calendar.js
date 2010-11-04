var CALENDAR = (function () {

    var routes = null,
        months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
        currentType,
        currentId,
        currentDate;

    function loadLeases(callback) {
        var service = routes.get + '?type=' + currentType + '&month=' + currentDate.getMonth() + '&year=' + currentDate.getFullYear() + '&id=' + currentId,
        target = document.getElementById('w-calendar');
        XML.transformWithCallback(service, 'widgets/calendar/calendar.xsl', target, function (xml) {
            callback();
            document.title = "Calendar @ Globant Library";
        });
    }

    function calendarURL(params) {
        var id = params.id || currentId,
            month = params.date ? params.date.getMonth() : currentDate.getMonth(),
            year = params.date ? params.date.getFullYear() : currentDate.getFullYear();
        return routes.prefix + '/' + month + '/' + year + '/' + id;
    }
    function initControls() {
        var href = '';

        $("#w-calendar-items a").each(function () {
            href = $(this).attr('href');
            $(this).attr('href', calendarURL({ id: href.substring(1) }));
        });
        $("#w-calendar-month-picker a.prev").each(function (e) {
            var prevMonth = new Date();
            prevMonth.setFullYear(currentDate.getFullYear());
            prevMonth.setMonth(currentDate.getMonth() - 1);
            href = $(this).attr('href');
            $(this).attr('href', calendarURL({ date: prevMonth }));
        });
        $("#w-calendar-month-picker a.next").each(function (e) {
            var nextMonth = new Date();
            nextMonth.setFullYear(currentDate.getFullYear());
            nextMonth.setMonth(currentDate.getMonth() + 1);
            href = $(this).attr('href');
            $(this).attr('href', calendarURL({ date: nextMonth }));
        });
        $("#w-calendar-month-picker span.month-name").html(months[currentDate.getMonth()] + ', ' + currentDate.getFullYear());
    }
    function init(params, callback) {
        routes = params.routes;
        currentType = params.type;
        currentId = params.id || "";
        currentDate = new Date();
        if (params.month) {
            currentDate.setMonth(params.month);
        }
        if (params.year) {
            currentDate.setFullYear(params.year);
        }
        loadLeases(function () {
            initControls();
            callback();
        });
    }

    /**
    * Public interface
    */
    return {
        "init": init
    };

} ());