using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Collections.ObjectModel;
using System.Web.Hosting;
using System.Web.Caching;


public class ConnessioneDAL
{

    // Stringa di connessione (Che trovo anche in Web.config)
    private string strConn = WebConfigurationManager.ConnectionStrings("strConn").ConnectionString;

    protected DataTable GetDataTable(string strSql, Collection<SqlParameter> sqlParametri = null/* TODO Change to default(_) if this is not a reference type */, string commandTimeout = "")
    {
        // DataTable ha la stessa funzione degli array associativi del PHP. Contiene un insieme di righe (come risultato di una generica query)
        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                if (!string.IsNullOrEmpty(commandTimeout))
                {
                    try
                    {
                        cmd.CommandTimeout = commandTimeout;
                    }
                    catch (Exception ex)
                    {
                        RegistraLogQueryGetDataTable(ex, strSql, sqlParametri);
                    }
                }
                cmd.Parameters.Clear();

                if (!sqlParametri == null)
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
                        RegistraLogQueryGetDataTable(ex, strSql, sqlParametri);
                    }
                    ad.Dispose();
                }
                cmd.Dispose();
            }
            conn.Dispose();
        }

        return dt;
    }

    protected object GetScalar(string strSql, Collection<SqlParameter> sqlParametri = null/* TODO Change to default(_) if this is not a reference type */)
    {
        DataTable dt = new DataTable();
        object obj = new object();

        using (SqlConnection conn = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.Clear();

                if (!sqlParametri == null)
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

        obj = dt.Rows(0).Item(0);

        return obj;
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
                            if (!sqlParametri.Item(stringIndex) == null)
                            {
                                foreach (SqlParameter parametro in sqlParametri.Item(stringIndex))
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
                    this.RegistraLogQuery(ex.Message, strSql, stringIndex);
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
                            if (sqlParametri != null && sqlParametri.Item(stringIndex) != null && sqlParametri.Item(stringIndex).Count > 0)
                            {
                                foreach (SqlParameter parametro in sqlParametri.Item(stringIndex))
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
                    this.RegistraLogQuery(ex.Message, strSql, stringIndex);
                    trans.Rollback();
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
                            if (!sqlParametri.Item(stringIndex) == null)
                            {
                                foreach (SqlParameter parametro in sqlParametri.Item(stringIndex))
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
                    this.RegistraLogQuery(ex.Message, strSql, stringIndex);
                    trans.Rollback();
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
                            if (!sqlParametri.Item(strSql.IndexOf(stringa)) == null)
                            {
                                foreach (SqlParameter parametro in sqlParametri.Item(strSql.IndexOf(stringa)))
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

    private void RegistraLogQueryGetDataTable(Exception ex, string strSql, Collection<SqlParameter> sqlParametri)
    {
        System.Text.StringBuilder str = new System.Text.StringBuilder();
        str.AppendLine("Errore nell'esecuzione della query: ");
        str.AppendLine(strSql);
        str.AppendLine("con i seguenti parametri:");
        // str &= strSql
        if (sqlParametri != null)
        {
            foreach (SqlParameter par in sqlParametri)
            {
                str.Append(par.ParameterName);
                str.Append(": ");
                str.AppendLine(par.Value);
            }
        }
        // Dim exc As New Exception(str.ToString, New Exception())
        // ----- Redirect in caso di DEADLOCK
        if ((ex) is SqlException)
        {
            if ((SqlException)ex.Number == 1205 && System.Web.HttpContext.Current != null)
            {
                string url = System.Web.HttpContext.Current.Request.RawUrl;
                System.Threading.Thread.Sleep(500);
                System.Web.HttpContext.Current.Response.Redirect(url);
            }
            else
                Globals.Utility.StringsUtils.RegisterErrorLog(ex, str.ToString());
        }
        else
            Globals.Utility.StringsUtils.RegisterErrorLog(ex, str.ToString());
    }


    private void RegistraLogQuery(string msg, Collection<string> strSql, int stringIndex)
    {
        // Dim miofile As String = HostingEnvironment.MapPath("/public/log/log500.txt")
        // ' Creo un oggetto StreamWriter col metodo AppendText
        // Dim objStreamWriter As System.IO.StreamWriter
        // objStreamWriter = System.IO.File.AppendText(miofile)
        // ' Scrivo un linea di testo contenente l'orario attuale
        // objStreamWriter.WriteLine(" --------------------- Errore nell'esecuzione di una query SQL ---------------------")
        // objStreamWriter.WriteLine(DateTime.Now)
        // If strSql IsNot Nothing AndAlso strSql.Count > 0 Then
        // If stringIndex > 0 Then
        // objStreamWriter.WriteLine("Query: " & strSql(stringIndex - 1))
        // Else
        // objStreamWriter.WriteLine("Query: " & strSql(0))
        // End If
        // End If
        // objStreamWriter.WriteLine("Errore: " & msg)
        // objStreamWriter.WriteLine("")
        // Try
        // objStreamWriter.WriteLine("URL: " & System.Web.HttpContext.Current.Request.RawUrl)
        // Catch ex As Exception
        // End Try
        // objStreamWriter.Close()
        // objStreamWriter.Dispose()
        string str = msg + @"
";
        if (strSql != null && strSql.Count > 0)
        {
            if (stringIndex > 0)
                str += "Query: " + strSql(stringIndex - 1);
            else
                str += "Query: " + strSql(0);
        }
        Exception exc = new Exception(str, new Exception());
        Globals.Utility.StringsUtils.RegisterErrorLog(exc);
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
                            if (sqlParametri != null && sqlParametri.Item(stringIndex) != null && sqlParametri.Item(stringIndex).Count > 0)
                            {
                                foreach (SqlParameter parametro in sqlParametri.Item(stringIndex))
                                    cmd.Parameters.Add(parametro);
                            }
                            stringIndex += 1;
                            id = cmd.ExecuteScalar();
                            cmd.Parameters.Clear();
                            cmd.Dispose();
                        }
                    }
                    trans.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    this.RegistraLogQuery(ex.Message, strSql, stringIndex);
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
