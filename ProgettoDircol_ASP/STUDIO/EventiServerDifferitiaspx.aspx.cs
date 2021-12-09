using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgettoDircol_ASP.STUDIO
{
    public partial class EventiServerDifferitiaspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            chk1.ForeColor = Color.Black;
            chk2.ForeColor = Color.Black;
            chk3.ForeColor = Color.Black;
            txt.ForeColor = Color.Black;

        }

        protected void CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.ForeColor = Color.Red;
        }

        protected void TextChanged(object sender, EventArgs e)
        {
            txt.ForeColor = Color.Red;
        }
    }
}