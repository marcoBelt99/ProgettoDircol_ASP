<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalveMondoSingle.aspx.cs" Inherits="ProgettoDircol_ASP.SalveMondoSingle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnSaluta" runat="server" Text="Saluta"
            OnClick="btnSaluta_Click" />
        <br />
        <asp:Label ID="lblSaluto" runat="server" Text="..."></asp:Label>
    </form>
</body>

</html>
