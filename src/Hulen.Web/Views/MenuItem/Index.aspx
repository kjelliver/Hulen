<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.MenuItemWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Menyelementer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Menyelementer for Hulen</h2>

    <table class="contenttable" cellspacing="0">
        <tr>
            <th class="contentheader">
                Navn
            </th>
            <th class="contentheader">
                Controller
            </th>
            <th class="contentheader">
                Action
            </th>
            <th class="contentheader">
                Tilgangsgruppe
            </th>
            <th class="contentheader"></th>
        </tr>

    <% foreach (var item in Model.AllMenuItems ) { %>
    
        <tr>
            <td class="contentcell_centeralign">
                <%: item.Name%>
            </td>
            <td class="contentcell_centeralign">
                <%: item.Controller%>
            </td>
            <td class="contentcell_centeralign">
                <%: item.Action %>
            </td>
            <td class="contentcell_centeralign">
                <%: item.AccessGroup %>
            </td>
            <td class="contentcell_centeralign">
                <%: Html.ActionLink("Rediger", "Edit", new { id = item.Id })%> |
                <%: Html.ActionLink("Slett", "Delete", new { id = item.Id })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Nytt menyelement", "Create") %>
    </p>

</asp:Content>