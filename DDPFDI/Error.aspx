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
            max-width: 400px;
            width: 100%;
            margin: 14% auto;
            text-align: center;
        }

            .error-box h2 {
                font-size: 80px;
                line-height: 80px;
            }

            .error-box p {
                font-size: 28px;
                margin-top: 22px;
                font-family: sans-serif;
            }
    </style>
</head>
<body class="error-page">
    <form id="form1" runat="server">
        <div>
            <div class="error-box">
                <h2 class="main-logo">404</h2>
                <p>Page Not Found !</p>
                <br />
                <br />
                <p>Error Rendering Page.</p>
                <br />
                <h3>We encountered an error when rendering this page.
                This is a generic and non-specific error message. Sorry we do not have more information.</h3>
                <br />
                <p>You may wish to: Check the error logs or  <a href="https://rgera@gov.in" rel="nofollow">Contact Support</a></p>

            </div>
        </div>
    </form>
</body>
</html>
