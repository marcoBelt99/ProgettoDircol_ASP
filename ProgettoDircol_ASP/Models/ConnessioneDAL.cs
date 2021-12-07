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

    // Funzione che mi permette di fare una query di stampa
    public DataTable GetDataTable(string strSql, Collection<SqlParameter> sqlParametri = null, int commandTimeout = 30)
    {
        // DataTable ha la stessa funzione degli array associativi del PHP. Contiene un insieme di righe (come risultato di una generica query)
        DataTable dt = new DataTable();

        // Quando metto questo using, alla fine devo sempre mettere un Dispose per chiudere tutte le conessioni
        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
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
                cmd.Parameters.Clear();

                if (!(sqlParametri == null))
                {
                    foreach (SqlParameter parametro in sqlParametri)
                        cmd.Parameters.Add(parametro);
                }

                conn.Open();
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    try
                    {
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


        return dt;
    }

    protected object GetScalar(string strSql, Collection<SqlParameter> sqlParametri = null)
    {/* TODO Change to default(_) if this is not a reference type */
        DataTable dt = new DataTable();
        object obj = new object();

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.Clear();

                if (!(sqlParametri == null))
                {
                    foreach (SqlParameter parametro in sqlParametri)
                        cmd.Parameters.Add(parametro);
                }

                conn.Open();
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    ad.Fill(dt);
                    ad.Dispose();
                }
            }
            conn.Dispose();
        }

        obj = dt.Rows[0][0];

        return obj;
        // obj = dt.Rows(0).Item(0);

        // return obj;
    }

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
