using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class Logger
    {
        static string FileName;
        static StreamWriter writer;
        private Logger() { }

        private static Logger logger = new Logger();
        public static void SetLogger(FileManager logfile)
        {
            logfile.CreateFile();
            FileName = logfile.GetFileName();
            writer = new StreamWriter(FileName,true);
            if(writer == null)
            {
                throw new LoggerException("Could not create stream writer");
            }
        }

        public static Logger GetInstance()
        {
            return logger;
        }

        public void WriteLog(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }
    }
}
