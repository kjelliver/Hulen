<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.UserWebModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Rediger brukerkonto
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

       <div class="errormessage">
        <%: ViewData["Message"]%>                            
    </div>  

    <h2>Rediger brukerkonto</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <table class="createtable">

            <tr>
                <td class="createheader">
                    <%: Html.LabelFor(m => m.User.Id)%>
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.User.Id, new {@readonly = true})%>
                    <%: Html.ValidationMessageFor(m => m.User.Id)%>
                </td>
            </tr> 
            <tr>
                <td class="createheader">
                    <%: Html.LabelFor(m => m.User.Username)%>
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.User.Username)%>
                    <%: Html.ValidationMessageFor(m => m.User.Username)%>
                </td>
            </tr>  
            
            <tr>
                <td class="createheader">
                    Passord
                </td>
                <td class="createcell">
                    <%:Html.PasswordFor(m => m.User.Password)%>
                    <%: Html.ValidationMessageFor(m => m.User.Password)%>
                </td>
            </tr>

            <tr>
                <td class="createheader">
                    Navn
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.User.Name)%>
                    <%: Html.ValidationMessageFor(m => m.User.Name)%>
                </td>
            </tr>
            
            <tr>
                <td class="createheader">
                    Rolle
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.User.Role)%>
                    <%: Html.ValidationMessageFor(m => m.User.Role)%>
                </td>
            </tr>

            <tr>
                <td class="createheader">
                    Deaktivert
                </td>
                <td class="createcell">
                    <%: Html.CheckBoxFor(m => m.User.Disabled)%>
                    <%: Html.ValidationMessageFor(m => m.User.Disabled)%>
                </td>
            </tr>

            <tr>
                <td class="createheader">
                    Må endre passord
                </td>
                <td class="createcell">
                    <%: Html.CheckBoxFor(m => m.User.MustChangePassword)%>
                    <%: Html.ValidationMessageFor(m => m.User.MustChangePassword)%>
                </td>
            </tr>
        </table>

        <p>
            <input type="submit" name="reset" id="reset" value="Reset passord" />            
            <input type="submit" name="save" id="save" value="Lagre" />
        </p>

    <% } %>

    <p>
        <%: Html.ActionLink("Tilbake til brukeroversikt", "Index") %>
    </p>

</asp:Content>

