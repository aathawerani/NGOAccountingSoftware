using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TrustApplication
{
    class CertificateDAL : DAL
    {
        public void UpdateCertificate(int TrustCode, string CertificateNo, string FolioNo, string Type, string Date)
        {
            string query = string.Format("UPDATE `" + DBName + "`.`certificates` SET `CertificateStatus` = 'MATURED', SaleDate = \'{3}\' WHERE `TrustCode` = \'{0}\'"
                + "and `CertificateNos` = \'{1}\' and `CertificateFolioNo` = \'{2}\' and `CertificateType` = \'{3}\';", TrustCode, CertificateNo, FolioNo, Type, Date);
            ExecuteNonQuery(query);
        }
        public void InsertCertificate(int TrustCode, string CertificateDate, string FolioNo, string CertificateNo, string Amount, string Type, string Status, 
            string Maturity, string PurchaseDate)
        {
            string query = string.Format("INSERT INTO `" + DBName + "`.`certificates`(`TrustCode`, `CertificateDate`, `CertificateNos`, `CertificateFolioNo`, "
                + "`CertificateType`, `CertificateAmount`, `CertificateStatus`, `MaturityDate`, `PurchaseDate`)VALUES(\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\', "
                + "\'{6}\', \'{7}\', \'{8}\');", TrustCode, CertificateDate, CertificateNo, FolioNo, Type, Amount, Status, Maturity, PurchaseDate);
            ExecuteNonQuery(query);
        }
        public void UpdateCertificate(string Date, string FolioNo, string CertificateNo, string Amount, string Maturity, string ID)
        {
            string query = string.Format("UPDATE `" + DBName + "`.`certificates` SET `CertificateDate` = \'{0}\', `CertificateNos` = \'{1}\', "
                + "`CertificateFolioNo` = \'{2}\', `CertificateAmount` = \'{3}\', `MaturityDate` = \'{4}\' WHERE `ID` = \'{5}\'; "
                , Date, CertificateNo, FolioNo, Amount, Maturity, ID);
            ExecuteNonQuery(query);
        }
        public DataSet GetCertificates(string TrustCode, string Type)
        {
            string query = string.Format("select ID, CertificateDate, CertificateNos, CertificateFolioNo, CertificateAmount, MaturityDate, CertificateStatus "
                + ", PurchaseDate, SaleDate from " + DBName + ".certificates where TrustCode = \'{0}\' and CertificateStatus = \'ACTIVE\' and CertificateType = \'{1}\' "
                + "order by CertificateFolioNo;", TrustCode, Type);
            return ExecuteQueryGetTable(query);
        }
        public DataSet GetCertificatesbyDate(string TrustCode, string Type, string StartDate, string EndDate)
        {
            string query = string.Format("select ID, CertificateDate, CertificateNos, CertificateFolioNo, CertificateAmount, MaturityDate, CertificateStatus"
                + ", PurchaseDate, SaleDate from " + DBName + ".certificates where TrustCode = \'{0}\' and CertificateType = \'{1}\' "
                + " and ( (STR_TO_DATE(CertificateDate, '%d-%m-%Y') >= STR_TO_DATE(\'{2}\', '%d-%m-%Y') "
                + " and STR_TO_DATE(CertificateDate, '%d-%m-%Y') <= STR_TO_DATE(\'{3}\', '%d-%m-%Y'))"
                + " or (STR_TO_DATE(SaleDate, '%d-%m-%Y') >= STR_TO_DATE(\'{2}\', '%d-%m-%Y') "
                + " and STR_TO_DATE(SaleDate, '%d-%m-%Y') <= STR_TO_DATE(\'{3}\', '%d-%m-%Y')) )"
                + "order by STR_TO_DATE(CertificateDate, '%d-%m-%Y');", TrustCode, Type, StartDate, EndDate);
            return ExecuteQueryGetTable(query);
        }
        public DataSet GetAllCertificates(string TrustCode, string Type)
        {
            string query = string.Format("select ID, CertificateDate, CertificateNos, CertificateFolioNo, CertificateAmount, MaturityDate, CertificateStatus "
                + ", PurchaseDate, SaleDate from " + DBName + ".certificates where TrustCode = \'{0}\' and CertificateType = \'{1}\' "
                + "order by CertificateFolioNo;", TrustCode, Type);
            return ExecuteQueryGetTable(query);
        }

        public List<string> GetCertificateTypes(string TrustCode)
        {
            string query = string.Format("select distinct certificatetype from " + DBName + ".certificates where TrustCode = \'{0}\';", TrustCode);
            return ExecuteQueryGetRows(query);
        }

        public DataSet GetCertificatesByFolio(string TrustCode, string Type, string FolioNo)
        {
            string query = string.Format("select ID, CertificateDate, CertificateNos, CertificateFolioNo, CertificateAmount, MaturityDate, CertificateStatus  "
                + ", PurchaseDate, SaleDate from " + DBName + ".certificates where TrustCode = \'{0}\' and CertificateStatus = \'ACTIVE\' and CertificateType = \'{1}\' and CertificateFolioNo = \'{2}\';",
                TrustCode, Type, FolioNo);
            return ExecuteQueryGetTable(query);
        }

        public DataSet GetCertificatesByCertNo(string TrustCode, string Type, string CertNo)
        {
            string query = string.Format("select ID, CertificateDate, CertificateNos, CertificateFolioNo, CertificateAmount, MaturityDate, CertificateStatus  "
                + ", PurchaseDate, SaleDate from " + DBName + ".certificates where TrustCode = \'{0}\' and CertificateStatus = \'ACTIVE\' and CertificateType = \'{1}\' and CertificateNos = \'{2}\';",
                TrustCode, Type, CertNo);
            return ExecuteQueryGetTable(query);
        }

        /*public List<string> GetCertificateByNo(string TrustCode, string CertificateNo, string FolioNo, out Response response)
        {
            string query = string.Format("select * from " + DBName + ".certificates where CertificateNos like \'%{0}%\' or CertificateFolioNo like \'%{1}%\' ", 
                TrustCode, CertificateNo, FolioNo);
            return ExecuteQueryGetColumns(query, out response);
        }*/

        public void InsertCertificate(string TrustCode, string CertificateDate, string FolioNo, string CertificateNo, string Amount, string Type, string Status,
            string Maturity, string PurchaseDate, string SaleDate)
        {
            string query = string.Format("INSERT INTO `" + DBName + "`.`certificates`(`TrustCode`, `CertificateDate`, `CertificateNos`, `CertificateFolioNo`, "
                + "`CertificateType`, `CertificateAmount`, `CertificateStatus`, `MaturityDate`, `PurchaseDate`, `SaleDate`) VALUES (\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\', "
                + "\'{6}\', \'{7}\', \'{8}\', \'{9}\');", TrustCode, CertificateDate, CertificateNo, FolioNo, Type, Amount, Status, Maturity, PurchaseDate, SaleDate);
            ExecuteNonQuery(query);
        }

        public void UpdateCertificate(string Date, string FolioNo, string CertificateNo, string Amount, string Maturity, string ID, string Status, 
            string SaleDate)
        {
            string query = string.Format("UPDATE `" + DBName + "`.`certificates` SET `CertificateDate` = \'{0}\', `CertificateNos` = \'{1}\', "
                + "`CertificateFolioNo` = \'{2}\', `CertificateAmount` = \'{3}\', `MaturityDate` = \'{4}\', `CertificateStatus` = \'{6}\',"
                + " `SaleDate` = \'{7}\' WHERE `ID` = \'{5}\'; ", Date, CertificateNo, FolioNo, Amount, Maturity, ID, Status, SaleDate);
            ExecuteNonQuery(query);
        }
    }
}
