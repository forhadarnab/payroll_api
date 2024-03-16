using BLL.Interfaces.Manager.Login;
using BLL.Utility;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.Login;
using EF.Core.Repository.Manager;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Implementation.Manager.Login
{
    public class loginManager : CommonManager<User_DbModel>,ILoginManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _dgParollCon;
        private readonly SqlConnection _dgSpecFo;
        public loginManager(dg_hrpayrollContext context, Dg_Common dgCommon) : base(new LoginRepository(context))
        {
            _dgCommon = dgCommon;
            _dgParollCon = new SqlConnection(Getway.Dg_Payroll);
            _dgSpecFo = new SqlConnection(Getway.SpecFoCon);
        }

        public async Task<string> SaveUserPasswordChange(int compID, string loginID, string confPassword, string newPassword)
        {
            string result = string.Empty;
            try
            {
                if (confPassword == newPassword)
                {
                    bool isSave = await _dgCommon.saveChangesAsync("update Tbl_User set [Password]='"+ newPassword + "' where CompId="+ compID + " and RTRIM(FullName)='" + loginID + "'", _dgParollCon);
                    if (isSave)
                    {
                        result = "Password Change Successfully !!";
                    }
                    else
                    {
                        result = "Password Not Change !!";
                    }
                }
                else
                {
                    result = "Password Mismatch !!";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result = "Something went wrong !!";
            }
            return result;
        }
        public async Task<List<object>> GetPermitedMenuList(List<MenuList> obj)
        {
            List<object> lstMenu = new List<object>();
            DataTable dt = await _dgCommon.get_InformationDataTableAsync("select Permission_Status from Smt_Users where cUserName='" + obj[0].userName + "'", _dgSpecFo);
            DataTable userGroup = await _dgCommon.get_InformationDataTableAsync("select nUgroup from Smt_Users where cUserName='" + obj[0].userName + "'", _dgSpecFo);
            if (userGroup.Rows[0]["nUgroup"].ToString() != "1")
            {
                if (dt.Rows.Count > 0)
                {
                    string x = dt.Rows[0]["Permission_status"].ToString();
                    if (x == "U")
                    {
                        for (int iac = 0; iac < obj.Count; iac++)
                        {
                            string frmName = obj[iac].menuText;
                            DataTable dtgtfrmU = await _dgCommon.get_InformationDataTableAsync("select Form_Name from Smt_UserPermittedform where User_ID='" + obj[0].userName + "' and Form_Name='" + frmName + "'", _dgSpecFo);
                            if (dtgtfrmU.Rows.Count < 1)
                            {
                                var LiID = new
                                {
                                    MenuText = obj[iac].menuText
                                };
                                lstMenu.Add(LiID);
                            }
                        }
                    }
                    else
                    {
                        for (int iac = 0; iac < obj.Count; iac++)
                        {
                            string frmName = obj[iac].menuText;
                            DataTable dtgtfrmU = await _dgCommon.get_InformationDataTableAsync("select Form_Name from Smt_UserPermittedform where nUgroup=" + userGroup.Rows[0]["nUgroup"].ToString() + " and Form_Name='" + frmName + "'", _dgSpecFo);
                            if (dtgtfrmU.Rows.Count < 1)
                            {
                                var LiID = new
                                {
                                    MenuText = obj[iac].menuText
                                };
                                lstMenu.Add(LiID);
                            }
                        }
                    }
                }
            }
            return lstMenu;
        }
    }
}
