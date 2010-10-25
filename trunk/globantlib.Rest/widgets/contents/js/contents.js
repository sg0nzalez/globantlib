var CONTENTS = (function () {
	
	/**
	 * Load and show the list of contents
	 */
	function showList() {
		var xml, xsl,
			target = document.getElementById('w-contents-list');
		target.className = 'loading';
		function loadData() {
			xml = XML.loadDocument('data/contents.xml', loadStyles);
		}
		function loadStyles(xhr) {
			if (xhr.readyState === 4) {
				xml = xml.responseXML;
				xsl = XML.loadDocument('widgets/contents/xsl/list.xsl', appendTransformed);
			}
		}
		function appendTransformed(xhr) {
			if (xhr.readyState === 4) {
				xsl = xsl.responseXML;
				XML.transformDocument(xml, xsl, target);
				target.className = '';
			}
		}
		loadData();
	}
	
	/**
	 * Public interface
	 */
	return {
		"showList" : showList
	};
	
}());