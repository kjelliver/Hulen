<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <p>
        <img src="/Content/Images/home.jpg" alt="some_text"/>
    </p>
    <p>
        Dersom du oppdager feil eller mangler ved denne portalen, ta kontakt med kjelliverb@gmail.com.
    </p>
</asp:Content>
