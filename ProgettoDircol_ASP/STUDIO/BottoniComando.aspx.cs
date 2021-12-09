using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgettoDircol_ASP.STUDIO
{
    public partial class BottoniComando : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*  Il gestore si limita a visualizzare mediante una Label il CommandName associato al bottone cliccato, 
            ottenuto dal parametro informazioni evento.In questo caso, la proprietà id dei bottoni non viene
            impostata, poiché è possibile discriminare il bottone cliccato mediante la proprietà CommandName.
            Un’altra funzionalità comune ai tre bottoni è la proprietà CausesValidation, attraverso la quale
            si può stabilire se eseguire o meno i controlli di valdazione inseriti nella pagina quando viene
            selezionato il bottone. Il valore predefinito è true. 
        */
        protected void EventoComando(object sender, CommandEventArgs e)
        {
            lblComando.Text = e.CommandName;
        }
    }
}