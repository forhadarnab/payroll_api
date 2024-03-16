using System.ComponentModel.DataAnnotations;

namespace BOL.Models
{
    public class Tiffinbillrule_DbModel
    {
        [Key]
        public int serial { get; set; }
        public int? groupID { get; set; } = null;
        public int? com_code { get; set; } = null;
        public string com_Name { get; set; } = null;
        public decimal? upto_time { get; set; } = null;
        public int? category { get; set; } = null;
        public string categoryName { get; set; } = null;
        public int? amount_perDay { get; set; } = null;
        public string entedby { get; set; } = null;
        public string updatedby { get; set; } = null;
        public DateTime? addtime { get; set; } = null;
        public DateTime? updatetime { get; set; } = null;
    }
    public class DgPayTiffinbillrule
    {
        public int Serial { get; set; }
        public int? GroupId { get; set; } = null;
        public int? ComCode { get; set; } = null;
        public string ComName { get; set; } = null;
        public decimal? UptoTime { get; set; } = null;
        public int? Category { get; set; } = null;
        public string CategoryName { get; set; } = null;
        public int? AmountPerDay { get; set; } = null;
        public string Entedby { get; set; } = null;
        public string Updatedby { get; set; } = null;
        public DateTime? Addtime { get; set; } = null;
        public DateTime? Updatetime { get; set; } = null;

        public static Tiffinbillrule_DbModel CustomToDbModel(DgPayTiffinbillrule obj)
        {
            try
            {
                var dbModel = new Tiffinbillrule_DbModel
                {
                    serial = obj.Serial,
                    groupID = obj.GroupId,
                    com_code = obj.ComCode,
                    com_Name = obj.ComName,
                    upto_time = obj.UptoTime,
                    category = obj.Category,
                    categoryName = obj.CategoryName,
                    amount_perDay = obj.AmountPerDay,
                    entedby = obj.Entedby,
                    updatedby = obj.Updatedby,
                    addtime = obj.Addtime,
                    updatetime = obj.Updatetime
                };
                return dbModel;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return null;
        }
        public static DgPayTiffinbillrule DbToCustomModel(Tiffinbillrule_DbModel obj)
        {
            try
            {
                var customModel = new DgPayTiffinbillrule
                {
                    Serial = obj.serial,
                    GroupId = obj.groupID,
                    ComCode = obj.com_code,
                    ComName = obj.com_Name,
                    UptoTime = obj.upto_time,
                    Category = obj.category,
                    CategoryName = obj.categoryName,
                    AmountPerDay = obj.amount_perDay,
                    Entedby = obj.entedby,
                    Updatedby = obj.updatedby,
                    Addtime = obj.addtime,
                    Updatetime = obj.updatetime
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