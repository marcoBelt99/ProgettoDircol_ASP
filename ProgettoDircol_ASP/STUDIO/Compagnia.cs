using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgettoDircol_ASP.STUDIO
{
    public class Compagnia
    {
        private string RagioneSociale { get; set; }
        private double Bilancio { get; set; }

        public Compagnia(string RagioneSociale, double Bilancio)
        {
            this.RagioneSociale = RagioneSociale;
            this.Bilancio = Bilancio;
        }
        
        public string GetRS()
        { return this.RagioneSociale; }
        public double GetB()
        { return this.Bilancio; }
        

    }
}