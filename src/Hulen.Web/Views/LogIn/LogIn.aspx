<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Hulen.WebCode.ViewModels.LogInModel>" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hulen - Logg inn</title>
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
                        <legend>Logg inn:</legend>
                
                        <div class="label">
                            Brukernavn
                        </div>
                        <div>
                            <%: Html.TextBoxFor(m => m.UserName) %>
                            <%: Html.ValidationMessageFor(m => m.UserName) %>
                        </div>
                
                        <div class="label">
                            Passord
                        </div>
                        <div>
                            <%: Html.PasswordFor(m => m.Password) %>
                            <%: Html.ValidationMessageFor(m => m.Password) %>
                        </div>

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
