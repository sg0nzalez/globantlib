var CONTENTS = (function () {

    /**
    * Load and show the list of contents
    */
    function showList(page) {
        showOverlay();
        var xml = '/LibraryService/Search?Text=',
            target = document.getElementById('w-contents-list');
        if (page) {
            xml += '&Page=' + page;
        }
        target.className = 'loading';
        //$.get(xml, function (data) {
        XML.transformWithCallback(xml, 'widgets/contents/xsl/list.xsl', target, function () {
            target.className = '';
            hideOverlay();
        });
        //});
    }

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


    function hideOverlay() {
        $("#overlay_back").remove();

    }


    /**
    * Load single content and show details
    */
    function showDetails(id) {
        var xml = '/LibraryService/' + id,
            target = document.getElementById('w-contents-details');
        target.className = 'loading';
        XML.transformWithCallback(xml, 'widgets/contents/xsl/details.xsl', target, function () {
            target.className = '';
        });
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
    * Public interface
    */
    return {
        "showList": showList,
        "showDetails": showDetails,
        "showDownload": showDownload
    };

} ());