using System;
using System.Text;
using Hulen.Storage.DTO;
using Hulen.Storage.Repositories;

namespace Hulen.ReportingServices.Reports
{
    public class AccountInfoReport : IReportingServices
    {
        private AccountInfoRepository _repository = new AccountInfoRepository();
        StringBuilder sb = new StringBuilder();

        public string GenerateHtmlBody()
        {
            OpenTable();
            GenerateTableHeader();
            GenerateTableData();
            CloseTable();
            return sb.ToString();
        }

        private void OpenTable()
        {
            sb.AppendLine("<table cellspacing=0 width=1024px>");
        }

        private void GenerateTableHeader()
        {
            sb.AppendLine("<tr class=rowHeader>");

            sb.AppendLine("<td class=columnAccNr>");
            sb.AppendLine("Kontonr.");
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccName>");
            sb.AppendLine("Kontonavn");
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccData>");
            sb.AppendLine("Resultatrapport");
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccData>");
            sb.AppendLine("Driftsdel");
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccData>");
            sb.AppendLine("Ukesdel");
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccData>");
            sb.AppendLine("Inntekt/Utgift");
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccYear>");
            sb.AppendLine("År");
            sb.AppendLine("</td>");

            sb.AppendLine("</tr>");
        }

        private void GenerateTableData()
        {
            var accountInfos = _repository.GetAllAccountCategories();
            foreach(var accountInfo in accountInfos)
            {
                AddDataRow(accountInfo);
            }
        }

        private void CloseTable()
        {
            sb.AppendLine("</table>");
        }
    
        private void AddDataRow(AccountInfoDTO accountInfo)
        {
            sb.AppendLine("<tr>");

            sb.AppendLine("<td class=columnAccNr>");
            sb.AppendLine(accountInfo.AccountNumber.ToString());
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccName>");
            sb.AppendLine(accountInfo.AccountName);
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccData>");
            sb.AppendLine(accountInfo.ResultReportCategory.ToString());
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccData>");
            sb.AppendLine(accountInfo.PartsReportCategory.ToString());
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccData>");
            sb.AppendLine(accountInfo.WeekCategory.ToString());
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccData>");
            sb.AppendLine(accountInfo.IsIncome.ToString());
            sb.AppendLine("</td>");

            sb.AppendLine("<td class=columnAccYear>");
            sb.AppendLine("År");
            sb.AppendLine("</td>");

            sb.AppendLine("</tr>");
        }
    }
}
