<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginHtmlControl.aspx.cs" Inherits="ProgettoDircol_ASP.STUDIO.LoginHtmlControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>

<body runat="server" id="body">
    <form id="form1" runat="server">
        <div>
            <input id="txtCognome" type="text" runat="server" />
            <br />
            <input id="txtNome" type="text" runat="server" />
            <br />
            <br />
            <input id="btnRegistra" type="submit" value="Login" runat="server" onserverclick="btnRegistra_Click" />
            <hr />
            <label id="lblConferma" runat="server"></label>
        </div>
    </form>
</body>
</html>

<%-- OnServerClick. Ad un bottone di tipo server (attributo runat) è possibile associare sia l’evento 
     click lato client, attraverso OnClick, che quello lato server, come nell’esempio. Il primo è intrinseco 
     del tag HTML e può essere gestito mediante uno script lato client, il secondo è gestito da ASP.NET. 
     Interessante è inoltre la gestione del tag <label>; per esso, come per molti altri tag, non esiste un 
     Html Control corrispondente. In questo caso, ASP.NET traduce il tag in un HtmlGenericControl.  
     Ovviamente, essendo un controllo generico, possiede un’interfaccia di programmazione essenziale, 
     nella quale spicca la proprietà InnerHtml, che consente di impostare il testo HTML contenuto tra il 
     tag di apertura e di chiusura.
--%>

<%-- 
     
--%>

