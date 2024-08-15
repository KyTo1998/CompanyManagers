using CompanyManagers.Common.Popups;
using CompanyManagers.Controllers;
using CompanyManagers.Models.ModelsPageStaff;
using CompanyManagers.Views.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagers.Views.PageStaff.Proposing
{
    /// <summary>
    /// Interaction logic for ListProposing.xaml
    /// </summary>
    public partial class ListCategoryProposing : UserControl,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private List<Result_CategoryProposing> _listCategoryProposingHome;
        public List<Result_CategoryProposing> listCategoyProposingHome
        {
            get { return _listCategoryProposingHome; }
            set { _listCategoryProposingHome = value; OnPropertyChanged("listCategoyProposingHome"); }
        }

         List<Result_CategoryProposing> listCategoyProposingInput = new List<Result_CategoryProposing>();
         List<Result_CategoryProposing> listCategoyProposingSearch = new List<Result_CategoryProposing>();
        ManagerHome managerHome { get; set; }   
        public ListCategoryProposing(ManagerHome _managerHome, List<Result_CategoryProposing> lstCategoyProposingHome)
        {
            InitializeComponent();
            managerHome = _managerHome;
            listCategoyProposingInput = lstCategoyProposingHome;
            listCategoyProposingHome = lstCategoyProposingHome.ToList();
        }

        private void HoverProposing(object sender, MouseEventArgs e)
        {
            Result_CategoryProposing dataCategoryProposing = (Result_CategoryProposing)(sender as Border).DataContext;
            if (dataCategoryProposing != null)
            {
                foreach (var item in listCategoyProposingHome)
                {
                    if (item._id == dataCategoryProposing._id)
                    {
                        item.statusHoverProposing = true;
                    }
                    else
                    {
                        item.statusHoverProposing = false;
                    }
                }  
            }
        }

        private void SearchProposing(object sender, TextChangedEventArgs e)
        {
            try
            {
                listCategoyProposingSearch = new List<Result_CategoryProposing>();
                foreach (var str in listCategoyProposingInput)
                {
                    if (str.name_cate_dx != null)
                    {
                        if (str.name_cate_dx_display.ToLower().RemoveUnicode().Contains(tb_SearchProposing.Text.ToLower().RemoveUnicode()))
                        {
                           listCategoyProposingSearch.Add(str);
                        }
                    }
                }
                listCategoyProposingHome = listCategoyProposingSearch.ToList();
                if (tb_SearchProposing.Text == "")
                {
                    listCategoyProposingHome = listCategoyProposingInput.ToList();
                }
            }
            catch (Exception)
            {
            }
        }

        private void ClickShowCreateProposing(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Result_CategoryProposing dataCategoryProposing = (Result_CategoryProposing)(sender as Border).DataContext;
                if (dataCategoryProposing != null)
                {
                    managerHome.GetSettingPropose(dataCategoryProposing.cate_dx);
                    if (dataCategoryProposing.cate_dx == 18)
                    {
                        managerHome.PagePopupGrayColor = new PagePopupGrayColor(managerHome);
                        managerHome.PagePopupGrayColor.Popup1.NavigationService.Navigate(new pageCreateProposedWorkSchedule(managerHome, dataCategoryProposing,null));
                        managerHome.PagePopup.NavigationService.Navigate(managerHome.PagePopupGrayColor);
                    }
                    else
                    {
                        managerHome.PagePopupGrayColor = new PagePopupGrayColor(managerHome);
                        managerHome.PagePopupGrayColor.Popup1.NavigationService.Navigate(new pageCreateNewProposing(managerHome, dataCategoryProposing,null,null));
                        managerHome.PagePopup.NavigationService.Navigate(managerHome.PagePopupGrayColor);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
