<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBindingSemplice.aspx.cs" Inherits="ProgettoDircol_ASP.STUDIO.DataBindingSemplice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            Ragione sociale: <%# GetRagioneSociale()%>

            Bilancio:
            <asp:Label ID="lblBilancio" runat="server"
                Text="<%# GetBilancio()%>"
                ForeColor="<%# GetColoreBilancio()%>"></asp:Label>
            <asp:Button ID="btnPrec" runat="server" Text="Indietro" OnClick="btnPrec_Click" />
        <asp:Button ID="btnNext" runat="server" Text="Avanti" OnClick="btnNext_Click" />
        </div>
    </form>
</body>
</html>
