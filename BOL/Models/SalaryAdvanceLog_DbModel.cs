using System.ComponentModel.DataAnnotations;

namespace BOL.Models
{
    public class SalaryAdvanceLog_DbModel
    {
        [Key]
        public int serial { get; set; }
        public int? sapl_compid { get; set; } = null;
        public DateTime? sapl_date { get; set; } = null;
        public int? sapl_days { get; set; } = null;
        public string sapl_user { get; set; } = null;
        public DateTime? sapl_udate { get; set; } = null;
    }
    public class DgPaySalaryAdvanceLog
    {
        public int Serial { get; set; }
        public int? SaplCompid { get; set; } = null;
        public DateTime? SaplDate { get; set; } = null;
        public int? SaplDays { get; set; } = null;
        public string SaplUser { get; set; } = null;
        public DateTime? SaplUdate { get; set; } = null;

        public static SalaryAdvanceLog_DbModel CustomToDbModel(DgPaySalaryAdvanceLog obj)
        {
            try
            {
                var dbModel = new SalaryAdvanceLog_DbModel
                {
                    serial = obj.Serial,
                    sapl_compid = obj.SaplCompid,
                    sapl_date = obj.SaplDate,
                    sapl_days = obj.SaplDays,
                    sapl_user = obj.SaplUser,
                    sapl_udate = obj.SaplUdate
                };
                return dbModel;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return null;
        }
        public static DgPaySalaryAdvanceLog DbToCustomModel(SalaryAdvanceLog_DbModel obj)
        {
            try
            {
                var customModel = new DgPaySalaryAdvanceLog
                {
                    Serial = obj.serial,
                    SaplCompid = obj.sapl_compid,
                    SaplDate = obj.sapl_date,
                    SaplDays = obj.sapl_days,
                    SaplUser = obj.sapl_user,
                    SaplUdate = obj.sapl_udate
                };
                return customModel;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return null;
        }
    }
}