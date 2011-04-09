<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.FileImportWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
            FileUpload
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>FileUpload</h2>
    
     <% using (Html.BeginForm("Index", "FileImport", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {%>

        <fieldset>
            <legend>KONTOINFORMASJON</legend>

            <div class="editor-label">
                <%: Html.LabelFor(m => m.AccountInfoYear)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.AccountInfoYear)%>
                <%: Html.ValidationMessageFor(m => m.AccountInfoYear)%>
            </div>

            <input name="uploadFile" type="file" />
            <input type="submit" value="Upload File" />
        </fieldset>
<%} %>
 
</asp:Content>
