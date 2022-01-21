using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.Sql;





// Per accedere alle classi
using ProgettoDircol_ASP.Dati;

// Per le operazione su file
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ProgettoDircol_ASP
{
    public partial class visualizzaCarrello : System.Web.UI.Page
    {

        // ################################################
        /* ########### VARIABILI GLOBALI ################# */
        // ################################################

        Operazione op = new Operazione();

        /// <summary>
        /// Dizionario che serve per salvarmi i capi contenuti nel file di testo 'CapiAggiuntiAlCarrello.txt'.
        /// Questo perchè: se aggiungo un capo al carrello, devo rimouverlo dalla tabella capi (altrimenti
        /// ho problemi nella pagina gestioneModelli), ma, se decido di rimuovere il capo, e riaggiungerlo alla
        /// tabella dei capi, senza l'ausilio del file.txt non riuscirei a ri-ottenere il capo.
        /// Leggo dunque ogni riga del file di testo e la salvo in un dizionario che poi mi servirà per fare
        /// delle elaborazioni di "join" con gli altri dati analoghi, e per recuperare le informazioni.
        /// ########## USO DEI DIZIONARI: sono coppie (Key, Value) ###########
        /// Data la chiave K --> ottengo il valore V
        /// /// Nel mio caso: K= variabile intera ID, V=Oggetto di tipo Capo
        /// 1) Aggiunta: dizionario.Add(K,V)
        /// 2) Accesso agli elementi (ottenere il valore data la chiave): dizionario[K]
        /// 3) Aggiornamento: dizionario[K] = V
        /// </summary>
        public Dictionary<int, Capo> Capi_Aggiunti_Al_Carrello = new Dictionary<int, Capo>();


        /// Dichiaro una variabile globale di tipo lista di carrelli, che popolo nel Page_Load
        public List<Carrello> ListaCarrelli = new List<Carrello>();

        /// <summary>
        /// LISTA DEI CAPI DA VISUALIZZARE DELL'UTENTE CORRENTE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public List<Capo> CAPI_UTENTE = new List<Capo>();

        // Nome del File dal quale andrò a leggere e scrivere
        protected string NomeFileCapiAggiuntiAlCarrello = HttpContext.Current.Server.MapPath("~/CapiAggiuntiAlCarrello.txt");

        string NomeFileJson = HttpContext.Current.Server.MapPath("~/Carrelli.json");    // Prelevo il nome del file JSON


        /// Creo un oggetto di tipo UtenteOrdinario che mi servirà per assicurarmi di star considerando
        /// l'utente della sessione corrente. In pratica lo uso per fare i confronti: if(utente == Session["utente"])
        UtenteOrdinario utente_ordinario = new UtenteOrdinario();

        // ####################################################################
        // ######################## METODI ###############################
        // ####################################################################


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
            if ((ViewState["contatore"] == null) || ((int)ViewState["contatore"] == 0))
                Mostra_Dettagli_Capo();
            else
                Nascondi_Dettagli_Capo();
            if (!Page.IsPostBack && (ViewState["contatore"] == null))
                Mostra_Dettagli_Capo();
            //string UrlImmagine = HttpContext.Current.Server.MapPath("~/Images/Icone/caret-right-fill.svg");
            //gvCarrello.Rows[0].Cells[4].
            //buttonFieldDettaglio.ImageUrl = UrlImmagine;


            /// POPOLO IL DIZIONARIO DEI CAPI AGGIUNTI AL CARRELLO ANDANDO A LEGGERE DAL FILE.TXT
            Lettura_File_CapiAggiunti_e_Popolamento_Dizionario();

            ///CARICO LA LISTA GLOBALE DEI CARRELLI ANDANDO A LEGGERE DAL FILE .JSON
            // Leggo da file: FileOggetto.json
            string json = File.ReadAllText(NomeFileJson);
            // Deserializzo il contenuto del file letto in una Lista di oggetti di tipo Carrello
            // e la salvo in una variabile
            ListaCarrelli = JsonConvert.DeserializeObject<List<Carrello>>(json);

            /// POPOLO LA GRID VIEW 
            PopolaGridView();


            ///MI SALVO LA QUANTITA' DI CAPI NELLA SESSIONE
            Session["quantita"] = CAPI_UTENTE.Count;



        }



        public void PopolaGridView()
        {
            /// Costruisco la lista CAPI_UTENTE
            // Ottengo il carrello dell'utente della sessione corrente
            if (ListaCarrelli != null)
            {
                Carrello carr = ListaCarrelli.Find(x => x.Username == Session["username"].ToString());

                // Se è stato trovato il carrello per questo utente nella lista globale
                if (carr != null)
                {
                    // Prelevami tutti i capi presenti nel dizionario con quell'id ed aggiungili alla lista CAPI_UTENTE
                    // In questo modo posso costruire tale lista che poi diventerà il DataSource della GridView
                    foreach (var id in carr.ListaIDCapi)
                    {
                        // Ottieni il capo di chiave id 
                        Capo capo = null;
                        capo = Capi_Aggiunti_Al_Carrello[id];
                        // Aggiungilo il capo al DataSource della GridView
                        CAPI_UTENTE.Add(capo);
                    }
                }
                // Altrimenti devi crearne uno nuovo per questo utente ed aggiungerlo alla lista dei carrelli globale
                else
                {
                    // Creo il nuovo carrello
                    carr = new Carrello();
                    carr.Username = Session["username"].ToString();
                    carr.ListaIDCapi = new List<int>();

                    //Lo aggiungo alla lista globale dei carrelli
                    ListaCarrelli.Add(carr);

                    // Scrivo la lista globale che ho appena aggiornato sul file JSON
                    var json = JsonConvert.SerializeObject(ListaCarrelli);
                    File.WriteAllText(NomeFileJson, json);
                }

                /// DATASOURCE E DATABINDING
                gvCarrello.DataSource = CAPI_UTENTE;
                gvCarrello.DataBind();
            }
        }


        public void Lettura_File_CapiAggiunti_e_Popolamento_Dizionario()
        {
            // Leggo da file i capi aggiunti e me li salvo nel Dizionario
            // Lettura_File_CapiAggiunti_e_Popolamento_Dizionario();

            try
            {
                using (StreamReader sr = new StreamReader(this.NomeFileCapiAggiuntiAlCarrello))
                {
                    // Leggi la prima riga
                    string line = sr.ReadLine();

                    while (line != null)
                    {
                        // Suddivido la riga corrente in un insieme di stringhe e le salvo in un vettore
                        string[] tok = line.Split('\t');
                        // Contatore di stringhe contenute in tok[]
                        int i = 0;
                        // Variabile che conterrà il capo
                        Capo capo = null;
                        // Finchè la 
                        while (i < tok.Length)
                        {
                            i = 0;
                            // Leggo e parsifico
                            int ID = int.Parse(tok[i]);
                            string Taglia = tok[i + 1];
                            string Colore = tok[i + 2];
                            int PuntoVendita = int.Parse(tok[i + 3]);
                            int CodModello = int.Parse(tok[i + 4]);

                            // Costruisco il capo
                            capo = new Capo(ID, Taglia, Colore, PuntoVendita, CodModello);

                            // Lo aggiungo al Dizionario
                            Capi_Aggiunti_Al_Carrello.Add(ID, capo);

                            // Faccio terminare il while facendo assumere ad i un valore > di tok.Lenght
                            i = tok.Length + 1;
                        } // fine while tok
                          // Leggo la prossima linea
                        line = sr.ReadLine();
                    } // fine while principale
                } // fine using
            }
            catch (IOException ex)
            {
                Console.Write(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                // Libero il file, così il SO può far accedere altri processi al file
                // Inoltre, resituisco le risorse aperte precedentemente
                Dispose();
            }

            /// A scopo di prova, provo a scrivere il dizionario nel file in un file temporaneao
            //string nomefileprova = HttpContext.Current.Server.MapPath("~/STUDIO/prova.txt");

            /// Converto in lista il dizionario per poterlo scrivere sul file
            ////var L = Capi_Aggiunti_Al_Carrello.Values.ToList();

            // Ogni elemento deve essere una stringa (in particolare, ogni elemento della lista di stringhe è
            // una stringa formata dal metodo toString() della classe Capo
            //string S = "";
            ////List<string> ss = new List<string>();

            ////foreach (var l in L)
            ////{
            //S += l.ToString();
            ////ss.Add(l.ToString());
            ////}
            ////var s = String.Join("\n", ss);

            // Scrivi su file 
            ////File.WriteAllText(NomeFileCapiAggiuntiAlCarrello,s);
            //File.AppendAllText(NomeFileCapiAggiuntiAlCarrello, s);
        }





        /// <summary>
        /// Nasconde le label ed i dettagli associati ad un capo
        /// </summary>
        public void Nascondi_Dettagli_Capo()
        {
            ImgModello.Visible = false;
            lblNome.Visible = false;
            lblDescrizione.Visible = false;
            lblPrezzoListino.Visible = false;
            lblGenere.Visible = false;
            lblCollezione.Visible = false;
            divDettaglio.Visible = false;
        }

        /// <summary>
        /// Mostra le label ed i dettagli associati ad un capo
        /// Dipende in base al valore del ViewState["contatore"]
        /// </summary>
        public void Mostra_Dettagli_Capo()
        {
            ImgModello.Visible = true;
            lblNome.Visible = true;
            lblDescrizione.Visible = true;
            lblPrezzoListino.Visible = true;
            lblGenere.Visible = true;
            lblCollezione.Visible = true;
            divDettaglio.Visible = true;
        }


        /// <summary>
        /// A SCOPO DI PROVA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnRimuovi_Click(object sender, EventArgs e)
        //{
        //    Lettura_File_CapiAggiunti_e_Popolamento_Dizionario();
        //}


        /// <summary>
        /// Da richiamare all'evento 'gvCarrello_RowCommand'
        /// </summary>
        /// <param name="CodModello"></param>
        /// <returns></returns>
        DataRow GetDettagliCapo(int CodModello)
        {
            /// DataTable rappresenta una tabella di dati in memoria
            DataTable dt = new DataTable();
            // Mi connetto ad un'origine dati (al DATABASE)
            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(op.GetConnectionString());
            string strSQL = "SELECT Immagine,Nome, Descrizione, PrezzoListino, Genere, Collezione FROM modelli WHERE modelli.CodModello = @CodModello";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@CodModello", CodModello);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            // Riempio la datatable
            da.Fill(dt);
            con.Close();
            return dt.Rows[0];



        }

        /// <summary>
        /// Evento generato in risposta a qualsiasi interazione con l'utente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCarrello_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            // Ogni CommandName è associato ad un bottone
            switch (e.CommandName)
            {
                /// Sfrutto le proprietà DataKeys e DataKeyNames: consentono di collezionare
                /// i valori di un campo a scelta della sorgente dati.
                /// DataKeyNames specifica i campi, tipicamente una chiave primaria;
                /// DataKeys colleziona i valori dei campi appartenenti ad ogni riga della sorgente
                /// coinvolta nell'operazione di binding.

                // #####################################################
                // ###############  MOSTRA DETTAGLI  ###################
                // #####################################################
                case "DettagliCapo":
                    // Recupero dettagli del Capo. Es) Il modello di appartenenza


                    int riga = Convert.ToInt32(e.CommandArgument);
                    // int CodModello = (int)gvCarrello.DataKeys[riga].Value;
                    int CodModello = int.Parse(gvCarrello.Rows[riga].Cells[3].Text);
                    DataRow r = GetDettagliCapo(CodModello);

                    /// IMMAGINE NOT ALLOWED
                    ImgModello.Src = HttpContext.Current.Server.MapPath("~/Immagini" + r["Immagine"].ToString());
                    lblNome.Text = "<b>Nome</b>:\t" + r["Nome"].ToString();
                    lblDescrizione.Text = "Descrizione:\t" + r["Descrizione"].ToString();
                    lblPrezzoListino.Text = "Prezzo di listino\t" + string.Format("{0:#,0.00} Euro", r["PrezzoListino"]);
                    lblGenere.Text = "Genere:\t" + r["Genere"].ToString();
                    lblCollezione.Text = "Collezione:\t" + r["Collezione"].ToString();


                    int contatore;
                    if (ViewState["contatore"] == null)
                        contatore = 0;
                    else
                        contatore = (int)ViewState["contatore"] + 1;

                    ViewState["contatore"] = contatore;

                    if (contatore > 1)
                    {
                        Nascondi_Dettagli_Capo();
                        contatore = 0;
                        ViewState["contatore"] = contatore;
                    }
                    else
                    {
                        Mostra_Dettagli_Capo();
                    }
                    break;
                // #####################################################
                // ############### ELIMINAZIONE ########################
                // #####################################################
                case "EliminaCapo":
                    riga = Convert.ToInt32(e.CommandArgument);
                    int ID = int.Parse(gvCarrello.DataKeys[riga]["ID"].ToString());

                    // Mi assicuro di star considerando l'utente della sessione corrente: Assegno ad utente_ordinario il risultato del metodo TrovaUtente
                    utente_ordinario = utente_ordinario.TrovaUtente(Session["username"].ToString());

                    // Se ho ottenuto un oggetto di tipo UtenteOrdinario dalla ricerca, allora esegui operazioni di aggiunta al carrello
                    if (utente_ordinario != null)
                    {
                        /// Ottieni il capo da inserire nuovamente nel DB
                        Capo capo = Capi_Aggiunti_Al_Carrello[ID];
                        if (capo != null)
                        {
                            /// Se l'ho ottenuto, lo inserisco nuovamente nel DB
                            capo.InserisciCapo(op.GetConnectionString(), capo);

                            /// Lo elimino dal dizionario
                            bool RimossoDaDizionario = Capi_Aggiunti_Al_Carrello.Remove(ID);
                            if (RimossoDaDizionario == true)
                            {
                                // Se l'ho rimosso dal dizionario
                                // Parsifico in lista il dizionario e lo scrivo nel file dei 'CapiAggiuntiAlCarrello.txt' (sovrascrivendolo)

                                /// Converto in lista il dizionario per poterlo scrivere sul file
                                var L = Capi_Aggiunti_Al_Carrello.Values.ToList();
                                // Ogni elemento deve essere una stringa (in particolare, ogni elemento della lista di stringhe è
                                // una stringa formata dal metodo toString() della classe Capo
                                List<string> ss = new List<string>();
                                foreach (var l in L)
                                {
                                    ss.Add(l.ToString());
                                }
                                ////////////////////////////////////////////////
                                ///////////////////////////////////////////////
                                ///ERA LEI LA RESPONSABILE!!!! String.Join("\n",ss)
                                /// Gli oggetti di tipo capo contenevano già il carattere '\n' nella
                                /// loro toString()
                                var s = String.Join("", ss);
                                // Scrivi su file 
                                File.WriteAllText(NomeFileCapiAggiuntiAlCarrello, s);

                                // Ora ho:
                                // 1) Inserito sul DB il capo
                                // 2) Rimosso dal dizionario il capo
                                // 3) Aggiornato il file: 'CapiAggiuntiAlCarrello'.txt


                                /// Ora devo Aggiornare il JSON;
                                /// Prima però aggiorno la lista dei carrelli:
                                // Ottengo il carrello desiderato: quello dell'utente della sessione corrente
                                Carrello carrello = ListaCarrelli.Find(c => c.Username == Session["username"].ToString());

                                // Se ho trovato il carrello desiderato ed è vuoto (lista di ID == 0), non serve più tenerlo, lo elimino dalla lista globale
                                if (carrello != null)
                                {
                                    if (carrello.ListaIDCapi.Count == 0)
                                        ListaCarrelli.Remove(carrello);

                                    /// Elimino il capo dal carrello 
                                    carrello.ListaIDCapi.Remove(ID);

                                    /// AGGIORNAMENTO EFFETTIVO DELLA LISTA GLOBALE:
                                    /// Rimuovo il vecchio carrello
                                    int rimossi = ListaCarrelli.RemoveAll(x => x.Username == carrello.Username);
                                    if (rimossi > 0)
                                    {
                                        /// Ci aggiungo quello modificato (quello la cui lista di ID è stata modificata, quello da cui ho appena rimosso)
                                        ListaCarrelli.Add(carrello);
                                    }
                                }
                                // Ho finalmente aggiornato la lista globale dei carrelli, ora la scrivo sul file.JSON

                                /** Aggiorno il file JSON: Ci riscrivo la lista globale appena aggiornata 
                                 * Scrivo la lista dei carrelli: serializzo la lista di carrelli come stringa json e la scrivo sul file */
                                var json = JsonConvert.SerializeObject(ListaCarrelli);
                                File.WriteAllText(NomeFileJson, json);

                                // Aggiorno la pagina con un redirect alla stessa pagina per vedere 
                                // Gli effettivi aggiornamenti al carrello
                                Response.Redirect("visualizzaCarrello");
                            }
                        } // if capo != null
                    } // fine controllo utente sessione corrente
                    break;
                default:
                    break;
            }

            //if (e.CommandName == "DettagliCapo")
            //{

            //}

        }






        /// <summary>
        /// Evento che gestisce la paginazione: quando cambio pagina, se non lo gestissi, perderei la persistenza dei dati
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCarrello_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCarrello.PageIndex = e.NewPageIndex;
            PopolaGridView();
        }


    }
}