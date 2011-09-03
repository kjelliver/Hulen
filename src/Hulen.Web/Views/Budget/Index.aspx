<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.BudgetIndexWebModel>" %>
<%@ Import Namespace="Hulen.WebCode.MvcBase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Budsjett
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Driftsbudsjett for Hulen</h2>

    <table class="contenttable" cellspacing="0">
        <tr>
            <th class="contentheader">
                År
            </th>
            <th class="contentheader">
                Status
            </th>
            <th class="contentheader">
                Kommentar
            </th>
            <th class="contentheader"></th>
        </tr>

    <% foreach (var item in Model.StoredBudgets ) { %>
    
        <tr>
            <td class="contentcell_centeralign">
                <%: item.Year%>
            </td>
            <td class="contentcell_centeralign">
                <%: item.BudgetStatus%>
            </td>
            <td class="contentcell_centeralign">
                <%: item.Comment %>
            </td>
            <td class="contentcell_centeralign">
                <%: Html.ActionLink("Åpne", "OpenReport", new { year = item.Year, budgetStatus = item.BudgetStatus })%> 

                <% if (ViewBase.UserHasAccessTo("FEAURE_BUDGET_EDIT")){%>
                | <%: Html.ActionLink("Slett", "Delete", new { year = item.Year, budgetStatus = item.BudgetStatus })%>
                <% } %>

            </td>
        </tr>
    
    <% } %>

    </table>

    <p> 
        <% if (ViewBase.UserHasAccessTo("FEAURE_BUDGET_EDIT")){%>
        <%: Html.ActionLink("Importer budsjett", "ImportBudget") %>
        <% } %>
    </p>

</asp:Content>
