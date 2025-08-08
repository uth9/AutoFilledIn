using System;
using System.ComponentModel;

namespace AutoFilledIn
{
	public class defaultRes : INotifyPropertyChanged
    {
        private string[] _regYearList = ["2025", "2026", "2027", "2028", "2029", "2030", "2031", "2032", "2033", "2034",];
        public string[] regYearList
        {
            get => this._regYearList;
            set
            {
                this._regYearList = value;
            }
        }
        private string[] _regMonthList = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
        public string[] regMonthList
        {
            get => this._regMonthList;
            set
            {
                this._regMonthList = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged is not null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

