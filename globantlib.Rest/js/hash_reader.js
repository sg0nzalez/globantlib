/**
 * Hash reader observes hash changes
 * and calls routes based on its value
 */
var HASH_READER = (function () {

    var oldHash = '',
        router = ROUTER; // object containing routes

    /**
    * Evaluate hash value
    */
    function checkHash() {
        var hash = document.location.hash;
        if (!hash) { // if no hash
            router.main(); // call main page
        }
        else if (hash !== oldHash) { // if there's hash
            oldHash = hash;
            hash = hash.substring(1); // remove # char
            calls = hash.split('/'); // get call stack
            var i, max = calls.length, // for variables
				obj = router, // iteration root
				params = []; // params list
            for (i = 0; i < max; i += 1) {
                if (calls[i] in obj) {
                    obj = obj[calls[i]];
                }
                else {
                    params.push(calls[i]);
                }
            }
            obj.apply(window, params);
        }
    }

    /**
    * Initialization
    */
    function init() {
        if (onhashchange in window) {
            window.onhashchange = checkHash;
            checkHash();
        }
        else {
            setInterval(checkHash, 1000);
        }
    }

    /**
    * Public interface
    */
    return {
        "init": init
    };

} ());