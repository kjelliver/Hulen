<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.AccessGroupIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Tilgangsgrupper
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Tilgangsgrupper for Hulen</h2>

    <table class="contenttable" cellspacing="0">
        <tr>
            <th class="contentheader">
                Navn
            </th>
            <th class="contentheader">
                Type
            </th>
            <th class="contentheader">
                Beskrivelse
            </th>
            <th class="contentheader"></th>
        </tr>

    <% foreach (var item in Model.AllAccessGroups ) { %>
    
        <tr>
            <td class="contentcell_leftalign">
                <%: item.Name%>
            </td>
            <td class="contentcell_centeralign">
                <%: item.Type%>
            </td>
            <td class="contentcell_leftalign">
                <%: item.Description %>
            </td>
            <td class="contentcell_centeralign">
                <%: Html.ActionLink("Rediger", "Edit", new { id = item.Id })%> |
                <%: Html.ActionLink("Slett", "Delete", new { id = item.Id })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Ny tilgangsgruppe", "Create") %>
    </p>

</asp:Content>

