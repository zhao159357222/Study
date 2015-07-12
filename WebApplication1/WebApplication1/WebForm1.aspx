<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Leo.FrameWork.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="txtJson" runat="server" TextMode="MultiLine" Height="500px" Width="500px">

    </asp:TextBox>
        result:<asp:TextBox ID="txtRes" runat="server" TextMode="MultiLine" Height="500px" Width="500px"></asp:TextBox>
        <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" />
    </div>
    </form>
</body>
</html>
