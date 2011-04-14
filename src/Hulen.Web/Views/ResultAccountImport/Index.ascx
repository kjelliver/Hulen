<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Hulen.WebCode.Models.ResultAccountImportWebModel>" %>
    
     <% using (Html.BeginForm("Index", "ResultAccountImport", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {%>

        <fieldset>
            <legend>RESULTATREGNSKAP</legend>

            <div class="editor-label">
                Måned
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.Month)%>
                <%: Html.ValidationMessageFor(m => m.Month)%>
            </div>

            <div class="editor-label">
                År
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.ResultYear)%>
                <%: Html.ValidationMessageFor(m => m.ResultYear)%>
            </div>

            <input name="uploadFile" type="file" />
            <input type="submit" value="Last opp" />

        </fieldset>

<%} %>