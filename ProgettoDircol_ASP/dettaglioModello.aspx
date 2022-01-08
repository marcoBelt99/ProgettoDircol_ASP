<%@ Page Title="Dettaglio modello" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dettaglioModello.aspx.cs" Inherits="ProgettoDircol_ASP.dettaglioModello" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- Stili che servono per la parte dei capi --%>
    <style type="text/css">
        /* Disabilito il click sui link */
        .disabilitaClick {
            pointer-events: none;
        }

        th {
            background-color: #7952b3;
            color: white;
        }

        td {
            background-color: white;
        }
    </style>


    <%-- Utilizzo del controllo FormView per visualizzare un singolo recordo da un'origine dati --%>
    <%-- RenderOuterTable = "false" --%>
    <div style="padding: 50px;"></div>
    <asp:FormView ID="fvDettagliModello" runat="server"
        ItemType="ProgettoDircol_ASP.Dati.Modello"
        SelectMethod="GetModello"
        RenderOuterTable="false">
        <ItemTemplate>
            <asp:Panel runat="server" ID="pndettaglioModello" HorizontalAlign="Center">

                <div class="row">
                    <div class="col-md-4">
                        <img src="Images/fotoModelli/<%#:Item.Immagine %>"
                            style="border: solid; align-content: center"
                            alt="<%#:Item.Nome %>" height="200" width="175" />

                    </div>

                    <div class="col-md-8">
                        <table class="table bg-white">
                            <tr>
                                <th>Nome:
                                </th>
                                <td><%#:Item.Nome %> 
                                </td>
                            </tr>
                            <tr>
                                <th>Descrizione:
                                </th>
                                <td>
                                    <%#:Item.Descrizione %>
                                </td>
                            </tr>

                            <tr>
                                <th>Genere:  
                                </th>
                                <td>
                                    <%#:Item.Genere%>
                                </td>
                            </tr>

                            <tr>
                                <th>Collezione:  
                                </th>
                                <td>
                                    <%#:Item.Collezione %>
                                </td>
                            </tr>


                            <tr>
                                <th>Prezzo:
                                </th>

                                <td>
                                    <%#: String.Format("{0:c}", Item.PrezzoListino) %>
                                </td>
                            </tr>

                            <tr>
                                <th>Modello numero:  
                                </th>
                                <td>
                                    <%#:Item.CodModello %>
                                </td>
                            </tr>

                        </table>
                    </div>


                </div>
            </asp:Panel>
        </ItemTemplate>
    </asp:FormView>
    <br />
    <br />








    <asp:Panel ID="pndettaglioModello_1" runat="server" HorizontalAlign="Center">


        <%-- Insieme di tutti i capi che sono classificati da questo modello --%>

        <h3>Insieme di tutti i capi che sono classificati da questo modello</h3>


        <asp:ListView ID="lvCapi" runat="server"
            DataKeyNames="ID" GroupItemCount="4"
            ItemType="ProgettoDircol_ASP.Dati.Capo"
            SelectMethod="GetCapiClassificati_1"
            >
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

                    <div class="list-group">
                        <%-- ID --%>
                        <%--<a class="list-group-item list-group-item-action flex-column align-items-start btn text-white"
                                role="button" data-toggle="collapse" href="#divCollassabile<%#:Item.ID%>"
                                style="background-color: #7952b3"
                                aria-expanded="false" aria-controls="divCollassabile<%#:Item.ID%>">--%>

                        <a class="list-group-item list-group-item-action flex-column align-items-start btn text-white"
                            role="button" href="#"
                            style="background-color: #7952b3">
                            <div class="d-flex w-100 justify-content-between">
                                <h5 class="mb-1">ID:&nbsp;<%#:Item.ID %></h5>
                            </div>
                            <%--<p class="mb-1"><%#:Item.ID %> </p>--%>
                        </a>

                        <%-- TAGLIA --%>
                        <a href="#" class="list-group-item list-group-item-action flex-column align-items-start disabilitaClick">
                            <div class="d-flex w-100 justify-content-between">
                                <h5 class="mb-1">Taglia</h5>
                            </div>
                        <p class="mb-1"><%#:Item.Taglia %>  </p>

                        <%-- COLORE --%>
                        <a href="#" class="list-group-item list-group-item-action flex-column align-items-start disabilitaClick">
                            <div class="d-flex w-100 justify-content-between">
                                <h5 class="mb-1">Colore</h5>
                            </div>
                            <%-- <p class="mb-1"><%#:Item.Colore %>  </p>--%>
                            <center>
                                <p class="mb-1 justify-content-center" style="text-align: center;">
                                    <center>
                                        <input type="color" value="<%#:Item.Colore %>" style="text-align: center;" disabled
                                            class="mb-1" />
                                    </center>
                                </p>
                            </center>
                            <div style="padding-bottom: 25px"></div>
                        </a>

                        <%-- PUNTO VENDITA --%>
                        <a href="#" class="list-group-item list-group-item-action flex-column align-items-start disabilitaClick">
                            <div class="d-flex w-100 justify-content-between">
                                <h5 class="mb-1">Punto Vendita</h5>
                            </div>
                            <p class="mb-1"><%#:Item.PuntoVendita %>  </p>
                        </a>
                        <div class="d-flex w-100 justify-content-between">
                            <a class="btn btn-secondary" href="/aggiungiAlCarrello.aspx?ID=<%#:Item.ID %>" role="button">
                                <i class="bi bi-cart-fill"></i>&nbsp;<b>Aggiungi al carrello<b>
                            </a>
                        </div>
                    </div>
                    <br />
                    <br />
                </td>
            </ItemTemplate>

            <LayoutTemplate>
                <table style="width: 100%;">
                    <tbody class="bg-white">
                        <tr class="bg-white">
                            <td class="bg-white">
                                <table id="groupPlaceholderContainer" runat="server" class="bg-white">
                                    <tr id="groupPlaceholder" class="bg-white"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="bg-white">
                            <td class="bg-white"></td>
                        </tr>
                        <tr class="bg-white"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemSeparatorTemplate>
            </ItemSeparatorTemplate>
        </asp:ListView>
        <%-- Fine ListView --%>
    </asp:Panel>
    <div style="padding: 50px;"></div>
</asp:Content>
