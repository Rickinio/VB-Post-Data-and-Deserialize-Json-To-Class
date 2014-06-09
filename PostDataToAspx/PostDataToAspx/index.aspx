<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="PostDataToAspx.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="//code.jquery.com/jquery-1.11.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    <script>
        function Run(){
            $.ajax({
                async: true,
                type: "POST",
                url: "PostedData.aspx",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ title:"This is Title",subtitle:"This is Subtitle",article:"This is article" }),
                success: function (msg) {
                    alert("success");

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("error");
                }
            });
        }
    </script>
</body>
</html>
