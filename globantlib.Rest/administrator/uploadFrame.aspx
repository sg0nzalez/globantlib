<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadFrame.aspx.cs" Inherits="globantlib.Rest.administrator.uploadFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="file" runat="server" id="device_image" 
                name="device_image" class="file"/>
                
        <asp:Button id="click_image" runat="server" UseSubmitBehavior="false" 
                onclick="click_image_Click" class="click" style="display: none;"/>
        
    </div>
    </form>
    <style>
        
        
        body
        {
            margin: 0;    
        }
    </style>
    <script>
        $(".file").change(clickButton);

        function clickButton() {
            window.parent.addLoadingBar();
            //This are inputs  from the container of the iframe (this page will appear inside an iframe)
            var device_name, device_format;  
            device_name= top.document.getElementById("device_name");
            device_format = top.document.getElementById("device_image_src");
            $(device_format).val($(this).val());
            window.parent.loadSrc();
            $(".click").click();
        }

       
    </script>
</body>
</html>
