<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Hulen.WebCode.Models.BudgetImportWebModel>" %>
    
     <% using (Html.BeginForm("Index", "BudgetImport", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {%>

        <fieldset>
            <legend>DRIFTSBUDSJETT</legend>

            <div class="editor-label">
                Budsjettår
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.BudgetYear)%>
                <%: Html.ValidationMessageFor(m => m.BudgetYear)%>
            </div>

            <div class="editor-label">
                Orginalt/Revidert
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(m => m.BudgetStatus)%>
                <%: Html.ValidationMessageFor(m => m.BudgetStatus)%>
            </div>

            <input name="uploadFile" type="file" />
            <input type="submit" value="Upload File" />

        </fieldset>

<%} %>


