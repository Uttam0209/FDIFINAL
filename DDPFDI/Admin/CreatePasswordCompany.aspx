<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreatePasswordCompany.aspx.cs" Inherits="Admin_CreatePasswordCompany" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link type="text/css" rel="shortcut icon" href="~/assets/images/favicon.ico">
    <link type="text/css" href="~/assets/css/bootstrap.min.css" rel="stylesheet">
    <link href="~/assets/css/font-awesome.css" rel="stylesheet">
    <link href="~/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/custom.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/responsive.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="loginBg clearfix">
            <h3>Create New Password</h3>
            <label for="pass" class="tetLable">
                Password
            </label>
            <asp:TextBox runat="server" ID="txtpassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
            <label for="pass" class="tetLable">
                Repeat Password
            </label>
            <asp:TextBox runat="server" ID="txttnewpass" TextMode="Password" CssClass="form-control"></asp:TextBox>
            <asp:Button runat="server" ID="btnchangepass" Text="Submit" CssClass="btn btn-primary createLoginPass" OnClick="btnchangepass_Click" />
        </div>

    </form>
</body>
</html>
