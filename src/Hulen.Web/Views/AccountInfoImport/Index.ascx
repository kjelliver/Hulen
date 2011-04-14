<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Hulen.WebCode.Models.AccountInfoImportWebModel>" %>


     <% using (Html.BeginForm("Index", "AccountInfoImport", FormMethod.Post, new { enctype = "multipart/form-data" }))
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
 
