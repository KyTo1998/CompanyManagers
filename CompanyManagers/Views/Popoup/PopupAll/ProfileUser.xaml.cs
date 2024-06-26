using CompanyManagers.Views.Home;
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
                    ResourceDictionary Olddict = new ResourceDictionary();
                    Olddict.Source = new Uri("..\\Resources\\Colors\\ResourceThemeGreen.xaml", UriKind.Relative);

                    ResourceDictionary Olddict2 = new ResourceDictionary();
                    Olddict2.Source = new Uri("..\\Resources\\Colors\\ResourceThemeBlue.xaml", UriKind.Relative);

                    ResourceDictionary Olddict3 = new ResourceDictionary();
                    Olddict3.Source = new Uri("..\\Resources\\Colors\\ResourceThemePink.xaml", UriKind.Relative);

                    ResourceDictionary Olddict6 = new ResourceDictionary();
                    Olddict6.Source = new Uri("..\\Resources\\Colors\\ResourceThemeOrange.xaml", UriKind.Relative);

                    if (theme.Equals("GradienBlue"))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(Olddict2);
                    }
                    else if (theme.Equals("ThemePinkSingle"))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(Olddict3);
                    }
                    else if (theme.Equals("Green"))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(Olddict);
                    }
                    else if (theme.Equals("ThemeOrange"))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(Olddict6);
                    }
                    theme = value;
                    if (theme.Equals("GradienBlue"))
                    {
                        Properties.Settings.Default.Theme = "GradienBlue";
                        Properties.Settings.Default.Save();
                        Application.Current.Resources.MergedDictionaries.Add(Olddict2);
                    }
                    else if (theme.Equals("ThemePinkSingle"))
                    {
                        Properties.Settings.Default.Theme = "ThemePinkSingle";
                        Properties.Settings.Default.Save();
                        Application.Current.Resources.MergedDictionaries.Add(Olddict3);
                    }
                    else if (theme.Equals("Green"))
                    {
                        Properties.Settings.Default.Theme = "Green";
                        Properties.Settings.Default.Save();
                        Application.Current.Resources.MergedDictionaries.Add(Olddict);
                    }
                    else if (theme.Equals("ThemeOrange"))
                    {
                        Properties.Settings.Default.Theme = "ThemeOrange";
                        Properties.Settings.Default.Save();
                        Application.Current.Resources.MergedDictionaries.Add(Olddict6);
                    }
                    OnPropertyChanged("Theme");
                }
                catch (Exception)
                {

                }
            }
        }
        ManagerHome managerHome;
        public ProfileUser(ManagerHome _managerHome)
        {
            InitializeComponent();
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
    }
}
