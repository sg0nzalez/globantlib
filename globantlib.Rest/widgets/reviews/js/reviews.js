var REVIEWS = (function () {

    /**
    * Initialize widget
    */
    function init() {
        var speed = 0,
            form = $("#w-contents-new-review-form").hide();
        $("#w-contents-new-review-show").click(function (e) {
            $(this).hide(speed, function () {
                $(this).remove();
            })
            form.show(speed);
            e.preventDefault();
        });
    }

    /**
    * Public interface
    */
    return {
        "init": init
    }

} ());