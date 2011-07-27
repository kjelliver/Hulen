<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.AccountInfoEditModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Opprett ny kontoinformasjon
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Opprett ny kontoinformasjon</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <table class="createtable">
            <tr>
                <td class="createheader">
                    Kontonummer
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.AccountInfo.AccountNumber)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.AccountInfo.AccountNumber)%>
                </td>
            </tr>    
            
            <tr>
                <td class="createheader">
                    Kontonavn
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.AccountInfo.AccountName)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.AccountInfo.AccountName)%>
                </td>
            </tr>  
            
            <tr>
                <td class="createheader">
                    Resultatrapportkategori
                </td>
                <td class="createcell">
                    <%: Html.DropDownListFor(m => m.AccountInfo.ResultReportCategory, new SelectList(Model.ResultCategories))%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.AccountInfo.ResultReportCategory)%>
                </td>
            </tr> 
               
            <tr>
                <td class="createheader">
                    Driftsdelkategori
                </td>
                <td class="createcell">
                    <%: Html.DropDownListFor(m => m.AccountInfo.PartsReportCategory, new SelectList(Model.PartsCategories))%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.AccountInfo.PartsReportCategory)%>
                </td>
            </tr> 

            <tr>
                <td class="createheader">
                    Ukeskategori
                </td>
                <td class="createcell">
                    <%: Html.DropDownListFor(m => m.AccountInfo.WeekCategory, new SelectList(Model.WeekCategories))%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.AccountInfo.WeekCategory)%>
                </td>
            </tr> 

            <tr>
                <td class="createheader">
                    Inntekt eller utgift?
                </td>
                <td class="createcell">
                    <%: Html.DropDownListFor(m => m.AccountInfo.IsIncome, new SelectList(Model.IsIncomes))%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.AccountInfo.IsIncome)%>
                </td>
            </tr> 

            <tr>
                <td class="createheader">
                    År
                </td>
                <td class="createcell">
                    <%: Html.TextBoxFor(m => m.AccountInfo.Year)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.AccountInfo.Year)%>
                </td>
            </tr> 
        </table>
            
        <p>
            <input type="submit" value="Opprett kontoinformasjon" />
        </p>

    <% } %>

    <p>
        <%: ViewData["Message"] %>
    </p>

    <p>
        <%: Html.ActionLink("Tilbake til oversikten", "Index") %>
    </p>

</asp:Content>

