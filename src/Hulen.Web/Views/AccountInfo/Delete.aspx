<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.AccountInfoWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Fields</legend>
        
        <% using (Html.BeginForm()) { %>

        <div class="display-label">Id</div>
        <div class="display-field"><%: Model.AccountInfo.Id%></div>
        
        <div class="display-label">År</div>
        <div class="display-field"><%: Model.AccountInfo.Year%></div>

        <div class="display-label">AccountNumber</div>
        <div class="display-field"><%: Model.AccountInfo.AccountNumber%></div>
        
        <div class="display-label">AccountName</div>
        <div class="display-field"><%: Model.AccountInfo.AccountName%></div>
        
        <div class="display-label">ResultReportCategory</div>
        <div class="display-field"><%: Model.AccountInfo.ResultReportCategory%></div>
        
        <div class="display-label">PartsReportCategory</div>
        <div class="display-field"><%: Model.AccountInfo.PartsReportCategory%></div>
        
        <div class="display-label">WeekCategory</div>
        <div class="display-field"><%: Model.AccountInfo.WeekCategory%></div>
        
        <div class="display-label">IsIncome</div>
        <div class="display-field"><%: Model.AccountInfo.IsIncome%></div>
        
    </fieldset>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to List", "Index") %>
        </p>
    <% } %>

</asp:Content>

