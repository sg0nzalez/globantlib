var CONTENTS_LIST = (function () {

    function hide() {
        $("#w-contents-list-ph").hide();
    }
    function show() {
        $("#w-contents-list-ph").show();
    }

    function init(page, query, callback) {
        var query = query ? escape(query) : "",
            xml = './LibraryService.mvc/Search?Text=' + query,
            target = document.getElementById('w-contents-list-ph');
        if (page) {
            xml += '&Page=' + page;
        }
        XML.transformWithCallback(xml, 'widgets/contents/list/list.xsl', target, function () {
            callback();
            show();
            document.title = "Contents List" + (query ? " (" + query + ")" : "") + " @ Globant Library";
        });
    }

    return {
        "init": init,
        "show": show,
        "hide": hide
    };

} ());