<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.AccountInfoImportModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Importer kontoinformasjon fra fil
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Importer kontoinformasjon fra fil</h2>

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

<% using (Html.BeginForm("Import", "AccountInfo", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {%>

        <fieldset>
            <legend>KONTOINFORMASJON</legend>

            <div class="editor-label">
                År
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.AccountInfoYear)%>
                <%: Html.ValidationMessageFor(m => m.AccountInfoYear)%>
            </div>

            <input name="uploadFile" type="file" />
            <input type="submit" value="Last opp" />
        </fieldset>
<%} %>

</asp:Content>
