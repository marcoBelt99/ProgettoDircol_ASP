using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgettoDircol_ASP.STUDIO
{
    public partial class GestioneViewState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se non è la prima volta che visiti la pagina (hai effettuato almeno un post back)
            if (Page.IsPostBack)
                ltPostBack.Text = "E' stato fatto il Post Back di questa pagina!" +
                    " Questo form è \"sticky (appiccicoso)\" ed i tuoi input sono stati" +
                    " caricati in un nuovo view state per la pagina!";
            else
                // Se è la prima volta che visiti la pagina (non ci sono ancora stati post back)
                ltPostBack.Text = "Riempi tutto il form. Non preoccuparti, i tuoi dati non saranno persi" +
                  " quando li invierai. Questo perchè abbiamo il View State!";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}