using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class Rent
    {
        Utils utils = new Utils();
        string _Date;
        public string Date
        {
            get
            {
                return _Date;
            }
            set
            {
                try
                {
                    _Date = utils.FormatDateGB(value);
                }
                catch (Exception e)
                {
                    _Date = utils.FormatDateUS(value);
                }
            }
        }
        public string No { get; set; }
        public string Rpart { get; set; }
        public string RApart 
        { 
            get; 
            set; 
        }
        public string Wpart { get; set; }
        public string WApart { get; set; }
        string _MRent;
        public string MRent {
            get
            {
                return utils.FormatAmount(_MRent);
            }
            set
            {
                _MRent = utils.RawAmount(value);
            }
        }
        string _WCharges;
        public string WCharges {
            get
            {
                return utils.FormatAmount(_WCharges);
            }
            set
            {
                _WCharges = utils.RawAmount(value);
            }
        }
        public string Name { get; set; }
        public string FDate { get; set; }
        public string TDate { get; set; }
        string _RArears;
        public string RArears {
            get
            {
                return utils.FormatAmount(_RArears);
            }
            set
            {
                _RArears = utils.RawAmount(value);
            }
        }
        string _WArears;
        public string WArears {
            get
            {
                return utils.FormatAmount(_WArears);
            }
            set
            {
                _WArears = utils.RawAmount(value);
            }
        }
        string _TotalRent;
        public string TotalRent {
            get
            {
                return utils.FormatAmount(_TotalRent);
            }
            set
            {
                _TotalRent = utils.RawAmount(value);
            }
        }
        string _TotalWCharges;
        public string TotalWCharges {
            get
            {
                return utils.FormatAmount(_TotalWCharges);
            }
            set
            {
                _TotalWCharges = utils.RawAmount(value);
            }
        }
        string _Total;
        public string Total {
            get
            {
                return utils.FormatAmount(_Total);
            }
            set
            {
                _Total = utils.RawAmount(value);
            }
        }
        public string ID 
        { 
            get; 
            set; 
        }

        public static List<Rent> GetRent(DataSet Rent)
        {
            List<Rent> Result = new List<Rent>();
            foreach (DataRow row in Rent.Tables[0].Rows)
            {
                Rent temp = new Rent();
                if (row["ID"] == null)
                {
                    throw new RentException("ID not found");
                }
                temp.ID = row["ID"].ToString();
                if (row["RentDate"] == null)
                {
                    throw new RentException("RentDate not found");
                }
                temp._Date = row["RentDate"].ToString();
                if (row["RentSerialNo"] == null)
                {
                    throw new RentException("RentSerialNo not found");
                }
                temp.No = row["RentSerialNo"].ToString();
                if (row["RentPerMonth"] == null)
                {
                    throw new RentException("RentPerMonth not found");
                }
                temp.MRent = row["RentPerMonth"].ToString();
                if (row["RentWaterchargesPerMonth"] == null)
                {
                    throw new RentException("RentWaterchargesPerMonth not found");
                }
                temp.WCharges = row["RentWaterchargesPerMonth"].ToString();
                if (row["RentTenantName"] == null)
                {
                    throw new RentException("RentTenantName not found");
                }
                temp.Name = row["RentTenantName"].ToString();
                if (row["RentFromDate"] == null)
                {
                    throw new RentException("RentFromDate not found");
                }
                temp.FDate = row["RentFromDate"].ToString();
                if (row["RentToDate"] == null)
                {
                    throw new RentException("RentToDate not found");
                }
                temp.TDate = row["RentToDate"].ToString();
                if (row["RentArears"] == null)
                {
                    throw new RentException("RentArears not found");
                }
                temp.RArears = row["RentArears"].ToString();
                if (row["RentWaterChargesArears"] == null)
                {
                    throw new RentException("RentWaterChargesArears not found");
                }
                temp.WArears = row["RentWaterChargesArears"].ToString();
                if (row["RentTotalRent"] == null)
                {
                    throw new RentException("RentTotalRent not found");
                }
                temp.TotalRent = row["RentTotalRent"].ToString();
                if (row["RentTotalWaterCharges"] == null)
                {
                    throw new RentException("RentTotalWaterCharges not found");
                }
                temp.TotalWCharges = row["RentTotalWaterCharges"].ToString();
                if (row["RentTotalAmount"] == null)
                {
                    throw new RentException("RentTotalAmount not found");
                }
                temp.Total = row["RentTotalAmount"].ToString();
                if (row["RentParticulars"] == null)
                {
                    throw new RentException("RentParticulars not found");
                }
                temp.Rpart = row["RentParticulars"].ToString();
                if (row["RentWaterParticulars"] == null)
                {
                    throw new RentException("RentWaterParticulars not found");
                }
                temp.Wpart = row["RentWaterParticulars"].ToString();
                if (row["RentArearsParticulars"] == null)
                {
                    throw new RentException("RentArearsParticulars not found");
                }
                temp.RApart = row["RentArearsParticulars"].ToString();
                if (row["RentWaterArearsParticulars"] == null)
                {
                    throw new RentException("RentWaterArearsParticulars not found");
                }
                temp.WApart = row["RentWaterArearsParticulars"].ToString();
                Result.Add(temp);
            }
            return Result;
        }


    }
}
