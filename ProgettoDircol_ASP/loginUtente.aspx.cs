using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ProgettoDircol_ASP.Dati; // Per accedere alle classi
using System.Data.SqlClient; // Per connessione al Database
using System.Data;


namespace ProgettoDircol_ASP
{
    public partial class loginUtente : System.Web.UI.Page
    {


        Operazione op = new Operazione();




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAccedi_Click(object sender, EventArgs e)
        {

            try
            {
                // Connessione al Database
                using (SqlConnection con = new SqlConnection(op.GetConnectionString()))
                {

                    // Verico che la connessione sia effettivamente aperta
                    if (con.State == ConnectionState.Closed)
                        con.Open();


                    // Stringa istruzione SQL. Trim() rimuove gli spazi bianchi
                    string strSQL = "SELECT * FROM utenti " +
                        "WHERE UsernameUtente ='"+txtUsernameUtente.Text.Trim()+"' AND PasswordUtente='"+txtPasswordUtente.Text.Trim()+"';";

                    // Preparazione comando
                    SqlCommand cmd = new SqlCommand(strSQL, con);

                    // Uso un SqlDataReader che consente di leggere un flusso forward-only
                    // di righe da un database SQL Server
                    /* La query è eseguita 'cmd.ExecuteReader()' ed il 'dr' si connette al
                     * database e legge il valore di ritornato della query */
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Controllo se la query ha ottenuto o no un record
                    if (dr.HasRows)
                    {
                        // Finchè il dr sta leggendo dei record dal Database
                        // Notare che non sto usando un SqlDataAdapter insieme ad una DataTable,
                        // ma sto puntando direttamente ai records nel Database.
                        // dr.GetValue(numero di colonna della tabella nel database); si parte da 0
                        while (dr.Read())
                        {
                            Response.Write("<script>alert('Login avvenuto correttamente!')</script>");

                            /* Uso della variabile di sessione Session["chiave"] = valore */
                            Session["username"] = dr.GetValue(0).ToString();
                            Session["nome"] = dr.GetValue(2).ToString();
                            Session["cognome"] = dr.GetValue(3).ToString();
                            // Il ruolo di questo utente è: utente
                            Session["ruolo"] = "utente";
                            // Lo stato dell'account può essere cambiato da: pending ad active solo dall'amministratore
                            Session["stato"] = dr.GetValue(11).ToString();
                        }

                        // Una volta loggato, rimando l'utente alla HomePage del sito
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Nessun utente trovato con quello username')</script>");
                    }

                    // Chiudo la connessione
                    // con.Close();
                }


            }
            catch(SqlException ex)
            {
                Response.Write("<script> alert('Eccezione SQL: '" + ex.Message + ");</script>");
            }
            catch (Exception  ex)
            {
                // Response.Write("<script> alert('');</script>")
                throw ex;
            }

        }

        protected void btnRegistrati_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/registrazioneUtente.aspx", false);
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            /* La fase 'Start' è completata e le proprietà di Page sono
            * state caricate e sto per entrare nella fase 'Initialization'
            * Ho ora l'accesso a proprietà come "Page.IsPostBack */
            if (Session["ruolo"] != null)
                op.GetAccessoPaginaUtenteComeNonLoggato(Session["ruolo"].ToString());
            else
            {
                Response.Redirect("Errore.aspx");
            }
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            pnLoginUtente.Style.Add("padding", "50px");
        }



    }
}