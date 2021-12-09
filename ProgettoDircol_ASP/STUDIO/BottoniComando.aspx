<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BottoniComando.aspx.cs" Inherits="ProgettoDircol_ASP.STUDIO.BottoniComando" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <%-- La pagina contiene tre bottoni dei tipi specificati, ognuno dei quali associa l’evento OnCommand al 
            gestore di evento EventoComando:  --%>
        <div>
            <asp:Button runat="server" Text="Button" CommandName="Button"
                OnCommand="EventoComando"></asp:Button>
            <br />
            <asp:LinkButton runat="server" CommandName="LinkButton"
                OnCommand="EventoComando">LinkButton</asp:LinkButton>
            <br />
            <asp:ImageButton runat="server" Width="98px" Height="25px"
                ImageUrl="bottone.gif" CommandName="ImageButton"
                OnCommand="EventoComando"></asp:ImageButton>
            <br />
            <br />

            <asp:Label runat="server" ID="lblComando"></asp:Label>
        </div>
    </form>
</body>
</html>
