using InputSimulatorStandard;
using InputSimulatorStandard.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InputSimulatorStandard;           // 核心命名空间
// using WindowsInput.Native;    // VirtualKeyCode 枚举

namespace AutoFilledIn
{
    public static class InputSimulatorService
    {
        private static readonly InputSimulator _inputSimulator = new InputSimulator();

        // 模拟Tab键
        public static void SimulateTab()
        {
            _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
        }

        // 模拟字符串输入
        public static void SimulateText(string text)
        {
            _inputSimulator.Keyboard.TextEntry(text);
        }
    }
}