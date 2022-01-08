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

        // Attributo lista di feedback di questo utente
        // Lo uso per contenere tutti i feedback di questo specifico utente
        // Posso anche pensare una soluzione con le Hash Table

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
        }


        // METODI
        /// <summary>
        /// Ritorna la lista degli utenti registrati all'applicazione web andando a leggere dal Database
        /// impostato nella stringa di connessione.
        /// Questo metodo mi serve per la VISUALIZZAZIONE dei dati dal Database.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <returns></returns>
        public List<UtenteOrdinario> GetUtenteOrdinario(string connectionString)
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
                    utenteOrdinario.Telefono =dr["TelefonoUtente"].ToString();
                    utenteOrdinario.Citta = dr["CittaUtente"].ToString();
                    utenteOrdinario.Indirizzo= dr["IndirizzoUtente"].ToString();
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
    }
}