using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class DocWriter
    {
        public void SearchAndReplace(string DocumentName, Dictionary<string, string> StringToReplace)
        {
            WordprocessingDocument wordDoc = WordprocessingDocument.Open(DocumentName, true);
            if (wordDoc == null)
            {
                throw new DocWriterException("SearchAndReplace: Could not open document");
            }
            string docText = null;
            StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream());
            if(sr == null)
            {
                throw new DocWriterException("SearchAndReplace: Could not create stream reader");
            }
            docText = sr.ReadToEnd();

            foreach (string key in StringToReplace.Keys)
            {
                Regex regexText = new Regex(key);
                docText = regexText.Replace(docText, StringToReplace[key]);
            }
            StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create));
            if (sw == null)
            {
                throw new DocWriterException("SearchAndReplace: Could not create stream writer");
            }
            sw.Write(docText);
            sw.Flush();
            sw.Close();
            sr.Close();
            wordDoc.Close();
        }
    }
}
