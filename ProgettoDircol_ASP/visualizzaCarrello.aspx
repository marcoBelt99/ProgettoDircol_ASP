<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="visualizzaCarrello.aspx.cs" Inherits="ProgettoDircol_ASP.visualizzaCarrello" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Script necessari per far funzionare il bottone hamburger menù --%>
    <header>
        <%-- JQuery --%>
        <script src="https://cdn.jsdelivr.net/npm/jquery@3.5.1/dist/jquery.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
        <%--<script src="https://code.jquery-1.11.3.min.js"></script>--%>
        <%-- Popper --%>
        <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
        <%-- Bootstrap --%>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.min.js" integrity="sha384-VHvPCCyXqtD5DqJeNxl2dtTyhF78xXNXdkwX1CZeRusQfRKp+tA7hAShOK/B/fQ2" crossorigin="anonymous"></script>

        <%-- JQUERY UI --%>
        <link rel="stylesheet" href="jquery-ui/jquery-ui.min.css">
        <link rel="stylesheet" href="jquery-ui/jquery-ui.theme.min.css">
        <script src="jquery-ui/jquery-ui.min.js"></script>


        <%-- IMPORT IL FONT CHE VOGLIO IO --%>
        <style>
            @import url("https://fonts.googleapis.com/css2?family=Poppins:wght@700&display=swap");
        </style>


        <script>

            // ICONE PER I PULSANTI CON JQUERY UI
            $(function () {
                $(".widget button")
                    .eq(0).button()
                    .end().eq(1).button({
                        icon: "ui-icon-gear",
                        showLabel: false
                    }).end().eq(2).button({
                        icon: "ui-icon-gear"
                    }).end().eq(3).button({
                        icon: "ui-icon-gear",
                        iconPosition: "end"
                    }).end().eq(4).button({
                        icon: "ui-icon-gear",
                        iconPosition: "top"
                    }).end().eq(5).button({
                        icon: "ui-icon-gear",
                        iconPosition: "bottom"
                    });
            });

        </script>

        <style>
            .EmptyDataVacancyClass {
                display: none;
                border: none;
                box-shadow: 0px 0px 0px #ffffff;
                background-color: white;
            }
        </style>

    </header>

    <%--<p>Rimuovi dal carrello</p>--%>
    <%-- <asp:Button ID="btnRimuovi" runat="server" OnClick="btnRimuovi_Click" Text="rimuovi" CssClass="btn btn-danger" />--%>

    <div style="padding: 50px;"></div>
    <asp:Panel ID="pnCarrello" runat="server"
        HorizontalAlign="Center">

        <h3>I miei ordini</h3>
        <br />

        <%-- ######## --%>
        <%-- RIGA --%>
        <%-- ####### --%>
        <div class="row">
            <%-- ########### --%>
            <%-- COLONNA 1 --%>
            <%-- ########### --%>
            <div class="col-md-6">

                <div class="col-md-12">
                    <%-- ############ --%>
                    <%-- GRID VIEW --%>
                    <%-- ############ --%>
                    <asp:GridView
                        ID="gvCarrello"
                        runat="server"
                        AutoGenerateColumns="false"
                        OnRowCommand="gvCarrello_RowCommand"
                        DataKeyNames="ID"
                        CssClass="table mx-auto w-auto stileTabelle"
                        BackColor="White"
                        HorizontalAlign="Center">
                        <%--<EmptyDataRowStyle CssClass="EmptyDataClass" />--%>
                        <EmptyDataRowStyle BackColor="White"
                            BorderStyle="None"
                            BorderColor="White"
                            ForeColor="white"
                            BorderWidth="0"
                            CssClass="EmptyDataClass" />
                        <EmptyDataTemplate>
                            <div class="justify-content-center border-white">
                                <div class="alert alert-warning alert-alert-dismissible" role="alert">
                                    <span type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></span>
                                    <center>
                                        <h6>Il carrello è vuoto.</h6>
                                        <h6>Corri subito ad acquistare un capo d'abbigliamento!</h6>
                                    </center>
                                </div>
                            </div>
                        </EmptyDataTemplate>
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
                                ControlStyle-CssClass="btn btn-info IconaDettagli"
                                HeaderText="Mostra dettagli" />
                            <%-- ######################## --%>
                            <asp:ButtonField
                                Text="Rimuovi"
                                ButtonType="Link"
                                CommandName="EliminaCapo"
                                ControlStyle-CssClass="btn btn-danger IconaRimuovi"
                                HeaderText="Rimuovi un capo" />
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
            <%-- ########### --%>
            <%-- COLONNA 2 --%>
            <%-- ########### --%>
            <div class="col-md-6">
                <div class="col-md-12">

                    <%-- CARD TOTALE PREZZO DI LISTINO --%>
                    <center>
                        <div class="card " style="max-width: 18rem;">
                            <div class="card-header text-white " style="background-color: #7952b3;">Spesa di listino</div>
                            <div class="card-body" style="background-color: #ceadff;">
                                <asp:Label class="card-title" ID="lblTotaleCarrello" runat="server"></asp:Label>
                                <%--<p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>--%>
                            </div>
                            <div class="card-footer " style="padding: 0px; margin: 0px; text-align: center; background-color: #7952b3;">
                                <div class="alert alert-warning" role="alert" style="padding: 0; display: inline-block; width: auto;">
                                    <p style="font-size: 10px;">
                                        <i class="bi bi-exclamation-diamond-fill" style="color: goldenrod"></i>
                                        &nbsp;Il prezzo è al netto di IVA e spese di trasporto
                                    </p>
                                </div>
                            </div>
                    </center>

                    <br />



                    <%-- DETTAGLI DEL MODELLO --%>
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
                </div>
            </div>
        </div>

        <%-- BOTTONE PROSEGUI CON GLI ACQUISTI
             BOTTONE PAGA --%>
        <center>
            <div style="max-width: 400px">
                <asp:LinkButton ID="btnContinua" runat="server"
                    CssClass="btn  btn btn-outline-dark btn-lg btn-block" role="button"
                    OnClick="btnContinua_Click">
                       <i class="bi bi-cart-plus-fill"></i>&nbsp;&nbsp;Continua con gli acquisti&nbsp;&nbsp;
                </asp:LinkButton>
                <asp:LinkButton ID="btnPaga" runat="server" CssClass="btn  btn btn-outline-success btn-lg btn-block" role="button"
                    OnClick="btnPaga_Click">
                        <i class="bi bi-wallet-fill"></i>&nbsp;&nbsp;Termina e Paga&nbsp;&nbsp;
                </asp:LinkButton>

            </div>
        </center>

    </asp:Panel>


    <div style="height: 150px;">
    </div>

    <%-- #################### AGGIUNGO UN'ICONA AI PULSANTI  #########################--%>
    <script>
        $(".IconaDettagli").html("<i class='bi bi-three-dots'></i> &nbsp;");
        $(".IconaRimuovi").html("<i class='bi bi-bag-dash-fill'></i> &nbsp;");
    </script>

</asp:Content>
