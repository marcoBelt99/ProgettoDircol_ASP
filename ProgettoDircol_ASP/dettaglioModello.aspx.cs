using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;



// Per accedere alle classi che mi sono creato
using ProgettoDircol_ASP.Dati;

namespace ProgettoDircol_ASP
{
    public partial class dettaglioModello : System.Web.UI.Page
    {
        // Variabili globali
        Operazione op = new Operazione();

        int CodiceDelModelloInDettaglio = 0;

        int IDCapo_DaAggiungereAlCarrello = 0; // Contiene il giusto ID preso come argomento dal CommandType del bottone Aggiungi Capo nel WebForm

        public List<Capo> capiclassificati = new List<Capo>();


        // PROVO A MANTENERE LO STATO DELLA LISTA DEI CAPI CLASSIFICATI
        // RICORDA: Il tipo della lista (in questo caso Capo) deve essere serializzabile [Serializable]
        const string listaCapiClassificati = "listaCapiClassificati";
        // DICHIARO LA MIA LISTA "PERMANENTE"
        public List<Capo> CapiClassificati
        {
            get
            {
                // check if not exist to make new (normally before the post back)
                // and at the same time check that you did not use the same viewstate for other object
                if (!(ViewState[listaCapiClassificati] is List<Capo>))
                {
                    // need to fix the memory and added to viewstate
                    ViewState[listaCapiClassificati] = new List<Capo>();
                }

                return (List<Capo>)ViewState[listaCapiClassificati];
            }
        }


        // Creo un oggetto di tipo UtenteOrdinario che mi servirà per assicurarmi di star considerando
        // L'utente della sessione corrente
        UtenteOrdinario utente_ordinario = new UtenteOrdinario();

        /// <summary>
        /// Metodo GetModello relativo al FormControl nel file dettagliModello
        /// </summary>
        /// <param name="codModello"></param>
        /// <returns></returns>
        public IQueryable<Modello> GetModello([QueryString("codModello")] int? codModello)
        {
            var TabellaModelli = new Modello();
            IQueryable<Modello> query = TabellaModelli.GetModelli(op.GetConnectionString()).AsQueryable();
            if (codModello.HasValue && codModello > 0)
            {
                query = query.Where(p => p.CodModello == codModello);
                // Recupero il codice del modello in dettaglio, per poter usare il suo valore nella list view successiva
                CodiceDelModelloInDettaglio = Convert.ToInt32(codModello);

                /* PROVO A MANTENERE IL VIEW STATE DEL CODICE DEL MODELLO IN DETTAGLIO:
                 * in questo modo, poi, quando faccio una redirect in questa pagina con una
                 * query string che mi costruisco, non perdo il valore del codice del modello */
                object codmod = ViewState["cod_modello_in_dettaglio"];
                ViewState["cod_modello_in_dettaglio"] = CodiceDelModelloInDettaglio;
                // codmod = CodiceDelModelloInDettaglio;
            }
            else
            {
                query = null;
            }
            return query;

        }

        /// <summary>
        /// NON FUNZIONANTE !!!!!
        /// Metodo GetModello_1 , un'altra versione elegante, relativo al FormControl nel file dettagliModello
        /// NON FUNZIONANTE !!!!!
        /// </summary>
        /// <param name="codModello"></param>
        /// <returns></returns>
        public Modello GetModello_1([QueryString("codModello")] int? codModello)
        {
            Modello mod = new Modello();

            // Ricerco nella lista dei modelli quel modello di codice pari a codModello
            // p => [espressione booleana] è un predicato
            return mod.GetModelli(op.GetConnectionString()).Find(p => p.CodModello == codModello);
        }


        // Non la richiamo da nessuna parte, la tengo solo perchè è interessante....
        public List<Capo> GetCapiClassificati([QueryString("codModello")] int? codModello) // 
        {
            Capo capo = new Capo();

            List<Capo> ListaCapiClassificati = new List<Capo>();

            // Ricerco nella lista dei capi quel capo avente come CodModello pari a codModello
            // p => [espressione booleana] è un predicato
            ListaCapiClassificati.Add(capo.GetCapi(op.GetConnectionString()).Find(p => p.CodModello == this.GetModello_1(codModello).CodModello));

            return ListaCapiClassificati;
        }


        /// <summary>
        /// Sto usando questa per la List View.
        /// Ritorna l'elenco (lista) di tutti i capi classificati
        /// da un determinato modello
        /// INIZIALIZZO LA LISTA DEI CAPI CLASSIFICATI DAL MODELLO CHE STO CONSIDERANDO
        /// </summary>
        /// <param name="codModello"></param>
        /// <returns></returns>
        public List<Capo> GetCapiClassificati_1() // 
        {
            Capo capo = new Capo();

            // List<Capo> ListaCapiClassificati = new List<Capo>();

            // Scorri la lista dei capi
            foreach (var c in capo.GetCapi(op.GetConnectionString()))
            {
                // Se il capo i-esimo ha come codice modello CodiceDelModelloInDettaglio
                if (c.CodModello == CodiceDelModelloInDettaglio)
                // Allora aggiungilo alla lista dei capi classificati
                {
                    // Aggiungo alla lista locale
                    //  ListaCapiClassificati.Add(c);
                    // Aggiungo alla lista preservata grazie al view state
                    CapiClassificati.Add(c);
                }

                else
                    // Altrimenti, vai avanti con la ricerca
                    continue;
            }

            //return ListaCapiClassificati;
            return CapiClassificati;
        }



        public void TogliCapoClassificato(int id)
        {
            
            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(op.GetConnectionString());

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'capi' dove CodModello=@CodModello
            string selectSQL = "SELECT * FROM capi WHERE CodModello=@codiceModello AND ID!=@id;";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(selectSQL, con);

            // Aggiungo il parametro della query
            cmd.Parameters.Add(new SqlParameter("@codiceModello", ViewState["cod_modello_in_dettaglio"]));
            cmd.Parameters.Add(new SqlParameter("@id", id.ToString()));

            // Leggo le righe (in modo forward-only) dal database
            SqlDataReader dr = cmd.ExecuteReader();

            // Se il data reader non è null
            if (dr != null)
            {
                // Finchè leggi una riga (un record) dal database
                while (dr.Read())
                {
                    // Crea un nuovo oggetto di tipo Capo
                    Capo capo = new Capo();

                    // Leggi e converti i dati dal record corrente
                    capo.ID = Convert.ToInt32(dr["ID"]);
                    capo.Taglia = dr["Taglia"].ToString();
                    capo.Colore = dr["Colore"].ToString();
                    capo.PuntoVendita = Convert.ToInt32(dr["PuntoVendita"]);
                    capo.CodModello = Convert.ToInt32(dr["CodModello"]);

                    // Aggiungi il capo alla lista dei capi classificati
                    CapiClassificati.Add(capo);
                }
            }
            // Chiudo la connessione al database
            con.Close();
        }





        /// <summary>
        /// GUARDARE BENE QUESTO GESTORE DI EVENTI:
        /// Gli sto passando un parametro dal Web Form !!!! 
        /// Fighissimo ;)  
        /// !!!!! Avevo perso 2 anni cercando di far comunicare dati tramite Ajax tra C# e Javascript...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAggiungiAlCarrello_Click(object sender, EventArgs e)
        {
            // Cast esplicito per ottenere la proprietà CommandArgument --> vale solo per i Button, non vale es) per i LinkButton
            Button btn = (Button)sender;

            // ID del capo passato come argomento
            int argomentoID = int.Parse(btn.CommandArgument.ToString());

            // Chiave di ricerca del capo da aggiungere al carrello
            IDCapo_DaAggiungereAlCarrello = argomentoID;

            // Mi assicuro di star considerando l'utente della sessione corrente:
            // Assegno ad utente_ordinario il risultato del metodo TrovaUtente
            utente_ordinario = utente_ordinario.TrovaUtente(Session["username"].ToString());

            // Se ho ottenuto un oggetto di tipo UtenteOrdinario dalla ricerca, allora esegui operazioni di aggiunta
            // al carrello
            if (utente_ordinario != null)
            {

                // Ricerca di oggetto di tipo Capo da aggiungere al carrello dell'utente in base alla chiave (l'ID passato come argomento
                foreach (var c in CapiClassificati)
                {
                    // Se ID della lista capi classificati dal modello considerato == ID passato come argomento
                    if (c.ID == IDCapo_DaAggiungereAlCarrello)
                    {
                        // Aggiungi il capo al carrello dell'utente della sessione corrente( di username Session["username"] )
                        utente_ordinario.AggiungiAlCarrello(c);

                        // Poi,o rendo indisponibile la listview del capo che ho appena aggiunto...
                        // Oppure posso togliere il capo dalla lista dei capi classificati
                        // this.GetCapiClassificati_1().Remove(c)
                        TogliCapoClassificato(c.ID);
                        // Quindi, faccio un aggiornamento della pagina
                        // Response.Redirect("~/dettagliModello.aspx?codModello=" + CodiceDelModelloInDettaglio.ToString(), false);
                        Response.Redirect(String.Format("~/dettaglioModello.aspx?codModello={0}", ViewState["cod_modello_in_dettaglio"]));
                        // non serve continuare, quindi esco dal ciclo
                        break;
                        // Provare così per il momento ;)
                    } // fine if uguaglianza degli id capo
                } // fine for
            } // fine if utente != null


        } // fine gestore evento





        protected void Page_PreInit(object sender, EventArgs e)
        {
            /* La fase 'Start' è completata e le proprietà di Page sono
            * state caricate e sto per entrare nella fase 'Initialization'
            * Ho ora l'accesso a proprietà come "Page.IsPostBack */
            if (Session["ruolo"] != null)
                op.GetAccessoPaginaComeUtente(Session["ruolo"].ToString());
            else
            {
                Response.Redirect("Errore.aspx");
            }

            // Inizializzo al Page_PreInit la lista dei capi disponibili
            //capiclassificati = op.GetCapiClassificatiDaUnModello(CodiceDelModelloInDettaglio);
            //capiclassificatiTest = GetCapiClassificati_1();

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //fvDettagliModello.Style.Add("background-color", "white");
            //lvCapi.Style.Add("background-color", "white");
            lvCapi.Style.Add("padding-left", "50px");
            lvCapi.Style.Add("margin-left", "50px");

            // Inizializzo la lista dei capi classificati
            //capiclassificati = op.GetCapiClassificatiDaUnModello(CodiceDelModelloInDettaglio);
            //capiclassificatiTest = GetCapiClassificati_1();
        }


    }
}