<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.FileImportWebModel>" %>
<%@ Register TagPrefix ="hulen" TagName="BudgetImport" Src="~/Views/BudgetImport/Index.ascx" %>
<%@ Register TagPrefix ="hulen" TagName="ResultAccountImport" Src="~/Views/ResultAccountImport/Index.ascx" %>
<%@ Register TagPrefix ="hulen" TagName="AccountInfoImport" Src="~/Views/AccountInfoImport/Index.ascx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
            Hulen - Importer filer
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Importer filer</h2>
    
    <hulen:AccountInfoImport ID="accountInfoImport" runat="server" />
    <hulen:BudgetImport ID="budgetImport" runat="server"/>
    <hulen:ResultAccountImport ID="resultAccountImport" runat="server" />
 
</asp:Content>

