using System;
using System.IO;
using System.Windows;

namespace VeatUAM._Services
{
    public static class LoggerService
    {
        private const string PATH = "../Logs/";
        private static StreamWriter streamWriter { get; set; }
        private static string FileName { get; set; }
        private static string Action { get; set; }

        public static bool NewLog(string action)
        {
            Action = action;
            FileName = DateTime.Now.ToString("yyyy-MM-dd");
            var todayDateTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            var header = $"[{todayDateTime} - {AuthenticationService.Email}]";
            try
            {
                streamWriter = new StreamWriter(PATH + FileName + ".log", true);
                streamWriter.WriteLine($"{header} : {Action}");
                streamWriter.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}