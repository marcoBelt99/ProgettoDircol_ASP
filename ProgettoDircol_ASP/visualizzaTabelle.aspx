<%@ Page Title="Visualizza tabelle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="visualizzaTabelle.aspx.cs" Inherits="ProgettoDircol_ASP.visualizzaTabelle" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%-- Costruisco un Panel: un <div>, cioè un contenitore per altri controlli --%>
    <asp:Panel ID="pnTabelle" runat="server" HorizontalAlign="Center">



        <%-- ACCORDION --%>
        <div class="accordion" id="accordionExample">
            <%-- Prima Card --%>
            <div class="card">
                <div class="card-header intestazioneAccordion" id="headingOne">
                    <h2 class="mb-0">
                        <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                            Visualizza Capi
                        </button>
                    </h2>
                </div>
                <%-- class="show" --%>
                <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordionExample">
                    <div class="card-body">
                        <%-- contenuto che ci voglio mettere dentro --> istruzioni per la tabella (grid view) --%>
                        <h2 id="titolo1">CAPI</h2>
                        <%-- Costruisco la tabella di visualizzazione della query --%>
                        <asp:GridView runat="server" ID="gvCapi" CellPadding="1" CssClass="table mx-auto w-auto stileTabelle"
                            HorizontalAlign="Center" GridLines="None">
                            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%-- Seconda Card --%>
            <div class="card">
                <div class="card-header intestazioneAccordion" id="headingTwo">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                            Visualizza Dipendenti
                        </button>
                    </h2>
                </div>
                <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                    <div class="card-body">
                        <%-- contenuto --%>

                        <h2 id="titolo2">DIPENDENTI</h2>
                        <asp:GridView runat="server" ID="gvDipendenti" CellPadding="1" CssClass="table mx-auto w-auto stileTabelle"
                            HorizontalAlign="Center" GridLines="None">
                            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%-- Terza Card --%>
            <div class="card">
                <div class="card-header intestazioneAccordion" id="headingThree">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                            Visualizza Modelli
                        </button>
                    </h2>
                </div>
                <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                    <div class="card-body">

                        <h2 id="titolo3">MODELLI</h2>
                        <asp:GridView runat="server" ID="gvModelli" CellPadding="1" CssClass="table mx-auto w-auto stileTabelle"
                            HorizontalAlign="Center" GridLines="None">
                            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </div>


            <%-- Quarta Card --%>
            <div class="card">
                <div class="card-header intestazioneAccordion" id="headingFour">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                            Visualizza Punti Vendita
                        </button>
                    </h2>
                </div>
                <div id="collapseFour" class="collapse" aria-labelledby="headingFour" data-parent="#accordionExample">
                    <div class="card-body">
                        <h2 id="titolo4">PUNTI VENDITA</h2>
                        <asp:GridView runat="server" ID="gvPuntiVendita" CellPadding="1" CssClass="table mx-auto w-auto stileTabelle"
                            HorizontalAlign="Center" GridLines="None">
                            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <%-- Quinta Card --%>
            <div class="card">
                <div class="card-header intestazioneAccordion" id="headingFive">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseFive" aria-expanded="false" aria-controls="collapseFive">
                            Visualizza Vendite
                        </button>
                    </h2>
                </div>
                <div id="collapseFive" class="collapse" aria-labelledby="headingFive" data-parent="#accordionExample">
                    <div class="card-body">
                        <%-- contenuto --%>

                        <h2 id="titolo5">VENDITE</h2>
                        <asp:GridView runat="server" ID="gvVendite" CellPadding="1" CssClass="table mx-auto w-auto stileTabelle"
                            HorizontalAlign="Center" GridLines="None">
                            <HeaderStyle BackColor="#7952b3" Font-Bold="true" ForeColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <!-- Fine div accordion-->
        <%-- FINE ACCORDION --%>
    </asp:Panel>



    <%-- Javascript e JQuery nel documento corrente --%>
    <script>
        $(document).ready(function () {
            // Aggiungo a tutte le tabelle con id "#table-centerN" le classi contenute in addClass(...)
            $(".table").addClass("table mx-auto w-auto stileTabelle");
            // $("[id$=gvCapi]").addClass("table mx-auto w-auto stileTabelle");

            $("#accordionExample").accordion({ collapsible: true, active: false });

    </script>

</asp:Content>

<%-- Alcune istruzioni per le grid view:
    
     <%-- <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="true" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="true" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" /> --%>
    
