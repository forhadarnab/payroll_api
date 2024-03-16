using BLL.Interfaces.Manager.AllProcess;
using BLL.Utility;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.AllProcess;
using EF.Core.Repository.Manager;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Implementation.Manager.AllProcess
{
    public class AllProcessManager : CommonManager<AllProcess_DbModel>, IAllProcessManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _sqlConnection;
        public AllProcessManager(dg_hrpayrollContext context, Dg_Common dgCommon) : base(new AllProcessRepository(context))
        {
            _dgCommon = dgCommon;
            _sqlConnection = new SqlConnection(Getway.Dg_Payroll);
        }

        public async Task<DataSet> GetDeshboard(int compid)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Deshboard_employee " + compid, _sqlConnection);
            return data;
        }
        public async Task<DataSet> GetAttendance_Process(DateTime SDate, int CompID)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Attandance_Process '" + SDate + "'," + CompID, _sqlConnection);
            return data;
        }
        public async Task<DataSet> GetSalary_Process(int groupid, int compid, DateTime pDate)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Dg_Pay_Sal_SalaryProcess_Full " + groupid + "," + compid + ",'" + pDate + "'", _sqlConnection);
            return data;
        }
        public ReturnObject SalaryProcessSave(int groupid, int compid, string pDate)
        {
            var result = new ReturnObject();
            try
            {
                var dtIsExists = _dgCommon.get_InformationDataTable("SELECT ss_compid FROM dg_pay_salarysheet WHERE ss_confirmed=1 AND ss_compid="+ compid + " AND MONTH(ss_date)=MONTH('"+ Convert.ToDateTime(pDate) + "') AND YEAR(ss_date)=YEAR('"+ Convert.ToDateTime(pDate) + "')", _sqlConnection);
                bool isExists = dtIsExists.Rows.Count > 0 ? true : false;
                if (!isExists)
                {
                    bool isProcess = _dgCommon.saveChanges("Dg_Pay_Sal_SalaryProcess_Full " + groupid + "," + compid + ",'" + pDate + "'", _sqlConnection);
                    if (isProcess)
                    {
                        result.IsSuccess = true;
                        result.Message = "Salary Process Successfull This Month(" + Convert.ToDateTime(pDate).ToString("MMMM-yyyy") + ") !!";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Salary Process Failed This Month(" + Convert.ToDateTime(pDate).ToString("MMMM-yyyy") + ") !!";
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Salary Already Confirm This Month("+Convert.ToDateTime(pDate).ToString("MMMM-yyyy") +") !!";
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                result.IsSuccess = false;
                result.Message = "Something Went Wrong !!";
            }
            return result;
        }
        public ReturnObject Salary_Process_Single(SingleSalaryProcess obj)
        {
            var result = new ReturnObject();
            try
            {
                foreach (var item in obj.emp_serial)
                {
                    var dtIsExists = _dgCommon.get_InformationDataTable("SELECT ss_compid FROM dg_pay_salarysheet WHERE ss_confirmed=1 AND ss_compid=" + obj.compid + " AND MONTH(ss_date)=MONTH('" + Convert.ToDateTime(obj.pDate) + "') AND YEAR(ss_date)=YEAR('" + Convert.ToDateTime(obj.pDate) + "')", _sqlConnection);
                    bool isExists = dtIsExists.Rows.Count > 0 ? true : false;
                    if (!isExists)
                    {
                        bool isProcess = _dgCommon.saveChanges("dg_Pay_Sal_SalaryProcess " + item + "," + obj.groupid + "," + obj.compid + ",'" + obj.pDate + "'", _sqlConnection);
                        if (isProcess)
                        {
                            result.IsSuccess = true;
                            result.Message = "Process Successfully !!";
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "Process Failed !!";
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Salary Already Confirm This Month(" + Convert.ToDateTime(obj.pDate).ToString("MMMM-yyyy") + ") !!";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result.IsSuccess = false;
                result.Message = "Something Went Wrong !!";
            }
            return result;
        }
        public async Task<DataSet> GetSalary_Confarmations(int com_id, int month, int year)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("dg_Pay_Salary_Confirmation_process " + com_id + "," + month + "," + year, _sqlConnection);
            return data;
        }
        public async Task<DataSet> GetCreate_User(string name, string EmailId, string Password, int Designation, DateTime Getdate, int CompId, int Emp_ID, string Active_status, int Emp_serial, int Compliance)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Create_User '" + name + "','" + EmailId + "','" + Password + "'," + Designation + ",'" + Getdate + "'," + CompId + "," + Emp_ID + ",'" + Active_status + "'," + Emp_serial + "," + Compliance + "", _sqlConnection);
            return data;
        }
        public async Task<DataTable> GetSearchUserlist()
        {
            var data = await _dgCommon.get_InformationDataTableAsync("UserList", _sqlConnection);
            return data;
        }
        public async Task<string> SalaryPaymentDate(int compid, string pDate,string salMonth)
        {
            string message = string.Empty;
            var data = await _dgCommon.get_InformationDataTableAsync("select ss_emp_serial from dg_pay_salarysheet where ss_compid=" + compid + " and Month(ss_date)=Month('"+ salMonth + "') and Year(ss_date)=year('"+ salMonth + "') and ss_confirmed=1", _sqlConnection);
            if(data.Rows.Count > 0)
            {
                message = "Salaries already confirmed this month("+Convert.ToDateTime(salMonth).ToString("MMMM-yyyy") +") ";
            }
            else
            {
                await _dgCommon.saveChangesAsync("dg_Pay_Salary_payment_date_process " + compid + ",'" + pDate + "','"+ salMonth + "'", _sqlConnection);
                message = "Update Successfully !!";
            }
            return message;
        }
    }
}