using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class MajlisDAL : DAL
    {
        public void InsertMajlisBill(string MajlisDate, string MajlisIslamicDay, string MajlisIslamicMonth, string MajlisIslamicYear, string MajlisSerialNo
            , string MajlistFromTime, string MajlisToTime, string MajlisName, string MajlisMilkQuantity, string MajlisMilkPricePerUnit, string MajlisMilkTotal,
            string MajlisSugarQuantity, string MajlisSugarPricePerUnit, string MajlisSugarTotal, string MajlisTeaQuantity, string MajlisTeaPricePerUnit,
            string MajlisTeaTotal, string MajlisSaffron, string MajlisCardamoms, string MajlisPistachios, string MajlisIce, string MajlisEssence,
            string MajlisMiscellaneous, string MajlisLightsFans, string MajlisGas, string MajlisLoudSpeaker, string MajlisMolana, string MajlisTotalAmount,
            string MajlisMiscDesc)
        {
            string query = string.Format("INSERT INTO `" + DBName + "`.`majlis`(`MajlisDate`,`MajlisIslamicDay`,`MajlisIslamicMonth`,`MajlisIslamicYear`"
                + ",`MajlisSerialNo`,`MajlistFromTime`,`MajlisToTime`,`MajlisName`,`MajlisMilkQuantity`,`MajlisMilkPricePerUnit`,`MajlisMilkTotal`,"
                + "`MajlisSugarQuantity`,`MajlisSugarPricePerUnit`,`MajlisSugarTotal`,`MajlisTeaQuantity`,`MajlisTeaPricePerUnit`,`MajlisTeaTotal`,"
                + "`MajlisSaffron`,`MajlisCardamoms`,`MajlisPistachios`,`MajlisIce`,`MajlisEssence`,`MajlisMiscellaneous`,`MajlisMiscellaneousDesc`,"
                + "`MajlisLightsFans`,`MajlisGas`,`MajlisLoudSpeaker`,`MajlisMolana`,`MajlisTotalAmount`)"
                + "VALUES(\'{0}\',\'{1}\',\'{2}\',\'{3}\',\'{4}\',\'{5}\',\'{6}\',\'{7}\',\'{8}\',\'{9}\',\'{10}\',\'{11}\',\'{12}\',\'{13}\',"
                + "\'{14}\',\'{15}\',\'{16}\',\'{17}\',\'{18}\',\'{19}\',\'{20}\',\'{21}\',\'{22}\',\'{23}\',\'{24}\',\'{25}\',\'{26}\',\'{27}\',\'{28}\');"
            , MajlisDate, MajlisIslamicDay, MajlisIslamicMonth, MajlisIslamicYear, MajlisSerialNo, MajlistFromTime, MajlisToTime, MajlisName,
            MajlisMilkQuantity, MajlisMilkPricePerUnit, MajlisMilkTotal, MajlisSugarQuantity, MajlisSugarPricePerUnit, MajlisSugarTotal, MajlisTeaQuantity,
            MajlisTeaPricePerUnit, MajlisTeaTotal, MajlisSaffron, MajlisCardamoms, MajlisPistachios, MajlisIce, MajlisEssence, MajlisMiscellaneous, MajlisMiscDesc,
            MajlisLightsFans, MajlisGas, MajlisLoudSpeaker, MajlisMolana, MajlisTotalAmount);

            ExecuteNonQuery(query);
        }
    }
}
