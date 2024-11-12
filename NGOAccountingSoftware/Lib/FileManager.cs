using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class FileManager
    {
        string Path, FileName, FullName;
        public FileManager(string path, string fileName)
        {
            Path = path;
            FileName = fileName;
            FullName = Path + "//" + FileName;

            
        }
        public void CreateFile()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            if (!File.Exists(FullName))
            {
                FileStream fs = File.Create(FullName);
                if (fs == null)
                {
                    throw new FileManagerException("Failed to create file");
                }
                fs.Close();
            }
        }

        public string GetFileName()
        {
            return FullName;
        }
        public void CopyTo(string sourceDIR, string sourceFileName)
        {
            File.Copy(sourceDIR + sourceFileName, GetFileName(), true);
        }
        public string GetAbsolutePath()
        {
            FileInfo fi = new FileInfo(FullName);
            return fi.FullName;
        }
    }
}
