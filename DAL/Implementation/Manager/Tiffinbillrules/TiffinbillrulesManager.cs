using BLL.Interfaces.Manager.Tiffinbillrules;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.Tiffinbillrules;
using EF.Core.Repository.Manager;

namespace DAL.Implementation.Manager.Tiffinbillrules
{
    public class TiffinbillrulesManager : CommonManager<Tiffinbillrule_DbModel>,ITiffinbillrulesManager
    {
        public TiffinbillrulesManager(dg_hrpayrollContext context):base(new TiffinbillrulesRepository(context))
        {            
        }        
    }
}
