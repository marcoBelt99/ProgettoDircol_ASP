<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="visualizzaCarrello.aspx.cs" Inherits="ProgettoDircol_ASP.visualizzaCarrello" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Script necessari per far funzionare il bottone hamburger menù --%>
    <header>
        <%-- JQuery --%>
        <%--<script src="https://cdn.jsdelivr.net/npm/jquery@3.5.1/dist/jquery.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>--%>
        <script src="https://code.jquery-1.11.3.min.js"></script>
        <%-- Popper --%>
        <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
        <%-- Bootstrap --%>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.min.js" integrity="sha384-VHvPCCyXqtD5DqJeNxl2dtTyhF78xXNXdkwX1CZeRusQfRKp+tA7hAShOK/B/fQ2" crossorigin="anonymous"></script>
        <%-- JQuery Mobile --%>
        <link rel="stylesheet" href="https://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.css" />
        <script src="https://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.js"></script>
    </header>

    <%--<p>Rimuovi dal carrello</p>--%>
    <%-- <asp:Button ID="btnRimuovi" runat="server" OnClick="btnRimuovi_Click" Text="rimuovi" CssClass="btn btn-danger" />--%>

    <div style="padding: 50px;"></div>
    <asp:Panel ID="pnCarrello" runat="server"
        HorizontalAlign="Center">


        <asp:GridView
            ID="gvCarrello"
            runat="server"
            AutoGenerateColumns="false"
            OnRowCommand="gvCarrello_RowCommand"
            DataKeyNames="ID"
            CssClass="table mx-auto w-auto stileTabelle"
            BackColor="White"
            HorizontalAlign="Center">
            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="Taglia" HeaderText="Taglia" />
                <asp:BoundField DataField="Colore" HeaderText="Colore" />
                <asp:BoundField DataField="PuntoVendita" HeaderText="Punto Vendita" />
                <asp:BoundField DataField="CodModello" HeaderText="Codice modello" />
                <asp:ButtonField
                    Text="Dettagli"
                    ImageUrl="~/Images/Icone/caret-right-fill.svg"
                    ButtonType="Link"
                    CommandName="DettagliCapo"
                    ControlStyle-CssClass="btn btn-info"
                    HeaderText="Mostra dettagli" />
                <%-- ######################## --%>
                <asp:ButtonField
                    Text="Rimuovi"
                    ButtonType="Button"
                    CommandName="EliminaCapo"
                    ControlStyle-CssClass="btn btn-danger"
                    HeaderText="Rimuovi un capo" />
            </Columns>
        </asp:GridView>






        <div class="content" id="divDettaglio" runat="server">
            <div class="col">
                <%--<asp:Image ID="ImgModello" runat="server"></asp:Image>--%>
                <img id="ImgModello" runat="server" />
            </div>
        </div>
        <div class="col">
            <asp:Label ID="lblNome" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblDescrizione" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblPrezzoListino" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblGenere" runat="server"></asp:Label>
            <asp:Label ID="lblCollezione" runat="server"></asp:Label>
        </div>



    </asp:Panel>






</asp:Content>
