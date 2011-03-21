<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.Storage.DTO.AccountInfoDTO>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">AccountNumber</div>
        <div class="display-field"><%: Model.AccountNumber %></div>
        
        <div class="display-label">AccountName</div>
        <div class="display-field"><%: Model.AccountName %></div>
        
        <div class="display-label">ResultReportCategory</div>
        <div class="display-field"><%: Model.ResultReportCategory %></div>
        
        <div class="display-label">PartsReportCategory</div>
        <div class="display-field"><%: Model.PartsReportCategory %></div>
        
        <div class="display-label">WeekCategory</div>
        <div class="display-field"><%: Model.WeekCategory %></div>
        
        <div class="display-label">IsIncome</div>
        <div class="display-field"><%: Model.IsIncome %></div>
        
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Tilbake til kontoer", "Index") %>
        </p>
    <% } %>

</asp:Content>

