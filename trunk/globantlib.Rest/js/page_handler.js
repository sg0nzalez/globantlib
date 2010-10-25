var PAGE_HANDLER = (function () {

	function hideContentList() {}
	function hideContentDetails() {}
	function hideDeviceList() {}

	function showContentList(page) {
		CONTENTS.showList();
	}
	
	function showContentDetails(content) {
		//console.log('content details');
	}
	
	function showDeviceList(page) {
		//console.log('devices list');
	}
	
	function showDeviceCalendar() {
		//console.log('device calendar');
	}

	return {
		"contentList" : showContentList,
		"contentDetails" : showContentDetails,
		"deviceList" : showDeviceList,
		"deviceCalendar" : showDeviceCalendar
	}

}());