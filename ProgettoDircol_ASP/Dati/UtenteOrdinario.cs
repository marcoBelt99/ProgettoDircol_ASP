using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProgettoDircol_ASP.Dati
{

    /// <summary>
    /// Classe UtenteOrdinario che estende Utente.
    /// Rappresenta un Utente loggato.
    /// Può effettuare alcune operazioni, come:
    /// - Acquistare un capo.
    /// - Rilasciare una feedback relativo ad un capo acquistato
    /// </summary>
    public class UtenteOrdinario : Utente
    {
        // DateTime().ToString("yyyy/MM/dd");

        // ATTRIBUTI
        // Attributi specifici di questa classe
        public string DataNascita { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Citta { get; set; }
        public string Indirizzo { get; set; }
        public string Stato { get; set; }
        public string CAP { get; set; }
        public string StatoAccount { get; set; }

        /// <summary>
        /// Attributo lista di id di Capi
        /// Lo uso per contenere tutti i carrello degli utenti
        /// </summary>
        public LinkedList<int> ListaIDCapi;
        Operazione op = new Operazione();


        // COSTRUTTORE
        // Costruttore senza parametri
        public UtenteOrdinario() : base()
        {
            // Attributi ereditati
            this.Username = "";
            this.Password = "";
            this.Nome = "";
            this.Cognome = "";

            // Attributi specifici
            this.DataNascita = new DateTime().ToString("yyyy/MM/dd");
            this.Email = "";
            this.Telefono = "";
            this.Citta = "";
            this.Indirizzo = "";
            this.Stato = "";
            this.CAP = "";
            this.StatoAccount = "pending";

            /// Carrello acquisti (gli utenti acquistano i carrello)
            this.ListaIDCapi = new LinkedList<int>();

        }

        // Costruttore con parametri
        public UtenteOrdinario(string Username, string Password, string Nome, string Cognome,
                DateTime DataNascita, string Email, string Telefono, string Citta, string Indirizzo,
                string Stato, string CAP) : base(Username, Password, Nome, Cognome)
        {
            this.DataNascita = new DateTime().ToString("yyyy/MM/dd");
            this.Email = Email;
            this.Telefono = Telefono;
            this.Citta = Citta;
            this.Indirizzo = Indirizzo;
            this.Stato = Stato;
            this.CAP = CAP;
            this.StatoAccount = "pending";

            /// Lista ID di capi: serve per aggiungerla al carrello
            this.ListaIDCapi = new LinkedList<int>();
        }


        // METODI
        /// <summary>
        /// Ritorna la lista degli utenti registrati all'applicazione web andando a leggere dal Database
        /// impostato nella stringa di connessione.
        /// Questo metodo mi serve per la VISUALIZZAZIONE dei dati dal Database.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <returns></returns>
        public List<UtenteOrdinario> GetUtentiOrdinari(string connectionString)
        {
            // Dichiaro la lista che poi dovrò ritornare
            List<UtenteOrdinario> listaUtenti = new List<UtenteOrdinario>();

            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'utenti'
            string selectSQL = "select * from utenti";

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
                    // Crea un nuovo oggetto di tipo UtenteOrdinario
                    UtenteOrdinario utenteOrdinario = new UtenteOrdinario();

                    // Leggi e converti i dati dal record corrente
                    utenteOrdinario.Username = dr["UsernameUtente"].ToString();
                    utenteOrdinario.Password = dr["PasswordUtente"].ToString();
                    utenteOrdinario.Nome = dr["NomeUtente"].ToString();
                    utenteOrdinario.Cognome = dr["CognomeUtente"].ToString();
                    utenteOrdinario.DataNascita = Convert.ToDateTime(dr["DataNascitaUtente"]).ToString("yyyy/MM/dd");
                    utenteOrdinario.Email = dr["EmailUtente"].ToString();
                    utenteOrdinario.Telefono = dr["TelefonoUtente"].ToString();
                    utenteOrdinario.Citta = dr["CittaUtente"].ToString();
                    utenteOrdinario.Indirizzo = dr["IndirizzoUtente"].ToString();
                    utenteOrdinario.Stato = dr["StatoUtente"].ToString();
                    utenteOrdinario.CAP = dr["CAPUtente"].ToString();
                    utenteOrdinario.StatoAccount = dr["StatoAccount"].ToString();

                    // Aggiungi il capo alla lista
                    listaUtenti.Add(utenteOrdinario);
                }
            }

            // Ritorno la lista di utenti
            return listaUtenti;
        } // fine metodo


        /// <summary>
        /// Trova un Utente avente come username il parametro passato 'usernameChiave'.
        /// Mi serve per assicurarmi di star considerando l'utente della sessione corrente, loggato adesso
        /// </summary>
        /// <param name="usernameChiave"></param>
        /// <returns></returns>
        public UtenteOrdinario TrovaUtente(string usernameChiave)
        {
            // Scorri la lista degli utenti ordinari
            foreach (var u in this.GetUtentiOrdinari(op.GetConnectionString()))
            {
                // Se ho trovato l'utente che ha come username usernamrChiave
                if (u.Username.Equals(usernameChiave))
                {
                    // L'ho trovato e lo ritorno
                    return u;
                }
            }
            // Altrimenti, non trovato, ritorna null
            return null;
        }





        public void AggiungiAlCarrello(int ID)
        {
            // Inserisco in testa al carrello il capo scelto dall'utente
            ListaIDCapi.AddFirst(ID);
        }

        public void RimuoviDalCarrello(int ID)
        {
            // Rimuovi dal carrello quel determinato capo quel determinato id:
            // Rimuove la prima occorrenza avente come id ID
            ListaIDCapi.Remove(ID);
        }


        /// <summary>
        /// Ritorna la somma totale dei capi aggiunti al carrello, al netto delle eventuali altre
        /// spese che concorreranno a formare il prezzo di Transazione
        /// </summary>
        /// <returns></returns>
       /*
        public double Spesa_Di_Listino()
        {
            double spesa_di_listino = 0.0;

            // Ho bisogno del prezzo di listino. Il prezzo si trova nella lista dei modelli
            // Recupero la lista dei modelli
            Modello m = new Modello();
            List<Modello> ListaModelli = m.GetModelli(op.GetConnectionString());

            // Recupero la lista dei capi
            Capo c = new Capo();
            

            // Per tutti i capi nel carrello
            foreach (var capo in c.GetCapi(op.GetConnectionString()))
            {
                if()
                // Per tutti i modelli esistenti
                foreach (var modello in ListaModelli)
                {
                    // Se ho trovato il capo inserito
                    if (capo.CodModello == modello.CodModello)
                        spesa_di_listino += modello.PrezzoListino;
                    else
                        continue;
                }
            }
            return spesa_di_listino;
        }
       */
    }
}