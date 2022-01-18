<%@ Page Title="Gestione Modelli" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gestioneModelli.aspx.cs" Inherits="ProgettoDircol_ASP.gestioneModelli" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- PAGINA DI GESTIONE DEI MODELLI --%>
    <%-- Utilizzo di un controllo List View --%>
    <section class="bg-white">
        <center>
            <h2>Modelli dei capi d'abbigliamento disponibili</h2>
        </center>


        <%-- LIST VIEW--%>
        <asp:ListView ID="lvModelli" runat="server"
            DataKeyNames="CodModello" GroupItemCount="4"
            ItemType="ProgettoDircol_ASP.Dati.Modello"
            SelectMethod="GetModelliDisponibili_2">
            <EmptyDataTemplate>
                <table class="bg-white">
                    <tr class="bg-white">
                        <td class="bg-white">Nessun dato è stato ritornato.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <EmptyItemTemplate>
                <td />
            </EmptyItemTemplate>
            <GroupTemplate>
                <tr id="itemPlaceholderContainer" runat="server" class="bg-white">
                    <td id="itemPlaceholder" runat="server" class="bg-white"></td>
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <td runat="server" class="bg-white">
                    <table class="bg-white">
                        <tr class="bg-white">
                            <td class="bg-white">
                                <a href="dettaglioModello.aspx?codModello=<%#:Item.CodModello%>">
                                    <img src="Images/fotoModelli/<%#:Item.Immagine%>"
                                        width="130" height="100" style="border: solid #7952b3;" /></a>
                            </td>
                        </tr>
                        <tr class="bg-white">
                            <td class="bg-white">
                                <a class="btn stretched-link text-light" style="background-color:#7952b3" href="dettaglioModello.aspx?codModello=<%#:Item.CodModello%>">
                                    <span>
                                        <%#:Item.Nome%>
                                    </span>
                                </a>
                                <br />
                                <br />
                                <span class="bg-light text-dark">Prezzo di listino: <span class="bg-warning text-text-white"><%#:String.Format("{0:c}", Item.PrezzoListino)%>
                                </span>
                                </span>
                                <br />
                            </td>
                        </tr>
                        <tr class="bg-white">
                            <td class="bg-white">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </ItemTemplate>
            <LayoutTemplate>
                <table style="width: 100%;">
                    <tbody class="bg-white">
                        <tr class="bg-white">
                            <td class="bg-white">
                                <table id="groupPlaceholderContainer" runat="server" style="width: 100%">
                                    <tr id="groupPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg-white"></td>
                        </tr>
                        <tr></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
        </asp:ListView>
        <%-- Fine ListView --%>
        <br />
        <center>
            <p class="text-info alert-info alert-dismissable alert">Modelli che classificano almeno un capo d'abbigliamento</p>
        </center>
    </section>
    <div style="padding: 50px;"></div>
</asp:Content>
