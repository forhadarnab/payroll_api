using BLL.Interfaces.Manager.Employees;
using BLL.Utility;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.Employees;
using EF.Core.Repository.Manager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Reporting.NETCore;
using QRCoder;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;

namespace DAL.Implementation.Manager.Employees
{
    public class EmployeesManager : CommonManager<Employee_DbModel>, IEmployeesManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeesManager(dg_hrpayrollContext context, Dg_Common dgCommon, IWebHostEnvironment webHostEnvironment) : base(new EmployeesRepository(context))
        {
            _dgCommon = dgCommon;
            _connection = new SqlConnection(Getway.Dg_Payroll);
            _webHostEnvironment = webHostEnvironment;
        }

        public void QR_Generator(string[] QrInfo, string[] FilePath)
        {
            string filepath = FilePath[0];
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            string fullPath = string.Concat(FilePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            string InfoFull = string.Concat(QrInfo);
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(InfoFull, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            var bmp = QrCode.GetGraphic(60);
            bmp.Save(fullPath, ImageFormat.Jpeg);
        }
        public async Task<DataSet> Employee_active_status(int Emp_serial, int Active_status, string active_Date, string Inactive_Date, string in_active_Resion, string userName)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Active_status " + Emp_serial + "," + Active_status + ",'" + active_Date + "','" + Inactive_Date + "','" + in_active_Resion + "','" + userName + "'", _connection);
            return data;
        }
        public async Task<ReturnObject> GetActiveInactiveHistory(int empSerial)
        {
            var result = new ReturnObject();
            try
            {
                var data = await _dgCommon.get_InformationDataTableAsync("Dg_active_inactive_history " + empSerial, _connection);
                if (data.Rows.Count > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "Data Loaded !!";
                    result.dataTable = data;
                }
                else
                {
                    result.Message = "Data Not Loaded !!";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result.Message = "Something Went Wrong !!";
            }
            return result;
        }
        public async Task<DataSet> textfile(int nempSerial, int Emp_ID, int ngroupid, int ncompid, DateTime nDate, string ntime)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("dg_pay_Att_Insert_Textfile " + nempSerial + "," + Emp_ID + "," + ngroupid + "," + ncompid + ",'" + nDate + "','" + ntime + "'", _connection);
            return data;
        }
        //public async Task<bool> Save_EmpReportParameter(ReportParameterModel obj)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        await this.DeleteOldReportFilterData("dg_print_employeelist");
        //        await _connection.OpenAsync();
        //        foreach (var item in obj.Emp_No)
        //        {
        //            SqlCommand cmd = new SqlCommand("Emp_filtering", _connection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Compid", obj.Compid);
        //            cmd.Parameters.AddWithValue("@Department", obj.Department);
        //            cmd.Parameters.AddWithValue("@section", obj.section);
        //            cmd.Parameters.AddWithValue("@Building", obj.Building);
        //            cmd.Parameters.AddWithValue("@Floor", obj.Floor);
        //            cmd.Parameters.AddWithValue("@Line", obj.Line);
        //            cmd.Parameters.AddWithValue("@Shift", obj.Shift);
        //            cmd.Parameters.AddWithValue("@Grade", obj.Grade);
        //            cmd.Parameters.AddWithValue("@salcat", obj.salcat);
        //            cmd.Parameters.AddWithValue("@Start_date", obj.Start_date);
        //            cmd.Parameters.AddWithValue("@End_date", obj.End_date);
        //            cmd.Parameters.AddWithValue("@User", obj.User);
        //            cmd.Parameters.AddWithValue("@EmpNo", item);
        //            await cmd.ExecuteNonQueryAsync();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        flag = false;
        //    }
        //    finally
        //    {
        //        await _connection.CloseAsync();
        //    }
        //    return flag;
        //}       
        //public async Task<bool> Save_LevReportParameter(ReportParameterModel obj)
        //{
        //    bool save = false;
        //    try
        //    {
        //        await this.DeleteOldReportFilterData("dg_print_employeelist_leave");
        //        await _connection.OpenAsync();
        //        foreach (var item in obj.Emp_No)
        //        {
        //            SqlCommand cmd = new SqlCommand("Emp_filtering_Leave", _connection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Compid", obj.Compid);
        //            cmd.Parameters.AddWithValue("@Department", obj.Department);
        //            cmd.Parameters.AddWithValue("@section", obj.section);
        //            cmd.Parameters.AddWithValue("@Building", obj.Building);
        //            cmd.Parameters.AddWithValue("@Floor", obj.Floor);
        //            cmd.Parameters.AddWithValue("@Line", obj.Line);
        //            cmd.Parameters.AddWithValue("@Shift", obj.Shift);
        //            cmd.Parameters.AddWithValue("@Grade", obj.Grade);
        //            cmd.Parameters.AddWithValue("@salcat", obj.salcat);
        //            cmd.Parameters.AddWithValue("@Start_date", obj.Start_date);
        //            cmd.Parameters.AddWithValue("@End_date", obj.End_date);
        //            cmd.Parameters.AddWithValue("@User", obj.User);
        //            cmd.Parameters.AddWithValue("@EmpNo", item);
        //            await cmd.ExecuteNonQueryAsync();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        save = false;
        //    }
        //    finally
        //    {
        //        await _connection.CloseAsync();
        //    }
        //    return save;
        //}
        //public async Task<bool> Save_AttReportParameter(ReportParameterModel obj)
        //{
        //    bool save = false;
        //    try
        //    {
        //        await this.DeleteOldReportFilterData("dg_print_employeelist_atttendance");
        //        await _connection.OpenAsync();
        //        foreach (var item in obj.Emp_No)
        //        {
        //            SqlCommand cmd = new SqlCommand("Emp_filtering_Attendance", _connection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Compid", obj.Compid);
        //            cmd.Parameters.AddWithValue("@Department", obj.Department);
        //            cmd.Parameters.AddWithValue("@section", obj.section);
        //            cmd.Parameters.AddWithValue("@Building", obj.Building);
        //            cmd.Parameters.AddWithValue("@Floor", obj.Floor);
        //            cmd.Parameters.AddWithValue("@Line", obj.Line);
        //            cmd.Parameters.AddWithValue("@Shift", obj.Shift);
        //            cmd.Parameters.AddWithValue("@Grade", obj.Grade);
        //            cmd.Parameters.AddWithValue("@salcat", obj.salcat);
        //            cmd.Parameters.AddWithValue("@Start_date", obj.Start_date);
        //            cmd.Parameters.AddWithValue("@End_date", obj.End_date);
        //            cmd.Parameters.AddWithValue("@User", obj.User);
        //            cmd.Parameters.AddWithValue("@EmpNo", item);
        //            await cmd.ExecuteNonQueryAsync();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        save = false;
        //    }
        //    finally
        //    {
        //        await _connection.CloseAsync();
        //    }
        //    return save;
        //}
        //public async Task<bool> Save_SalReportParameter(ReportParameterModel obj)
        //{
        //    bool save = false;
        //    try
        //    {
        //        await this.DeleteOldReportFilterData("dg_print_employeelist_salary");
        //        await _connection.OpenAsync();
        //        foreach (var item in obj.Emp_No)
        //        {
        //            SqlCommand cmd = new SqlCommand("Emp_filtering_salary", _connection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Compid", obj.Compid);
        //            cmd.Parameters.AddWithValue("@Department", obj.Department);
        //            cmd.Parameters.AddWithValue("@section", obj.section);
        //            cmd.Parameters.AddWithValue("@Building", obj.Building);
        //            cmd.Parameters.AddWithValue("@Floor", obj.Floor);
        //            cmd.Parameters.AddWithValue("@Line", obj.Line);
        //            cmd.Parameters.AddWithValue("@Shift", obj.Shift);
        //            cmd.Parameters.AddWithValue("@Grade", obj.Grade);
        //            cmd.Parameters.AddWithValue("@salcat", obj.salcat);
        //            cmd.Parameters.AddWithValue("@Start_date", obj.Start_date);
        //            cmd.Parameters.AddWithValue("@End_date", obj.End_date);
        //            cmd.Parameters.AddWithValue("@oi_bank", obj.oi_bank);
        //            cmd.Parameters.AddWithValue("@Inactive_reson", obj.Inactive_reson);
        //            cmd.Parameters.AddWithValue("@User", obj.User);
        //            cmd.Parameters.AddWithValue("@EmpNo", item);
        //            await cmd.ExecuteNonQueryAsync();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        save = false;
        //    }
        //    finally
        //    {
        //        await _connection.CloseAsync();
        //    }
        //    return save;
        //}
        public async Task<DataSet> employee_ShiftChange(int? Compid = null, int? Department = null, int? section = null, int?
            Building = null, int? Floor = null, int? Line = null, int? Shift = null, int? Grade = null, int?
            salcat = null, int? Newshift = null, DateTime? EffectDate = null, string user = null)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Emp_filtering_shiftchange " + Compid + "," + Department + "," + section + "," + Building + "," + Floor + "," + Line + "," + Shift + "," + Grade + "," + salcat + "," + Newshift + ",'" + EffectDate + "','" + user + "'", _connection);
            return data;
        }
        public async Task<DataSet> get()
        {
            var data = await _dgCommon.get_InformationDtasetAsync("textfile_datetime_position", _connection);
            return data;
        }
        public async Task<DataSet> employee_Info(int emp_serial)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Emp_filtering_individual " + emp_serial, _connection);
            return data;
        }
        public async Task<DataSet> employee_Info()
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Id_card_bangla", _connection);
            return data;
        }
        public async Task<DataSet> GetCompanyName()
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_company", _connection);
            return data;
        }
        public async Task<DataSet> GetdivisionName()
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Division", _connection);
            return data;
        }
        public async Task<DataSet> GetDistict(int di_id)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_district " + di_id, _connection);
            return data;
        }
        public async Task<DataSet> GetThana(int th_id)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Thana " + th_id, _connection);
            return data;
        }
        public async Task<DataSet> Getpostoffice(int ThanaID)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Postoffice " + ThanaID, _connection);
            return data;
        }
        public async Task<DataSet> Getvillage(int thanaID)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_village " + thanaID, _connection);
            return data;
        }
        public async Task<DataSet> Companywiseemployee(int compid)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_employee_companywise " + compid, _connection);
            return data;
        }
        public async Task<DataSet> Employee_ID(int compid)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_employee_id_companywise " + compid, _connection);
            return data;
        }
        public async Task<DataSet> singleEmployee(int comId, int EmpId)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_employee_companywise_and_Idwise " + comId + "," + EmpId + "", _connection);
            return data;
        }
        public async Task<DataSet> formeternity(int compid)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_employee_id_companywise_formeternity " + compid, _connection);
            return data;
        }
        public async Task<DataSet> Proxmity_ID(int compid)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_employee_proxid_companywise " + compid, _connection);
            return data;
        }
        public async Task<DataSet> GetDesignation()
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Designation", _connection);
            return data;
        }
        public async Task<DataSet> Department(int compid)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Departmennt " + compid, _connection);
            return data;
        }
        public async Task<DataSet> GetSection(int Department)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Section " + Department, _connection);
            return data;
        }
        public async Task<DataSet> Getbuilding(int compid)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Building " + compid, _connection);
            return data;
        }
        public async Task<DataSet> GetFloor(int compid, int Building)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Floor " + compid + "," + Building + "", _connection);
            return data;
        }
        public async Task<DataSet> GetLine(int compid, int Building, int Floor)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Line " + compid + "," + Building + "," + Floor + "", _connection);
            return data;
        }
        public async Task<DataSet> Getsalarycategory(int compID)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_salary_category " + compID, _connection);
            return data;
        }
        public async Task<DataSet> GetShift(int compid)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Shift " + compid, _connection);
            return data;
        }
        public async Task<DataSet> GetGrade()
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Grade", _connection);
            return data;
        }
        public async Task<DataSet> GetBank()
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Bank", _connection);
            return data;
        }
        public async Task<DataTable> GetCompanyAddress(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select com_name,com_ad1 from dg_pay_company where com_id=" + compID, _connection);
            return data;
        }


        private async Task<bool> DeleteOldReportFilterData(string TableName)
        {
            bool flag = false;
            try
            {
                await _connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("truncate table " + TableName + "", _connection);
                cmd.CommandType = CommandType.Text;
                await cmd.ExecuteNonQueryAsync();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                flag = false;
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return flag;
        }
        public async Task<bool> EmpLeaveGenerate(DgPayEmployee obj)
        {
            bool flag = false;
            try
            {
                SqlCommand cmd = new SqlCommand("Auto_casual_medical_leavegenarate", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@lev_compid", obj.Compid);
                cmd.Parameters.AddWithValue("@emp_no", obj.EmpNo);
                await _connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return flag;
        }

        //New Version
        public async Task<bool> UploadEmpInfoImage(IFormFile fileEmp, IFormFile fileNominee, IFormFile fileSign, string compid, int empid)
        {
            bool flag = true;
            try
            {
                if (fileEmp != null)
                {
                    string filepathEmp = $"{_webHostEnvironment.WebRootPath}\\EmployeeImage\\" + compid + "\\";
                    if (!Directory.Exists(filepathEmp))
                    {
                        Directory.CreateDirectory(filepathEmp);
                    }
                    string[] fileArr = new string[] { filepathEmp, null, null };
                    fileArr[1] = empid.ToString();
                    fileArr[2] = ".jpg";
                    string fileFull = string.Concat(fileArr);
                    if (System.IO.File.Exists(fileFull))
                    {
                        System.IO.File.Delete(fileFull);
                    }
                    using (var stream = new FileStream(fileFull, FileMode.Create))
                    {
                        await fileEmp.CopyToAsync(stream);
                        flag = true;
                    }
                    //using var image = Image.Load(fileEmp.OpenReadStream());
                    //image.Mutate(x => x.Resize(300, 300));
                    //await image.SaveAsync(fileFull);
                }
                if (fileNominee != null)
                {
                    string filepathNominee = $"{_webHostEnvironment.WebRootPath}\\NomineeImage\\" + compid + "\\";
                    if (!Directory.Exists(filepathNominee))
                    {
                        Directory.CreateDirectory(filepathNominee);
                    }
                    string[] fileArr = new string[] { filepathNominee, null, null };
                    fileArr[1] = empid.ToString();
                    fileArr[2] = ".jpg";
                    string fileFull = string.Concat(fileArr);
                    if (System.IO.File.Exists(fileFull))
                    {
                        System.IO.File.Delete(fileFull);
                    }
                    using (var stream = new FileStream(fileFull, FileMode.Create))
                    {
                        await fileNominee.CopyToAsync(stream);
                        flag = true;
                    }
                }
                if (fileSign != null)
                {
                    string filepathSign = $"{_webHostEnvironment.WebRootPath}\\EmployeeSignature\\" + compid + "\\";
                    if (!Directory.Exists(filepathSign))
                    {
                        Directory.CreateDirectory(filepathSign);
                    }
                    string[] fileArr = new string[] { filepathSign, null, null };
                    fileArr[1] = empid.ToString();
                    fileArr[2] = ".png";
                    string fileFull = string.Concat(fileArr);
                    if (System.IO.File.Exists(fileFull))
                    {
                        System.IO.File.Delete(fileFull);
                    }
                    using (var stream = new FileStream(fileFull, FileMode.Create))
                    {
                        await fileSign.CopyToAsync(stream);
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                flag = false;
            }
            return flag;
        }
        public async Task<string> AddOrEditEmpPersonalInfo(Employee_Personal_Info obj)
        {
            string result = string.Empty;
            if (obj.emp_serial == 0)
            {
                bool isExists = this.EmployeeNoExists(int.Parse(obj.compid.ToString()), int.Parse(obj.emp_no.ToString()));
                if (!isExists)
                {
                    bool isSave = await _dgCommon.saveChangesAsync("dg_Pay_InsertOrUpdate_EmpPersonalInfo", _connection, obj);
                    if (isSave)
                    {
                        return result = "Save Successfully !!";
                    }
                    else
                    {
                        return result = "Something is wrong,Data not save !!";
                    }
                }
                else
                {
                    return result = "Employee numner(" + obj.emp_no + ") already exists !!";
                }
            }
            else
            {
                bool isSave = await _dgCommon.saveChangesAsync("dg_Pay_InsertOrUpdate_EmpPersonalInfo", _connection, obj);
                if (isSave)
                {
                    return result = "Save Successfully !!";
                }
                else
                {
                    return result = "Something is wrong,Data not save !!";
                }
            }
        }
        public async Task<Employee_Personal_Info_View> GetEmpPersonalInfo_ByEmpNo(int compid, int emp_no)
        {
            var dataTable = await _dgCommon.get_InformationDataTableAsync("dg_Pay_GetEmpPersonalInfo_ByEmpNo " + compid + "," + emp_no, _connection);
            var data = _dgCommon.GetSingleListObject<Employee_Personal_Info_View>(dataTable);
            return data;
        }
        public async Task<string> UpdateEmpOfficialInfo(Employee_Office_Info obj)
        {
            string result = string.Empty;
            bool isSave = await _dgCommon.saveChangesAsync("dg_Pay_Update_EmpOfficialInfo", _connection, obj);
            if (isSave)
            {
                return result = "Save Successfully !!";
            }
            return result = "Something is wrong,Data not save !!";
        }
        public async Task<Employee_Office_Info_View> GetEmpOfficialInfo_ByEmpSerial(int emp_serial)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("dg_Pay_EmpOfficialInfo_ByEmpSerial " + emp_serial, _connection);
            var newData = _dgCommon.GetSingleListObject<Employee_Office_Info_View>(data);
            return newData;
        }
        public async Task<bool> SaveEmployeeAttenData(int emp_serial)
        {
            bool flag = await _dgCommon.saveChangesAsync("dg_pay_InsertMenualAttRow " + emp_serial, _connection);
            return flag;
        }
        public async Task<string> UpdateEmpNomineeInfo(Employee_Nominee_Info obj)
        {
            string result = string.Empty;
            bool isSave = await _dgCommon.saveChangesAsync("dg_Pay_Update_EmpNomineeInfo", _connection, obj);
            if (isSave)
            {
                return result = "Save Successfully !!";
            }
            return result = "Something is wrong,Data not save !!";
        }
        public async Task<Employee_Nominee_Info_View> GetEmpNomineeInfo_ByEmpSerial(int emp_serial)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("dg_Pay_EmpNomineeInfo_ByEmpSerial " + emp_serial, _connection);
            var newData = _dgCommon.GetSingleListObject<Employee_Nominee_Info_View>(data);
            return newData;
        }
        public async Task<string> UpdateEmpEducationInfo(Employee_Education_Info obj)
        {
            string result = string.Empty;
            bool isSave = await _dgCommon.saveChangesAsync("dg_Pay_InsertOrUpdate_EmpEducationInfo", _connection, obj);
            if (isSave)
            {
                return result = "Save Successfully !!";
            }
            return result = "Something is wrong,Data not save !!";
        }
        public async Task<Employee_Education_Info_View> GetEmpEducationInfo_ByEmpNo(int compid, int emp_no)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("dg_Pay_EducationInfo_ByEmpNo " + compid + "," + emp_no, _connection);
            var newData = _dgCommon.GetSingleListObject<Employee_Education_Info_View>(data);
            return newData;
        }
        public async Task<string> addOrEditEmpExperince_Info(Employee_Experince_Info obj)
        {
            string result = string.Empty;
            bool isSave = await _dgCommon.saveChangesAsync("dg_Pay_InsertOrUpdate_ExperinceInfo", _connection, obj);
            if (isSave)
            {
                return result = "Save Successfully !!";
            }
            return result = "Something is wrong,Data not save !!";
        }
        public async Task<Employee_Experince_Info_View> GetEmpExperinceInfo_ByEmpNo(int compid, int emp_no)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("dg_Pay_ExperinceInfo_ByEmpNo " + compid + "," + emp_no, _connection);
            var newData = _dgCommon.GetSingleListObject<Employee_Experince_Info_View>(data);
            return newData;
        }
        public async Task<Employee_FullName_View> GetEmployeeFullName(int compid, int emp_no)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select pi_fullname from dg_pay_Employee where compid=" + compid + " and emp_no=" + emp_no, _connection);
            var newData = _dgCommon.GetSingleListObject<Employee_FullName_View>(data);
            return newData;
        }
        public async Task<DataTable> GetAllDegree_Title()
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select * from dg_exam_and_degree_title order by degree_title", _connection);
            return data;
        }
        public async Task<DataTable> GetAllExam_Board()
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select * from dg_exam_board order by eb_id desc", _connection);
            return data;
        }
        public async Task<DataTable> GetEmployeeNo(int compid)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select emp_serial,emp_no from dg_pay_Employee where oi_active=1 and compid=" + compid, _connection);
            return data;
        }
        public async Task<ReturnObject> GetCompanyWiseEmployee_ForPMS(int companyID)
        {
            var result = new ReturnObject();
            try
            {
                var dtEmpInfo = await _dgCommon.get_InformationDataTableAsync("employee_Info_company_wise " + companyID, _connection);
                if (dtEmpInfo.Rows.Count > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "Employee Information Loaded !!";
                    result.dataTable = dtEmpInfo;
                }
                else
                {
                    result.Message = "Employee Information Not Loaded !!";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result.Message = "Something Went Wrong !!";
            }
            return result;
        }
        private bool EmployeeNoExists(int compid, int emp_no)
        {
            var checkData = _dgCommon.get_InformationDataTable("SELECT emp_no FROM dg_pay_Employee WHERE compid=" + compid + " AND emp_no=" + emp_no, _connection);
            if (checkData.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public byte[] Dg_EmployeeSingleDetailedInformation(int emp_serial, string userName, string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_emp_CV_Single " + emp_serial, _connection);
            string dataset = "rpt_emp_CV";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_EmployeeInfo.rdlc";
            string imgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeImage\\").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("EmpImagePath",imgPath),
                new ReportParameter("PrintUser",userName)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
    }
}