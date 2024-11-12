using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class PrintDocument
    {
        public void Print(string FullName)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                Verb = "print",
                FileName = FullName //put the correct path here
            };
            bool result = p.Start();
            p.WaitForExit();
            if (!result)
            {
                throw new PrintDocumentException("Document printing failed");
            }
        }
    }
}
