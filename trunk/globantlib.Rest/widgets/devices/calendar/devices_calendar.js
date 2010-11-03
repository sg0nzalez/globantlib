var DEVICES_CALENDAR = (function () {

    /**
    * Manage display state
    */
    function show() {
        $("#w-devices-calendar").show();
    }
    function hide() {
        $("#w-devices-calendar").hide();
    }

    /**
    * Calendar marking
    */
    function markStartDate() {
    }
    function markEndDate() {
    }

    /**
    *
    */
    function calendarTable() {
        var table = document.createElement('table'),
            tr = table.insertRow(0),
            td,
            rows = 0,
            cells = 0,
            date = 0,
            day = 0;
        while (date < 35) {
            if (day === 0) {
                tr = table.insertRow(rows);
                cells = 0;
                rows++;
            }
            td = tr.insertCell(cells);
            cells++;
            date++;
            day = (day + 1) % 7;
        }
        table.width = '100%';
        return table;
    }
    function drawCalendar() {
        var table = $(calendarTable()),
            firstDate = new Date(),
            date = 1,
            month = firstDate.getMonth();
        firstDate.setDate(date);
        table.find('td').each(function (i) {
            if ((i % 7) === firstDate.getDay() && month === firstDate.getMonth()) {
                date++;
                firstDate.setDate(date);
                $(this).html(i);
            }
        });
        $("#w-devices-calendar").html("").append(table);
    }
    function appendLeases(xml) {
        var xml = $(xml);
        drawCalendar();
    }

    /**
    * Initialize
    */
    function init(id, callback) {
        var service = './DeviceService.mvc/Calendar?Id=',
            target = document.getElementById('w-devices-calendar');
        XML.transformWithCallback(service, 'widgets/devices/calendar/calendar.xsl', target, function (xml) {
            callback();
            appendLeases(xml);
        });
    }

    return {
        "init": init,
        "show": show,
        "hide": hide
    };

} ());