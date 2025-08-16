using GlobalHotKey;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            ReconfirmedIdentifyCodeBox.DataContext = student;
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
#pragma warning disable CS0168 // 声明了变量，但从未使用过
                try
                {
                    studentDataList.Remove((Student)studentData.SelectedItem);
                }
                catch (Exception err)
                {
                    MessageTemplates.ShowFailedDeleteFile();
                }
#pragma warning restore CS0168 // 声明了变量，但从未使用过
            }
            else
            {
                MessageTemplates.ShowDocMinimum();
            }
        }

        private void ReloadFromButton_Click(object sender, RoutedEventArgs e)
        {
            switch (File.Exists(@".\tempData.log"))
            {
                case true:
                    string XmlString = File.ReadAllText(@".\tempData.log");
#pragma warning disable CS0168 // 声明了变量，但从未使用过
                    try
                    {
                        if (MessageTemplates.AskIfOverrideData() == MessageBoxResult.Yes)
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
#pragma warning restore CS0168 // 声明了变量，但从未使用过
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
#pragma warning disable CS0168 // 声明了变量，但从未使用过
            try
            {
                File.WriteAllText(path, XmlString);
                MessageTemplates.ShowSuccessfulWriteData();
            }
            catch (Exception err)
            {
                MessageTemplates.ShowFailedWriteData();
            }
#pragma warning restore CS0168 // 声明了变量，但从未使用过
        }

        private void StartServiceButton_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS0168 // 声明了变量，但从未使用过
            try
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
                GlobalHotkeyManager.Register(CustomKey, CustomModifierKey);
                GlobalHotkeyManager.KeyPressed += (sender, e) =>
                {
                    HotKeyPressed();
                };
                MessageTemplates.ShowSuccessfulRegHotKey();
                ChangeHotKeyGroupEnabledState(false);
                this.StopServiceButton.IsEnabled = true;
                this.StartServiceButton.IsEnabled = false;
            }
            catch (IndexOutOfRangeException)
            {
                MessageTemplates.ShowTextIsZero(this.HotKeyCustom);
            }
            catch (Exception err)
            {
                MessageTemplates.ShowFailedRegHotKey();
            }
#pragma warning restore CS0168 // 声明了变量，但从未使用过

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
            // MessageBox.Show(@$"快捷键被按下", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            InputSimulatorService.SimulateText(this.NameBox.Text);
            InputSimulatorService.SimulateTab();
            InputSimulatorService.SimulateText(this.PersonalIdentifyCodeBox.Text);
            InputSimulatorService.SimulateTab();
            InputSimulatorService.SimulateText(this.PersonalIdentifyCodeBox.Text);
            InputSimulatorService.SimulateTab();
            { InputSimulatorService.SimulateDown(2); } //TODO:默认输入汉族，其它之后再改
            InputSimulatorService.SimulateTab();
            InputSimulatorService.SimulateDown(2); //输入职业
            InputSimulatorService.SimulateTab();
            InputSimulatorService.SimulateDown(1,false);
            InputSimulatorService.SimulateUp(3);
            InputSimulatorService.SimulateTab();
            InputSimulatorService.SimulateDown(1, false);
            InputSimulatorService.SimulateUp(1);
            InputSimulatorService.SimulateTab();
            InputSimulatorService.SimulateDown(2);
            //TODO: 要输日期力（悲）
            //TODO:写不下去了，看过操作逻辑再说吧
        }

        private void StopServiceButton_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS0168 // 声明了变量，但从未使用过
            try
            {
                GlobalHotkeyManager.Unregister(CustomKey, CustomModifierKey);
                ChangeHotKeyGroupEnabledState(true);
                MessageTemplates.ShowSuccessfulUnregHotKey();
                this.StartServiceButton.IsEnabled = true;
                this.StopServiceButton.IsEnabled = false;
            }
            catch (Exception err)
            {
                MessageTemplates.ShowFailedUnregHotKey();
            }
#pragma warning restore CS0168 // 声明了变量，但从未使用过
        }

        private void ReconfirmedBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            NumberOnlyBox_PreviewLostKeyboardFocus(sender, e);
            if (this.ReconfirmedIdentifyCodeBox.Text != this.PersonalIdentifyCodeBox.Text)
            {
                MessageTemplates.ShowTwoInputsNotEqual("身份证号");
                e.Handled = true;
            }
        }
    }
}