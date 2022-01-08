using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgettoDircol_ASP.Dati
{
    /// <summary>
    /// Classe che rappresenta un Utente
    /// </summary>
    public class Utente
    {
        // ATTRIBUTI
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        // COSTRUTTORE
        /// <summary>
        /// Costruttore senza parametri
        /// </summary>
        public Utente()
        {
            this.Username = "";
            this.Password = "";
            this.Nome = "";
            this.Cognome = "";
        }

        /// <summary>
        /// Costruttore con parametri
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="Nome"></param>
        /// <param name="Cognome"></param>
        public Utente(string Username, string Password, string Nome, string Cognome)
        {
            this.Username = Username;
            this.Password = Password;
            this.Nome = Nome;
            this.Cognome = Cognome;
        }

        // METODI

        // Metodi Getters e Setters
        public string GetUsername()
        {
            return this.Username;
        }
        public void SetUsername(string Username)
        {
            this.Username = Username;
        }



        public string GetPassword()
        {
            return this.Password;
        }
        public void SetPassword(string Password)
        {
            this.Password = Password;
        }



        public string GetNome()
        {
            return this.Nome;
        }
        public void SetNome(string Nome)
        {
            this.Nome = Nome;
        }



        public string GetCognome()
        {
            return this.Cognome;
        }
        public void SetCognome(string Cognome)
        {
            this.Cognome = Cognome;
        }


    }
}