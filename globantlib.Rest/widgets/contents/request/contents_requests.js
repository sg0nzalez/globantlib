var CONTENTS_REQUESTS = (function () {

    var initiated = false;

    function showLoading() {
        $("#loading-message")
            .attr('class', 'loading')
            .text('Sending request...');
    }
    function hideLoading() {
        $("#loading-message")
            .attr('class', 'ready')
            .text('Ready');
    }

    function validate() {
        var text = $("#w-contents-request input.text")
        text.css('border-color', (text.val() ? '' : 'red'));
        return !!text.val();
    }

    function sendRequest(e) {
        var text = $("#w-contents-request input.text"),
            valid = validate();
        if (valid) {
            var service = '/LibraryService.mvc/BookRequest?Text=' + text.val(),
            req = XML.createXMLHttpRequest();
            req.open("GET", service, true);
            req.onreadystatechange = function () {
                if (this.readyState === 4) {
                    text.val("");
                    hideLoading();
                    document.location = "#contents/requests";
                    PAGE_HANDLER.contentRequests();
                }
            }
            showLoading();
            req.send();
        }
    }

    function show(callback) {
        $('#w-contents-request-list').show();
    }
    function hide() {
        $('#w-contents-request-list').hide();
    }

    function loadList(callback) {
        var service = '/LibraryService.mvc/BookRequests',
            target = document.getElementById('w-contents-request-list');
        XML.transformWithCallback(service, 'widgets/contents/request/list.xsl', target, callback);
    }

    function init(callback) {
        loadList(function () {
            callback();
            initSidebar();
            show();
            document.title = "Requested Titles @ Globant Library";
        });
    }

    function initSidebar() {
        if (!initiated) {
            $("#w-contents-request form").submit(function (e) {
                sendRequest();
                e.preventDefault();
            });
            $("#w-contents-request input.text").keyup(validate);
            $("#w-contents-request input.text").blur(function () {
                $(this).removeAttr('style');
            });
            initiated = true;
        }
    }

    return {
        "init": init,
        "initSidebar": initSidebar,
        "show": show,
        "hide": hide
    };

} ());