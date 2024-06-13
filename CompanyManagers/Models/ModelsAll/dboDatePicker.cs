using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Models.ModelsAll
{
    public class lichlamviec : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int id;
        public int ngay { get; set; }
        public string ngayString { get; set; } = "";

        public int _shiftSelected;
        public int shiftSelected
        {
            get { return _shiftSelected; }
            set
            {
                _shiftSelected = value;
                OnPropertyChanged("0");
            }
        }

        public int _statusClick;

        public int statusClick
        {
            get { return _statusClick; }
            set
            {
                _statusClick = value;
                OnPropertyChanged("statusClick");
            }
        }
        //public List<Item_CaLamViec> dsca { get; set; }
        //public List<Item_CaLamViec> caThayDoi { get; set; }
    }
}
