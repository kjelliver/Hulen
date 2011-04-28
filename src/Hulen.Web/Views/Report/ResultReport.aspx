<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Report.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.ReportModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="style" ContentPlaceHolderID="StyleContent" runat="server">
    <%= Model.CssStyle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%= Model.HtmlBody %>

</asp:Content>
