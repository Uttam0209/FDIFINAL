<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

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
    <style>
        .error-box {
            max-width: 85%;
            width: 100%;
            margin: 14% auto;
            text-align: center;
        }

            .error-box h2 {
                font-size: 80px;
                line-height: 80px;
            }

            .error-box h3 {
                font-size: 28px;
            }

            .error-box p {
                font-size: 18px;
                margin-top: 15px;
                font-family: sans-serif;
            }

            .error-box a {
                color: #494ca2;
                text-decoration: underline;
            }
    </style>
</head>
<body class="error-page">
    <form id="form1" runat="server">
        <div>
            <div class="error-box">
                <h2 class="main-logo">404</h2>
                <h3>Error Rendering Page.</h3>

                <p>We encountered an error when rendering this page.</p>
                <p>This is a generic and non-specific error message. Sorry we do not have more information.</p>
                <p>
                    You may wish to Check the error logs or  <a href="https://rgera@gov.in" rel="nofollow">Contact Support
                    </a>
                </p>
                <asp:Label ID="FriendlyErrorMsg" runat="server" Text="Label" Font-Size="Large" Style="color: red"></asp:Label>
                <asp:Panel ID="DetailedErrorPanel" runat="server" Visible="false">
                    <p>&nbsp;</p>
                    <h4>Detailed Error:</h4>
                    <p>
                        <asp:Label ID="ErrorDetailedMsg" runat="server" Font-Size="Small" /><br />
                    </p>
                    <h4>Error Handler:</h4>
                    <p>
                        <asp:Label ID="ErrorHandler" runat="server" Font-Size="Small" /><br />
                    </p>
                    <h4>Detailed Error Message:</h4>
                    <p>
                        <asp:Label ID="InnerMessage" runat="server" Font-Size="Small" /><br />
                    </p>
                    <p>
                        <asp:Label ID="InnerTrace" runat="server" />
                    </p>
                </asp:Panel>

            </div>


        </div>
    </form>
</body>
</html>
