using CompanyManagers.Controllers;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using static CompanyManagers.Common.Tool.DatePicker;
using static CompanyManagers.Views.Home.ManagerHome;


namespace CompanyManagers.Views.PageStaff.Proposing
{
    /// <summary>
    /// Interaction logic for Proposing.xaml
    /// </summary>
    public partial class ProposingHome : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private List<InforDx_Proposing> _listProposingHome;
        public List<InforDx_Proposing> listProposingHome
        {
            get { return _listProposingHome; }
            set { _listProposingHome = value; OnPropertyChanged("listProposingHome"); }
        } 

        private List<Result_CategoryProposing> _listCategoryProposingHome;
        public List<Result_CategoryProposing> listCategoyProposingHome
        {
            get { return _listCategoryProposingHome; }
            set { _listCategoryProposingHome = value; OnPropertyChanged("listCategoyProposingHome"); }
        }
        ManagerHome managerHome;
        public ProposingHome(ManagerHome _managerHome)
        {
            InitializeComponent();
            managerHome = _managerHome;
            tb_TitelTime.Text = $"Hôm Nay, ngày {DateTime.Now.ToString("dd/MM/yyyy")}";
            GetCategoryProposing();
        }

        public async void GetProposingShowHome()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        try
                        {
                            Root_Proposing dataProposing = JsonConvert.DeserializeObject<Root_Proposing>(UnicodeEncoding.UTF8.GetString(e.Result));
                            if (dataProposing != null)
                            {
                                tb_CountSumProposing.Text = dataProposing.data.totaldx.ToString();
                                tb_CountWaitProposing.Text = dataProposing.data.dxChoDuyet.ToString();
                                tb_CountApprovedProposing.Text = dataProposing.data.dxduyet.ToString();
                                tb_CountNecessaryProposing.Text = dataProposing.data.dxCanduyet.ToString();
                                listProposingHome = dataProposing.data.data.ToList();
                                foreach (var itemProposingHome in listProposingHome)
                                {
                                    foreach (var itemCategoyProposing in listCategoyProposingHome)
                                    {
                                        if (itemProposingHome.type_dx == itemCategoyProposing.cate_dx)
                                        {
                                            itemProposingHome.type_dx_string = itemCategoyProposing.name_cate_dx;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    };
                    await request.UploadValuesTaskAsync(UrlApi.Url_Api_Proposing + UrlApi.Name_Api_ShowHomeProposing, request.Headers);
                }
            }
            catch (Exception)
            {
            }
        }

        int numberPage = 1;
        int TotalPages;

        public async void GetCategoryProposing()
        {
            try
            {
                do
                {
                    LoadingSpinner.Visibility= System.Windows.Visibility.Visible;
                    var client = HttpClientSingleton.Instance;
                    var request = new HttpRequestMessage(HttpMethod.Post, UrlApi.Url_Api_Proposing + UrlApi.Name_Api_CategoryProposing);
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(numberPage.ToString()), "page");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var resConten = await response.Content.ReadAsStringAsync();
                        Root_CategoryProposing dataCategoryProposing = JsonConvert.DeserializeObject<Root_CategoryProposing>(resConten);
                        if (dataCategoryProposing.data != null)
                        {
                            TotalPages = dataCategoryProposing.data.totalPages;
                            if (listCategoyProposingHome == null)
                            {
                                listCategoyProposingHome = new List<Result_CategoryProposing>();
                            }
                            foreach (var item in dataCategoryProposing.data.result)
                            {
                                listCategoyProposingHome.Add(item);
                            }
                            numberPage++;
                        }
                    }
                } while (numberPage <= TotalPages);
                LoadingSpinner.Visibility= System.Windows.Visibility.Collapsed;
                GetProposingShowHome();
            }
            catch (Exception)
            {
            }
        }

        private void ShowListCategoryProposing(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListCategoryProposing listProposing = new ListCategoryProposing(managerHome, listCategoyProposingHome);
                managerHome.PageFunction.Content = listProposing;
                managerHome.backToBack = "BackToProposingHome";
            }
            catch (Exception)
            {
            }
        }

        private void ShowProposingIsMe(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListProposingMine listProposingMine = new ListProposingMine(managerHome, listCategoyProposingHome,"AllToHome");
                managerHome.PageFunction.Content = listProposingMine;
                managerHome.backToBack = "BackToProposingHome";
            }
            catch (Exception)
            {
            }
        }

        private void ProposeWaitComfirm(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListProposingMine listProposingMine = new ListProposingMine(managerHome, listCategoyProposingHome, "WaitComfirmToHome");
                managerHome.PageFunction.Content = listProposingMine;
                managerHome.backToBack = "BackToProposingHome";
            }
            catch (Exception)
            {
            }
        }

        private void ProposeNecessaryComfirm(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListProposingMine listProposingMine = new ListProposingMine(managerHome, listCategoyProposingHome, "NecessaryComfirmToHome");
                managerHome.PageFunction.Content = listProposingMine;
                managerHome.backToBack = "BackToProposingHome";
            }
            catch (Exception)
            {
            }
        }

        private void ProposeApprovedProposing(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListProposingMine listProposingMine = new ListProposingMine(managerHome, listCategoyProposingHome, "ProposeApprovedProposing");
                managerHome.PageFunction.Content = listProposingMine;
                managerHome.backToBack = "BackToProposingHome";
            }
            catch (Exception)
            {
            }
        }
    }
}
