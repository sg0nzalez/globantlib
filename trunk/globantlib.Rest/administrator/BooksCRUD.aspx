<%@ Page Title="" Language="C#" MasterPageFile="~/administrator/adminTemplate.Master"
    AutoEventWireup="true" CodeBehind="BooksCRUD.aspx.cs" Inherits="globantlib.Rest.administrator.BooksCRUD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset class="fs_left">
        <legend>Book Information</legend>
        <ul class="ul_form">
            <li>
                <label>
                    Title:</label>
                <input type="text" form="form1" id="title" name="title" required="true" />
            </li>
            <li>
                <label>
                    Author:</label>
                <input type="text" form="form1" id="author" name="author" required="true" />
            </li>
            <li>
                <label>
                    Release Date:</label>
                <input type="date" form="form1" id="release" name="release" required="true" />
            </li>
            <li>
                <label>
                    Pages Number:
                </label>
                <input type="number" form="form1" range="1" min="0" id="pages" name="pages" required="true" />
            </li>
            <li>
                <label>
                    Publisher:
                </label>
                <input type="text" form="form1" id="publisher" name="publisher" required="true" />
            </li>
            <li>
                <label>
                    ISBN - 10:
                </label>
                <input type="number" form="form1" id="isbn" name="isbn" required="true" range="1" max="9999999999" />
            </li>
            <li>
                <label>
                    Description:
                </label>
                <textarea form="form1" id="description" name="description" rows="4" cols="15"></textarea>
            </li>
            <li>
                <label>
                    Digital Content:
                </label>
                <input form="form1" id="file_0" name="file[]" type="file" />
                <input form="form1" id="file_1" name="file[]" type="file" class="hidden" />
                <input form="form1" id="file_2" name="file[]" type="file" class="hidden" />
                <input form="form1" id="file_3" name="file[]" type="file" class="hidden" />
                <input form="form1" id="file_4" name="file[]" type="file" class="hidden" />
                <div class="chk_files" id="chk_files">
                </div>
            </li>
            <li></li>
            <li class="error_li">
                <ul class="error_ul" id="error_ul">
                </ul>
            </li>
            <li class="li_buttons">
                <input form="form1" type="submit" id="add_edit_book" value="Add/Edit" runat="server" onclick="return add_edit_book_onclick()" />
                <button id="delete_book">
                    Delete</button>
            </li>
        </ul>
        <input type="hidden" value="add" id="action" name="action" />
    </fieldset>
    <fieldset class="fs_right">
        <legend>Stored Books</legend>
        <input type="search" value="Type book title..." name="search" id="search" class="search_gray" />
        <select class="select_book" size="30" id="select_book">
            <option>Book 1</option>
            <option>Book B</option>
            <option>The book</option>
            <option>New Book</option>
        </select>
    </fieldset>
    <script>

        $("form").submit(send_to_server)
        $("#search").click(clearSearch).blur(restoreSearch);
        $("input[type='file']").change(hideFile);



        function send_to_server() {
            if (!validate()) {
                return false;
            }

            cleanBeforeSend();


        }

        function validate() {
            var error_msg, validator;
            error_msg = "";
            validator = VALIDATION;

            $(".error").removeClass("error");
            if (!validator.text($("#title").val())) {
                error_msg += "<li>Title must not be empty</li>";
                $("#title").addClass("error");
            }

            if (!validator.text($("#author").val())) {
                error_msg += "<li>Author must not be empty</li>";
                $("#author").addClass("error");
            }

            if (!validator.date($("#release").val())) {
                error_msg += "<li>Release date must be a date with format mm/dd/yyyy</li>";
                $("#release").addClass("error");
            }

            if (!validator.text($("#publisher").val())) {
                error_msg += "<li>Publisher must not be empty</li>";
                $("#publisher").addClass("error");
            }

            if (!validator.number($("#pages").val())) {
                error_msg += "<li>Pages number must be a valid number</li>";
                $("#pages").addClass("error");
            }

            if (!validator.isbn($("#isbn").val())) {
                error_msg += "<li>ISBN must be a ten digits number</li>";
                $("#isbn").addClass("error");
            }


            if (error_msg != "") {
                $('#error_ul').html("");
                $("#error_ul").append(error_msg);
                return false;
            }

            $('#error_ul').html("");
            return true;
        }
        function clearSearch() {
            if ($(this).val() == "Type book title...") {
                $(this).val("").removeClass("search_gray");
            }

        }

        function restoreSearch() {
            if ($(this).val() == "") {
                $(this).val("Type book title...").addClass("search_gray");
            }
        }

        function clearAll() {
            $('input').val("");
            $('textarea').val("");
            $('#search').val("Type book title...");
            $('#error_ul').html("");
        }



        function showOverlay() {
            var width, height, x, y, search_height, overlay_back, overlay_swirling;
            width = $('#content-wrap').width();
            height = $('#content-wrap').height();
            x = $('#content-wrap').position().left;
            y = $('#content-wrap').position().top;
            overlay_back = $("<div>").attr("id", "overlay_back").css({ "width": width, "height": height, "top": y, "left": x });
            overlay_swirling = $("<img>").attr({ "src": "/img/loading.gif", "alt": "loading", "id": "overlay_swirling" }).css({ "width": "100px", "height": "100px", "margin-left": (width - 100) / 2, "margin-top": (height - 100) / 2 });
            overlay_back.append(overlay_swirling).prependTo($('#content-wrap'));

        }
        function hideOverlay() {
            $("#overlay_back").fadeOut("500");
            setTimeout("$('#overlay_back').remove();", 500);

        }

        var VALIDATION = (function () {

            function text(text_to_validate) {
                return new RegExp(".+").test(text_to_validate);
            }

            function number(number_to_validate) {

                return new RegExp("^[0-9]+$").test(number_to_validate);
            }

            function date(date_to_validate) {
                var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
                var returnval = false

                if (validformat.test(date_to_validate)) {
                    var monthfield = date_to_validate.split("/")[0]
                    var dayfield = date_to_validate.split("/")[1]
                    var yearfield = date_to_validate.split("/")[2]
                    var dayobj = new Date(yearfield, monthfield - 1, dayfield)
                    if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield)) {
                        alert("2");
                    } else {
                        returnval = true;
                    }
                }

                return returnval
            }

            function isbn(isbn_to_validate) {
                return number(isbn_to_validate) && (isbn_to_validate + "").length == 10;

            }

            return {
                "text": text,
                "number": number,
                "date": date,
                "isbn": isbn
            }

        } ());

        function hideFile() {

            var next_to_show, next_id;
            next_id = parseInt($(this).attr("id").split("_")[1]) + 1;
            next_to_show = "file_" + next_id;
            $('#chk_files').append('<span class="block"><input type="checkbox" id="chk_file_' + next_id + '" name="chk_file_' + next_id + '" value="chk_file_' + next_id + '" data="' + $(this).attr("id") + '" checked="true"/>' + $(this).val() + '</span>');
            if ($("#" + next_to_show).length > 0) {
                $("input[type='file']").addClass("hidden");
                $("#" + next_to_show).removeClass("hidden");
            } else {
                $(this).attr('disabled', 'true');
            }
        }

        function cleanBeforeSend() {

            for (var i = 0; i < $("input[type='checkbox'][checked!='true']").length; i++) {


                var id_to_delete = $("input[type='checkbox'][checked!='true']:eq(" + i + ")").attr("data");

                $("#" + id_to_delete).remove();
            }

        }



        /*function bookCRUD(action) {
        showOverlay();
        var title, autor, release, pages, isbn, publisher, description, id, thumbnail;
        title = $("#title").val();
        author = $("#author").val();
        release = $("#release").val();
        publisher = $("#publisher").val();
        description = $("#description").val();
        isbn = $("#isbn").val();
        pages = $("#pages").val();
        thumbnail = "";
        id = ($("#select_book").val() != "" && $("#select_book").val() != "undefined" && $("#select_book").val() != null) ? $("#select_book").val() : "";

        xmlData = '<?xml version="1.0" encoding="utf-8"?>'
        + '<Content xmlns:i="http://www.w3.org/2001/XMLSchema-instance">'
        + '<Author>' + author + '</Author>'
        + '<Description>' + description + '</Description>'
        + '<Pages>' + pages + '</Pages>'
        + '<ISBN>' + isbn + '</ISBN>'
        + '<Publisher>' + publisher + '</Publisher>'
        + '<Released>' + release + '</Released>'
        + '<Thumbnail>' + thumbnail + '</Thumbnail>'
        + '<Title>' + title + '</Title>'
        + '</Content>';


        $.ajax({
        url: "/LibraryService.mvc/",
        type: action,
        contentType: "application/xml",
        dataType: "xml",
        data: xmlData,
        success: function (msg) {
        $("#overlay_swirling").attr("src", "/img/ok.png");
        },
        error: function (msg, error_msg) {
        $("#overlay_swirling").attr("src", "/img/error.png");
        },
        final: clearAll()

        });

        setTimeout("hideOverlay()", 1000);
        }*/

        ///LibraryService.mvc/?Text=aasd;

   

    </script>
</asp:Content>
