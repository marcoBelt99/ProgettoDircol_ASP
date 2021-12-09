using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgettoDircol_ASP
{
    public partial class SalveMondoSingle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // All' evento click sul bottone di id btnSaluta, si scatena l'esecuzione di questa procedura
        protected void btnSaluta_Click(object sender, EventArgs e)
        {
            lblSaluto.Text = "Salve, Mondo !";
        }
    }
}

/*  Separazione del layout della pagina dal codice esecutivo che ne governa il 
    comportamento
*/


