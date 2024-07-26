using CompanyManagers.Models.ModelsAll;
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

        public int id { get; set; }
        public int DayInCalendar { get; set; }
        public string dayString { get; set; }

        public int _shiftSelected;
        public int shiftSelected
        {
            get { return _shiftSelected; }
            set
            {
                _shiftSelected = value;
                OnPropertyChanged("shiftSelected");
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

        public int _statusPast;

        public int statusPast
        {
            get { return _statusPast; }
            set
            {
                _statusPast = value;
                OnPropertyChanged("statusPast");
            }
        }
        public List<Item_ShiftAll> listShiftSelectedAll {  get; set; }

        private List<string> _list_shift_name_picker = new List<string>();
        public List<string> list_shift_name_picker
        {
            get { return _list_shift_name_picker; }
            set { _list_shift_name_picker = value; OnPropertyChanged("list_shift_name"); }
        }
    }
}
