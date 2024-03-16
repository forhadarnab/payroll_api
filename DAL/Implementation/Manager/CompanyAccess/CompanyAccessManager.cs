using BLL.Interfaces.Manager.CompanyAccess;
using BLL.Utility;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.CompanyAccess;
using EF.Core.Repository.Manager;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Implementation.Manager.CompanyAccess
{
    public class CompanyAccessManager : CommonManager<CompanyAccess_DbModel>,ICompanyAccessManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _connection;
        public CompanyAccessManager(dg_hrpayrollContext context, Dg_Common dgCommon) : base( new CompanyAccessRepository(context))
        {
            _dgCommon = dgCommon;
            _connection = new SqlConnection(Getway.Dg_Payroll);
        }

        public async Task<DataSet> User_List()
        {
            var data = await _dgCommon.get_InformationDtasetAsync("User_List", _connection);
            return data;
        }
        public async Task<DataSet> User_Listfrom_ACCESS(string user)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("User_List_from_access_table '"+ user + "'", _connection);
            return data;
        }
    }
}
