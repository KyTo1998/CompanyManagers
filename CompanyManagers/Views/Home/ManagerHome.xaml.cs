﻿using CompanyManagers.Common.Popups;
using CompanyManagers.Controllers;
using CompanyManagers.Models.HomeFunction;
using CompanyManagers.Models.Logins;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Models.ModelsShift;
using CompanyManagers.Views.Functions.HomeFunction;
using CompanyManagers.Views.Login;
using CompanyManagers.Views.Logout;
using CompanyManagers.Views.PageStaff.Proposing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

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
        private string _backToBack;
        public string backToBack 
        {
            get { return _backToBack; }
            set { _backToBack = value; OnPropertyChanged("backToBack");}
        }
         public PagePopupGrayColor PagePopupGrayColor { get; set; }
        LoginHome loginHome { get; set; }
        pageCreateNewProposing pageNewProposing { get; set; }
        public ManagerHome(DataUserLogin userCurrent, LoginHome _loginHome)
        {
            InitializeComponent();
            this.DataContext = this;
            UserCurrent = userCurrent;
            loginHome = _loginHome;
            switch (userCurrent.type)
            {
                case 1:
                    tb_NameUser.Text = userCurrent.user_info.com_name;
                    tb_IdUser.Text = userCurrent.user_info.com_id.ToString();
                    break;
                case 2:
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
                    await request.UploadValuesTaskAsync(UrlApi.apiListStaffAll, request.Headers);
                }
            }
            catch (Exception) { }
        }

        public async void GetListShiftAll()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, UrlApi.apiListShiftAll);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token); 
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
                    await request.UploadValuesTaskAsync(UrlApi.apiListComfrimFollow, request.Headers);
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
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, UrlApi.apiListShiftForDay);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
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
            if (bor_DetailAcount.Visibility == Visibility.Collapsed)
            {
                bor_DetailAcount.Visibility = Visibility.Visible;
            }
            else
            {
                bor_DetailAcount.Visibility = Visibility.Collapsed;
            }
        }

        private void AdddscrollMain(object sender, MouseWheelEventArgs e)
        {
            scrollMain.ScrollToVerticalOffset(scrollMain.VerticalOffset - e.Delta);
        }

        private void ShowPopupLogout(object sender, MouseButtonEventArgs e)
        {
            try
            {
                PagePopupGrayColor = new PagePopupGrayColor(this);
                PagePopupGrayColor.Popup1.NavigationService.Navigate(new PageLogout(this, loginHome));
                PagePopup.NavigationService.Navigate(PagePopupGrayColor);
                bor_DetailAcount.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
            } 
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
