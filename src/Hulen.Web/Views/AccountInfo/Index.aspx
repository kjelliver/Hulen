<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.Web.Models.AccountInfo.AccountInfoModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Kontonummer
            </th>
            <th>
                Kontonavn
            </th>
            <th>
                Resultatrapport
            </th>
            <th>
                Driftsdelrapport
            </th>
            <th>
                Ukeregnskap
            </th>
            <th>
                Inntekt
            </th>
        </tr>

    <% foreach (var item in Model.AccountInfos) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Rediger", "Edit", new { id=item.Id  }) %>
                |
                <%= Html.ActionLink("Slett", "Delete", new { id=item.Id  }) %>
            </td>
            <td>
                <%= Html.Encode(item.AccountNumber) %>
            </td>
            <td>
                <%= Html.Encode(item.AccountName) %>
            </td>
            <td>
                <%= Html.Encode(item.ResultReportCategory) %>
            </td>
            <td>
                <%= Html.Encode(item.PartsReportCategory) %>
            </td>
            <td>
                <%= Html.Encode(item.WeekCategory) %>
            </td>
            <td>
                <%= Html.Encode(item.IsIncome) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Ny konto", "Create") %>
    </p>

</asp:Content>
