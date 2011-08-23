<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.ResultIndexWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Regnskapsrapporter
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Regnskapsrapporter for Hulen</h2>

    <table class="contenttable" cellspacing="0">
        <tr>
            <th class="contentheader">
                År
            </th>
            <th class="contentheader">
                Periode
            </th>
            <th class="contentheader">
                Brukt budsjett
            </th>
            <th class="contentheader">
                Kommentar
            </th>
            <th class="contentheader"></th>
        </tr>

    <% foreach (var item in Model.Results ) { %>
    
        <tr>
            <td class="contentcell">
                <%: item.Year%>
            </td>
            <td class="contentcell">
                <%: item.Period%>
            </td>
            <td class="contentcell">
                <%: item.UsedBudget %>
            </td>
            <td class="contentcell">
                <%: item.Comment %>
            </td>
            <td class="contentcell">
                <%: Html.ActionLink("Åpne", "OpenReport", new { year = item.Year, period = item.Period, item.UsedBudget })%> |
                <%: Html.ActionLink("Slett", "Delete", new { year = item.Year, period = item.Period })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p> 
        <%: Html.ActionLink("Importer regnskapsrapport", "ImportResult") %>
    </p>


</asp:Content>
