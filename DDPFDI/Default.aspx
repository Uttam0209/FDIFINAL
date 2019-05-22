﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="~/assets/images/favicon.ico">
    <link href="~/assets/css/bootstrap.css" rel="stylesheet">
    <link href="~/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/custom.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/responsive.css" rel="stylesheet" type="text/css">
    <!-- <script type="text/javascript" src="assets/js/loader.js"></script> -->
    <style>
        .swal2-container.swal2-center.swal2-shown {
            z-index: 22222;
        }

        .modal {
            margin-top: 70px !important;
        }
    </style>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements. All other JS at the end of file. -->
    <!--[if lt IE 9]>
      <script src="js/html5shiv.js"></script>
      <![endif]-->
    <style type="text/css">
        .form-group {
            margin: 8px 0px;
        }

        .hhead {
            background-color: #f5f5f5;
            color: #000;
            border: 0px;
            margin-top: 8px !important;
            margin-bottom: 8px !important;
            padding: 10px 10px;
            border-radius: 0px;
            font: normal 14px/18px Arial, Helvetica, sans-serif;
        }
        /* .indiacompanydetails
    {
      display: none;
    }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar" role="navigation">

            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <a class="navbar-brand" href="#">
                    <span class="main-logo" title="Department of Defense Product">DDP</span>
                </a>
            </div>

            <!-- Collect the nav links, forms, and other content for toggling -->

            <!-- /.navbar-collapse -->
        </nav>
        <div class="container">
            <div class="row">
                <div class="loginBg clearfix">
                    <h3>Login</h3>
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">
                        <label for="uname" class=" tetLable">
                            Email
                        </label>
                        <asp:TextBox runat="server" ID="txtUserName" class="form-control" TabIndex="0" autocomplete="off" type="email" placeholder="Email" ToolTip="Please enter valid registerd email id." required="" autofocus=""></asp:TextBox>
                        <label for="psw" class=" tetLable">
                            Password
                        </label>
                        <span class="passbox">
                            <asp:TextBox runat="server" ID="txtPwd" TabIndex="1" name="txtPwd" class="form-control passField" autocomplete="off" ToolTip="Please enter valid password (case sensitive)." placeholder="Password" type="password" required=""></asp:TextBox>
                            <span toggle="#password-field" class="fa fa-fw fa-eye field-icon toggle-password"></span>
                        </span>
                        <p style="margin-top: 15px;">
                            Password is case sensitive
                        </p>
                        <label for="psw" class=" tetLable">
                            Captcha
                        </label>
                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="row">
                                        <asp:TextBox runat="server" name="txtCaptcha" type="text" ID="txtCaptcha" TabIndex="2" ToolTip="enter captcha (case sensitive)" class="form-control" autocomplete="off" placeholder="Captcha" required=""></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="col-sm-9">
                                        <asp:Image ID="Image2" runat="server" Height="35px" ImageUrl="~/CaptchaCall.aspx"
                                            Width="120px" />
                                    </div>
                                    <div class="col-sm-1" style="margin-top: 15px;">
                                        <asp:LinkButton ID="btnCaptchaNew" runat="server" class="" TabIndex="4" CausesValidation="false" OnClick="btnCaptchaNew_Click"><i class="fas fa-sync-alt fa-lg"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="clearfix" style="border-bottom: 1px solid #fff; margin-top: 15px;">
                        </div>
                        <br>
                        <asp:CustomValidator ID="CustomValidator1" ErrorMessage="" OnServerValidate="ValidateCaptcha"
                            runat="server" />
                        <div class="clearfix p10">
                        </div>
                        <asp:LinkButton ID="btnLogin" runat="server" TabIndex="3" CssClass="btn btn-info" ToolTip="After validate your username or password we will redirect to your dashboard."
                            CausesValidation="true" OnClick="btnLogin_Click">Login</asp:LinkButton>
                        <span class="psw"></span>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
    <script src="assets/js/jquery-1.12.4.js">
    </script>
    <script src="assets/js/custom.js"></script>
</body>
</html>
