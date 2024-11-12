using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class CertificateBL : BL
    {
        CertificateDAL dal = new CertificateDAL(); //AccountsDAL adal = new AccountsDAL();

        public static List<Certificate> GetCertificate(DataSet Certificate)
        {
            List<Certificate> Result = new List<Certificate>();
            foreach (DataRow row in Certificate.Tables[0].Rows)
            {
                Certificate temp = new Certificate();
                if (row["ID"] == null)
                {
                    throw new CertificateBLException("ID not found");
                }
                temp.ID = row["ID"].ToString();
                if (row["CertificateDate"] == null)
                {
                    throw new CertificateBLException("CertificateDate not found");
                }
                temp.Date = row["CertificateDate"].ToString();
                if (row["CertificateNos"] == null)
                {
                    throw new CertificateBLException("CertificateNos not found");
                }
                temp.No = row["CertificateNos"].ToString();
                if (row["CertificateFolioNo"] == null)
                {
                    throw new CertificateBLException("CertificateFolioNo not found");
                }
                temp.Folio = row["CertificateFolioNo"].ToString();
                if (row["CertificateAmount"] == null)
                {
                    throw new CertificateBLException("CertificateAmount not found");
                }
                temp.Amount = row["CertificateAmount"].ToString();
                if (row["MaturityDate"] == null)
                {
                    throw new CertificateBLException("MaturityDate not found");
                }
                temp.Maturity = row["MaturityDate"].ToString();
                if (row["CertificateStatus"] == null)
                {
                    throw new CertificateBLException("CertificateStatus not found");
                }
                temp.Status = row["CertificateStatus"].ToString();
                if (row["PurchaseDate"] == null)
                {
                    throw new CertificateBLException("PurchaseDate not found");
                }
                temp.PurchaseDate = row["PurchaseDate"].ToString();
                if (row["SaleDate"] == null)
                {
                    throw new CertificateBLException("SaleDate not found");
                }
                temp.SaleDate = row["SaleDate"].ToString();
                Result.Add(temp);
            }
            return Result;
        }

        public List<Certificate> GetCertificates(int TrustCode, string Type)
        {
            return GetCertificate(dal.GetCertificates(TrustCode.ToString(), Type));
        }
        public List<Certificate> GetCertificatesbyDate(int TrustCode, string Type, string StartDate, string EndDate)
        {
            return GetCertificate(dal.GetCertificatesbyDate(TrustCode.ToString(), Type, StartDate, EndDate));
        }
        public List<Certificate> GetAllCertificates(int TrustCode, string Type)
        {
            return GetCertificate(dal.GetAllCertificates(TrustCode.ToString(), Type));
        }
        public List<Certificate> GetCertificatesByFolio(int TrustCode, string Type, string FolioNo)
        {
            return GetCertificate(dal.GetCertificatesByFolio(TrustCode.ToString(), Type, FolioNo));
        }
        public List<Certificate> GetCertificatesByCertNo(int TrustCode, string Type, string CertNo)
        {
            return GetCertificate(dal.GetCertificatesByCertNo(TrustCode.ToString(), Type, CertNo));
        }
        /*public List<Certificate> GetCertificatesByMaturity(int TrustCode, string Type, string Maturity)
        {
            return Certificate.GetCertificate(dal.GetCertificatesByMaturity(TrustCode.ToString(), Type, Maturity));
        }*/
        public void CertificateSold(int TrustCode, string Date, string CertificateNo, string FolioNo, string Amount, string Type)
        {
            string CertList = GetCertificates(TrustCode, Type, CertificateNo, FolioNo);
            string formattedamount = util.RawAmount(Amount);
            List<Accounts> maturedentries = th.GetCertificateMatured(TrustCode, Type, CertList, formattedamount,
                FolioNo, Date);
            AccountsDualInsert(TrustCode, maturedentries);
            string formatteddate = util.FormatDate(Date);
            dal.UpdateCertificate(TrustCode, CertificateNo, FolioNo, Type, formatteddate);
        }
        public void CertificatePurchased(int TrustCode, string PurchaseDate, string CertificateNo, string FolioNo, string Amount, 
            string Type, string MaturityYear, string CertificateDateStr)
        {
            DateTime CertificateDate = DateTime.Parse(CertificateDateStr);
            string Maturity = util.FormatDate(new DateTime(Convert.ToInt32(MaturityYear), CertificateDate.Month, CertificateDate.Day));
            string formattedamount = util.RawAmount(Amount);
            List<Accounts> purchasedentries = th.GetCertificatePurchased(TrustCode, Type, CertificateNo, formattedamount,
                FolioNo, PurchaseDate);
            AccountsDualInsert(TrustCode, purchasedentries);
            string formatteddate = util.FormatDate(CertificateDateStr);
            string formatteddate2 = util.FormatDate(PurchaseDate);
            dal.InsertCertificate(TrustCode, formatteddate, FolioNo, CertificateNo, formattedamount,
                Type, "ACTIVE", Maturity, formatteddate2);
        }
        public void CertificateUpdate(string Date, string CertificateNo, string FolioNo, string Amount, string MaturityYear, 
            string ID)
        {
            DateTime PurchaseDate = DateTime.Parse(Date);
            string Maturity = util.FormatDate(new DateTime(Convert.ToInt32(MaturityYear), PurchaseDate.Month, PurchaseDate.Day));
            string formatteddate = util.FormatDate(Date);
            string formattedamount = util.RawAmount(Amount);
            dal.UpdateCertificate(formatteddate, FolioNo, CertificateNo, formattedamount, Maturity, 
                ID);
        }
        public string GetCertificates(int TrustCode, string Type, string CertNo, string FolioNo)
        {
            string result = "";
            List<Certificate> CertList;
            if (FolioNo != "")
            {
                CertList = GetCertificatesByFolio(TrustCode, Type, FolioNo);
                result += FolioNo + " ";
                result += CertList[0].No + " ";
                result += "DATED " + CertList[0].Date;
            }
            else if (CertNo != "")
            {
                CertList = GetCertificatesByCertNo(TrustCode, Type, CertNo);
                result += CertList[0].No + " ";
                result += "DATED " + CertList[0].Date;
            }
            return result;
        }
        public void CertificateProfit(int TrustCode, string Date, string CertificateNo, string FolioNo, string Amount, string Type, string Tax)
        {
            string CertList = GetCertificates(TrustCode, Type, CertificateNo, FolioNo);
            string formattedamount = util.RawAmount(Amount);
            string formattedamount2 = util.RawAmount(Tax);
            List<Accounts> profitentries = th.GetCertificateProfit(TrustCode, CertList, formattedamount, formattedamount2
                , FolioNo, Date);
            AccountsDualInsert(TrustCode, profitentries);
        }

        public string GetNetProfit(string Profit, string Tax)
        {
            double profit = 0, tax = 0;
            if (double.TryParse(Profit, out profit) && (double.TryParse(Tax, out tax)))
            {
                return (profit - tax).ToString();
            }
            throw new CertificateBLException("Failed to parse profit and tax");
        }

        public List<string> GetCertificateTypes(string TrustCode)
        {
            return dal.GetCertificateTypes(TrustCode);
        }

        public void InsertCertificate(int TrustCode, string CertificateDate, string FolioNo, string CertificateNo, string Amount, string Type, string Status,
            string Maturity, string PurchaseDate, string SaleDate)
        {
            dal.InsertCertificate(TrustCode.ToString(), CertificateDate, FolioNo, CertificateNo, Amount, Type, Status,
            Maturity, PurchaseDate, SaleDate);
        }

        public void UpdateCertificate(string Date, string FolioNo, string CertificateNo, string Amount, string Maturity, string ID, 
            string Status, string SaleDate)
        {
            dal.UpdateCertificate(Date, FolioNo, CertificateNo, Amount, Maturity, ID, Status, SaleDate);
        }
    }
}
