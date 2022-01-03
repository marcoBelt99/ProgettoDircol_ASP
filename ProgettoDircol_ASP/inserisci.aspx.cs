using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


// Aggiungo riferimento per accedere al file Web.config
// using System.Web.Configuration;
// Aggiungo riferimento alla cartella che contiene la classe che comunica col database
using ProgettoDircol_ASP.Dati;

namespace ProgettoDircol_ASP
{
    public partial class inserisci : System.Web.UI.Page
    {

        /* Richiamo un oggetto di classe Operazione per poter accedere alle
           sue funzioni che ritornano le liste con i dati necessari per fare
           il DataBinding */
        Operazione op = new Operazione();

        // Mi salvo la stringa di connessione presente in Web.config
        // private readonly string connectionString = WebConfigurationManager.ConnectionStrings["dircolDB"].ConnectionString;



        /* ############### VALIDATORI CUSTOM. (In ordine di come appaiono sulla pagina)  ###########################*/

        /// <summary>
        /// Valida tutte le drop down list passate, controllando che il valore
        /// inserito sia diverso da "---", in tal caso la ddl è valida,
        /// altrimenti è invalida
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void ValidazioneDropDownListCustom(object source, ServerValidateEventArgs args)
        {
            // Prelevo quello che c'è inserito nel controllo
            string stringaSelezionata = args.Value;

            // Momentaneamente il controllo non è valido
            args.IsValid = false;

            // Solo se la stringa selezionata è != stringa di controllo, dove stringDicontrollo = "---"
            if (stringaSelezionata != op.stringaDicontrollo)
            // Rendi valido questo input
            {
                args.IsValid = true;
                return;
            }
            // Altrimenti rimane invalido 
            else
                // ed esci
                return;

        }

        /// <summary>
        /// Guardo cosa c'è selezionato nella DropDownList: ddlTaglia
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void cvTaglia_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidazioneDropDownListCustom(source, args);
            /*
            // Prelevo quello che c'è inserito nel controllo
            string stringaSelezionata = args.Value;

            // Momentaneamente il controllo non è valido
            args.IsValid = false;

            // Solo se la stringa selezionata è != stringa di controllo
            if (stringaSelezionata != op.stringaDicontrollo)
            // Rendi valido questo input
            {
                args.IsValid = true;
                return;
            }
            // Altrimenti rimane invalido 
            else
                // ed esci
                return;
            */


        }

        /// <summary>
        /// Guardo cosa c'è selezionato nella DropDownList: ddlPuntoVendita
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void cvPuntoVendita_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidazioneDropDownListCustom(source, args);
        }

        /// <summary>
        /// Guardo cosa c'è selezionato nella DropDownList: ddlCodiceModello
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void cvCodiceModello_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidazioneDropDownListCustom(source, args);
        }

        /// <summary>
        /// Scorro la lista delle matricole e vedo se quella inserita è diversa
        /// da tutte quelle già inserite
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void cvMatricolaDipendente_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string matricolaInserita = args.Value;
            args.IsValid = false;
            // Scorri la lista della matricole
            foreach (string matricola in op.GetMatricoleDipendenti())
            {
                // Se la matricola inserita è diversa da quella corrente
                if (matricolaInserita != matricola)
                    // passa alla prossima della lista
                    continue;
                else
                    // Altrimenti, ho trovato due matricole uguali, non è valido!
                    return;
            }
            // Alla fine della scansione, se è andato tutto bene,
            // Allora il test è stato superato
            args.IsValid = true;
            return;
        }

        /// <summary>
        /// Guardo cosa c'è selezionato nella DropDownList: ddlPuntoVendita_Dipendenti
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void cvPuntoVendita_Dipendenti_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidazioneDropDownListCustom(source, args);
        }

        /// <summary>
        /// Guardo cosa c'è selezionato nella DropDownList: ddlMatricola_Vendite
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void cvMatricola_Vendite_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidazioneDropDownListCustom(source, args);
        }

        /// <summary>
        /// Guardo cosa c'è selezionato nella DropDownList: ddlIDCapo_Vendite
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void cvddlIDCapo_Vendite_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidazioneDropDownListCustom(source, args);
        }

        /* ############### FINE VALIDATORI CUSTOM  ###########################*/



        /* ################ INSERIMENTO CAPO ################################ */
        /// <summary>
        /// Inserimento capo nella tabella capi del database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInserisciCapo_Click(object sender, EventArgs e)
        {
            // Passo i valori letti al costruttore
            Capo capo = new Capo();

            capo.Taglia = ddlTaglia.SelectedItem.Value;
            capo.Colore = colorInput.Value;
            capo.PuntoVendita = Convert.ToInt32(ddlPuntoVendita.SelectedItem.Value);
            capo.CodModello = Convert.ToInt32(ddlCodiceModello.SelectedItem.Value);


            // Inserisco il capo nel database
            capo.InserisciCapo(op.GetConnectionString(), capo);

            Response.Redirect("visualizzaTabelle.aspx");
        } // fine bntInserisci


        /// <summary>
        /// Serve per resettare i campi del fieldset dei capi:
        /// All'evento click su questo bottone, vengono annullati
        /// i campi del suddetto fieldset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnnullaCapo_Click(object sender, EventArgs e)
        {
            // Resetto i giusti campi
            //ddlTaglia.Text = op.stringaDicontrollo; // stringaDiControllo = "---";
            //colorInput.Value = "#000000";
            //ddlPuntoVendita.Text = op.stringaDicontrollo;
            //ddlCodiceModello.Text = op.stringaDicontrollo;
            Response.Redirect("~/inserisci.aspx", false);
        }

        /* ################### FINE INSERIMENTO CAPO ################################ */


        /* ################### INSERIMENTO MODELLO ################################ */
        protected void btnInserisciModello_Click(object sender, EventArgs e)
        {
            Modello modello = new Modello();

            modello.Immagine = fileUploadImmagine.PostedFile.FileName; // ottengo il nome completo del file
            modello.Nome = txtNomeModello.Text;
            modello.Descrizione = txtDescrizioneModello.Text;
            modello.PrezzoListino = Convert.ToDouble(txtPrezzoDiListino.Text);
            modello.Genere = Convert.ToChar(txtGenere.Text);
            modello.Collezione = txtCollezione.Text;

            // Inserimento modello in Database
            modello.InserisciCapo(op.GetConnectionString(), modello);

            // Visito la pagina delle tabelle, così vedo il nuovo record inserito
            Response.Redirect("visualizzaTabelle.aspx");
        }

        /// <summary>
        /// Serve per resettare i campi del fieldset dei capi:
        /// All'evento click su questo bottone, vengono annullati
        /// i campi del suddetto fieldset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAnnullaModello_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/inserisci.aspx", false);
        }

        /* ################### FINE INSERIMENTO MODELLO ################################ */


        /* ###################  INSERIMENTO PUNTOVENDITA ################################ */
        protected void btnInserisciPuntoVendita_Click(object sender, EventArgs e)
        {
            PuntoVendita puntovendita = new PuntoVendita();

            puntovendita.Indirizzo = txtIndirizzo.Text;
            puntovendita.Telefono = txtTelefono.Text;
            puntovendita.Citta = txtCitta.Text;
            puntovendita.DataInizio = txtDataInizio.Text;
            puntovendita.Nazione = txtNazione.Text;


            // Inserimento modello in Database
            puntovendita.InserisciPuntoVendita(op.GetConnectionString(), puntovendita);

            // Visito la pagina delle tabelle, così vedo il nuovo record inserito
            Response.Redirect("visualizzaTabelle.aspx");
        }

        protected void btnAnnullaPuntoVendita_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/inserisci.aspx", false);
        }
        /* ################### FINE INSERIMENTO PUNTOVENDITA ################################ */


        /* ###################  INSERIMENTO DIPENDENTE ################################ */

        protected void btnInserisciDipendente_Click(object sender, EventArgs e)
        {
            // Passo i valori letti al costruttore
            Dipendente dipendente = new Dipendente();

            // dipendente.campo = controllo.text
            dipendente.Matricola = txtMatricola.Text;
            dipendente.Cognome = txtCognome.Text;
            dipendente.Nome = txtNomeDipendente.Text;
            dipendente.CodiceFiscale = txtCodiceFiscale.Text;
            dipendente.Qualifica = txtQualifica.Text;
            dipendente.PuntoVendita = Convert.ToInt32(ddlPuntoVendita_Dipendenti.SelectedItem.Value);

            // Inserisco il capo nel database
            dipendente.InserisciDipendente(op.GetConnectionString(), dipendente);

            Response.Redirect("visualizzaTabelle.aspx");
        }

        protected void btnAnnullaDipendente_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/inserisci.aspx", false);
        }
        /* ################### FINE INSERIMENTO DIPENDENTE ################################ */




        /* ###################  INSERIMENTO VENDITA ################################ */
        protected void btnInserisciVendita_Click(object sender, EventArgs e)
        {
            // Passo i valori letti al costruttore
            Vendita vendita = new Vendita();

            // vendita.campo = controllo.text
            vendita.DataVendita = txtDataVendita.Text;
            vendita.PrezzoVendita = Convert.ToDouble( txtPrezzoVendita.Text);
            vendita.Matricola =  ddlMatricola_Vendite.SelectedItem.Value;
            vendita.IDCapo = Convert.ToInt32( ddlIDCapo_Vendite.SelectedItem.Value);



            // Inserisco il capo nel database
            vendita.InserisciVendita(op.GetConnectionString(), vendita);

            Response.Redirect("visualizzaTabelle.aspx");

        }

        protected void btnAnnullaVendita_Click(object sender, EventArgs e)
        {

        }
        /* ################### FINE INSERIMENTO VENDITA ################################ */







        protected void Page_Load(object sender, EventArgs e)
        {
           /* pnInserimento.Style.Add("padding", "50px");*/ // Do un po' di padding

            if (IsPostBack)
                return;

            /* Associazione sorgente dati e binding DropDownList */

            // TAGLIE
            ddlTaglia.DataSource = op.GetTaglie(); // Stabilisco il collegamento tra controllo e sorgente dati
            ddlTaglia.DataBind(); // Determino l'associazione vera e propria
            ddlTaglia.Items.Insert(0, new ListItem(op.stringaDicontrollo)); // Aggiungo un valore di default: "---"

            // PUNTI VENDITA: (sia per la ddlPuntoVendita, sia per la ddl ddlPuntoVendita_Dipendenti
            List<int> ListaCodiciPuntivendita = op.GetCodiciPuntiVendita(); // necessario per evitare il problema dei doppioni
            ddlPuntoVendita.DataSource = ListaCodiciPuntivendita;
            ddlPuntoVendita.DataBind();
            ddlPuntoVendita.Items.Insert(0, new ListItem(op.stringaDicontrollo));


            ddlPuntoVendita_Dipendenti.DataSource = ListaCodiciPuntivendita;
            ddlPuntoVendita_Dipendenti.DataBind();
            ddlPuntoVendita_Dipendenti.Items.Insert(0, new ListItem(op.stringaDicontrollo));


            // CODICI MODELLO
            ddlCodiceModello.DataSource = op.GetCodiciModelli();
            ddlCodiceModello.DataBind();
            ddlCodiceModello.Items.Insert(0, new ListItem(op.stringaDicontrollo));


            // ID CAPO
            ddlIDCapo_Vendite.DataSource = op.GetIDCapi();
            ddlIDCapo_Vendite.DataBind();
            ddlIDCapo_Vendite.Items.Insert(0, new ListItem(op.stringaDicontrollo));


            // MATRICOLA
            ddlMatricola_Vendite.DataSource = op.GetMatricoleDipendenti();
            ddlMatricola_Vendite.DataBind();
            ddlMatricola_Vendite.Items.Insert(0, new ListItem(op.stringaDicontrollo));
        }




    } // fine classe
} // fine namespace