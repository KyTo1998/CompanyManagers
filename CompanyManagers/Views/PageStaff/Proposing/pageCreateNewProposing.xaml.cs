using CompanyManagers.Common.Popups;
using CompanyManagers.Controllers;
using CompanyManagers.Models.ModelsAll;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Models.ModelsShift;
using CompanyManagers.Views.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static CompanyManagers.Views.Home.ManagerHome;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Globalization;
using CompanyManagers.Models.ModelRose;


namespace CompanyManagers.Views.PageStaff.Proposing
{
    /// <summary>
    /// Interaction logic for pageCreateNewProposing.xaml
    /// </summary>
    public partial class pageCreateNewProposing : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        PagePopupGrayColor pagePopupGrayColor { get; set; }

        public class JsonOnLeave
        {
            public string nameShif {  get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public List<object> nghi_phep { get; set; }
        }
        public class lstJsonOnLeave
        {
            public List<List<object>> nghi_phep { get; set; } = new List<List<object>>();
        }
        private List<JsonOnLeave> _listShiftSelect;
        public List<JsonOnLeave> listShiftSelect
        {
            get { return _listShiftSelect; }
            set { _listShiftSelect = value; OnPropertyChanged("listShiftSelect"); }
        }

        private List<LanhDaoDuyet> _dataListUserComfrim;
        public List<LanhDaoDuyet> dataListUserComfrim
        {
            get { return _dataListUserComfrim; }
            set { _dataListUserComfrim = value; OnPropertyChanged("dataListUserComfrim"); }
        }

        private int _typeCategoryProposing;
        public int typeCategoryProposing
        {
            get { return _typeCategoryProposing; }
            set { _typeCategoryProposing = value;OnPropertyChanged("typeCategoryProposing");}
        }
        private bool _statusValidate;
        public bool statusValidate
        {
            get { return _statusValidate; }
            set { _statusValidate = value; OnPropertyChanged("statusValidate"); }
        }
        private string _stringValidateKey;
        public string stringValidateKey
        {
            get { return _stringValidateKey; }
            set { _stringValidateKey = value; OnPropertyChanged("stringValidateKey"); }
        }
        ManagerHome managerHome { set; get; }
        int typePlan {  get; set; }
        Result_CategoryProposing dataCategoryProposing;
        Detail_Proposet detailPropose;
        public pageCreateNewProposing(ManagerHome _managerHome, Result_CategoryProposing _dataCategoryProposing, Detail_Proposet _detailPropose, string _categoryProposeName)
        {
            InitializeComponent();
            managerHome = _managerHome;
            dataCategoryProposing = _dataCategoryProposing;
            txbBackToBack.Text = _dataCategoryProposing.name_cate_dx;
            typeCategoryProposing = _dataCategoryProposing.cate_dx;
            tb_UserNameCreate.Text = _managerHome.UserCurrent.user_info.ep_name;
            tb_CategoryProposingCreate.Text = _dataCategoryProposing.name_cate_dx;
            SelectUserComfirm.ItemsSource = _managerHome.dataListUserComfrim.ToList();
            SelectUserFollow.ItemsSource = _managerHome.dataListUserFollow.ToList();
            SelectTypeComfirm.ItemsSourceSelected = _managerHome.lstTypeConfirms.ToList();
            SelectLeverRevenue.ItemsSourceSelected = _managerHome.listLeverlRevernue.ToList();
        }
        
        #region Nghỉ Phép
        public async void CreateProposingOnLeave()
        {
            try
            {
                if (cbSelectYesPlan.IsChecked == false && cbSelectNoPlan.IsChecked == false)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    stringValidateKey = "TypePropose";
                    tb_ValidateProposing.Text = "Chưa chọn loại đề xuất";
                    statusValidate = false;
                }
                else if (StartDateOnLeave.SelectedDate == null || EndDateOnLeave.SelectedDate == null)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    stringValidateKey = "StartOrEndDatePropose";
                    tb_ValidateProposing.Text = "Chưa chọn ngày bắt đầu hoặc kết thúc";
                    statusValidate = false;
                }
                else if (ShiftOnLeave.SelectedIndexSelected < 0)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    stringValidateKey = "ShiftOnLeave";
                    tb_ValidateProposing.Text = "Chưa chọn ca nghỉ";
                    statusValidate = false;
                }
                else
                {
                    statusValidate = true;
                    var data = new lstJsonOnLeave();
                    foreach (var item in listShiftSelect)
                    {
                        data.nghi_phep.Add(item.nghi_phep);
                    }
                    string jsonString = JsonConvert.SerializeObject(data);

                    List<string> listUserConfirm = new List<string>();
                    foreach (var item in dataListUserComfrim)
                    {
                        listUserConfirm.Add(item.idQLC.ToString());
                    }
                    string userConfirm = String.Join(",", listUserConfirm);
                    var client = HttpClientSingleton.Instance;
                    var request = new HttpRequestMessage(HttpMethod.Post,UrlApi.Url_Api_Proposing + UrlApi.Name_Api_CreateProposingOnLeave);
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(jsonString), "noi_dung");
                    content.Add(new StringContent(tb_InputNameProposing.Text), "name_dx");
                    content.Add(new StringContent(((typeConfirm)SelectTypeComfirm.SelectedItemSelected).id_Custom.ToString()), "kieu_duyet");
                    content.Add(new StringContent(typePlan.ToString()), "loai_np");
                    content.Add(new StringContent(tb_InputReasonCreateProposing.Text), "ly_do");
                    content.Add(new StringContent(userConfirm), "id_user_duyet");
                    content.Add(new StringContent(((ListUsersTheoDoi)SelectUserFollow.SelectedItem).idQLC.ToString()), "id_user_theo_doi");
                    int numberFile = 0;
                    if (managerHome.lstInfoFileCreateProposing != null)
                    {
                        foreach (var item in managerHome.lstInfoFileCreateProposing)
                        {
                            content.Add(new StreamContent(File.OpenRead(item.FullName)), $"fileKem[{numberFile}]", item.FullName);
                            numberFile++;
                        }
                    }
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        dataListUserComfrim.Clear();
                        listShiftSelect.Clear();
                        managerHome.PagePopup.NavigationService.Navigate(new PopupNoticationAll(managerHome, "", "Tạo đề xuất xin nghỉ phép thành công", ""));
                    }
                }
            }
            catch (System.Exception)
            {
                managerHome.PagePopup.NavigationService.Navigate(new PopupNoticationAll(managerHome, "", "Tạo đề xuất xin nghỉ phép thất bại, vui lòng thử lại sau ít phút", ""));
            }
        }
       
        private void ClickShiftOnLeave(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                StaffShiftInDay dataShift = (StaffShiftInDay)ShiftOnLeave.SelectedItemSelected;
                if (dataShift != null)
                {
                    JsonOnLeave OnLeave = new JsonOnLeave();
                    OnLeave.nameShif = dataShift.shift_name;
                    OnLeave.startDate = StartDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd");
                    OnLeave.endDate = EndDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd");
                    OnLeave.nghi_phep = new List<object> { $"{StartDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd")}", $"{EndDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd")}", dataShift.shift_id };
                    if (listShiftSelect == null) listShiftSelect = new List<JsonOnLeave>();
                    listShiftSelect.Add(OnLeave);
                    listShiftSelect = listShiftSelect.ToList();
                    if (listShiftSelect.Count > 0)
                    {
                        lsvListShifForDay.Visibility = Visibility.Visible;
                        stringValidateKey = "null";
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void SelectYesPlan(object sender, RoutedEventArgs e)
        {
            cbSelectNoPlan.IsChecked = false;
            typePlan = 1;
        }

        private void SelectNoPlan(object sender, RoutedEventArgs e)
        {
            cbSelectYesPlan.IsChecked= false;
            typePlan = 2;
        }

        private void DeleteShiftOnLeave(object sender, MouseButtonEventArgs e)
        {
            try
            {
                JsonOnLeave dataShiftOnLeave = (JsonOnLeave)(sender as Border).DataContext;
                if (dataShiftOnLeave != null)
                {
                    listShiftSelect.Remove(dataShiftOnLeave);
                    listShiftSelect = listShiftSelect.ToList();
                    if (listShiftSelect.Count == 0)
                    {
                        lsvListShifForDay.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void LoadNumRowShifForDay(object sender, DataGridRowEventArgs e)
        {
             e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void SelectedStartDateOnLeave(object sender, SelectionChangedEventArgs e)
        {
            EndDateOnLeave.IsEnabled = true;
            managerHome.GetShiftForDay(StartDateOnLeave.SelectedDate.Value.ToString("yyyy-MM-dd"), managerHome.UserCurrent.user_info.ep_id.ToString());
        }
        private void SelectedEndDateOnLeave(object sender, SelectionChangedEventArgs e)
        {
            ShiftOnLeave.ItemsSourceSelected = managerHome.dataListStaffShiftInDay;
        }
        private void ClickSelectTypeComfirm(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region Hoa hồng doanh thu
        public async void CreateProposingRoseRevenue()
        {
            try
            {
                if (tb_InputMonneyRoseRevenue.Text == "")
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    stringValidateKey = "MonneyRoseRevenue";
                    tb_ValidateProposing.Text = "Chưa nhập doanh thu";
                    statusValidate = false;
                }
                else if (StartDateRoseRevenuePeriod.SelectedDate == null)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    stringValidateKey = "DateRoseRevenuePeriod";
                    tb_ValidateProposing.Text = "Chưa chọn chu kỳ doanh thu";
                    statusValidate = false;
                }
                else if (SelectLeverRevenue.SelectedIndexSelected < 0)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    stringValidateKey = "LeverRevenue";
                    tb_ValidateProposing.Text = "Chưa chọn mức doanh thu";
                    statusValidate = false;
                }
                else
                {
                    List<string> listUserConfirm = new List<string>();
                    foreach (var item in dataListUserComfrim)
                    {
                        listUserConfirm.Add(item.idQLC.ToString());
                    }
                    string userConfirm = String.Join(",", listUserConfirm);

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, UrlApi.Url_Api_Rose + UrlApi.Name_Api_AddProposingRose);
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(managerHome.UserCurrent.user_info.ep_name), "name_user");
                    content.Add(new StringContent(StartDateRoseRevenuePeriod.SelectedDate.Value.ToString("yyyy-MM-dd")), "item_mdt_date");
                    content.Add(new StringContent(StartDateRoseRevenuePeriod.SelectedDate.Value.ToString("yyyy-MM")), "chu_ky");
                    content.Add(new StringContent(((RevenueList)SelectLeverRevenue.SelectedItemSelected).tl_id.ToString()), "name_dt");
                    content.Add(new StringContent(tb_InputNameProposing.Text), "name_dx");
                    content.Add(new StringContent(((typeConfirm)SelectTypeComfirm.SelectedItemSelected).id_Custom.ToString()), "kieu_duyet");
                    content.Add(new StringContent(tb_InputMonneyRoseRevenue.Text.Replace(".", "")), "dt_money");
                    content.Add(new StringContent(tb_InputReasonCreateProposing.Text), "ly_do");
                    content.Add(new StringContent(userConfirm), "id_user_duyet");
                    content.Add(new StringContent(((ListUsersTheoDoi)SelectUserFollow.SelectedItem).idQLC.ToString()), "id_user_theo_doi");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        dataListUserComfrim.Clear();
                        managerHome.PagePopup.NavigationService.Navigate(new PopupNoticationAll(managerHome, "", "Tạo đề xuất hoa hồng thành công", ""));
                    }
                }
            }
            catch (Exception)
            {
                managerHome.PagePopup.NavigationService.Navigate(new PopupNoticationAll(managerHome, "", "Tạo đề xuất hoa hồng thất bại, vui lòng thử lại sau ít phút", ""));
            }
        }
        private void ConverToViVnMonney(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.TextChanged -= ConverToViVnMonney;
            if (decimal.TryParse(textBox.Text.Replace(".", ""), out decimal result))
            {
                textBox.Text = FormatNumber(result);
                textBox.SelectionStart = textBox.Text.Length;
            }
            textBox.TextChanged += ConverToViVnMonney;
        }
        private string FormatNumber(decimal value)
        {
            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberGroupSeparator = ".",
                NumberDecimalDigits = 0,
                NumberDecimalSeparator = ","
            };
            return value.ToString("N", nfi);
        }
        private void CheckInputNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void SelectedStartDateRoseRevenuePeriod(object sender, SelectionChangedEventArgs e)
        {

        }
        private void SelectedStartDateRoseRevenueAPointInTime(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ClickSelectLeverRevenue(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region Cộng công
        public async void CreateProposingCongCong()
        {
            try
            {
                if (StartDateComfirmCongCong.SelectedDate == null)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    stringValidateKey = "DateComfirmCongCong";
                    tb_ValidateProposing.Text = "Chưa chọn ngày xác nhận công";
                    statusValidate = false;
                }
                else if (ShiftWorkCongCong.SelectedIndexSelected == -1)
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    stringValidateKey = "ShiftWorkCongCong";
                    tb_ValidateProposing.Text = "Chưa chọn ca xác nhận công";
                    statusValidate = false;
                }
                else if (InputTimeShift.txtH.Text == "" || InputTimeShift.txtM.Text == "" || InputTimeShift.txtC.Text == "")
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    stringValidateKey = "InputTimeShift";
                    tb_ValidateProposing.Text = "Chưa nhập đầy đủ giờ vào ca";
                    statusValidate = false;
                } 
                else if (OutputTimeShift.txtH.Text == "" || OutputTimeShift.txtM.Text == "" || OutputTimeShift.txtC.Text == "")
                {
                    tb_ValidateProposing.Visibility = Visibility.Visible;
                    stringValidateKey = "OutputTimeShift";
                    tb_ValidateProposing.Text = "Chưa nhập đầy đủ giờ hết ca";
                    statusValidate = false;
                }
                else
                {
                    long StartDateFormat = ConvertToEpochTime(StartDateComfirmCongCong.SelectedDate.Value.ToString("dd/MM/yyyy")) * 1000;
                    List<string> listUserConfirm = new List<string>();
                    foreach (var item in dataListUserComfrim)
                    {
                        listUserConfirm.Add(item.idQLC.ToString());
                    }
                    string userConfirm = String.Join(",", listUserConfirm);
                    var InputTimeShiftSplit = InputTimeShift.Time.Split(':');
                    var OutputTimeShiftSplit = OutputTimeShift.Time.Split(':');
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, UrlApi.Url_Api_Rose + UrlApi.Name_Api_CreateProposeCongCong);
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(managerHome.UserCurrent.user_info.ep_name), "name_user");
                    content.Add(new StringContent(tb_InputNameProposing.Text), "name_dx");
                    content.Add(new StringContent(((typeConfirm)SelectTypeComfirm.SelectedItemSelected).id_Custom.ToString()), "kieu_duyet");
                    content.Add(new StringContent(tb_InputReasonCreateProposing.Text), "ly_do");
                    content.Add(new StringContent(userConfirm), "id_user_duyet");
                    content.Add(new StringContent(((ListUsersTheoDoi)SelectUserFollow.SelectedItem).idQLC.ToString()), "id_user_theo_doi");
                    int numberFile = 0;
                    if (managerHome.lstInfoFileCreateProposing != null)
                    {
                        foreach (var item in managerHome.lstInfoFileCreateProposing)
                        {
                            content.Add(new StreamContent(File.OpenRead(item.FullName)), $"fileKem[{numberFile}]", item.FullName);
                            numberFile++;
                        }
                    }
                    content.Add(new StringContent(StartDateFormat.ToString()), "time_xnc");
                    content.Add(new StringContent(((StaffShiftInDay)ShiftWorkCongCong.SelectedItemSelected).shift_id.ToString()), "id_ca_xnc");
                    content.Add(new StringContent(((StaffShiftInDay)ShiftWorkCongCong.SelectedItemSelected).shift_name.ToString()), "ca_xnc");
                    content.Add(new StringContent($"{InputTimeShiftSplit[0].ToString()}:{InputTimeShiftSplit[1].ToString()}"), "time_vao_ca");
                    content.Add(new StringContent($"{OutputTimeShiftSplit[0].ToString()}:{OutputTimeShiftSplit[1].ToString()}"), "time_het_ca");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        dataListUserComfrim.Clear();
                        managerHome.PagePopup.NavigationService.Navigate(new PopupNoticationAll(managerHome, "", "Tạo đề xuất cộng công thành công", ""));
                    }
                }
            }
            catch (Exception)
            {
                managerHome.PagePopup.NavigationService.Navigate(new PopupNoticationAll(managerHome, "", "Tạo đề xuất cộng công thất bại, vui lòng thử lại sau ít phút", ""));
            }
        }
        private void SelectedStartDateComfirmCongCong(object sender, SelectionChangedEventArgs e)
        {
            ShiftWorkCongCong.IsEnabled = true;
            managerHome.GetShiftForDay(StartDateComfirmCongCong.SelectedDate.Value.ToString("yyyy-MM-dd"), managerHome.UserCurrent.user_info.ep_id.ToString());
            managerHome.dataListStaffShiftInDay.RemoveAt(0);
            ShiftWorkCongCong.ItemsSourceSelected = managerHome.dataListStaffShiftInDay.ToList();
        } 
        private void ClickShiftWorkCongCong(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Commons
        private static bool IsTextAllowed(string text)
        {
            // Chỉ cho phép các ký tự số
            Regex regex = new Regex("[^0-9]+"); // Chỉ các số
            return !regex.IsMatch(text);
        }
        private void SelectionChangeUserComfirm(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SelectUserComfirm.Text != null)
                {
                    LanhDaoDuyet dataUserComfirm = new LanhDaoDuyet();
                    dataUserComfirm = SelectUserComfirm.SelectedItem as LanhDaoDuyet;
                    lsvUserComfirmSelected.Visibility = Visibility.Visible;
                    if (dataListUserComfrim == null)
                    {
                        dataListUserComfrim = new List<LanhDaoDuyet>();
                    }
                    if (!dataListUserComfrim.Contains(dataUserComfirm))
                    {
                        dataListUserComfrim.Add(dataUserComfirm);
                        if (dataListUserComfrim.Count > 1)
                        {
                            btnDeleteUserConfirmAll.Visibility = Visibility.Visible;
                        }
                    }
                    dataListUserComfrim = dataListUserComfrim.ToList();
                    SelectUserComfirm.PlaceHolder = null;
                    SelectUserComfirm.Text = null;
                    SelectUserComfirm.VerticalAlignment = VerticalAlignment.Center;
                }
            }
            catch (Exception)
            {
            }
        }
        private void DeleteUserConfirm(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LanhDaoDuyet dataUserComfirm = (LanhDaoDuyet)(sender as Border).DataContext;
                if (dataUserComfirm != null)
                {
                    dataListUserComfrim.Remove(dataUserComfirm);
                    if (dataListUserComfrim.Count == 0)
                    {
                        lsvUserComfirmSelected.Visibility = Visibility.Collapsed;
                        SelectUserComfirm.PlaceHolder = "Chọn người duyệt";
                    }
                    if (dataListUserComfrim.Count == 1)
                    {
                        btnDeleteUserConfirmAll.Visibility = Visibility.Collapsed;
                    }
                    dataListUserComfrim = dataListUserComfrim.ToList();
                }
            }
            catch (Exception)
            {
            }
        }
        private void DeleteUserConfirmAll(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dataListUserComfrim.Count > 0)
                {
                    dataListUserComfrim.Clear();
                    btnDeleteUserConfirmAll.Visibility = Visibility.Collapsed;
                    lsvUserComfirmSelected.Visibility = Visibility.Collapsed;
                    SelectUserComfirm.PlaceHolder = "Chọn người duyệt";
                    dataListUserComfrim = dataListUserComfrim.ToList();
                }
            }
            catch (Exception)
            {
            }
        }

        private void ClosePopupCreateProposing(object sender, MouseButtonEventArgs e)
        {
            try
            {
                pagePopupGrayColor = new PagePopupGrayColor(managerHome);
                pagePopupGrayColor.Popup.NavigationService.Navigate(null);
                managerHome.PagePopup.NavigationService.Navigate(null);
            }
            catch (Exception)
            {
            }
        }

        private void ClickCancel(object sender, MouseButtonEventArgs e)
        {
            try
            {
                pagePopupGrayColor = new PagePopupGrayColor(managerHome);
                pagePopupGrayColor.Popup.NavigationService.Navigate(null);
                managerHome.PagePopup.NavigationService.Navigate(null);
            }
            catch (Exception)
            {
            }
        }
        private void CheckValidateProposing()
        {
            try
            {
                if (string.IsNullOrEmpty(tb_InputNameProposing.Text))
                {
               
                        tb_ValidateProposing.Visibility = Visibility.Visible;
                        stringValidateKey = "NamePropose";
                        tb_ValidateProposing.Text = "Tên đề xuất không được để trống";
                        statusValidate = false;
                   
                    
                }
                else if (SelectTypeComfirm.SelectedIndexSelected < 0)
                {
                   
                        tb_ValidateProposing.Visibility = Visibility.Visible;
                        stringValidateKey = "TypeComfirmPropose";
                        tb_ValidateProposing.Text = "Chưa chọn kiểu duyệt";
                        statusValidate = false;
                   
                    
                }
                else if (string.IsNullOrEmpty(tb_InputReasonCreateProposing.Text))
                {
                   
                        tb_ValidateProposing.Visibility = Visibility.Visible;
                        stringValidateKey = "ReasonPropose";
                        tb_ValidateProposing.Text = "Lý do không được để trống";
                        statusValidate = false;
                   
                }
                else if (SelectUserFollow.SelectedIndex < 0)
                {
                    
                        tb_ValidateProposing.Visibility = Visibility.Visible;
                        stringValidateKey = "UserFollowPropose";
                        tb_ValidateProposing.Text = "Chưa chọn người theo dõi";
                        statusValidate = false;
                    
                }
                else if (dataListUserComfrim == null || dataListUserComfrim.Count == 0)
                {
                        tb_ValidateProposing.Visibility = Visibility.Visible;
                        stringValidateKey = "UserComfrimPropose";
                        tb_ValidateProposing.Text = $"chưa chọn người duyệt, số người duyệt là {managerHome.userNumberConfirm}";
                        statusValidate = false;
                   
                }
                else
                {
                    statusValidate = true;
                }

            }
            catch (System.Exception)
            {
            }
        }

        private void ClickCreateProposing(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CheckValidateProposing();
                if (statusValidate)
                {
                    switch (dataCategoryProposing.cate_dx)
                    {
                        case 1:
                            CreateProposingOnLeave();
                            break; 
                        case 20:
                            CreateProposingRoseRevenue();
                            break;
                        case 17:
                            CreateProposingCongCong();
                            break;
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }

        private void AddFileGimForProposing(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OpenFileDialog selectFile = new OpenFileDialog();
                selectFile.Filter = "All files (*.*)|*.*";
                selectFile.Multiselect = true;
                if (selectFile.ShowDialog() == true)
                {
                    managerHome.selectionFile(selectFile.FileNames, lsvFileGim);
                    lsvFileGim.Visibility = Visibility.Visible;
                    TextHidenFileGim.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
            }
        }

        private void DeleteFileGim(object sender, MouseButtonEventArgs e)
        {
            try
            {
                InfoFile infoFile = (InfoFile)(sender as Border).DataContext;
                if (infoFile != null)
                {
                    lsvFileGim.Items.Remove(infoFile);
                    lsvFileGim.Items.Refresh();
                    TextHidenFileGim.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            {
            }
        }
        private void ScollCreateProposing(object sender, MouseWheelEventArgs e)
        {
            scoll.ScrollToVerticalOffset(scoll.VerticalOffset - e.Delta);
        }
        private void SelectionChangeUserFollow(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion
    }
}
