using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace Hulen.Reporting.Services
{
    public class AccountInfoReport
    {
        private readonly Templates _templates = new Templates();

        public void GeneratePdf()
        {
            Workbook workbook = _templates.GetAccountInfoReportTemplate();

            string paramExportFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Temp/accountInfo";
            XlFixedFormatType paramExportFormat = XlFixedFormatType.xlTypePDF;
            XlFixedFormatQuality paramExportQuality = XlFixedFormatQuality.xlQualityStandard;
            bool paramOpenAfterPublish = true;
            bool paramIncludeDocProps = true;
            bool paramIgnorePrintAreas = true;
            object paramFromPage = Type.Missing;
            object paramToPage = Type.Missing;
            if (workbook != null)
                workbook.ExportAsFixedFormat(paramExportFormat,
                paramExportFilePath, paramExportQuality,
                paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
                paramToPage, paramOpenAfterPublish,
                Type.Missing);
        }
    }
}
