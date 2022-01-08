<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestioneViewState.aspx.cs" Inherits="ProgettoDircol_ASP.STUDIO.GestioneViewState" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <%-- GESTIONE DEL VIEW STATE --%>
    <h1>Gestione del View State</h1>

    <div class="lead">
        <%-- Literal control --%>
        <asp:Literal ID="ltPostBack" runat="server" />
    </div>

    <%-- Creazione Form --%>
    <div class="form-group">
        <label>Nome</label>
        <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label>Cognome</label>
        <asp:TextBox ID="txtCognome" runat="server" CssClass="form-control" />
    </div>
     <div class="form-group">
        <label>Username</label>
        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label>Email</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group">
        <label>Numero di Telefono</label>
        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
    </div>
     <div class="form-group">
        <label>Citta</label>
        <asp:TextBox ID="txtCitta" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Regione</label>
        <%-- DropDownList --%>
        <asp:DropDownList ID="ddlRegioni" CssClass="form-control" runat="server">
            <asp:ListItem Value="">Seleziona la tua Regione</asp:ListItem>
            <asp:ListItem Value="ABR">Abruzzo</asp:ListItem>
            <asp:ListItem Value="BAS">Basilicata</asp:ListItem>
            <asp:ListItem Value="CAL">Calabria</asp:ListItem>
            <asp:ListItem Value="CAM">Campania</asp:ListItem>
            <asp:ListItem Value="ERO">Emilia-Romagna</asp:ListItem>
            <asp:ListItem Value="FRI">Friuli Venezia Giulia</asp:ListItem>
            <asp:ListItem Value="LAZ">Lazio</asp:ListItem>
            <asp:ListItem Value="LIG">Liguria</asp:ListItem>
            <asp:ListItem Value="LOM">Lombardia</asp:ListItem>
            <asp:ListItem Value="MAR">Marche</asp:ListItem>
            <asp:ListItem Value="MOL">Molise</asp:ListItem>
            <asp:ListItem Value="PIE">Piemonte</asp:ListItem>
            <asp:ListItem Value="PUG">Puglia</asp:ListItem>
            <asp:ListItem Value="SAR">Sardegna</asp:ListItem>
            <asp:ListItem Value="SIC">Sicilia</asp:ListItem>
            <asp:ListItem Value="TOS">Toscana</asp:ListItem>
            <asp:ListItem Value="TAA">Trentino-Alto Adige</asp:ListItem>
            <asp:ListItem Value="UMB">Umbria</asp:ListItem>
            <asp:ListItem Value="VDA">Valle d'Aosta</asp:ListItem>
            <asp:ListItem Value="VEN">Veneto</asp:ListItem>
        </asp:DropDownList>
    </div>

    <div class="form-group">
        <%-- Button con un evento click --%>
        <asp:Button ID="btnSubmit" runat="server"
            OnClick="btnSubmit_Click" CssClass="btn btn-primary"
            Text="Invia"/>
    </div>

</asp:Content>
