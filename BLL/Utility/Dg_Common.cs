﻿using Microsoft.AspNetCore.Http;
using Microsoft.Reporting.NETCore;
using OfficeOpenXml;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.Reflection;

namespace BLL.Utility
{
    public class Dg_Common
    {
        public DataTable get_InformationDataTable(string sqlstatement, SqlConnection sqlCon)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(sqlstatement, sqlCon);
                sda.SelectCommand.CommandTimeout = 5000;
                if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
                {
                    sqlCon.Open();
                }
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return dt;
        }
        public DataTable get_InformationDataTableByType(string storedProcedureName, SqlConnection sqlCon, params SqlParameter[] parameters)
        {
            var dataTable = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
                {
                    sqlCon.Open();
                }
                SqlCommand cmd = new SqlCommand(storedProcedureName,sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 5000;
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                    dataTable.Load(cmd.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return dataTable;
        }
        public DataSet get_InformationDtaset(string sqlstatement, SqlConnection sqlCon)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(sqlstatement, sqlCon);
                sda.SelectCommand.CommandTimeout = 5000;
                if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
                {
                    sqlCon.Open();
                }
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return ds;
        }
        public bool saveChanges(string sqlQuery, SqlConnection sqlCon, Object parameterModel = null)
        {
            bool flag = false;
            try
            {
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlCon);
                if (parameterModel != null)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataSet _DataSet = this.GetTableColums(sqlQuery, sqlCon);
                    foreach (DataRow _Row in _DataSet.Tables[0].Rows)
                    {
                        string objKeyName = _Row[0].ToString().Substring(1);
                        string parameterType = _Row[0].ToString();
                        var parameterValue = parameterModel.GetType().GetProperty(objKeyName).GetValue(parameterModel, null);
                        if (parameterValue == null)
                        {
                            parameterValue = string.Empty;
                        }
                        if (string.IsNullOrEmpty(parameterValue.ToString()) || parameterValue.ToString() == "0")
                        {
                            cmd.Parameters.AddWithValue(parameterType, DBNull.Value);
                            cmd.CommandTimeout = 5000;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(parameterType, parameterValue);
                            cmd.CommandTimeout = 5000;
                        }
                    }
                    sqlCon.Open();
                    int exec = cmd.ExecuteNonQuery();
                    flag = exec > 0 ? true : false;

                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 5000;
                    sqlCon.Open();
                    int exec = cmd.ExecuteNonQuery();
                    flag = exec > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                flag = false;
            }
            finally
            {
                sqlCon.Close();
            }
            return flag;
        }
        public bool saveChangesByType(string storedProcedureName, SqlConnection sqlCon, params SqlParameter[] parameters)
        {
            bool flag = false;
            try
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName,sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                    sqlCon.Open();
                    int exec = cmd.ExecuteNonQuery();
                    flag = exec > 0 ? true : false;
                }               
            }
            catch (Exception ex)
            {
                ex.ToString();
                sqlCon.Close();
                flag = false;
            }
            finally
            {
                sqlCon.Close();
            }
            return flag;
        }
        public async Task<DataSet> get_InformationDtasetAsync(string sqlstatement, SqlConnection sqlCon)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(sqlstatement, sqlCon);
                sda.SelectCommand.CommandTimeout = 5000;
                if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
                {
                    await sqlCon.OpenAsync();
                }
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    await sqlCon.CloseAsync();
                }
            }
            return ds;
        }
        public async Task<DataTable> get_InformationDataTableAsync(string sqlstatement, SqlConnection sqlCon)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(sqlstatement, sqlCon);
                sda.SelectCommand.CommandTimeout = 5000;
                if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
                {
                    await sqlCon.OpenAsync();
                }
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    await sqlCon.CloseAsync();
                }
            }
            return dt;
        }
        public async Task<bool> saveChangesAsync(string sqlQuery, SqlConnection sqlCon, Object parameterModel = null)
        {
            bool flag = false;
            try
            {
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlCon);
                if (parameterModel != null)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataSet _DataSet = this.GetTableColums(sqlQuery, sqlCon);
                    foreach (DataRow _Row in _DataSet.Tables[0].Rows)
                    {
                        string objKeyName = _Row[0].ToString().Substring(1);
                        string parameterType = _Row[0].ToString();
                        var parameterValue = parameterModel.GetType().GetProperty(objKeyName).GetValue(parameterModel, null);
                        if (parameterValue == null)
                        {
                            parameterValue = string.Empty;
                        }
                        if (string.IsNullOrEmpty(parameterValue.ToString()) || parameterValue.ToString() == "0")
                        {
                            cmd.Parameters.AddWithValue(parameterType, DBNull.Value);
                            cmd.CommandTimeout = 5000;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(parameterType, parameterValue);
                            cmd.CommandTimeout = 5000;
                        }
                    }
                    await sqlCon.OpenAsync();
                    int exec = await cmd.ExecuteNonQueryAsync();
                    flag = exec > 0 ? true : false;
                    
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 5000;
                    await sqlCon.OpenAsync();
                    int exec = await cmd.ExecuteNonQueryAsync();
                    flag = exec > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                flag = false;
            }
            finally
            {
                await sqlCon.CloseAsync();
            }
            return flag;
        }
        public async Task<bool> saveChangesAsync(string[] sqlQueryArr, SqlConnection sqlCon, Object parameterModel = null)
        {
            bool flag = false;
            SqlTransaction transaction = null;
            try
            {
                if (sqlCon.State == ConnectionState.Closed || sqlCon.State == ConnectionState.Broken)
                {
                    await sqlCon.OpenAsync();
                }
                transaction = await Task.Run<SqlTransaction>(() => sqlCon.BeginTransaction());
                SqlCommand cmd = sqlCon.CreateCommand();
                cmd.Connection = sqlCon;
                cmd.Transaction = transaction;
                if (parameterModel != null)
                {
                    for (int i = 0; i < sqlQueryArr.Length; i++)
                    {
                        cmd.CommandText = sqlQueryArr[i];
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        DataSet _DataSet = this.GetColumsForSqltransaction(sqlQueryArr[i], sqlCon, transaction);
                        foreach (DataRow _Row in _DataSet.Tables[0].Rows)
                        {
                            string objKeyName = _Row[0].ToString().Substring(1);
                            string parameterType = _Row[0].ToString();
                            var parameterValue = parameterModel.GetType().GetProperty(objKeyName).GetValue(parameterModel, null);
                            if (parameterValue == null)
                            {
                                parameterValue = string.Empty;
                            }
                            if (string.IsNullOrEmpty(parameterValue.ToString()) || parameterValue.ToString() == "0")
                            {
                                cmd.Parameters.AddWithValue(parameterType, DBNull.Value);
                                cmd.CommandTimeout = 5000;
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(parameterType, parameterValue);
                            }
                            //cmd.Parameters.AddWithValue(_Row[0].ToString(), parameterModel.GetType().GetProperty(objKeyName).GetValue(parameterModel, null));
                        }
                        int exec = await cmd.ExecuteNonQueryAsync();
                        flag = exec > 0 ? true : false;
                    }
                    await Task.Run(() => transaction.Commit());
                }
                else
                {
                    for (int i = 0; i < sqlQueryArr.Length; i++)
                    {
                        cmd.CommandText = sqlQueryArr[i];
                        cmd.CommandTimeout = 5000;
                        cmd.CommandType = CommandType.Text;
                        int exec = await cmd.ExecuteNonQueryAsync();
                        flag = exec > 0 ? true : false;
                    }
                    await Task.Run(() => transaction.Commit());
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                await transaction.RollbackAsync();
                flag = false;
            }
            finally
            {
                await transaction.DisposeAsync();
                if (sqlCon.State == ConnectionState.Open)
                {
                    await sqlCon.CloseAsync();
                }
            }
            return flag;
        }
        public T GetSingleListObject<T>(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
                var properties = typeof(T).GetProperties();
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        if (pro.PropertyType == typeof(int) || pro.PropertyType == typeof(Nullable<int>))
                        {
                            pro.SetValue(objT, Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0][pro.Name].ToString()) ? default(T) : dt.Rows[0][pro.Name]));
                        }
                        else if (pro.PropertyType == typeof(string))
                        {
                            pro.SetValue(objT, string.IsNullOrEmpty(dt.Rows[0][pro.Name].ToString()) ? default(T) : dt.Rows[0][pro.Name].ToString().Trim());
                        }
                        else if (pro.PropertyType == typeof(decimal) || pro.PropertyType == typeof(Nullable<decimal>))
                        {
                            pro.SetValue(objT, Convert.ToDecimal(string.IsNullOrEmpty(dt.Rows[0][pro.Name].ToString()) ? default(T) : dt.Rows[0][pro.Name]));
                        }
                        else if (pro.PropertyType == typeof(bool) || pro.PropertyType == typeof(Nullable<bool>))
                        {
                            pro.SetValue(objT, Convert.ToBoolean(string.IsNullOrEmpty(dt.Rows[0][pro.Name].ToString()) ? default(T) : dt.Rows[0][pro.Name]));
                        }
                        else if (pro.PropertyType == typeof(DateTime) || pro.PropertyType == typeof(Nullable<DateTime>))
                        {
                            pro.SetValue(objT, string.IsNullOrEmpty(dt.Rows[0][pro.Name].ToString()) ? default(T) : dt.Rows[0][pro.Name]);
                        }
                        else
                        {
                            pro.SetValue(objT, dt.Rows[0][pro.Name]);
                        }
                    }
                }
                return objT;
            }
            return default(T);
        }
        public List<T> GetToListObject<T>(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
                var properties = typeof(T).GetProperties();
                DataRow[] rows = dt.Select();
                return rows.Select(row =>
                {
                    var objT = Activator.CreateInstance<T>();
                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name))
                        {
                            if (pro.PropertyType == typeof(int) || pro.PropertyType == typeof(Nullable<int>))
                            {
                                pro.SetValue(objT, Convert.ToInt32(string.IsNullOrEmpty(row[pro.Name].ToString()) ? default(T) : row[pro.Name]));
                            }
                            else if (pro.PropertyType == typeof(string))
                            {
                                pro.SetValue(objT, string.IsNullOrEmpty(row[pro.Name].ToString()) ? default(T) : row[pro.Name].ToString().Trim());
                            }
                            else if (pro.PropertyType == typeof(decimal) || pro.PropertyType == typeof(Nullable<decimal>))
                            {
                                pro.SetValue(objT, Convert.ToDecimal(string.IsNullOrEmpty(dt.Rows[0][pro.Name].ToString()) ? default(T) : dt.Rows[0][pro.Name]));
                            }
                            else if (pro.PropertyType == typeof(bool) || pro.PropertyType == typeof(Nullable<bool>))
                            {
                                pro.SetValue(objT, Convert.ToBoolean(string.IsNullOrEmpty(row[pro.Name].ToString()) ? default(T) : row[pro.Name]));
                            }
                            else if (pro.PropertyType == typeof(DateTime) || pro.PropertyType == typeof(Nullable<DateTime>))
                            {
                                pro.SetValue(objT, string.IsNullOrEmpty(row[pro.Name].ToString()) ? default(T) : row[pro.Name]);
                            }
                            else
                            {
                                pro.SetValue(objT, row[pro.Name]);
                            }
                        }
                    }
                    return objT;
                }).ToList();
            }
            return default(List<T>);
        }
        public async Task<ExcelWorksheet> GetExcelWorkSheet(IFormFile formFile,int sheetIndex)
        {
            ExcelWorksheet worksheet;
            using (var strem = new MemoryStream())
            {
                await formFile.CopyToAsync(strem);
                var package = new ExcelPackage(strem);
                worksheet = package.Workbook.Worksheets[sheetIndex];
            }
            return worksheet;
        }
        public DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo props in Props)
            {
                dataTable.Columns.Add(props.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        //public List<T> DataTableToList<T>(DataTable dt)
        //{
        //    const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
        //    var columnNames = dt.Columns.Cast<DataColumn>()
        //        .Select(c => c.ColumnName)
        //        .ToList();
        //    var objectProperties = typeof(T).GetProperties(flags);
        //    var targetList = dt.AsEnumerable().Select(dataRow =>
        //    {
        //        var instanceOfT = Activator.CreateInstance<T>();

        //        foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
        //        {
        //            properties.SetValue(instanceOfT, dataRow[properties.Name], null);
        //        }
        //        return instanceOfT;
        //    }).ToList();

        //    return targetList;
        //}
        public List<T> DataTableToList<T>(DataTable table) where T : new()
        {
            List<T> list = new List<T>();
            var typeProperties = typeof(T).GetProperties().Select(propertyInfo => new
            {
                PropertyInfo = propertyInfo,
                Type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType
            }).ToList();

            foreach (var row in table.Rows.Cast<DataRow>())
            {
                T obj = new T();
                foreach (var typeProperty in typeProperties)
                {
                    object value = row[typeProperty.PropertyInfo.Name];
                    object safeValue = value == null || DBNull.Value.Equals(value)
                        ? null
                        : Convert.ChangeType(value, typeProperty.Type);

                    typeProperty.PropertyInfo.SetValue(obj, safeValue, null);
                }
                list.Add(obj);
            }
            return list;
        }

        private DataSet GetTableColums(string tableName, SqlConnection sqlCon)
        {
            DataSet ds = this.get_InformationDtaset("SELECT syscolumns.name FROM syscolumns WHERE syscolumns.id = object_id('" + tableName + "') ORDER BY syscolumns.colid ASC", sqlCon);
            return ds;
        }
        private DataTable GetSpSelectTableColums(string tableName, SqlConnection sqlCon)
        {
            var dt = this.get_InformationDataTable("SELECT name FROM sys.dm_exec_describe_first_result_set_for_object(OBJECT_ID('"+ tableName + "'),0) ORDER BY column_ordinal ASC", sqlCon);
            return dt;
        }
        private DataSet GetColumsForSqltransaction(string tableName, SqlConnection sqlCon, SqlTransaction transaction)
        {
            DataSet _DataSet;
            SqlCommand _Command = new SqlCommand("SELECT syscolumns.name FROM syscolumns WHERE syscolumns.id = object_id('" + tableName + "') ORDER BY syscolumns.colid ASC", sqlCon, transaction);
            SqlDataAdapter _Adapter = new SqlDataAdapter(_Command);
            _Adapter.Fill(_DataSet = new DataSet(), tableName);
            return _DataSet;
        }

        //Common Report Code
        public byte[] GenerateReport(DataTable dataTable, string datasetName, string rdlcFilePath, string reportType, ReportParameterCollection reportParameters = null)
        {
            LocalReport localReport = new LocalReport();
            ReportDataSource reportDataSource = new ReportDataSource(datasetName, dataTable);
            localReport.ReportPath = rdlcFilePath;
            localReport.EnableExternalImages = true;
            localReport.DataSources.Clear();
            localReport.DataSources.Add(reportDataSource);
            if (reportParameters != null)
            {
                localReport.SetParameters(reportParameters);
            }
            localReport.Refresh();
            byte[] reportBytes;
            switch (reportType.ToUpper())
            {
                case "PDF":
                    reportBytes = localReport.Render("PDF");
                    break;
                case "EXCEL":
                    reportBytes = localReport.Render("EXCELOPENXML");
                    break;
                case "WORD":
                    reportBytes = localReport.Render("WORDOPENXML");
                    break;
                default:
                    throw new ArgumentException("Unsupported report type");
            }
            return reportBytes;
        }
        public byte[] GenerateReport(DataTable[] dataTable, string[] datasetName, string rdlcFilePath, string reportType, ReportParameterCollection reportParameters = null)
        {
            LocalReport localReport = new LocalReport();
            localReport.DataSources.Clear();
            for (int i = 0; i < dataTable.Length; i++)
            {
                string dataset = datasetName[i];
                var dt = dataTable[i];
                ReportDataSource reportDataSource = new ReportDataSource(dataset, dt);
                localReport.DataSources.Add(reportDataSource);
            }
            localReport.ReportPath = rdlcFilePath;
            localReport.EnableExternalImages = true;
            if (reportParameters != null)
            {
                localReport.SetParameters(reportParameters);
            }
            localReport.Refresh();
            byte[] reportBytes;
            switch (reportType.ToUpper())
            {
                case "PDF":
                    reportBytes = localReport.Render("PDF");
                    break;
                case "EXCEL":
                    reportBytes = localReport.Render("EXCELOPENXML");
                    break;
                case "WORD":
                    reportBytes = localReport.Render("WORDOPENXML");
                    break;
                default:
                    throw new ArgumentException("Unsupported report type");
            }
            return reportBytes;
        }
        public string GetContentType(string reportType)
        {
            switch (reportType.ToUpper())
            {
                case "PDF":
                    return "rpt.pdf";
                case "EXCEL":
                    return "rpt.xls";
                case "WORD":
                    return "rpt.doc";
                default:
                    throw new ArgumentException("Unsupported report type");
            }
        }
        public Image Crop(string path, int width, int height)
        {
            Image img = Image.FromFile(path);
            Bitmap bmp = new Bitmap(width, height);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(img, 0, 0);
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                return bmp;
            }
        }

        //public static class Extensions
        //{
        //    public static void AddArrayParameters<T>(this SqlCommand cmd, string name, IEnumerable<T> values)
        //    {
        //        name = name.StartsWith("@") ? name : "@" + name;
        //        var names = string.Join(", ", values.Select((value, i) => {
        //            var paramName = name + i;
        //            cmd.Parameters.AddWithValue(paramName, value);
        //            return paramName;
        //        }));
        //        cmd.CommandText = cmd.CommandText.Replace(name, names);
        //    }
        //}
        //var ageList = new List<int> { 1, 3, 5, 7, 9, 11 };
        //var cmd = new SqlCommand();
        //cmd.CommandText = "SELECT * FROM MyTable WHERE Age IN (@Age)";    
        //cmd.AddArrayParameters("Age", ageList);
    }
}