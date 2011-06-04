<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.UserWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Brukeradministrasjon
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Brukeradmininstrasjon</h2>

    <table>
        <tr>
            <th>
                Navn
            </th>
            
            <th></th>
        </tr>

    <% foreach (var item in Model.Users ) { %>
    
        <tr>
            <td>
                <%: item.Name %>
            </td>

            <td>
                <%: Html.ActionLink("Rediger", "Edit", new { username = item.Username })%> |
                <%: Html.ActionLink("Slett", "Index", new { id = item.Id })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Ny brukerkonto", "Create") %>
    </p>

</asp:Content>
