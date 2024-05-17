using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagers.Models.HomeFunction
{
    public class DataFunction : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private int _idFunction;

        public int  idFunction 
        {
            get { return _idFunction; }
            set { _idFunction = value; OnPropertyChanged("idFunction");}
        }

        private string _nameFunction;
        public string nameFunction 
        {
            get { return _nameFunction; }
            set { _nameFunction = value; OnPropertyChanged("nameFunction");}
        }

        private string _colorFunction1;
        private string _colorFunction2;
       
       
        public string colorFunction1 
        {
            get { return _colorFunction1; }
            set { _colorFunction1 = value; OnPropertyChanged("colorFunction1");}
        }

        public string colorFunction2 
        {
            get { return _colorFunction2; }
            set { _colorFunction2 = value; OnPropertyChanged("colorFunction2");}
        }
         
    }
}
