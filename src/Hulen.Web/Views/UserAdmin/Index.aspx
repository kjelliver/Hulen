<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.UserWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Brukeradministrasjon
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>

    <h2>Brukeradmininstrasjon</h2>

    <table  class="contenttable" cellspacing="0">
        <tr>
            <th class="contentheader">
                Navn
            </th>

            <th class="contentheader">
                Brukernavn
            </th>

            <th class="contentheader">
                Rolle
            </th>

            <th class="contentheader">
                Inaktiv
            </th>
            
            <th class="contentheader"></th>
        </tr>

    <% foreach (var item in Model.Users ) { %>
    
        <tr>
            <td class="contentcell_centeralign">
                <%: item.Name %>
            </td>

            <td class="contentcell_centeralign">
                <%: item.Username %>
            </td>

            <td class="contentcell_centeralign">
                <%: item.Role %>
            </td>

            <td class="contentcell_centeralign">
                <%: Html.CheckBox("Disabled", item.Disabled, new { disabled = "disabled" })%>
            </td>

            <td class="contentcell_centeralign">
                <%: Html.ActionLink("Rediger", "Edit", new { username = item.Username })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Ny brukerkonto", "Create") %>
    </p>

</asp:Content>
