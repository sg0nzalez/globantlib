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
        XML.transformWithCallback(xml, 'widgets/contents/details/details.xsl', target, function () {
            CONTENTS_REVIEWS.init(id);
            callback();
        });
    }

    return {
        "init" : init,
        "show": show,
        "hide": hide
    };

} ());