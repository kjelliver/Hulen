<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Ingen tilgang til denne siden!</title>
</head>
<body>
    <div>
        Du har ingen tilgang til denne siden.
    </div>
    
    <div>
        <%: Html.ActionLink("Hjem", "Index", "Home") %>
    </div>

</body>
</html>
