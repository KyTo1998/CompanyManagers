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
    /// Interaction logic for pageViewDetailPropose.xaml
    /// </summary>
    public partial class pageViewDetailPropose : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private int _type_duyet;
        public int type_duyet
        {
            get { return _type_duyet; }
            set { _type_duyet = value; OnPropertyChanged("type_duyet"); }
        }

        private int _nhom_de_xuat;
        public int nhom_de_xuat
        {
            get { return _nhom_de_xuat; }
            set { _nhom_de_xuat = value; OnPropertyChanged("nhom_de_xuat"); }
        }
        
        private bool _confirm_status;
        public bool confirm_status
        {
            get { return _confirm_status; }
            set { _confirm_status = value; OnPropertyChanged("confirm_status"); }
        }
        private Detail_Proposet detailPropose;
        InforDx_Proposing dataPropose;
        ManagerHome managerHome;
        public pageViewDetailPropose(ManagerHome _managerHome, Detail_Proposet _detailPropose, InforDx_Proposing _dataPropose)
        {
            InitializeComponent();
            managerHome = _managerHome;
            detailPropose = _detailPropose;
            dataPropose = _dataPropose;
            type_duyet = _detailPropose.type_duyet;
            nhom_de_xuat = _detailPropose.nhom_de_xuat;
            confirm_status = _detailPropose.confirm_status;
            tb_NamePropose.Text = _detailPropose.ten_de_xuat.ToString();
            tb_GroundPropose.Text = _dataPropose.type_dx_string;
            tb_TimeCreatePropose.Text = _detailPropose.thoi_gian_tao.ToString();
            TimeUpdatePropose.Text = $"{_detailPropose.cap_nhat} Ngày trước";
            NameUserCreatePropose.Text = _detailPropose.nguoi_tao;
            tb_TypeComfirm.Text = _detailPropose.kieu_phe_duyet_format;
            /*tb_HandOverCRM.Text = _detailPropose.*/
            /*tb_ReasonCreatePropse.Text = */
        }

        private void ScollCreateProposing(object sender, MouseWheelEventArgs e)
        {
            scoll.ScrollToVerticalOffset(scoll.VerticalOffset - e.Delta);
        }
    }
}
