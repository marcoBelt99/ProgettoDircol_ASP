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

        .sfondoBtnAggiungi {
            background-color: gold;
            color: antiquewhite;
        }
    </style>

    <div style="padding: 50px;"></div>


    <%-- Utilizzo del controllo FormView per visualizzare un singolo recordo da un'origine dati --%>
    <%-- RenderOuterTable = "false" --%>
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

















    <%-- VARIABILI GLOBALI --%>



    <%-- <script> 
        // Array di interi che contiene gli ID dei capi classificati della pagina
        var IDs = new Array();
        var IDs_v1 = new Array();
        var IDs_stringa = "";
    </script>--%>




    <asp:Panel ID="pndettaglioModello_1" runat="server" HorizontalAlign="Center">

        <%-- Insieme di tutti i capi che sono classificati da questo modello --%>
        <h3>Insieme di tutti i capi che sono classificati da questo modello</h3>
        <asp:ListView ID="lvCapi" runat="server"
            DataKeyNames="ID" GroupItemCount="4"
            ItemType="ProgettoDircol_ASP.Dati.Capo"
            SelectMethod="GetCapiClassificati_1">
            <EmptyDataTemplate>
                <div class="justify-content-center">
                    <div class="alert alert-info alert-alert-dismissible" role="alert">
                        <span type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></span>
                        <center>
                            <h6>Al momento non ci sono più capi disponibili per questo modello.</h6>
                            <h6>La merce ritornerà in stock il prima possibile!</h6>
                        </center>
                    </div>
                </div>
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
                    <div class="justify-content-center">
                        <div class="list-group" id="listviewCapo" runat="server">
                            <%-- ID --%>
                            <%--<%#  GetIDCapoDaAggiungere(Item.ID); %>--%>
                            <%--<%#: IDCapo_DaAggiungereAlCarrello = Item.ID %>--%>
                            <%--<a class="list-group-item list-group-item-action flex-column align-items-start btn text-white"
                                role="button" data-toggle="collapse" href="#divCollassabile<%#:Item.ID%>"
                                style="background-color: #7952b3"
                                aria-expanded="false" aria-controls="divCollassabile<%#:Item.ID%>">--%>


                            <a class="list-group-item list-group-item-action flex-column align-items-start btn text-white"
                                role="button"
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
                                <%-- ############################################################# --%>
                                <%-- PASSO COME ARGOMENTO Item.ID AL GESTORE DELL'EVENTO CLICK --%>
                                <%-- ############################################################# --%>
                                <%--<asp:LinkButton CssClass="btn btn-secondary"
                                ID="btnAggiungiAlCarrello" runat="server"
                                CommandArgument='<%#Eval("ID")%>'
                                OnClick="btnAggiungiAlCarrello_Click">
                            </asp:LinkButton>--%>
                                <asp:Button CssClass="btn IconaAggiungi sfondoBtnAggiungi"
                                    ID="LinkButton1" runat="server"
                                    CommandArgument='<%#Eval("ID")%>'
                                    OnClick="btnAggiungiAlCarrello_Click"
                                    Text="Aggiungi al carrello"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />



                    <script>
                        // PREPARO I VETTORI
                        // Mi salvo questo valore (pari all'ID i-esimo)
                        // var x = document.getElementsByClassName("OK").value = "<%--<%#:Item.ID%>--%>";
                        // Lo aggiungo all'array, così mi conservo il valore
                        // IDs.push(x);
                        // Provo ad aggiungere questo elemento anche in questo modo
                        // IDs_v1.push(<%--<%#:Item.ID %>--%>);
                        // Provo ad aggiungerlo ad una stinga che sto costruendo.
                        // Nel prossimo script, qua sotto avrò finito di costruirla
                        // IDs_stringa = IDs_stringa + <%--<%#:Item.ID.ToString() %> --%>
                    </script>
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



    <script>

        // ICONE PER IL BOTTONE AGGIUNGI AL CARRELLO
        $(".IconaAggiungi").html("<i class='bi bi-bag-plus-fill'></i> &nbsp; Aggiungi al carrello");
        $("input[type='submit']").html("<i class='bi bi-bag-plus-fill'></i> &nbsp; Aggiungi al carrello");


        // Stampo a console il vettore
        /// console.log(IDs);
        // stampo a console il vettore v1
        /// console.log(IDs_v1);
        // stampo a console la stringa costruita sopra
        /// console.log(IDs_stringa); // questa è una stringa che ho costruito in precedenza (guarda sopra nel codice)

        // Ora passo la stringa costruita prima come valore a questo campo nascosto di id='nascosto'
        // document.getElementById("nascosto").value = IDs_stringa;
        // Do al campo di id="hiddenData" il valore della stringa costruita sopra
        //document.getElementsByClassName("OK").value = IDs_stringa;
         <%-- Immetto un campo nascosto e gli assegno come valore quello del vettore  --%>
        // Ora passo la stringa costruita prima come valore a questo campo non nascosto di id='non_nascosto'
        // document.getElementById("non_nascosto").value = IDs_stringa;
        //document.getElementById('non_nascosto').value = IDs_stringa;
        //$('#non_nascosto').val(IDs_stringa);
        //$('#non_nascosto').value = IDs_stringa;
        // 
        //IDs.map(String);

        <%-- Ciclo for in Javascript che mi permette di stampare tanti campi nascosti quanti sono i capi nella pagina
         Questo mi servirà per passare il corretto IDCapo al CodeBehind --%>


        // Creo alcuni text box
        //for (var i = IDs.length; i--;) {
        //    var input = document.createElement('input'); 
        //    input.setAttribute('ID', "txtInput" + (i + 1));
        //    input.setAttribute('type', 'text');
        //    input.setAttribute('value', IDs[i]);
        //    input.setAttribute('runat', 'server');
        //    input.readOnly = true;
        //}


        // document.getElementById('nascosto').value=IDs;
        // $("#nascosto").val(IDs);
        // $("#nascosto").val(IDs_stringa);
        // campoNascosto.value = IDs.toString();

        /* $(document).ready(function () {
             $.ajax({
                 url:'dettaglioModello.aspx',
                 type: 'POST',
                 data: JSON.stringify(IDs),
                 dataType: "json",
                // contentType: 'application/json; charset=utf-8',
                 success: function (html) {
                     console.log(data);
                     alert("Inviato!");
                 },
                 error: function (request, status, error) {
                     alert(request + status + error);
                 }
             })
         })
         */
    </script>


    <%-- A scopo di debugging: --%>
    <%--<asp:Button runat="server" ID="btnMostraAlertIDCapi" CssClass="btn btn-block" OnClick="btnMostraAlertIDCapi_Click" />--%>


    <%-- Immetto un campo nascosto e gli assegno come valore quello del vettore  --%>
    <%--<asp:TextBox Text="" ID="txtDati" runat="server"></asp:TextBox>
    <input type="text" id="hope" value="<%#S%>" runat="server" />--%>
    <div style="padding: 50px;"></div>
</asp:Content>
