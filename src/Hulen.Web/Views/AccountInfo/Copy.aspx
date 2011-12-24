<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.AccountInfoCopyModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Kopier kontoinformasjon
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Kopier kontoinformasjon</h2>

    <% using (Html.BeginForm()){%>
           
        <div class="errormessage">
            <%: ViewData["Message"]%>                            
        </div>  

        <table class="createtable">
            <tr>
                <td class="createheader">
                    Kopier fra år
                </td>
                <td>
                    <%:Html.DropDownListFor(x => x.SelectedCopyFromYear, new SelectList(Model.CopyFromYears))%>
                </td>
            </tr>

            <tr>
                <td class="createheader">
                    Kopier til år
                </td>
                <td>
                    <%:Html.DropDownListFor(x => x.SelectedCopyToYear, new SelectList(Model.CopyToYears))%>
                </td>
            </tr>
        </table>

        <p>
            <input type="submit" value="Kopier" />        
        </p>

    <% }%>

</asp:Content>




