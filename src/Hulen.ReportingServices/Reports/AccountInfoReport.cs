using System.Text;
using Hulen.Objects.DTO;
using Hulen.Storage.DTO;
using Hulen.Storage.Repositories;

namespace Hulen.ReportingServices.Reports
{
    public class AccountInfoReport : IReportingServices
    {
        private readonly AccountInfoRepository _repository = new AccountInfoRepository();
        readonly StringBuilder _sb = new StringBuilder();

        public string GenerateCssStyle()
        {
            _sb.Clear();
            _sb.AppendLine("<style type=\"text/css\">");
            _sb.AppendLine("h1 {color:red}");
            _sb.AppendLine(".rowHeader { height:30px; background-color: #AAAAAA; font-weight: bold; }");
            _sb.AppendLine(".columnAccNr { width:7%;}");
            _sb.AppendLine(".columnAccName {width:24%;}");
            _sb.AppendLine(".columnAccData {width:16%; text-align:center;}");
            _sb.AppendLine(".columnAccYear {width:5%; text-align:center;}");
            _sb.AppendLine("table {border: 1px solid black;}");
            _sb.AppendLine("td {border: 1px solid black;}");
            _sb.AppendLine("</style>");
            return _sb.ToString();
        }

        public string GenerateHtmlBody()
        {
            _sb.Clear();
            OpenTable();
            GenerateTableHeader();
            GenerateTableData();
            CloseTable();
            return _sb.ToString();
        }

        private void OpenTable()
        {
            _sb.AppendLine("<table cellspacing=0 width=1024px>");
        }

        private void GenerateTableHeader()
        {
            _sb.AppendLine("<tr class=rowHeader>");

            _sb.AppendLine("<td class=columnAccNr>");
            _sb.AppendLine("Kontonr.");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccName>");
            _sb.AppendLine("Kontonavn");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccData>");
            _sb.AppendLine("Resultatrapport");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccData>");
            _sb.AppendLine("Driftsdel");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccData>");
            _sb.AppendLine("Ukesdel");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccData>");
            _sb.AppendLine("Inntekt/Utgift");
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccYear>");
            _sb.AppendLine("År");
            _sb.AppendLine("</td>");

            _sb.AppendLine("</tr>");
        }

        private void GenerateTableData()
        {
            var accountInfos = _repository.GetAll();
            foreach(var accountInfo in accountInfos)
            {
                AddDataRow(accountInfo);
            }
        }

        private void CloseTable()
        {
            _sb.AppendLine("</table>");
        }
    
        private void AddDataRow(AccountInfoDTO accountInfo)
        {
            _sb.AppendLine("<tr>");

            _sb.AppendLine("<td class=columnAccNr>");
            _sb.AppendLine(accountInfo.AccountNumber.ToString());
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccName>");
            _sb.AppendLine(accountInfo.AccountName);
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccData>");
            _sb.AppendLine(accountInfo.ResultReportCategory.ToString());
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccData>");
            _sb.AppendLine(accountInfo.PartsReportCategory.ToString());
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccData>");
            _sb.AppendLine(accountInfo.WeekCategory.ToString());
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccData>");
            _sb.AppendLine(accountInfo.IsIncome.ToString());
            _sb.AppendLine("</td>");

            _sb.AppendLine("<td class=columnAccYear>");
            _sb.AppendLine("År");
            _sb.AppendLine("</td>");

            _sb.AppendLine("</tr>");
        }
    }
}
