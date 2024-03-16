using BOL.Models;
using EF.Core.Repository.Interface.Manager;

namespace BLL.Interfaces.Manager.Designations
{
    public interface IDesignationsManager : ICommonManager<Designation_DbModel>
    {
        Task<ReturnObject> AddOrEditDesignation(DesignationPayload obj);
        Task<ReturnObject<Designation_DbModel>> GetAllDesignation();
    }
}
