using CompanyManagers.Controllers;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
        string typeClickAtHomePropose;
        public ListProposingMine(ManagerHome _managerHome, List<Result_CategoryProposing> listCateProHome, string _typeClickAtHomePropose)
        {
            InitializeComponent();
            managerHome = _managerHome;
            typeClickAtHomePropose = _typeClickAtHomePropose;
            listCategoyProposingHome = listCateProHome;
            searchKeyRespon.ItemsSource = _managerHome.dataListStaffAll;
            searchKeyCateProRespon.ItemsSource = listCateProHome;
            searchKeySend.ItemsSource = _managerHome.dataListStaffAll;
            ClickSend.Background = new SolidColorBrush(Colors.LightBlue);
            GetProposingSendToAll();
            if (_typeClickAtHomePropose == "AllToHome")
            {
                typeClickProposing = 1;                                        
                typeClickFilterProposing = 1;
            }
            else if (_typeClickAtHomePropose == "WaitComfirmToHome")
            {
                typeClickProposing = 1;
                LoadFilterWait();
            }
            else if(_typeClickAtHomePropose == "NecessaryComfirmToHome")
            {
                typeClickProposing = 2;
                LoadUiClickRespon();
            }
            else if(_typeClickAtHomePropose == "ProposeApprovedProposing")
            {
                typeClickProposing = 2;
                LoadUiClickRespon();
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
                if (typeClickAtHomePropose == "NecessaryComfirmToHome")
                {
                    LoadFilterWait();
                }
                else if (typeClickAtHomePropose == "ProposeApprovedProposing")
                {
                    LoadFilterApproved();
                }
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
                if (dateTimeStartSend.SelectedDate != null || dateTimeStopSend.SelectedDate != null)
                {
                    data = new
                    {
                        time_s = dateTimeStartSend.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        time_e = dateTimeStopSend.SelectedDate.Value.ToString("yyyy-MM-dd")
                    };
                }
                if (searchKeySend.SelectedItem != null && ((Info_StaffAll)searchKeySend.SelectedItem).ep_id > 0 && dateTimeStartSend.SelectedDate != null || dateTimeStopSend.SelectedDate != null)
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
                StartApi();
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
                    { "time_s", dateTimeStartRespon.SelectedDate.Value.ToString("yyyy-MM-dd") },
                    { "time_e", dateTimeStopRespon.SelectedDate.Value.ToString("yyyy-MM-dd") }
                };
                    data.Add(newItem);
                }
                Dictionary<string, string> newItemAll = new Dictionary<string, string>
                {
                    { "pageSize", "1000" }
                };
                data.Add(newItemAll);
                string jsonData = ConvertListOfDictionariesToJson(data);
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + Properties.Settings.Default.Token;
                    threadSendToMe = new Thread(() =>
                    {
                        try
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
                        }
                        catch (Exception)
                        {
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

        public static string ConvertListOfDictionariesToJson(List<Dictionary<string, string>> list)
        {
            // Tạo một JObject mới để chứa kết quả hợp nhất
            JObject combinedObject = new JObject();
            // Duyệt qua từng từ điển trong danh sách
            foreach (var dict in list)
            {
                // Duyệt qua từng cặp key-value trong từ điển
                foreach (var kvp in dict)
                {
                    // Thêm từng cặp key-value vào đối tượng kết hợp
                    combinedObject[kvp.Key] = kvp.Value;
                }
            }
            // Chuyển đổi đối tượng kết hợp thành chuỗi JSON
            return combinedObject.ToString(Formatting.None);
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
            typeClickAtHomePropose = "";
            ClickSend.Background = new SolidColorBrush(Colors.LightBlue);
            ClickRespon.Background = (Brush)br.ConvertFrom("#F8F8F8");
            ClickFollow.Background = (Brush)br.ConvertFrom("#F8F8F8");
            GetProposingSendToAll();
        }

        private void MouseClickRespon(object sender, MouseButtonEventArgs e)
        {
            typeClickProposing = 2;
            typeClickFilterProposing = 1;
            typeClickAtHomePropose = "";
            LoadUiClickRespon();
        }
        
        private void LoadUiClickRespon()
        {
            ClickRespon.Background = new SolidColorBrush(Colors.LightBlue);
            ClickSend.Background = (Brush)br.ConvertFrom("#F8F8F8");
            ClickFollow.Background = (Brush)br.ConvertFrom("#F8F8F8");
            GetProposingSendToMe();
        }

        private void MouseClickFollow(object sender, MouseButtonEventArgs e)
        {
            typeClickProposing = 3;
            typeClickAtHomePropose = "";
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
            typeClickAtHomePropose = "";
            if (typeClickProposing == 3)
            {
                GetProposingFollow(typeClickFilterProposing);
            }
            ListProposingSendAll.UpdateOrder(listProposingSendAllLocal);
            listProposingSendAll = listProposingSendAllLocal.ToList();
        }

        private void FilterWait(object sender, MouseButtonEventArgs e)
        {
            typeClickAtHomePropose = "";
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
            typeClickAtHomePropose = "";
            if (typeClickProposing == 3)
            {
                GetProposingFollow(typeClickFilterProposing);
            }
        }
        private void FilterApproved(object sender, MouseButtonEventArgs e)
        {
            typeClickAtHomePropose = "";
            LoadFilterApproved();
        }

        private void LoadFilterApproved()
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

        private void ShowDetailPropose(object sender, MouseButtonEventArgs e)
        {
            ListProposingSendAll dataPropose = (ListProposingSendAll)(sender as Border).DataContext;
            if (dataPropose != null)
            {
                managerHome.GetDetailPropose(null, dataPropose);
            }
        }
    }
}
