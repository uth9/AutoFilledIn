using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
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
using System.Xml.Serialization;

namespace AutoFilledIn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// 初始化显示列表资源
        public ObservableCollection<Student> studentDataList = new ObservableCollection<Student>();

        /// 初始化数据库相关定义
        private static string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Data.mdf");

        /// 刷新数据上下文
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

            /// 必要数据初始化
            
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
                regDate = "2025/01",
                telephoneNumber = "",
                volunteerState = true,
            };
            studentDataList.Add(student);
            RefreshDataContext(student);
            

            
            
        }

        private void CreateNewColumnButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.studentDataList.Add(new Student(true)
            {
                studentName = "",
                studentNation = "汉族",
                personalId = "",
                reConfirmedId = "",
                developedNumber = "",
                address = "",
                regDate = "2025/01",
                telephoneNumber = "",
                volunteerState = true,
            });
            RefreshDataContext(studentDataList[^1]);
        }

        private void studentData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int selectedIndex = studentData.SelectedIndex;
            RefreshDataContext(studentDataList[selectedIndex]);
        }

        private void DelColumnButton_Click(object sender, RoutedEventArgs e)
        {
            studentDataList.Remove((Student)studentData.SelectedItem);
        }

        private void ReloadFromButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SaveToButton_Click(object sender, RoutedEventArgs e)
        {
            string XmlString = XmlHelper.Serialize((ObservableCollection<Student>)studentDataList);
        }
    }
}