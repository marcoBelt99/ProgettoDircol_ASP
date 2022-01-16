<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FeedbackProva.aspx.cs" Inherits="ProgettoDircol_ASP.STUDIO.FeedbackProva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script
        src="https://code.jquery.com/jquery-3.6.0.js"
        integrity="sha256-H+K7U5CnXl1h5ywQfKtSj8PCmoN9aaq30gDh27Xc0jk="
        crossorigin="anonymous"></script>
    <%-- PROVA CRUD IN AJAX: RECENSIONE --%>
    <table <%--border="0" cellpadding="0" cellspacing="0"--%>>
        <tr>
            <td>Descrizione recensione:  
            </td>
            <td>
                <asp:TextBox ID="txtDescrizioneRecensione" runat="server" Text="" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td>Grado di soddisfazione (punteggio):  
            </td>
            <td>
                <asp:TextBox ID="txtPunteggio" runat="server" Text="" TextMode="Number" />
            </td>
        </tr>
        <tr>
            <td>Modello di riferimento:  
            </td>
            <td>
                <asp:TextBox ID="txtModello" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Username utente:  
            </td>
            <td>
                <asp:TextBox ID="txtUsernameUtente" runat="server" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <%--<asp:Button ID="btnSave" Text="Inserisci recensione" runat="server" />
                <asp:Button ID="btnUpdate" Text="Modifica recensione" runat="server" />
                <asp:Button ID="btnDelete" Text="Elimina recensione" runat="server" />--%>
                <input type="button" id="btnSave" value="Inserisci recensione" />
                <input type="button" id="btnUpdate" value="Modificarecensione" />
                <input type="button" id="btnDelete" value="Elimina recensione" />
            </td>
        </tr>
    </table>
    <hr />
    <asp:GridView ID="gvRecensioni" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#3AC0F2"
        HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2">
        <Columns>
            <asp:BoundField DataField="Descrizione" HeaderText="Descrizione" />
            <asp:BoundField DataField="Punteggio" HeaderText="Punteggio" />
            <asp:BoundField DataField="Modello" HeaderText="Modello" />
            <asp:BoundField DataField="UsernameUtente" HeaderText="UsernameUtente" />
        </Columns>
    </asp:GridView>

    <%-- PARTE AJAX --%>
    <script type="text/javascript">  

        // Chiamata per l'inserimento
        $(document).ready(function () {
            $(document).on("click", "[id*=btnSave]", function () {
                var Descrizione = $("[id*=txtDescrizioneRecensione]");
                var Punteggio = $("[id*=txtPunteggio]");
                var Modello = $("[id*=txtModello]");
                var UsernameUtente = $("[id*=txtUsernameUtente]");
                // Prove a console
                console.log(Descrizione.val())
                console.log(Punteggio.val())
                console.log(Modello.val())
                console.log(UsernameUtente.val())
                // Chiamata AJAX
                // data: '{DescrizioneRecensione:"' + Descrizione.val() + '",Punteggio:"' + Punteggio.val() + '",CodModello:"' + Modello.val() + '",UsernameUtente:"' + UsernameUtente.val() + '"}'
                $.ajax({
                    type: "POST",
                    url: "FeedbackProva.aspx/AggiungiRecensione",
                    data: {
                        DescrizioneRecensione: Descrizione.val(),
                        Punteggio: Punteggio.val(),
                        CodModello: Modello.val(),
                        UsernameUtente: UsernameUtente.val()
                    },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    error: errore,
                    success: function (response) {
                        alert("Recensione aggiunta correttamente.");
                        // window.location.reload();
                    }
                });
                function errore() {
                    alert("Errore nella chiamata AJAX");
                }
                return false;
            });
        })
    </script>

    <script type="text/javascript"> 
        // Chiamata per l'aggiornamento
        $(document).ready(function () {
            $(document).on("click", "[id*=btnUpdate]", function () {
                var ID = $("[id*=txtID]");
                var Username = $("[id*=txtUsername]");
                var Password = $("[id*=txtPassword]");
                $.ajax({
                    type: "POST",
                    url: "crud_ajax.aspx/UpdateUser",
                    data: '{Username:"' + Username.val() + '",Password:"' + Password.val() + '",id:"' + ID.val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        alert("User has been Update successfully.");
                        window.location.reload();
                    }
                });
                return false;
            });
        });
    </script>
    <script type="text/javascript">  
        $(document).ready(function () {
            $(document).on("click", "[id*=btnDelete]", function () {
                var ID = $("[id*=txtID]");
                $.ajax({
                    type: "POST",
                    url: "crud_ajax.aspx/DeleteUser",
                    data: '{deleteUserID:"' + ID.val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        alert("User has been Delete successfully.");
                        window.location.reload();
                    }
                });
                return false;
            });
        });
    </script>
</asp:Content>
