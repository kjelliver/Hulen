<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.ResultImportWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Kontooversikt
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Gi nytt kontonummer på følgende</h2>

     <% using (Html.BeginForm("EditedAccounts", "Result", FormMethod.Post))
        {%>
    <table>
        <tr>
            <th>
                Id
            </th>
            <th>
                Kontonummer
            </th>
            <th>
                Måned
            </th>
            <th>
                År
            </th>
            <th>
                Beløp mnd
            </th>
            <th>
                Beløp år
            </th>
            <th>
                RealAccount
            </th>
        </tr>

    <% foreach (var item in Model.FailedAccounts ) { %>
    
        <tr>
            <td>
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].Id, new { @readonly = true, style = "width:150px;" })%>
            </td>
            <td>
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].AccountNumber, new { @readonly = true, style = "width:75px;" })%>
            </td>
            <td>
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].Month, new { @readonly = true, style = "width:75px;" })%>
            </td>
            <td>
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].Year, new { @readonly = true, style = "width:75px;" })%>
            </td>
            <td>
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].AmountMonth, new { @readonly = true, style = "width:75px;" })%>
            </td>
            <td>
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].AmountSoFar, new { @readonly = true, style = "width:75px;" })%>
            </td>
            <td>
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].RealAccount) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <input type="submit" value="Save" />
    </p>


    <% } %>

</asp:Content>

