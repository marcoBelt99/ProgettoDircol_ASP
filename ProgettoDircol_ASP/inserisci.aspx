<%@ Page Title="Inserimento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="inserisci.aspx.cs" Inherits="ProgettoDircol_ASP.inserisci" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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


    <header>
        <%-- JQuery --%>
        <script src="https://code.jquery.com/jquery-3.6.0.js"></script>

        <%-- JQUERY UI --%>
        <link rel="stylesheet" href="jquery-ui/jquery-ui.min.css">
        <link rel="stylesheet" href="jquery-ui/jquery-ui.theme.min.css">
        <script src="jquery-ui/jquery-ui.min.js"></script>


        <%-- IMPORT IL FONT CHE VOGLIO IO --%>
        <style>
            @import url("https://fonts.googleapis.com/css2?family=Poppins:wght@700&display=swap");
        </style>


         <%--   --%>
    </header>

    <asp:Panel ID="pnInserimento" runat="server"
        HorizontalAlign="Center">
        <%-- ############################## --%>
        <%-- ########### CAPI ############# --%>
        <%-- ############################## --%>
        <h2>Inserimento di un nuovo capo d'abbigliamento</h2>
        <div class="d-flex justify-content-center">
            <fieldset runat="server" id="fsCapi">
                <div class="form-group">
                    <%-- TAGLIA --%>
                    <p>
                        <asp:Label Text="Seleziona una taglia:" runat="server" ID="lblTaglia"
                            Style="float: left;" />
                        &emsp;
                        <asp:DropDownList ID="ddlTaglia" runat="server"
                            Style="float: right;">
                            <asp:ListItem Value="---" EnableViewState="true" />
                        </asp:DropDownList>
                        <%-- Validazione input --%>
                        <asp:RequiredFieldValidator
                            runat="server" ID="rfvTaglia"
                            ControlToValidate="ddlTaglia"
                            ErrorMessage="Obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="capiForm" />
                        <asp:CompareValidator
                            runat="server" ID="compareValidatorTaglia"
                            ControlToValidate="ddlTaglia"
                            ValueToCompare="---"
                            Operator="NotEqual"
                            ErrorMessage="Seleziona una taglia valida."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="capiForm">
                        </asp:CompareValidator>
                        <%-- validatore custom--%>
                        <asp:CustomValidator
                            ID="customValidatorTaglia" runat="server"
                            OnServerValidate="cvTaglia_ServerValidate"
                            ControlToValidate="ddlTaglia"
                            ErrorMessage="Selezionare una taglia valida."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="capiForm">
                        </asp:CustomValidator>
                    </p>
                    <%-- COLORE --%>
                    <p>
                        <asp:Label Text="Colore del nuovo capo:" runat="server" ID="lblColore"
                            Style="float: left;" />
                        &emsp;
                        <input type="color" id="colorInput" runat="server"
                            style="float: right;" />
                        <%-- validatori --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvColore"
                            ControlToValidate="colorInput"
                            ErrorMessage="Obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="capiForm" />
                    </p>
                    <%-- PUNTO VENDITA --%>
                    <p>
                        <asp:Label Text="Punto vendita di interesse:" runat="server" ID="lblPuntoVendita"
                            Style="float: left" />
                        &emsp;
                        <asp:DropDownList ID="ddlPuntoVendita" runat="server"
                            Style="float: right;">
                            <asp:ListItem Value="---" />
                        </asp:DropDownList>
                        <%-- Validazione input --%>
                        <asp:RequiredFieldValidator
                            ID="rfvPuntoVendita" runat="server"
                            ControlToValidate="ddlPuntoVendita"
                            ErrorMessage="Seleziona un punto vendita."
                            Display="Dynamic"
                            ValidationGroup="capiForm" />
                        <asp:CompareValidator
                            runat="server" ID="compareValidatorPuntoVendita"
                            ControlToValidate="ddlPuntoVendita"
                            ValueToCompare="---"
                            Operator="NotEqual"
                            ErrorMessage="Seleziona un punto vendita valido."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="capiForm">
                        </asp:CompareValidator>
                        <%-- validatore custom --%>
                        <asp:CustomValidator
                            ID="cvPuntoVendita" runat="server"
                            OnServerValidate="cvPuntoVendita_ServerValidate"
                            ControlToValidate="ddlTaglia"
                            ErrorMessage="Selezionare una taglia valida."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="capiForm">
                        </asp:CustomValidator>
                    </p>
                    <%-- CODICE MODELLO --%>
                    <p>
                        <asp:Label Text="Codice modello del capo:" runat="server" ID="lblCodiceModello"
                            Style="float: left;" />
                        &emsp;
                        <asp:DropDownList ID="ddlCodiceModello" runat="server"
                            Style="float: right;">
                            <asp:ListItem Value="---" />
                        </asp:DropDownList>
                        <%-- Validazione input --%>
                        <asp:RequiredFieldValidator
                            ID="rfvCodiceModello" runat="server"
                            ControlToValidate="ddlCodiceModello"
                            ErrorMessage="Seleziona un codice di modello."
                            Display="Dynamic"
                            ValidationGroup="capiForm" />
                        <asp:CompareValidator
                            runat="server" ID="compareValidatorCodiceModello"
                            ControlToValidate="ddlCodiceModello"
                            ValueToCompare="---"
                            Operator="NotEqual"
                            ErrorMessage="Seleziona un codice di modello."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="capiForm">
                        </asp:CompareValidator>
                        <%-- validatore custom --%>
                        <asp:CustomValidator
                            ID="cvCodiceModello" runat="server"
                            OnServerValidate="cvCodiceModello_ServerValidate"
                            ControlToValidate="ddlTaglia"
                            ErrorMessage="Selezionare codice di modello valido."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="capiForm">
                        </asp:CustomValidator>
                    </p>
                    <br />
                    <p>
                        <asp:Button ID="btnInserisciCapo" Text="Inserisci" runat="server"
                            OnClick="btnInserisciCapo_Click"
                            CssClass="Stile_btnSubmit"
                            ValidationGroup="capiForm" />
                        <asp:Button ID="btnAnnullaCapo" Text="Annulla" runat="server"
                            OnClick="btnAnnullaCapo_Click"
                            CssClass="Stile_bntAnnulla" />
                    </p>

                </div>
            </fieldset>
        </div>







        <%-- ################################# --%>
        <%-- ########### MODELLI ############# --%>
        <%-- ################################# --%>
        <h2>Inserimento di un nuovo modello di capo d'abbigliamento</h2>
        <div class="d-flex justify-content-center">
            <fieldset runat="server" id="Fieldset1">
                <div class="form-group">

                    <%-- IMMAGINE --%>
                    <div class="custom-file">
                        <p>
                            <asp:Label Text="Immagine del modello:" runat="server" ID="lblImmagine" />
                            <asp:FileUpload ID="fileUploadImmagine" runat="server" />
                            <%-- Validazione --%>
                            <asp:RequiredFieldValidator runat="server" ID="rfvImmagine"
                                ControlToValidate="fileUploadImmagine"
                                ErrorMessage="Campo obbligatorio"
                                Display="Dynamic"
                                ValidationGroup="modelliForm" />
                        </p>
                    </div>

                    <%-- NOME MODELLO --%>
                    <p>
                        <asp:Label Text="Nome del modello:" runat="server" ID="lblNomeModello" />
                        <asp:TextBox runat="server" ID="txtNomeModello">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvNomeModello"
                            ControlToValidate="txtNomeModello"
                            ErrorMessage="Il nome è obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="modelliForm" />
                    </p>

                    <%-- DESCRIZIONE MODELLO (abilito TextMode="MultiLine" per creare una textarea --%>
                    <p>
                        <asp:Label Text="Descrizione del modello:" runat="server" ID="lblDescrizioneModello" />
                    </p>
                    <p>
                        <asp:TextBox ID="txtDescrizioneModello" runat="server"
                            TextMode="MultiLine">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvDescrizioneModello"
                            ControlToValidate="ddlPuntoVendita"
                            ErrorMessage="Obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="modelliForm" />
                    </p>

                    <%-- PREZZO DI LISTINO (con il type="number" rendo il campo numerico; oppure con TextMode) --%>
                    <p>
                        <asp:Label Text="Prezzo di listino:" runat="server" ID="lblPrezzoListino" />
                        <asp:TextBox ID="txtPrezzoDiListino" runat="server"
                            TextMode="Number">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvPrezzoDiListino"
                            ControlToValidate="txtPrezzoDiListino"
                            ErrorMessage="Campo obbligatoio"
                            Display="Dynamic"
                            ValidationGroup="modelliForm" />
                    </p>

                    <%-- GENERE --%>
                    <p>
                        <asp:Label Text="Genere (M/F):" runat="server" ID="lblGenere" />
                        <asp:TextBox ID="txtGenere" runat="server">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvGenere"
                            ControlToValidate="txtGenere"
                            ErrorMessage="Campo obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="modelliForm" />
                    </p>

                    <%-- COLLEZIONE --%>
                    <p>
                        <asp:Label Text="Collezione:" runat="server" ID="lblCollezione" />
                        <asp:TextBox ID="txtCollezione" runat="server">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvCollezione"
                            ControlToValidate="txtCollezione"
                            ErrorMessage="Obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="modelliForm" />
                    </p>

                    <br />
                    <asp:Button ID="btnInserisciModello"
                        ValidationGroup="modelliForm" Text="Inserisci" runat="server"
                        CssClass="Stile_btnSubmit"
                        OnClick="btnInserisciModello_Click" />
                    <asp:Button ID="btnAnnullaModello" Text="Annulla" runat="server"
                        OnClick="btnAnnullaModello_Click"
                        CssClass="Stile_bntAnnulla" />
                </div>
            </fieldset>
        </div>











        <%-- ################################# --%>
        <%-- ########### PUNTIVENDITA ############# --%>
        <%-- ################################# --%>
        <h2>Inserimento di una nuova filiale (punto vendita)</h2>
        <div class="d-flex justify-content-center">
            <fieldset runat="server" id="Fieldset2">
                <div class="form-group">

                    <%-- INDIRIZZO --%>
                    <div class="custom-file">
                        <p>
                            <asp:Label Text="Indirizzo del nuovo Punto Vendita:" runat="server" ID="lblIndirizzo" />
                            <asp:TextBox ID="txtIndirizzo" runat="server" />
                            <%-- Validazione --%>
                            <asp:RequiredFieldValidator runat="server" ID="rfvIndirizzo"
                                ControlToValidate="txtIndirizzo"
                                ErrorMessage="Campo obbligatorio"
                                Display="Dynamic"
                                ValidationGroup="puntivenditaForm" />
                        </p>
                    </div>

                    <%-- TELEFONO. Mettere tipo= telefono --%>
                    <p>
                        <asp:Label Text="Telefono del nuov Punto Vendita:" runat="server" ID="lblTelefono" />
                        <asp:TextBox runat="server" ID="txtTelefono">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvTelefono"
                            ControlToValidate="txtTelefono"
                            ErrorMessage="Il telefono è obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="puntivenditaForm" />
                    </p>

                    <%-- CITTA' --%>
                    <p>
                        <asp:Label Text="Città del nuovo Punto Vendita:" runat="server" ID="lblCitta" />
                        <asp:TextBox ID="txtCitta" runat="server">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvCitta"
                            ControlToValidate="txtCitta"
                            ErrorMessage="Obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="puntivenditaForm" />
                    </p>

                    <%-- DATA INIZIO: TextBox con TextMode Date --%>
                    <p>
                        <asp:Label Text="Data di inizio attività:" runat="server" ID="lblDataInizio" />
                        <asp:TextBox ID="txtDataInizio" runat="server"
                            TextMode="Date">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvDataInizio"
                            ControlToValidate="txtDataInizio"
                            ErrorMessage="Campo obbligatoio"
                            Display="Dynamic"
                            ValidationGroup="puntivenditaForm" />
                    </p>

                    <%-- NAZIONE --%>
                    <p>
                        <asp:Label Text="Nazione:" runat="server" ID="lblNazione" />
                        <asp:TextBox ID="txtNazione" runat="server">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvNazione"
                            ControlToValidate="txtNazione"
                            ErrorMessage="Campo obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="puntivenditaForm" />
                    </p>

                    <br />
                    <asp:Button ID="btnInserisciPuntoVendita"
                        ValidationGroup="puntivenditaForm" Text="Inserisci" runat="server"
                        CssClass="Stile_btnSubmit"
                        OnClick="btnInserisciPuntoVendita_Click" />

                    <asp:Button ID="btnAnnullaPuntoVendita" Text="Annulla" runat="server"
                        OnClick="btnAnnullaPuntoVendita_Click"
                        CssClass="Stile_bntAnnulla" />
                </div>
            </fieldset>
        </div>











        <%-- ################################# --%>
        <%-- ########### DIPENDENTI ############# --%>
        <%-- ################################# --%>
        <h2>Inserimento di un nuovo Dipendente</h2>
        <div class="d-flex justify-content-center">
            <fieldset runat="server" id="Fieldset3">
                <div class="form-group">

                    <%-- MATRICOLA --%>
                    <div class="custom-file">
                        <p>
                            <asp:Label Text="Matricola:" runat="server" ID="lblMatricola" />
                            <asp:TextBox ID="txtMatricola" runat="server" />
                            <%-- Validazione--%>
                            <asp:RequiredFieldValidator runat="server" ID="rfvMatricola"
                                ControlToValidate="txtMatricola"
                                ErrorMessage="Campo obbligatorio"
                                Display="Dynamic"
                                ValidationGroup="dipendentiForm" />
                            <%-- validatore custom. . Usare:    --%>
                            <asp:CustomValidator
                                ID="cvMatricolaDipendente"
                                ControlToValidate="txtMatricola"
                                runat="server"
                                ErrorMessage="Matricola già esistente"
                                OnServerValidate="cvMatricolaDipendente_ServerValidate" />
                        </p>
                    </div>

                    <%-- COGNOME --%>
                    <p>
                        <asp:Label Text="Cognome:" runat="server" ID="lblCognome" />
                        <asp:TextBox runat="server" ID="txtCognome">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvCognome"
                            ControlToValidate="txtCognome"
                            ErrorMessage="Il cognome è obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="dipendentiForm" />
                    </p>

                    <%-- NOME' --%>
                    <p>
                        <asp:Label Text="Nome:" runat="server" ID="lblNomeDipendente" />
                        <asp:TextBox ID="txtNomeDipendente" runat="server">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvNomeDipendente"
                            ControlToValidate="txtNomeDipendente"
                            ErrorMessage="Obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="dipendentiForm" />
                    </p>

                    <%-- CODICE FISCALE --%>
                    <p>
                        <asp:Label Text="Codice Fiscale:" runat="server" ID="lblCodiceFiscale" />
                        <asp:TextBox ID="txtCodiceFiscale" runat="server">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvCodiceFiscale"
                            ControlToValidate="txtCodiceFiscale"
                            ErrorMessage="Campo obbligatoio"
                            Display="Dynamic"
                            ValidationGroup="dipendentiForm" />
                    </p>

                    <%-- QUALIFICA --%>
                    <p>
                        <asp:Label Text="Qualifica:" runat="server" ID="lblQualifica" />
                        <asp:TextBox ID="txtQualifica" runat="server">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvQualifica"
                            ControlToValidate="txtQualifica"
                            ErrorMessage="Campo obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="dipendentiForm" />
                    </p>
                    <%-- PUNTO VENDITA: ddlPuntoVendita_Dipendenti --%>
                    <p>
                        <asp:Label Text="Punto vendita:" runat="server" ID="lblPuntoVendita_Dipendenti" />
                        <asp:DropDownList ID="ddlPuntoVendita_Dipendenti" runat="server"
                            ValidationGroup="dipendentiForm">
                            <asp:ListItem Value="---" />
                        </asp:DropDownList>
                        <%-- Validazione input --%>
                        <asp:RequiredFieldValidator
                            ID="rfvPuntoVendita_Dipendenti" runat="server"
                            ControlToValidate="ddlPuntoVendita_Dipendenti"
                            ErrorMessage="Seleziona un punto vendita."
                            Display="Dynamic"
                            ValidationGroup="dipendentiForm" />
                        <asp:CompareValidator
                            runat="server" ID="compareValidatorPuntoVendita_Dipendenti"
                            ControlToValidate="ddlPuntoVendita_Dipendenti"
                            ValueToCompare="---"
                            Operator="NotEqual"
                            ErrorMessage="Seleziona un punto vendita valido."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="dipendentiForm">
                        </asp:CompareValidator>
                        <%-- validatore custom --%>
                        <asp:CustomValidator
                            ID="cvPuntoVendita_Dipendenti" runat="server"
                            OnServerValidate="cvPuntoVendita_Dipendenti_ServerValidate"
                            ControlToValidate="ddlPuntoVendita_Dipendenti"
                            ErrorMessage="Selezionare una taglia valida."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="dipendentiForm">
                        </asp:CustomValidator>
                    </p>

                    <br />
                    <asp:Button ID="btnInserisciDipendente"
                        ValidationGroup="dipendentiForm" Text="Inserisci" runat="server"
                        CssClass="Stile_btnSubmit"
                        OnClick="btnInserisciDipendente_Click" />

                    <asp:Button ID="btnAnnullaDipendente" Text="Annulla" runat="server"
                        OnClick="btnAnnullaDipendente_Click"
                        CssClass="Stile_bntAnnulla" />
                </div>
            </fieldset>
        </div>















        <%-- ################################# --%>
        <%-- ########### TRANSAZIONI ############# --%>
        <%-- ################################# --%>
        <h2>Registrazione di una nuova transazione</h2>
        <div class="d-flex justify-content-center">
            <fieldset runat="server" id="Fieldset4">
                <div class="form-group">

                    <%-- DATA DELLA TRANSAZIONE --%>
                    <p>
                        <asp:Label Text="Data della transaizone:" runat="server" ID="lblDataTransazione" />
                        <asp:TextBox ID="txtDataTransazione" runat="server"
                            TextMode="Date" />
                        <%-- Validazione--%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvDataTransazione"
                            ControlToValidate="txtDataTransazione"
                            ErrorMessage="Campo obbligatorio"
                            Display="Dynamic"
                            ValidationGroup="transazioniform" />
                    </p>


                    <%-- PREZZO TRANSAZIONE --%>
                    <p>
                        <asp:Label Text="Prezzo della transazione:" runat="server" ID="lblPrezzoTransazione" />
                        <asp:TextBox runat="server" ID="txtPrezzoTransazione"
                            TextMode="Number">
                        </asp:TextBox>
                        <%-- Validazione --%>
                        <asp:RequiredFieldValidator runat="server" ID="rfvPrezzoTransazione"
                            ControlToValidate="txtPrezzoTransazione"
                            ErrorMessage="Il prezzo della transazione è obbligatorio."
                            Display="Dynamic"
                            ValidationGroup="transazioniForm" />
                    </p>

                    <%-- MATRICOLA: ddlMatricola_Transazioni --%>
                    <%-- NOTA BENE: vedere se lasciare la scelta della matricola di vendita o se assegnare il venditore in
                        modo random dall'elenco dei dipendenti --%>
                    <p>
                        <asp:Label Text="Matricola agente di vendita transazione:" runat="server" ID="lblMatricola_Transazioni" />
                        <asp:DropDownList ID="ddlMatricola_Transazioni" runat="server"
                            ValidationGroup="transazioniForm">
                            <asp:ListItem Value="---" />
                        </asp:DropDownList>
                        <%-- Validazione input --%>
                        <asp:RequiredFieldValidator
                            ID="rfvMatricola_Transazioni" runat="server"
                            ControlToValidate="ddlMatricola_Transazioni"
                            ErrorMessage="Seleziona una matricola."
                            Display="Dynamic"
                            ValidationGroup="transazioniForm" />
                        <asp:CompareValidator
                            runat="server" ID="compareValidatorMatricola_Transazioni"
                            ControlToValidate="ddlMatricola_Transazioni"
                            ValueToCompare="---"
                            Operator="NotEqual"
                            ErrorMessage="Seleziona una matricola valida."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="transazioniForm">
                        </asp:CompareValidator>
                        <%-- validatore custom --%>
                        <asp:CustomValidator
                            ID="cvMatricola_Transazioni" runat="server"
                            OnServerValidate="cvMatricola_Transazioni_ServerValidate"
                            ControlToValidate="ddlMatricola_Transazioni"
                            ErrorMessage="Selezionare una matricola valida."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="transazioniForm">
                        </asp:CustomValidator>
                    </p>

                    <%-- ID CAPO: ddlIDCapo_Transazioni --%>
                    <p>
                        <asp:Label Text="ID capo:" runat="server" ID="lblIDCapo_Transazione" />
                        <asp:DropDownList ID="ddlIDCapo_Transazioni" runat="server"
                            ValidationGroup="transazioniForm">
                            <asp:ListItem Value="---" />
                        </asp:DropDownList>
                        <%-- Validazione input --%>
                        <asp:RequiredFieldValidator
                            ID="rfvddlIDCapo_Transazioni" runat="server"
                            ControlToValidate="ddlIDCapo_Transazioni"
                            ErrorMessage="Seleziona un ID capo valido."
                            Display="Dynamic"
                            ValidationGroup="transazioniForm" />
                        <asp:CompareValidator
                            runat="server" ID="compareValidatorddlIDCapo_Transazioni"
                            ControlToValidate="ddlIDCapo_Transazioni"
                            ValueToCompare="---"
                            Operator="NotEqual"
                            ErrorMessage="Seleziona un punto vendita valido."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="transazioniForm">
                        </asp:CompareValidator>
                        <%-- validatore custom --%>
                        <asp:CustomValidator
                            ID="cvddlIDCapo_Transazioni" runat="server"
                            OnServerValidate="cvddlIDCapo_Transazioni_ServerValidate"
                            ControlToValidate="ddlIDCapo_Transazioni"
                            ErrorMessage="Selezionare una taglia valida."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="transazioniForm">
                        </asp:CustomValidator>
                    </p>


                    <%-- USERNAME UTENTE: ddlUsernameUtente_Transazioni --%>
                    <p>
                        <asp:Label Text="Username dell'utente che intende acquistare:" runat="server" ID="lblUsernameUtente_Transazioni" />
                        <asp:DropDownList ID="ddlUsernameUtente_Transazioni" runat="server"
                            ValidationGroup="transazioniForm">
                            <asp:ListItem Value="---" />
                        </asp:DropDownList>
                        <%-- Validazione input --%>
                        <asp:RequiredFieldValidator
                            ID="rfvUsernameUtente_Transazioni" runat="server"
                            ControlToValidate="ddlUsernameUtente_Transazioni"
                            ErrorMessage="Seleziona uno username."
                            Display="Dynamic"
                            ValidationGroup="transazioniForm" />
                        <asp:CompareValidator
                            runat="server" ID="cvUsernameUtente_Transazioni"
                            ControlToValidate="ddlUsernameUtente_Transazioni"
                            ValueToCompare="---"
                            Operator="NotEqual"
                            ErrorMessage="Seleziona uno username valido."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="transazioniForm">
                        </asp:CompareValidator>
                        <%-- validatore custom --%>
                        <%--<asp:CustomValidator
                            ID="CustomValidator1" runat="server"
                            OnServerValidate="cvMatricola_Transazioni_ServerValidate"
                            ControlToValidate="ddlMatricola_Transazioni"
                            ErrorMessage="Selezionare una matricola valida."
                            CssClass="alert alert-warning alert-dismissable text-sm-left"
                            Display="Dynamic"
                            ValidationGroup="transazioniForm">
                        </asp:CustomValidator>--%>
                    </p>








                    <br />
                    <asp:Button ID="btnInserisciTransazione"
                        ValidationGroup="transazioniForm" Text="Inserisci" runat="server"
                        CssClass="Stile_btnSubmit"
                        OnClick="btnInserisciTransazione_Click" />

                    <asp:Button ID="btnAnnullaTransazione" Text="Annulla" runat="server"
                        OnClick="btnAnnullaTransazione_Click"
                        CssClass="Stile_bntAnnulla" />
                </div>
            </fieldset>
        </div>
    </asp:Panel>

    <div id="UserLoggedinMessage" style="float: right; width: 280px;">

        <!-- removed width of 350, 350 inside div of 280 doesn't make sense -->
        <!-- also separated everything into separate divs and reordered them -->
        <div style="float: right;">
            <asp:LinkButton ID="LinkButton1" runat="server">Login now</asp:LinkButton>
        </div>
        <div style="float: right;">
            <asp:Label ID="Label3" runat="server" />
        </div>
        <!-- Search Box -->
        <div id="WLSearchBoxDiv" style="float: right">
        </div>
        <!-- Seach Box -->
    </div>

</asp:Content>
