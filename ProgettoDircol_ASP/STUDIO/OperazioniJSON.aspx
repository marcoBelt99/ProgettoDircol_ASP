<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OperazioniJSON.aspx.cs" Inherits="ProgettoDircol_ASP.STUDIO.OperazioniJSON" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="pnOperazioniJSON" runat="server">


        <%-- PROVA PER UN SINGOLO CARRELLO --%>
        <h3>PROVA PER VISUALIZZARE UN SINGOLO CARRELLO </h3>
        <br />
        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:DropDownList ID="ddlListaID" runat="server" CssClass="form-control"></asp:DropDownList>

        <br />
        <hr />
        <br />

        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>



        <%-- REPEATER PER PROVARE  A VISUALIZZARE LA LISTA CARRELLI --%>
        <h3>REPEATER PER VISUALIZZARE LISTA CARRELLI </h3>
        <br />
        <asp:Repeater ID="rptCarrello" runat="server" OnItemDataBound="rptCarrello_ItemDataBound">
            <HeaderTemplate>
                <table class="table table-dark">
                    <thead class="thead-dark">
                        <th>Username</th>
                        <th>Lista ID capi</th>
                    </thead>
                    <tbody class="tbody-dark">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("Username")%>
                    </td>
                    <td>
                        <%-- POSSO SCEGLIERE DI VEDERE SIA CON LIST BOX SIA CON DDL --%>
                        <%-- <asp:DropDownList ID="ddlCarrello_Iesimo" runat="server"
                            DataSource='<%# Eval("ListaIDCapi") %>' CssClass="form-control">
                        </asp:DropDownList>--%>
                        <asp:ListBox ID="lbCarrello_Iesimo" runat="server"
                            DataSource='<%# Eval("ListaIDCapi") %>' CssClass="form-control"></asp:ListBox>
                    </td>
                </tr>
            </ItemTemplate>
            <SeparatorTemplate>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
            </SeparatorTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <br />
        <%-- ############################################################################## --%>
        <%-- ############################################################################## --%>
        <%-- ############################################################################## --%>
        <%-- ############################################################################## --%>
        <%-- ############################################################################## --%>
        <%-- ############################################################################## --%>
        <%-- ############################################################################## --%>

        <%-- QUERIES IN LINQ --%>


        <h2 style="color: darkslateblue">Queries in LINQ</h2>
        <h4 style="color: red">Devo:</h4>

        <ol>
            <li>Definire il modello --> La classe. Nel mio caso Carrello</li>
            <li>Leggere e deserializzare il file JSON</li>
            <li>Eseguire la Query in LINQ</li>
        </ol>


        <div>
            <h4>Elenco degli utenti che hanno un carrello</h4>
            <h5>Tutti quelli che hanno almeno 1 capo nella lista degli id</h5>
            <asp:TextBox ID="txtQuery_1" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
        </div>
        <asp:Button ID="btnQuery_1" runat="server" Text="Elenco Utenti"
            OnClick="btnQuery_1_Click" />


        <div>
            <h4>Ricerca di un determinato utente</h4>
            <h5>Cerco in base al suo Username</h5>
            <p>Inserire lo username da ricercare</p>
            <asp:TextBox ID="txtQuery_2" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
        </div>
        <asp:Button ID="btnQuery_2" runat="server" Text="Ricerca determinato utente"
            OnClick="btnQuery_2_Click" />
        <br />
        <asp:Literal ID="ltRisultatoQuery_2" runat="server"></asp:Literal>

        <br />
        <hr />
        <br />




        <div>
            <h4>ELENCO DEI CAPI (DEGLI ID) DI UN DETERMINATO UTENTE)</h4>
            <h5>Cerco in base al suo Username</h5>
            <p>Inserire lo username da ricercare</p>
            <asp:TextBox ID="txtQuery_3" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
        </div>
        <asp:Button ID="btnQuery_3" runat="server" Text="Lista capi"
            OnClick="btnQuery_3_Click" />
        <br />
        <asp:TextBox ID="txtRisultatoQuery_3" runat="server" TextMode="MultiLine" BorderStyle="Dashed"></asp:TextBox>

        <br />
        <hr />
        <br />






        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>
        <%-- ###################################################################### --%>

        <div>
            <h3 style="color: aquamarine">QUERY DI INSERIMENTO IN APPEND AL JSON</h3>
            <p>Username da inserire:</p>
            <asp:TextBox ID="txtUsername_Inserimento" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
            <p>Lista di ID di capi separati da una virgola</p>
            <asp:TextBox ID="txtListaIDCapi_Inserimento" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="btnInserisci" runat="server" Text="Inserisci"
            OnClick="btnInserisci_Click" />
        <br />
        <asp:Literal ID="ltInserimento" runat="server"></asp:Literal>

        <br />




    </asp:Panel>


    <%-- Piccolo script in JQuery --%>
    <script>
        $('input[type="submit"').addClass("btn btn-primary mx-auto");
        $('input[type="submit"').style("btn btn-primary mx-auto");
    </script>

</asp:Content>



<%-- ItemType="ProgettoDircol_ASP/Dati/Carrello"
                            SelectMethod="GetListaID"
                            DataSource='<%# Eval("ListaIDCapi") %>' --%>


