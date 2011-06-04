<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Hulen.WebCode.Models.MenuWebModel>" %>


<table>

    <% foreach (var item in Model.MenuItems){%>
    <tr>
        <td>
            <%: Html.ActionLink(item.Name, item.Action, item.Controller) %>
        </td>
    </tr>
    <%}%>

</table>



