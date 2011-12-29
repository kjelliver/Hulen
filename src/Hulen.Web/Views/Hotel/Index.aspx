<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.HotelViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Hotellavtaler
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="errormessage">
    <%: ViewData["Message"]%>                            
</div> 

<h2>Hotellavtaler</h2>

<table class="contenttable">
    <tr>
        <th class="contentheader">
            Hotellnavn
        </th>
        <th class="contentheader">
            Aktiv avtale
        </th>
        <th class="contentheader"></th>
    </tr>

    <% foreach (var item in Model.Hotels ) { %>
    
        <tr>
            <td class="contentcell_centeralign">
                <%: item.Name %>
            </td>
            <td class="contentcell_centeralign">
                <%: Html.CheckBox("IsActive", item.IsActive, new { disabled = "disabled" })%>
            </td>
            <td class="contentcell_centeralign">
                <%: Html.ActionLink("Rediger", "Edit", new { id = item.Id })%> |
                <%: Html.ActionLink("Slett", "Delete", new { id = item.Id })%>
            </td>
        </tr>
    
    <% } %>

</table>

<p>
    <%: Html.ActionLink("Ny hotellavtale", "Create") %>
</p>

</asp:Content>
