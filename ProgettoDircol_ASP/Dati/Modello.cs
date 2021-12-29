using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/* Aggiunta namespace che mi servono */
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace ProgettoDircol_ASP.Dati
{
    /// <summary>
    /// CLASSE CHE VA AD INTERAGIRE COL DATABASE:  LEGGE I DATI E LI SALVA PER POTERLI POI VISUALIZZARE,
    /// OPPURE FA OPERAZIONI DI CREAZIONE, MODIFICA, CANCELLAZIONE DI RECORD.
    /// I DATI VENGONO RACCOLTI DALLE PAGINE CODE BEHIND TRAMITE L'ACCESSO AI VALORI DEI CONTROLLI SERVER.
    /// I DATI VENGONO SPEDITI ALLA PAGINE CODE BEHIND TRAMITE DELLE LISTE
    /// </summary>
    public class Modello
    {
        // ATTRIBUTI
        public int CodModello { get; set; }
        public string Immagine { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public double PrezzoListino { get; set; }
        public char Genere { get; set; }
        public string Collezione { get; set; }

        // COSTRUTTORE VUOTO
        public Modello()
        {
            this.Immagine = "";
            this.Nome = "";
            this.Descrizione = "";
            this.PrezzoListino = 0.0;
            this.Genere = 'M'; // carattere
            this.Collezione = "";
        }

        // COSTRUTTORE CHE SETTA I PARAMETRI
        public Modello(string Immagine, string Nome, string Descrizione,
         double PrezzoListino, char Genere, string Collezione)
        {
            this.Immagine = Immagine;
            this.Nome = Nome;
            this.Descrizione = Descrizione;
            this.PrezzoListino = PrezzoListino;
            this.Genere = Genere;
            this.Collezione = Collezione;
        }


        // METODI
        /// <summary>
        /// Ritorna la lista di modelli andando a leggere dal Database
        /// impostato nella stringa di connessione.
        /// Questo metodo mi serve per la VISUALIZZAZIONE dei dati dal Database.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <returns></returns>
        public List<Modello> GetModelli(string connectionString)
        {
            // Dichiaro la lista che poi dovrò ritornare
            List<Modello> listaModelli = new List<Modello>();

            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'modelli'
            string selectSQL = "select * from modelli";

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
                    // Crea un nuovo oggetto di tipo Modello
                    Modello modello = new Modello();

                    // Leggi e converti i dati dal record corrente
                    modello.CodModello = Convert.ToInt32(dr["CodModello"]);
                    modello.Immagine = dr["Immagine"].ToString();
                    modello.Nome = dr["Nome"].ToString();
                    modello.Descrizione = dr["Descrizione"].ToString();
                    modello.PrezzoListino = Convert.ToDouble(dr["PrezzoListino"]);
                    modello.Genere = Convert.ToChar(dr["Genere"]);
                    modello.Collezione = dr["Collezione"].ToString();


                    // Aggiungi il modello alla lista
                    listaModelli.Add(modello);

                }
            }

            // Ritorno la lista di modelli
            return listaModelli;
        }



        /// <summary>
        /// Metodo di inserimento di un modello nella tabella modelli.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <param name="capo">oggetto da inserire nella tabella</param>
        public void InserisciCapo(string connectionString, Modello modello)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Query parametrizzata
                    string strSQL = "INSERT INTO modelli (Immagine, Nome, Descrizione, " +
                        "PrezzoListino, Genere, Collezione) " +
                        "VALUES (@Immagine, @Nome, @Descrizione, " +
                        "@PrezzoListino, @Genere, @Collezione);";

                    SqlCommand cmd = new SqlCommand(strSQL, con);

                    // Creo sia un comando di test, di sicuro non una stored procedure
                    cmd.CommandType = CommandType.Text;

                    // Aggiungo i parametri che ho usato nella query
                    cmd.Parameters.Add(new SqlParameter("@Immagine", modello.Immagine));
                    cmd.Parameters.Add(new SqlParameter("@Nome", modello.Nome));
                    cmd.Parameters.Add(new SqlParameter("@Descrizione", modello.Descrizione));
                    cmd.Parameters.Add(new SqlParameter("@PrezzoListino", modello.PrezzoListino));
                    cmd.Parameters.Add(new SqlParameter("@Genere", modello.Genere));
                    cmd.Parameters.Add(new SqlParameter("@Collezione", modello.Collezione));

                    // Apro la connessione
                    con.Open();

                    // Eseguo la query di inserimento
                    cmd.ExecuteNonQuery();

                    // chiudo la connessione
                    con.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    } // fine classe
} // fine namespace