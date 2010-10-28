var REVIEWS = (function () {

    var formAniSpeed = 0;

    /**
    * Loading messages
    */
    function showOverlay() {
    }
    function hideOverlay() {
    }

    /**
    * Handle form visibility
    */
    function showForm() {
        $("#w-contents-new-review-show").hide(formAniSpeed);
        $("#w-contents-new-review-form").show(formAniSpeed);
        e.preventDefault();
    }
    function hideForm() {
        $("#w-contents-new-review-show").hide(formAniSpeed);
        $("#w-contents-new-review-form").show(formAniSpeed);
        e.preventDefault();
    }

    /**
    * Show submit message
    */
    function submitError() {
    }
    function submitSuccess() {
    }

    /**
    * Call service and populate review list
    */
    function gatherFormData() {
        data = {};
        return data;
    }
    function loadList(id, callback) {
        var formData = gatherFormData;
        showOverlay();
        $.ajax({
            "url": "",
            "type": "POST",
            "data": formData,
            "success": function () {
                submitSuccess();
            },
            "error": function () {
                submitError();
            },
            "complete": function () {
                hideOverlay();
            }
        });
    }

    /**
    * Validate review fields values
    */
    function validateReview() {
        return false;
    }

    /**
    * Submit review to server
    */
    function submitReview() {
        if (validateReview()) {
            alert('submit');
        }
    }

    /**
    * Initialize widget
    */
    function init(id) {
        $("#w-contents-new-review-show").click(showForm);
        hideForm();
        loadList(id);
    }

    /**
    * Public interface
    */
    return {
        "init": init
    }

} ());