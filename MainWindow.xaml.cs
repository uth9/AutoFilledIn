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
using GlobalHotKey;

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

        /// 初始化快捷键管理器
        HotKeyManager GlobalHotkeyManager = new HotKeyManager();
        Key CustomKey;
        ModifierKeys CustomModifierKey;
        
        /// 启用、禁用热键修改
        private void ChangeHotKeyGroupEnabledState(bool State)
        {
            HotKeyCtrl.IsEnabled = State;
            HotKeyShift.IsEnabled = State;
            HotKeyCustom.IsEnabled = State;
        }

        /// 刷新数据上下文
        private void RefreshDataContext(Student student)
        {
            NameBox.DataContext = student;
            DevelopNumberBox.DataContext = student;
            NationBox.DataContext = student;
            PersonalIdentifyCodeBox.DataContext = student;
            ReconfirmedIdentifyCodeBox.DataContext= student;
            RegistedYearBox.DataContext = student;
            RegistedMonthBox.DataContext = student;
            TelephoneNumberBox.DataContext = student;
            AddressBox.DataContext = student;
            IfRegAsVolunteerBox.DataContext = student;
            studentData.ItemsSource = studentDataList;
        }


        public MainWindow()
        {
            InitializeComponent();

            /// 设置默认焦点
            NameBox.Focus();

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
                string NewText = Regex.Replace(originSender.Text, "[^0-9]", "");
                if (NewText != originSender.Text)
                {
                    MessageTemplates.ShowNumberOnly(originSender);
                    e.Handled = true;
                }
            }
        }

        private void CreateNewColumnButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.studentDataList.Add(new Student(true)
            {
                number = studentDataList.Count + 1,
                studentName = "",
                studentNation = NationBox.Text,
                personalId = "",
                reConfirmedId = "",
                developedNumber = DevelopNumberBox.Text switch
                {
                    "" => "",
                    _ => (int.Parse(DevelopNumberBox.Text) + 1).ToString()
                },
                address = "",
                regDate = string.Concat(RegistedYearBox.Text, "/", RegistedMonthBox.Text),
                telephoneNumber = "",
                volunteerState = true,
            });
            RefreshDataContext(studentDataList[^1]);
            NameBox.Focus();
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
                    MessageTemplates.ShowFailedDeleteFile();
                }
            }
            else
            {
                MessageTemplates.ShowDocMinimum();
            }
        }

        private void ReloadFromButton_Click(object sender, RoutedEventArgs e)
        {
            switch (File.Exists(@".\tempData.log")){
                case true:
                    string XmlString = File.ReadAllText(@".\tempData.log");
                    try
                    {   if (MessageTemplates.AskIfOverrideData() == MessageBoxResult.Yes)
                        {
                            studentDataList = XmlHelper.Deserialize<Student>(XmlString);
                            RefreshDataContext(studentDataList[0]);
                            MessageTemplates.ShowDataLoadSuccess();
                        }
                    }
                    catch (Exception err)
                    {
                        MessageTemplates.ShowDataLoaded();
                    }
                    break;
                case false:
                    MessageTemplates.ShowPathNotExist();
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
                MessageTemplates.ShowSuccessfulWriteData();
            }
            catch(Exception err)
            {
                MessageTemplates.ShowFailedWriteData();
            }
        }

        private void StartServiceButton_Click(object sender, RoutedEventArgs e)
        {
            bool CtrlState = this.HotKeyCtrl.IsChecked switch { true => true, _ => false };
            bool ShiftState = this.HotKeyShift.IsChecked switch { true => true, _ => false };
            int CustomCharCode = (int)Char.ToUpper(this.HotKeyCustom.Text[0]);
            CustomKey = KeyInterop.KeyFromVirtualKey(CustomCharCode);
            CustomModifierKey = (CtrlState, ShiftState) switch
            {
                (true, true) => ModifierKeys.Control | ModifierKeys.Shift,
                (true, false) => ModifierKeys.Control,
                (false, true) => ModifierKeys.Shift,
                (false, false) => ModifierKeys.None,
            };
            try
            {
                GlobalHotkeyManager.Register(CustomKey,CustomModifierKey);
                GlobalHotkeyManager.KeyPressed += (sender, e) =>
                {
                    HotKeyPressed();
                };
                MessageTemplates.ShowSuccessfulRegHotKey();
                ChangeHotKeyGroupEnabledState(false);
            }
            catch( Exception err ) {
                MessageTemplates.ShowFailedRegHotKey();
            }
            
        }

        private void HotKeyCustom_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox originSender = sender as TextBox;
            if (originSender != null && originSender.Text != "")
            {
                string NewText = Regex.Replace(originSender.Text, "[^a-z,A-Z]", "");
                if (NewText != originSender.Text)
                {
                    MessageTemplates.ShowCharOnly(originSender);
                    e.Handled = true;
                }
            }
        }
        private void HotKeyPressed()
        {
            MessageBox.Show(@$"快捷键被按下", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void StopServiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobalHotkeyManager.Unregister(CustomKey, CustomModifierKey);
                ChangeHotKeyGroupEnabledState(true);
                MessageTemplates.ShowSuccessfulUnregHotKey();
            }
            catch (Exception err)
            {
                MessageTemplates.ShowFailedUnregHotKey();
            }
        }
    }
}