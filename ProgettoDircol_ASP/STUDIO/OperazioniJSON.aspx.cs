using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Per le operazioni su file
using System.IO;

// (Per usare DataRowView)
using System.Data;


// Per la serializzazione di oggetti
using System.Runtime.Serialization;
// Per operare con i file JSON
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

// Per accedere alle classi
using ProgettoDircol_ASP.Dati;

namespace ProgettoDircol_ASP.STUDIO
{
    public partial class OperazioniJSON : System.Web.UI.Page
    {
        // ################ VARIABILI GLOBALI ######################
        Operazione op = new Operazione();

        // Mi costruisco il mio oggetto di tipo utente ordinario
        Utente u = new UtenteOrdinario();

        // Dichiaro una variabile globale di tipo lista di carrelli, che popolo dopo
        List<Carrello> ListaCarrelli;

        // Dichiaro la lista degli ID che servirà come DataSource per le varie DDL
        List<int> ListaID_Capi;

        /*
        // MANTENGO LO STATO DELLA LISTA DEI CAPI CLASSIFICATI
        // RICORDA: Il tipo della lista (in questo caso Carrello) deve essere serializzabile [Serializable]
        const string listaCarrelli = "listaCarrelli";

        public List<Carrello> ListaCarrelli // DICHIARO LA MIA LISTA "PERMANENTE"
        {
            get
            {
                // check if not exist to make new (normally before the post back)
                // and at the same time check that you did not use the same viewstate for other object
                if (!(ViewState[listaCarrelli] is List<Carrello>))
                {
                    // need to fix the memory and added to viewstate
                    ViewState[listaCarrelli] = new List<Carrello>();
                }

                return (List<Carrello>)ViewState[listaCarrelli];
            }
        }
        */






        //// Mi costruisco una lista di carrelli
        //public List<Carrello> ListaCarrelli;

        protected void Page_Load(object sender, EventArgs e)
        {
            // PROVO CON UN SINGOLO OGGETTO DI TIPO CARRELLO. file: FileOggetto.json

            /*{
                "Username": "mark99",
                "ListaIDCapi": [ 2, 5, 6 ]
               }*/

            // Prelevo il nome del file
            string nomefile = HttpContext.Current.Server.MapPath("~/STUDIO/FileOggetto.json");

            // Leggo da file: FileOggetto.json
            string json = File.ReadAllText(nomefile);

            // Deserializzo il contenuto del file letto in un oggetto di tipo Carrello
            // e lo salvo in una variabile
            var carrello = JsonConvert.DeserializeObject<Carrello>(json);

            // Scrivo nei controlli il contenuto
            txtUsername.Text = carrello.Username;
            ddlListaID.DataSource = carrello.ListaIDCapi;
            ddlListaID.DataBind();
            ddlListaID.Items.Insert(0, new ListItem(op.stringaDicontrollo));


            // PROVO CON UNA LISTA DI OGGETTI DI TIPO CARRELLO. file: FileListaOggettiProva.json

            /* {              
                  "Username": "mark99",
                  "ListaIDCapi": [ 2, 5, 6 ]
             },
             {              
                  "Username": "ale01",
                  "ListaIDCapi": [ 1, 4, 7 ]
             },
              {              
                  "Username": "gioDR3",
                  "ListaIDCapi": [ 8, 9, 10 ]
             } */


            nomefile = HttpContext.Current.Server.MapPath("~/STUDIO/FileListaOggetti.json");

            // Leggo da file: FileListaOggetti.json
            json = File.ReadAllText(nomefile);

            // Deserializzo il contenuto del file letto in una Lista di oggetti di tipo Carrello
            // e la salvo in una variabile

            // ViewState[listaCarrelli] = JsonConvert.DeserializeObject<List<Carrello>>(json);
            ListaCarrelli = JsonConvert.DeserializeObject<List<Carrello>>(json);


            // Assegno come DataSource la lista appena ottenuta
            this.rptCarrello.DataSource = ListaCarrelli;

            // Eseguo il DataBind() del repeater --> gli attributi diventano le colonne
            this.rptCarrello.DataBind();

            // Sistemo lo stile del Panel
            pnOperazioniJSON.Style.Add("padding", "50px");
        }



        /// <summary>
        /// Evento DataBound del Repeater:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptCarrello_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            // Ho ottenuto la lista dei carrelli, ora, devo estrarre:
            // Per ogni carrello con quel determinato username, l'elenco degli ID dei capi
            // In questo modo ottengo una lista che farà da DataSource alla DDL
            //foreach (var c in ListaCarrelli)
            //{
            //    if (c.Username.Equals(Session["username"].ToString()))
            //    {
            //        ListaID_Capi = c.ListaIDCapi;
            //        break;
            //    }

            //}

            // Se trovi un controllo lista all'interno dell'ItemTemplate, devi fargli il DataSource ed il DataBind().
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                 //((DropDownList)e.Item.FindControl("ddlCarrello_Iesimo")).DataSource = (List<int>)e.Item.DataItem; 
                 //((DropDownList)e.Item.FindControl("ddlCarrello_Iesimo")).DataSource = (DataRowView)e.Item.DataItem; 
                 // ((DropDownList)e.Item.FindControl("ddlCarrello_Iesimo")).DataSource = (DataRowView)(List<int>)e.Item.DataItem["ListaIDCapi"]; 
                 // ((DropDownList)e.Item.FindControl("ddlCarrello_Iesimo")).DataBind();
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Ottengo la DDL che mi serve
                var DDL = e.Item.FindControl("ddlCarrello_Iesimo") as DropDownList;
                //var Oggetto = e.Item.DataItem;
                //DDL.DataSource = (List<int>)Oggetto.ListaIDCapi;
                // DDL.DataBind();
                // Devo assegnarle la lista giusta
                
                // DataRowView DRV = 
                // List<int> LISTA = e.Item.DataItem as List<int>;
                // var drv = e.Item.DataItem as DataRowView;
                // var lista = drv.Row["ListaIDCapi"] as List<int>;
                // List<int> lista = (List<int>)rowView["ListaIDCapi"];

                //ddlCarrello_Iesimo.DataSource = lista;
                //ddlCarrello_Iesimo.DataBind();
            }
        }
    }
}