<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.ResultDeleteWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Slett regnskapsrapport
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Slett regnskapsrapport</h2>

        <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <table class="createtable">

            <tr>
                <td class="createheader">
                    Id
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.SelectedResult.Id, new { @readonly = true })%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.SelectedResult.Id)%>
                </td>
            </tr>  

            <tr>
                <td class="createheader">
                    År
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.SelectedResult.Year)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.SelectedResult.Year)%>
                </td>
            </tr>    
            
            <tr>
                <td class="createheader">
                    Controller
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.SelectedResult.Period)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.SelectedResult.Period)%>
                </td>
            </tr>  
            
            <tr>
                <td class="createheader">
                    Action
                </td>
                <td class="createcell">
                    <%: Html.TextAreaFor(m => m.SelectedResult.Comment)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.SelectedResult.Comment)%>
                </td>
            </tr>   
            
        </table>

        <p>
            <input type="submit" name="save" id="save" value="Slett regnskap" />
        </p>

    <% } %>

    <p>
        <%: Html.ActionLink("Tilbake til oversikten", "Index") %>
    </p>

</asp:Content>
