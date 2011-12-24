<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.BudgetImportWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Importer budsjett
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Importer budsjett</h2>

    <% using (Html.BeginForm("ImportBudget", "Budget", FormMethod.Post, new { enctype = "multipart/form-data" })){%>

        <table class="createtable">
                <tr>
                    <td class="createheader">
                        Budsjettår
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.BudgetYear)%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.BudgetYear)%>
                    </td>
                </tr> 

                <tr>
                    <td class="createheader">
                        Orginalt/Revidert?
                    </td>
                    <td class="createcell">
                        <%: Html.DropDownListFor(m => m.BudgetStatus, new SelectList(Model.BudgetStatusList))%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.BudgetStatus)%>
                    </td>
                </tr> 

                <tr>
                    <td class="createheader">
                        Kommentar
                    </td>
                    <td class="createcell">
                        <%: Html.TextAreaFor(m => m.Comment)%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.Comment)%>
                    </td>
                </tr> 
                <tr><td><br/></td></tr>
            </table>
            <p><input name="uploadFile" type="file" /></p>
            <p><input type="submit" value="Last opp" /></p>

    <%}%>

    <p>
        <%: Html.ActionLink("Tilbake til oversikten", "Index") %>
    </p>

</asp:Content>
