namespace BLL.Utility
{
    public class Getway
    {
        private static string _Mr_SpecFo;
        private static string _Dg_Payroll;
        public static string SpecFoCon
        {
            get
            {
                if (_Mr_SpecFo == null)
                {
                    //_Mr_SpecFo = String.Format("Data Source=.;Initial Catalog=SpecFo;Persist Security Info=true; User ID=sa; Password=DG!@#$;TrustServerCertificate=True");
                    //_Mr_SpecFo = String.Format("Data Source=192.168.101.250;Initial Catalog=SpecFo;Persist Security Info=true; User ID=sa; Password=dg@INFO;TrustServerCertificate=True");
                    _Mr_SpecFo = String.Format("Data Source=192.168.1.42;Initial Catalog=SpecFo;Persist Security Info=true; User ID=sa; Password=dg@2022;TrustServerCertificate=True");
                }
                return _Mr_SpecFo;
            }
        }
        public static string Dg_Payroll
        {
            get
            {
                if (_Dg_Payroll == null)
                {
                    //_Dg_Payroll = String.Format("Data Source=.;Max Pool Size=100;pooling='true';TrustServerCertificate=true;connection timeout=5000;MultipleActiveResultSets=True;Initial Catalog=dg_hrpayroll;Persist Security Info=True;User ID=sa;Password=DG!@#$", new object[0]);
                    //_Dg_Payroll = String.Format("Data Source=192.168.1.42;Initial Catalog=dg_hrpayroll;Persist Security Info=true; User ID=sa; Password=dg@2022;TrustServerCertificate=true;Connection Timeout=3600");
                    //_Dg_Payroll = String.Format("Data Source=192.168.101.250;Max Pool Size=100;pooling='true';TrustServerCertificate=true;connection timeout=5000;MultipleActiveResultSets=True;Initial Catalog=dg_hrpayroll;Persist Security Info=True;User ID=sa;Password=dg@INFO", new object[0]);
                    _Dg_Payroll = String.Format("Data Source=192.168.1.42;Max Pool Size=100;pooling='true';TrustServerCertificate=true;connection timeout=5000;MultipleActiveResultSets=True;Initial Catalog=dg_hrpayroll;Persist Security Info=True;User ID=sa;Password=dg@2022", new object[0]);
                }
                return _Dg_Payroll;
            }
        }
    }
}
