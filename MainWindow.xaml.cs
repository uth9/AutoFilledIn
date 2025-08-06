using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;

namespace AutoFilledIn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// 初始化日期
        public static string todayYear = DateTime.Today.ToString("yyyy");
        public static string todayMonth = DateTime.Today.ToString("MM");
        
        /// 初始化显示列表资源
        public required List<List> StudentDataList;
        static private List<string> nationList = ["汉族", "满族", "畲族"];
        static private List<string> regisYearList = ["2008"];
        static private List<string> regisMonthList = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
        static private List<string> genYearList = [];

        /// 初始化当前显示
        public string currentName = "";
        public string currentDevelopedId = "";
        public string currentNation = nationList[1];
        public string currentIdentifyCode = "";
        public string currentId = "";
        public string currentTel = "";
        public string currentAddress = "";
        public string[] registedDate = {};
        public bool currentVolunteerState = true;
        
        public MainWindow()
        {
            InitializeComponent();

            /// 初始化年份
            if (todayYear == null || todayYear == "0000") { todayYear = "2008"; }  //判断年份是否获取成功
            for (int i = Convert.ToInt16(todayYear); i < Convert.ToInt16(todayYear) + 10; i++)
            {
                genYearList.Add(i.ToString());  // 生成年份序列
            }
            regisYearList = genYearList; // 设置年份序列
            
            registedDate = [ regisYearList[0], regisMonthList[0] ]; //绑定显示对象 

            /// 初始化ComboBox控件数值绑定
            nationBox.ItemsSource = nationList;
            nationBox.SelectedIndex = 0;
            registedYearBox.ItemsSource = regisYearList;
            registedYearBox.SelectedIndex = 0;
            registedMonthBox.ItemsSource = regisMonthList;
            registedMonthBox.SelectedIndex = 0;

            /// 初始化StudentData数值绑定
            studentData.ItemsSource = StudentDataList;

            /// 初始化当前显示数据绑定
            nameBox.Text = currentName;
            developIdBox.Text = currentDevelopedId;
            nationBox.Text = currentNation;
            personalIdentifyCodeBox.Text = currentIdentifyCode;
            reEnterIdentifyCodeBox.Text = currentId;
            ifRegAsVolunteerBox.IsChecked = currentVolunteerState;
            telephoneNumberBox.Text = currentTel;
            addressBox.Text = currentAddress;
            registedYearBox.Text = registedDate[0];
            registedMonthBox.Text = registedDate[1];
            ifRegAsVolunteerBox.IsChecked= currentVolunteerState;

        }

        private void CreateNewColumnButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}