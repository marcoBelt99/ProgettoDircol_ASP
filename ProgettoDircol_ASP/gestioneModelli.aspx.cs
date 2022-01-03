using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using ProgettoDircol_ASP.Dati; // Per accedere alle classi

namespace ProgettoDircol_ASP
{
    public partial class gestioneModelli : System.Web.UI.Page
    {
        Operazione op = new Operazione();



        protected void Page_PreInit(object sender, EventArgs e)
        {
            /* La fase 'Start' è completata e le proprietà di Page sono
            * state caricate e sto per entrare nella fase 'Initialization'
            * Ho ora l'accesso a proprietà come "Page.IsPostBack */
            if (Session["ruolo"] != null)
                op.GetAccessoPaginaComeUtente(Session["ruolo"].ToString());
            else
            {
                Response.Redirect("Errore.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}