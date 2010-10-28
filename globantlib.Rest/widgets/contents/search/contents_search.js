var CONTENTS_SEARCH = (function () {

    var keyUpDelay = 500,
        runningSearch = null,
        currentQuery = "";

    function show() {
        $("#w-contents-search").show();
    }
    function hide() {
        $("#w-contents-search").hide();
    }

    function search() {
        currentQuery = $("#w-contents-search input[type='text']").val();
        document.location.hash = 'contents/list/1/' + escape(currentQuery);
    }

    function initForm() {
        $("#w-contents-search form").submit(function (e) {
            search();
            e.preventDefault();
        });
    }
    function initTextBox() {
        $("#w-contents-search input[type=text]")
            .keyup(function (e) {
                var newQuery = $("#w-contents-search input[type='text']").val();
                if (currentQuery !== newQuery) {
                    clearTimeout(runningSearch);
                    runningSearch = setTimeout(function () {
                        search();
                    }, keyUpDelay);
                }
                e.preventDefault();
            })
            .val(unescape(currentQuery))
            .focus();
    }
    function initPagination() {
        $("#w-contents-pagination a").each(function () {
            href = $(this).attr('href');
            $(this).attr('href', href + "/" + currentQuery);
        });
    }

    function initControls() {
        initForm();
        initTextBox();
        initPagination(currentQuery);
    }

    function init(query) {
        currentQuery = query ? query : "";
        initControls();
        show();
    }

    return {
        "init": init,
        "show": show,
        "hide": hide
    };

} ());