using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgettoDircol_ASP.STUDIO
{
    public partial class LoginHtmlControl : System.Web.UI.Page
    {
        /* BodyHtmlControl.aspx, è più significativo e mostra come intervenire da programma sul tag <body> 
           della pagina. Essendo marcato come tag server, <body> viene tradotto in HtmlGenericControl e quindi reso 
            accessibile al codice esecutivo lato server. Al caricamento della pagina vengono impostati il colore 
            della stessa e lo script lato client da eseguire, in questo caso una semplice message dialog di 
            benvenuto. Ovviamente avremmo potuto scrivere direttamente gli attributi nel tag <body>, ma 
            l’esempio fa comprendere come sia semplice personalizzare il codice HTML in base ad 
            impostazioni memorizzate in un file, su un database, oppure in relazione all’utente che richiede la 
            pagina, eccetera.   
        */
        protected void Page_Load(object sender, EventArgs e)
        {
            body.Attributes["bgcolor"] = "lightcyan";
            body.Attributes["onload"] = "alert(\"Benvenuti\");";
        }

        protected void btnRegistra_Click(object sender, EventArgs e)
        {
            // Scrivi il valore di quello che hai letto negli elementi HTML di input,
            // inner (in mezzo) ai tag html dell'elemento di id lblConferma
            lblConferma.InnerHtml = string.Format("Utente connesso: <b>{0}, {1}</b>",
                                                     txtCognome.Value, txtNome.Value);
        }
    }
}