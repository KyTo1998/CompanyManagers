using CompanyManagers.Models.HomeFunction;
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

namespace CompanyManagers.Views.Functions.HomeFunction
{
    /// <summary>
    /// Interaction logic for ListChildFunction.xaml
    /// </summary>
    public partial class ListChildFunction : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private List<DataChildFunction> _dataChildFunction;
        public List<DataChildFunction> dataChildFunction 
        {
            get { return _dataChildFunction; }
            set { _dataChildFunction = value; OnPropertyChanged("dataChildFunction");}
        }
        ListFunction pageListFunction { get; set; }
        DataFunction dataFunction;
        public ListChildFunction(ListFunction _pageListFunction, DataFunction _dataFunction, List<DataChildFunction> _dataChildFunction)
        {
            InitializeComponent();
            this.pageListFunction = _pageListFunction;
            this.dataFunction = _dataFunction;
            switch (_dataFunction.idFunction)
            {
                case 1:
                    tb_TitleFunction.Text = _dataFunction.nameFunction;
                    break;
                case 2:
                    tb_TitleFunction.Text = _dataFunction.nameFunction;
                    break;
                case 3:
                    tb_TitleFunction.Text = _dataFunction.nameFunction;
                    break;
                case 4:
                    tb_TitleFunction.Text = _dataFunction.nameFunction;
                    break;
            }
            dataChildFunction = _dataChildFunction;
            
        }
    }
}
