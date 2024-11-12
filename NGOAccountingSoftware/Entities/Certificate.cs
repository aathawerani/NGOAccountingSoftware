using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class Certificate
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
        public string Folio { get; set; }
        public string No { get; set; }
        string _Amount;
        public string Amount {
            get
            {
                return utils.FormatAmount(_Amount);
            }
            set
            {
                _Amount = utils.RawAmount(value);
            }
        }
        public string Maturity { get; set; }
        public string Status { get; set; }
        public string ID { get; set; }
        public string PurchaseDate { get; set; }
        public string SaleDate { get; set; }

    }
}
