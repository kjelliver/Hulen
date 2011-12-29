<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.HotelViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hulen - Hotellavtaler
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="errormessage">
    <%: ViewData["Message"]%>                            
</div>

<h2>Slett hotellavtale</h2>

<% using (Html.BeginForm()) {%>
<%: Html.ValidationSummary(true) %>

    <%: Html.HiddenFor(m => m.Hotel.Id) %>

    <table class="createtable">
        <tr>
            <td class="createheader">
                Navn
            </td>
            <td class="createcell">
                <%: Html.TextBoxFor(m => m.Hotel.Name, new { @readonly = true })%>
            </td>
            <td>
                <%: Html.ValidationMessageFor(m => m.Hotel.Name)%>
            </td>
        </tr>    
            
        <tr>
            <td class="createheader">
                Pris på enkeltrom
            </td>
            <td class="createcell">
                <%: Html.TextBoxFor(m => m.Hotel.SingleRoomPrice, new { @readonly = true })%>
            </td>
            <td>
                <%: Html.ValidationMessageFor(m => m.Hotel.SingleRoomPrice)%>
            </td>
        </tr>  
            
        <tr>
            <td class="createheader">
                Pris på dobbelrom
            </td>
            <td class="createcell">
                <%: Html.TextBoxFor(m => m.Hotel.DoubleRoomPrice, new { @readonly = true })%>
            </td>
            <td>
                <%: Html.ValidationMessageFor(m => m.Hotel.DoubleRoomPrice)%>
            </td>
        </tr>   

         <tr>
            <td class="createheader">
                Pris på trippelrom
            </td>
            <td class="createcell">
                <%: Html.TextBoxFor(m => m.Hotel.TripleRoomPrice, new { @readonly = true })%>
            </td>
            <td>
                <%: Html.ValidationMessageFor(m => m.Hotel.TripleRoomPrice)%>
            </td>
        </tr> 

        <tr>
            <td class="createheader">
                Pris på grupperom
            </td>
            <td class="createcell">
                <%: Html.TextBoxFor(m => m.Hotel.GroupRoomPrice, new { @readonly = true })%>
            </td>
            <td>
                <%: Html.ValidationMessageFor(m => m.Hotel.GroupRoomPrice)%>
            </td>
        </tr> 

        <tr>
            <td class="createheader">
                Pris på ekstra seng
            </td>
            <td class="createcell">
                <%: Html.TextBoxFor(m => m.Hotel.ExtraBedPrice, new { @readonly = true })%>
            </td>
            <td>
                <%: Html.ValidationMessageFor(m => m.Hotel.ExtraBedPrice)%>
            </td>
        </tr> 

        <tr>
            <td class="createheader">
                Aktiv
            </td>
            <td class="createcell">
                <%: Html.CheckBoxFor(m => m.Hotel.IsActive, new { @readonly = true })%>
            </td>
            <td>
                <%: Html.ValidationMessageFor(m => m.Hotel.IsActive)%>
            </td>
        </tr> 
        
    </table>

    <p>
        <input type="submit" id="save" value="Slett hotellavtale" />
    </p>

<% } %>

<p>
    <%: Html.ActionLink("Tilbake til oversikten", "Index") %>
</p>

</asp:Content>
