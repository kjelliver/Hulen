<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.UserWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Rediger brukerkonto
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Rediger brukerkonto</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>

            <%: ViewData["Message"] %>

            <% if(Model.User != null){%>

            <legend>Kontoinformasjon</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(m => m.User.Id )%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.User.Id, new { @readonly = true})%>
                <%: Html.ValidationMessageFor(m => m.User.Id )%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.User.Username )%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.User.Username)%>
                <%: Html.ValidationMessageFor(m => m.User.Username )%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.User.Password )%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.User.Password)%>
                <%: Html.ValidationMessageFor(m => m.User.Password )%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.User.Name )%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.User.Name)%>
                <%: Html.ValidationMessageFor(m => m.User.Name )%>
            </div>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.User.Disabled )%>
            </div>
            <div class="editor-field">
                <%: Html.CheckBoxFor(m => m.User.Disabled)%>
                <%: Html.ValidationMessageFor(m => m.User.Disabled )%>
            </div>

            <%: Html.HiddenFor(m => m.UserNameStoredInDb)%>

            <p>
                <input type="submit" value="Lagre endringer" />
            </p>
        
            <% } %>
    <% } %>

        </fieldset>
    <div>
        <%: Html.ActionLink("Tilbake til brukeroversikt", "Index")%>
    </div>

</asp:Content>

