<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.ResultImportWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Importer regskapsrapport
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Importer regnskapsrapport</h2>

    <% using (Html.BeginForm("ImportResult", "Result", FormMethod.Post, new { enctype = "multipart/form-data" })){%>

        <table class="createtable">
                <tr>
                    <td class="createheader">
                        År
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.Year)%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.Year)%>
                    </td>
                </tr> 

                <tr>
                    <td class="createheader">
                        Periode
                    </td>
                    <td class="createcell">
                        <%: Html.DropDownListFor(m => m.Period, new SelectList(Model.PeriodList))%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.PeriodList)%>
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
