using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutoFilledIn
{
    public static class MessageTemplates
    {
        public static MessageBoxResult ShowFailedDeleteFile()
        {
            return MessageBox.Show(@"删除档案失败", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static MessageBoxResult ShowDocMinimum()
        {
            return MessageBox.Show(@"至少需要存在一份档案", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public static MessageBoxResult ShowDataLoadSuccess()
        {
            return MessageBox.Show(@"数据加载成功", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static MessageBoxResult ShowDataLoaded()
        {
            return MessageBox.Show(@"数据加载失败", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static MessageBoxResult ShowPathNotExist()
        {
            return MessageBox.Show(@"路径.\tempData.log不存在，请检查根目录", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static MessageBoxResult ShowSuccessfulWriteData()
        {
            return MessageBox.Show(@"数据写入成功，位置在<程序根目录\tempData.log>", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static MessageBoxResult ShowFailedWriteData()
        {
            return MessageBox.Show(@"数据写入失败，请检查权限或尝试联系开发者", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static MessageBoxResult ShowSuccessfulRegHotKey()
        {
            return MessageBox.Show("快捷键注册成功\n注意：Ctrl快捷键目前似乎会带来许多未知的问题，请谨慎使用", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static MessageBoxResult ShowFailedRegHotKey()
        {
            return MessageBox.Show(@"快捷键注册失败，请检查权限或尝试联系开发者", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static MessageBoxResult ShowCharOnly(TextBox textBox)
        {
            return MessageBox.Show(@$"{textBox.Name}内不能出现字母以外的字符", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public static MessageBoxResult ShowNumberOnly(TextBox textBox)
        {
            return MessageBox.Show(@$"{textBox.Name}内不能出现数字以外的字符", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public static MessageBoxResult ShowSuccessfulUnregHotKey()
        {
            return MessageBox.Show(@"快捷键解绑成功", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static MessageBoxResult ShowFailedUnregHotKey()
        {
            return MessageBox.Show(@"快捷键解绑失败，请检查权限或尝试联系开发者", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static MessageBoxResult AskIfOverrideData()
        {
            return MessageBox.Show(@"确认覆盖当前数据？", "Information", MessageBoxButton.OKCancel, MessageBoxImage.Information);
        }
        public static MessageBoxResult ShowTextIsZero(TextBox textBox)
        {
            return MessageBox.Show(@$"输入框{textBox.Name}不能为空，请重试", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static MessageBoxResult ShowTwoInputsNotEqual(string text)
        {
            return MessageBox.Show(@$"两次{text}输入值不同，请重新输入", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
