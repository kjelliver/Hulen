<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.UserWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Opprett en ny bruker
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ny bruker</h2>

    <%: ViewData["Message"] %>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.User.Username)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.User.Username)%>
                <%: Html.ValidationMessageFor(m => m.User.Username)%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.User.Password)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.User.Password)%>
                <%: Html.ValidationMessageFor(m => m.User.Password)%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.User.Name)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.User.Name)%>
                <%: Html.ValidationMessageFor(m => m.User.Name)%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.User.Disabled)%>
            </div>
            <div class="editor-field">
                <%: Html.CheckBoxFor(m => m.User.Disabled)%>
                <%: Html.ValidationMessageFor(m => m.User.Disabled)%>
            </div>

            <p>
                <input type="submit" value="Opprett" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Tilbake til brukeroversikt", "Index") %>
    </div>

</asp:Content>

