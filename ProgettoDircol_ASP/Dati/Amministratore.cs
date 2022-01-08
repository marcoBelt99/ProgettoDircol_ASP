using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgettoDircol_ASP.Dati
{
    public class Amministratore : Utente
    {
        // Per ora non ha attributi

        // COSTRUTTORE
        public Amministratore(string Username, string Password)
        {
            Username = "admin";
            Password = "admin";
        }

        // METODI

    }
}