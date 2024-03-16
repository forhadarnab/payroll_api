using BOL.Models;
using EF.Core.Repository.Interface.Manager;
using System.Data;

namespace BLL.Interfaces.Manager.CompanyAccess
{
    public interface ICompanyAccessManager : ICommonManager<CompanyAccess_DbModel>
    {
        Task<DataSet> User_List();
        Task<DataSet> User_Listfrom_ACCESS(string user);
    }
}
