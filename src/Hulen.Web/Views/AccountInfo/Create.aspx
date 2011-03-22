<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.BusinessServices.ViewModels.AccountInfoViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AccountNumber) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AccountNumber) %>
                <%: Html.ValidationMessageFor(model => model.AccountNumber) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AccountName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AccountName) %>
                <%: Html.ValidationMessageFor(model => model.AccountName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ResultReportCategory) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ResultReportCategory) %>
                <%: Html.ValidationMessageFor(model => model.ResultReportCategory) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.PartsReportCategory) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.PartsReportCategory) %>
                <%: Html.ValidationMessageFor(model => model.PartsReportCategory) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.WeekCategory) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.WeekCategory) %>
                <%: Html.ValidationMessageFor(model => model.WeekCategory) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.IsIncome) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.IsIncome) %>
                <%: Html.ValidationMessageFor(model => model.IsIncome) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

