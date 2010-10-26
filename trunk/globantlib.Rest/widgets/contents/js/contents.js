var CONTENTS = (function () {

    // currently waiting search
    var currentSearch = null;

    /**
    * Load and show the list of contents
    */
    function showList(page, search) {
        var search = search ? escape(search) : "",
            xml = '/LibraryService/Search?Text=' + search,
            target = document.getElementById('w-contents-left');
        if (page) {
            xml += '&Page=' + page;
        }
        showOverlay();
        XML.transformWithCallback(xml, 'widgets/contents/xsl/list.xsl', target, function () {
            initSearch(search);
            hideOverlay();
        });
    }

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
    * Load single content and show details
    */
    function showDetails(id) {
        var xml = '/LibraryService/' + id,
            target = document.getElementById('w-contents-left');
        target.className = 'loading';
        XML.transformWithCallback(xml, 'widgets/contents/xsl/details.xsl', target);
    }

    /**
    * Show download popup
    */
    function showDownload(id) {
        var xml = 'data/content1.xml?id=' + id,
            target = document.getElementById('w-contents-download');
        target.className = '';
        $(target).dialog({
            modal: true,
            resizable: false,
            title: "Download Content"
        });
        XML.transformWithCallback(xml, 'widgets/contents/xsl/download.xsl', target, function () {
            target.className = '';
        });
    }

    /**
    * Initialize list
    */
    function initSearch(searchStr) {
        var href,
            search = function () { document.location.hash = 'contents/list/1/' + escape($("#w-contents-search input[type=text]").val()); };
        $("#w-contents-search input[type=text]")
            .keyup(function () {
                clearTimeout(currentSearch);
                currentSearch = setTimeout(search, 500);
            })
            .val(unescape(searchStr))
            .focus();
        $("#w-contents-pagination a").each(function () {
            href = $(this).attr('href');
            $(this).attr('href', href + "/" + searchStr);
        });
        $("#w-contents-search form").submit(function (e) {
            search();
            e.preventDefault();
        });
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