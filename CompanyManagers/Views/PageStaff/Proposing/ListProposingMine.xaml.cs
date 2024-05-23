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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private int _typeClickFilterProposing;
        public int typeClickFilterProposing
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
        BrushConverter br = new BrushConverter();
        public ListProposingMine(ManagerHome _managerHome, List<Result_CategoryProposing> listCateProHome)
        {
            InitializeComponent();
            managerHome = _managerHome;
            listCategoyProposingHome = listCateProHome;
            searchKeyRespon.ItemsSource = _managerHome.dataListStaffAll;
            searchKeyCateProRespon.ItemsSource = listCateProHome;
            searchKeySend.ItemsSource = _managerHome.dataListStaffAll;
            ClickSend.Background = new SolidColorBrush(Colors.LightBlue);
            typeClickProposing = 1;
            typeClickFilterProposing = 1;
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
                        try
                        {
                            Root_ProposingSendAll dataProposingSendAll = JsonConvert.DeserializeObject<Root_ProposingSendAll>(UnicodeEncoding.UTF8.GetString(e.Result));
                            if (dataProposingSendAll.data.data != null)
                            {
                                ListProposingSendAll.UpdateOrder(dataProposingSendAll.data.data);
                                listProposingSendAllLocal = dataProposingSendAll.data.data.ToList();
                                listProposingSendAll = dataProposingSendAll.data.data.ToList();
                            }
                        }
                        catch (Exception)
                        {
                        }
                    };
                    await request.UploadValuesTaskAsync(UrlApi.apiProposingSendToAll, request.QueryString);
                }
            }
            catch (Exception)
            {
            }
        }

        public async void GetProposingSendToMe()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.TokenEp);
                    if (searchKeyRespon.SelectedItem != null && ((Info_StaffAll)searchKeySend.SelectedItem).ep_id > 0)
                    {
                        request.QueryString.Add("id_user", ((Info_StaffAll)searchKeySend.SelectedItem).ep_id.ToString());
                    }
                    if (tb_SearchProposing.Text != null || tb_SearchProposing.Text != "")
                    {
                        request.QueryString.Add("name_dx", tb_SearchProposing.Text);
                    }
                    if (searchKeyCateProRespon.SelectedItem != null && ((Result_CategoryProposing)searchKeySend.SelectedItem)._id > 0)
                    {
                        request.QueryString.Add("type_dx", ((Result_CategoryProposing)searchKeySend.SelectedItem)._id.ToString());
                    }
                    if (dateTimeStartRespon.SelectedDate != null || dateTimeStartRespon.SelectedDate != null)
                    {
                        request.QueryString.Add("time_s", dateTimeStartRespon.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        request.QueryString.Add("time_e", dateTimeStopRespon.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    }
                    request.QueryString.Add("pageSize", "1000");
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        try
                        {
                            Root_ProposingSendAll dataProposingSendToMe = JsonConvert.DeserializeObject<Root_ProposingSendAll>(UnicodeEncoding.UTF8.GetString(e.Result));
                            if (dataProposingSendToMe.data.data != null)
                            {
                                ListProposingSendAll.UpdateOrder(dataProposingSendToMe.data.data);
                                listProposingSendAllLocal = dataProposingSendToMe.data.data.ToList();
                                listProposingSendAll = dataProposingSendToMe.data.data.ToList();
                            }
                        }
                        catch (Exception)
                        {
                        }
                    };
                    await request.UploadValuesTaskAsync(UrlApi.apiProposingSendToMe, request.QueryString);
                }
            }
            catch (Exception)
            {
            }
        }

        public async void GetProposingFollow(int type)
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.TokenEp);
                    request.QueryString.Add("type", type.ToString());
                    request.QueryString.Add("pageSize", "1000");
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        try
                        {
                            Root_ProposingSendAll dataProposingSendToMe = JsonConvert.DeserializeObject<Root_ProposingSendAll>(UnicodeEncoding.UTF8.GetString(e.Result));
                            if (dataProposingSendToMe.data.data != null)
                            {
                                ListProposingSendAll.UpdateOrder(dataProposingSendToMe.data.data);
                                listProposingSendAllLocal = dataProposingSendToMe.data.data.ToList();
                                listProposingSendAll = dataProposingSendToMe.data.data.ToList();
                            }
                        }
                        catch (Exception)
                        {
                        }
                    };
                    await request.UploadValuesTaskAsync(UrlApi.apiProposingFollow, request.QueryString);
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
            ClickSend.Background = new SolidColorBrush(Colors.LightBlue);
            ClickRespon.Background = (Brush)br.ConvertFrom("#F8F8F8");
            ClickFollow.Background = (Brush)br.ConvertFrom("#F8F8F8");
            GetProposingSendToAll();
        }

        private void MouseClickRespon(object sender, MouseButtonEventArgs e)
        {
            typeClickProposing = 2;
            ClickRespon.Background = new SolidColorBrush(Colors.LightBlue);
            ClickSend.Background = (Brush)br.ConvertFrom("#F8F8F8");
            ClickFollow.Background = (Brush)br.ConvertFrom("#F8F8F8");
            GetProposingSendToMe();
        }
        

        private void MouseClickFollow(object sender, MouseButtonEventArgs e)
        {
            typeClickProposing = 3;
            ClickFollow.Background = new SolidColorBrush(Colors.LightBlue);
            ClickRespon.Background = (Brush)br.ConvertFrom("#F8F8F8");
            ClickSend.Background = (Brush)br.ConvertFrom("#F8F8F8");
            GetProposingFollow(1);
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
            typeClickFilterProposing = 1;
            if (typeClickProposing == 3)
            {
                GetProposingFollow(typeClickFilterProposing);
            }
            ListProposingSendAll.UpdateOrder(listProposingSendAllLocal);
            listProposingSendAll = listProposingSendAllLocal.ToList();
            GetProposingFollow(typeClickFilterProposing);
        }

        private void FilterWait(object sender, MouseButtonEventArgs e)
        {
            typeClickFilterProposing = 2;
            if (typeClickProposing == 3)
            {
                GetProposingFollow(typeClickFilterProposing);
            }
            List<ListProposingSendAll> listProposingSendAllSearch = new List<ListProposingSendAll>();
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
        private void FilterOverdue(object sender, MouseButtonEventArgs e)
        {
            typeClickFilterProposing = 3;
            if (typeClickProposing == 3)
            {
                GetProposingFollow(typeClickFilterProposing);
            }
        }
        private void FilterApproved(object sender, MouseButtonEventArgs e)
        {
            typeClickFilterProposing = 4;
            if (typeClickProposing == 3)
            {
                GetProposingFollow(typeClickFilterProposing);
            }
            List<ListProposingSendAll> listProposingSendAllSearch = new List<ListProposingSendAll>();
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

        private void FilterGivingUp(object sender, MouseButtonEventArgs e)
        {
            typeClickFilterProposing = 5;
            if (typeClickProposing == 3)
            {
                GetProposingFollow(typeClickFilterProposing);
            }
            List<ListProposingSendAll> listProposingSendAllSearch = new List<ListProposingSendAll>();
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
