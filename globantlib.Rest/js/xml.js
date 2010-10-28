var XML = (function () {

    var currentCall = null;

    /**
    * Returns XMLHttpRequest depending on the user agent
    */
    function createXMLHttpRequest() {
        var obj;
        if (window.XMLHttpRequest) {
            obj = new XMLHttpRequest();
        }
        else {
            obj = new ActiveXObject('Microsoft.XMLHTTP');
        }
        return obj;
    }

    /**
    * Creates an empty XML document
    */
    function createDocument(body) {
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
        currentCall = createXMLHttpRequest();
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

    /**
    * Flattens the object to XML
    */
    function flatten(obj) {
        var str = "";
        for (i in obj) {
            if (typeof obj[i] === 'string') {
                str += "<" + i + ">" + obj[i] + "</" + i + ">";
            }
            else {
                str += flatten(obj[i]);
            }
        }
        return str;
    }

    /**
    * Gets an object and returns an XML document
    */
    function sendAsXML(options) {
        function sendObj(str) {
            var req = createXMLHttpRequest();
            req.open(options.type, options.service, true);
            req.setRequestHeader("Content-Type", "application/xml; charset=UTF-8");
            req.onreadystatechange = options.callback;
            req.send(str);
        }
        if (typeof obj === 'string') {
            sendObj(options.data);
        }
        else if (typeof obj === 'object') {
            sendObj("<" + options.root + ">" + flatten(options.data) + "</" + options.root + ">");
        }
    }

    return {
        "createDocument": createDocument,
        "loadDocument": loadDocument,
        "transformDocument": transformDocument,
        "transformWithCallback": transformWithCallback,
        "sendAsXML": sendAsXML,
        "flatten": flatten
    }

} ());