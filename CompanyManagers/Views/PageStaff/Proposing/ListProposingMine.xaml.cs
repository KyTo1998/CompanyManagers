using CompanyManagers.Common.Tool;
using CompanyManagers.Controllers;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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

        private string _typeClickFilterProposing;
        public string typeClickFilterProposing
        {
            get { return _typeClickFilterProposing; }
            set { _typeClickFilterProposing = value; OnPropertyChanged("typeClickFilterProposing"); }
        }
        private List<Result_CategoryProposing> _listCategoryProposingHome;
        public List<Result_CategoryProposing> listCategoyProposingHome
        {
            get { return _listCategoryProposingHome; }
            set { _listCategoryProposingHome = value; OnPropertyChanged("listCategoyProposingHome"); }
        }
        private List<ListProposingSendAll> _listProposingSendAll;
        public List<ListProposingSendAll> listProposingSendAll
        {
            get { return _listProposingSendAll; }
            set { _listProposingSendAll = value; OnPropertyChanged("listProposingSendAll"); }
        }
        List<ListProposingSendAll> listProposingSendAllLocal = new List<ListProposingSendAll>();
        List<string> typeWait = new List<string>() {"0","6","7","10","11" };
        ManagerHome managerHome { get; set; }
        public ListProposingMine(ManagerHome _managerHome, List<Result_CategoryProposing> listCateProHome)
        {
            InitializeComponent();
            managerHome = _managerHome;
            listCategoyProposingHome = listCateProHome;
            searchKeyRespon.ItemsSource = _managerHome.dataListStaffAll;
            searchKeyCateProRespon.ItemsSource = listCateProHome;
            searchKeySend.ItemsSource = _managerHome.dataListStaffAll;
            typeClickProposing = 1;
            typeClickFilterProposing = "All";
            GetProposingSendToAll();
        }

        public async void GetProposingSendToAll()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.TokenEp);
                    if (searchKeySend.SelectedItem != null && ((Info_StaffAll)searchKeySend.SelectedItem).ep_id > 0)
                    {
                        request.QueryString.Add("id_user_duyet", ((Info_StaffAll)searchKeySend.SelectedItem).ep_id.ToString());
                    }
                    if (dateTimeStartSend.SelectedDate != null || dateTimeStopSend.SelectedDate != null)
                    {
                        request.QueryString.Add("time_s", dateTimeStartSend.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        request.QueryString.Add("time_e", dateTimeStopSend.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    }
                    request.QueryString.Add("pageSize", "1000");
                    request.UploadValuesCompleted += (s, e) =>
                    {
                         Root_ProposingSendAll dataProposingSendAll = JsonConvert.DeserializeObject<Root_ProposingSendAll>(UnicodeEncoding.UTF8.GetString(e.Result));
                        if (dataProposingSendAll.data.data != null)
                        {
                            ListProposingSendAll.UpdateOrder(dataProposingSendAll.data.data);
                            listProposingSendAllLocal = dataProposingSendAll.data.data.ToList();
                            listProposingSendAll = dataProposingSendAll.data.data.ToList();
                            
                        }
                    };
                    await request.UploadValuesTaskAsync(UrlApi.apiProposingSendToAll, request.QueryString);
                }
            }
            catch (Exception)
            {
            }
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

        private void SearchProposingSendTo(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GetProposingSendToAll();
            }
            catch (Exception)
            {
            }
        }
       
        private void FilterAll(object sender, MouseButtonEventArgs e)
        {
            typeClickFilterProposing = "All";
            if (typeClickProposing == 1)
            {
               ListProposingSendAll.UpdateOrder(listProposingSendAllLocal);
               listProposingSendAll = listProposingSendAllLocal.ToList();
            }

        }

        private void FilterWait(object sender, MouseButtonEventArgs e)
        {
            typeClickFilterProposing = "Wait";
            List<ListProposingSendAll> listProposingSendAllSearch = new List<ListProposingSendAll>();
            if (typeClickProposing == 1)
            {
                foreach (var item in listProposingSendAllLocal)
                {
                    if (typeWait.Contains(item.type_duyet))
                    {
                        listProposingSendAllSearch.Add(item);
                    }
                }
                ListProposingSendAll.UpdateOrder(listProposingSendAllSearch);
                listProposingSendAll = listProposingSendAllSearch.ToList();
            }
        }

        private void FilterApproved(object sender, MouseButtonEventArgs e)
        {
            typeClickFilterProposing = "Approved";
            List<ListProposingSendAll> listProposingSendAllSearch = new List<ListProposingSendAll>();
            if (typeClickProposing == 1)
            {
                foreach (var item in listProposingSendAllLocal)
                {
                    if (item.type_duyet == "5")
                    {
                        listProposingSendAllSearch.Add(item);
                    }
                }
                 ListProposingSendAll.UpdateOrder(listProposingSendAllSearch);
                listProposingSendAll = listProposingSendAllSearch.ToList();
            }
        }

        private void FilterGivingUp(object sender, MouseButtonEventArgs e)
        {
            typeClickFilterProposing = "GivingUp";
            List<ListProposingSendAll> listProposingSendAllSearch = new List<ListProposingSendAll>();
            if (typeClickProposing == 1)
            {
                foreach (var item in listProposingSendAllLocal)
                {
                    if (item.type_duyet == "3")
                    {
                        listProposingSendAllSearch.Add(item);
                    }
                }
                 ListProposingSendAll.UpdateOrder(listProposingSendAllSearch);
                listProposingSendAll = listProposingSendAllSearch.ToList();
            }
        }
    }
}
