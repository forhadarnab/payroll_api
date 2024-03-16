using BLL.Interfaces.Manager.SalaryAdvanceLogs;
using BLL.Utility;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.SalaryAdvanceLogs;
using EF.Core.Repository.Manager;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Implementation.Manager.SalaryAdvanceLogs
{
    public class SalaryAdvanceLogsManager : CommonManager<SalaryAdvanceLog_DbModel>,ISalaryAdvanceLogsManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _connection;
        public SalaryAdvanceLogsManager(dg_hrpayrollContext context, Dg_Common dgCommon) :base(new SalaryAdvanceLogsRepository(context))
        {
            _dgCommon = dgCommon;
            _connection = new SqlConnection(Getway.Dg_Payroll);
        }

        public async Task<DataSet> AdvanceProcess(int SAMonth, int SAYear, int sp_groupid, int sp_compid, int days)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("dg_Pay_Sal_ProcessSalaryAdvance "+ SAMonth + ","+ SAYear + ","+ sp_groupid + ","+ sp_compid + ","+ days + "", _connection);
            return data;
        }
        public async Task<DataSet> AdvanceProcess(int CompId, int month, int year)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("dg_SalaryAdvance_Search "+ CompId + ","+ month + ","+ year + "", _connection);
            return data;
        }
        public async Task<DataSet> AdvanceProcessSum(int CompId, int month, int year)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("dg_SalaryAdvanceSum_Search "+ CompId + ","+ month + ","+ year + "", _connection);
            return data;
        }        
    }
}
