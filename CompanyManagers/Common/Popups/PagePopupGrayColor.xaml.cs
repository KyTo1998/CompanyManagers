using CompanyManagers.Views.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyManagers.Common.Popups
{
    /// <summary>
    /// Interaction logic for PagePopupGrayColor.xaml
    /// </summary>
   
    public partial class PagePopupGrayColor : Page
    {
        ManagerHome managerHome {  get; set; }
        public PagePopupGrayColor(ManagerHome _managerHome)
        {
            InitializeComponent();
            managerHome = _managerHome;
        }

        private void CloseAllPopup(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Popup.NavigationService.Navigate(null);
                Popup1.NavigationService.Navigate(null);
                Popup2.NavigationService.Navigate(null);
                managerHome.PagePopup.NavigationService.Navigate(null);
            }
            catch (Exception)
            {
            }
        }
    }
}
