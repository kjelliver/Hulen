<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.ReportModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Hulen - Skriv ut rapport
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Rapportoversikt</h2>

    <% using (Html.BeginForm("ResultReport", "Report", FormMethod.Post))
       {%>
    <table> 
        <tr>
            <td>
                <%:Html.TextBoxFor(m => m.ResultReportMonth)%>            
            </td>
            <td>
                <%:Html.TextBoxFor(m => m.ResultReportYear)%>            
            </td>
        </tr>
    </table>

    <p>
        <input type="submit" value="Save" />
    </p>

    <%}%>

</asp:Content>

