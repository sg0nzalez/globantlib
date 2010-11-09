/**
 * Hash reader observes hash changes
 * and calls routes based on its value
 */
var HASH_READER = (function () {

    var oldHash = 'DUMMYHASHTOBESETASCHANGEDTHEFIRSTTIMETHEPAGELOADS',
        router = ROUTER; // object containing routes

    /**
    * Evaluate hash value
    */
    function checkHash() {
        var hash = document.location.hash;
        if (hash !== oldHash) { // if there's hash
            oldHash = hash;
            hash = hash.substring(1); // remove # char
            calls = hash.split('/'); // get call stack
            var i, max = calls.length, // for variables
				func = router, // iteration root
				params = []; // params list
            for (i = 0; i < max; i += 1) {
                if (calls[i] in func) {
                    func = func[calls[i]];
                }
                else {
                    params.push(calls[i]);
                }
            }
            func.apply(window, params);
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
            setInterval(checkHash, 100);
        }
    }

    /**
    * Public interface
    */
    return {
        "init": init
    };

} ());