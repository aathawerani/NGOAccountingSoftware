using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TrustApplication
{
    class Message
    {
        public static void ShowError(string message)
        {
            MessageBox.Show(message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            Logger logger = Logger.GetInstance();
            logger.WriteLog(DateTime.Now.ToString() + ": ERROR : " + message);
        }

        public static void ShowSuccess(string message)
        {
            MessageBox.Show(message, "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
            Logger logger = Logger.GetInstance();
            logger.WriteLog(DateTime.Now.ToString() + ": SUCCESS : " + message);
        }

        public static void ShowWarning(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "WARNING", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            Logger logger = Logger.GetInstance();
            logger.WriteLog(DateTime.Now.ToString() + ": WARNING : " + message);
        }
    }
}
