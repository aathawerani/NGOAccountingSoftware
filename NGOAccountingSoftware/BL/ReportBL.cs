using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class ReportBL : BL
    {
        //AccountsBL Abl = new AccountsBL();
        LoadAccountsBL lbl = new LoadAccountsBL();
        AccountingSheets accSheet;

        public List<string> GetStatementList()
        {
            List<string> statements = new List<string>();
            statements.Add("Trial Balance");
            return statements;
        }

        public void WriteAccounts(int TrustCode, string Years)
        {
            accSheet = th.GetSheet(TrustCode);
            accSheet.SetWriterFile();
            ExportAccounts(TrustCode, Years);
        }

        void ExportAccounts(int TrustCode, string Years)
        {
            lbl.LoadData(TrustCode, Years);
            List<string> AccountCodes = accSheet.GetAccounts();
            foreach (string str in AccountCodes)
            {
                accSheet.SetAccountingSheet(str);
                List<Accounts> Enteries = lbl.GetEntries(str);
                foreach (Accounts entry in Enteries)
                {
                    accSheet.WriteAccounts(entry.Date, entry.No, entry.Account, entry.Name, entry.Particulars,
                        entry.Debit, entry.Credit);
                }
            }
        }

        void WriteStatement(AccountingSheets Sheet, string SheetName, string Total)
        {
            Sheet.SetAccountingSheet(SheetName); 
            accSheet.WriteStatement(Total);
        }

        void WriteAccount(AccountingSheets Sheet, string AccountName, string Date, string Account, string Particulars, string Debit, string Credit, int Count)
        {
            Sheet.SetAccountingSheet(AccountName); 
            accSheet.WriteAccounts(Date, "", Account, "", Particulars, Debit, Credit, Count);
        }

        public void YearlyStatement(int TrustCode, string Years)
        {
            accSheet = th.GetYearlyStatement(TrustCode);
            ExportAccounts(TrustCode, Years);

            Dictionary<string, string> yearlytotals = th.GetYearlyTotals(TrustCode, lbl);
            foreach (string key in yearlytotals.Keys)
            {
                WriteStatement(accSheet, key, yearlytotals[key]);
            }

            DateTime endDate = util.GetEndDate(Years);
            string endDateStr = util.FormatDate(endDate);

            List<Accounts> closingentries = th.GetClosingEntries(TrustCode, lbl, endDateStr);
            foreach (Accounts closingentry in closingentries)
            {
                WriteAccount(accSheet, closingentry.Account, closingentry.Date, closingentry.ContraAccount, closingentry.Particulars,
                    closingentry.Debit, closingentry.Credit, lbl.GetAccountEntriesCount(closingentry.Account));
            }
        }

        public List<Accounts> TrialBalance(int TrustCode, string Years)
        {
            lbl.LoadData(TrustCode, Years);

            return th.GetTrialBalance(TrustCode, lbl);
        }

    }
}
