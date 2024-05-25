using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for pageCreateNewProposing.xaml
    /// </summary>
    public partial class pageCreateNewProposing : UserControl
    {
        public class typeConfirm
        {
            public int id_Confirm { get; set; }
            public string name_Confirm { get; set; }
        }
        List<typeConfirm> lstTypeConfirms = new List<typeConfirm>();
        ManagerHome managerHome { set; get; }
        Result_CategoryProposing dataCategoryProposing;
        public pageCreateNewProposing(ManagerHome _managerHome, Result_CategoryProposing _dataCategoryProposing)
        {
            InitializeComponent();
            managerHome = _managerHome;
            tb_UserNameCreate.Text = _managerHome.UserCurrent.user_info.ep_name;
            tb_CategoryProposingCreate.Text = _dataCategoryProposing.name_cate_dx;
            lstTypeConfirms.Add(new typeConfirm() { id_Confirm = 1, name_Confirm = "Duyệt đồng thời" });
            lstTypeConfirms.Add(new typeConfirm() { id_Confirm = 2, name_Confirm = "Duyệt lần lượt" });
            SelectTypeComfirm.ItemsSourceSelected = lstTypeConfirms.ToList();
        }

        private void ClickSelectTypeComfirm(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void SelectionChangeUserComfirm(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void SelectionChangeUserFollow(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
