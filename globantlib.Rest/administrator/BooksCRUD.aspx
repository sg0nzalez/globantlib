<%@ Page Title="" Language="C#" MasterPageFile="~/administrator/adminTemplate.Master"
    AutoEventWireup="true" CodeBehind="BooksCRUD.aspx.cs" Inherits="globantlib.Rest.administrator.BooksCRUD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
    <fieldset class="fs_left">
        <legend>Book Information</legend>
        <ul class="ul_form">
            <li>
                <label runat="server" id="label_title">
                    Title:</label>
                <input type="text" form="form1" id="title" name="title" required="true" runat="server" />
            </li>
            <li>
                <label>
                    Author:</label>
                <input type="text" form="form1" id="author" name="author" required="true" runat="server" />
            </li>
            <li>
                <label>
                    Release Date:</label>
                <input type="text" form="form1" id="release" name="release" required="true" runat="server" />
            </li>
            <li>
                <label>
                    Pages Number:
                </label>
                <input type="text" form="form1" range="1" min="0" id="pages" name="pages" required="true"
                    runat="server" />
            </li>
            <li>
                <label>
                    Publisher:
                </label>
                <input type="text" form="form1" id="publisher" name="publisher" required="true" runat="server" />
            </li>
            <li>
                <label>
                    ISBN - 10:
                </label>
                <input type="text" form="form1" id="isbn" name="isbn" required="true" range="1" max="9999999999"
                    runat="server" />
            </li>
            <li>
                <label>
                    Description:
                </label>
                <textarea form="form1" id="description" name="description" rows="4" cols="15" runat="server"></textarea>
            </li>
            <li>
                <label>
                    Digital Content:
                </label>
                <input form="form1" id="file_0" name="file[]" type="file" runat="server" />
                <input form="form1" id="file_1" name="file[]" type="file" class="hidden" runat="server" />
                <input form="form1" id="file_2" name="file[]" type="file" class="hidden" runat="server" />
                <input form="form1" id="file_3" name="file[]" type="file" class="hidden" runat="server" />
                <input form="form1" id="file_4" name="file[]" type="file" class="hidden" runat="server" />
                <div class="chk_files" id="chk_files">
                </div>
            </li>
            
            <li class="error_li">
                <ul class="error_ul" id="error_ul">
                
            </ul>
            </li>
            <li class="li_buttons">
                <asp:Button ID="add_edit" Text="Add/Edit" runat="server" OnClick="add_edit_Click" />
                <%-- <input id="add_edit_book" type="submit" value="submit" runat="server" onclick="add_edit_book_ServerClick" />
                --%>
                <input type="button" id="delete" value="Delete"/>
            </li>
        </ul>
        <input type="hidden" value="add" id="action" name="action" runat="server" />
        <input type="hidden" value="" id="book_id" name="book_id" runat="server" />
        <input type="hidden" id="digitals_to_delete" name="digitals_to_delete" runat="server" />
    </fieldset>
    <fieldset class="fs_right">
        <legend>Stored Books</legend>
        <input type="text" value="Type book title..." name="search" id="search" class="search_gray" />
        <select class="select_book" size="30" id="select_book">
        </select>
    </fieldset>
    </form>
    <script>

        $("#form1").submit(send_to_server)
        $("#search").click(clearSearch)//.blur(restoreSearch);
        $("input[type='file']").change(hideFile);
        $("#search").keyup(fillBooksList);
        $('#select_book').change(fillBookInfo)
        $(document).ready(clearAll);
        $("#delete").click(delete_book);


        function delete_book() {

            var id = $('input[id$="book_id"]').val();
         
            if (id != null && id!="" &&id!="undefined") {
              
                var request = XML.createXMLHttpRequest();
                request.open("DELETE", '/LibraryService.mvc/' + id, false);
                request.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        alert("Deleted");
                    }
                }
                request.setRequestHeader("Accept", "application/xml");
                request.send();
            } else {

              alert("You must select a book");
            }

            clearAll();
        }




        function send_to_server() {
            
            if (!validate()) {
                return false;
            }

            if ($("input[id$='book_id']").val() != "") {
                $("input[id$='action']").val("edit");
                delete_digitals();
            }
            cleanBeforeSend();


        }

        function validate() {
            var error_msg, validator;
            error_msg = "";
            validator = VALIDATION;

            $(".error").removeClass("error");
            if (!validator.text($("input[id$='title']").val())) {
                error_msg += "<li>Title must not be empty</li>";
                $("#title").addClass("error");
            }

            if (!validator.text($("input[id$='author']").val())) {
                error_msg += "<li>Author must not be empty</li>";
                $("#author").addClass("error");
            }

            if (!validator.date($("input[id$='release']").val())) {
                error_msg += "<li>Release date must be a date with format mm/dd/yyyy</li>";
                $("#release").addClass("error");
            }

            if (!validator.text($("input[id$='publisher']").val())) {
                error_msg += "<li>Publisher must not be empty</li>";
                $("#publisher").addClass("error");
            }

            if (!validator.number($("input[id$='pages']").val())) {
                error_msg += "<li>Pages number must be a valid number</li>";
                $("#pages").addClass("error");
            }

            if (!validator.isbn($("input[id$='isbn']").val())) {
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
            $('fieldset').find('input[type!="submit"][type!="button"]').val("");
            $('textarea').val("");
            $('#search').val("Type book title...").addClass("search_gray");
            $('#select_book').html("");
            $('input[id$="action"]').val("add");
            $('#error_ul').html("");
            resetFiles();
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
            next_id = parseInt($(this).attr("id").substring($(this).attr("id").length - 1)) + 1;

            next_to_show = "file_" + next_id;
            $('#chk_files').append('<span class="block"><input type="checkbox" id="chk_file_' + next_id + '" name="chk_file_' + next_id + '" value="chk_file_' + next_id + '" data="' + $(this).attr("id") + '" checked="true"/>' + $(this).val() + '</span>');

            if ($("input[type='file'][id$='" + next_to_show + "']").length > 0) {
                $("input[type='file']").addClass("hidden");
                $("input[type='file'][id$='" + next_to_show + "']").removeClass("hidden");
            } else {
                $(this).addClass("hidden");
            }
        }

        function cleanBeforeSend() {

            for (var i = 0; i < $("input[type='checkbox'][checked!='true']").length; i++) {


                var id_to_delete = $("input[type='checkbox'][checked!='true']:eq(" + i + ")").attr("data");

                $("#" + id_to_delete).val("");
            }

        }





        function fillBooksList() {
            var text, xml;
            text = encodeURIComponent($(this).val());
            xml = "/LibraryService.mvc/?Text=" + text;
            LOADER.show("Loading...");
            XML.transformWithCallback(xml, "/administrator/xsl/getBooks.xsl", document.getElementById("select_book"), function () { LOADER.hide(); })

        }

        function resetFiles() {
            $('input[type="file"]').addClass("hidden").val("");
            $('input[type="file"]:first').removeClass("hidden");
            $('#chk_files').html("");
            $("#error_ul").html("");
        }

        function setId() {

            $('input[id$="book_id"]').val($('#select_book').val());
        }
        function fillBookInfo() {



            
            $.get("/LibraryService.mvc/" + $(this).val(), function (data) {
                resetFiles();

                $('.fs_left input[type!="submit"][type!="button"]').val("");
                $('textarea').val("");
                $('input[id$="title"]').val($(data).find("Title").text());
                $('input[id$="author"]').val($(data).find("Author").text());
                $('input[id$="release"]').val($(data).find("Released").text());
                $('input[id$="pages"]').val($(data).find("Pages").text());
                $('input[id$="publisher"]').val($(data).find("Publisher").text());
                $('input[id$="isbn"]').val($(data).find("ISBN").text());
                $('textarea[id$="description"]').val($(data).find("Description").text());
                setId();

                $(data).find("Digital").each(function (x, i) {
                    var name = $(i).find("Link").text();
                    name = name.substring(name.lastIndexOf("/") + 1);

                    $('input[id$="file_' + x + '"]').addClass("hidden");
                    $('input[id$="file_' + (x + 1) + '"]').removeClass("hidden");
                    $('#chk_files').append('<span class="block"><input type="checkbox" id="chk_file_' + x + '" name="chk_file_' + x + '" value="chk_file_' + x + '" data="' + $('input[id$="file_' + x + '"]').attr("id") + '" checked="true" digital="'+$(i).find("ID").text()+'"/>' + name + '</span>');


                })


            })
           
        }


        function delete_digitals() {
            
            $("input[type='checkbox'][digital!=''][checked='false']").each(function () {
                previous_id = $("input[id$='digitals_to_delete']").val() == "" ? $("input[id$='digitals_to_delete']").val() : $("input[id$='digitals_to_delete']").val() + ",";
                
                $("input[id$='digitals_to_delete']").val(previous_id + $(this).attr("digital"));

            })

            
        
        }

        

    </script>
</asp:Content>
