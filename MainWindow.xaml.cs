using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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

            /// 设置默认焦点
            nameBox.Focus();

            /// 必要数据初始化
            studentData.ItemsSource = studentDataList;

            /// 生成默认对象
            Student student = new Student(true)
            {
                number = 1,
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


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                Regex regex = new Regex("[^0-9]+");
                if (regex.IsMatch(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void NumberOnlyBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox originSender = sender as TextBox;
            if (originSender != null && originSender.Text != "")
            {
                originSender.Text = Regex.Replace(originSender.Text, "[^0-9]", "");
                MessageBox.Show(@$"{originSender.Name}内不能出现数字以外的字符", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
        }

        private void CreateNewColumnButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.studentDataList.Add(new Student(true)
            {
                number = studentDataList.Count + 1,
                studentName = "",
                studentNation = nationBox.Text,
                personalId = "",
                reConfirmedId = "",
                developedNumber = developNumberBox.Text switch
                {
                    "" => "",
                    _ => (int.Parse(developNumberBox.Text) + 1).ToString()
                },
                address = "",
                regDate = string.Concat(registedYearBox.Text, "/", registedMonthBox.Text),
                telephoneNumber = "",
                volunteerState = true,
            });
            RefreshDataContext(studentDataList[^1]);
            nameBox.Focus();
        }

        private void studentData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int selectedIndex = studentData.SelectedIndex;
            RefreshDataContext(studentDataList[selectedIndex]);
        }

        private void DelColumnButton_Click(object sender, RoutedEventArgs e)
        {
            if (studentDataList.Count > 1)
            {
                try
                {
                    studentDataList.Remove((Student)studentData.SelectedItem);
                }
                catch (Exception err)
                {
                    MessageBox.Show(@"删除档案失败", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(@"至少需要存在一份档案", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ReloadFromButton_Click(object sender, RoutedEventArgs e)
        {
            switch (File.Exists(@".\tempData.log")){
                case true:
                    string XmlString = File.ReadAllText(@".\tempData.log");
                    try
                    {
                        studentDataList = XmlHelper.Deserialize<Student>(XmlString);
                        RefreshDataContext(studentDataList[0]);
                        MessageBox.Show(@"数据加载成功", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception err)
                    {
                        
                    }
                    break;
                case false:
                    MessageBox.Show(@"路径.\tempData.log不存在，请检查根目录", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void SaveToButton_Click(object sender, RoutedEventArgs e)
        {
            string XmlString = XmlHelper.Serialize((ObservableCollection<Student>)studentDataList);
            string path = @".\tempData.log";
            try
            {
                File.WriteAllText(path, XmlString);
                MessageBox.Show(@"数据写入成功，位置在<程序根目录\tempData.log>", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception err)
            {
                MessageBox.Show(@"数据写入失败，请检查权限或尝试联系开发者", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}