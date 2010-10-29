<%@ Page Title="" Language="C#" MasterPageFile="~/administrator/adminTemplate.Master" AutoEventWireup="true" CodeBehind="BooksCRUD.aspx.cs" Inherits="globantlib.Rest.administrator.BooksCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <fieldset class="fs_left">
            <legend>Book Information</legend>
            <ul class="ul_form">
                <li>
                    <label>Title:</label>
                    <input type="text" id="title" name="title"/>
                </li>
                <li>
                    <label>Author:</label>
                    <input type="text" id="author" name="author"/>
                </li>
                <li>
                    <label>Release Date:</label>
                    <input type="text" id="release" name="release"/>
                </li>
                <li>
                    <label>Pages Number: </label>
                    <input type="text" id="pages" name="pages"/>
                </li>
                <li>
                    <label>Publisher: </label>
                    <input type="text" id="publisher" name="publisher"/>
                </li>
                <li>
                    <label>Description: </label>
                    <textarea id="description" name="description" rows="4" cols="15"></textarea>
                </li>
                <li class="li_buttons">
                    <button id="add_edit_book">Add/Edit</button>
                    <button id="delete_book">Delete</button>
                </li>
            </ul>
        </fieldset>
        <fieldset class="fs_right">
            <legend>Stored Books</legend>
            <input type="search" value="Type book title..." name="search" id="search" class="search_gray"/>
            <select class="select_book" size="30" id="select_book">
                <option>Book 1</option>
                <option>Book B</option>
                <option>The book</option>
                <option>New Book</option>

            </select>
        </fieldset>

        <script>

            $("form").submit(function () { return false; })
            $("#search").click(clearSearch).blur(restoreSearch);
            $("#add_edit_book").click(function () {bookCRUD("post"); });

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
            }

            function bookCRUD(action) {
                var title, autor, release, pages, publisher, description, id, thumbnail;
                title = $("#title").val();
                author = $("#author").val();
                release = $("#release").val();
                publisher = $("#publisher").val();
                description = $("#description").val();
                pages = $("#pages").val();
                thumbnail = "";
                id = ($("#select_book").val() != "" && $("#select_book").val() != "undefined" && $("#select_book").val() != null)?$("#select_book").val():"";

                xmlData = '<?xml version="1.0" encoding="utf-8"?>'
                            + '<Content>'
                                + '<Author>' + author + '</Author>'
                                + '<Description>' + description + '</Description>'
                                + '<Pages>' + pages + '</Pages>'
                                + '<Publisher>' + publisher + '</Publisher>'
                                + '<Released>' + release + '</Released>'
                                + '<Thumbnail>' + thumbnail + '</Thumbnail>'
                                + '<Title>' + title + '</Title>'
                            + '</Content>';

                alert(xmlData);

                $.ajax({
                    url: "/LibraryService.mvc/",
                    contentType : "application/xml",
                    dataType : "xml",
                    type: action,
                    data: xmlData,
                    success: function (msg) {
                        alert(msg);
                    },
                    error: function (msg, error_msg) {
                        alert(error_msg);
                    }

                });
            

            }
        </script>

</asp:Content>
