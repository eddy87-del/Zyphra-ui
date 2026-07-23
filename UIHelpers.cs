using System;
using System.Windows;

namespace ZyphraVirtualAssistant
{
    public partial class MainWindow : Window
    {
        // ... existing code ...

        private void InputTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                TextInputButton_Click(null, null);
                e.Handled = true;
            }
        }

        private void ToolsButton_Click(object sender, RoutedEventArgs e)
        {
            AddLog("🛠️ Opening System Tools Menu...");
            // Could be extended with a tools menu window
            Speak("Quick tools: Notepad, Calculator, Paint, Explorer, or Media Player. Say 'open' followed by the tool name.");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
