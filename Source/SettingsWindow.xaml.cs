using System.Windows;
using System.Windows.Controls;

namespace ExtensionSpoofer
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        public SettingsWindow(string fileName, string extension, string spoofedExtension)
        {
            InitializeComponent();
            FileNameTextBox.Text = fileName;
            FileName = fileName;
            SpoofedExtensionComboBox.Text = spoofedExtension;
            SpoofedExtension = spoofedExtension;
            switch (extension)
            {
                case "EXE":
                    ExtensionComboBox.SelectedIndex = 0;
                    break;
                case "SCR":
                    ExtensionComboBox.SelectedIndex = 1;
                    break;
                case "COM":
                    ExtensionComboBox.SelectedIndex = 2;
                    break;
            }
            Extension = extension;
        }

        public string FileName { get; set; }
        public string Extension { get; set; }
        public string SpoofedExtension { get; set; }

        private void RefreshResult()
        {
            if (!IsLoaded)
                return;

            FileName = FileNameTextBox.Text;
            Extension = ExtensionComboBox.Text;
            SpoofedExtension = SpoofedExtensionComboBox.Text;

            ResultTextBlock.Text = FileName + Extension.ToLower().Reverse() + SpoofedExtension;
        }

        private void FileNameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshResult();
        }

        private void SpoofedExtensionComboBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshResult();
        }

        private void ExtensionComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = ((ComboBoxItem) e.AddedItems[0]).Content as string;
            ExtensionComboBox.Text = text;
            RefreshResult();
        }

        private void SettingsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            RefreshResult();
        }
    }
}