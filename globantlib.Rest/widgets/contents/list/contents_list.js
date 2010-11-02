var CONTENTS_LIST = (function () {

    function hide() {
        $("#w-contents-list").hide();
    }
    function show() {
        $("#w-contents-list").show();
    }

    function init(page, query, callback) {
        var query = query ? escape(query) : "",
            xml = '/LibraryService.mvc/Search?Text=' + query,
            target = document.getElementById('w-contents-list');
        if (page) {
            xml += '&Page=' + page;
        }
        XML.transformWithCallback(xml, 'widgets/contents/list/list.xsl', target, function () {
            callback();
            show();
        });
    }

    return {
        "init": init,
        "show": show,
        "hide": hide
    };

} ());