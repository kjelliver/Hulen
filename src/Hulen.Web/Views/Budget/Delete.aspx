<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.BudgetDeleteWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Slett budsjett
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Slett budsjett</h2>

        <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <table class="createtable">

            <tr>
                <td class="createheader">
                    Id
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.SelectedBudget.Id, new { @readonly = true })%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.SelectedBudget.Id)%>
                </td>
            </tr>  

            <tr>
                <td class="createheader">
                    År
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.SelectedBudget.Year)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.SelectedBudget.Year)%>
                </td>
            </tr>    
            
            <tr>
                <td class="createheader">
                    Controller
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.SelectedBudget.BudgetStatus)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.SelectedBudget.BudgetStatus)%>
                </td>
            </tr>  
            
            <tr>
                <td class="createheader">
                    Action
                </td>
                <td class="createcell">
                    <%: Html.TextAreaFor(m => m.SelectedBudget.Comment)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.SelectedBudget.Comment)%>
                </td>
            </tr>   
            
        </table>

        <p>
            <input type="submit" name="save" id="save" value="Slett budsjett" />
        </p>

    <% } %>

    <p>
        <%: Html.ActionLink("Tilbake til oversikten", "Index") %>
    </p>

</asp:Content>
