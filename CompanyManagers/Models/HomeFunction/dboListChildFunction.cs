using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Models.HomeFunction
{
    public class DataChildFunction : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private int _idChildFunction;

        public int  idChildFunction 
        {
            get { return _idChildFunction; }
            set { _idChildFunction = value; OnPropertyChanged("idFunction");}
        }

        private string _nameChildFunction;
        public string nameChildFunction
        {
            get { return _nameChildFunction; }
            set { _nameChildFunction = value; OnPropertyChanged("nameFunction");}
        }
    }
}
