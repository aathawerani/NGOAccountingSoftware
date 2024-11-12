using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class LoadAccountsBL : BL
    {
        Dictionary<string, List<Accounts>> AllAccountEntries;
        //AccountsBL abl = new AccountsBL(); 
        CertificateBL cbl = new CertificateBL(); 
        DAL dal = new DAL();
        public int GetAccountTotal(string AccountCode)
        {
            if (AllAccountEntries[AccountCode].Count > 0)
            {
                return int.Parse(util.RawAmount(AllAccountEntries[AccountCode].Last().Total));
            }
            else
            {
                return 0;
            }
        }
        public string GetAccountOpening(string AccountCode)
        {
            //string AccountTypeCode = GetAccountTypeCode(AccountCode);
            if (AllAccountEntries[AccountCode].Count > 0)
            {
                return util.RawAmount(AllAccountEntries[AccountCode].First().Total);
            }
            else
            {
                return "0";
            }
        }
        public int GetAccountEntriesCount(string AccountCode)
        {
            return AllAccountEntries[AccountCode].Count;
        }
        public int GetTotalDebit(string AccountCode)
        {
            List<Accounts> accountentries = AllAccountEntries[AccountCode];
            int TotalDebit = 0;
            foreach (Accounts entry in accountentries)
            {
                int debit = 0;
                if (int.TryParse(util.RawAmount(entry.Debit), out debit))
                {
                    TotalDebit += debit;
                }
                else
                {
                    throw new LoadAccountsBLException("Failed to parse amount");
                }
            }
            return TotalDebit;
        }
        public List<Accounts> ConvertCertificatesToAccounts(List<Certificate> certificates)
        {
            List<Accounts> Entries = new List<Accounts>();
            int index = 0;
            foreach (Certificate cert in certificates)
            {
                Accounts acc = new Accounts();
                acc.Date = "";
                acc.No = "";
                acc.Account = "";
                acc.Name = "";
                acc.Particulars = cert.Folio + " " + cert.No;
                acc.Debit = util.RawAmount(cert.Amount);
                Entries.Add(acc);
                index++;
            }
            return Entries;
        }
        /*public string GetParticulars(List<Accounts> Enteries, out Response response)
        {
            List<string> particulars = new List<string>();
            foreach (Accounts acc in Enteries)
            {
                string[] temp = acc.Particulars.Split(' ');
                foreach (string s in temp)
                {
                    particulars.Add(s);
                }
            }
            string formatted = "";
            foreach (string s in particulars)
            {
                if (formatted != "") formatted += ", ";
                formatted += "\'" + s + "\'";
            }
            return formatted;
        }*/

        public List<Accounts> GetEntries(string AccountCode)
        {
            return AllAccountEntries[AccountCode];
        }

        public void LoadData(int TrustCode, string Years)
        {
            //DateTime reportStartDate = util.GetStartDate(Years, out response);
            //DateTime reportEndDate = util.GetEndDate(Years, out response);
            List<string> AccountCodes = dal.GetAccountCodes(TrustCode.ToString());
            foreach (string str in AccountCodes)
            {
                List<string> certificateslist = th.GetCertificates(TrustCode);
                if (certificateslist.Contains(str))
                {
                    string formattedstartdate = util.FormatDate(gs.StartDate);
                    string formattedenddate = util.FormatDate(gs.EndDate);
                    List<Certificate> certificates = cbl.GetCertificatesbyDate(TrustCode, str, formattedstartdate,
                        formattedenddate);
                    AllAccountEntries.Add(str, ConvertCertificatesToAccounts(certificates));
                }
                else
                {
                    DataSet accountslist = GetAccountsByAccountCode(TrustCode, str);
                    AllAccountEntries.Add(str, GetAccounts(accountslist));
                }
            }
        }

    }
}
