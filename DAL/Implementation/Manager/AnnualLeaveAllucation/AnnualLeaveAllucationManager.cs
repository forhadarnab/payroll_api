using BLL.Interfaces.Manager.AnnualLeaveAllucation;
using BLL.Utility;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.AnnualLeaveAllucation;
using EF.Core.Repository.Manager;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Implementation.Manager.AnnualLeaveAllucation
{
    public class AnnualLeaveAllucationManager : CommonManager<AnnualLeaveAllucation_DbModel>, IAnnualLeaveAllucationManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _connection;
        public AnnualLeaveAllucationManager(dg_hrpayrollContext context, Dg_Common dgCommon) : base(new AnnualLeaveAllucationRepository(context))
        {
            _dgCommon = dgCommon;
            _connection = new SqlConnection(Getway.Dg_Payroll);
        }

        public async Task<DataSet> GetAnnualLeave_process(int year, int casual, int medical, int annul, int comID)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Dg_Pay_Annual_Leave_generate " + year + "," + casual + "," + medical + "," + annul + "," + comID + "", _connection);
            return data;
        }
    }
}
