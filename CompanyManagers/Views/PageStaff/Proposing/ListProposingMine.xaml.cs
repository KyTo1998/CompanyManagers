using CompanyManagers.Controllers;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Models.ModelsShift;
using CompanyManagers.Views.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Documents;
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
        private int _statusError;
        public int statusError
        {
            get { return _statusError; }
            set { _statusError = value; OnPropertyChanged("statusError"); }
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
        public ListProposingMine(ManagerHome _managerHome, List<Result_CategoryProposing> listCateProHome, string typeClickAtHomePropose)
        {
            InitializeComponent();
            managerHome = _managerHome;
            listCategoyProposingHome = listCateProHome;
            searchKeyRespon.ItemsSource = _managerHome.dataListStaffAll;
            searchKeyCateProRespon.ItemsSource = listCateProHome;
            searchKeySend.ItemsSource = _managerHome.dataListStaffAll;
            ClickSend.Background = new SolidColorBrush(Colors.LightBlue);
            GetProposingSendToAll();
            if (typeClickAtHomePropose == "AllToHome")
            {
                typeClickProposing = 1;                                        
                typeClickFilterProposing = 1;
            }
            else if (typeClickAtHomePropose == "WaitComfirmToHome")
            {
                typeClickProposing = 1;
                LoadFilterWait();
            }
            else if(typeClickAtHomePropose == "NecessaryComfirmToHome")
            {
                typeClickProposing = 2;
                typeClickFilterProposing = 2;
                ClickRespon.Background = new SolidColorBrush(Colors.LightBlue);
                ClickSend.Background = (Brush)br.ConvertFrom("#F8F8F8");
                ClickFollow.Background = (Brush)br.ConvertFrom("#F8F8F8");
                GetProposingSendToMe();
                LoadFilterWait();
            }
            
        }

        public void LoadDataProposing(Root_ProposingSendAll dataProposing)
        {
            if (dataProposing.data.data != null && dataProposing.data.data.Count > 0)
            {
                ListProposingSendAll.UpdateOrder(dataProposing.data.data);
                listProposingSendAllLocal = dataProposing.data.data.ToList();
                listProposingSendAll = dataProposing.data.data.ToList();
                LoadingSpinner.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                statusError = 1;
                ListProposingSendAll.UpdateOrder(dataProposing.data.data);
                listProposingSendAllLocal = dataProposing.data.data.ToList();
                listProposingSendAll = dataProposing.data.data.ToList();
                LoadingSpinner.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        public void LoadErroGetApi()
        {
            statusError = 2;
            LoadingSpinner.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void StartApi()
        {
            statusError = 0;
            LoadingSpinner.Visibility = System.Windows.Visibility.Visible;
        }
        public void GetProposingSendToAll()
        {
            try
            {
                StartApi();
                var data = new object();
                if (searchKeySend.SelectedItem != null && ((Info_StaffAll)searchKeySend.SelectedItem).ep_id > 0)
                {
                    data = new
                    {
                        id_user_duyet = ((Info_StaffAll)searchKeySend.SelectedItem).ep_id.ToString()
                    };
                }
                else if (dateTimeStartSend.SelectedDate != null || dateTimeStopSend.SelectedDate != null)
                {
                    data = new
                    {
                        time_s = dateTimeStartSend.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        time_e = dateTimeStopSend.SelectedDate.Value.ToString("yyyy-MM-dd")
                    };
                }
                else if (searchKeySend.SelectedItem != null && ((Info_StaffAll)searchKeySend.SelectedItem).ep_id > 0 && dateTimeStartSend.SelectedDate != null || dateTimeStopSend.SelectedDate != null)
                {
                    data = new
                    {
                        id_user_duyet = ((Info_StaffAll)searchKeySend.SelectedItem).ep_id.ToString(),
                        time_s = dateTimeStartSend.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        time_e = dateTimeStopSend.SelectedDate.Value.ToString("yyyy-MM-dd")
                    };
                }
                string jsonData = JsonConvert.SerializeObject(data);
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + Properties.Settings.Default.Token;
                    var resData = webClient.UploadString(UrlApi.Url_Api_Proposing + UrlApi.Name_Api_ProposingSendToAll, "POST", jsonData);
                    byte[] bytesData = Encoding.Default.GetBytes(resData);
                    Root_ProposingSendAll dataProposingSendAll = JsonConvert.DeserializeObject<Root_ProposingSendAll>(UnicodeEncoding.UTF8.GetString(bytesData));
                    try
                    {
                        if (dataProposingSendAll != null)
                        {
                            LoadDataProposing(dataProposingSendAll);
                        } 
                    }
                    catch (Exception)
                    {
                        LoadErroGetApi();
                    }
                }
            }
            catch (Exception)
            {
                LoadErroGetApi();
            }
        }
        Thread threadSendToMe;
        public void GetProposingSendToMe()
        {
            try
            {
                
                    Dispatcher.Invoke(() =>
                    {
                        StartApi();
                    });
                    List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
                    if (searchKeyRespon.SelectedItem != null && ((Info_StaffAll)searchKeyRespon.SelectedItem).ep_id > 0)
                    {
                        Dictionary<string, string> newItem = new Dictionary<string, string>
                    {
                        { "id_user", ((Info_StaffAll)searchKeyRespon.SelectedItem).ep_id.ToString() }
                    };
                        data.Add(newItem);
                    }
                    if (tb_SearchProposing.Text != "")
                    {
                        Dictionary<string, string> newItem = new Dictionary<string, string>
                    {
                        { "name_dx", tb_SearchProposing.Text }
                    };
                        data.Add(newItem);
                    }
                    if (searchKeyCateProRespon.SelectedItem != null && ((Result_CategoryProposing)searchKeyCateProRespon.SelectedItem)._id > 0)
                    {
                        Dictionary<string, string> newItem = new Dictionary<string, string>
                    {
                        { "type_dx", ((Result_CategoryProposing)searchKeyCateProRespon.SelectedItem)._id.ToString() }
                    };
                        data.Add(newItem);
                    }
                    if (dateTimeStartRespon.SelectedDate != null || dateTimeStartRespon.SelectedDate != null)
                    {
                        Dictionary<string, string> newItem = new Dictionary<string, string>
                    {
                        { "time_s", ((Result_CategoryProposing)searchKeyCateProRespon.SelectedItem)._id.ToString() },
                        { "time_e", ((Result_CategoryProposing)searchKeyCateProRespon.SelectedItem)._id.ToString() }
                    };
                        data.Add(newItem);
                    }
                    Dictionary<string, string> newItemAll = new Dictionary<string, string>
                    {
                        { "pageSize", "1000" }
                    };
                    data.Add(newItemAll);
                    string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                    if (jsonData.StartsWith("["))
                    {
                        jsonData = jsonData.Substring(1);
                    }
                    if (jsonData.EndsWith("]"))
                    {
                        jsonData = jsonData.Substring(0, jsonData.Length - 1);
                    }
                    jsonData = jsonData.Replace("\r", "").Replace("\n", "").Replace(" ", "");
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + Properties.Settings.Default.Token;
                    threadSendToMe = new Thread(() =>
                    {
                        var resData = webClient.UploadString(UrlApi.Url_Api_Proposing + UrlApi.Name_Api_ProposingSendToMe, "POST", jsonData);
                        byte[] bytesData = Encoding.Default.GetBytes(resData);
                        try
                        {
                            Root_ProposingSendAll dataProposingSendToMe = JsonConvert.DeserializeObject<Root_ProposingSendAll>(UnicodeEncoding.UTF8.GetString(bytesData));
                            if (dataProposingSendToMe != null)
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    LoadDataProposing(dataProposingSendToMe);
                                });
                            }
                        }
                        catch (Exception)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                LoadErroGetApi();
                            });

                        }
                    });
                    threadSendToMe.Start();

                }
            }
            catch (Exception)
            {
                LoadErroGetApi();
            }
        }

        public void GetProposingFollow(int type)
        {
            try
            {
                StartApi();
                var data = new object();
                data = new
                {
                    type = type.ToString(),
                    pageSize = "1000"
                };
                string jsonData = JsonConvert.SerializeObject(data);
                using (WebClient request = new WebClient())
                {
                    request.Headers[HttpRequestHeader.ContentType] = "application/json";
                    request.Headers[HttpRequestHeader.Authorization] = "Bearer " + Properties.Settings.Default.Token;
                    var resData = request.UploadString(UrlApi.Url_Api_Proposing + UrlApi.Name_Api_ProposingFollow, "POST", jsonData);
                    byte[] bytesData = Encoding.Default.GetBytes(resData);
                    Root_ProposingSendAll dataProposingFollow = JsonConvert.DeserializeObject<Root_ProposingSendAll>(UnicodeEncoding.UTF8.GetString(bytesData));
                    try
                    {
                        if (dataProposingFollow != null)
                        {
                            LoadDataProposing(dataProposingFollow);
                        }
                    }
                    catch (Exception)
                    {
                        LoadErroGetApi();
                    }
                }
            }
            catch (Exception)
            {
                LoadErroGetApi();
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
        private void SearchProposingSendToMe(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GetProposingSendToMe();
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
        }

        private void FilterWait(object sender, MouseButtonEventArgs e)
        {
            LoadFilterWait();
        }

        private void LoadFilterWait()
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
