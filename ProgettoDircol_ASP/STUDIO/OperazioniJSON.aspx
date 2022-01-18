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


        <div id="query1">
            <h4>Elenco degli utenti che hanno un carrello</h4>
            <h5>Tutti quelli che hanno almeno 1 capo nella lista degli id</h5>
            <asp:TextBox ID="txtQuery_1" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
        </div>
        <asp:Button ID="btnQuery_1" runat="server" Text="Elenco Utenti"
            OnClick="btnQuery_1_Click" />


        <div id="query2">
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


        <div id="query3">
            <h4>ELENCO DEI CAPI (DEGLI ID) DI UN DETERMINATO UTENTE)</h4>
            <h5>Cerco in base al suo Username</h5>
            <p>Inserire lo username da ricercare</p>
            <asp:TextBox ID="txtQuery_3" runat="server" CssClass="form-control"></asp:TextBox>
            <br />
        </div>
        <asp:Button ID="btnQuery_3" runat="server" Text="Lista capi"
            OnClick="btnQuery_3_Click" />
        <br />
        <asp:TextBox ID="txtRisultatoQuery_3" runat="server"
            TextMode="MultiLine" BorderStyle="Dashed"
            ReadOnly="true"></asp:TextBox>

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


        <%-- INSERIMENTO --%>

        <div id="inserimento">
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




        <%-- AGGIORNAMENTO --%>
        <div id="aggiornamento">
            <h3 style="color: aquamarine">QUERY DI AGGIORNAMENTO JSON</h3>
            <p>Seleziona lo Username</p>
            <asp:DropDownList ID="ddlUsernameAggiornamento" runat="server" CssClass="form-control">
            </asp:DropDownList>
            <br />

            <p>Seleziona l'ID del capo che vuoi aggiornare</p>
            <%--<asp:DropDownList ID="ddlListaIDCapiUtente_Aggiornamento" runat="server" CssClass="form-control">
            </asp:DropDownList>--%>
            <asp:TextBox ID="txtIDDaAggiornare_Aggiornamento" runat="server" TextMode="Number" CssClass="form-control">
            </asp:TextBox>
            <br />

            <p>Nuovo ID da immettere nella lista</p>
            <asp:TextBox ID="txtNuovoID_Aggiornamento" runat="server" TextMode="Number" CssClass="form-control">
            </asp:TextBox>
            <br />

            <p style="color: peru">Lista ID prima dell'aggiornamento</p>
            <asp:TextBox ID="txtListaIDCapi_Aggiornamento_Prima" runat="server"
                TextMode="MultiLine" CssClass="form-control" ReadOnly="true">
            </asp:TextBox>
            <p style="color: crimson">Lista ID dopo l'aggiornamento</p>
            <asp:TextBox ID="txtListaIDCapi_Aggiornamento_Dopo" runat="server"
                TextMode="MultiLine" CssClass="form-control" ReadOnly="true">
            </asp:TextBox>
            <br />
            <%--<p>ID del capo che vuoi aggiornare</p>
            <asp:TextBox ID="txtAggiornamento" runat="server" 
                CssClass="form-control"
                TextMode="Number">
            </asp:TextBox>--%>
        </div>
        <asp:Button ID="btnAggiorna" runat="server" Text="Aggiorna"
            OnClick="btnAggiorna_Click" />
        <br />
        <asp:Literal ID="ltAggiornamento" runat="server"></asp:Literal>

        <br />
        <hr />
        <br />


        <%-- ELIMINAZIONE --%>
        <div id="eliminazione">
            <h3 style="color: aquamarine">QUERY DI ELIMINAZIONE JSON</h3>
            <p>Seleziona lo Username</p>
            <asp:DropDownList ID="ddlUsernameEliminazione" runat="server" CssClass="form-control">
            </asp:DropDownList>
            <br />

            <p>Seleziona l'ID del capo che vuoi eliminare</p>
            <asp:TextBox ID="txtID_Da_Eliminare" runat="server" TextMode="Number" CssClass="form-control">
            </asp:TextBox>
            <br />

            <br />

            <p style="color: peru">Lista ID prima dell'eliminazione</p>
            <asp:TextBox ID="txtEliminazione_Prima" runat="server"
                TextMode="MultiLine" CssClass="form-control" ReadOnly="true">
            </asp:TextBox>
            <p style="color: crimson">Lista ID dopo l'eliminazione</p>
            <asp:TextBox ID="txtEliminazione_Dopo" runat="server"
                TextMode="MultiLine" CssClass="form-control" ReadOnly="true">
            </asp:TextBox>
            <br />
        </div>
        <asp:Button ID="Elimina" runat="server" Text="Elimina"
            OnClick="btnElimina_Click" />
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


