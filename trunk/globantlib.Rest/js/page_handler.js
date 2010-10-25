var PAGE_HANDLER = (function () {

	function showContentList(page) {
        $('#w-contents-details').hide();
        $('#w-contents-list').show();
		CONTENTS.showList(page);
	}
	
	function showContentDetails(id) {
        $('#w-contents-list').hide();
        $('#w-contents-details').show();
        CONTENTS.showDetails(id)
	}
	
	function showDeviceList(page) {
        DEVICES.showList(page);
	}
	
	function showDeviceCalendar(id) {
        DEVICES.showCalendar(id);
	}

	return {
		"contentList" : showContentList,
		"contentDetails" : showContentDetails,
		"deviceList" : showDeviceList,
		"deviceCalendar" : showDeviceCalendar
	}

}());