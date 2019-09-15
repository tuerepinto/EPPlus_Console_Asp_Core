using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CargaDados
{
    public class CargaModeloAeronave
    {
        private const string access_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6WyJ0dWVyZXBpbnRvQGdtYWlsLmNvbSIsInR1ZXJlcGludG9AZ21haWwuY29tIl0sImp0aSI6IjU1NTIwNDk2NmFlMTRkMGJhYjc2NzVjNDFmZjBmMDNkIiwibmJmIjoxNTY2OTI3Njg3LCJleHAiOjE1NjY5MzQ4ODcsImlhdCI6MTU2NjkyNzY4NywiaXNzIjoiQURBTSBDb250cm9sZSBPcGVyYWNpb25hbCIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0In0.HQZwHnoQFJ-KPxTwBhm-lAqArhOjksNXHJ3qrD7JgXU";

        private LogTxt _logTxt;
        private LogTxt LogTxt
        {
            get { return _logTxt ?? (_logTxt = new LogTxt()); }
        }

        public void Read(FileInfo excelLocal, int aba)
        {
            using (ExcelPackage package = new ExcelPackage(excelLocal))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[aba];
            }
        }
    }
}
