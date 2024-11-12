using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class Tenant
    {
        Utils utils = new Utils();
        public string Name { get; set; }
        public string Space { get; set; }
        public string No { get; set; }
        public string MRent { get; set; }
        public string WCharges { get; set; }
        public string CNIC { get; set; }
        public string ID { get; set; }
        public static List<Tenant> GetTenant(DataSet Tenant)
        {
            List<Tenant> Result = new List<Tenant>();
            foreach (DataRow row in Tenant.Tables[0].Rows)
            {
                Tenant temp = new Tenant();
                if (row["TenantName"] == null)
                {
                    throw new TenantException("TenantName not found");
                }
                temp.Name = row["TenantName"].ToString();
                if (row["TenantSpaceType"] == null)
                {
                    throw new TenantException("TenantSpaceType not found");
                }
                temp.Space = row["TenantSpaceType"].ToString();
                if (row["TenantSpaceNo"] == null)
                {
                    throw new TenantException("TenantSpaceNo not found");
                }
                temp.No = row["TenantSpaceNo"].ToString();
                if (row["TenantRentPerMonth"] == null)
                {
                    throw new TenantException("TenantRentPerMonth not found");
                }
                temp.MRent = row["TenantRentPerMonth"].ToString();
                if (row["TenantWaterChargesPerMonth"] == null)
                {
                    throw new TenantException("TenantWaterChargesPerMonth not found");
                }
                temp.WCharges = row["TenantWaterChargesPerMonth"].ToString();
                if (row["CNIC"] == null)
                {
                    throw new TenantException("CNIC not found");
                }
                temp.CNIC = row["CNIC"].ToString();
                if (row["ID"] == null)
                {
                    throw new TenantException("ID not found");
                }
                temp.ID = row["ID"].ToString();
                Result.Add(temp);
            }
            return Result;
        }
    }
}
