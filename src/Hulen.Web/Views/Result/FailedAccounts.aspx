<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.ResultImportWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Kontooversikt
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div> 

    <h2>Gi nytt kontonummer på følgende</h2>


     <% using (Html.BeginForm("FailedAccounts", "Result", FormMethod.Post))
        {%>
    <table class="contenttable" cellspacing="0">
        <tr>
            <th class="contentheader">
                Kontonr fra Visma
            </th>
            <th class="contentheader">
                Måned
            </th>
            <th class="contentheader">
                År
            </th>
            <th class="contentheader">
                Beløp mnd
            </th>
            <th class="contentheader">
                Beløp år
            </th>
            <th class="contentheader">
                Skal føres på
            </th>
        </tr>

    <% foreach (var item in Model.FailedAccounts ) { %>
    
        <tr>
            <td class="contentcell">
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].AccountNumber, new { @readonly = true, style = "width:75px;" })%>
            </td>
            <td class="contentcell">
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].Month, new { @readonly = true, style = "width:75px;" })%>
            </td>
            <td class="contentcell">
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].Year, new { @readonly = true, style = "width:75px;" })%>
            </td>
            <td class="contentcell">
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].AmountMonth, new { @readonly = true, style = "width:75px;" })%>
            </td>
            <td class="contentcell">
                <%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].AmountSoFar, new { @readonly = true, style = "width:75px;" })%>
            </td>
            <td class="contentcell">
                <%: Html.DropDownListFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].RealAccount, new SelectList(Model.Accounts, "AccountNumber", "NumberAndName"))%>
                <%--<%: Html.TextBoxFor(m => m.FailedAccounts[Model.FailedAccounts.IndexOf(item)].RealAccount) %>--%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <input type="submit" value="Save" />
    </p>

    <% } %>

    <p>
        <%: Html.ActionLink("Tilbake til oversikten", "Index") %>
    </p>

</asp:Content>

