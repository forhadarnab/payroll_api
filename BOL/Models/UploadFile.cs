using Microsoft.AspNetCore.Http;

namespace BOL.Models
{
    public class UploadFile
    {
        public int CompanyID { get; set; }
        public string DateFormat { get; set; }
        public IFormFile file { get; set; }
    }
    public class MyEntity
    {
        public string emp_ProxID { get; set; }
        public string emp_Slno { get; set; }
        public string date { get; set; }
        public string time { get; set; }
    }
}
