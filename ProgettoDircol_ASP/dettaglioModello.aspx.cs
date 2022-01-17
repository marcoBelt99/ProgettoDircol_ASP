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

// Per leggere/scrivere su file:
using System.IO;


// Per accedere alle classi che mi sono creato
using ProgettoDircol_ASP.Dati;

namespace ProgettoDircol_ASP
{
    public partial class dettaglioModello : System.Web.UI.Page
    {
        // VARIABILI GLOBALI
        Operazione op = new Operazione();

        int CodiceDelModelloInDettaglio = 0;

        int IDCapo_DaAggiungereAlCarrello = 0; // Contiene il giusto ID preso come argomento dal CommandType del bottone Aggiungi Capo nel WebForm

        public List<Capo> capiclassificati = new List<Capo>();

        // Mappa capi aggiunti al carrello (salvati sul file di testo:
        // 'CapiAggiuntiAlCarrello.txt' (Da inizializzare al page load):
        // ... 



        // MANTENGO LO STATO DELLA LISTA DEI CAPI CLASSIFICATI
        // RICORDA: Il tipo della lista (in questo caso Capo) deve essere serializzabile [Serializable]
        const string listaCapiClassificati = "listaCapiClassificati";

        public List<Capo> CapiClassificati // DICHIARO LA MIA LISTA "PERMANENTE"
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

        // MANTENGO LO STATO DELLA 'SPESA TOTALE DI LISTINO'
        // ViewState["spesa_di_listino"] = 


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

            // Mi assicuro di star considerando l'utente della sessione corrente: Assegno ad utente_ordinario il risultato del metodo TrovaUtente
            utente_ordinario = utente_ordinario.TrovaUtente(Session["username"].ToString());

            // Se ho ottenuto un oggetto di tipo UtenteOrdinario dalla ricerca, allora esegui operazioni di aggiunta al carrello
            if (utente_ordinario != null)
            {
                // Ricerca di oggetto di tipo Capo da aggiungere al carrello dell'utente in base alla chiave (l'ID passato come argomento
                foreach (var c in CapiClassificati)
                {
                    // Se ID della lista capi classificati dal modello considerato == ID passato come argomento
                    if (c.ID == IDCapo_DaAggiungereAlCarrello)
                    {
                        // Ho trovato il capo che voglio aggiugere al carrello

                        // Aggiungi l'id del capo alla lista di ID del carrello dell'utente della sessione corrente( di username Session["username"] )
                        utente_ordinario.AggiungiAlCarrello(c.ID);


                        // MI SALVO L'OGGETTO SU FILE: 'CapiAggiuntiAlCarrello.txt'
                        // Voglio togliere dalla pagina il capo appena inserito nel carrello
                        // perchè poi lo dovrò eliminare fisicamente dal DB
                        // altrimenti nella pagina visualizzerò sempre gli stessi capi 
                        // Dichiaro il nome del file (comprensivo del percorso, in questo caso si trova nella stessa directory del progetto)
                        //  string file = "~/CapiAggiuntiAlCarrello.txt";
                        string file = HttpContext.Current.Server.MapPath("~/CapiAggiuntiAlCarrello.txt");

                        //StreamWriter sw = new StreamWriter()
                        File.AppendAllText(file, c.ToString() + Environment.NewLine);

                        // ORA CHE HO l'ho SALVATO NEL FILE, POSSO ELIMINARLO DAL DATABASE 
                        c.EliminaCapo(c.ID);

                        //E DALLA RELATIVA LISTA
                        CapiClassificati.Remove(c);



                        // GESTISCO LA SPESA DI LISTINO:
                        Modello m = new Modello();
                        double spesa_di_listino;
                        object sl = ViewState["spesa_di_listino"]; // recupera l’ultimo valore
                        if (sl == null) // dopo il primo postback la variabile non è ancorastata persistita
                            spesa_di_listino = 0.0; // La inizializzo
                        else
                        {
                            var ModelloDiInteresse = m.GetModelli(op.GetConnectionString()).Find(mod => mod.CodModello == CodiceDelModelloInDettaglio);
                            spesa_di_listino = double.Parse(sl.ToString()) + ModelloDiInteresse.PrezzoListino;
                        }
                        ViewState["contatore"] = spesa_di_listino; // imposta il valore aggiornato


                        // Quindi, faccio un aggiornamento della pagina per vedere le cose tutte aggiornate
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