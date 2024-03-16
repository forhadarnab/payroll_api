using BOL.Models;
using EF.Core.Repository.Interface.Manager;
using System.Data;

namespace BLL.Interfaces.Manager.AllProcess
{
    public interface IAllProcessManager : ICommonManager<AllProcess_DbModel>
    {
        Task<DataSet> GetDeshboard(int compid);
        Task<DataSet> GetAttendance_Process(DateTime SDate, int CompID);
        Task<DataSet> GetSalary_Process(int groupid, int compid, DateTime pDate);
        ReturnObject Salary_Process_Single(SingleSalaryProcess obj);
        Task<DataSet> GetSalary_Confarmations(int com_id, int month, int year);
        Task<DataSet> GetCreate_User(string name, string EmailId, string Password, int Designation,
            DateTime Getdate, int CompId, int Emp_ID, string Active_status, int Emp_serial, int Compliance);
        Task<DataTable> GetSearchUserlist();
        Task<string> SalaryPaymentDate(int compid, string pDate, string salMonth);
        ReturnObject SalaryProcessSave(int groupid, int compid, string pDate);
    }
}