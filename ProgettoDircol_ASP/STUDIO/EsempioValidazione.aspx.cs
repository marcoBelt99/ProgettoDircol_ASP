using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgettoDircol_ASP.STUDIO
{
    public partial class EsempioValidazione : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            /* Senza questo controllo, se l'utente disabilita javascript
             * non ho garanzia che la pagina sia valida*/
            if (Page.IsValid)
            {
                ltMessaggio.Text = "La Pagina è valida.";
            }
            else
            {
                /* Se la pagina non è valida, rendo visibile il  */
                valSummaryForm.Visible = true;
            }
        }

    }
}