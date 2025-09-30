using ClosedXML.Excel;
using DocumentFormat.OpenXml.Presentation;
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
using System.Windows.Controls;
using System.Windows.Threading;
using TrustApplication.Exceptions;

namespace TrustApplication.UI
{
    class ImportUI
    {
        TextBox TxtUserGivenName; TextBox TxtFilePath; Button Browse; CheckBox ChkHasHeader; TextBox TxtLog; Button BtnImport;
        ProgressBar Progress;
        ImportBL bl = new ImportBL();
        private Func<string, Task> _log;
        private Task Log(string msg) => _log(msg);

        public void SetControls(TextBox _TxtUserGivenName, TextBox _TxtFilePath, Button _Browse,
            CheckBox _ChkHasHeader, TextBox _TxtLog, Button _BtnImport, ProgressBar _Progress)
        {
            TxtUserGivenName = _TxtUserGivenName;
            TxtFilePath = _TxtFilePath;
            Browse = _Browse;
            ChkHasHeader = _ChkHasHeader;
            TxtLog = _TxtLog;
            BtnImport = _BtnImport;
            Progress = _Progress;
        }

        public void BtnBrowse_Click()
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Excel 97-2003 (*.xls)|*.xls|Excel Workbook (*.xlsx)|*.xlsx",
                CheckFileExists = true,
                Multiselect = false
            };
            if (dlg.ShowDialog() == true)
            {
                TxtFilePath.Text = dlg.FileName;
            }
        }

        public async void BtnImport_Click(Func<string, Task>? logAsync)
        {
            _log = logAsync ?? (_ => Task.CompletedTask);
            BtnImport.IsEnabled = false;
            Progress.Value = 0;
            TxtLog.Clear();
            try
            {
                bl.ImportAccounts(TxtFilePath.Text, TxtUserGivenName.Text, ChkHasHeader.IsChecked, Log, Progress);
            }
            catch (Exception ex)
            {
                await Log($"FAILED: {ex.Message}");

                // Best-effort: mark the import_file as failed if we already created it
                // (We parse id from log line "Created import_file id=123".)
                var marker = "Created import_file id=";
                var txt = TxtLog.Text;
                if (txt.Contains(marker))
                {
                    try
                    {
                        var start = txt.IndexOf(marker, StringComparison.Ordinal) + marker.Length;
                        var end = txt.IndexOf('.', start);
                        if (end < 0) end = start + 32;
                        var idStr = txt.Substring(start, end - start).Trim();
                        if (long.TryParse(idStr, out var failId))
                        {
                            bl.updateImportRecordError(failId, ex);
                        }
                    }
                    catch { /* swallow secondary failure */ }
                }
            }
            finally
            {
                BtnImport.IsEnabled = true;
            }

        }



        // --- Mapping & helpers --------------------------------------------------

    }
}
