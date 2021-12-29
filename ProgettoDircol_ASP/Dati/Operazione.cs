/* Aggiunta namespace che mi servono */
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System;
using System.Collections.Generic;
// Aggiungo riferimento per accedere al file Web.config
using System.Web.Configuration;



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

        /// Mi salvo la stringa di connessione presente in Web.config
        private readonly string connectionString = WebConfigurationManager.ConnectionStrings["dircolDB"].ConnectionString;
        /// Array di stringhe delle taglie
        private string[] taglie = { "M", "L", "S", "XL", "XS", "XXS", "XXL" };
        /// Stinga di controllo
        public string stringaDicontrollo = "---";
        /// Lista di interi che contiene i codici dei punti vendita
        private List<int> codiciPuntiVendita = new List<int>();
        /// Lista di interi che contiene i codici dei modelli di un capo
        private List<int> codiciModelli = new List<int>();
        /// Lista di interi che contiene gli ID dei capi
        private List<int> IDCapi = new List<int>();
        /// Lista di stringhe che contiene le matricole di tutti i dipendenti
        private List<string> matricoleDipendenti = new List<string>();


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



        // altre operazioni utili in futuro


    } // fine classe

} // fine namespace