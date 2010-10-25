var PAGE_HANDLER = (function () {

	function contentList(page) {
        $('#w-contents-details, #w-contents-download, #w-contents-calendar').hide();
        $('#w-contents-list').show();
		CONTENTS.showList(page);
	}
	
	function contentDetails(id) {
        $('#w-contents-search, #w-contents-list, #w-contents-download, #w-contents-calendar').hide();
        $('#w-contents-details').show();
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
        "contentDownload" : contentDownload
	}

}());