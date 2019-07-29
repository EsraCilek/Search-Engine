<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LinkScanner._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Enter url:
        <asp:TextBox ID="urlTxt" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="urlTxt" ErrorMessage="Invalid url" 
            ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
            <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Search" />
        <br />
        <asp:ListBox ID="urlLst" runat="server" Height="276px" Width="647px">
        </asp:ListBox>
    
    </div>
    </form>
</body>
</html>
