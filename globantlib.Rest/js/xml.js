﻿var XML = (function () {

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
        doc.setRequestHeader("X-Requested-With", "XMLHttpRequest");
        doc.setRequestHeader("Accept", "application/xml");
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