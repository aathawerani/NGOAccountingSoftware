using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    abstract class Receipt
    {
        protected Utils util = new Utils();
        protected Dictionary<string, string> StringToReplace = new Dictionary<string, string>();
        protected string FileName = "";
        protected string DestFileName = "";

        public void PrintReceipt()
        {
            Response result = new Response(ResponseType.Success, "Receipt printed successfully");
            FileManager fm = new FileManager("receipt", DestFileName);
            fm.CreateFile();
            if (fm == null) 
                throw new ReceiptException("Could not open receipt file");
            fm.CopyTo("master/", FileName);
            string FullName = fm.GetFileName();
            if(FullName == "") 
                throw new ReceiptException("Could not get file name");
            DocWriter dw = new DocWriter();
            dw.SearchAndReplace(FullName, StringToReplace);
            
            PrintDocument pd = new PrintDocument();
            FullName = fm.GetAbsolutePath();
            pd.Print(FullName);
        }
    }
}
