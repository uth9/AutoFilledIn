using System;
using System.Collections;
using System.ComponentModel;

namespace AutoFilledIn
{
    public class Student : INotifyPropertyChanged
    {
        public Student() { }
        public Student(bool regToDataList)
        {
            if (regToDataList)
            {
                StudentDatas.Add(this);
                return;
            }
        }

        public static List<Student> StudentDatas = [];

        /// 初始化日期
        private static string _todayYear = DateTime.Today.ToString("yyyy");
        public string todayYear
        {
            get
            {
                if (_todayYear is null || _todayYear == "0000")
                {
                    _todayYear = "2025";
                }
                return _todayYear;
            }
            set
            {
                _todayYear = value;
            }
        }
        private static string _todayMonth = DateTime.Today.ToString("MM");
        public string todayMonth
        {
            get
            {
                if (_todayMonth is null || _todayMonth == "00")
                {
                    _todayMonth = "01";
                }
                return _todayMonth;
            }
            set
            {
                _todayMonth = value;
            }
        }
        private static string[] _regYearList = ["2025", "2026", "2027", "2028", "2029", "2030", "2031", "2032", "2033", "2034", "2035", "2036", "2037", "2038", "2039", "2040", "2041", "2042", "2043", "2044"];
        public string[] regYearList
        {
            get
            {
                if (todayYear != _regYearList[0])
                {
                    int year = int.Parse(todayYear);
                    for(int i = 0; i < 20; i++, year++)
                    {
                       _regYearList[i] = Convert.ToString(year);
                    }
                }
                return _regYearList;
            }
            set { }
        }
        private static string[] _regMonthList = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
        public string[] regMonthList
        {
            get
            {
                return _regMonthList;
            }
            set { }
        }


        private static string[] _nationList = ["汉族", "畲族", "回族"];
        public string[] nationList
        {
            get => _nationList;
            set { }
        }


        private string _studentName = "";
        public string studentName
        {
            get => this._studentName;
            set
            {
                this._studentName = value;
                this.onPropertyChanged(nameof(this.studentName));
            }
        }
        private string _studentNation = "汉族";
        public string studentNation
        {
            get => this._studentNation;
            set
            {
                this._studentNation = value;
                this.onPropertyChanged(nameof(this.studentNation));
            }
        }
        private string _personalId = "";
        public string personalId
        {
            get => _personalId;
            set
            {
                this._personalId = value;
                this.onPropertyChanged(nameof(this.personalId));
            }
        }
        private string _reConfirmedId = "";
        public string reConfirmedId
        {
            get => this._reConfirmedId;
            set
            {
                this._reConfirmedId = value;
                this.onPropertyChanged(nameof(this.reConfirmedId));
            }
        }
        private string _developedNumber = "";
        public string developedNumber
        {
            get => this._developedNumber;
            set
            {
                this._developedNumber = value;
                this.onPropertyChanged(nameof(this.developedNumber));
            }
        }
        private string[] _regDate = ["2025", "01"];
        public string regDate
        {
            get => string.Concat(this._regDate[0], "/", this._regDate[1]);
            set { _regDate = value.Split('/'); }
        }
        public string regYear
        {
            get => this._regDate[0];
            set
            {
                this._regDate[0] = value;
                this.onPropertyChanged(nameof(this.regYear));
                this.onPropertyChanged(nameof(this.regDate));
            }
        }
        public string regMonth
        {
            get => this._regDate[1];
            set
            {
                this._regDate[1] = value;
                this.onPropertyChanged(nameof(this.regMonth));
                this.onPropertyChanged(nameof(this.regDate));
            }
        }
        private string _telephoneNumber = "";
        public string telephoneNumber
        {
            get => this._telephoneNumber;
            set
            {
                this._telephoneNumber = value;
                this.onPropertyChanged(nameof(this.telephoneNumber));
            }
        }
        private string _address = "";
        public string address
        {
            get => this._address;
            set
            {
                this._address = value;
                this.onPropertyChanged(nameof(this.address));
            }
        }
        private bool _volunteerState = true;
        public bool volunteerState
        {
            get => this._volunteerState;
            set
            {
                this._volunteerState = value;
                this.onPropertyChanged(nameof(volunteerState));
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

