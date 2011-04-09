<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.AccountInfoWebModel>" %>

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
                <%: Html.LabelFor(m => m.AccountInfo.Year)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.AccountInfo.Year)%>
                <%: Html.ValidationMessageFor(m => m.AccountInfo.Year)%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.AccountInfo.AccountNumber)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.AccountInfo.AccountNumber)%>
                <%: Html.ValidationMessageFor(m => m.AccountInfo.AccountNumber)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.AccountInfo.AccountName)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.AccountInfo.AccountName)%>
                <%: Html.ValidationMessageFor(m => m.AccountInfo.AccountName)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.AccountInfo.ResultReportCategory)%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(m => m.AccountInfo.ResultReportCategory, new SelectList(Model.ResultCategories))%>
                <%: Html.ValidationMessageFor(m => m.AccountInfo.ResultReportCategory)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.AccountInfo.PartsReportCategory)%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(m => m.AccountInfo.PartsReportCategory, new SelectList(Model.PartsCategories))%>
                <%: Html.ValidationMessageFor(m => m.AccountInfo.PartsReportCategory)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.AccountInfo.WeekCategory)%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(m => m.AccountInfo.WeekCategory, new SelectList(Model.WeekCategories))%>
                <%: Html.ValidationMessageFor(m => m.AccountInfo.WeekCategory)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.AccountInfo.IsIncome)%>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(m => m.AccountInfo.IsIncome, new SelectList(Model.IsIncomes))%>
                <%: Html.ValidationMessageFor(m => m.AccountInfo.IsIncome)%>
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

