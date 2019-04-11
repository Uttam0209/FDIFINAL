<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link type="text/css" rel="shortcut icon" href="assets/images/favicon.ico">
    <link type="text/css" href="assets/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/css/font-awesome.css" rel="stylesheet">
    <link href="assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="assets/css/custom.css" rel="stylesheet" type="text/css">
    <link href="assets/css/responsive.css" rel="stylesheet" type="text/css">

    <script type="text/javascript" src="assets/js/loader.js"></script>
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
                    <span class="main-logo" title="Foreign Direct Investment">FDI </span>
                </a>
            </div>

            <!-- Collect the nav links, forms, and other content for toggling -->

            <!-- /.navbar-collapse -->
        </nav>
        <div class="container">
            <div class="row">
                <div class="loginBg clearfix">
                    <h3>Login</h3>
                   
                    <label for="uname" class=" tetLable">
                        Email
                    </label>
                    <asp:TextBox runat="server" ID="txtUserName" class="form-control" autocomplete="off" type="email" placeholder="Email" required="" autofocus=""></asp:TextBox>
                    <label for="psw" class=" tetLable">
                        Password
                    </label>
                    <asp:TextBox runat="server" ID="txtPwd" TabIndex="1" name="txtPwd" class="form-control" autocomplete="off" placeholder="Password" type="password" required=""></asp:TextBox>
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
                                    <asp:TextBox runat="server" name="txtCaptcha" type="text" ID="txtCaptcha" TabIndex="3" class="form-control" autocomplete="off" placeholder="Captcha" required=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="col-sm-9">
                                    <asp:Image ID="Image2" runat="server" Height="35px" ImageUrl="~/CaptchaCall.aspx"
                                        Width="120px" />
                                </div>
                                <div class="col-sm-1" style="margin-top: 15px;">
                                    <asp:LinkButton ID="btnCaptchaNew" runat="server" class="" CausesValidation="false" OnClick="btnCaptchaNew_Click"><i class="fas fa-sync-alt fa-lg"></i></asp:LinkButton>
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
                    <asp:LinkButton ID="btnLogin" runat="server" TabIndex="4" CssClass="btn btn-info"
                        CausesValidation="true" OnClick="btnLogin_Click">Login</asp:LinkButton>
                    <span class="psw"></span>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
