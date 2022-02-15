using System;
using Chocoholics_Anonymous.CA_Models;

namespace Chocoholics_Anonymous.CA_DataDriver
{
    public class ReportDriver
    {
        private String path = "../../../Reports/";
        public void print(String fileName, String fileType, Report report)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{path}{fileName}.{fileType}"))
            {
                if(!report.Header.Equals("")) file.WriteLine(report.Header);
                if(!report.Body.Equals("")) file.WriteLine(report.Body);
                if(!report.Footer.Equals("")) file.WriteLine(report.Footer);
            }
        }
    }
}