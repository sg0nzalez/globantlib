var PAGE_HANDLER = (function () {

	function contentList(page) {
		CONTENTS.showList(page);
	}
	
	function contentDetails(id) {
        CONTENTS.showDetails(id)
	}
	
    function contentDownload(id) {
        CONTENTS.showDownload(id);
    }
    
	function showDeviceList(page) {
        DEVICES.showList(page);
	}
	
	function showDeviceCalendar(id) {
        DEVICES.showCalendar(id);
	}

	return {
		"contentList" : contentList,
		"contentDetails" : contentDetails,
		"contentDownload": contentDownload,
		"deviceList": showDeviceList,
        "deviceCalendar": showDeviceCalendar
	}

}());