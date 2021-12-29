using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ProgettoDircol_ASP.Dati; // Per accedere alle classi

namespace ProgettoDircol_ASP
{
    public partial class loginUtente : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            pnLoginUtente.Style.Add("padding", "50px");
        }
    }
}