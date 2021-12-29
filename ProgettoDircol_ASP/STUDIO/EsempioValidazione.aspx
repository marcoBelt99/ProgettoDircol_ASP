<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EsempioValidazione.aspx.cs" Inherits="ProgettoDircol_ASP.STUDIO.EsempioValidazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- WEB SERVER CONTROL VALIDATION --%>
    <h1>Esempio Validazione</h1>
    <%-- Creo alcuni campi di input con alcuni Controlli Server
         Inoltre, metto anche i relativi Controlli di Validazione.
        Sono presenti molti attributi:
        * 'ValidationGroup'--> Deve esserci in tutti i controlli (e sul bottone anche)
        * 'Type' -->
        * 'MinimumValue', 'MaximimValue' --> Per il RangeValidator
        * 'Visible' --> true o false, serve per rendere visibile o meno il controllo
        * 'Display' --> In che modo presento il Controllo
        * ''
        * ''
        * ''
    --%>
    <div class="form-group">
        <div class="form-text">
            *Campi marcati con un asterisco sono obbligatori.
        </div>
    </div>
    <p class="bg-primary">
        <%-- Controllo Literal --%>
        <asp:Literal ID="ltMessaggio" runat="server" />
        <%-- ValidationSummary --%>
        <asp:ValidationSummary ID="valSummaryForm" runat="server"
            CssClass="bg-danger" ValidationGroup="valForm"
            DisplayMode="BulletList" HeaderText="Per favore, sistema i seguenti errori:"
            Visible="false" />
    </p>
    <%-- Nominativo --%>
    <div class="form-group">
        <label>*Nome e Cognome:</label>
        <asp:TextBox ID="txtNominativo" CssClass="form-control" runat="server" />
        <asp:RequiredFieldValidator ID="rfvNominativo" runat="server"
            ControlToValidate="txtNominativo" ValidationGroup="valForm"
            CssClass="bg-danger" ErrorMessage="*Il nominativo è obbligatorio."
            Display="Dynamic" />
    </div>
    <%-- Username (assumo non sia obbligatorio) --%>
    <div class="form-group">
        <label>Username:</label>
        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
    </div>
    <%-- Età --%>
    <div class="form-group">
        <label>*La tua età:</label>
        <asp:TextBox ID="txtEta" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvEta" runat="server"
            ControlToValidate="txtEta" ValidationGroup="valForm"
            CssClass="bg-danger" ErrorMessage="*L'età è obbligatoria." />
        <asp:RangeValidator ID="rvEta" runat="server"
            ControlToValidate="txtEta" ValidationGroup="valForm"
            CssClass="bg-danger" Type="Integer" MinimumValue="4"
            MaximumValue="120" ErrorMessage="*Trovo difficile credere nella tua età. Per favore, inserisci la tua reale età."
            Display="Dynamic" />
    </div>
    <%-- Email --%>
    <div class="form-group">
        <label>*Email:</label>
        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
            ControlToValidate="txtEmail" ValidationGroup="valForm"
            CssClass="bg-danger" ErrorMessage="*L'email è obbligatoria." />
    </div>
    <%-- Prezzo --%>
    <div class="form-group">
        <label>*Prezzo (in €):</label>
        <asp:TextBox ID="txtPrezzo" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvPrezzo" runat="server"
            ControlToValidate="txtPrezzo" ValidationGroup="valForm"
            CssClass="bg-danger" ErrorMessage="*Il Prezzo è obbligatorio." />
        <asp:CompareValidator ID="cvPrezzo" runat="server"
            ControlToValidate="txtPrezzo" ValidationGroup="valForm"
            CssClass="bg-danger" Operator="DataTypeCheck"
            Type="Currency" ErrorMessage="Per favore, selezionala un prezzo valido"
            Display="Dynamic" />
    </div>
    <%-- Button invia --%>
    <div class="form-group">
        <asp:Button ID="btnSubmit" CssClass="btn btn-success" Text="Invia"
            CausesValidation="true" runat="server" ValidationGroup="valForm"
            OnClick="btnSubmit_Click" />
    </div>

</asp:Content>

