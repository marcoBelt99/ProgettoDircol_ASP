/* Aggiunta namespace che mi servono */
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System;
using System.Collections.Generic;
using System.IO; // Per leggere/scrivere da file
using System.Web.Configuration; // Aggiungo riferimento per accedere al file Web.config
using System.Web; // Per gestire il percorso del file



namespace ProgettoDircol_ASP.Dati
{
    /// <summary>
    /// Classe che serve per eseguire alcune operazioni con array o con le liste.
    /// - Ritorna la stringa di connessione col metodo 'GetConnectionString'.
    /// - Esegue interrogazioni al Database per recuperare i dati di interesse,
    ///   salvarli in alcune liste che poi passerò ad 'inserisci.aspx.cs'
    ///   per poter poi fare il Data Binding ai Controlli.
    /// </summary>
    public class Operazione
    {

        /// <summary> 
        /// Mi salvo la stringa di connessione presente in Web.config 
        /// </summary>
        private readonly string connectionString = WebConfigurationManager.ConnectionStrings["dircolDB"].ConnectionString;
        /// <summary>
        /// Array di stringhe delle taglie
        /// </summary>
        private string[] taglie = { "M", "L", "S", "XL", "XS", "XXS", "XXL" };
        /// <summary> 
        /// Stinga di controllo 
        /// </summary>
        public string stringaDicontrollo = "---";
        /// <summary> 
        /// Lista di interi che contiene i codici dei punti vendita 
        /// </summary>
        private List<int> codiciPuntiVendita = new List<int>();
        /// <summary> 
        /// Lista di interi che contiene i codici dei modelli di un capo 
        /// </summary>
        private List<int> codiciModelli = new List<int>();
        /// <summary> 
        /// Lista di interi che contiene gli ID dei capi 
        /// </summary>
        private List<int> IDCapi = new List<int>();
        /// <summary> 
        /// Lista di stringhe che contiene le matricole di tutti i dipendenti 
        /// </summary>
        private List<string> matricoleDipendenti = new List<string>();
        /// <summary> 
        /// Lista di stringhe che contiene le matricole di tutti i dipendenti la cui 
        /// Qualifica = 'Venditore'
        /// </summary>
        private List<string> matricoleDipendentiVenditori = new List<string>();
        /// <summary>
        /// Lista di stringhe che contiene i nomi degli stati letti dal file 'statiMembri.txt'
        /// </summary>
        private List<string> statiMembri = new List<string>();
        /// <summary> 
        /// Lista di stringhe che contiene gli username di tutti gli utenti 
        /// </summary>
        private List<string> usernameUtenti = new List<string>();
        /// <summary>
        /// Ottiene la stringa di connessione al Database.
        /// Usare questa funzione nelle altre classi per semplificare
        /// il codice ed evitare di recuperarla sempre (e di invocare ogni
        /// volta 'using System.Web.Configuration;'
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return this.connectionString;
        }





        /// <summary>
        /// Ritorna l'array delle taglie ordinato.
        /// </summary>
        /// <returns></returns>
        public string[] GetTaglie()
        {
            // Riordino l'array di stringhe
            Array.Sort(taglie);
            return taglie;
        }







        /// <summary>
        /// Ritorna la lista dei codici di tutti i punti vendita.
        /// </summary>
        /// <returns></returns>
        public List<int> GetCodiciPuntiVendita()
        {
            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'capi'
            string selectSQL = "select distinct CodPV from puntivendita order by CodPV";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(selectSQL, con);

            // Leggo le righe (in modo forward-only) dal database
            SqlDataReader dr = cmd.ExecuteReader();

            // 
            if (dr != null)
            {
                // Finchè leggi una riga (un record) dal database
                while (dr.Read())
                {
                    // Crea un nuovo elemento di tipo intero da aggiungere alla lista 
                    int CodPV = 0;

                    // Leggi e converti i dati dal record corrente
                    CodPV = Convert.ToInt32(dr["CodPV"]);

                    // Aggiungi il capo alla lista
                    codiciPuntiVendita.Add(CodPV);

                }
            }

            // Chiudo la connessione
            con.Close();

            // Ritorno la lista ordinata
            codiciPuntiVendita.Sort();
            return codiciPuntiVendita;

        }







        /// <summary>
        /// Ritorna la lista dei codici di tutti i modelli.
        /// </summary>
        /// <returns></returns>
        public List<int> GetCodiciModelli()
        {
            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'capi'
            string selectSQL = "select distinct CodModello from modelli order by CodModello";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(selectSQL, con);

            // Leggo le righe (in modo forward-only) dal database
            SqlDataReader dr = cmd.ExecuteReader();

            // 
            if (dr != null)
            {
                // Finchè leggi una riga (un record) dal database
                while (dr.Read())
                {
                    // Crea un nuovo elemento di tipo intero da aggiungere alla lista 
                    int CodModello = 0;

                    // Leggi e converti i dati dal record corrente
                    CodModello = Convert.ToInt32(dr["CodModello"]);

                    // Aggiungi il capo alla lista
                    codiciModelli.Add(CodModello);

                }
            }

            // Chiudo la connessione
            con.Close();

            // Ritorno la lista ordinata
            codiciModelli.Sort();
            return codiciModelli;

        }







        /// <summary>
        /// Ritorna la lista degli ID di tutti i capi.
        /// </summary>
        /// <returns></returns>
        public List<int> GetIDCapi()
        {
            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'capi'
            string selectSQL = "select distinct ID from capi order by ID";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(selectSQL, con);

            // Leggo le righe (in modo forward-only) dal database
            SqlDataReader dr = cmd.ExecuteReader();

            // 
            if (dr != null)
            {
                // Finchè leggi una riga (un record) dal database
                while (dr.Read())
                {
                    // Crea un nuovo elemento di tipo intero da aggiungere alla lista 
                    int ID = 0;

                    // Leggi e converti i dati dal record corrente
                    ID = Convert.ToInt32(dr["ID"]);

                    // Aggiungi il capo alla lista
                    IDCapi.Add(ID);

                }
            }

            // Chiudo la connessione
            con.Close();

            // Ritorno la lista ordinata
            IDCapi.Sort();
            return IDCapi;
        }






        /// <summary>
        /// Ritorna la lista delle matricole di tutti i dipendenti.
        /// </summary>
        /// <returns></returns>
        public List<string> GetMatricoleDipendenti()
        {
            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'capi'
            string selectSQL = "select distinct Matricola from dipendenti order by Matricola";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(selectSQL, con);

            // Leggo le righe (in modo forward-only) dal database
            SqlDataReader dr = cmd.ExecuteReader();

            // 
            if (dr != null)
            {
                // Finchè leggi una riga (un record) dal database
                while (dr.Read())
                {
                    // Crea un nuovo elemento di tipo intero da aggiungere alla lista 
                    string matricola = "";

                    // Leggi e converti i dati dal record corrente
                    matricola = dr["Matricola"].ToString();

                    // Aggiungi il capo alla lista
                    matricoleDipendenti.Add(matricola);

                }
            }

            // Chiudo la connessione
            con.Close();

            // Ritorno la lista ordinata
            matricoleDipendenti.Sort();
            return matricoleDipendenti;
        }

        /// <summary>
        /// Ritorna la lista delle matricole di tutti i dipendenti.
        /// </summary>
        /// <returns></returns>
        public List<string> GetMatricoleDipendentiVenditori()
        {
            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'capi'
            string selectSQL = "select distinct Matricola from dipendenti where Qualifica='Venditore' order by Matricola";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(selectSQL, con);

            // Leggo le righe (in modo forward-only) dal database
            SqlDataReader dr = cmd.ExecuteReader();

            // 
            if (dr != null)
            {
                // Finchè leggi una riga (un record) dal database
                while (dr.Read())
                {
                    // Crea un nuovo elemento di tipo intero da aggiungere alla lista 
                    string matricola = "";

                    // Leggi e converti i dati dal record corrente
                    matricola = dr["Matricola"].ToString();

                    // Aggiungi il capo alla lista
                    matricoleDipendentiVenditori.Add(matricola);

                }
            }

            // Chiudo la connessione
            con.Close();

            // Ritorno la lista ordinata
            matricoleDipendentiVenditori.Sort();
            return matricoleDipendentiVenditori;
        }


        /// <summary>
        /// Ritorna la lista degli stati che vado a leggere dal file 'statiMembri.txt'
        /// La richiamo in 'registraUtente.aspx' per andare a popolare
        /// la drop down list degli stati membri.
        /// </summary>
        /// <returns></returns>
        public List<string> GetStatiMembriUE()
        {

            // Dichiaro il nome del file (comprensivo del percorso, in questo caso si trova nella stessa directory del progetto)
            //  string file = "~/statiMembri.txt";
            string file = HttpContext.Current.Server.MapPath("~/statiMembri.txt");


            // Se il file esiste, allora fai le operazioni che vuoi
            // if (File.Exists(file)
            // {
            // Lettore di stream (flussi-->sequenze di byte--> che derivano da un supporto di memorizzazione di massa)
            using (StreamReader leggi = new StreamReader(file))
            {

                // Stringa che legge le stringhe di ogni riga del file
                string riga = "";

                // Gestione eccezioni
                try
                {
                    // Lettura: Finchè hai ancora da leggere qualcosa. (Se arrivi alla fine del file è false)
                    while (!leggi.EndOfStream) // leggi.EndOfStream != false
                    {
                        // Leggo il contenuto della riga del file e lo salvo in riga
                        riga = leggi.ReadLine();

                        // Aggiungo alla lista di stringhe 'stati'
                        statiMembri.Add(riga);
                    }
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
                    leggi.Close();
                }

            } // fine using

            // } // fine if esistenza file
            /*
            else
            {
                // Crea il file ecc....
            }
            */

            // Ritorno la lista degli stati
            return statiMembri;
        }






        /// <summary>
        /// Ritorna la lista degli username di tutti gli utenti.
        /// </summary>
        /// <returns></returns>
        public List<string> GetUsernameUtenti()
        {
            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'utenti'
            string selectSQL = "select distinct UsernameUtente from utenti order by UsernameUtente";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(selectSQL, con);

            // Leggo le righe (in modo forward-only) dal database
            SqlDataReader dr = cmd.ExecuteReader();

            // 
            if (dr != null)
            {
                // Finchè leggi una riga (un record) dal database
                while (dr.Read())
                {
                    // Crea un nuovo elemento di tipo stringa da aggiungere alla lista 
                    string username = "";

                    // Leggi e converti i dati dal record corrente
                    username = dr["UsernameUtente"].ToString();

                    // Aggiungi il capo alla lista
                    usernameUtenti.Add(username);

                }
            }
            // Chiudo la connessione
            con.Close();

            // Ritorno la lista ordinata
            usernameUtenti.Sort();
            return usernameUtenti;
        }







        /// <summary>
        /// Funzione da richiamare in ogni pagina, prima del Page_Load.
        /// Controlla il ruolo attuale nell'oggetto Session.
        /// Da richiamare solo nelle pagine in cui voglio che siano accedute 
        /// solo da UTENTI NON LOGGATI.
        /// </summary>
        /// <param name="Ruolo">Valore di Session["ruolo"]</param>
        public void GetAccessoPaginaUtenteComeNonLoggato(string Ruolo)
        {
            if ((Ruolo.Equals("amministratore") == false) && (Ruolo.Equals("utente") == false))
            {
                // Accesso consentito
                return;
            }
            else
            {
                // Accesso negato, vengo rimandato alla pagina di errore
                HttpContext.Current.Response.Redirect("AccessoNegato.aspx");

            }

        }


        /// <summary>
        /// Funzione da richiamare in ogni pagina, prima del Page_Load.
        /// Controlla il ruolo attuale nell'oggetto Session.
        /// Da richiamare solo nelle pagine in cui voglio che siano accedute 
        /// solo da UTENTI LOGGATI.
        /// </summary>
        /// <param name="Ruolo">Valore di Session["ruolo"]</param>
        public void GetAccessoPaginaComeUtente(string Ruolo)
        {
            if (Ruolo.Equals("utente"))
            {
                // Accesso consentito
                return;
            }
            else
            {
                // Accesso negato, vengo rimandato alla pagina di errore
                HttpContext.Current.Response.Redirect("AccessoNegato.aspx");

            }

        }


        /// <summary>
        /// Funzione da richiamare in ogni pagina, prima del Page_Load.
        /// Controlla il ruolo attuale nell'oggetto Session.
        /// Da richiamare solo nelle pagine in cui voglio che siano accedute 
        /// solo da AMMINISTRATORI LOGGATI.
        /// </summary>
        /// <param name="Ruolo">Valore di Session["ruolo"]</param>
        public void GetAccessoPaginaComeAmministratore(string Ruolo)
        {
            if (Ruolo.Equals("amministratore"))
            {
                // Accesso consentito
                return;
            }
            else
            {
                // Accesso negato, vengo rimandato alla pagina di errore
                HttpContext.Current.Response.Redirect("AccessoNegato.aspx");

            }

        }

        /* Accesso alle pagine con più utenti "sincrono" */
        public void GetAccessoPagina_Utente_Ed_Amministratore(string Ruolo)
        {
            if (Ruolo.Equals("utente") || Ruolo.Equals("amministratore"))
                return;
            else
                HttpContext.Current.Response.Redirect("AccessoNegato.aspx");
        }

        public void GetAccessoPagina_Utente_Ed_UtenteNonLoggato(string Ruolo)
        {
            // Se il ruolo non è uguale ad amministratore, allora può essere uguale a tutte le altre stringhe,
            // (compreso 'utente' e '""') basta appunto che non sia 'amministratore'
            if ((Ruolo.Equals("amministratore") == false))
                return;
            else
                HttpContext.Current.Response.Redirect("Errore.aspx");
        }

        public void GetAccessoPagina_Amministratore_Ed_UtenteNonLoggato(string Ruolo)
        {
            // Se il ruolo non è uguale ad utente, allora può essere uguale a tutte le altre stringhe,
            // (compreso 'amministratore' e '""') basta appunto che non sia 'utente'
            if ((Ruolo.Equals("utente") == false))
                return;
            else
                HttpContext.Current.Response.Redirect("AccessoNegato.aspx");
        }



    } // fine classe

} // fine namespace