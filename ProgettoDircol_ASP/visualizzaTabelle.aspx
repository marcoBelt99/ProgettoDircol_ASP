<%@ Page Title="Visualizza tabelle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="visualizzaTabelle.aspx.cs" Inherits="ProgettoDircol_ASP.visualizzaTabelle" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%-- Costruisco un Panel: un <div>, cioè un contenitore per altri controlli --%>
    <asp:Panel ID="pnTabelle" runat="server" HorizontalAlign="Center" >
        
        <h2 id="titolo1">CAPI</h2>
        <%-- Costruisco la tabella di visualizzazione della query --%>
        <asp:GridView runat="server" ID="gvCapi" CellPadding="1" CssClass="table mx-auto w-auto stileTabelle"
            HorizontalAlign="Center" GridLines="None" >
            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White"  />
       <%--<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="true" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="true" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />--%>
        </asp:GridView>



        <h2 id="titolo2">DIPENDENTI</h2>
        <asp:GridView runat="server" ID="gvDipendenti" CellPadding="1" CssClass="table mx-auto w-auto stileTabelle"
            HorizontalAlign="Center" GridLines="None">
            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White"  />
        </asp:GridView>



        <h2 id="titolo3">MODELLI</h2>
        <asp:GridView runat="server" ID="gvModelli" CellPadding="1" CssClass="table mx-auto w-auto stileTabelle"
            HorizontalAlign="Center" GridLines="None">
            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White"  />
        </asp:GridView>

         <h2 id="titolo4">PUNTI VENDITA</h2>
        <asp:GridView runat="server" ID="gvPuntiVendita" CellPadding="1" CssClass="table mx-auto w-auto stileTabelle"
            HorizontalAlign="Center" GridLines="None">
            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White"  />
        </asp:GridView>



        <h2 id="titolo5">VENDITE</h2>
        <asp:GridView runat="server" ID="gvVendite" CellPadding="1" CssClass="table mx-auto w-auto stileTabelle"
            HorizontalAlign="Center" GridLines="None">
            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White"  />
        </asp:GridView>

    </asp:Panel>



    <%-- Javascript e JQuery nel documento corrente --%>
    <script>
        $(document).ready(function () {
            // Aggiungo a tutte le tabelle con id "#table-centerN" le classi contenute in addClass(...)
            $(".table").addClass("table mx-auto w-auto stileTabelle");
            // $("[id$=gvCapi]").addClass("table mx-auto w-auto stileTabelle");

    </script>

</asp:Content>
