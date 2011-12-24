<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.AccountInfoIndexModel>" %>
<%@ Import Namespace="Hulen.WebCode.MvcBase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Kontooversikt
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <% if (Model.AccountInfos != null) {%>

        <h2>Kontooversikt</h2>


        <% using (Html.BeginForm()){%>
            <%:Html.DropDownListFor(x => x.SelectedYear, new SelectList(Model.Years, Model.DefaultYear))%>

                <input type="submit" value="Oppdater" />

        <% }%>

        <table class="contenttable" cellspacing="0">
            <tr>
                <th class="contentheader">
                    Kontonr.
                </th>
                <th class="contentheader">
                    Kontonavn
                </th>
                <th class="contentheader">
                    Resultatrapport
                </th>
                <th class="contentheader">
                    Driftsdel
                </th>
                <th class="contentheader">
                    Ukesregn
                </th>
                <th class="contentheader">
                    Inntekt/Utgift
                </th>

                <% if (ViewBase.UserHasAccessTo("FEAURE_ACCOUNTINFO_EDIT")){%>
                <th class="contentheader"></th>
                <% } %>
            </tr>

        <% foreach (var item in Model.AccountInfos ) { %>
    
            <tr>
                <td class="contentcell_centeralign">
                    <%: item.AccountNumber%>
                </td>
                <td class="contentcell_centeralign">
                    <%: item.AccountName%>
                </td>
                <td class="contentcell_centeralign">
                    <%: item.ResultReportCategory%>
                </td>
                <td class="contentcell_centeralign">
                    <%: item.PartsReportCategory%>
                </td>
                <td class="contentcell_centeralign">
                    <%: item.WeekCategory%>
                </td>
                <td class="contentcell_centeralign">
                    <%: item.IsIncome%>
                </td>

                <% if (ViewBase.UserHasAccessTo("FEAURE_ACCOUNTINFO_EDIT")){%>
                <td class="contentcell_centeralign">
                    <%: Html.ActionLink("Rediger", "Edit", new { id = item.Id })%> |
                    <%: Html.ActionLink("Slett", "Delete", new { id = item.Id })%>
                </td>
                <% } %>

            </tr>
    
        <% } %>
    <% } %>
    </table>

    <p>
        <% if (ViewBase.UserHasAccessTo("FEAURE_ACCOUNTINFO_EDIT")){%>
        <%: Html.ActionLink("Ny konto", "Create") %> | 
        <%: Html.ActionLink("Kopier år", "Copy") %> |
        <%: Html.ActionLink("Importer", "Import") %> 
        <% } %>
    </p>

</asp:Content>

