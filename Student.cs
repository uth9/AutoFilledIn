using System;
using System.Collections;
using System.ComponentModel;

namespace AutoFilledIn
{
	public class Student : INotifyPropertyChanged
	{
        /// 初始化日期
        private string _todayYear = DateTime.Today.ToString("yyyy");
        public string todayYear
        {
            get
            {
                if (this._todayYear is null || this._todayYear == "0000")
                {
                    this._todayYear = "2025";
                }
                return this._todayYear;
            }
            set
            {
                this._todayYear = value;
            }
        }
        private string _todayMonth = DateTime.Today.ToString("MM");
        public string todayMonth
        {
            get
            {
                if (this._todayMonth is null || this._todayMonth == "00")
                {
                    this._todayMonth = "01";
                }
                return this._todayMonth;
            }
            set
            {
                this._todayMonth = value;
            }
        }
        private string[] _regYearList = ["2025", "2026", "2027", "2028", "2029", "2030", "2031", "2032", "2033", "2034", "2035", "2036", "2037", "2038", "2039", "2040", "2041", "2042", "2043", "2044"];
        public string[] regYearList
        {
            get
            {
                if (todayYear != this._regYearList[0])
                {
                    int year = int.Parse(todayYear);
                    for(int i = 0; i < 20; i++, year++)
                    {
                       this._regYearList[i] = Convert.ToString(year);
                    }
                }
                return this._regYearList;
            }
            set { }
        }
        private string[] _regMonthList = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
        public string[] regMonthList
        {
            get
            {
                return this._regMonthList;
            }
            set { }
        }


        private ArrayList _nationList = ["汉族", "畲族", "回族"];
        public ArrayList nationList
        {
            get => this._nationList;
            set { }
        }
        public int nationIndex
        {
            get { return this._nationList.IndexOf(_studentNation); }
            set { }
        }


        private string _studentName = "";
        public string studentName
        {
            get => this._studentName;
            set
            {
                this._studentName = value;
            }
        }
        private string _studentNation = "汉族";
        public string studentNation
        {
            get => this._studentNation;
            set
            {
                this._studentNation = value;
            }
        }
        private string _personalId = "";
        public string personalId
        {
            get => _personalId;
            set
            {
                this._personalId = value;
            }
        }
        private string _reConfirmedId = "";
        public string reConfirmedId
        {
            get => this._reConfirmedId;
            set
            {
                this._reConfirmedId = value;
            }
        }
        private string _developedNumber = "";
        public string developedNumber
        {
            get => this._developedNumber;
            set
            {
                this._developedNumber = value;
            }
        }
        private string[] _regDate = ["2025", "01"];
        public string[] regDate
        {
            get => this._regDate;
            set
            {
                this._regDate = value;
            }
        }
        public string regYear
        {
            get => this._regDate[0];
            set
            {
                this._regDate[0] = value;
            }
        }
        public string regMonth
        {
            get => this._regDate[1];
            set
            {
                this._regDate[1] = value;
            }
        }
        private string _telephoneNumber = "";
        public string telephoneNumber
        {
            get => this._telephoneNumber;
            set
            {
                this._telephoneNumber = value;
            }
        }
        private string _address = "";
        public string address
        {
            get => this._address;
            set
            {
                this._address = value;
            }
        }
        private bool _volunteerState = true;
        public bool volunteerState
        {
            get => this._volunteerState;
            set
            {
                this._volunteerState = value;
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

