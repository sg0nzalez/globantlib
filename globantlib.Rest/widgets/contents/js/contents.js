var CONTENTS = (function () {
	
	/**
	 * Load and show the list of contents
	 */
	function showList() {
        var target = document.getElementById('w-contents-list');
        target.className = 'loading';
        XML.transformWithCallback('data/contents.xml', 'widgets/contents/xsl/list.xsl', target, function () {
            target.className = '';
        });
	}
    
    /**
     * Load single content and show details
     */
    function showDetails(id) {
        var target = document.getElementById('w-contents-details');
        target.className = 'loading';
        XML.transformWithCallback('data/content1.xml', 'widgets/contents/xsl/details.xsl', target, function () {
            target.className = '';
        });
    }
	
	/**
	 * Public interface
	 */
	return {
		"showList" : showList,
        "showDetails" : showDetails
	};
	
}());