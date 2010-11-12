<%@ Page Title="" Language="C#" MasterPageFile="~/administrator/adminTemplate.Master" AutoEventWireup="true" CodeBehind="devicesCRUD.aspx.cs" Inherits="globantlib.Rest.administrator.devicesCRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="form1" runat="server" enctype="multipart/form-data" method="post">
    <fieldset class="fs_left">
        <legend>Device information</legend>
        <ul class="ul_form">
            <li>
                <label>
                    Name:</label>
                <input type="text" id="device_name" name="device_name" />
            </li>
            <li>
                <label>
                    Description:</label>
                <textarea  id="device_description" name="device_description" rows="4" cols="15" runat="server"></textarea>
                
            </li>
            <li>
                <label>
                    Image:</label>
                <iframe src="uploadFrame.aspx"></iframe>
              
                
            </li>
             <li>
                <img id="device_thumbnail" alt="" src="" runat="server" class="device_thumbnail"/>
                <input type="hidden" id="device_image_src" />
             </li>
             <li>
                <label>Dispositives:</label>
                <table class="dispositives_list">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Serial Number</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <td colspan="2">
                                <a href="#" class="link" id="add_new">Add New</a>
                            </td>
                        </tr>
                    </tfoot>
                    <tbody>
                        <tr>
                            <td class="td_center">Pancho</td>
                            <td class="td_right">12346578</td>
                        </tr>
                    </tbody>
                </table>
             </li>
            
        </ul>
    </fieldset>
    <fieldset class="fs_right">
        <legend></legend>
    </fieldset>

</form>



</asp:Content>
<asp:Content ContentPlaceHolderID="overlay_placeHolder" runat="server">
<div class="overlay_back">
        <div class="overlay_content">
            <div class="close_div">
                <a href="#" class="close_link" id="close_link">X Close</a>    
            </div>
            <div class="form_div">
                <form id="f_device" action="">
                    <ul class="ul_form in_overlay">
                        <li>
                            <label>Name: </label>
                            <input type="text" name="new_device_name" id="new_device_name" />
                        </li>
                        <li>
                            <label>Serial Number: </label>
                            <input type="text" name="new_device_sn" id="new_device_sn" />
                        </li>
                        <li>
                            <input type="button" id="btn_new" class="btn" name="btn_new" value="Add"/>
                        </li>
                    </ul>
            
                </form>
            </div>
        </div>

    </div>

</asp:Content>

<asp:Content ContentPlaceHolderID="scripts_placeHolder" runat="server">
    <script>
        $(".file").change(clickButton);


        function clickButton() {
            $(".click").click();
        }

        function addLoadingBar() {
            var img_src = "/img/loading_bar.gif";
            $(".device_thumbnail").attr("src", img_src).addClass("loading_bar");

        }

        function loadSrc() {
            setTimeout("loadImage();", 1000);


        }

        function loadImage() {
            var img_src = "/img/devices/" + $("#device_image_src").val();
            $(".device_thumbnail").attr("src", img_src).removeClass("loading_bar");

            if ($(".device_thumbnail").height() == 0 || $(".device_thumbnail").height() == 20) {

                setTimeout("loadSrc()", 1000);
            }

        }

  
        

        

        

        $(document).ready(loadFunctions);

        function loadFunctions() {
            $("#close_link").click(OVERLAY.close);
            $("#btn_new").click(saveDevice);
            $("#add_new").click(OVERLAY.show);
        }
      
        function saveDevice() { }

        
        
        var OVERLAY = (function () {
            function showOverlay() {
                $(".overlay_back").fadeIn(500);
            }
            function closeOverlay() {

                $(".overlay_back").fadeOut(500, clearOverlayInputs);
                

            }
            function clearOverlayInputs() {
                $("#f_device").find("input[type='text']").val("");
            }
            return {
                "show" : showOverlay,
                "close" : closeOverlay
            }

        } ());

    </script>

</asp:Content>