<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Report.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.Web.Models.ReportModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="style" ContentPlaceHolderID="StyleContent" runat="server">
    <%= Model.CssStyle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <%= Model.HtmlBody %>

</asp:Content>
