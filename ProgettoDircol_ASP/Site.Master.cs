using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

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


        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/loginAdmin.aspx", false);
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
                // Se l'oggetto Session non è null
                if (Session["ruolo"] != null)
                {
                    /* CASO UTENTE NON LOGGATO */
                    if (Session["ruolo"].Equals(""))
                    {
                        Response.Write("<script>alert('Ciao');</script>");

                        // Imposto i link della pagina da mostrare a questo tipo di utente
                        linkLoginUtente.Visible = true; // navbar dropdown accesso, link accedi
                        linkRegistrazioneUtente.Visible = true; // navbar dropdown accesso, link registrati
                                                                //navbarDropdownAccesso.InnerHtml = "<i class='bi bi-person - lines - fill'></i>&nbsp;Accesso"; // navbar dropdown accesso
                        navbarDropdownAccesso.InnerHtml = "<i class='bi bi-person - lines - fill'></i>&nbsp;Accesso"; // navbar dropdown accesso
                        btnAdminLogin.Visible = true;
                        // navbarDropdownAccesso.InnerText = "Ciao prova";

                        // Imposto i link della pagina da nascondere a questo tipo di utente
                        linkEsciUtente.Visible = false; // navbar dropdown accesso, link esci
                        linkGestioneModelli.Visible = false; // navbar link gestione modelli
                        linkVisualizzaTabelle.Visible = false; // navbar link visualizza tabelle
                        navbarDropdown.Visible = false; // navbar drop down operazioni DML
                        linkInserisciDML.Visible = false;
                        linkAggiornaDML.Visible = false;
                        linkEliminaDML.Visible = false;
                        linkInterrogazioni.Visible = false; // navbar link interrogazioni
                    }
                    /* CASO UTENTE LOGGATO */
                    else if (Session["ruolo"].Equals("utente"))
                    {


                    }
                    /* CASO AMMINISTRATORE LOGGATO */
                    else if (Session["ruolo"].Equals("amministratore"))
                    {

                    }
                }
                else
                {
                    Response.Write("<script>alert('La variabile Session vale null');</script>");

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

            // Response.Redirect("~/Default.aspx", false);

        } // fine page load
    }

}