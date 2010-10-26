var ROUTER = (function () {

	var devices, contents;
	
	// devices list first page
	devices = function () {
		PAGE_HANDLER.deviceList();
	}; 
	

	// devices list page N
	devices.list = function (page) {
		PAGE_HANDLER.deviceList(page);
	};
	
	// device calendar
	devices.calendar = function (id) {
		PAGE_HANDLER.deviceCalendar(id);
	};
	
	// contents list first page
	contents = function () {
		PAGE_HANDLER.contentList();
	}; 
	
	// contents list page N
    contents.list = function (page, search) {
	    PAGE_HANDLER.contentList(page, search);
	};
	
	// content detail page
	contents.details = function (id) {
		PAGE_HANDLER.contentDetails(id);
	};
	
	// content download
	contents.download = function (id) {
		PAGE_HANDLER.contentDownload(id);
	};
	
	// content calendar
	contents.calendar = function (id) {
		PAGE_HANDLER.contentDetails(id);
	};

	/**
	 * Public interface
	 */
	return {
		"main" : contents,
		"devices" : devices,
		"contents" : contents
	};

}());