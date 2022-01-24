using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgettoDircol_ASP
{
    public partial class pagamento : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            /* La fase 'Start' è completata e le proprietà di Page sono
            * state caricate e sto per entrare nella fase 'Initialization'
            * Ho ora l'accesso a proprietà come "Page.IsPostBack */
            //if (Session["ruolo"] != null)
            //    op.GetAccessoPaginaComeUtente(Session["ruolo"].ToString());
            //else
            //{
            //    Response.Redirect("Errore.aspx");
            //}

            // Inizializzo al Page_PreInit la lista dei capi disponibili
            //capiclassificati = op.GetCapiClassificatiDaUnModello(CodiceDelModelloInDettaglio);
            //capiclassificatiTest = GetCapiClassificati_1();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pnPagamento.Style.Add("padding", "50px");
        }
    }
}