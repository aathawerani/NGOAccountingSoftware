using ClosedXML.Excel;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TrustApplication.UI;

namespace TrustApplication.Views
{
    public partial class ImportView : UserControl
    {
        ImportUI importUI = new ImportUI();
        public ImportView()
        {
            InitializeComponent();
            importUI.SetControls(TxtUserGivenName, TxtFilePath, BtnBrowse, ChkHasHeader, TxtLog, BtnImport, Progress);
        }

        private async Task LogAsync(string message)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                TxtLog.AppendText($"{DateTime.Now:HH:mm:ss}  {message}{Environment.NewLine}");
                TxtLog.ScrollToEnd();
            });
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            importUI.BtnBrowse_Click();
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            importUI.BtnImport_Click(LogAsync);
        }

    }
}
