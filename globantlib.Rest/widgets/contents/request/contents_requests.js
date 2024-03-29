var CONTENTS_REQUESTS = (function () {

    var initiated = false;

    function show(callback) {
        $('#w-contents-request-list-ph').show();
    }
    function hide() {
        $('#w-contents-request-list-ph').hide();
    }

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
        var text = $("#w-contents-request input.text");
        var valid = validate();
        if (valid) {
            $("<div class='request-popup'/>")
                .html("You're about to send this text: \"<b>" + text.val() + "</b>\"")
                .dialog({
                    resizable: false,
                    modal: true,
                    draggable: false,
                    title: "Request Content",
                    buttons: {
                        "Go ahead": function () {
                            var service = './LibraryService.mvc/BookRequest?Text=' + text.val(),
                            req = XML.createXMLHttpRequest();
                            req.open("GET", service, true);
                            req.onreadystatechange = function () {
                                if (this.readyState === 4) {
                                    text.val("");
                                    hideLoading();
                                    document.location.href = "#contents/requests";
                                }
                            }
                            showLoading();
                            req.send();
                            $(this).dialog("close");
                        },
                        "Nope": function () {
                            $(this).dialog("close");
                        }
                    }
                });
        }
    }

    function loadList(callback) {
        var service = './LibraryService.mvc/BookRequests',
            target = document.getElementById('w-contents-request-list-ph');
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