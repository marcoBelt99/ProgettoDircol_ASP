using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Collections.ObjectModel;
using System.Web.Hosting;
using System.Web.Caching;
using System;
using System.Collections.Generic;

public class ConnessioneDAL
{

    // Stringa di connessione (Che trovo anche in Web.config)
    // private string strConn = WebConfigurationManager.ConnectionStrings("strConn").ConnectionString;
    // private string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;
    private string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


    /// <summary>
    /// GetDataTable: Effettua la stampa per righe degli elementi di una tabella.
    /// </summary>
    /// <param name="strSql">Stringa che contiene un comando SQL </param>
    /// <param name="sqlParametri">Insieme di parametri che compongono il mio comando SQL</param>
    /// <param name="commandTimeout">Tempo di connessione</param>
    /// <returns></returns>
    public DataTable GetDataTable(string strSql, Collection<SqlParameter> sqlParametri = null, int commandTimeout = 30)
    {
        // Creo una nuova tabella di dati. Essa  ha la stessa funzione degli array associativi del PHP.
        // Contiene un insieme di righe (come risultato di una generica query)
        DataTable dt = new DataTable();

        // Quando metto questo using, alla fine devo sempre mettere un Dispose per chiudere tutte le conessioni
        // Creo un nuovo oggetto 'conn' di tipo SqlConnection, a cui passo la stringa di connessione ottenuta prima
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            // Creo un nuovo oggetto 'cmd' di tipo SqlCommand
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                // Controllo e gestisco il "tempo di esecuzione"
                if (commandTimeout > 30)
                {
                    try
                    {
                        cmd.CommandTimeout = commandTimeout;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                // Rimuovo tutti gli oggetti 'SqlParameter' dalla collezione 'SqlParameterCollection'
                cmd.Parameters.Clear();

                // Se è falso che sqlParametri è == null
                if (!(sqlParametri == null))
                {
                    // Per ogni parametro nella collezione 'sqlParametri'
                    foreach (SqlParameter parametro in sqlParametri)
                        // Aggiungilo al comando
                        cmd.Parameters.Add(parametro);
                }

                // Apre una connessione a un database con le impostazioni delle proprietà specificate
                // dalla proprietà System.Data.SqlClient.SqlConnection.ConnectionString.
                conn.Open();

                // SqlDataAdapter è una classe che serve per contenere un set di comandi ('cmd') ed una connessione
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    try
                    {
                        // Aggiungo o aggiorno righe in un determinato intervallo del DataSet 'dt'
                        // affinché corrispondano a quelle nell'origine dati con il System.Data.DataTable
                        // nome.
                        ad.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    ad.Dispose();
                }
                cmd.Dispose();
            }
            conn.Dispose();
        }
        // Ritorno l'elemento dt
        return dt;
    } // Fine GetDataTable 





    /// <summary>
    /// GetScalar: Effettua la stampa di un singolo valore (di una singola riga)
    /// </summary>
    /// <param name="strSql">Stringa che contiene un comando SQL</param>
    /// <param name="sqlParametri">Insieme di parametri che compongono il mio comando SQL</param>
    /// <returns></returns>
    protected object GetScalar(string strSql, Collection<SqlParameter> sqlParametri = null)
    {/* TODO Change to default(_) if this is not a reference type */

        // Creo una nuova tabella di dati
        DataTable dt = new DataTable();
        object obj = new object();

        // Creo una nuova connessione, usando la stringa di connessione
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            // Creo un nuovo comando (insieme di parametri)
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                // Rimuovo tutti gli oggetti SqlParameter dalla SqlParameterCollections
                cmd.Parameters.Clear();

                // Se è falso che sqlParametri è == null
                if (!(sqlParametri == null))
                {
                    // Per ogni parametro nella collezione 'sqlParametri'
                    foreach (SqlParameter parametro in sqlParametri)
                        // Aggiungilo al comando
                        cmd.Parameters.Add(parametro);
                }

                // Apre una connessione a un database con le impostazioni delle proprietà specificate
                // dalla proprietà System.Data.SqlClient.SqlConnection.ConnectionString.
                conn.Open();

                // SqlDataAdapter è una classe che serve per contenere un set di comandi ('cmd') ed una connessione
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    // Aggiungo o aggiorno righe in un determinato intervallo del DataSet 'dt'
                    // affinché corrispondano a quelle nell'origine dati con il System.Data.DataTable
                    // nome.
                    ad.Fill(dt);
                    ad.Dispose();
                }
            }
            conn.Dispose();
        }

        // Mi salvo la riga della tabella (di fatto, lo scalare che sto cercando)
        obj = dt.Rows[0][0];

        // Ritorno lo scalare
        return obj;
    }


   






    /// <summary>
    /// Insert: Funzione di inserimento
    /// </summary>
    /// <param name="strSql"></param>
    /// <param name="sqlParametri"></param>
    /// <returns></returns>
    protected int Insert(Collection<string> strSql, Collection<Collection<SqlParameter>> sqlParametri)
    {
        int rowAffected = -1;
        int stringIndex = 0;

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    foreach (string stringa in strSql)
                    {
                        using (SqlCommand cmd = new SqlCommand(stringa, conn, trans))
                        {
                            if (!(sqlParametri[stringIndex] == null))
                            {
                                foreach (SqlParameter parametro in sqlParametri[stringIndex])
                                    cmd.Parameters.Add(parametro);
                            }
                            stringIndex += 1;
                            rowAffected = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            cmd.Dispose();
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    //this.RegistraLogQuery(ex.Message, strSql, stringIndex);
                    Console.WriteLine(ex.Message);
                    trans.Rollback();
                    return -1;
                }
            }
            conn.Dispose();
        }

        return rowAffected;
    }

    protected int Update(Collection<string> strSql, Collection<Collection<SqlParameter>> sqlParametri)
    {
        int rowAffected = -1;
        int stringIndex = 0;

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    foreach (string stringa in strSql)
                    {
                        using (SqlCommand cmd = new SqlCommand(stringa, conn, trans))
                        {
                            if (sqlParametri != null && sqlParametri[stringIndex] != null && sqlParametri[stringIndex].Count > 0)
                            {
                                foreach (SqlParameter parametro in sqlParametri[stringIndex])
                                    cmd.Parameters.Add(parametro);
                            }
                            stringIndex += 1;
                            rowAffected = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            cmd.Dispose();
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    // this.RegistraLogQuery(ex.Message, strSql, stringIndex);
                    trans.Rollback();
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
            conn.Dispose();
        }

        return rowAffected;
    }

    protected int Delete(Collection<string> strSql, Collection<Collection<SqlParameter>> sqlParametri)
    {
        int rowAffected = -1;
        int stringIndex = 0;

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    foreach (string stringa in strSql)
                    {
                        using (SqlCommand cmd = new SqlCommand(stringa, conn, trans))
                        {
                            // if (!sqlParametri.Item(stringIndex) == null)
                            if (!(sqlParametri[stringIndex] == null))
                            {
                                foreach (SqlParameter parametro in sqlParametri[stringIndex])
                                    cmd.Parameters.Add(parametro);
                            }
                            stringIndex += 1;
                            rowAffected = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            cmd.Dispose();
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    // this.RegistraLogQuery(ex.Message, strSql, stringIndex);
                    trans.Rollback();
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
            conn.Dispose();
        }

        return rowAffected;
    }

    protected int Transaction(Collection<string> strSql, Collection<Collection<SqlParameter>> sqlParametri)
    {
        int rowAffected = -1;

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    foreach (var stringa in strSql)
                    {
                        using (SqlCommand cmd = new SqlCommand(stringa, conn, trans))
                        {
                            if (!(sqlParametri[strSql.IndexOf(stringa)] == null))
                            {
                                foreach (SqlParameter parametro in sqlParametri[strSql.IndexOf(stringa)])
                                    cmd.Parameters.Add(parametro);
                            }

                            rowAffected = cmd.ExecuteNonQuery();
                            trans.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return rowAffected;
                }
            }
            conn.Dispose();
        }

        return rowAffected;
    }



    protected int InsertGetID(Collection<string> strSql, Collection<Collection<SqlParameter>> sqlParametri)
    {
        int rowAffected = 0;
        int id = 0;
        int stringIndex = 0;

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    foreach (string stringa in strSql)
                    {
                        using (SqlCommand cmd = new SqlCommand(stringa, conn, trans))
                        {
                            if (sqlParametri != null && sqlParametri[stringIndex] != null && sqlParametri[stringIndex].Count > 0)
                            {
                                foreach (SqlParameter parametro in sqlParametri[stringIndex])
                                    cmd.Parameters.Add(parametro);
                            }
                            stringIndex += 1;
                            id = (int)cmd.ExecuteScalar();
                            cmd.Parameters.Clear();
                            cmd.Dispose();
                        }
                    }
                    trans.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    // this.RegistraLogQuery(ex.Message, strSql, stringIndex);
                    trans.Rollback();
                    return -1;
                }
            }
            conn.Dispose();
        }
    }


    public object GetCacheData(string cacheItemName)
    {
        return HostingEnvironment.Cache.Get(cacheItemName);
    }

    public void SetCacheData(string cacheItemName, object dataSet, string tableName)
    {
        string cacheEntryname = "DatabaseSito";

        SqlCacheDependencyAdmin.EnableNotifications(this.strConn);
        SqlCacheDependencyAdmin.EnableTableForNotifications(this.strConn, tableName);

        SqlCacheDependency dependency = new SqlCacheDependency(cacheEntryname, tableName);
        HostingEnvironment.Cache.Insert(cacheItemName, dataSet, dependency);
    }

    public void SetCacheData(string cacheItemName, object dataSet, List<string> tablesName)
    {
        string cacheEntryname = "DatabaseSito";

        SqlCacheDependencyAdmin.EnableNotifications(this.strConn);
        SqlCacheDependencyAdmin.EnableTableForNotifications(this.strConn, tablesName.ToArray());

        AggregateCacheDependency aggDep = new AggregateCacheDependency();
        foreach (string tName in tablesName)
            aggDep.Add(new SqlCacheDependency(cacheEntryname, tName));

        HostingEnvironment.Cache.Insert(cacheItemName, dataSet, aggDep);
    }
}
