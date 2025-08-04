using System.Collections.ObjectModel;
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

        public MainWindow()
        {
            InitializeComponent();

            /// 初始化ComboBox控件数值绑定
            NationBox.ItemsSource = NationList;
            NationBox.SelectedIndex = 0;
            RegisteredYearBox.ItemsSource = RegisYearList;
            RegisteredYearBox.SelectedIndex = 0;
            RegisteredMonthBox.ItemsSource= RegisMonthList;
            RegisteredMonthBox.SelectedIndex = 0;

            /// 初始化StudentData数值绑定
            
            StudentData.ItemsSource = StudentDataList;
        }

        private void CreateNewColumnButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}