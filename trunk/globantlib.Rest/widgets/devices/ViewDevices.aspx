<!DOCTYPE html>
<html>

	<head>
		<meta charset="UTF-8" />
		<title>Contents List</title>
		<link rel="stylesheet" href="/css/styles.css" />
		<link rel="stylesheet" href="/css/jqueryui/jqueryui.css" />
		<script src="/js/modernizr.js"></script>
	</head>
	
	<body>
		
		<div id="header-wrap" class="full">
			<header id="header" class="center">
				<a href="" id="logo">Globant Library</a> <!-- #logo -->
				<nav id="main-nav">
					<ul>
						<li><a href="#contents">Contents</a></li>
						<li><a href="#devices">Devices</a></li>
					</ul>
				</nav> <!-- #main-nav -->
			</header> <!-- #header -->
		</div> <!-- #header-wrap -->
		
		<div id="content-wrap" class="full">
			<div id="content" class="center">
			<style >
			    
			</style>
				
				<div id="w-devices">
					<div id="w-devices-list">
                        
                    </div> <!-- #w-devices-list -->
					<div id="w-devices-calendar"></div> <!-- #w-devices-calendar .dialog -->
				</div> <!-- #w-devices -->
			
			</div> <!-- #content -->
		</div> <!-- #content-wrap -->
		
		<div id="footer-wrap" class="full">
			<footer id="footer" class="center">
				<p id="copyright">&copy; tremendo!</p> <!-- #copyright -->
			</footer> <!-- #footer -->
		</div> <!-- #footer-wrap -->
	
		<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
		<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/jquery-ui.min.js"></script>
		<script src="/widgets/devices/js/devices.js"></script>
		<script src="/js/xml.js"></script>
		<script src="/js/router.js"></script>
		<script src="/js/page_handler.js"></script>
		<script src="/js/hash_reader.js"></script>
		
		<script>
		    $(function () {
		        HASH_READER.init();
		    });
		</script>
	
	</body>

</html>	