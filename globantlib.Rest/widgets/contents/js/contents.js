var CONTENTS = (function () {
	
	/**
	 * Load and show the list of contents
	 */
	function showList(page) {
        var xml = 'data/contents.xml',
            target = document.getElementById('w-contents-list');
        if (page) {
            xml += '?page=' + page;
        }
        target.className = 'loading';
        XML.transformWithCallback(xml, 'widgets/contents/xsl/list.xsl', target, function () {
            target.className = '';
        });
	}
    
    /**
     * Load single content and show details
     */
    function showDetails(id) {
        var xml = 'data/content1.xml?id=' + id,
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
            modal : true,
            resizable : false,
            title : "Download Content"
        });
        XML.transformWithCallback(xml, 'widgets/contents/xsl/download.xsl', target, function () {
            target.className = '';
        });
    }
	
	/**
	 * Public interface
	 */
	return {
		"showList" : showList,
        "showDetails" : showDetails,
        "showDownload" : showDownload
	};
	
}());