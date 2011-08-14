<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.Models.NewPasswordModel>" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hulen - Bytt passord</title>
    <link href="../../Content/LogIn.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <table class="loginbox">
        <tr>
            <td align="center">
                <% using (Html.BeginForm()) { %>
                <%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
                <div>
                    <fieldset>
                        <legend>Bytt passord:</legend>
                
                        <div class="label">
                            Passord
                        </div>
                        <div>
                            <%: Html.TextBoxFor(m => m.NewPassword) %>
                            <%: Html.ValidationMessageFor(m => m.NewPassword) %>
                        </div>
                
                        <div class="label">
                            Gjenta passord
                        </div>
                        <div>
                            <%: Html.PasswordFor(m => m.RepeatPassword) %>
                            <%: Html.ValidationMessageFor(m => m.RepeatPassword) %>
                        </div>

                        <%: Html.HiddenFor(m => m.UserName) %>

                        <p>
                            <input type="submit" value="Logg inn" />
                        </p>

                        <div class="errormessage">
                            <%: TempData["Message"]%>                            
                        </div>

                    </fieldset>
                </div>
            <% } %>
            </td>
        </tr>
    </table>
</body>
</html>
