using CompanyManagers.Controllers;
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
                    request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.TokenEp);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        Root_Proposing dataProposing = JsonConvert.DeserializeObject<Root_Proposing>(UnicodeEncoding.UTF8.GetString(e.Result));
                        if (dataProposing != null)
                        {
                            tb_CountSumProposing.Text = dataProposing.data.totaldx.ToString();
                            tb_CountWaitProposing.Text = dataProposing.data.dxChoDuyet.ToString();
                            tb_CountApprovedProposing.Text = dataProposing.data.dxduyet.ToString();
                            tb_CountNecessaryProposing.Text = dataProposing.data.dxCanduyet.ToString();
                            listProposingHome = dataProposing.data.data.ToList();
                            foreach (var item in listProposingHome)
                            {
                                item.type_dx_string = listCategoyProposingHome.Find(x => x.cate_dx == item.type_dx).name_cate_dx;
                            }
                        }
                    };
                    await request.UploadValuesTaskAsync(UrlApi.apiShowHomeProposing, request.Headers);
                }
            }
            catch (Exception)
            {
            }
        }

        public async void GetCategoryProposing()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    using (WebClient request = new WebClient())
                    {
                        request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.TokenEp);
                        request.QueryString.Add("page", $"{i}");
                        request.UploadValuesCompleted += (s, e) =>
                        {
                            Root_CategoryProposing dataCategoryProposing = JsonConvert.DeserializeObject<Root_CategoryProposing>(UnicodeEncoding.UTF8.GetString(e.Result));
                            if (listCategoyProposingHome == null)
                            {
                                listCategoyProposingHome = new List<Result_CategoryProposing>();
                            }
                            foreach (var item in dataCategoryProposing.data.result)
                            {
                                listCategoyProposingHome.Add(item);
                            }
                        };
                        await request.UploadValuesTaskAsync(UrlApi.apiCategoryProposing, request.QueryString);
                    }
                }
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
    }
}
