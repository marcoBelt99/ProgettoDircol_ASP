using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing; // Per il colore

namespace ProgettoDircol_ASP.STUDIO
{
    public partial class DataBindingSemplice : System.Web.UI.Page
    {
        List<Compagnia> compagnie;
        int indiceCompagnia;



        public string GetRagioneSociale()
        {
            Compagnia c = compagnie[indiceCompagnia];
            return c.GetRS();
        }

        public string GetBilancio()
        {
            Compagnia c = compagnie[indiceCompagnia];
            return c.GetB().ToString("#,0.00");
        }

        public Color GetColoreBilancio()
        {
            Compagnia c = compagnie[indiceCompagnia];
            return c.GetB() > 0 ? Color.Green : Color.Red;
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            compagnie = new List<Compagnia>();
            compagnie.Add(new Compagnia("A.C.M.E.", 200000));
            compagnie.Add(new Compagnia("Marte S.P.A.", -1500000));
            compagnie.Add(new Compagnia("Petroli", -24500000));
            if (!IsPostBack)
            {
                ViewState["IndiceCompagnia"] = 0;
                DataBind();
            }
            else
            {
                indiceCompagnia = Convert.ToInt32(ViewState["IndiceCompagnia"]);
            }
        }

        protected void btnPrec_Click(object sender, EventArgs e)
        {
            if (indiceCompagnia > 0)
                indiceCompagnia--;
            ViewState["IndiceCompagnia"] = indiceCompagnia;
            DataBind();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (indiceCompagnia <= 0)
                indiceCompagnia++;
            ViewState["IndiceCompagnia"] = indiceCompagnia;
            DataBind();
        }
    }
}
