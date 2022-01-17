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
    /// Ricorda: per mantenere lo stato tra un post back e l'altro, uso il View State.
    /// Se salvo una lista nel View State, il tipo di classe della lista deve essere serializzabile
    /// </summary>
    [Serializable]
    public class Capo
    {
        // ATTRIBUTI
        public int ID { get; set; }
        public string Taglia { get; set; }
        public string Colore { get; set; }
        public int PuntoVendita { get; set; }
        public int CodModello { get; set; }

        // COSTRUTTORE VUOTO
        public Capo()
        {
            this.Taglia = "";
            this.Colore = "";
            this.PuntoVendita = 0;
            this.CodModello = 0;
        }

        // COSTRUTTORE CHE SETTA I PARAMETRI
        public Capo(string Taglia, string Colore,
                     int PuntoVendita, int CodModello)
        {
            this.Taglia = Taglia;
            this.Colore = Colore;
            this.PuntoVendita = PuntoVendita;
            this.CodModello = CodModello;
        }


        Operazione op = new Operazione();


        // METODI
        /// <summary>
        /// Ritorna la lista di capi andando a leggere dal Database
        /// impostato nella stringa di connessione.
        /// Questo metodo mi serve per la VISUALIZZAZIONE dei dati dal Database.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <returns></returns>
        public List<Capo> GetCapi(string connectionString)
        {
            // Dichiaro la lista che poi dovrò ritornare
            List<Capo> listaCapi = new List<Capo>();

            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'capi'
            string selectSQL = "select * from capi";

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
                    // Crea un nuovo oggetto di tipo Capo
                    Capo capo = new Capo();

                    // Leggi e converti i dati dal record corrente
                    capo.ID = Convert.ToInt32(dr["ID"]);
                    capo.Taglia = dr["Taglia"].ToString();
                    capo.Colore = dr["Colore"].ToString();
                    capo.PuntoVendita = Convert.ToInt32(dr["PuntoVendita"]);
                    capo.CodModello = Convert.ToInt32(dr["CodModello"]);

                    // Aggiungi il capo alla lista
                    listaCapi.Add(capo);

                }
            }

            // Ritorno la lista di capi
            return listaCapi;
        }


        /// <summary>
        /// Metodo di inserimento di un capo nella tabella capi.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <param name="capo">oggetto da inserire nella tabella</param>
        public void InserisciCapo(string connectionString, Capo capo)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Query parametrizzata
                    string strSQL = "INSERT INTO capi (Taglia, Colore, PuntoVendita, CodModello) " +
                        "VALUES (@Taglia, @Colore, @PuntoVendita, @CodModello);";

                    SqlCommand cmd = new SqlCommand(strSQL, con);

                    // Creo sia un comando di test, di sicuro non una stored procedure
                    cmd.CommandType = CommandType.Text;

                    // Aggiungo i parametri che ho usato nella query
                    cmd.Parameters.Add(new SqlParameter("@Taglia", capo.Taglia));
                    cmd.Parameters.Add(new SqlParameter("@Colore", capo.Colore));
                    cmd.Parameters.Add(new SqlParameter("@PuntoVendita", capo.PuntoVendita));
                    cmd.Parameters.Add(new SqlParameter("@CodModello", capo.CodModello));

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











        /// <summary>
        /// Eliminazione record da tabella capi del database
        /// </summary>
        public void EliminaCapo(int ID_da_Eliminare)
        {
            // Dichiaro una variabile per la connessione
            using (SqlConnection con = new SqlConnection(op.GetConnectionString()))
            {
                // Apro la connessione
                con.Open();

                // Stringa SQL: Seleziona tutti i dati dalla tabella 'capi'
                string selectSQL = "DELETE FROM capi WHERE ID=@id;";
                // Imposto il comando SQL
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                // Aggiungi al comando il parametro immesso nella query 
                cmd.Parameters.Add(new SqlParameter("@id", ID_da_Eliminare));

                // Esegui la query
                cmd.ExecuteNonQuery();
                // Chiudo la connessione al DB
                con.Close();
            }
        }




        public override string ToString()
        {
            return this.ID + "\t" + this.Taglia + "\t"
                + this.Colore + "\t" + this.PuntoVendita + "\t" + this.CodModello;
        }


    } // fine classe
} // fine namespace