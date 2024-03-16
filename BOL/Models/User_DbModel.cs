using System.ComponentModel.DataAnnotations;

namespace BOL.Models
{
    public class User_DbModel
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; } = null;
        public string UserFullname { get; set; } = null;
        public string EmailId { get; set; } = null;
        public string Password { get; set; } = null;
        public string Designation { get; set; } = null;
        public DateTime? CreatedDate { get; set; } = null;
        public int? CompId { get;set; } = null;
        public string Emp_ID { get; set; } = null;
        public string Active_status { get; set; } = null;
        public int? Emp_serial { get; set; } = null;
        public int? Compliance { get; set; } = null;
    }
    public class TblUser
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null;
        public string UserFullName { get; set; } = null;
        public string EmailId { get; set; } = null;
        public string Password { get; set; } = null;
        public string Designation { get; set; } = null;
        public DateTime? CreatedDate { get; set; } = null;
        public int? CompId { get; set; } = null;
        public string EmpId { get; set; } = null;
        public string ActiveStatus { get; set; } = null;
        public int? EmpSerial { get; set; } = null;
        public int? Compliance { get; set; } = null;

        public static User_DbModel CustomToDbModel(TblUser obj)
        {
            try
            {
                var dbModel = new User_DbModel
                {
                    ID = obj.Id,
                    FullName = obj.FullName,
                    UserFullname = obj.UserFullName,
                    EmailId = obj.EmailId,
                    Password = obj.Password,
                    Designation = obj.Designation,
                    CreatedDate = obj.CreatedDate,
                    CompId = obj.CompId,
                    Emp_ID = obj.EmpId,
                    Active_status = obj.ActiveStatus,
                    Emp_serial = obj.EmpSerial,
                    Compliance = obj.Compliance
                };
                return dbModel;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return null;
        }
        public static TblUser DbToCustomModel(User_DbModel obj)
        {
            try
            {
                var customModel = new TblUser
                {
                    Id = obj.ID,
                    FullName = obj.FullName,
                    UserFullName = obj.UserFullname,
                    EmailId = obj.EmailId,
                    Password = obj.Password,
                    Designation = obj.Designation,
                    CreatedDate = obj.CreatedDate,
                    CompId = obj.CompId,
                    EmpId = obj.Emp_ID,
                    ActiveStatus = obj.Active_status,
                    EmpSerial = obj.Emp_serial,
                    Compliance = obj.Compliance
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
    public class UserModel
    {
        public int ID { get; set; }
        public int? CompID { get; set; } = null;
        public string compName { get; set; } = null;
        public string FullName { get; set; } = null;
        public string UserFullName { get; set; } = null;
        public string EmailId { get; set; } = null;
        public string Password { get; set; } = null;
        public string Designation { get; set; } = null;
        public string UserMessage { get; set; } = null;
        public string AccessToken { get; set; } = null;
        public DateTime? CreatedDate { get; set; } = null;
    }

    public class MenuList
    {
        public string menuText { get; set; }
        public string userName { get; set; }
    }
}