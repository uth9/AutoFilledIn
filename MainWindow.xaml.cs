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

namespace AutoFilledIn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public required List<List> StudentDataList;
        List<string> NationList = ["汉族", "满族", "畲族"];
        List<string> RegisYearList = ["2025"];
        List<string> RegisMonthList = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
        
        /// 初始化当前显示
        public string CurrentName = "姓名";
        public string CurrentDevelopedId = "id";
        public string CurrentNation = "畲族";
        public string CurrentIdentifyCode = "idcode";
        public string CurrentReconfirmedId = "confirmedid";
        public string CurrentTel = "";
        public string CurrentAddress = "";
        public int[] RegistedDate = {9, 23};
        public bool CurrentVolunteerState = true;
        
        public MainWindow()
        {
            InitializeComponent();

            /// 初始化ComboBox控件数值绑定
            NationBox.ItemsSource = NationList;
            NationBox.SelectedIndex = 0;
            RegistedYearBox.ItemsSource = RegisYearList;
            RegistedYearBox.SelectedIndex = 0;
            RegistedMonthBox.ItemsSource= RegisMonthList;
            RegistedMonthBox.SelectedIndex = 0;

            /// 初始化StudentData数值绑定
            StudentData.ItemsSource = StudentDataList;

            /// 初始化当前显示数据绑定
            NameBox.Text = CurrentName;
            DevelopIdBox.Text = CurrentDevelopedId;
            NationBox.Text = CurrentNation;
            PersonalIdentifyCodeBox.Text = CurrentIdentifyCode;
            ReEnterIdentifyCodeBox.Text = CurrentReconfirmedId;
            IfRegAsVolunteerBox.IsChecked = CurrentVolunteerState;
        }

        private void CreateNewColumnButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}