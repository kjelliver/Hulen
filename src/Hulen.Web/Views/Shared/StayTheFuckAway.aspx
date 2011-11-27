<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Timeout eler ingen tilgang.</title>
</head>
<body>
    <div>
        Timeout eller ingen tilgang til denne siden.
    </div>
    
    <div>
        <%: Html.ActionLink("Hjem", "Index", "Home") %> | 
        <%: Html.ActionLink("Login", "LogIn", "LogIn") %>
    </div>

</body>
</html>
