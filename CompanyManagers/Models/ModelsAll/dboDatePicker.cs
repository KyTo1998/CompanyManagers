using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CompanyManagers.Models.ModelsAll
{
    public class lichlamviec : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string date { get; set; }
        public string shift_id { get; set;}
        public int id { get; set; }
        /*{
            get 
            {
                if (shift_id != null)
                {
                    return int.Parse(shift_id);
                }
                else
                {
                    return id;
                }
            }
            set { } 
        }*/
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
