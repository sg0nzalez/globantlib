var CONTENTS_DETAILS = (function () {

    function hide() {
        $("#w-contents-details").hide();
    }
    function show() {
        $("#w-contents-details").show();
    }

    function init(id, callback) {
        var xml = '/LibraryService.mvc/' + id,
            target = document.getElementById('w-contents-details');
        target.className = 'loading';
        XML.transformWithCallback(xml, 'widgets/contents/details/details.xsl', target, function (xml) {
            callback();
            show();
            CONTENTS_REVIEWS.init(id);
            document.title = $(xml).find("Title").text() + " @ Globant Library";
        });
    }

    return {
        "init": init,
        "show": show,
        "hide": hide
    };

} ());