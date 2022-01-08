using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

using ProgettoDircol_ASP.Dati;

namespace ProgettoDircol_ASP
{
    public partial class dettaglioModello : System.Web.UI.Page
    {

        Operazione op = new Operazione();

        // Variabile globale
        int CodiceDelModelloInDettaglio = 0;


        /// <summary>
        /// Metodo GetModello relativo al FormControl nel file dettagliModello
        /// </summary>
        /// <param name="codModello"></param>
        /// <returns></returns>
        public IQueryable<Modello> GetModello([QueryString("codModello")] int? codModello)
        {
            var TabellaModelli = new Modello();
            IQueryable<Modello> query = TabellaModelli.GetModelli(op.GetConnectionString()).AsQueryable();
            if (codModello.HasValue && codModello > 0)
            {
                query = query.Where(p => p.CodModello == codModello);
                // Recupero il codice del modello in dettaglio, per poter usare il suo valore nella list view successiva
                CodiceDelModelloInDettaglio = Convert.ToInt32(codModello);
            }
            else
            {
                query = null;
            }
            return query;

        }

        /// <summary>
        /// Metodo GetModello_1 , un'altra versione elegante, relativo al FormControl nel file dettagliModello
        /// </summary>
        /// <param name="codModello"></param>
        /// <returns></returns>
        public Modello GetModello_1([QueryString("codModello")] int? codModello)
        {
            Modello mod = new Modello();

            // Ricerco nella lista dei modelli quel modello di codice pari a codModello
            // p => [espressione booleana] è un predicato
            return mod.GetModelli(op.GetConnectionString()).Find(p => p.CodModello == codModello);
        }



        public List<Capo> GetCapiClassificati([QueryString("codModello")] int? codModello) // 
        {
            Capo capo = new Capo();

            List<Capo> ListaCapiClassificati = new List<Capo>();

            // Ricerco nella lista dei capi quel capo avente come CodModello pari a codModello
            // p => [espressione booleana] è un predicato
            ListaCapiClassificati.Add(capo.GetCapi(op.GetConnectionString()).Find(p => p.CodModello == this.GetModello_1(codModello).CodModello));

            return ListaCapiClassificati;
        }


        /// <summary>
        /// Necessaria per la List View
        /// </summary>
        /// <param name="codModello"></param>
        /// <returns></returns>
        public List<Capo> GetCapiClassificati_1() // 
        {
            Capo capo = new Capo();

            List<Capo> ListaCapiClassificati = new List<Capo>();

            foreach (var c in capo.GetCapi(op.GetConnectionString()))
            {
                if (c.CodModello == CodiceDelModelloInDettaglio)
                    ListaCapiClassificati.Add(c);
                else
                    continue;
            }

            return ListaCapiClassificati;
        }


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
            //fvDettagliModello.Style.Add("background-color", "white");
            //lvCapi.Style.Add("background-color", "white");
            lvCapi.Style.Add("padding-left", "50px");
            lvCapi.Style.Add("margin-left", "50px");
        }
    }
}