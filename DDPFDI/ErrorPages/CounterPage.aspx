<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CounterPage.aspx.cs" Inherits="ErrorPages_CounterPage" %>

<!DOCTYPE html>  
  
<html xmlns="http://www.w3.org/1999/xhtml">  
<head runat="server">  
    <title>Get IP Address</title>  
    <link href="Files/css/bootstrap.css" rel="stylesheet" />  
</head>  
<body>  
    <form id="form1" runat="server">  
    <div>  
        <h1><strong>Your IP Address : </strong></h1>  
        <br/>  
        <h1><strong>Method 1 : </strong><asp:Label ID="lblIPAddress" CssClass="label label-primary" runat="server" Text="Label"></asp:Label></h1>  
        <br />  
        <h1><strong>Method 2 : </strong><asp:Label ID="lblIPAdd" CssClass="label label-info" runat="server" Text="Label"></asp:Label></h1>  
    </div>  
        <div>
    <fieldset style ="width:220px;">
    <legend>Count site visited and Online users</legend>   
        <asp:Label ID="lblSiteVisited" runat="server" Text=""
            style="color: #FFFFFF; background-color: #3366FF"></asp:Label><br />
        <asp:Label ID="lblOnlineUsers" runat="server" Text=""
            style="color: #FFFFFF; background-color: #009933"></asp:Label><br />
       
        <asp:Button ID="btnClearSesson" runat="server" Text="Clear Session"
            onclick="btnClearSesson_Click" />
        </fieldset>
        </div>
    </form>  
</body>  
</html>  
