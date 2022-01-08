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
    public partial class visualizzaTabelle : System.Web.UI.Page
    {

        // Mi salvo la stringa di connessione presente in Web.config
        //  private readonly string connectionString = WebConfigurationManager.ConnectionStrings["dircolDB"].ConnectionString;
        Operazione op = new Operazione(); // per accedere al metodo op.GetConnectionString()


        /// <summary>
        /// Procedura di popolamento della Grid presente in 'visualizzaTabelle.aspx' con i dati letti dal
        /// database. Da richiamare nel Page_Load()
        /// </summary>
        private void RiempiGridCapi()
        {
            // Creo una nuova lista di oggetti di tipo Capo
            List<Capo> ListaCapi = new List<Capo>();

            // Creo un nuovo oggetto di tipo Capo
            Capo capo = new Capo();

            ListaCapi = capo.GetCapi(op.GetConnectionString());

            /* 'DataSource' serve per ottenere la sorgente dalla quale
             *  si stanno recuperando i dati */
            gvCapi.DataSource = ListaCapi;

            /* Associo la sorgente dati al controllo GridView*/
            gvCapi.DataBind();
        }


        /// <summary>
        /// Procedura di popolamento della Grid presente in 'visualizzaTabelle.aspx' con i dati letti dal
        /// database. Da richiamare nel Page_Load()
        /// </summary>
        private void RiempiGridDipendenti()
        {
            // Creo una nuova lista di oggetti di tipo Dipendente
            List<Dipendente> ListaDipendenti = new List<Dipendente>();

            // Creo un nuovo oggetto di tipo Dipendente
            Dipendente dipendente = new Dipendente();

            ListaDipendenti = dipendente.GetDipendenti(op.GetConnectionString());

            /* 'DataSource' serve per ottenere la sorgente dalla quale
             *  si stanno recuperando i dati */
            gvDipendenti.DataSource = ListaDipendenti;

            /* Associo la sorgente dati al controllo GridView*/
            gvDipendenti.DataBind();
        }


        /// <summary>
        /// Procedura di popolamento della Grid presente in 'visualizzaTabelle.aspx' con i dati letti dal
        /// database. Da richiamare nel Page_Load()
        /// </summary>
        private void RiempiGridModelli()
        {
            // Creo una nuova lista di oggetti di tipo Modello
            List<Modello> ListaModelli = new List<Modello>();

            // Creo un nuovo oggetto di tipo Modello
            Modello modello = new Modello();

            ListaModelli = modello.GetModelli(op.GetConnectionString());

            /* 'DataSource' serve per ottenere la sorgente dalla quale
             *  si stanno recuperando i dati */
            gvModelli.DataSource = ListaModelli;

            /* Associo la sorgente dati al controllo GridView*/
            gvModelli.DataBind();
        }



        /// <summary>
        /// Procedura di popolamento della Grid presente in 'visualizzaTabelle.aspx' con i dati letti dal
        /// database. Da richiamare nel Page_Load()
        /// </summary>
        private void RiempiGridPuntiVendita()
        {
            // Creo una nuova lista di oggetti di tipo Modello
            List<PuntoVendita> ListaPuntiVendita = new List<PuntoVendita>();

            // Creo un nuovo oggetto di tipo Modello
            PuntoVendita puntovendita = new PuntoVendita();

            ListaPuntiVendita = puntovendita.GetPuntiVendita(op.GetConnectionString());

            /* 'DataSource' serve per ottenere la sorgente dalla quale
             *  si stanno recuperando i dati */
            gvPuntiVendita.DataSource = ListaPuntiVendita;

            /* Associo la sorgente dati al controllo GridView*/
            gvPuntiVendita.DataBind();

        }


        /// <summary>
        /// Procedura di popolamento della Grid presente in 'visualizzaTabelle.aspx' con i dati letti dal
        /// database. Da richiamare nel Page_Load()
        /// </summary>
        private void RiempiGridVendite()
        {
            // Creo una nuova lista di oggetti di tipo Vendita
            List<Vendita> ListaVendite = new List<Vendita>();

            // Creo un nuovo oggetto di tipo Vendita
            Vendita vendita = new Vendita();

            ListaVendite = vendita.GetVendite(op.GetConnectionString());

            /* 'DataSource' serve per ottenere la sorgente dalla quale
             *  si stanno recuperando i dati */
            gvVendite.DataSource = ListaVendite;

            /* Associo la sorgente dati al controllo GridView*/
            gvVendite.DataBind();

        }


        private void RiempiGridUtenti()
        {
            // Creo una nuova lista di oggetti di tipo Vendita
            List<UtenteOrdinario> ListaUtenti = new List<UtenteOrdinario>();

            // Creo un nuovo oggetto di tipo UtenteOrdinario
            UtenteOrdinario utenteOrdinario = new UtenteOrdinario();

            ListaUtenti = utenteOrdinario.GetUtenteOrdinario( op.GetConnectionString() );

            /* 'DataSource' serve per ottenere la sorgente dalla quale
             *  si stanno recuperando i dati */
            gvUtenti.DataSource = ListaUtenti;

            /* Associo la sorgente dati al controllo GridView*/
            gvUtenti.DataBind();
        }


        // Gestione ruoli utente: solo l'admin può accedere a questa pagina
        protected void Page_PreInit(object sender, EventArgs e)
        {
            /* La fase 'Start' è completata e le proprietà di Page sono
            * state caricate e sto per entrare nella fase 'Initialization'
            * Ho ora l'accesso a proprietà come "Page.IsPostBack */
            if (Session["ruolo"] != null)
                op.GetAccessoPaginaComeAmministratore(Session["ruolo"].ToString());
            else
            {
                Response.Redirect("Errore.aspx");
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnTabelle.Style.Add("padding", "50px"); // Do un po' di padding
                //gvCapi.Style.Add("max-height", "60px");
                //gvCapi.Style.Add("overflow", "scroll");
                //gvModelli.Style.Add("max-height", "60px");
                //gvModelli.Style.Add("overflow", "scroll");
                RiempiGridCapi();
                RiempiGridDipendenti();
                RiempiGridModelli();
                RiempiGridPuntiVendita();
                RiempiGridVendite();
                RiempiGridUtenti();
            }
        }
    }
}