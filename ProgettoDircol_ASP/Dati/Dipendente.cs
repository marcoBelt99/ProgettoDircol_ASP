﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/* Aggiunta namespace che mi servono */
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace ProgettoDircol_ASP.Dati
{

    /// <summary>
    /// CLASSE CHE VA AD INTERAGIRE COL DATABASE:  LEGGE I DATI E LI SALVA PER POTERLI POI VISUALIZZARE,
    /// OPPURE FA OPERAZIONI DI CREAZIONE, MODIFICA, CANCELLAZIONE DI RECORD.
    /// I DATI VENGONO RACCOLTI DALLE PAGINE CODE BEHIND TRAMITE L'ACCESSO AI VALORI DEI CONTROLLI SERVER.
    /// I DATI VENGONO SPEDITI ALLA PAGINE CODE BEHIND TRAMITE DELLE LISTE
    /// </summary> 
    public class Dipendente
    {
        // ATTRIBUTI
        public string Matricola { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string CodiceFiscale { get; set; }
        public string Qualifica { get; set; }
        public int PuntoVendita { get; set; }

        // COSTRUTTORE VUOTO
        public Dipendente()
        {
            this.Matricola = "";
            this.Cognome = "";
            this.Nome = "";
            this.CodiceFiscale = "";
            this.Qualifica = "";
            this.PuntoVendita = 0;
        }

        // COSTRUTTORE CHE SETTA I PARAMETRI
        public Dipendente(string Matricola, string Cognome, string Nome,
         string CodiceFiscale, string Qualifica, int PuntoVendita)
        {
            this.Matricola = Matricola;
            this.Cognome = Cognome;
            this.Nome = Nome;
            this.CodiceFiscale = CodiceFiscale;
            this.Qualifica = Qualifica;
            this.PuntoVendita = PuntoVendita;
        }


        // METODI
        /// <summary>
        /// Ritorna la lista di capi andando a leggere dal Database
        /// impostato nella stringa di connessione.
        /// Questo metodo mi serve per la VISUALIZZAZIONE dei dati dal Database.
        /// </summary>
        /// <param name="connectionString">stringa di connessione al database</param>
        /// <returns></returns>
        public List<Dipendente> GetDipendenti(string connectionString)
        {
            // Dichiaro la lista che poi dovrò ritornare
            List<Dipendente> listaDipendenti = new List<Dipendente>();

            // Dichiaro una variabile per la connessione
            SqlConnection con = new SqlConnection(connectionString);

            // Stringa SQL: Seleziona tutti i dati dalla tabella 'dipendenti'
            string selectSQL = "select * from dipendenti";

            // Apro la connessione
            con.Open();

            // Imposto il comando SQL
            SqlCommand cmd = new SqlCommand(selectSQL, con);

            // Leggo le righe (in modo forward-only) dal database
            SqlDataReader dr = cmd.ExecuteReader();

            // 
            if (dr != null)
            {
                // Finchè leggi una riga (un record) dal database
                while (dr.Read())
                {
                    // Crea un nuovo oggetto di tipo Dipendente
                    Dipendente dipendente = new Dipendente();

                    // Leggi e converti i dati dal record corrente
                    dipendente.Matricola = dr["Matricola"].ToString();
                    dipendente.Cognome = dr["Cognome"].ToString();
                    dipendente.Nome = dr["Nome"].ToString();
                    dipendente.CodiceFiscale = dr["CodiceFiscale"].ToString();
                    dipendente.Qualifica = dr["Qualifica"].ToString();
                    dipendente.PuntoVendita = Convert.ToInt32(dr["PuntoVendita"]);

                    // Aggiungi il capo alla lista
                    listaDipendenti.Add(dipendente);

                }
            }

            // Ritorno la lista di dipendenti
            return listaDipendenti;
        }


    } // fine classe
} // fine namespace