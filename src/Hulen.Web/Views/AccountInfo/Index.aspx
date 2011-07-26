<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.AccountInfoWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Kontooversikt
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>

    <% if (Model.AccountInfos != null) {%>

        <h2>Kontooversikt</h2>

        <table class="contenttable" cellspacing="0">
            <tr>
                <th class="contentheader">
                    Kontonr.
                </th>
                <th class="contentheader">
                    Kontonavn
                </th>
                <th class="contentheader">
                    Resultatrapport
                </th>
                <th class="contentheader">
                    Dritsdel
                </th>
                <th class="contentheader">
                    Ukesregn
                </th>
                <th class="contentheader">
                    Inntekt/Utgift
                </th>
                <th class="contentheader"></th>
            </tr>

        <% foreach (var item in Model.AccountInfos ) { %>
    
            <tr>
                <td class="contentcell">
                    <%: item.AccountNumber%>
                </td>
                <td class="contentcell">
                    <%: item.AccountName%>
                </td>
                <td class="contentcell">
                    <%: item.ResultReportCategory%>
                </td>
                <td class="contentcell">
                    <%: item.PartsReportCategory%>
                </td>
                <td class="contentcell">
                    <%: item.WeekCategory%>
                </td>
                <td class="contentcell">
                    <%: item.IsIncome%>
                </td>
                <td class="contentcell">
                    <%: Html.ActionLink("Rediger", "Edit", new { id = item.Id })%> |
                    <%: Html.ActionLink("Slett", "Delete", new { id = item.Id })%>
                </td>
            </tr>
    
        <% } %>
    <% } %>
    </table>

    <p>
        <%: Html.ActionLink("Ny konto", "Create") %> | 
        <%: Html.ActionLink("Åpne rapport", "OpenReport") %>
    </p>

</asp:Content>

