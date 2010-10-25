var XML = (function () {
	
	/**
	 * Creates an empty XML document
	 */
	function createDocument() {
		var doc;
		if (window.DOMParser) {
			doc = (new DOMParser()).parseFromString('', 'text/xml');
		}
		else {
			doc = new ActiveXObject('Microsoft.XMLDOM');
		}
		return doc;
	}
	
	/**
	 * Loads a document from an external source
	 */
	function loadDocument(url, callback) {
		var doc;
		if (window.XMLHttpRequest) {
			doc = new XMLHttpRequest();
		}
		else {
			doc = new ActiveXObject('Microsoft.XMLHTTP');
		}
		doc.open("GET", url, !!callback);
		if (callback) {
			doc.onreadystatechange = function () { callback(doc); }
		}
		doc.send();
		return callback ? doc : doc.responseXML;
	}
	
	/**
	 * Transform XML to HTML using XSLT
	 */
	function transformDocument(xml, xsl, target) {
		var fragment = document.createDocumentFragment();
		target.innerHTML = "";
		if (window.XSLTProcessor) {
			var xslproc = new XSLTProcessor();
			xslproc.importStylesheet(xsl);
			target.appendChild( xslproc.transformToFragment(xml, document) );
		}
		else {
			var html = xml.transformNode(xsl),
				div = document.createElement('div');
			div.innerHTML = html;
			target.appendChild(div);
		}
		return fragment;
	}
	
	return {
		"createDocument" : createDocument,
		"loadDocument" : loadDocument,
		"transformDocument" : transformDocument
	}
	
}());