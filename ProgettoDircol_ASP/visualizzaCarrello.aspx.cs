using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.Sql;


// Per accedere alle classi
using ProgettoDircol_ASP.Dati;

using System.IO;

namespace ProgettoDircol_ASP
{
    public partial class visualizzaCarrello : System.Web.UI.Page
    {
        /* ########### VARIABILI GLOBALI ################# */

        Operazione op = new Operazione();

        /// <summary>
        /// Dizionario che serve per salvarmi i capi contenuti nel file di testo 'CapiAggiuntiAlCarrello.txt'.
        /// Questo perchè: se aggiungo un capo al carrello, devo rimouverlo dalla tabella capi (altrimenti
        /// ho problemi nella pagina gestioneModelli), ma, se decido di rimuovere il capo, e riaggiungerlo alla
        /// tabella dei capi, senza l'ausilio del file.txt non riuscirei a ri-ottenere il capo.
        /// Leggo dunque ogni riga del file di testo e la salvo in un dizionario che poi mi servirà per fare
        /// delle elaborazioni di "join" con gli altri dati analoghi, e per recuperare le informazioni.
        /// ########## USO DEI DIZIONARI: sono coppie (Key, Value) ###########
        /// Data la chiave K --> ottengo il valore V
        /// /// Nel mio caso: K= variabile intera ID, V=Oggetto di tipo Capo
        /// 1) Aggiunta: dizionario.Add(K,V)
        /// 2) Accesso agli elementi (ottenere il valore data la chiave): dizionario[K]
        /// 3) Aggiornamento: dizionario[K] = V
        /// </summary>
        public Dictionary<int, Capo> Capi_Non_Disponibili = new Dictionary<int, Capo>();

        // File dal quale andrò a leggere e scrivere
        protected string NomeFileCapiAggiuntiAlCarrello = HttpContext.Current.Server.MapPath("~/CapiAggiuntiAlCarrello.txt");

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

            // Inizializzo al Page_PreInit la lista dei capi disponibili
            //capiclassificati = op.GetCapiClassificatiDaUnModello(CodiceDelModelloInDettaglio);
            //capiclassificatiTest = GetCapiClassificati_1();

        }


        protected void Page_Load(object sender, EventArgs e)
        {


        }

        public void Lettura_File_CapiAggiunti_e_Popolamento_Dizionario()
        {

            try
            {
                using (StreamReader sr = new StreamReader(this.NomeFileCapiAggiuntiAlCarrello))
                {
                    // Leggi la prima linea
                    string line = sr.ReadLine();

                    while (line != null)
                    {
                        // Suddivido la riga corrente e recupero le stringhe
                        string[] tok = line.Split('\t');
                        // Contatore di stringhe contenute in tok[]
                        int i = 0;
                        // Variabile che conterrà il capo
                        Capo capo = null;
                        // Finchè la 
                        while (i < tok.Length)
                        {
                            // Leggo e parsifico
                            int ID = int.Parse(tok[i]);
                            string Taglia = tok[i + 1];
                            string Colore = tok[i + 2];
                            int PuntoVendita = int.Parse(tok[i + 3]);
                            int CodModello = int.Parse(tok[i + 4]);

                            // Costruisco il capo
                            capo = new Capo(ID, Taglia, Colore, PuntoVendita, CodModello);

                            // Lo aggiungo al Dizionario
                            Capi_Non_Disponibili.Add(ID, capo);

                            // Faccio terminare il while facendo assumere ad i un valore > di tok.Lenght
                            i = tok.Length + 1;
                        } // fine while tok
                        // Leggo la prossima linea
                        line = sr.ReadLine();
                    } // fine while principale

                }
                Dispose();
            }
            catch (IOException ex)
            {
                Console.Write(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                // Libero il file, così il SO può far accedere altri processi al file
                // Inoltre, resituisco le risorse aperte precedentemente

            }
        }
        protected void btnRimuovi_Click(object sender, EventArgs e)
        {
            // Leggo da file i capi aggiunti e me li salvo nel Dizionario
            Lettura_File_CapiAggiunti_e_Popolamento_Dizionario();




        }
    }
}