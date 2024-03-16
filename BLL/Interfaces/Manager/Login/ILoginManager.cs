using BOL.Models;
using EF.Core.Repository.Interface.Manager;

namespace BLL.Interfaces.Manager.Login
{
    public interface ILoginManager : ICommonManager<User_DbModel>
    {
        Task<string> SaveUserPasswordChange(int compID,string loginID,string confPassword, string newPassword);
        Task<List<object>> GetPermitedMenuList(List<MenuList> obj);
    }
}
