var XML = (function () {

    var currentCall = null;

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
        if (currentCall) {
            currentCall.abort();
        }
        if (window.XMLHttpRequest) {
            currentCall = new XMLHttpRequest();
        }
        else {
            currentCall = new ActiveXObject('Microsoft.XMLHTTP');
        }
        currentCall.open("GET", url, !!callback);
        currentCall.setRequestHeader("Accept", "application/xml");
        if (callback) {
            currentCall.onreadystatechange = function () { callback(currentCall); }
        }
        currentCall.send();
        return callback ? currentCall : currentCall.responseXML;
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
            target.appendChild(xslproc.transformToFragment(xml, document));
        }
        else {
            var html = xml.transformNode(xsl),
				div = document.createElement('div');
            div.innerHTML = html;
            target.appendChild(div);
        }
        return fragment;
    }

    /**
    * Transform, append and run callback
    */
    function transformWithCallback(xml, xsl, target, callback) {
        function loadData() {
            xml = XML.loadDocument(xml, loadStyles);
        }
        function loadStyles(xhr) {
            if (xhr.readyState === 4) {
                xml = xml.responseXML;
                xsl = XML.loadDocument(xsl, appendTransformed);
            }
        }
        function appendTransformed(xhr) {
            if (xhr.readyState === 4) {
                xsl = xsl.responseXML;
                XML.transformDocument(xml, xsl, target);
                target.className = '';
                if (callback) {
                    callback(xml, xsl, target);
                }
            }
        }
        loadData();
    }

    return {
        "createDocument": createDocument,
        "loadDocument": loadDocument,
        "transformDocument": transformDocument,
        "transformWithCallback": transformWithCallback
    }

} ());