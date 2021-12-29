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
    public class Vendita
    {
        // ATTRIBUTI
        public int ID { get; set; }
        public string DataVendita { get; set; }
        public double PrezzoVendita { get; set; }
        public string Matricola { get; set; }
        public int IDCapo { get; set; }

        // COSTRUTTORE VUOTO
        public Vendita()
        {
            this.ID = 0;
            this.DataVendita = new DateTime().ToString("yyyy/MM/dd");
            this.PrezzoVendita = 0.0;
            this.Matricola = "";
            this.IDCapo = 0;
        }

        // COSTRUTTORE CHE SETTA I PARAMETRI
        public Vendita(int ID, DateTime DataVendita, double PrezzoVendita,
                        string Matricola, int IDCapo)
        {
            this.ID = ID;
            this.DataVendita = DataVendita.ToString("yyyy/MM/dd");
            this.PrezzoVendita = PrezzoVendita;
            this.Matricola = Matricola;
            this.IDCapo = IDCapo;

        }


        // METODI
        /// <summary>
        /// Ritorna la lista delle vendite andando a leggere dal Database
        /// impostato nella stringa di connessione.
        /// Questo metodo mi serve per la VISUALIZZAZIONE dei dati dal Database.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <returns></returns>
        public List<Vendita> GetVendite(string connectionString)
        {
            // Dichiaro la lista che poi dovrò ritornare
            List<Vendita> listaVendite = new List<Vendita>();

            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'vendite'
            string selectSQL = "select * from vendite";

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
                    // Crea un nuovo oggetto di tipo Vendita
                    Vendita vendita = new Vendita();

                    // Leggi e converti i dati dal record corrente
                    vendita.ID = Convert.ToInt32(dr["ID"]);
                    vendita.DataVendita = Convert.ToDateTime(dr["DataVendita"]).ToString("yyyy/MM/dd");
                    vendita.PrezzoVendita = Convert.ToDouble(dr["PrezzoVendita"]);
                    vendita.Matricola = dr["Matricola"].ToString();
                    vendita.IDCapo = Convert.ToInt32(dr["IDCapo"]);

                    // Aggiungi il capo alla lista
                    listaVendite.Add(vendita);

                }
            }

            // Ritorno la lista di capi
            return listaVendite;
        }



        /// <summary>
        /// Metodo di inserimento di un vendita nella tabella vendite.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <param name="vendita">oggetto da inserire nella tabella</param>
        public void InserisciVendita(string connectionString, Vendita vendita)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Query parametrizzata: DataVendita, PrezzoVendita, Matricola, IDCapo
                    string strSQL = "INSERT INTO vendite (DataVendita, PrezzoVendita, Matricola, IDCapo) " +
                        "VALUES (@DataVendita, @PrezzoVendita, @Matricola, @IDCapo);";

                    SqlCommand cmd = new SqlCommand(strSQL, con);

                    // Creo sia un comando di test, di sicuro non una stored procedure
                    cmd.CommandType = CommandType.Text;

                    // Aggiungo i parametri che ho usato nella query
                    cmd.Parameters.Add(new SqlParameter("@DataVendita", vendita.DataVendita));
                    cmd.Parameters.Add(new SqlParameter("@PrezzoVendita", vendita.PrezzoVendita));
                    cmd.Parameters.Add(new SqlParameter("@Matricola", vendita.Matricola));
                    cmd.Parameters.Add(new SqlParameter("@IDCapo", vendita.IDCapo));

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