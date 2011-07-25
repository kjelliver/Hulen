<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Hulen.WebCode.Models.MenuWebModel>" %>


<table class="mainmenu">

    <% foreach (var item in Model.MenuItems){%>

        <% if (item.MenuLevel == 1){%>
            <tr>
                <td style="font-weight:bold;">
                    <% if (item.IsLink){%>
                        <%: Html.ActionLink(item.Name, item.Action, item.Controller)%>
                    <%}else{%>
                        <%: item.Name %>
                    <%}%>
                </td>
            </tr>
        <%} else {%>
            <tr>
                <td>
                    &nbsp;&nbsp; <%: Html.ActionLink(item.Name, item.Action, item.Controller) %>
                </td>
            </tr>
        <%}%>
    <%}%>

</table>



