var CONTENTS_REVIEWS = (function () {

    var contentId = null, // The content the reviews are related to.
        formAniSpeed = 0, // The speed of the animation when showing the form
    // Validation rules
    // Keys are tested against values and error messages are returned
        validation = {
            "w-contents-review-title": {
                ".+": "Title can't be empty"
            },
            "w-contents-review-text": {
                ".+": "Comment can't be empty"
            }
        };

    /**
    * Loading messages
    */
    function showOverlay() {
        $("#loading-message")
            .text('Submitting Review...');
    }
    function hideOverlay() {
        $("#loading-message")
            .text('Ready');
    }

    /**
    * Handle form visibility
    */
    function showForm() {
        $("#w-contents-new-review-show").hide(formAniSpeed);
        $("#w-contents-new-review-form > div").show(formAniSpeed);
    }
    function hideForm() {
        $("#w-contents-new-review-show").show(formAniSpeed);
        $("#w-contents-new-review-form > div").hide(formAniSpeed);
    }

    /**
    * Show submit message
    */
    function submitError() {
        alert('ERRORRR!!');
    }
    function submitSuccess() {
        loadList();
    }
    function submitValidationErrors(errors) {
        var span;
        $("#w-contents-new-review-form span.error").remove();
        for (elem in errors) {
            span = $('<span class="error"/>').text(errors[elem]);
            $('#' + elem).parent().append(span);
        }
    }

    /**
    * Call service and populate review list
    */
    function loadList(callback) {
        var service = '/LibraryService.mvc/Review?ContentId=' + contentId,
            target = document.getElementById('w-contents-reviews');
        XML.transformWithCallback(service, 'widgets/contents/reviews/list.xsl', target, function () {
            hideForm();
            initControls();
        });
    }

    /**
    * Validate review fields values
    */
    function validateReview(callback) {
        var rule, elem,
            errorCount = 0, errors = {};
        for (iElem in validation) {
            elem = $('#' + iElem);
            for (iRule in validation[iElem]) {
                rule = new RegExp(iRule);
                if (!rule.test(elem.val())) {
                    errorCount++;
                    errors[iElem] = validation[iElem][iRule];
                }
            }
        }
        return {
            "errorCount": errorCount,
            "errors": errors
        };
    }

    /**
    * Submit review to server
    */
    function gatherFormData() {
        var rightNow = new Date(),
            day = rightNow.getDate(),
            month = rightNow.getMonth(),
            year = (rightNow.getFullYear() + "").substring(2),
            hours = rightNow.getHours(),
            minutes = rightNow.getMinutes() < 10 ? "0" + rightNow.getMinutes() : rightNow.getMinutes();
        data = {};
        data.Comment = $("#w-contents-review-text").val();
        data.ID = "0";
        data.Rate = $("#w-contents-review-rate").val();
        data.Submitted = (day + '/' + month + '/' + year + ' ' + hours + ':' + minutes);
        data.Title = $("#w-contents-review-title").val();
        data.User = "<Name>Milka</Name><Thumbnail>no-img.png</Thumbnail>";
        return data;
    }
    function submitReview() {
        var validation = validateReview();
        submitValidationErrors(validation.errors); // Show submit errors
        if (validation.errorCount === 0) {
            hideForm();
            showOverlay();
            XML.sendAsXML({
                "data": gatherFormData(),
                "type": "POST",
                "root": "Review",
                "service": '/LibraryService.mvc/SubmitReview?ContentId=' + contentId,
                "callback": function (xhr) {
                    if (xhr.readyState === 4) {
                        if (xhr.status === 200) {
                            submitSuccess();
                        }
                        else {
                            submitError();
                        }
                        hideOverlay();
                    }
                }
            });
        }
    }

    /**
    * Initialize widget
    */
    function initControls() {
        $("#w-contents-new-review-form").submit(function (e) {
            submitReview();
            e.preventDefault();
        });
        $("#w-contents-new-review-show").click(function (e) {
            showForm();
            e.preventDefault();
        });
        $("#w-contents-new-review-hide").click(function (e) {
            hideForm();
            e.preventDefault();
        });
    }
    function init(id) {
        contentId = id;
        loadList();
    }

    /**
    * Public interface
    */
    return {
        "init": init
    }

} ());