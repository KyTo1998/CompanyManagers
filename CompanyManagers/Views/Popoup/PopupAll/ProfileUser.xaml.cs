using CompanyManagers.Common.Popups;
using CompanyManagers.Views.Home;
using CompanyManagers.Views.Login;
using CompanyManagers.Views.Logout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyManagers.Views.Popoup.PopupAll
{
    /// <summary>
    /// Interaction logic for ProfileUser.xaml
    /// </summary>
    public partial class ProfileUser : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        public string theme = "";
        public string Theme
        {
            get { return theme; }
            set
            {
                try
                {
                    ResourceDictionary Olddict1 = new ResourceDictionary();
                    Olddict1.Source = new Uri("..\\Resources\\Colors\\ResourceThemeBlue.xaml", UriKind.Relative);

                    ResourceDictionary Olddict2 = new ResourceDictionary();
                    Olddict2.Source = new Uri("..\\Resources\\Colors\\ResourceThemeGreen.xaml", UriKind.Relative);

                    ResourceDictionary Olddict3 = new ResourceDictionary();
                    Olddict3.Source = new Uri("..\\Resources\\Colors\\ResourceThemeOrange.xaml", UriKind.Relative);

                    ResourceDictionary Olddict4 = new ResourceDictionary();
                    Olddict4.Source = new Uri("..\\Resources\\Colors\\ResourceThemePink.xaml", UriKind.Relative);

                    if (theme.Equals("GradienBlue"))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(Olddict1);
                    }
                    else if (theme.Equals("Green"))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(Olddict2);
                    }
                    else if (theme.Equals("ThemeOrange"))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(Olddict3);
                    }
                    else if (theme.Equals("ThemePinkSingle"))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(Olddict4);
                    }
                    theme = value;
                    if (theme.Equals("GradienBlue"))
                    {
                        Properties.Settings.Default.Theme = "GradienBlue";
                        Properties.Settings.Default.Save();
                        Application.Current.Resources.MergedDictionaries.Add(Olddict1);
                    }
                    else if (theme.Equals("Green"))
                    {
                        Properties.Settings.Default.Theme = "Green";
                        Properties.Settings.Default.Save();
                        Application.Current.Resources.MergedDictionaries.Add(Olddict2);
                    }
                    else if (theme.Equals("ThemeOrange"))
                    {
                        Properties.Settings.Default.Theme = "ThemeOrange";
                        Properties.Settings.Default.Save();
                        Application.Current.Resources.MergedDictionaries.Add(Olddict3);
                    }
                    else if (theme.Equals("ThemePinkSingle"))
                    {
                        Properties.Settings.Default.Theme = "ThemePinkSingle";
                        Properties.Settings.Default.Save();
                        Application.Current.Resources.MergedDictionaries.Add(Olddict4);
                    }
                    OnPropertyChanged("Theme");
                }
                catch (Exception)
                {

                }
            }
        }
        private string _LinkAvatar;
        public string LinkAvatar
        {
            get { return _LinkAvatar; }
            set { _LinkAvatar = value; OnPropertyChanged("LinkAvatar"); }
        }
        ManagerHome managerHome;
        LoginHome loginHome { get; set; }
        public ProfileUser(ManagerHome _managerHome, LoginHome _loginHome)
        {
            InitializeComponent();
            Theme = Properties.Settings.Default.Theme;
            Profile_tb_name.Text = _managerHome.UserName;
            NumberPhoneUser.Text = _managerHome.NumberPhone;
            this.loginHome = loginHome;
            this.managerHome = _managerHome;
            LinkAvatar = _managerHome.LinkAvatar;
        }

        private void ChangeThemeClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Border).Name.Equals("ThemeGradienBlue"))
            {
                Theme = "GradienBlue";
            }
            else if ((sender as Border).Name.Equals("ThemePinkSingle"))
            {
                Theme = "ThemePinkSingle";
            }
            else if ((sender as Border).Name.Equals("ThemeGreen"))
            {
                Theme = "Green";
            }
            else if ((sender as Border).Name.Equals("ThemeOrange"))
            {
                Theme = "ThemeOrange";
            }
        }

        private void ShowPopupLogout(object sender, MouseButtonEventArgs e)
        {
            try
            {
                managerHome.PagePopupGrayColor = new PagePopupGrayColor(managerHome);
                managerHome.PagePopupGrayColor.Popup1.NavigationService.Navigate(new PageLogout(managerHome, loginHome));
                managerHome.PagePopup.NavigationService.Navigate(managerHome.PagePopupGrayColor);
            }
            catch (Exception)
            {
            }
        }
    }
}
