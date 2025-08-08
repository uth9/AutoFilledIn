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
        
        
        
        /// 初始化显示列表资源
        public required List<List<string>> studentDataList;

        public void RefreshDataContext(Student student)
        {
            nameBox.DataContext = student;
            developNumberBox.DataContext = student;
            nationBox.DataContext = student;
            personalIdentifyCodeBox.DataContext = student;
            reConfirmedIdentifyCodeBox.DataContext= student;
            registedYearBox.DataContext = student;
            registedMonthBox.DataContext = student;
            telephoneNumberBox.DataContext = student;
            addressBox.DataContext = student;
            ifRegAsVolunteerBox.DataContext = student;
        }
        
        public MainWindow()
        {
            InitializeComponent();

            /// 生成默认对象
            Student student = new Student
            {
                studentName = "",
                studentNation = "汉族",
                personalId = "",
                reConfirmedId = "",
                developedNumber = "",
                address = "",
                regDate = ["2025", "01"],
                telephoneNumber = "",
                volunteerState = true
            };
            RefreshDataContext(student);
            

            
            /// 绑定默认对象
            //this.DataContext = student;
            //registedYearBox.DataContext = ;
        }

        private void CreateNewColumnButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}