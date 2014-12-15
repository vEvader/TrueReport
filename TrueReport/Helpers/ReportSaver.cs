using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TrueReport.Helpers
{
    public static class ReportSaver
    {

        public static bool SaveReport(byte[] reportBuffer, out string reportName)
        {
            bool result = true;

            BinaryWriter Writer = null;
            reportName = GetReportName();

            try
            {
                Writer = new BinaryWriter(File.OpenWrite(reportName));
                Writer.Write(reportBuffer);
                Writer.Flush();
                Writer.Close();
            }
            catch
            {
                //add loging
                result = false;
                reportName = string.Empty;
            }

            return result;
        }

        private static string GetReportName()
        {
            //generating newname for report
            return "report.pdf";
        }
    }
}