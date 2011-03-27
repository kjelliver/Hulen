using System.Drawing;
using Microsoft.Office.Interop.Excel;

namespace Hulen.Reporting
{
    public class Templates
    {
        private readonly Application _application = new Application();
        private readonly object _misValue = System.Reflection.Missing.Value;

        public Workbook GetAccountInfoReportTemplate()
        {
            Workbook workBook = _application.Workbooks.Add(_misValue);
            Worksheet workSheet = (Worksheet)workBook.Worksheets.Item[1];

            //Document
            Range chartRange = workSheet.Range["A1", "G47"];
            chartRange.Borders[XlBordersIndex.xlEdgeBottom].Color = Color.Black;
            chartRange.Borders[XlBordersIndex.xlEdgeLeft].Color = Color.Black;
            chartRange.Borders[XlBordersIndex.xlEdgeRight].Color = Color.Black;
            chartRange.Borders[XlBordersIndex.xlEdgeTop].Color = Color.Black;
            chartRange.ColumnWidth = 12.2;

            return workBook;
        }
    }
}
