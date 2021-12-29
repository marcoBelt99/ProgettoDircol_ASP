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
    public class PuntoVendita
    {

        // ATTRIBUTI
        public int CodPV { get; set; }
        public string Indirizzo { get; set; }
        public string Telefono { get; set; }
        public string Citta { get; set; }
        public string DataInizio { get; set; } // Nel costruttore è una data, convertita poi in stringa
        public string Nazione { get; set; }

        // COSTRUTTORE VUOTO
        public PuntoVendita()
        {
            this.CodPV = 0;
            this.Indirizzo = "";
            this.Telefono = "";
            this.Citta = "";
            this.DataInizio = new DateTime().ToString("yyyy/MM/dd");
            this.Nazione = "";
        }

        // COSTRUTTORE CHE SETTA I PARAMETRI
        public PuntoVendita(int CodPV, string Indirizzo, string Telefono,
                     string Citta, DateTime DataInizio, string Nazione)
        {
            this.CodPV = CodPV;
            this.Indirizzo = Indirizzo;
            this.Telefono = Telefono;
            this.Citta = Citta;
            this.DataInizio = DataInizio.ToString("yyyy/MM/dd");
            this.Nazione = Nazione;
        }


        // METODI
        /// <summary>
        /// Ritorna la lista di capi andando a leggere dal Database
        /// impostato nella stringa di connessione.
        /// Questo metodo mi serve per la VISUALIZZAZIONE dei dati dal Database.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <returns></returns>
        public List<PuntoVendita> GetPuntiVendita(string connectionString)
        {
            // Dichiaro la lista che poi dovrò ritornare
            List<PuntoVendita> listaPuntiVendita = new List<PuntoVendita>();

            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'capi'
            string selectSQL = "select * from puntivendita";

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
                    // Crea un nuovo oggetto di tipo PuntoVendita
                    PuntoVendita puntovendita = new PuntoVendita();

                    // Leggi e converti i dati dal record corrente
                    puntovendita.CodPV = Convert.ToInt32(dr["CodPV"]);
                    puntovendita.Indirizzo = dr["Indirizzo"].ToString();
                    puntovendita.Telefono = dr["Telefono"].ToString();
                    puntovendita.Citta = dr["Citta"].ToString();
                    puntovendita.DataInizio = Convert.ToDateTime(dr["DataInizio"]).ToString("yyyy/MM/dd");
                    puntovendita.Nazione = dr["Nazione"].ToString();

                    // Aggiungi il capo alla lista
                    listaPuntiVendita.Add(puntovendita);

                }
            }

            // Ritorno la lista di puntovendita
            return listaPuntiVendita;
        }


        /// <summary>
        /// Metodo di inserimento di un capo nella tabella puntivendita.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <param name="capo">oggetto da inserire nella tabella</param>
        public void InserisciPuntoVendita(string connectionString, PuntoVendita puntovendita)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Query parametrizzata
                    string strSQL = "INSERT INTO puntivendita (Indirizzo, Telefono, Citta, DataInizio, Nazione) " +
                        "VALUES (@Indirizzo, @Telefono, @Citta, @DataInizio, @Nazione);";

                    SqlCommand cmd = new SqlCommand(strSQL, con);

                    // Creo sia un comando di test, di sicuro non una stored procedure
                    cmd.CommandType = CommandType.Text;

                    // Aggiungo i parametri che ho usato nella query
                    cmd.Parameters.Add(new SqlParameter("@Indirizzo", puntovendita.Indirizzo));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", puntovendita.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@Citta", puntovendita.Citta));
                    cmd.Parameters.Add(new SqlParameter("@DataInizio", puntovendita.DataInizio));
                    cmd.Parameters.Add(new SqlParameter("@Nazione", puntovendita.Nazione));

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