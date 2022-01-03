using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ProgettoDircol_ASP.Dati; // Per accedere a classi (in particolare ad 'Operazione.cs')
using System.Data.SqlClient; // Per la connessione al database
using System.Data;

namespace ProgettoDircol_ASP
{
    public partial class registrazioneUtente : System.Web.UI.Page
    {

        Operazione op = new Operazione();


        /// <summary>
        /// Controlla se un Utente è già stato inserito nella tabella
        /// 'utenti' del Database (quindi, se è stato inserito uno username già esistente)
        /// </summary>
        /// <returns></returns>
        bool ControllaEsistenzaUtente()
        {

            try
            {
                SqlConnection con = new SqlConnection(op.GetConnectionString());
                if (con.State == ConnectionState.Closed)
                    con.Open();


                // Stringa comando SQL
                string strSQL = "SELECT * FROM utenti WHERE UsernameUtente='" + txtUsernameUtente.Text.Trim() + "';";


                // Preparazione comando SQL
                SqlCommand cmd = new SqlCommand(strSQL, con);


                // Chiudo la connessione al Database
                con.Close();

                // Preparazione SqlDataAdapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                // Rappresenta una tabella di dati in memoria
                // Serve per salvare i dati ritornati dalla query del comando SQL
                DataTable dt = new DataTable();

                // Salvo i record ottenuti dalla query nella DataTable 'dt'
                da.Fill(dt);

                // Se ho trovato almeno una riga nella tabella DataTable 'dt'
                if (dt.Rows.Count >= 1)
                    // Ho trovato un dato che metcha con la query
                    // True: è vero che esiste almeno un utente che ha quello username
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "');</script>");
                return false;
            }
        }


        /// <summary>
        /// Procedura di inserimento utente nella tabella 'utenti' del database
        /// </summary>
        void InserisciUtente()
        {
            try
            {
                // Connessione al Database
                using (SqlConnection con = new SqlConnection(op.GetConnectionString()))
                {

                    // Vedo se la connessione al DB è chiusa
                    if (con.State == ConnectionState.Closed)
                        // Se lo è, la apro
                        con.Open();

                    // string comando SQL
                    string strSQL = "INSERT INTO utenti (UsernameUtente, PasswordUtente, NomeUtente, " +
                        "CognomeUtente, DataNascitaUtente, EmailUtente, TelefonoUtente, CittaUtente, " +
                        "IndirizzoUtente, StatoUtente, CAPUtente, StatoAccount)" +
                        " VALUES(@Username, @Password, @Nome, @Cognome, @DataNascita, @Email, @Telefono, " +
                        "@Citta, @Indirizzo, @Stato, @CAP, @StatoAccount);";

                    // Comando SQL
                    SqlCommand cmd = new SqlCommand(strSQL, con);

                    // Aggiunta dei parametri al comando:
                    cmd.Parameters.AddWithValue("@Cognome", txtCognomeUtente.Text.Trim());
                    cmd.Parameters.AddWithValue("@Nome", txtNomeUtente.Text.Trim());
                    cmd.Parameters.AddWithValue("@DataNascita", txtDataNascitaUtente.Text.Trim());
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefonoUtente.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmailUtente.Text.Trim());
                    cmd.Parameters.AddWithValue("@Stato", ddlStatoUtente.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@Citta", txtCittaUtente.Text.Trim());
                    cmd.Parameters.AddWithValue("@CAP", txtCAPUtente.Text.Trim());
                    cmd.Parameters.AddWithValue("@Indirizzo", txtIndirizzoUtente.Text.Trim());
                    cmd.Parameters.AddWithValue("@Username", txtUsernameUtente.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPasswordUtente.Text.Trim());
                    /* La prima volta che un utente si registra al sito, lo stato del suo account è 'pending' */
                    cmd.Parameters.AddWithValue("@StatoAccount", "pending");

                    // Eseguo la query
                    cmd.ExecuteNonQuery();

                    // Chiudo la connessione
                    con.Close();

                    Response.Write("<script>alert('Registrazione avvenuta con successo." +
                        " Vai ad Login Utente per effettuare il login');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Registrazione fallita. '" + ex.Message + "' " +
                        " )</script>");
                throw ex;
            }
        }





        protected void btnRegistrati_Click(object sender, EventArgs e)
        {
            // Se l'utente esiste già <-- lo username è già presente
            if (ControllaEsistenzaUtente())
            {
                // Errore
                Response.Write("<script>alert('Lo Username inserito è già stato scelto; " +
                    "inseriscine un altro');</script>");
            }
            else
            {
                // Altrimenti ok, lo username non è ancora stato inserito --> Inserisci l'utente
                InserisciUtente();
            }


        } // fine btnRegistrati_Click

        protected void btnTornaHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            // Dò un po' di padding al form
            pnRegistrazioneUtente.Style.Add("padding", "50px");

            if (IsPostBack)
                return;

            // Drop down list 'ddlStatoUtente'
            ddlStatoUtente.DataSource = op.GetStatiMembriUE(); // Prendo i dati da file .txt
            ddlStatoUtente.DataBind();
            ddlStatoUtente.Items.Insert(0, new ListItem(op.stringaDicontrollo)); // Aggiungo un valore di default: "---"


        }



    }
}