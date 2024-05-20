using CompanyManagers.Views.Home;
using CompanyManagers.Views.Login;
using System;
using System.Windows.Input;


namespace CompanyManagers.Views.Logout
{
    /// <summary>
    /// Interaction logic for PageLogout.xaml
    /// </summary>
    public partial class PageLogout
    {
        ManagerHome managerHome { get; set; }
        LoginHome loginHome { get; set; }
        public PageLogout(ManagerHome _managerHome,  LoginHome _loginHome)
        {                           
            InitializeComponent();
            managerHome = _managerHome;
            loginHome = _loginHome;
        }

        private void CancelLogout(object sender, MouseButtonEventArgs e)
        {
            managerHome.PagePopup.NavigationService.Navigate(null);
        }

        private void ConfirmLogout(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.Type365 == "1")
                {
                    Properties.Settings.Default.TokenCom = "";
                    Properties.Settings.Default.PassCom = "";
                    Properties.Settings.Default.RememberMe = true;
                    Properties.Settings.Default.Save();
                    managerHome.Close();
                    managerHome = null;
                    loginHome = new LoginHome("ClickLogout",Properties.Settings.Default.UserCom);
                }
                else 
                {
                    Properties.Settings.Default.TokenEp = "";
                    Properties.Settings.Default.PassEp = "";
                    Properties.Settings.Default.RememberMe = true;
                    Properties.Settings.Default.Save();
                    managerHome.Close();
                    managerHome = null;
                    loginHome = new LoginHome("ClickLogout",Properties.Settings.Default.UserEp);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
