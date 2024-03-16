using BLL.Interfaces.Manager.EidBonusSetups;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.EidBonusSetups;
using EF.Core.Repository.Manager;

namespace DAL.Implementation.Manager.EidBonusSetups
{
    public class EidBonusSetupsManager : CommonManager<EidBonusSetup_DbModel>, IEidBonusSetupsManager
    {
        public EidBonusSetupsManager(dg_hrpayrollContext context) : base(new EidBonusSetupsRepository(context))
        {           
        }        
    }
}
