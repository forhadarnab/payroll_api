using System.ComponentModel.DataAnnotations;

namespace BOL.Models
{
    public class Shift_DbModel
    {
        [Key]
        public int sh_code { get; set; }
        public string sh_name { get; set; } = null;
        public int? sh_comp { get; set; } = null;
        public string sh_compName { get; set; } = null;
        public int? sh_groupid { get; set; } = null;
        public decimal? sh_InTime { get; set; } = null;
        public decimal? sh_OutTime { get; set; } = null;
        public decimal? sh_Lateafter { get; set; } = null;
        public decimal? sh_ComplianceOut { get; set; } = null;
        public decimal? sh_OTCountTime { get; set; } = null;
        public bool? sh_SameDayOut { get; set; } = null;
        public string sh_user { get; set; } = null;
        public DateTime? sh_udate { get; set; } = null;
        public decimal? intime_start { get; set; } = null;
        public decimal? intime_stop { get; set; } = null;
        public decimal? outtime_start { get; set; } = null;
        public decimal? outtime_stop { get; set; } = null;
        public decimal? lunch_start_time { get; set; } = null;
        public decimal? lunch_stope_time { get; set; } = null;
        public decimal? lunch_hours { get; set; } = null;
        public decimal? Up_to_time_staff { get; set; } = null;
        public decimal? Up_to_time_sworker { get; set; } = null;
        public decimal? Amount_per_day_worker { get; set; } = null;
        public decimal? Amount_per_day_staff { get; set; } = null;
        public string Sh_statas { get; set; } = null;
    }
    public class DgPayShift
    {
        public int ShCode { get; set; }
        public string ShName { get; set; } = null;
        public int? ShComp { get; set; } = null;
        public string ShCompName { get; set; } = null;
        public int? ShGroupid { get; set; } = null;
        public decimal? ShInTime { get; set; } = null;
        public decimal? ShOutTime { get; set; } = null;
        public decimal? ShLateafter { get; set; } = null;
        public decimal? ShComplianceOut { get; set; } = null;
        public decimal? ShOtcountTime { get; set; } = null;
        public bool? ShSameDayOut { get; set; } = null;
        public string ShUser { get; set; } = null;
        public DateTime? ShUdate { get; set; } = null;
        public decimal? IntimeStart { get; set; } = null;
        public decimal? IntimeStop { get; set; } = null;
        public decimal? OuttimeStart { get; set; } = null;
        public decimal? OuttimeStop { get; set; } = null;
        public decimal? lunchStartTime { get; set; } = null;
        public decimal? lunchStopeTime { get; set; } = null;
        public decimal? LunchHours { get; set; } = null;
        public decimal? UpToTimeStaff { get; set; } = null;
        public decimal? UpToTimeWorker { get; set; } = null;
        public decimal? AmountPerDayWorker { get; set; } = null;
        public decimal? AmountPerDayStaff { get; set; } = null;
        public string ShStatas { get; set; } = null;

        public static Shift_DbModel CustomToDbModel(DgPayShift obj)
        {
            try
            {
                var dbModel = new Shift_DbModel
                {
                    sh_code = obj.ShCode,
                    sh_name = obj.ShName,
                    sh_comp = obj.ShComp,
                    sh_compName = obj.ShCompName,
                    sh_groupid = obj.ShGroupid,
                    sh_InTime = obj.ShInTime,
                    sh_OutTime = obj.ShOutTime,
                    sh_Lateafter = obj.ShLateafter,
                    sh_ComplianceOut = obj.ShComplianceOut,
                    sh_OTCountTime = obj.ShOtcountTime,
                    sh_SameDayOut = obj.ShSameDayOut,
                    sh_user = obj.ShUser,
                    sh_udate = obj.ShUdate,
                    intime_start = obj.IntimeStart,
                    intime_stop = obj.IntimeStop,
                    outtime_start = obj.OuttimeStart,
                    outtime_stop = obj.OuttimeStop,
                    lunch_start_time = obj.lunchStartTime,
                    lunch_stope_time = obj.lunchStopeTime,
                    lunch_hours = obj.LunchHours,
                    Up_to_time_staff = obj.UpToTimeStaff,
                    Up_to_time_sworker = obj.UpToTimeWorker,
                    Amount_per_day_worker = obj.AmountPerDayWorker,
                    Amount_per_day_staff = obj.AmountPerDayStaff,
                    Sh_statas = obj.ShStatas
                };
                return dbModel;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return null;
        }
        public static DgPayShift DbToCustomModel(Shift_DbModel obj)
        {
            try
            {
                var customModel = new DgPayShift
                {
                    ShCode = obj.sh_code,
                    ShName = obj.sh_name,
                    ShComp = obj.sh_comp,
                    ShCompName = obj.sh_compName,
                    ShGroupid = obj.sh_groupid,
                    ShInTime = obj.sh_InTime,
                    ShOutTime = obj.sh_OutTime,
                    ShLateafter = obj.sh_Lateafter,
                    ShComplianceOut = obj.sh_ComplianceOut,
                    ShOtcountTime = obj.sh_OTCountTime,
                    ShSameDayOut = obj.sh_SameDayOut,
                    ShUser = obj.sh_user,
                    ShUdate = obj.sh_udate,
                    IntimeStart = obj.intime_start,
                    IntimeStop = obj.intime_stop,
                    OuttimeStart = obj.outtime_start,
                    OuttimeStop = obj.outtime_stop,
                    lunchStartTime = obj.lunch_start_time,
                    lunchStopeTime = obj.lunch_stope_time,
                    LunchHours = obj.lunch_hours,
                    UpToTimeStaff = obj.Up_to_time_staff,
                    UpToTimeWorker = obj.Up_to_time_sworker,
                    AmountPerDayWorker = obj.Amount_per_day_worker,
                    AmountPerDayStaff = obj.Amount_per_day_staff,
                    ShStatas = obj.Sh_statas
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
    public class ShiftRostaring
    {
        public int sc_empserial { get; set; }
        public DateTime sc_start_date { get; set; }
        public DateTime sc_end_date { get; set; }
        public int sc_old_shift { get; set; }
        public int sc_new_shift { get; set; }
        public string sc_user { get; set; }
    }
    public class ShiftRostaring2
    {
        public int sc_empserial { get; set; }
        public DateTime sc_start_date { get; set; }
        public DateTime sc_end_date { get; set; }
        public string sc_transfer_type { get; set; }
        public int sc_old_shift { get; set; }
        public int sc_new_shift { get; set; }
        public string sc_user { get; set; }
        public DateTime sc_addate { get; set; }
    }
    public class ShiftRostaringAuto
    {
        public int companyID { get; set; }
        public string fDate { get; set; }
        public string tDate { get; set; }
        public List<ShiftRostaringAutoChild> rosterChild { get; set; }
        public string userName { get; set; }
    }
    public class ShiftRostaringAutoChild
    {
        public int empSerial { get; set; }
        public int empId { get; set; }
        public int rGroupId { get; set; }
    }

    public class ShiftRosterPaloadList
    {
        public int sc_compID { get; set; }
        public int sc_empserial { get; set; }
        public string sc_effect_date { get; set; }
        public string sc_start_date { get; set; }
        public string sc_end_date { get; set; }
        public int sc_old_shift { get; set; }
        public int sc_new_shift { get; set; }
        public string sc_transfer_type { get; set; }
        public string sc_user { get; set; }
    }
}