using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgettoDircol_ASP.Dati
{
    /// <summary>
    /// Classe che rappersenta il Feedback di un utente quando acquista un capo
    /// La recensione è fatta sul modello che classifica tale capo
    /// </summary>
    public class Recensione
    {
        // Attributi
        public int IDRecensione { get; set; }
        public string DescrizioneRecensione { get; set; }
        public int PunteggioRecensione { get; set; }
        public int CodModello { get; set; }
        public string UsernameUtente { get; set; }
    }
}