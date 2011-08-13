<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.MenuItemWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Slett menyelement
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Slett menyelement</h2>

        <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <table class="createtable">

            <tr>
                <td class="createheader">
                    Id
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.MenuItem.Id, new { @readonly = true })%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.MenuItem.Id)%>
                </td>
            </tr>  

            <tr>
                <td class="createheader">
                    Navn
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.MenuItem.Name)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.MenuItem.Name)%>
                </td>
            </tr>    
            
            <tr>
                <td class="createheader">
                    Controller
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.MenuItem.Controller)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.MenuItem.Controller)%>
                </td>
            </tr>  
            
            <tr>
                <td class="createheader">
                    Action
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.MenuItem.Action)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.MenuItem.Action)%>
                </td>
            </tr>   

            <tr>
                <td class="createheader">
                    Menynivå
                </td>
                <td class="createcell">
                    <%: Html.DropDownListFor(m => m.MenuItem.MenuLevel, new SelectList(Model.MenuLevels))%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.MenuItem.MenuLevel)%>
                </td>
            </tr>
            
            <tr>
                <td class="createheader">
                    Forelder
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.MenuItem.Parent)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.MenuItem.Parent)%>
                </td>
            </tr> 

            <tr>
                <td class="createheader">
                    Rekkefølge
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.MenuItem.SortOrder)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.MenuItem.SortOrder)%>
                </td>
            </tr>  

            <tr>
                <td class="createheader">
                    Link?
                </td>
                <td class="createcell">
                    <%: Html.CheckBoxFor(m => m.MenuItem.IsLink) %>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.MenuItem.IsLink)%>
                </td>
            </tr> 

             <tr>
                <td class="createheader">
                    Tilgangsgruppe
                </td>
                <td class="createcell">
                    <%: Html.DropDownListFor(m => m.MenuItem.MenuLevel, new SelectList(Model.AccessGroups))%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.MenuItem.AccessGroup)%>
                </td>
            </tr> 
        </table>

        <p>
            <input type="submit" name="save" id="save" value="Slett menyelement" />
        </p>

    <% } %>

    <p>
        <%: Html.ActionLink("Tilbake til oversikten", "Index") %>
    </p>

</asp:Content>
