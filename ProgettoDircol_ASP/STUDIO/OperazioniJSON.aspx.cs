using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Per i colori
using System.Drawing;


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

        protected void btnQuery_1_Click(object sender, EventArgs e)
        {
            string nomefile = HttpContext.Current.Server.MapPath("~/STUDIO/FileListaOggetti.json");
            // Leggi e deserializza il JSON
            string json = File.ReadAllText(nomefile);
            var lista_carrelli = JsonConvert.DeserializeObject<List<Carrello>>(json);

            // Esegui la query e salva il risultato
            var lista_utenti = from c in lista_carrelli select c.Username;

            // Stampa il risultato su qualche controllo
            // OSSERVA: Non posso scrivere il contenuto di una lista di stringhe
            // in un text box, devo crearne una unica facendo il Join di tutte
            // le stringhe della lista di stringhe

            txtQuery_1.Text = String.Join(Environment.NewLine, lista_utenti);


        }

        protected void btnQuery_2_Click(object sender, EventArgs e)
        {
            string nomefile = HttpContext.Current.Server.MapPath("~/STUDIO/FileListaOggetti.json");
            // Leggi e deserializza il JSON
            string json = File.ReadAllText(nomefile);
            var lista_carrelli = JsonConvert.DeserializeObject<List<Carrello>>(json);

            // Esegui la query e salva il risultato
            // var res = lista_carrelli.Where(c => c.Username.Equals(txtQuery_2.Text));

            //var res = from c in lista_carrelli
            //          where c.Username == txtQuery_2.Text // Notare l'uso di == nel confronto delle due stringhe
            //          select c.Username;


            // Con Any mi viene restituito un risultato booleano
            var res = lista_carrelli.Any(c => c.Username == txtQuery_2.Text);

            if (res == true)
            {
                ltRisultatoQuery_2.Text = "Utente Presente!!";
            }
            else
            {
                ltRisultatoQuery_2.Text = "Utente NON Presente!!";
            }
        }

        protected void btnQuery_3_Click(object sender, EventArgs e)
        {

            string nomefile = HttpContext.Current.Server.MapPath("~/STUDIO/FileListaOggetti.json");
            // Leggi e deserializza il JSON
            string json = File.ReadAllText(nomefile);
            var lista_carrelli = JsonConvert.DeserializeObject<List<Carrello>>(json);

            // Creazione Query, esecuzione e salvataggio in variabile
            //var resInt = from c in lista_carrelli
            //             where c.Username == txtQuery_3.Text
            //             select c.ListaIDCapi;
            // Converto in stringa ogni elemento della lista di interi e l'elaborazione la metto in una lista
            //var resString = resInt.Select(x => x.ToString()).ToList();


            // Ricerca in lista
            Carrello carrelloDesiderato = new Carrello();
            foreach (var c in lista_carrelli)
            {
                // Se sto considerando l'utente scelto
                if (c.Username == txtQuery_3.Text)
                {
                    // Prelevami il suo carrello (La sua lista di ID)
                    carrelloDesiderato = c;
                    break;
                }
            }


            // Converto la lista di interi in lista di stringhe
            List<string> listaStringhe = new List<string>();
            foreach (var i in carrelloDesiderato.ListaIDCapi)
            {
                listaStringhe.Add(i.ToString());
            }



            // Scrivo il risultato. Notare ancora che con la Join vado a trasformare una lista di stringhe in un'unica stringa
            txtRisultatoQuery_3.Text = String.Join("\t", listaStringhe);

            // Do del colore al textbox
            txtRisultatoQuery_3.BackColor = Color.DarkMagenta;
            txtRisultatoQuery_3.ForeColor = Color.White;
        }

        protected void btnInserisci_Click(object sender, EventArgs e)
        {
            Carrello c = new Carrello();

            // Popolo lo username del nuovo carrello
            c.Username = txtUsername_Inserimento.Text;

            // Controllare che ci sia già nel database...


            string str = txtListaIDCapi_Inserimento.Text;
            string[] Stringhe = str.Split(',');


            // Popolo la lista di capi del nuovo carrello
            foreach (var s in Stringhe)
            {
                if (s.Length != 1) // s.Length < 0 || s.Length > 1
                {
                    ltInserimento.Text = "<style color='yellow';>Devi scrivere numeri separati da virgola senza spazi!!</style>";
                    break;
                }
                c.ListaIDCapi.Add(int.Parse(s));
            }


            ltInserimento.Text = "<style color='red';>Errore nell'inserimento del Carrello</style>";
            ltInserimento.Text = "<style color='green';>Carrello inserito correttamente</style>";

            // Aggiungo il carrello appena creato nella lista globale dei carrelli
            ListaCarrelli.Add(c);

            // Aggiorno il file JSON: Serializzo la lista di carrelli come stringa
            // json, la scrivo sul file
            string nomefile = HttpContext.Current.Server.MapPath("~/STUDIO/FileListaOggetti.json");
            var json = JsonConvert.SerializeObject(ListaCarrelli);
            File.WriteAllText(nomefile, json);

        }
    }
}