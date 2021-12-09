using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Web.UI;
// using DALayer;
using System.Web;
using System.Web.UI.WebControls;
// using Globals.Configurazione;



// Class RecensioniDAL eredita ConnessioneDAL
public class RecensioniDAL : ConnessioneDAL
{
    // Inserisci recensione
    public int InsertRecensione(int idUtente, bool pubblicata, string messaggio, int idProd, int valutazione)
    {
        // Stringa comando SQL
        string strSql = "INSERT INTO Recensioni " + "(data, idUtente, pubblicata, messaggio, idProd, valutazione, dataModificaRecensione) " + "VALUES(" + "GETDATE(), @idUtente, @pubblicata, @messaggio, @idProd, @valutazione, NULL);";
        
        // Altra stringa comando SQL
        string strSqlID = "SELECT SCOPE_IDENTITY() AS ID FROM Recensioni";

        // Insieme di stringhe
        Collection<string> collStrings = new Collection<string>();

        // Aggiungo il comando SQL all'insieme di stringhe
        collStrings.Add(strSql + strSqlID);

        // Insieme di paramtri SQL
        Collection<SqlParameter> collPar = new Collection<SqlParameter>();

        // Aggiungo le variabili all'insieme di parametri SQL
        collPar.Add(new SqlParameter("@idUtente", idUtente));
        collPar.Add(new SqlParameter("@pubblicata", pubblicata));
        collPar.Add(new SqlParameter("@messaggio", messaggio));
        collPar.Add(new SqlParameter("@idProd", idProd));
        collPar.Add(new SqlParameter("@valutazione", valutazione));

        // Insieme di insiemi di parametri
        Collection<Collection<SqlParameter>> collPars = new Collection<Collection<SqlParameter>>();

        // Aggiungo all'insieme delle recensioni
        collPars.Add(collPar);

        // Ritorno L'ID
        return this.InsertGetID(collStrings, collPars);
    }



    // Aggiorna la singola recensione
    public bool UpdateRecensione(int idRecensione, string messaggio, int idProd, int valutazione)
    {
        // Stringa comando SQL
        string strSql = "UPDATE Recensioni SET messaggio = @messaggio, valutazione = @valutazione, dataModificaRecensione = GETDATE() WHERE Recensioni.iD = @idRecensione";

        // Insieme di stringhe
        Collection<string> collStrings = new Collection<string>();

        // Aggiungo il comando SQL all'insieme di stringhe
        collStrings.Add(strSql);

        // Insieme di parametri sql
        Collection<SqlParameter> collPar = new Collection<SqlParameter>();

        // Aggiungo le variabili all'insieme
        collPar.Add(new SqlParameter("@valutazione", valutazione));
        collPar.Add(new SqlParameter("@idProd", idProd));
        collPar.Add(new SqlParameter("@messaggio", messaggio));
        collPar.Add(new SqlParameter("@idRecensione", idRecensione));

        // Insieme di insiemi
        Collection<Collection<SqlParameter>> collPars = new Collection<Collection<SqlParameter>>();

        // Aggiungo all'insieme delle recensioni
        collPars.Add(collPar);

        // Se ho fatto almeno un aggiornamento, allora è vero che ho aggiornato qualcosa, altrimenti falso
        if (this.Update(collStrings, collPars) > 0)
            return true;
        return false;
    }

    //public DataTable GetRecensioni(object idProdotto)
    //{
    //    // Comando SQL per ottenere quello che desidero
    //    string strSql = string.Empty;
    //    strSql = "SELECT DISTINCT Anagrafica.idCliente, Recensioni.ID, Recensioni.data, AnagraficaIndirizzi.nome, AnagraficaIndirizzi.cognome, Recensioni.messaggio, Recensioni.valutazione, Recensioni.dataRisposta, Recensioni.messaggioRisposta, Recensioni.dataModificaRecensione " + "FROM Recensioni " + "LEFT OUTER JOIN Anagrafica ON Recensioni.idUtente = Anagrafica.idCliente " + "LEFT OUTER JOIN AnagraficaIndirizzi ON Anagrafica.idCliente = AnagraficaIndirizzi.idUtente " + "WHERE (Recensioni.idProd = @idProd) AND (Recensioni.pubblicata = 1) AND (AnagraficaIndirizzi.fatturazione = 1)" + "ORDER BY Recensioni.data DESC"; // "ORDER BY Recensioni.valutazione DESC, Recensioni.data DESC"

    //    // Creo insieme di stringhe e ci inserisco il comando
    //    Collection<string> collStrings = new Collection<string>();
    //    collStrings.Add(strSql);

    //    // Creo insieme di parametri SQL e ci inserisco la variabile sql
    //    Collection<SqlParameter> collPar = new Collection<SqlParameter>();
    //    collPar.Add(new SqlParameter("@idProd", idProdotto));

    //    // Non capisco cosa ritorno...
    //    DataTable dt = this.GetDataTable(strSql, collPar);

    //    foreach (var r in dt.r)
    //    {

    //    }
    //}

    public DataTable GetRecensioneUtente(int idProdotto, int idUtente)
    {
        // Come per GetRecensioni, solo che considero anche idUtente
        string strSql = string.Empty;
        strSql = "SELECT Recensioni.ID, Anagrafica.idCliente, Recensioni.data, AnagraficaIndirizzi.nome, AnagraficaIndirizzi.cognome, Recensioni.messaggio, Recensioni.idProd, Recensioni.valutazione, Recensioni.dataRisposta, Recensioni.messaggioRisposta, Recensioni.dataModificaRecensione " + "FROM Recensioni " + "LEFT OUTER JOIN Anagrafica ON Recensioni.idUtente = Anagrafica.idCliente " + "LEFT OUTER JOIN AnagraficaIndirizzi ON Anagrafica.idCliente = AnagraficaIndirizzi.idUtente " + "WHERE (Recensioni.idProd = @idProd) AND (Anagrafica.idCliente = @idUtente)  AND (Recensioni.pubblicata = 1) AND (AnagraficaIndirizzi.fatturazione = 1)" + "ORDER BY Recensioni.data DESC"; // "ORDER BY Recensioni.valutazione DESC, Recensioni.data DESC"

        Collection<string> collStrings = new Collection<string>();
        collStrings.Add(strSql);

        Collection<SqlParameter> collPar = new Collection<SqlParameter>();
        collPar.Add(new SqlParameter("@idProd", idProdotto));
        collPar.Add(new SqlParameter("@idUtente", idUtente));

        // Ritorno la tabella desiderata
        return this.GetDataTable(strSql, collPar);
    }


    public void InsertNotificheAmministratori(string idRecensione)
    {
        Collection<string> stringhe = new Collection<string>();
        Collection<Collection<SqlParameter>> paramsColl = new Collection<Collection<SqlParameter>>();

        string strSql = @"INSERT INTO NotificheDettaglio(idNotifica,idElemento,letta,idUtente)
                        SELECT @idNotifica, @idElemento, @letta, idCliente From Anagrafica WHERE Anagrafica.idCliente 
                        IN (SELECT Anagrafica.idCliente FROM Abilitazione_Anagrafica_Ruoli INNER JOIN Anagrafica INNER JOIN AnagraficaIndirizzi ON Anagrafica.idCliente = AnagraficaIndirizzi.idUtente ON Abilitazione_Anagrafica_Ruoli.idCliente = Anagrafica.idCliente INNER JOIN Ruoli ON Ruoli.ID = Abilitazione_Anagrafica_Ruoli.idRuolo INNER JOIN NotificheUtente ON NotificheUtente.idUtente = Anagrafica.idCliente  WHERE (AnagraficaIndirizzi.fatturazione = 1 AND Abilitazione_Anagrafica_Ruoli.approvato = 1 AND ( Ruoli.admin = 1 OR Ruoli.superAdmin = 1 OR Ruoli.software = 1) AND NotificheUtente.idNotifica = 5 AND NotificheUtente.active = 1))";
        Collection<SqlParameter> p = new Collection<SqlParameter>();
        p.Add(new SqlParameter("@idNotifica", 5));
        p.Add(new SqlParameter("@idElemento", idRecensione));
        p.Add(new SqlParameter("@letta", 0));
        paramsColl.Add(p);
        stringhe.Add(strSql);

        this.Insert(stringhe, paramsColl);
    }
}
