<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.AccessGroupEditModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Opprett tilgangsgruppe
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Opprett tilgangsgruppe</h2>

        <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <table class="createtable">
            <tr>
                <td class="createheader">
                    Navn
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.AccessGroup.Name)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.AccessGroup.Name)%>
                </td>
            </tr>    
            
            <tr>
                <td class="createheader">
                    Type
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.AccessGroup.Type)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.AccessGroup.Type)%>
                </td>
            </tr>  
            
            <tr>
                <td class="createheader">
                    Beskrivelse
                </td>
                <td class="createcell">
                    <%: Html.TextAreaFor(m => m.AccessGroup.Description)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.AccessGroup.Description)%>
                </td>
            </tr> 
               
        </table>
            
        <p>
            <input type="submit" value="Opprett tilgangsgruppe" />
        </p>

    <% } %>

    <p>
        <%: Html.ActionLink("Tilbake til oversikten", "Index") %>
    </p>

</asp:Content>
