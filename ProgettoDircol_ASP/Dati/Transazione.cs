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
    public class Transazione
    {
        // ATTRIBUTI
        public int ID { get; set; }
        public string DataTransazione { get; set; }
        public double PrezzoTransazione { get; set; }
        public string Matricola { get; set; }
        public int IDCapo { get; set; }
        public string UsernameUtente { get; set; }

        // COSTRUTTORE VUOTO
        public Transazione()
        {
            this.ID = 0;
            this.DataTransazione = new DateTime().ToString("yyyy/MM/dd");
            this.PrezzoTransazione = 0.0;
            this.Matricola = "";
            this.IDCapo = 0;
            this.UsernameUtente = "";

        }

        // COSTRUTTORE CHE SETTA I PARAMETRI
        public Transazione(int ID, DateTime DataTransazione, double PrezzoTransazione,
                        string Matricola, int IDCapo, string UsernameUtente)
        {
            this.ID = ID;
            this.DataTransazione = DataTransazione.ToString("yyyy/MM/dd");
            this.PrezzoTransazione = PrezzoTransazione;
            this.Matricola = Matricola;
            this.IDCapo = IDCapo;
            this.UsernameUtente = UsernameUtente;

        }


        // METODI
        /// <summary>
        /// Ritorna la lista delle transazioni andando a leggere dal Database
        /// impostato nella stringa di connessione.
        /// Questo metodo mi serve per la VISUALIZZAZIONE dei dati dal Database.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <returns></returns>
        public List<Transazione> GetTransazioni(string connectionString)
        {
            // Dichiaro la lista che poi dovrò ritornare
            List<Transazione> listaTranzazioni = new List<Transazione>();

            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'transazioni'
            string selectSQL = "select * from transazioni";

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
                    // Crea un nuovo oggetto di tipo Transazione
                    Transazione transazione = new Transazione();

                    // Leggi e converti i dati dal record corrente
                    transazione.ID = Convert.ToInt32(dr["ID"]);
                    transazione.DataTransazione = Convert.ToDateTime(dr["DataTransazione"]).ToString("yyyy/MM/dd");
                    transazione.PrezzoTransazione = Convert.ToDouble(dr["PrezzoTransazione"]);
                    transazione.Matricola = dr["Matricola"].ToString();
                    transazione.IDCapo = Convert.ToInt32(dr["IDCapo"]);
                    transazione.UsernameUtente = dr["UsernameUtente"].ToString();

                    // Aggiungi la transazione alla lista
                    listaTranzazioni.Add(transazione);

                }
            }

            // Ritorno la lista di capi
            return listaTranzazioni;
        }



        /// <summary>
        /// Metodo di inserimento di un transazione nella tabella transazioni.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <param name="transazione">oggetto da inserire nella tabella</param>
        public void InserisciTransazione(string connectionString, Transazione transazione)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Query parametrizzata: DataTransazione, PrezzoTransazione, Matricola, IDCapo
                    string strSQL = "INSERT INTO transazioni (DataTransazione, PrezzoTransazione, Matricola, IDCapo, UsernameUtente) " +
                        "VALUES (@DataTransazione, @PrezzoTransazione, @Matricola, @IDCapo, @UsernameUtente);";

                    SqlCommand cmd = new SqlCommand(strSQL, con);

                    // Creo sia un comando di test, di sicuro non una stored procedure
                    cmd.CommandType = CommandType.Text;

                    // Aggiungo i parametri che ho usato nella query
                    cmd.Parameters.Add(new SqlParameter("@DataTransazione", transazione.DataTransazione));
                    cmd.Parameters.Add(new SqlParameter("@PrezzoTransazione", transazione.PrezzoTransazione));
                    cmd.Parameters.Add(new SqlParameter("@Matricola", transazione.Matricola));
                    cmd.Parameters.Add(new SqlParameter("@IDCapo", transazione.IDCapo));
                    cmd.Parameters.Add(new SqlParameter("@UsernameUtente", transazione.UsernameUtente));

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