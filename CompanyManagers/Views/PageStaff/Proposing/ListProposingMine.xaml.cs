using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class ListProposingMine : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private int _typeClickProposing;
        public int typeClickProposing
        {
            get { return _typeClickProposing; }
            set { _typeClickProposing = value; OnPropertyChanged("typeClickProposing"); }
        }

        private List<Result_CategoryProposing> _listCategoryProposingHome;
        public List<Result_CategoryProposing> listCategoyProposingHome
        {
            get { return _listCategoryProposingHome; }
            set { _listCategoryProposingHome = value; OnPropertyChanged("listCategoyProposingHome"); }
        }
        ManagerHome managerHome { get; set; }
        public ListProposingMine(ManagerHome _managerHome, List<Result_CategoryProposing> listCateProHome)
        {
            InitializeComponent();
            managerHome = _managerHome;
            listCategoyProposingHome = listCateProHome;
            searchKeyRespon.ItemsSource = _managerHome.dataListStaffAll;
            searchKeyCateProRespon.ItemsSource = listCateProHome;
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

        private void MouseClickSend(object sender, MouseButtonEventArgs e)
        {
            typeClickProposing = 1;
        }

        private void MouseClickRespon(object sender, MouseButtonEventArgs e)
        {
            typeClickProposing = 2;
        }

        private void MouseClickFollow(object sender, MouseButtonEventArgs e)
        {
            typeClickProposing = 3;
        }
    }
}
