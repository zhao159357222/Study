<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtURL" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtResultPath" runat="server"></asp:TextBox>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>
