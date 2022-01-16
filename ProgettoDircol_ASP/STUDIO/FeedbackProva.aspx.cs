using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


// Direttive aggiunte:
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using ProgettoDircol_ASP.Dati; // Per accedere a classe Operazioni.cs

namespace ProgettoDircol_ASP.STUDIO
{
    public partial class FeedbackProva : System.Web.UI.Page
    {
        Operazione op = new Operazione();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(op.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM recensioni"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            // Riempi la tabella
                            sda.Fill(dt);
                            gvRecensioni.DataSource = dt;
                            gvRecensioni.DataBind();
                        }
                    }
                }
            }
        } // Fine Page_Load()


        [WebMethod]
        [ScriptMethod]
        public static void AggiungiRecensione(Recensione recensione)
        {
            Operazione op = new Operazione();

            using (SqlConnection con = new SqlConnection(op.GetConnectionString()))
            {
                string strSQL = "INSERT INTO recensioni(DescrizioneRecensione, Punteggio, CodModello, UsernameUtente)" +
                    " VALUES(@DescrizioneRecensione, @Punteggio, @CodModello, @UsernameUtente)";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@DescrizioneRecensione", recensione.DescrizioneRecensione);
                    cmd.Parameters.AddWithValue("@Punteggio", recensione.PunteggioRecensione);
                    cmd.Parameters.AddWithValue("@CodModello", recensione.CodModello);
                    cmd.Parameters.AddWithValue("@UsernameUtente", recensione.UsernameUtente);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static void UpdateUser(string Username, string Password, int ID)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_tes"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Update Users SET Username=@Username,Password=@Password Where id=@id)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        [WebMethod]
        [ScriptMethod]
        public static void DeleteUser(int deleteUserID)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE id = @id"))
                {
                    cmd.Parameters.AddWithValue("@id", deleteUserID);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }


    }

}