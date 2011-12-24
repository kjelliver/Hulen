<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.FixedArrangementCostsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Faste arrangementskostnader
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Faste arrangementskostnader</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
            
            <%: Html.HiddenFor(m => m.FixedArrangementCosts.Id)%>

            <table class="createtable">  
                <tr>
                    <td class="createheader">
                        Sist oppdatert
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.FixedArrangementCosts.GeneratedDate)%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.FixedArrangementCosts.GeneratedDate)%>
                    </td>
                </tr> 
                <tr>
                    <td class="createheader">
                        Pris pr. bandøl
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.FixedArrangementCosts.PricePerBeer)%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.FixedArrangementCosts.PricePerBeer)%>
                    </td>
                </tr> 
                <tr>
                    <td class="createheader">
                        Pris pr. bandvin
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.FixedArrangementCosts.PricePerWine)%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.FixedArrangementCosts.PricePerWine)%>
                    </td>
                </tr> 
                <tr>
                    <td class="createheader">
                        Musikkutstyrsordningen
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.FixedArrangementCosts.FixedTechRental )%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.FixedArrangementCosts.FixedTechRental)%>
                    </td>
                </tr> 
                <tr>
                    <td class="createheader">
                        Tekniker, grunnlønn
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.FixedArrangementCosts.SoundmanSalery )%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.FixedArrangementCosts.SoundmanSalery)%>
                    </td>
                </tr> 
                <tr>
                    <td class="createheader">
                        Tekniker pr. oppvarming
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.FixedArrangementCosts.SoundmanSaleryPerWarmUp )%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.FixedArrangementCosts.SoundmanSaleryPerWarmUp )%>
                    </td>
                </tr> 
                <tr>
                    <td class="createheader">
                        PR-kostnader
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.FixedArrangementCosts.PromotionExpences )%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.FixedArrangementCosts.PromotionExpences)%>
                    </td>
                </tr> 
                <tr>
                    <td class="createheader">
                        Faste kostnader
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.FixedArrangementCosts.FixedCosts )%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.FixedArrangementCosts.FixedCosts )%>
                    </td>
                </tr> 
                <tr>
                    <td class="createheader">
                        Document ID
                    </td>
                    <td class="createcell">
                        <%: Html.TextBoxFor(m => m.FixedArrangementCosts.DocumentId )%>
                    </td>
                    <td>
                        <%: Html.ValidationMessageFor(m => m.FixedArrangementCosts.DocumentId)%>
                    </td>
                </tr> 
            </table>

            <p>
                <input type="submit" value="Lagre endringer" />
            </p>

    <% } %>

</asp:Content>
