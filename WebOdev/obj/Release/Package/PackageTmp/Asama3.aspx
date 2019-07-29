<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Asama3.aspx.cs" Inherits="WebOdev.Asama3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css"/>
</head>
<body style="background-color: purple">
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-sm bg-dark navbar-dark">
  <!-- Brand/logo -->
  <a class="navbar-brand" href="#">Arama Motoru</a>
  
  <!-- Links -->
  <ul class="navbar-nav">
    <li class="nav-item">
      <a class="nav-link" href="Asama1.aspx">Aşama 1</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="Asama2.aspx">Aşama 2</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" href="Asama3.aspx">Aşama 3</a>
    </li>
       <li class="nav-item">
      <a class="nav-link" href="Asama4.aspx">Aşama 4</a>
    </li>
  </ul>
</nav>
        <div style="margin-top: 150px">

            <center>
                      

        <div>
            <asp:Label ID="lblsonuc" style="color:black" Visible="false" runat="server" Text=""></asp:Label>
        </div>
        <div >
            <asp:Label runat="server" style="color:white" Text="Anahtar Kelime"></asp:Label>
            <asp:TextBox ID="txtanahtar" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" style="color:white" Text="URL"></asp:Label>
            <asp:TextBox ID="txturl" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
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
