var CONTENTS = (function () {

    // currently waiting search
    var lastQuery = '',
        currentSearch = null;

    /**
    * Shows overlay before it starts loading
    */
    function showOverlay() {
        var width, height, x, y, search_height, overlay_back, overlay_swirling;
        width = $('#w-contents-left').width();
        height = $('#w-contents-left').height();
        x = $('#w-contents-left').position().left;
        y = $('#w-contents-left').position().top;
        search_height = $("#w-contents-search").height();
        overlay_back = $("<div>").attr("id", "overlay_back").css({ "width": width, "height": height - search_height, "top": y + search_height, "left": x });
        overlay_swirling = $("<img>").attr({ "src": "/img/loading.gif", "alt": "loading" }).css({ "width": "100px", "height": "100px", "margin-left": (width - 100) / 2, "margin-top": (height - 100) / 2 });
        overlay_back.append(overlay_swirling).prependTo($('#w-contents-left'));
    }

    /**
    * Hides overlay after loading
    */
    function hideOverlay() {
        $("#overlay_back").remove();
    }

    /**
    * Load and show the list of contents
    */
    function showList(page, query) {
        var query = query ? escape(query) : "",
            xml = '/LibraryService.mvc/Search?Text=' + query,
            target = document.getElementById('w-contents-left');
        if (page) {
            xml += '&Page=' + page;
        }
        showOverlay();
        XML.transformWithCallback(xml, 'widgets/contents/xsl/list.xsl', target, function () {
            initSearch(query);
            //hideOverlay();
        });
    }

    /**
    * Load single content and show details
    */
    function showDetails(id) {
        var xml = '/LibraryService.mvc/' + id,
            target = document.getElementById('w-contents-left');
        target.className = 'loading';
        XML.transformWithCallback(xml, 'widgets/contents/xsl/details.xsl', target, function () {
            initReviews(id);
        });
    }

    /**
    * Show download popup
    */
    function showDownload(id) {
        var xml = 'data/content1.xml?id=' + id;
        $('#w-contents-download').dialog({
            modal: true,
            resizable: false,
            title: "Download Content"
        });
        XML.transformWithCallback(xml, 'widgets/contents/xsl/download.xsl', target);
    }

    /**
    * Initialize list
    */
    function initSearch(searchStr) {
        var href,
            search = function (e) {
                var currentValue = $("#w-contents-search input[type=text]").val();
                if (lastQuery !== currentValue) {
                    lastQuery = currentValue;
                    clearTimeout(currentSearch);
                    currentSearch = setTimeout(function () {
                        document.location.hash = 'contents/list/1/' + escape(lastQuery);
                    }, 500);
                }
                e.preventDefault();
            };
        $("#w-contents-search form")
            .submit(search);
        $("#w-contents-search input[type=text]")
            .keyup(search)
            .val(unescape(searchStr))
            .focus();
        $("#w-contents-pagination a")
            .each(function () {
                href = $(this).attr('href');
                $(this).attr('href', href + "/" + searchStr);
            });
    }

    /**
    * Initialize reviews 
    */
    function initReviews(id) {
        REVIEWS.init(id);
    }

    /**
    * Public interface
    */
    return {
        "showList": showList,
        "showDetails": showDetails,
        "showDownload": showDownload
    };

} ());