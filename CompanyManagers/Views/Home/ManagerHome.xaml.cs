﻿using CompanyManagers.Common.Popups;
using CompanyManagers.Controllers;
using CompanyManagers.Models.HomeFunction;
using CompanyManagers.Models.Logins;
using CompanyManagers.Models.ModelRose;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Models.ModelsShift;
using CompanyManagers.Views.Functions.HomeFunction;
using CompanyManagers.Views.Login;
using CompanyManagers.Views.Logout;
using CompanyManagers.Views.PageStaff.Proposing;
using CompanyManagers.Views.Popoup.PopupAll;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using static CompanyManagers.Views.PageStaff.Proposing.pageCreateNewProposing;

namespace CompanyManagers.Views.Home
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ManagerHome : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private int _IsFull = 0;
        public int IsFull
        {
            get { return _IsFull; }
            set
            {
                _IsFull = value;
                var workingArea = System.Windows.SystemParameters.WorkArea;
                switch (IsFull)
                {
                    case 0:
                        this.WindowState = WindowState.Normal;
                        Width = oldWidth;
                        Height = oldHeight;
                        Left = oldLeft;
                        Top = oldTop;
                        this.ResizeMode = ResizeMode.CanResizeWithGrip;
                        NormalSize.Visibility = Visibility.Collapsed;
                        Maximimize.Visibility = Visibility.Visible;
                        logoInTitle.Margin = new Thickness(0, 0, 0, 0);
                        break;
                    case 1:
                        oldWidth = this.ActualWidth;
                        oldHeight = this.ActualHeight;
                        oldTop = this.Top;
                        oldLeft = this.Left;
                        this.WindowState = WindowState.Normal;
                        Left = workingArea.TopLeft.X;
                        Top = workingArea.TopLeft.Y;
                        Width = workingArea.Width;
                        Height = workingArea.Height;
                        this.ResizeMode = ResizeMode.CanResizeWithGrip;
                        NormalSize.Visibility = Visibility.Visible;
                        Maximimize.Visibility = Visibility.Collapsed;
                        break;
                    case 2:
                        oldWidth = workingArea.Right - 440;
                        oldHeight = workingArea.Bottom - 180;
                        oldLeft = (workingArea.Right / 2) - (this.oldWidth / 2);
                        oldTop = (workingArea.Bottom / 2) - (this.oldHeight / 2);
                        this.WindowState = WindowState.Normal;
                        Left = workingArea.TopLeft.X;
                        Top = workingArea.TopLeft.Y;
                        Width = workingArea.Width;
                        Height = workingArea.Height;
                        this.ResizeMode = ResizeMode.CanResizeWithGrip;
                        NormalSize.Visibility = Visibility.Visible;
                        Maximimize.Visibility = Visibility.Collapsed;
                        break;
                    default:
                        break;
                }
                OnPropertyChanged("IsFull");
            }
        }

        double oldWidth = 824;
        double oldHeight = 690;
        double oldTop = 690;
        double oldLeft = 690;

        private List<DataFunction> _dataFunctionHome;
        public List<DataFunction> dataFunctionHome 
        {
            get { return _dataFunctionHome; }
            set { _dataFunctionHome = value; OnPropertyChanged("dataFunctionHome");}
        }
        private DataUserLogin _UserCurrent;
        public DataUserLogin UserCurrent 
        {
            get { return _UserCurrent; }
            set { _UserCurrent = value; OnPropertyChanged("UserCurrent");}
        }

        private List<Info_StaffAll> _dataListStaffAll;
        public List<Info_StaffAll> dataListStaffAll
        {
            get { return _dataListStaffAll; }
            set { _dataListStaffAll = value; OnPropertyChanged("dataListStaffAll"); }
        }

        private List<Item_ShiftAll> _dataListShiftAll;
        public List<Item_ShiftAll> dataListShiftAll
        {
            get { return _dataListShiftAll; }
            set { _dataListShiftAll = value; OnPropertyChanged("dataListShiftAll"); }
        }

        private List<ListUsersDuyet> _dataListUserComfrim;
        public List<ListUsersDuyet> dataListUserComfrim
        {
            get { return _dataListUserComfrim; }
            set { _dataListUserComfrim = value; OnPropertyChanged("dataListUserComfrim"); }
        }

        private List<ListUsersTheoDoi> _dataListUserFollow;
        public List<ListUsersTheoDoi> dataListUserFollow
        {
            get { return _dataListUserFollow; }
            set { _dataListUserFollow = value; OnPropertyChanged("dataListUserFollow"); }
        }

        private List<StaffShiftInDay> _dataListStaffShiftInDay;
        public List<StaffShiftInDay> dataListStaffShiftInDay
        {
            get { return _dataListStaffShiftInDay; }
            set { _dataListStaffShiftInDay = value; OnPropertyChanged("dataListStaffShiftInDay"); }
        }

        public class typeConfirm
        {
            public int id_Custom { get; set; }
            public string name_Custom { get; set; }
        }
        public List<typeConfirm> lstTypeConfirms = new List<typeConfirm>();
        public List<typeConfirm> lstTypeConfirmsLocal = new List<typeConfirm>();
       
        private string _backToBack;
        public string backToBack 
        {
            get { return _backToBack; }
            set { _backToBack = value; OnPropertyChanged("backToBack");}
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged("UserName"); }
        }
        private string numberPhone;
        public string NumberPhone
        {
            get { return numberPhone; }
            set { numberPhone = value; OnPropertyChanged("NumberPhone"); }
        }
        public List<ListPrivateLevel> _lstDataPrivateLevels;
        public List<ListPrivateLevel> lstDataPrivateLevels
        {
            get { return _lstDataPrivateLevels; }
            set { _lstDataPrivateLevels = value; OnPropertyChanged("lstDataPrivateLevels"); }
        }
        private List<ListPrivateType> _listPrivateTypes;
        public List<ListPrivateType> listPrivateTypes
        {
            get { return _listPrivateTypes; }
            set { _listPrivateTypes = value; OnPropertyChanged("listPrivateTypes"); }
        }
        private List<ListPrivateTime> _listPrivateTimes;
        public List<ListPrivateTime> listPrivateTimes
        {
            get { return _listPrivateTimes; }
            set { _listPrivateTimes = value; OnPropertyChanged("listPrivateTimes"); }
        }
        private List<InfoFile> _lstInfoFileCreateProposing;
        public List<InfoFile> lstInfoFileCreateProposing
        {
            get { return _lstInfoFileCreateProposing; }
            set { _lstInfoFileCreateProposing = value; OnPropertyChanged("lstInfoFileCreateProposing"); }
        }
        
        private List<RevenueList> _listLeverlRevernue;
        public List<RevenueList> listLeverlRevernue
        {
            get { return _listLeverlRevernue; }
            set { _listLeverlRevernue = value; OnPropertyChanged("listLeverlRevernue"); }
        }
        public SettingPropose dataSettingPro { get; set; }
        public List<ListPrivateType> listPrivateType { get; set; }
        public List<ListPrivateTime> listPrivateTime { get; set; }
        public int userNumberConfirm { get; set; }
        public PagePopupGrayColor PagePopupGrayColor { get; set; }
        LoginHome loginHome { get; set; }
        pageCreateNewProposing pageNewProposing { get; set; }
        ProfileUser pageProfileUser { get; set; }
        public ManagerHome(DataUserLogin userCurrent, LoginHome _loginHome)
        {
            InitializeComponent();
            this.DataContext = this;
            UserCurrent = userCurrent;
            loginHome = _loginHome;
            switch (userCurrent.type)
            {
                case 1:
                    UserName = userCurrent.user_info.com_name;
                    NumberPhone = userCurrent.user_info.check_phone;
                    tb_NameUser.Text = userCurrent.user_info.com_name;
                    tb_IdUser.Text = userCurrent.user_info.com_id.ToString();
                    break;
                case 2:
                    UserName = userCurrent.user_info.ep_name;
                    NumberPhone = userCurrent.user_info.ep_phone;
                    tb_NameUser.Text = userCurrent.user_info.ep_name;
                    tb_IdUser.Text = userCurrent.user_info.ep_id.ToString();
                    break;
            }
            StartDynamicText();
            AddFunctionSytem();
            ListFunction lstFunction = new ListFunction(this);
            PageFunction.Content = lstFunction;
            GetListStaffAll();
            GetListShiftAll();
            if (Properties.Settings.Default.Type365 == "2")
            {
                GetListComfirmAndFollow();
                GetListComfirmAndFollow();
            }
            if (Properties.Settings.Default.Type365 != "1")
            {
                GetSettingComfirm();
            }
            SettingApp();
            GetListLevelRevernue();
        }

        private void SettingApp()
        {
            if (Properties.Settings.Default.Theme.Equals(""))
            {
                Properties.Settings.Default.Theme = "GradienBlue";
                Properties.Settings.Default.Save();
                SettingThemeApp("GradienBlue");
            }
            else
            {
                SettingThemeApp(Properties.Settings.Default.Theme);
            }
        }

        private void SettingThemeApp(string theme)
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
                if (pageProfileUser == null)
                {
                    pageProfileUser = new ProfileUser(this, loginHome);
                }
                if (theme.Equals("GradienBlue"))
                {
                    System.Windows.Application.Current.Resources.MergedDictionaries.Add(Olddict1);
                }
                else if (theme.Equals("Green"))
                {
                    System.Windows.Application.Current.Resources.MergedDictionaries.Add(Olddict2);
                }
                else if (theme.Equals("ThemeOrange"))
                {
                    System.Windows.Application.Current.Resources.MergedDictionaries.Add(Olddict3);
                }
                else if (theme.Equals("ThemePinkSingle"))
                {
                    System.Windows.Application.Current.Resources.MergedDictionaries.Add(Olddict4);
                }
            }
            catch (Exception)
            {}
        }

        private Thread textUpdateThread;
        private void StartDynamicText()
        {
            try
            {
                textUpdateThread = new Thread(() =>
                {
                    while (true)
                    {
                        // Thay đổi nội dung của TextBlock
                        Dispatcher.Invoke(() =>
                        {
                            dynamicTextBlock.Text = "" + DateTime.Now.ToString("HH:mm:ss");
                        });

                        // Đợi 1 giây trước khi cập nhật lại
                        Thread.Sleep(1000);
                    }
                });

                textUpdateThread.Start();
            }
            catch (Exception)
            {
            }
        }
        public static BitmapImage ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            try
            {
                var stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Vui lòng chọn đúng định dạng ảnh");
                return null;
            }
        }
        public static long ConvertToEpochTime(string dateString)
        {
            // Define the format of the input date string
            string format = "dd/MM/yyyy";

            // Parse the input date string using the specified format
            DateTime date = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

            // Calculate the number of seconds since Unix epoch (January 1, 1970)
            TimeSpan timeSpan = date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)timeSpan.TotalSeconds;
        }
        public static List<int> GetAllPastDaysInMonth(int month, int year, DateTime selectedDate)
        {
            List<int> pastDays = new List<int>();
            // Tìm ngày đầu tiên và ngày cuối cùng của tháng
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Kiểm tra nếu ngày được chọn không nằm trong tháng được chọn
            if (selectedDate < firstDayOfMonth || selectedDate > lastDayOfMonth)
            {
                throw new ArgumentException("Ngày được chọn không nằm trong tháng được chọn.");
            }
            // Duyệt qua các ngày trong tháng
            for (DateTime date = firstDayOfMonth; date < selectedDate; date = date.AddDays(1))
            {
                // Thêm số ngày vào danh sách
                pastDays.Add(date.Day);
            }
            return pastDays;
        }
        public static List<int> GetAllSaturdaysInMonth(int month, int year)
        {
            List<int> saturdays = new List<int>();
            // Tìm ngày đầu tiên và ngày cuối cùng của tháng
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            // Duyệt qua các ngày trong tháng
            for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
            {
                // Kiểm tra xem ngày đó có phải là thứ Bảy không
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    saturdays.Add(date.Day);
                }
            }
            return saturdays;
        }

        public static List<int> GetAllSundaysInMonth(int month, int year)
        {
            List<int> sundays = new List<int>();
            // Tìm ngày đầu tiên và ngày cuối cùng của tháng
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            // Duyệt qua các ngày trong tháng
            for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
            {
                // Kiểm tra xem ngày đó có phải là Chủ Nhật không
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    sundays.Add(date.Day);
                }
            }
            return sundays;
        }
        public static List<int> GetAllPastDaysInMonth(int month, int year)
        {
            List<int> pastDays = new List<int>();
            // Tìm ngày đầu tiên và ngày cuối cùng của tháng
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            // Lấy ngày hiện tại
            DateTime today = DateTime.Today;
            // Duyệt qua các ngày trong tháng
            for (DateTime date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
            {
                // Kiểm tra xem ngày đó có trước ngày hiện tại không
                if (date < today)
                {
                    pastDays.Add(date.Day);
                }
            }
            return pastDays;
        }
        public void selectionFile(string[] listFile, ListView nameListUi)
        {
            try
            {
                foreach (string file1 in listFile)
                {
                    FileInfo fileInfo = new FileInfo(file1);
                    if (fileInfo.Length <= (1024 * 1024 * 50))
                    {
                        if ((fileInfo.Name.ToUpper().EndsWith(".JPEG") || fileInfo.Name.ToUpper().EndsWith(".JPG") || fileInfo.Name.ToUpper().EndsWith(".PNG") || fileInfo.Name.ToUpper().EndsWith(".GIF")) &&(ConvertByteArrayToBitmapImage(File.ReadAllBytes(fileInfo.FullName)) != null))
                        {
                            InfoFile SetFile = new InfoFile();
                            SetFile.SizeFile = fileInfo.Length;
                            SetFile.NameDisplay = fileInfo.Name;
                            var AvartarPath = Environment.GetEnvironmentVariable("APPDATA") + @"\CompanyManagers\uploadFile\";
                            if (!Directory.Exists(AvartarPath))
                            {
                                Directory.CreateDirectory(AvartarPath);
                            }
                            SetFile.FullName = fileInfo.FullName;
                            SetFile.TypeFile = "sendPhoto";
                            SetFile.Source = ConvertByteArrayToBitmapImage(File.ReadAllBytes(fileInfo.FullName));
                            SetFile.ImageSource = fileInfo.FullName;
                            if (!nameListUi.Items.Contains(SetFile))
                            {
                                nameListUi.Items.Add(SetFile);
                                if (lstInfoFileCreateProposing == null)
                                {
                                    lstInfoFileCreateProposing = new List<InfoFile>();
                                }
                                lstInfoFileCreateProposing.Add(SetFile);
                            }
                        }
                        else
                        {
                            InfoFile SetFile = new InfoFile();
                            SetFile.SizeFile = fileInfo.Length;
                            SetFile.NameDisplay = fileInfo.Name;
                            var AvartarPath = Environment.GetEnvironmentVariable("APPDATA") + @"\CompanyManagers\uploadFile\";
                            if (!Directory.Exists(AvartarPath))
                            {
                                Directory.CreateDirectory(AvartarPath);
                            }
                            SetFile.FullName = fileInfo.FullName;
                            SetFile.TypeFile = "sendFile";
                            SetFile.LinkFile = fileInfo.FullName;
                            if (!nameListUi.Items.Contains(SetFile))
                            {
                                nameListUi.Items.Add(SetFile);
                                if (lstInfoFileCreateProposing == null)
                                {
                                    lstInfoFileCreateProposing = new List<InfoFile>();
                                }
                                lstInfoFileCreateProposing.Add(SetFile);
                            }
                        }
                    }
                    else
                    {
                        PagePopup.NavigationService.Navigate(new PopupNoticationAll(this, "", "Bạn đã chọn tệp có kích thước quá lớn, giới hạn là 50MB", ""));         
                    }
                }
            }
            catch (Exception)
            {}
        }
        public static class HttpClientSingleton
        {
            private static readonly HttpClient httpClient = new HttpClient();
            static HttpClientSingleton()
            {
                // Thiết lập các cấu hình chung cho HttpClient, nếu cần
                httpClient.Timeout = TimeSpan.FromSeconds(30);
            }
            public static HttpClient Instance => httpClient;
        }

        public async void GetListStaffAll()
        {
            try 
            {
                using (WebClient request = new WebClient())
                {
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        Root_StaffAll dataStaffAll = JsonConvert.DeserializeObject<Root_StaffAll>(UnicodeEncoding.UTF8.GetString(e.Result));
                        if (dataStaffAll.data.items != null)
                        {
                            dataListStaffAll = dataStaffAll.data.items;
                            Info_StaffAll dataStaff = new Info_StaffAll();
                            dataStaff.ep_id = -1;
                            dataStaff.ep_name = "Tất cả";
                            dataListStaffAll.Insert(0, dataStaff);
                           
                        }
                    };
                    await request.UploadValuesTaskAsync(UrlApi.Url_Api_Staff + UrlApi.Name_Api_Staff, request.Headers);
                }
            }
            catch (Exception) { }
        }

        public async void GetListShiftAll()
        {
            try
            {
                var client = HttpClientSingleton.Instance;
                var request = new HttpRequestMessage(HttpMethod.Get, UrlApi.Url_Api_Shift + UrlApi.Name_Api_ListShiftAll);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token); 
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var returl = await response.Content.ReadAsStringAsync();
                    Root_ShiftAll dataShiftAll = JsonConvert.DeserializeObject<Root_ShiftAll>(returl);
                    if (dataShiftAll.data.items != null && dataShiftAll.data.items.Count > 0)
                    {
                        dataListShiftAll = dataShiftAll.data.items.ToList();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public async void GetListComfirmAndFollow()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        Root_ComfrimAndFollow dataComfrimAndFollow = JsonConvert.DeserializeObject<Root_ComfrimAndFollow>(UnicodeEncoding.UTF8.GetString(e.Result));
                        if(dataComfrimAndFollow.data != null)
                        {
                            dataListUserComfrim = dataComfrimAndFollow.data.listUsersDuyet.ToList();
                            dataListUserFollow = dataComfrimAndFollow.data.listUsersTheoDoi.ToList();

                        }
                    };
                    await request.UploadValuesTaskAsync(UrlApi.Url_Api_Proposing + UrlApi.Name_Api_ListComfrimFollow, request.Headers);
                }
            }
            catch (Exception)
            {
            }
        }

        public async void GetShiftForDay(string dateShift, string idStaff)
        {
            try
            {
                var client = HttpClientSingleton.Instance;
                var request = new HttpRequestMessage(HttpMethod.Post,UrlApi.Url_Api_Shift + UrlApi.Name_Api_ListShiftForDay);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(dateShift), "day");
                if (Properties.Settings.Default.Type365 == "1")
                {
                    content.Add(new StringContent(idStaff), "ep_id");
                }
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resContent = await response.Content.ReadAsStringAsync();
                    Root_StaffShiftInDay dataStaffShiftInDay = JsonConvert.DeserializeObject<Root_StaffShiftInDay>(resContent);
                    if (dataStaffShiftInDay.list != null)
                    {
                        StaffShiftInDay dataShift = new StaffShiftInDay();
                        dataShift.shift_name = "Cả ngày(tất cả các ca)";
                        dataShift.shift_id = "";
                        dataStaffShiftInDay.list.Insert(0, dataShift);
                        dataListStaffShiftInDay = dataStaffShiftInDay.list.ToList();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        
        public void GetSettingPropose(int catePorpose)
        {
            try
            {
                var data = new
                {
                    dexuat_id = catePorpose
                };
                string jsonData = JsonConvert.SerializeObject(data);
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Headers[HttpRequestHeader.Authorization] = "Bearer " + Properties.Settings.Default.Token;
                    var resData = webClient.UploadString(UrlApi.Url_Api_Proposing + UrlApi.Name_Api_GetSettingPropose, "POST", jsonData);
                    Root_SettingPropose dataSettingPropose = JsonConvert.DeserializeObject<Root_SettingPropose>(resData);
                    if (dataSettingPropose.settingPropose != null)
                    {
                        dataSettingPro = dataSettingPropose.settingPropose;
                        if (dataSettingPro.confirm_level > 0)
                        {
                            userNumberConfirm = dataSettingPro.confirm_level;
                            if (dataSettingPro.confirm_type == 2)
                            {
                                if (lstTypeConfirms.Count == 0)
                                {
                                    lstTypeConfirms.Add(new typeConfirm() { id_Custom = 1, name_Custom = "Duyệt lần lượt" });
                                }
                            }
                            else if (dataSettingPro.confirm_type == 1)
                            {
                                if (lstTypeConfirms.Count == 0)
                                {
                                    lstTypeConfirms.Add(new typeConfirm() { id_Custom = 0, name_Custom = "Duyệt đồng thời" });
                                }
                            }
                            else if (dataSettingPro.confirm_type == 3)
                            {
                                if (lstTypeConfirms.Count < 2)
                                {
                                    lstTypeConfirms.Add(new typeConfirm() { id_Custom = 0, name_Custom = "Duyệt đồng thời" });
                                    lstTypeConfirms.Add(new typeConfirm() { id_Custom = 1, name_Custom = "Duyệt lần lượt" });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        
        public async void GetSettingComfirm()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        Root_SettingComfirm dataSettingComfirm = JsonConvert.DeserializeObject<Root_SettingComfirm>(UnicodeEncoding.UTF8.GetString(e.Result));
                        if (dataSettingComfirm != null)
                        {
                            lstDataPrivateLevels = dataSettingComfirm.settingConfirm.listPrivateLevel;
                            listPrivateTypes = dataSettingComfirm.settingConfirm.listPrivateType;
                            listPrivateTimes = dataSettingComfirm.settingConfirm.listPrivateTime;
                        }
                    };
                    await request.UploadValuesTaskAsync(UrlApi.Url_Api_Proposing + UrlApi.Name_Api_GetSettingComfirm, request.Headers);
                }
            }
            catch (Exception)
            {
            }
        }

        public async void GetListLevelRevernue()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        Root_LevelRevenue dataListLeverlRevernue = JsonConvert.DeserializeObject<Root_LevelRevenue>(UnicodeEncoding.UTF8.GetString(e.Result));
                        if (dataListLeverlRevernue.data != null)
                        {
                            listLeverlRevernue = dataListLeverlRevernue.data.danhthuList;
                        }
                    };
                    await request.UploadValuesTaskAsync(UrlApi.Url_Api_Rose + UrlApi.Name_Api_ListLeverlRevernue, request.Headers);
                }
            }
            catch (Exception)
            {
            }
        } 

        public void AddFunctionSytem()
        {
            try
            {
                dataFunctionHome = new List<DataFunction>();
                if (Properties.Settings.Default.Type365 == "1")
                {
                    dataFunctionHome.Add(new DataFunction()
                    {
                        idFunction = 1,
                        nameFunction = "Quản lý công ty",
                        colorFunction1 = "#FFA13B",
                        colorFunction2 = "#E8811A",
                        dataChildFunction = new List<DataChildFunction> 
                        { 
                            new DataChildFunction { idChildFunction = 1, nameChildFunction = "Thiết lập cơ cấu tổ chức" },
                            new DataChildFunction { idChildFunction = 2, nameChildFunction = "Thiết lập vị trí"},
                            new DataChildFunction { idChildFunction = 3, nameChildFunction = "Quản lý nhân viên"},
                            new DataChildFunction { idChildFunction = 2, nameChildFunction = "Thiết lập cơ cấu tổ chức"}
                        }
                    });
                    dataFunctionHome.Add(new DataFunction() 
                    { 
                        idFunction = 2, 
                        nameFunction = "Chấm công", 
                        colorFunction1 = "#97C25F", 
                        colorFunction2 = "#7DA047", 
                        dataChildFunction = new List<DataChildFunction> 
                        { 
                            new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Cấu hình chấm công"},
                            new DataChildFunction() { idChildFunction = 2, nameChildFunction = "Quản lý ca làm việc"},
                            new DataChildFunction() { idChildFunction = 3, nameChildFunction = "Quản lý Lịch làm việc"},
                            new DataChildFunction() { idChildFunction = 4, nameChildFunction = "Xuất công"},
                            new DataChildFunction() { idChildFunction = 5, nameChildFunction = "Lịch sử điểm danh"}
                        } 
                    });
                    dataFunctionHome.Add(new DataFunction() 
                    { 
                        idFunction = 3, 
                        nameFunction = "Cài đặt lương", 
                        colorFunction1 = "#8069FF", 
                        colorFunction2 = "#5E53C9",
                        dataChildFunction = new List<DataChildFunction> 
                        {
                            new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Cài đặt lương cơ bản"},
                            new DataChildFunction() { idChildFunction = 2, nameChildFunction = "Cài đặt đi muộn - về sớm"},
                            new DataChildFunction() { idChildFunction = 3, nameChildFunction = "Cài đặt nghỉ phép"},
                            new DataChildFunction() { idChildFunction = 4, nameChildFunction = "Cài đặt bảo hiểm"},
                            new DataChildFunction() { idChildFunction = 5, nameChildFunction = "Cài đặt phụ cấp"},
                            new DataChildFunction() { idChildFunction = 6, nameChildFunction = "Cài đặt thuế"},
                            new DataChildFunction() { idChildFunction = 7, nameChildFunction = "Cộng công"},
                            new DataChildFunction() { idChildFunction = 8, nameChildFunction = "Thưởng phạt"},
                            new DataChildFunction() { idChildFunction = 9, nameChildFunction = "Hoa hồng"}
                        } 
                    });
                    dataFunctionHome.Add(new DataFunction() 
                    { 
                        idFunction = 4, 
                        nameFunction = "Cài đặt đề xuất",
                        colorFunction1 = "#FF5B4D", colorFunction2 = "#C1403A", 
                        dataChildFunction = new List<DataChildFunction> 
                        {  
                            new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Cài đặt đề xuất"}
                        } 
                    });
                }   
                else
                {   
                    dataFunctionHome.Add(new DataFunction() 
                    { 
                        idFunction = 1, 
                        nameFunction = "Tạo đề xuất", 
                        colorFunction1 = "#FFA13B", 
                        colorFunction2 = "#E8811A",
                        dataChildFunction = new List<DataChildFunction> 
                        {  
                            new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Loại đề xuất"}
                        } 
                    });
                    dataFunctionHome.Add(new DataFunction() 
                    { 
                        idFunction = 2, 
                        nameFunction = "Lịch sử", 
                        colorFunction1 = "#97C25F", 
                        colorFunction2 = "#7DA047",
                        dataChildFunction = new List<DataChildFunction> 
                        {  
                            new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Chấm công"},
                            new DataChildFunction() { idChildFunction = 2, nameChildFunction = "Lương hiện tại"}
                        }  
                    });
                    dataFunctionHome.Add(new DataFunction() 
                    { 
                        idFunction = 3, 
                        nameFunction = "Tính lương", 
                        colorFunction1 = "#FF5B4D", 
                        colorFunction2 = "#C1403A",
                        dataChildFunction = new List<DataChildFunction> 
                        {  
                            new DataChildFunction() { idChildFunction = 1, nameChildFunction = "Xem bảng công"},
                            new DataChildFunction() { idChildFunction = 2, nameChildFunction = "Xem bảng lương"},
                            new DataChildFunction() { idChildFunction = 3, nameChildFunction = "Xem lịch làm việc"}
                        }
                    }); 
                }
            }
            catch (Exception) { }
        }
        private void Minimimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void NormalSize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsFull = 0;
        }

        private void Maximimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsFull = 1;
        }

        private void CloseWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            this.Close();
        }

        private void ShowOutPut(object sender, MouseButtonEventArgs e)
        {
            try
            {
                PagePopupGrayColor = new PagePopupGrayColor(this);
                PagePopupGrayColor.Popup1.NavigationService.Navigate(new ProfileUser(this, loginHome));
                PagePopup.NavigationService.Navigate(PagePopupGrayColor);
            }
            catch (Exception)
            {
            }
        }

        private void AdddscrollMain(object sender, MouseWheelEventArgs e)
        {
            scrollMain.ScrollToVerticalOffset(scrollMain.VerticalOffset - e.Delta);
        }


        private void BackToBack(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (backToBack == "BackToManagerHome")
                {
                    ListFunction lstFunction = new ListFunction(this);
                    PageFunction.Content = lstFunction;
                }
                else if(backToBack == "BackToProposingHome")
                {
                    ProposingHome proposingHome = new ProposingHome(this);
                    PageFunction.Content = proposingHome;
                    backToBack = "BackToManagerHome";
                }
            }
            catch (Exception)
            {
            }
        }

        private void pageTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
