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
using System.Collections;

namespace AutoFilledIn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        
        /// 初始化显示列表资源
        public List<Student> studentDataList = new List<Student>();

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
            studentData.ItemsSource = studentDataList;
        }

        
        
        public MainWindow()
        {
            InitializeComponent();
            studentData.ItemsSource = studentDataList;

            /// 生成默认对象
            Student student = new Student(true)
            {
                studentName = "",
                studentNation = "汉族",
                personalId = "",
                reConfirmedId = "",
                developedNumber = "",
                address = "",
                regDate = ["2025", "01"],
                telephoneNumber = "",
                volunteerState = true,
            };
            studentDataList.Add(student);
            RefreshDataContext(student);
            

            
            
        }

        private void CreateNewColumnButton_Click(object sender, RoutedEventArgs e)
        {
            
            studentDataList.Add(new Student(true)
            {
                studentName = "",
                studentNation = "汉族",
                personalId = "",
                reConfirmedId = "",
                developedNumber = "",
                address = "",
                regDate = ["2025", "01"],
                telephoneNumber = "",
                volunteerState = true,
            });
            RefreshDataContext(studentDataList[^1]);
        }
    }
}