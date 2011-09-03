<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.ResultIndexWebModel>" %>
<%@ Import Namespace="Hulen.WebCode.MvcBase" %>

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
            <td class="contentcell_centeralign">
                <%: item.Year%>
            </td>
            <td class="contentcell_centeralign">
                <%: item.Period%>
            </td>
            <td class="contentcell_centeralign">
                <%: item.UsedBudget %>
            </td>
            <td class="contentcell_centeralign">
                <%: item.Comment %>
            </td>
            <td class="contentcell_centeralign">
                <%: Html.ActionLink("Åpne", "OpenReport", new { year = item.Year, period = item.Period, item.UsedBudget })%> |
                <%: Html.ActionLink("Slett", "Delete", new { year = item.Year, period = item.Period })%>
            </td>
        </tr>
    
    <% } %>

    </table>


    
    <p> 
        <% if(ViewBase.UserHasAccessTo("TEST")){%>
            <%: Html.ActionLink("Importer regnskapsrapport", "ImportResult") %>
        <% } %>
    </p>
     

</asp:Content>
