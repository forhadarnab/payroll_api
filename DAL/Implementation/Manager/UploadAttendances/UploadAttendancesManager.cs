using BLL.Interfaces.Manager.UploadAttendances;
using BLL.Utility;
using BOL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL.Implementation.Manager.UploadAttendances
{
    public class UploadAttendancesManager : IUploadAttendancesManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SqlConnection _sqlConnection;
        public UploadAttendancesManager(Dg_Common dgCommon, IWebHostEnvironment webHostEnvironment)
        {
            _dgCommon = dgCommon;
            _webHostEnvironment = webHostEnvironment;
            _sqlConnection = new SqlConnection(Getway.Dg_Payroll);
        }

        public async Task<List<MyEntity>> WriteFile([FromForm] UploadFile model)
        {
            List<MyEntity> entities = new List<MyEntity>();
            int integer;
            int num;
            int integer1;
            int num1;
            int integer2;
            int num2;
            int integer3;
            int num3;
            int integer4;
            int num4;
            int integer5;
            int num5;
            int integer6;
            int num6;
            try
            {
                if (model.file.Length > 0)
                {
                    string filepath = $"{_webHostEnvironment.WebRootPath}\\TextFileUpload\\";
                    string[] fileArr = new string[] { filepath, model.CompanyID.ToString(), "_", null, null, null, null, null, null };
                    int day = this.GetCurrentDateTime().Day;
                    fileArr[3] = day.ToString();
                    int month = this.GetCurrentDateTime().Month;
                    fileArr[4] = month.ToString();
                    int year = this.GetCurrentDateTime().Year;
                    fileArr[5] = year.ToString();
                    int minute = this.GetCurrentDateTime().Minute;
                    fileArr[6] = minute.ToString();
                    int second = this.GetCurrentDateTime().Second;
                    fileArr[7] = second.ToString();
                    fileArr[8] = ".txt";
                    string fileFull = string.Concat(fileArr);
                    using (var stream = new FileStream(fileFull, FileMode.Create))
                    {
                        await model.file.CopyToAsync(stream);
                    }
                    DataTable dtClock = await this.GetPayAttnClock(model.CompanyID);
                    if (dtClock.Rows.Count > 0)
                    {
                        num = Convert.ToInt32(dtClock.Rows[0]["adi_stposition"].ToString());
                        integer = Convert.ToInt32(dtClock.Rows[0]["adi_noofposition"].ToString());
                        var dtDay = await this.GetPayAttnDay(model.CompanyID);
                        if (dtDay.Rows.Count > 0)
                        {
                            num1 = Convert.ToInt32(dtDay.Rows[0]["adi_stposition"].ToString());
                            integer1 = Convert.ToInt32(dtDay.Rows[0]["adi_noofposition"].ToString());
                        }
                        else
                        {
                            num1 = 0;
                            integer1 = 0;
                        }
                        var dtMonth = await this.GetPayAttnMonth(model.CompanyID);
                        if (dtMonth.Rows.Count > 0)
                        {
                            num2 = Convert.ToInt32(dtMonth.Rows[0]["adi_stposition"].ToString());
                            integer2 = Convert.ToInt32(dtMonth.Rows[0]["adi_noofposition"].ToString());
                        }
                        else
                        {
                            num2 = 0;
                            integer2 = 0;
                        }
                        var dtYear = await this.GetPayAttnYear(model.CompanyID);
                        if (dtYear.Rows.Count > 0)
                        {
                            num3 = Convert.ToInt32(dtYear.Rows[0]["adi_stposition"].ToString());
                            integer3 = Convert.ToInt32(dtYear.Rows[0]["adi_noofposition"].ToString());
                        }
                        else
                        {
                            num3 = 0;
                            integer3 = 0;
                        }
                        var dtHours = await this.GetPayAttnHours(model.CompanyID);
                        if (dtHours.Rows.Count > 0)
                        {
                            num4 = Convert.ToInt32(dtHours.Rows[0]["adi_stposition"].ToString());
                            integer4 = Convert.ToInt32(dtHours.Rows[0]["adi_noofposition"].ToString());
                        }
                        else
                        {
                            num4 = 0;
                            integer4 = 0;
                        }
                        var dtMinutes = await this.GetPayAttnMinutes(model.CompanyID);
                        if (dtMinutes.Rows.Count > 0)
                        {
                            num5 = Convert.ToInt32(dtMinutes.Rows[0]["adi_stposition"].ToString());
                            integer5 = Convert.ToInt32(dtMinutes.Rows[0]["adi_noofposition"].ToString());
                        }
                        else
                        {
                            num5 = 0;
                            integer5 = 0;
                        }
                        var dtProxID = await this.GetPayAttnProxID(model.CompanyID);
                        if (dtProxID.Rows.Count > 0)
                        {
                            num6 = Convert.ToInt32(dtProxID.Rows[0]["adi_stposition"].ToString());
                            integer6 = Convert.ToInt32(dtProxID.Rows[0]["adi_noofposition"].ToString());
                        }
                        else
                        {
                            num6 = 0;
                            integer6 = 0;
                        }
                        using (FileStream file2 = new FileStream(fileFull, FileMode.Open, FileAccess.Read))
                        {
                            using (StreamReader reader = new StreamReader(file2))
                            {
                                string line = string.Empty;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    string proxID = line.Substring(num6, integer6);
                                    string empSerial = string.Empty;
                                    var dtProxid = await this.GetEmployeeSl(int.Parse(proxID), model.CompanyID);
                                    if (dtProxid.Rows.Count > 0)
                                    {
                                        empSerial = dtProxid.Rows[0]["emp_serial"].ToString();
                                    }
                                    string[] dateArr = new string[]{ };
                                    if (integer3 != 4)
                                    {
                                        dateArr = new string[] { "20" + line.Substring(num3, integer3), "/", line.Substring(num2, integer2), "/", line.Substring(num1, integer1) };
                                    }
                                    else
                                    {
                                        dateArr = new string[] { line.Substring(num3, integer3), "/", line.Substring(num2, integer2), "/", line.Substring(num1, integer1) };
                                    }                                   
                                    string nDate = Convert.ToDateTime(string.Concat(dateArr)).ToString("yyyy/MM/dd");
                                    string[] timeArr = new string[] { line.Substring(num4, integer4), ".", line.Substring(num5, integer5) };
                                    string ntime = string.Concat(timeArr);
                                    var entity = new MyEntity
                                    {
                                        emp_Slno = empSerial.ToString(),
                                        emp_ProxID = proxID,
                                        date = nDate,
                                        time = ntime,
                                    };
                                    if (empSerial != "0")
                                    {
                                        //await this.InsertDataAttn(empSerial.ToString(), proxID, nDate, ntime);
                                    }
                                    entities.Add(entity);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return entities;
        }
        public async Task<List<MyEntity>> WriteFile1([FromForm] UploadFile model)
        {
            
            //DateTime resultF;
            //DateTime.TryParseExact("12032023", "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out resultF);
            //DateTime.TryParseExact("20231202", "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out resultF);
            List<MyEntity> entities = new List<MyEntity>();
            int integer;
            int num;
            int integer1;
            int num1;
            int integer2;
            int num2;
            int integer3;
            int num3;
            int integer4;
            int num4;
            int integer5;
            int num5;
            int integer6;
            int num6;
            try
            {
                if (model.file.Length > 0)
                {
                    string filepath = $"{_webHostEnvironment.WebRootPath}\\TextFileUpload\\";
                    string[] fileArr = new string[] { filepath, model.CompanyID.ToString(), "_", null, null, null, null, null, null };
                    int day = this.GetCurrentDateTime().Day;
                    fileArr[3] = day.ToString();
                    int month = this.GetCurrentDateTime().Month;
                    fileArr[4] = month.ToString();
                    int year = this.GetCurrentDateTime().Year;
                    fileArr[5] = year.ToString();
                    int minute = this.GetCurrentDateTime().Minute;
                    fileArr[6] = minute.ToString();
                    int second = this.GetCurrentDateTime().Second;
                    fileArr[7] = second.ToString();
                    fileArr[8] = ".txt";
                    string fileFull = string.Concat(fileArr);
                    using (var stream = new FileStream(fileFull, FileMode.Create))
                    {
                        await model.file.CopyToAsync(stream);
                    }
                    var dtClock = await this.GetPayAttnClock1(model.CompanyID);
                    if (dtClock.Rows.Count > 0)
                    {
                        num = Convert.ToInt32(dtClock.Rows[0]["adi_stposition1"].ToString());
                        integer = Convert.ToInt32(dtClock.Rows[0]["adi_noofposition1"].ToString());
                        var dtDay = await this.GetPayAttnDay1(model.CompanyID);
                        if (dtDay.Rows.Count > 0)
                        {
                            num1 = Convert.ToInt32(dtDay.Rows[0]["adi_stposition1"].ToString());
                            integer1 = Convert.ToInt32(dtDay.Rows[0]["adi_noofposition1"].ToString());
                        }
                        else
                        {
                            num1 = 0;
                            integer1 = 0;
                        }
                        var dtMonth = await this.GetPayAttnMonth1(model.CompanyID);
                        if (dtMonth.Rows.Count > 0)
                        {
                            num2 = Convert.ToInt32(dtMonth.Rows[0]["adi_stposition1"].ToString());
                            integer2 = Convert.ToInt32(dtMonth.Rows[0]["adi_noofposition1"].ToString());
                        }
                        else
                        {
                            num2 = 0;
                            integer2 = 0;
                        }
                        var dtYear = await this.GetPayAttnYear1(model.CompanyID);
                        if (dtYear.Rows.Count > 0)
                        {
                            num3 = Convert.ToInt32(dtYear.Rows[0]["adi_stposition1"].ToString());
                            integer3 = Convert.ToInt32(dtYear.Rows[0]["adi_noofposition1"].ToString());
                        }
                        else
                        {
                            num3 = 0;
                            integer3 = 0;
                        }
                        var dtHours = await this.GetPayAttnHours1(model.CompanyID);
                        if (dtHours.Rows.Count > 0)
                        {
                            num4 = Convert.ToInt32(dtHours.Rows[0]["adi_stposition1"].ToString());
                            integer4 = Convert.ToInt32(dtHours.Rows[0]["adi_noofposition1"].ToString());
                        }
                        else
                        {
                            num4 = 0;
                            integer4 = 0;
                        }
                        var dtMinutes = await this.GetPayAttnMinutes1(model.CompanyID);
                        if (dtMinutes.Rows.Count > 0)
                        {
                            num5 = Convert.ToInt32(dtMinutes.Rows[0]["adi_stposition1"].ToString());
                            integer5 = Convert.ToInt32(dtMinutes.Rows[0]["adi_noofposition1"].ToString());
                        }
                        else
                        {
                            num5 = 0;
                            integer5 = 0;
                        }
                        var dtProxID = await this.GetPayAttnProxID1(model.CompanyID);
                        if (dtProxID.Rows.Count > 0)
                        {
                            num6 = Convert.ToInt32(dtProxID.Rows[0]["adi_stposition1"].ToString());
                            integer6 = Convert.ToInt32(dtProxID.Rows[0]["adi_noofposition1"].ToString());
                        }
                        else
                        {
                            num6 = 0;
                            integer6 = 0;
                        }
                        using (FileStream file2 = new FileStream(fileFull, FileMode.Open, FileAccess.Read))
                        {
                            string value = Strings.Right("",4);
                            //StringBuilder sb = new StringBuilder();
                            using (StreamReader reader = new StreamReader(file2))
                            {
                                string line = string.Empty;
                                while ((line = reader.ReadLine().Trim()) != null)
                                {
                                    //sb.AppendLine(line);
                                    //string content = sb.ToString();
                                    //string cleanedContent = content.Replace("\t",string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(":", string.Empty).Replace(" ", string.Empty).Replace(",", string.Empty).Replace("|", string.Empty);
                                    string proxID = line.Substring(num6, integer6);                                    
                                    string empSerial = string.Empty;
                                    var dtProxid = await this.GetEmployeeSl(int.Parse(proxID),model.CompanyID);
                                    if (dtProxid.Rows.Count > 0)
                                    {
                                        empSerial = dtProxid.Rows[0]["emp_serial"].ToString();
                                    }
                                    string[] dateArr = new string[] { };
                                    int dateLen = line.Substring(num1, integer1).Length;
                                    if (integer3 != 4)
                                    {
                                        dateArr = new string[] { line.Substring(num3, integer3), "/", line.Substring(num2, integer2), "/", "20" + line.Substring(num1, integer1) };
                                    }
                                    else
                                    {
                                        dateArr = new string[] { line.Substring(num3, integer3), "/", line.Substring(num2, integer2), "/", line.Substring(num1, integer1) };
                                    }                                    
                                    string nDate = Convert.ToDateTime(string.Concat(dateArr)).ToString("yyyy/MM/dd");
                                    string[] timeArr = new string[] { line.Substring(num4, integer4), ".", line.Substring(num5, integer5) };
                                    string ntime = string.Concat(timeArr);
                                    var entity = new MyEntity
                                    {
                                        emp_Slno = empSerial.ToString(),
                                        emp_ProxID = proxID,
                                        date = nDate,
                                        time = ntime,
                                    };
                                    if (empSerial != "0")
                                    {
                                        await this.InsertDataAttn(empSerial.ToString(), proxID, nDate, ntime);
                                    }
                                    entities.Add(entity);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return entities;
        }

        //New Action By Forhad 12/04/2023
        public async Task<List<object>> ReadAttnTextFile([FromForm] UploadFile model)
        {
            var response = new List<object>();
            int numDateIndex=0;
            int dateLength=0;
            int numHourIndex = 0;
            int HourLength = 0;
            int numMinuteIndex = 0;
            int MinuteLength = 0;
            int numProxyIndex = 0;
            int ProxyLength = 0;
            try
            {
                if (model.file.Length > 0)
                {
                    string filepath = $"{_webHostEnvironment.WebRootPath}\\TextFileUpload\\";
                    string[] fileArr = new string[] { filepath, model.CompanyID.ToString(), "_", null, null, null, null, null, ".txt" };
                    fileArr[3] = DateTime.Now.Day.ToString();
                    fileArr[4] = DateTime.Now.Month.ToString();
                    fileArr[5] = DateTime.Now.Year.ToString();
                    fileArr[6] = DateTime.Now.Minute.ToString();
                    fileArr[7] = DateTime.Now.Second.ToString();
                    string fileFull = string.Concat(fileArr);
                    using (var stream = new FileStream(fileFull, FileMode.Create))
                    {
                        await model.file.CopyToAsync(stream);
                    }
                    var dtDate = await this.GetPayAttnTextFileRead_Date(model.CompanyID);
                    var dtHour = await this.GetPayAttnTextFileRead_Hour(model.CompanyID);
                    var dtMinute = await this.GetPayAttnTextFileRead_Minute(model.CompanyID);
                    var dtProxy = await this.GetPayAttnTextFileRead_proxy(model.CompanyID);
                    if (dtDate.Rows.Count > 0)
                    {
                        numDateIndex = Convert.ToInt32(dtDate.Rows[0]["txt_redStart"]);
                        dateLength = Convert.ToInt32(dtDate.Rows[0]["txt_redLength"]);
                    }
                    if (dtHour.Rows.Count > 0)
                    {
                        numHourIndex = Convert.ToInt32(dtHour.Rows[0]["txt_redStart"]);
                        HourLength = Convert.ToInt32(dtHour.Rows[0]["txt_redLength"]);
                    }
                    if (dtMinute.Rows.Count > 0)
                    {
                        numMinuteIndex = Convert.ToInt32(dtMinute.Rows[0]["txt_redStart"]);
                        MinuteLength = Convert.ToInt32(dtMinute.Rows[0]["txt_redLength"]);
                    }
                    if (dtProxy.Rows.Count > 0)
                    {
                        numProxyIndex = Convert.ToInt32(dtProxy.Rows[0]["txt_redStart"]);
                        ProxyLength = Convert.ToInt32(dtProxy.Rows[0]["txt_redLength"]);
                    }
                    var fileLines = File.ReadAllLines(fileFull);
                    if (fileLines.Length > 0)
                    {
                        foreach (var line in fileLines)
                        {
                            try
                            {
                                string textFileLine = string.Empty;
                                string replaceLine = line.Replace("\t", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(":", string.Empty).Replace(" ", string.Empty).Replace(",", string.Empty).Replace("|", string.Empty).Replace("-", string.Empty).Replace("/",string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                                if (model.CompanyID == 40 || model.CompanyID == 38) //OK
                                {
                                    textFileLine = replaceLine.Substring(0, 22);
                                }
                                else if (model.CompanyID == 53 || model.CompanyID == 37) //OK
                                {
                                    //string Line1st = string.Empty;
                                    //string Line2nd = string.Empty;
                                    //string[] Line1st2ndArr;
                                    if (replaceLine.Length == 20)
                                    {
                                        replaceLine = replaceLine.Insert(14, "  ");
                                        textFileLine = replaceLine.Substring(0, 22);
                                    }
                                    else
                                    {
                                        //replaceLine = replaceLine.Substring (0, 22);
                                        //Line1st = new string(replaceLine.Reverse().Take(14).Reverse().ToArray());
                                        //Line2nd = replaceLine.Substring(0, replaceLine.Length - Line1st.Length);
                                        //Line1st2ndArr = new string[] { Line1st, Line2nd };
                                        textFileLine = replaceLine.Substring(0, 22);
                                    }
                                }
                                else if (model.CompanyID == 49) //OK
                                {
                                    if (replaceLine.Length < 22)
                                    {
                                        replaceLine = replaceLine.Length == 15 ? replaceLine.Insert(14, "       ") : replaceLine;
                                        replaceLine = replaceLine.Length == 16 ? replaceLine.Insert(14, "      ") : replaceLine;
                                        replaceLine = replaceLine.Length == 17 ? replaceLine.Insert(14, "     ") : replaceLine;
                                        replaceLine = replaceLine.Length == 18 ? replaceLine.Insert(14, "    ") : replaceLine;
                                        replaceLine = replaceLine.Length == 19 ? replaceLine.Insert(14, "   ") : replaceLine;
                                        replaceLine = replaceLine.Length == 20 ? replaceLine.Insert(14, "  ") : replaceLine;
                                        replaceLine = replaceLine.Length == 21 ? replaceLine.Insert(14, " ") : replaceLine;
                                        textFileLine = replaceLine.Substring(0, 22);
                                    }
                                    else
                                    {
                                        textFileLine = replaceLine.Substring(0, 22);
                                    }
                                }
                                else if(model.CompanyID == 41) //OK
                                {
                                    if (replaceLine.Length < 22)
                                    {
                                        replaceLine = replaceLine.Length == 15 ? replaceLine.Insert(14, "       ") : replaceLine;
                                        replaceLine = replaceLine.Length == 16 ? replaceLine.Insert(14, "      ") : replaceLine;
                                        replaceLine = replaceLine.Length == 17 ? replaceLine.Insert(14, "     ") : replaceLine;
                                        replaceLine = replaceLine.Length == 18 ? replaceLine.Insert(14, "    ") : replaceLine;
                                        replaceLine = replaceLine.Length == 19 ? replaceLine.Insert(14, "   ") : replaceLine;
                                        replaceLine = replaceLine.Length == 20 ? replaceLine.Insert(14, "  ") : replaceLine;
                                        replaceLine = replaceLine.Length == 21 ? replaceLine.Insert(14, " ") : replaceLine;
                                        textFileLine = replaceLine.Substring(0,22);
                                    }
                                    else
                                    {
                                        textFileLine = replaceLine.Substring(0, 22);
                                    }
                                }
                                else if (model.CompanyID == 46 ||  model.CompanyID == 54 || model.CompanyID == 55)
                                {
                                    string Line1st = string.Empty;
                                    string Line2nd = string.Empty;
                                    string[] Line1st2ndArr;
                                    if (replaceLine.Length < 22)
                                    {
                                        Line1st = new string(replaceLine.Reverse().Take(14).Reverse().ToArray());
                                        Line2nd = replaceLine.Substring(0, replaceLine.Length - Line1st.Length);
                                        Line1st2ndArr = new string[] { Line1st, Line2nd };
                                        replaceLine = string.Concat(Line1st2ndArr);
                                        replaceLine = replaceLine.Length == 15 ? replaceLine.Insert(14, "       ") : replaceLine;
                                        replaceLine = replaceLine.Length == 16 ? replaceLine.Insert(14, "      ") : replaceLine;
                                        replaceLine = replaceLine.Length == 17 ? replaceLine.Insert(14, "     ") : replaceLine;
                                        replaceLine = replaceLine.Length == 18 ? replaceLine.Insert(14, "    ") : replaceLine;
                                        replaceLine = replaceLine.Length == 19 ? replaceLine.Insert(14, "   ") : replaceLine;
                                        replaceLine = replaceLine.Length == 20 ? replaceLine.Insert(14, "  ") : replaceLine;
                                        replaceLine = replaceLine.Length == 21 ? replaceLine.Insert(14, " ") : replaceLine;
                                        textFileLine = replaceLine.Substring(0, 22);
                                    }
                                    else
                                    {
                                        textFileLine = replaceLine.Substring(0, 22);
                                    }
                                }
                                string setDate = this.MakeDateFormat(textFileLine.Substring(numDateIndex, dateLength).Trim(),model.DateFormat); //Date Format yyyy/MM/dd
                                string setProxy = textFileLine.Substring(numProxyIndex, ProxyLength).Trim();
                                string[] timeArr = new string[] { textFileLine.Substring(numHourIndex, HourLength).Trim(), ".", textFileLine.Substring(numMinuteIndex, MinuteLength).Trim() };
                                string ntime = string.Concat(timeArr);
                                if (ntime == "00.00")
                                {
                                    ntime = ntime.Substring(0, 4);
                                    ntime = ntime.Insert(4, "1");
                                }
                                var dtempSerial = await this.GetEmployeeSl(int.Parse(setProxy), model.CompanyID);
                                string empSerial = dtempSerial.Rows.Count > 0 ? dtempSerial.Rows[0]["emp_serial"].ToString() : string.Empty;
                                string emp_no = dtempSerial.Rows.Count > 0 ? dtempSerial.Rows[0]["emp_no"].ToString() : string.Empty;
                                if (empSerial != string.Empty)
                                {
                                    var isSave = await _dgCommon.saveChangesAsync("dg_pay_Att_Insert_Textfile " + int.Parse(empSerial) + ",'" + setProxy + "',0," + model.CompanyID + ",'" + setDate + "'," + ntime + "", _sqlConnection);
                                    if (isSave)
                                    {
                                        response.Add(new
                                        {
                                            responseType = "Success",
                                            date = setDate,
                                            time = ntime,
                                            responseMessage = "Employee No('" + emp_no + "') Upload Successfully !!"
                                        });
                                    }
                                    else
                                    {
                                        response.Add(new
                                        {
                                            responseType = "Error",
                                            date = setDate,
                                            time = ntime,
                                            responseMessage = "Employee No('" + emp_no + "') Upload Faill !!"
                                        });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return response;
        }

        #region"Setup"
        private string MakeDateFormat(string dateString,string dateFormat)
        {
            string dateResult = string.Empty;
            try
            {
                DateTime customDate;
                //string[] formats = { "MMddyyyy", "yyyyMMdd","ddMMyyyy", "MMddyy","yyMMdd","ddMMyy" };
                //DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out customDate);
                DateTime.TryParseExact(dateString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out customDate);
                dateResult = Convert.ToDateTime(customDate).ToString("yyyy/MM/dd");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return dateResult;
        }
        private DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
        //New Action 12/04/2023
        private async Task<DataTable> GetPayAttnTextFileRead_Date(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select txt_redStart,txt_redLength from dg_pay_attdataImportsetup where adi_code='ATT_DATE' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnTextFileRead_Hour(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select txt_redStart,txt_redLength from dg_pay_attdataImportsetup where adi_code='HOURS' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnTextFileRead_Minute(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select txt_redStart,txt_redLength from dg_pay_attdataImportsetup where adi_code='MINUTES' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnTextFileRead_proxy(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select txt_redStart,txt_redLength from dg_pay_attdataImportsetup where adi_code='EMPID' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        //End New Action 12/04/2023
        private async Task<DataTable> GetPayAttnClock(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition,adi_noofposition from dg_pay_attdataImportsetup where adi_code='CLOCK' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnDay(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition,adi_noofposition from dg_pay_attdataImportsetup where adi_code='DAY' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnMonth(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition,adi_noofposition from dg_pay_attdataImportsetup where adi_code='MONTH' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnYear(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition,adi_noofposition from dg_pay_attdataImportsetup where adi_code='YEAR' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnHours(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition,adi_noofposition from dg_pay_attdataImportsetup where adi_code='HOURS' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnMinutes(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition,adi_noofposition from dg_pay_attdataImportsetup where adi_code='MINUTES' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnProxID(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition,adi_noofposition from dg_pay_attdataImportsetup where adi_code='EMPID' and adi_company=" + compID, _sqlConnection);
            return data;
        }

        //2nd
        private async Task<DataTable> GetPayAttnClock1(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition1,adi_noofposition1 from dg_pay_attdataImportsetup where adi_code='CLOCK' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnDay1(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition1,adi_noofposition1 from dg_pay_attdataImportsetup where adi_code='DAY' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnMonth1(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition1,adi_noofposition1 from dg_pay_attdataImportsetup where adi_code='MONTH' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnYear1(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition1,adi_noofposition1 from dg_pay_attdataImportsetup where adi_code='YEAR' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnHours1(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition1,adi_noofposition1 from dg_pay_attdataImportsetup where adi_code='HOURS' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnMinutes1(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition1,adi_noofposition1 from dg_pay_attdataImportsetup where adi_code='MINUTES' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetPayAttnProxID1(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select adi_stposition1,adi_noofposition1 from dg_pay_attdataImportsetup where adi_code='EMPID' and adi_company=" + compID, _sqlConnection);
            return data;
        }
        private async Task<DataTable> GetEmployeeSl(int Proxid,int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select emp_serial,emp_no from dg_pay_Employee where compid=" + compID + " and emp_proxid=" + Proxid, _sqlConnection);
            return data;
        }
        private async Task<bool> InsertDataAttn(string empSerial, string proxID, string nDate, string ntime)
        {
            bool flag = false;
            try
            {
                try
                {
                    SqlCommand command = new SqlCommand("dg_pay_Att_Insert_Textfile", _sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@at_emp_serial", empSerial);
                    command.Parameters.AddWithValue("@at_proxid", proxID);
                    command.Parameters.AddWithValue("@at_groupid", 0);
                    command.Parameters.AddWithValue("@at_compid", 0);
                    command.Parameters.AddWithValue("@at_date", nDate);
                    command.Parameters.AddWithValue("@at_intime", ntime);
                    await _sqlConnection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    flag = true;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    flag = false;
                }
            }
            finally
            {
                await _sqlConnection.CloseAsync();
            }
            return flag;
        }
        //public DateTime ParseRequestDate()
        //{
        //    // https://stackoverflow.com/questions/2883576/how-do-you-convert-epoch-time-in-c

        //    CultureInfo enUS = new CultureInfo("en-US");

        //    var dt = "1374755180";
        //    //var dt = "7/25/2013 6:37:31 PM";
        //    //var dt = "2013-07-25 14:26:00";

        //    DateTime dateValue;
        //    long dtLong;

        //    // Scenario #1
        //    if (long.TryParse(dt, out dtLong))
        //        return dtLong.FromUnixTime();

        //    // Scenario #2
        //    if (DateTime.TryParseExact(dt, "MM/dd/yyyy hh:mm:ss tt", enUS, DateTimeStyles.None, out dateValue))
        //        return dateValue;

        //    // Scenario #3
        //    if (DateTime.TryParseExact(dt, "yyyy-MM-dd hh:mm:ss", enUS, DateTimeStyles.None, out dateValue))
        //        return dateValue;
        //}
        #endregion
    }
}
