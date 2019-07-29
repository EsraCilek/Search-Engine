<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebOdev.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.css" rel="stylesheet" />
</head>
<body style="background-color:purple">
    <form id="form1" runat="server">
        <div style="margin-top:200px">
            
         <center>
        <div>
            <asp:Label ID="lblsonuc" style="color:black" Visible="false" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" style="color:white" Text="Anahtar Kelime"></asp:Label>
            <asp:TextBox ID="txtanahtar" TextMode="MultiLine" CssClass="form-text" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" style="color:white" Text="URL"></asp:Label>
            <asp:TextBox ID="txturl" CssClass="form-text" TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>
        <div>
            <br />
            <asp:Button ID="btnbul" OnClick ="btnbul_Click" CssClass="btn btn-danger" style="width:175px" runat="server"  Text="Ara" />
        </div>
        </center>
            </div>
    </form>
</body>
</html>
