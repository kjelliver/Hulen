<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Hulen.Web.Models.AccountInfoModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Kontooversikt
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Kontooversikt</h2>

    <table>
        <tr>
            <th>
                Kontonr.
            </th>
            <th>
                Kontonavn
            </th>
            <th>
                Resultatrapport
            </th>
            <th>
                Dritsdel
            </th>
            <th>
                Ukesregn
            </th>
            <th>
                Inntekt/Utgift
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: item.AccountNumber %>
            </td>
            <td>
                <%: item.AccountName %>
            </td>
            <td>
                <%: item.ResultReportCategory %>
            </td>
            <td>
                <%: item.PartsReportCategory %>
            </td>
            <td>
                <%: item.WeekCategory %>
            </td>
            <td>
                <%: item.IsIncome %>
            </td>
            <td>
                <%: Html.ActionLink("Rediger", "Edit", new { id = item.Id }) %> |
                <%: Html.ActionLink("Slett", "Delete", new { id = item.Id })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Ny konto", "Create") %> | 
        <%: Html.ActionLink("Åpne rapport (.xls)", "OpenReportInExcel") %>
    </p>

</asp:Content>

