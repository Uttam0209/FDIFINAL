<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404_Error_page.aspx.cs" Inherits="User_404_Error_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Page Nor Found</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta charset="utf-8" />
    <link rel="icon" href="~/assets/images/icon.png" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <style>
        * {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
            color: #6915cf;
        }

        .container-fluid {
            height: 100vh;
            width: 100vw;
            background: url(../assets/images/bg.png) no-repeat center top;
            background-size: cover;
        }


        #myfont {
            font-size: 50px;
            font-weight: 900;
            padding-top: 30px;
        }

        .myfont2 {
            font-size: 24px;
            font-weight: 400;
        }

        .btn {
            background-color: #6915cf;
            border: solid #6915cf;
            color: #fff;
            padding: 10px;
            font-weight: 400;
        }

        a {
            text-decoration: none;
        }

            a:hover {
                background-color: #fff;
                color: #6915cf !important;
                font-weight: 400;
                border: solid #6915cf;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row d-flex justify-content-center" id="box" style="padding-top: 100px;">
                <div class="col-md-4 py-4" id="box1">
                    <p id="myfont">Error code: 404</p>
                    <p class="myfont2">We can't seem to find the page you're looking for.</p>
                    <a class="btn mx-auto md-auto" href="~/ProductList">BACK TO HOME PAGE</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
