using CompanyManagers.Views.Home;
using System;
using System.Windows.Controls;
using System.Windows.Input;


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
