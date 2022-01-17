using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgettoDircol_ASP.Dati
{
    /// <summary>
    /// Classe che serve per creare un nuovo carrello per l'utente ordinario della sessione corrente
    /// </summary>
    [Serializable]
    public class Carrello
    {
        // Attributi
        public string Username { get; set; }
        public List<int> ListaIDCapi;

        // Costruttore con parametri
        public Carrello(string Username, List<int> ListaIDCapi)
        {
            this.Username = Username;
            this.ListaIDCapi = ListaIDCapi;
        }

        // Costruttore senza parametri
        public Carrello()
        {
            this.Username = "";
            this.ListaIDCapi = new List<int>();
        }

        // Costruttore "promiscuo"
        public Carrello(string Username)
        {
            this.Username = "";
            this.ListaIDCapi = new List<int>();
        }

        // Metodi

    }
}