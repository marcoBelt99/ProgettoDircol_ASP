<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventiServerDifferitiaspx.aspx.cs" Inherits="ProgettoDircol_ASP.STUDIO.EventiServerDifferitiaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:CheckBox ID="chk1" Text="Fumatore" runat="server" />
            <br />
            <asp:CheckBox ID="chk2" Text="Fumatore occasionale" runat="server" />
            <br />
            <asp:CheckBox ID="chk3" Text="Non Fumatore" runat="server" />
            <br />
            <asp:TextBox ID="txt" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btn" runat="server" CommandName="btnComando" onserverclick="CheckedChanged"/>
        </div>
    </form>
</body>
</html>
<%-- Si immagini una pagina aspx contenente svariati controlli CheckBox, i quali rappresentano delle 
     opzioni di visualizzazione. Si desidera poter gestire il cambiamento di stato (checked/unchecked) 
     dei CheckBox in modo efficiente, evitando che ogni singola modifica dell’utente si traduca in un 
     postback. In sostanza, i cambiamenti di stato devono essere registrati, ma gestiti in un unica 
     soluzione al successivo postback della pagina, prodotto da un bottone. 
     È questa la modalità di gestione predefinita, caratterizzata dal valore false nella proprietà 
     AutoPostBack. La pagina di esempio EventiServerDifferiti.aspx, contenente un TextBox, tre 
     CheckBox e un Button necessario per produrre il postback, mostra questo meccanismo settando il 
     ForeColor dei controlli che hanno subito un cambiamento di testo. Ciò viene fatto gestendo 
     l’evento TextChanged del TextBox e CheckChanged dei CheckBox, in quest’ultimo caso con un 
     solo gestore associato ai tre controlli. La gestione dell’evento Page_Load serve a resettare ogni 
     volta il colore allo stato originale
--%>