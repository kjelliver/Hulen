<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.Web.Models.AccountInfoModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.AccountNumber) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.AccountNumber) %>
                <%: Html.ValidationMessageFor(m => m.AccountNumber) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.AccountName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.AccountName) %>
                <%: Html.ValidationMessageFor(m => m.AccountName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.ResultReportCategory) %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(m => m.ResultReportCategory, new SelectList(Model.ResultCategories)) %>
                <%: Html.ValidationMessageFor(m => m.ResultReportCategory) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.PartsReportCategory) %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(m => m.PartsReportCategory, new SelectList(Model.PartsCategories)) %>
                <%: Html.ValidationMessageFor(m => m.PartsReportCategory) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.WeekCategory) %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(m => m.WeekCategory, new SelectList(Model.WeekCategories)) %>
                <%: Html.ValidationMessageFor(m => m.WeekCategory) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.IsIncome) %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(m => m.IsIncome, new SelectList(Model.IsIncomes)) %>
                <%: Html.ValidationMessageFor(m => m.IsIncome) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

