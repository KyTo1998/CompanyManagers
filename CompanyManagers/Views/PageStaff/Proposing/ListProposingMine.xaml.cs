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

namespace CompanyManagers.Views.PageStaff.Proposing
{
    /// <summary>
    /// Interaction logic for ListProposingMine.xaml
    /// </summary>
    public partial class ListProposingMine : UserControl
    {
        ManagerHome managerHome { get; set; }
        public ListProposingMine(ManagerHome _managerHome)
        {
            InitializeComponent();
            managerHome = _managerHome;
        }

        private void Send_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void SendCatePro_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void TimeSearchStartSend(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void TimeSearchStopSend(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void TimeSearchStartRespon(object sender, SelectionChangedEventArgs e)
        {

        }
        private void TimeSearchStopRespon(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchProposing(object sender, TextChangedEventArgs e)
        {

        }
    }
}
