using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ProgettoDircol_ASP.Dati;

namespace ProgettoDircol_ASP
{
    public partial class pagamento : System.Web.UI.Page
    {
        /// <summary>
        /// LISTA CHE CONTIENE I CAPI DEL CARRELLO DELL'UTENTE (CREATA IN 'visualizzaCarrello.aspx'
        /// E' STATA PRESERVATA GRAZIE ALL'USO DELL'OGGETTO APPLICATION
        /// </summary>
        public List<Capo> Capi_Utente = new List<Capo>();


        double PrezzoTotaleListino = 0.0;
        double PrezzoTrasporto = 0.0;

        Operazione op = new Operazione();



        /// <summary>
        /// NUMERI CASUALI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static Random rnd = new Random();

        double PrezzoTotaleVendita = 0.0;
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


            /**  USO DELLA VARIABILE APPLICATION PER MEMORIZZARE LE INFORMAZIONI DI UNA VARIABILE TRA PIU' PAGINE */
            Capi_Utente = (List<Capo>)Application["CAPI_UTENTE"];

            /**  PROVO AD OTTENERE IL PREZZO TOTALE DI LISTINO DALLA QUERY STRING  */
            bool isQuery = (Request.QueryString["totaleListino"] != null);

            if (isQuery)
            {
                string qs = "QueryString";
                PrezzoTotaleListino = double.Parse(Request.QueryString["totaleListino"]);

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Do' un po' di stile
            pnPagamento.Style.Add("padding", "50px");

            // Calcolo e stampo i valori della vendita
            txtTotaleListino.ReadOnly = true;
            txtPrezzoTrasporto.ReadOnly = true;
            txtIVA.ReadOnly = true;
            txtTotaleVendita.ReadOnly = true;

            Calcola_Prezzo_Di_Trasporto();
            double imponibile = PrezzoTotaleListino + PrezzoTrasporto;
            double IVA_22 = (imponibile * 0.22);

            txtTotaleListino.Text = PrezzoTotaleListino.ToString() + " €";
            txtPrezzoTrasporto.Text = PrezzoTrasporto.ToString() + " €";
            txtIVA.Text = (IVA_22).ToString();
            PrezzoTotaleVendita = (imponibile + IVA_22);
            txtTotaleVendita.Text = PrezzoTotaleVendita.ToString() + " €";

        }


        public void Calcola_Prezzo_Di_Trasporto()
        {
            if (PrezzoTotaleListino < 10.0)
                PrezzoTrasporto = 20;
            else if (PrezzoTotaleListino < 25.0)
                PrezzoTrasporto = 16;
            else if (PrezzoTotaleListino < 50.0)
                PrezzoTrasporto = 12;
            else if (PrezzoTotaleListino < 75)
                PrezzoTrasporto = 7;
            else if (PrezzoTotaleListino >= 100)
                PrezzoTrasporto = 4;
            else
                PrezzoTrasporto = 0.0;

            // Calcolo del prezzo di trasporto
            /*
            switch (PrezzoTotaleListino)
            {
                case < 10.00:
                    PrezzoTrasporto = 20;
                    break;
                case < 20:
                    PrezzoTrasporto = 15;
                    break;
                default:
                    break;
            }
            */

        }

        public double Calcola_Prezzo_Singolo(double listino)
        {

            double trasporto = listino / Capi_Utente.Count;
            double IVA22 = 0.22;
            double temp = (trasporto + listino) * IVA22;
            double TOT = listino + trasporto + temp;
            return TOT;
        }


        protected void btnConfermaPagamento_Click(object sender, EventArgs e)
        {

            List<Dipendente> Venditori = op.GetDipendentiVenditori();
            // Ottieni le matricole dei dipendenti
            List<string> Venditori_str = op.GetMatricoleDipendentiVenditori();
            List<int> Venditori_int = new List<int>();

            foreach (var s in Venditori_str)
            {
                int v = Convert.ToInt32(s);
                Venditori_int.Add(v);
            }

            // Scegli casualmente
            int matricola_casuale = rnd.Next(Venditori_int.Count);

            /// CICLO FOREACH DI INSERIMENTO DI OGNI SINGOLO CAPO DELLA LISTA 'CAPI_UTENTE' NELLA TABELLA DELLE TRANSAZIONI
            /// CHE MATRICOLA DI DIPENDENTE CI METTO? 
            /// 2 POSSIBILITA':
            /// 1] SCELGO CASUALE TRA L'ELENCO DEI DIPENDENTI CON QUALIFICA VENDITORE
            /// 2] SCELGO QUEL DIPENDETE CHE LAVORA NELLO STESSO PUNTO VENDITA DEL CAPO, SE NON C'E' ALLORA LO PRENDO CASUALE


            Dipendente venditore = new Dipendente();
            Modello modello = new Modello();

            // Scorri la lista dei capi utente
            foreach (var c in Capi_Utente)
            {

                /// Recupero il venditore in uno dei 2 modi
                // 1]
                venditore = Venditori.Find(x => x.PuntoVendita == c.PuntoVendita);
                if (venditore == null)
                {
                    // 2]
                    venditore = new Dipendente();
                    venditore.Matricola = matricola_casuale.ToString();
                }


                /// Recupero il prezzo del capo (del modello)
                double listino = modello.GetModelli(op.GetConnectionString()).Find(y => y.CodModello == c.CodModello).PrezzoListino;

                // Calcola il prezzo della transazione
                double PrezzoTransazione = Calcola_Prezzo_Singolo(listino);

                // Creo una nuova transazione per questo capo
                Transazione transazione = new Transazione();

                transazione.DataTransazione = DateTime.Now.ToString("yyyy/MM/dd");
                transazione.PrezzoTransazione = PrezzoTransazione;
                transazione.Matricola = venditore.Matricola;
                transazione.IDCapo = c.ID;
                transazione.UsernameUtente = Session["Username"].ToString();

                // La inserisco nella tabella del DB
                transazione.InserisciTransazione(op.GetConnectionString(), transazione);


                Response.Redirect("pagamento.aspx");

                /// ORA DEVO AGGIORNARE I DUE FILE: 'CapiAggiuntiAlCarrello.txt','Carrelli.JSON' ANDANDO A RIMUOVERE IL CAPO E L'ID DI QUESTO CAPO
            }



        }
    }
}



/** NUMERI CASUALI:
 * Create an instance of Random class somewhere. Note that it's pretty important not to create a new instance each time you need a random number. You should reuse the old instance to achieve uniformity in the generated numbers. You can have a static field somewhere (be careful about thread safety issues):

static Random rnd = new Random();
Ask the Random instance to give you a random number with the maximum of the number of items in the ArrayList:

int r = rnd.Next(list.Count);
Display the string:

MessageBox.Show((string)list[r]); 
 *
 *
 *
 */