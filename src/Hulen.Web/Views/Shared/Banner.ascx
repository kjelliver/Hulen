<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Hulen.WebCode.Models.BannerWebModel>" %>

<table>
    <tr>
        <td width="224px">
        </td>
        <td width="800xpx">
            Pålogget bruker: <%: Model.LoggedOnUser.Name %> 
            &nbsp;&nbsp;&nbsp;&nbsp;
            Rolle: <%: Model.LoggedOnUser.Role %>
        </td>
    </tr>
</table>






