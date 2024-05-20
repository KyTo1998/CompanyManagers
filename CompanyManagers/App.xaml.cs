
using System.Windows;

namespace CompanyManagers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                /*if (e.Args.Length > 0)
                {
                    CompanyManagers.Properties.Settings.Default.UserLinkApp = e.Args[0];
                    CompanyManagers.Properties.Settings.Default.PassMD5 = e.Args[1];
                    CompanyManagers.Properties.Settings.Default.Type365 = e.Args[2];
                    CompanyManagers.Properties.Settings.Default.IdDeXuat = e.Args[3];
                }
                else
                {
                    CompanyManagers.Properties.Settings.Default.UserLinkApp = "";
                    CompanyManagers.Properties.Settings.Default.PassMD5 = "";
                    CompanyManagers.Properties.Settings.Default.Type365 = "";
                    CompanyManagers.Properties.Settings.Default.IdDeXuat = "";
                }*/
            }
            catch { }
            
        }
    }
}
