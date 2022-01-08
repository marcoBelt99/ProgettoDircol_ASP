using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using ProgettoDircol_ASP.Dati; // Per accedere alle classi
using System.Data.SqlClient; // Per connessione al DB
using System.Web.ModelBinding; // Per usare le Query string

namespace ProgettoDircol_ASP
{
    public partial class gestioneModelli : System.Web.UI.Page
    {



        Operazione op = new Operazione();

        Capo capo = new Capo();


        /// <summary>
        /// Vettore che contiene tutti i codici dei modelli attualmente disponibili (che classificano almeno un capo)
        /// </summary>
        int[] CodiciModelliDisponibili = { 0 };

        /// <summary>
        /// Funzione che ritorna l'array di tutti i Codici dei modelli attualmente disponibili (che classificano almeno un capo).
        /// Effettua la query:
        /// "select distinct CodModello from capi order by CodModello;"
        /// </summary>
        /// <returns></returns>
        protected int[] GetCodiciModelliDisponibili()
        {

            // return op.GetCodiciModelli();
            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(op.GetConnectionString());

            // Stringa SQL: Seleziona tutti i codici dei modelli dalla tabella 'capi'
            string selectSQL = "select distinct CodModello from capi order by CodModello; ";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(selectSQL, con);

            // Leggo le righe (in modo forward-only) dal database
            SqlDataReader dr = cmd.ExecuteReader();

            // 
            if (dr != null)
            {
                int i = 0;
                // Finchè leggi una riga (un record) dal database
                while (dr.Read())
                {
                    // Crea un nuovo elemento di tipo intero da aggiungere alla lista 
                    int CodModello = 0;

                    // Leggi e converti i dati dal record corrente
                    CodModello = Convert.ToInt32(dr["CodModello"]);

                    // Aggiungi il capo alla lista
                    CodiciModelliDisponibili[i] = CodModello;
                    i++;

                }
            }

            // Chiudo la connessione
            con.Close();

            // Ritorno l'array
            return CodiciModelliDisponibili;
        }

        /// <summary>
        /// Funzione che ritorna la lista dei capi
        /// </summary>
        /// <returns></returns>
        protected List<Capo> GetCapi()
        {
            return capo.GetCapi(op.GetConnectionString());
        }



        // Metodo GetModelli a cui fa riferimento la proprietà ItemType del controllo ListView nella pagina
        // gestioneModelli.aspx
        public IQueryable<Modello> GetModelliDisponibili([QueryString("id")] int? codModello)
        {

            // 1) OTTENGO L'ORIGINE DATI --> Accedo alla classe dell'entità Modello
            var tabModelli = new ProgettoDircol_ASP.Dati.Modello();

            // Converto da lista dei modelli da List<> ad IQuerable<>, e salvo il risultato 
            IQueryable<Modello> ModelliDisponibili = tabModelli.GetModelli(op.GetConnectionString()).AsQueryable();

            // Se il parametro formale ha un valore (essendo un intero nullable int? ) e se è > 0 
            if (codModello.HasValue && codModello > 0)
            {
                // 2 CREAZIONE DELLA QUERY   

                /* SELECT * 
	                FROM modelli 
		                WHERE CodModello in (SELECT DISTINCT CodModello FROM capi); */
                ModelliDisponibili = ModelliDisponibili.Where(p => this.GetCodiciModelliDisponibili().Contains(p.CodModello));
                // query1 = query1.Where(p => p.CodModello == codModello).Distinct();  // select distinct CodModello from capi
                // prova = query.Where(p => p.CodModello == codModello).;
                // ModelliDisponibili = ModelliDisponibili.Where(p => p.CodModello == codModello);
            }

            // 3) ESECUZIONE DELLA QUERY / Ritorno questa query in un oggetto di tipo IQuerable<Modello>
            return ModelliDisponibili;
        }

        public List<Modello> GetModelliDisponibili_1([QueryString("id")] int? codModello)

        {
            Modello mod = new Modello();
            Capo capo = new Capo();

            // Lista di tutti i modelli
            List<Modello> ListaModelli = new List<Modello>();
            // Lista di tutti i capi
            List<Capo> ListaCapi = new List<Capo>();

            ListaModelli = mod.GetModelli(op.GetConnectionString());
            ListaCapi = capo.GetCapi(op.GetConnectionString());

            // Lista che conterrà solo i modelli disponibili
            List<Modello> ModelliDisponibili = new List<Modello>();

            // Scorri la lista dei modelli
            foreach( var m in ListaModelli)
            {
                // Scorri la lista dei capi
                foreach (var c in ListaCapi)
                {
                    // Se il codice del modello corrente è == al codice di questo capo
                    //          ed il codicemodello del modello corrente == codModello da ricercare
                    if ((m.CodModello == c.CodModello) /*&& (m.CodModello == codModello)*/)
                        // Ho trovato un modello disponibile (che classifica un almeno un capo con quel codice modello)
                        // Dunque aggiungilo nella lista dei modelli disponibili
                        ModelliDisponibili.Add(m);
                    else
                        // Passa a confrontare il modello corrente con il prossimo capo
                        continue;
                }
            }
            return ModelliDisponibili;
        }

        public List<Modello> GetModelliDisponibili_2([QueryString("id")] int? codModello)
        {
            // Dichiaro la lista che poi dovrò ritornare
            List<Modello> ListaModelliDisponibili = new List<Modello>();

            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(op.GetConnectionString());

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'capi'
            string selectSQL = "SELECT * FROM modelli" +
                " WHERE CodModello in (SELECT DISTINCT CodModello FROM capi); ";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(selectSQL, con);

            // Leggo le righe (in modo forward-only) dal database
            SqlDataReader dr = cmd.ExecuteReader();

            // 
            if (dr != null)
            {
                // Finchè leggi una riga (un record) dal database
                while (dr.Read())
                {
                    // Crea un nuovo oggetto di tipo Modello
                    Modello modello = new Modello();

                    // Leggi e converti i dati dal record corrente
                    modello.CodModello = Convert.ToInt32(dr["CodModello"]);
                    modello.Immagine = dr["Immagine"].ToString();
                    modello.Nome = dr["Nome"].ToString();
                    modello.Descrizione = dr["Descrizione"].ToString();
                    modello.PrezzoListino = Convert.ToDouble(dr["PrezzoListino"]);
                    modello.Genere = Convert.ToChar(dr["Genere"]);
                    modello.Collezione = dr["Collezione"].ToString();

                    // Aggiungi il capo alla lista
                    ListaModelliDisponibili.Add(modello);
                }
            }
            // Ritorno la lista di capi
            return ListaModelliDisponibili;
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

        }
    }
}