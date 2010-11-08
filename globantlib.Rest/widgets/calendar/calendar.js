var CALENDAR = (function () {

    /**
    * Private
    */
    var routes = null,
        months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
        currentType,
        currentId,
        currentDate;

    /**
    * Mark dates and do reservation
    */
    function calendarStartOver() {
        var dates = $('#w-calendar-entries .date');
        dates.removeClass('hovered candidate');
        dates
            .filter(".free")
            .unbind('click')
            .unbind('mouseover')
            .bind('click', function (e) {
                markStartDate($(this));
                e.preventDefault();
            });
        $('#w-calendar-helper').slideUp();
    }
    function markStartDate(startDiv) {
        var dates = $('#w-calendar-entries .date'),
            index = dates.index(startDiv),
            select = true;
        dates
            .unbind('click')
            .filter(':gt(' + index + '), :eq(' + index + ')')
            .addClass('candidate')
            .each(function (i) {
                if ($(this).hasClass('used')) {
                    select = false;
                }
                if (select) {
                    $(this).bind('click', function () {
                        markEndDate(startDiv, $(this));
                    });
                    $(this).bind('mouseover', function () {
                        hoverEndDates(startDiv, $(this));
                    });
                }
            });
        startDiv.addClass('hovered');
        $('#w-calendar-helper').slideDown();
    }
    function hoverEndDates(startDiv, endDiv) {
        var dates = $('#w-calendar-entries .date'),
            bot = dates.index(startDiv),
            top = dates.index(endDiv);
        dates
            .removeClass('hovered')
            .slice(bot, top)
            .addClass('hovered');
        endDiv.addClass('hovered');
    }
    function markEndDate(startDiv, endDiv) {
        $('#w-calendar-email').val('');
        $('#w-calendar-form')
            .css('opacity', 1)
            .submit(function (e) {
                var email = $(this).find('input[type=text]').val();
                if (email && email.indexOf('@globant.com') != -1) {
                    sendReservation(startDiv, endDiv);
                    e.preventDefault();
                }
            })
            .dialog({
                modal: true,
                resizable: false,
                draggable: false,
                title: 'Enter your Globant email address',
                buttons: {
                    'Send': function () {
                        $(this).submit();
                    },
                    'Cancel': function () {
                        $(this).dialog('destroy');
                        calendarStartOver();
                    }
                }
            });
    }
    function sendReservation(startDiv, endDiv) {
        var dates = $('#w-calendar-entries .date'),
            data = {}
        data.Year = currentDate.getFullYear();
        data.Month = currentDate.getMonth() + 1;
        data.StartDate = startDiv.find('span.number').html();
        data.Length = dates.index(endDiv) - dates.index(startDiv);
        data.Email = $('#w-calendar-email').val();
        data.ID = currentId;
        $('#w-calendar-form').css('opacity', 0.2);
        XML.sendAsXML({
            "data": data,
            "type": "POST",
            "root": "Lease",
            "service": routes.submit,
            "callback": function (xhr) {
                if (xhr.readyState === 4) {
                    $('#w-calendar-form').dialog('destroy');
                    loadLeases(function () {
                        calendarStartOver();
                    });
                }
            }
        });
    }

    /**
    * Load current reservations
    */
    function loadLeases(callback) {
        var service = routes.get + '?type=' + currentType + '&month=' + currentDate.getMonth() + '&year=' + currentDate.getFullYear() + '&id=' + currentId,
        target = document.getElementById('w-calendar');
        XML.transformWithCallback(service, 'widgets/calendar/calendar.xsl', target, function (xml) {
            initControls();
            callback();
            document.title = "Calendar @ Globant Library";
        });
    }

    /**
    * Format a calendar URL given a date and an id
    */
    function calendarURL(params) {
        var id = params.id || currentId,
            month = params.date ? params.date.getMonth() : currentDate.getMonth(),
            year = params.date ? params.date.getFullYear() : currentDate.getFullYear();
        return routes.prefix + '/' + (month + 1) + '/' + year + '/' + id;
    }

    /**
    * Adds listeners to the links
    */
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
        $("#w-calendar-month-picker span.month-name")
            .html(months[currentDate.getMonth()] + ', ' + currentDate.getFullYear());
        $('#w-calendar-helper a.calendar-reset').click(function (e) {
            calendarStartOver();
            e.preventDefault();
        });
        calendarStartOver();
    }

    /**
    * Initialize
    */
    function init(params, callback) {
        routes = params.routes;
        currentType = params.type;
        currentDate = new Date();
        currentId = params.id || '';
        if (params.year) {
            currentDate.setFullYear(params.year);
        }
        if (params.month) {
            currentDate.setMonth(params.month - 1);
        }
        loadLeases(function () {
            currentId = params.id || $('#w-calendar-items a.current').attr('deviceid');
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