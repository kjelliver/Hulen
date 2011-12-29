<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.ArrangementBudgetViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Bandbudsjetter
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="errormessage">
    <%: ViewData["Message"]%>                            
</div>  

<h2>Bandbudsjetter</h2>

<% using (Html.BeginForm()){%>
    <%--Dropdown for booker--%>
    <%--Dropdown for status--%> 
    <input id="fromDate" type="date" />
    <input id="toDate" type="date" />
    <input type="submit" id="update" value="Oppdater" />
<% }%>

<table class="contenttable">
    <tr>
        <th class="contentheader">
            Dato
        </th>
        <th class="contentheader">
            Artist
        </th>
        <th class="contentheader">
            Honorar
        </th>
        <th class="contentheader">
            Booker
        </th>
        <th class="contentheader">
            Status
        </th>
        <th></th>
    </tr>

    <% foreach (var item in Model.ArrangementBudgets) {%>
    <tr>
        <td class="contentcell_centeralign"><%: item.Date.ToShortDateString() %></td>
    </tr>
    <tr>
        <td class="contentcell_leftalign"><%: item.Artist %></td>
    </tr>
    <tr>
        <td class="contentcell_centeralign"><%: item.ArtistFee %></td>
    </tr>
    <tr>
        <td class="contentcell_centeralign"><%: item.BookerInCharge %></td>
    </tr>
    <tr>
        <td class="contentcell_centeralign"><%: item.Status %></td>
    </tr>
    <tr>
        <td>
            <%: Html.ActionLink("Rediger", "Edit", new { id = item.Id })%> |
            <%: Html.ActionLink("Skriv ut", "Print", new { id = item.Id })%>
        </td>
    </tr>
    <% } %>

</table>

<p>
    <%: Html.ActionLink("Nytt budsjett", "Create")%> |
    <%: Html.ActionLink("Skriv ut sammendrag", "PrintSummery")%>
    <%: Html.ActionLink("Søk etter budsjett", "Search")%> |
</p>

</asp:Content>
