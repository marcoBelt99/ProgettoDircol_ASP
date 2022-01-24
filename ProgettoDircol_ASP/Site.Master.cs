using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


using ProgettoDircol_ASP.Dati; // Per accedere alle classi
using System.IO; // Per operazioni su file 
using Newtonsoft.Json; // Per operazioni su file JSON


using Microsoft.AspNet.Identity;

namespace ProgettoDircol_ASP
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Il codice seguente facilita la protezione da attacchi XSRF
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Utilizzare il token Anti-XSRF dal cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generare un nuovo token Anti-XSRF e salvarlo nel cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Impostare il token Anti-XSRF
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Convalidare il token Anti-XSRF
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Convalida del token Anti-XSRF non riuscita.");
                }
            }
        }



        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }



        /* ##########  Funzioni definite da me  ########################################### */
        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/loginAdmin.aspx", false);
        }



        public int GetQuantitaCapiCarrello(string UsernameUtenteSessione)
        {
            List<Carrello> ListaCarrelli = new List<Carrello>();
            string NomeFileJson = HttpContext.Current.Server.MapPath("~/Carrelli.json");
            string json = File.ReadAllText(NomeFileJson);
            // Deserializzo il contenuto del file letto in una Lista di oggetti di tipo Carrello
            // e la salvo in una variabile
            ListaCarrelli = JsonConvert.DeserializeObject<List<Carrello>>(json);

            // Se esiste almeno un carrello nella lista
            if (ListaCarrelli != null)
            {
                // Ricerco il carrello nella lista dei carrelli
                var carrello = ListaCarrelli.Find(c => c.Username == UsernameUtenteSessione.ToString());
                // Se non l'ho trovato, vuol dire che per questo utente ancora non esiste un carrello
                if ((carrello != null) && (carrello.ListaIDCapi != null) && (carrello.ListaIDCapi.Count > 0))
                    return carrello.ListaIDCapi.Count;
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }


        }



        /// <summary>
        /// Inizializzo i valori della Session
        /// </summary>
        protected void InizializzaSessione()
        {
            Session["username"] = "";
            Session["nome"] = "";
            Session["cognome"] = "";
            Session["ruolo"] = "";
            Session["stato"] = "";
            Session["quantita"] = 0;
            // Session["quantita"] = GetQuantitaCapiCarrello();
        }


        /// <summary>
        /// Link di logout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // Chiudo la sessione per questo utente
            InizializzaSessione();

            // Per default visualizzo/nascondo i link di un utente non loggato
            GestioneUtenteNonLoggato();

            // Torno alla home
            Response.Redirect("Default.aspx");
        }




        /// <summary>
        /// Mostra/Nasconde parti della pagina per un utente che non ha effettuato l'accesso
        /// </summary>
        protected void GestioneUtenteNonLoggato()
        {
            // Response.Write("<script>alert('Ciao UTENTE NON LOGGATO');</script>");


            // Link da mostrare ad utente non loggato
            linkLoginUtente.Visible = true; // navbar dropdown accesso, link accedi
            linkRegistrazioneUtente.Visible = true; // navbar dropdown accesso, link registrati
            navbarDropdownAccesso.InnerHtml = "<i class='bi bi-person - lines - fill'></i>&nbsp;Accesso"; // navbar dropdown accesso
            btnAdminLogin.Visible = true;
            // navbarDropdownAccesso.InnerText = "Ciao prova";

            // Link da nascondere ad utente non loggato
            LinkButton1.Visible = false; // navbar dropdown accesso, link esci
            linkGestioneModelli.Visible = false; // navbar link gestione modelli
            linkVisualizzaTabelle.Visible = false; // navbar link visualizza tabelle
            navbarDropdown.Visible = false; // navbar drop down operazioni DML
            linkInserisciDML.Visible = false;
            linkAggiornaDML.Visible = false;
            linkEliminaDML.Visible = false;
            linkInterrogazioni.Visible = false; // navbar link interrogazioni
            linkCarrelloAcquisti.Visible = false; // navbar dropdown access, link carrello

            InizializzaSessione();

        }

        /// <summary>
        /// Mostra/Nasconde parti della pagina per un utente che ha effettuato l'accesso
        /// </summary>
        protected void GestioneUtenteLoggato()
        {
            // Response.Write("<script>alert('CIAO UTENTE LOGGATO:\n" + Session["nome"] + " " + Session["cognome"] + "');</script>");

            Session["quantita"] = GetQuantitaCapiCarrello(Session["username"].ToString());

            // Link da mostrare ad utente ordinario
            navbarDropdownAccesso.InnerHtml = "<i class='bi bi-person - lines - fill'></i>&nbsp;" + Session["username"] + "&nbsp;&nbsp;&nbsp;"; // navbar dropdown accesso
            LinkButton1.Visible = true; // navbar dropdown accesso, link esci
            btnAdminLogin.Visible = true;
            linkGestioneModelli.Visible = true; // navbar link gestione modelli
            linkCarrelloAcquisti.Visible = true;
            spanQuantitaCarrello.InnerHtml = Session["quantita"].ToString();

            // Link da nascondere ad utente ordinario
            linkLoginUtente.Visible = false; // navbar dropdown accesso, link accedi
            linkRegistrazioneUtente.Visible = false; // navbar dropdown accesso, link registrati
            linkVisualizzaTabelle.Visible = false; // navbar link visualizza tabelle
            navbarDropdown.Visible = false; // navbar drop down operazioni DML
            linkInserisciDML.Visible = false;
            linkAggiornaDML.Visible = false;
            linkEliminaDML.Visible = false;
            linkInterrogazioni.Visible = false; // navbar link interrogazioni
        }

        /// <summary>
        /// Mostra/Nasconde parti della pagina per un utente che ha effettuato l'accesso
        /// come amministratore
        /// </summary>
        protected void GestioneAmministratoreLoggato()
        {
            // Response.Write("<script>alert('CIAO UTENTE AMMINISTRATORE');</script>");
            // Link da mostrare all'amministratore
            navbarDropdownAccesso.InnerHtml = "<i class='bi bi-person - lines - fill'></i>&nbsp;" + Session["username"] + ""; // navbar dropdown accesso
            LinkButton1.Visible = true; // navbar dropdown accesso, link esci
            navbarDropdown.Visible = true; // navbar drop down operazioni DML
            linkInserisciDML.Visible = true;
            linkAggiornaDML.Visible = true;
            linkEliminaDML.Visible = true;
            linkInterrogazioni.Visible = true; // navbar link interrogazioni


            // Link da nascondere all'amministratore
            btnAdminLogin.Visible = false;
            linkGestioneModelli.Visible = false; // navbar link gestione modelli
            linkLoginUtente.Visible = false; // navbar dropdown accesso, link accedi
            linkRegistrazioneUtente.Visible = false; // navbar dropdown accesso, link registrati
            linkVisualizzaTabelle.Visible = true; // navbar link visualizza tabelle
            linkCarrelloAcquisti.Visible = false;
        }




        /// <summary>
        /// PAGE LOAD DEL SITE.MASTER
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Inizializzo l'oggetto Session
                //  InizializzaSessione();

                // Se l'oggetto Session non è null
                if (Session["ruolo"] != null)
                {
                    /* CASO UTENTE NON LOGGATO: Se il ruolo della sessione non è utente e se il ruolo della sessione non è amministratore */
                    if ((Session["ruolo"].Equals("utente") == false) && (Session["ruolo"].Equals("amministratore") == false))
                        GestioneUtenteNonLoggato();

                    /* CASO UTENTE LOGGATO */
                    else if (Session["ruolo"].Equals("utente"))
                        GestioneUtenteLoggato();

                    /* CASO AMMINISTRATORE LOGGATO */
                    else if (Session["ruolo"].Equals("amministratore"))
                        GestioneAmministratoreLoggato();
                }
                // Di default, non ho ancora popolato la variabile Session, quindi rientro nel caso di utente che non è loggato
                else
                {
                    //  Response.Write("<script>alert('La variabile Session vale null');</script>");
                    GestioneUtenteNonLoggato();
                }
            }
            catch (NullReferenceException ex)
            {
                Response.Write("<script>alert('NullReferenceException generata.\nMessaggio di errore: " + ex.Message + ".\n');</script>");

            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Eccezione generata.\nMessaggio di errore: " + ex.Message + ".\n');</script>");
            }


            /// Assegno dello stile al link in alto a destra della navbar

            // Tolgo il padding generico
            linkLoginUtente.Style.Add("padding", "0px");
            linkRegistrazioneUtente.Style.Add("padding", "0px");
            linkRegistrazioneUtente.Style.Add("padding", "0px");
            linkCarrelloAcquisti.Style.Add("padding", "0px");
            LinkButton1.Style.Add("padding", "0px");
            // Aggiungo il padding top
            linkLoginUtente.Style.Add("padding-top", "3px");
            linkRegistrazioneUtente.Style.Add("padding-top", "4px");
            linkRegistrazioneUtente.Style.Add("padding-top", "3px");
            linkCarrelloAcquisti.Style.Add("padding-top", "3px");
            LinkButton1.Style.Add("padding-top", "3px");
            // Aggiungo il padding bottom
            linkLoginUtente.Style.Add("padding-bottom", "3px");
            linkRegistrazioneUtente.Style.Add("padding-bottom", "4px");
            linkRegistrazioneUtente.Style.Add("padding-bottom", "3px");
            linkCarrelloAcquisti.Style.Add("padding-bottom", "3px");
            LinkButton1.Style.Add("padding-bottom", "3px");
        } // fine page load





    }

}