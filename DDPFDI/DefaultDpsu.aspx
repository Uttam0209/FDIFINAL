<%@ Page Language="C#" AutoEventWireup="true" ErrorPage="~/ErrorPages/Error.aspx" CodeFile="DefaultDpsu.aspx.cs" Inherits="_DefaultD" %>

<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel="shortcut icon" href="~/assets/images/icon.png">
    <link href="~/assets/css/bootstrap.css" rel="stylesheet">
    <link href="~/assets/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/custom.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/responsive.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/fonts-googleapis.css" rel="stylesheet" />
    <style>
        #top {
            background-color: #373f50;
            text-transform: uppercase;
            color: #f1faee !important;
            padding-bottom: 0px !important;
            padding-top: 0px !important;
            padding-left: 20px;
        }

            #top a {
                color: #f1faee !important;
                display: inline-block;
                padding: 15px 10px 15px 10px;
                text-decoration: none;
            }


        #top2 {
            box-shadow: 0 0 5px;
        }

        #footer2 {
            background-color: #0c0032;
            color: #f1faee;
            width: 100%;
            text-align: center;
            padding: 15px !important;
        }


            #footer2 a {
                color: white !important;
                text-decoration: none !important;
            }


        .swal2-container.swal2-center.swal2-shown {
            z-index: 22222;
        }

        .modal {
            margin-top: 70px !important;
        }

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

        @media screen and (max-width: 993px) {

            #top2 {
                align-content: center;
            }
        }

        @media screen and (max-width: 993px) {

            #top2 h2 {
                text-align: center;
            }

            #top2 .col-md-3 {
                text-align: center;
            }
        }
    </style>
    <script src="assets/js/jquery-3.4.1.js"></script>
    <script>
        function ErrorMssgPopup(data) {
            $("body").addClass('CaptchaError');
            $("#alertPopup").show();
            $("#alertPopup .alertMsg").append(data);
            return false;
        }
        //Hide Alert Pop up
        $('.close_alert').on('click', function () {
            $("body").css('overflow', 'visible');
            $('.alert-overlay-error').hide();
        });

    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" asp-antiforgery="false">
        <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
        <div id="top">
            <a href="#"><i class="fa fa-envelope" aria-hidden="true"></i>&nbsp; helpdesk-dpit@ddpmod.gov.in</a>
            <a href="#"><i class="fa fa-phone-square" aria-hidden="true"></i>&nbsp; 011-20836145 &nbsp;|&nbsp; 011-23019066</a>
            <li class="nav-item active" style="float: right; margin-right: 30px">
                <a class="nav-link" runat="server" href="~/ProductList"><i class="fa fa-home" aria-hidden="true"></i></a>
            </li>
        </div>
        <div id="top2" class="container-fluid" style="background: white;">

            <div class="row">
                <div class="col-md-2">
                    <a href="ProductList">
                        <img src="ddp_logo.png" class="img-fluid" style="max-height: 70px" /></a>
                </div>
                <div class="col-md-8">
                    <h2 class="text-center" style="color: #6915cf;">OPPORTUNITIES FOR MAKE IN INDIA IN DEFENCE</h2>
                </div>
                <div class="col-md-2">
                </div>
            </div>
        </div>
        <div class="container" style="min-height: 83vh;">
            <div class="row">
                <div style="margin: 5px; color: red; padding: 5px;">
                    <marquee scrollamount="3"><p>Note:-&nbsp;1.The Menus has been Reorganised please contact helpdesk-dpit@ddpmod.gov.in for any clarifications.</p> </marquee>

                </div>
                <div class="loginBg clearfix">
                    <h2 style="text-align: center; margin: 0px; color: black; padding: 5px;">Login</h2>
                    <div class="text-center">
                        <img id="logo" src="assets/images/login.png" class="mx-auto d-block rounded-circle " />
                    </div>
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">
                        <label for="uname" class=" tetLable">
                            Email
                        </label>
                        <asp:TextBox runat="server" ID="txtUserName" class="form-control" TabIndex="1" focus="true" autocomplete="off" type="email" placeholder="Email" ToolTip="Please enter valid registerd email id." required="" autofocus=""></asp:TextBox>
                        <label for="psw" class=" tetLable">
                            Password
                        </label>
                        <span class="passbox">
                            <asp:TextBox runat="server" ID="txtPwd" TabIndex="2" name="txtPwd" class="form-control passField" autocomplete="off" ToolTip="Please enter valid password (case sensitive)." placeholder="Password" type="password" required=""></asp:TextBox>
                            <%-- <asp:RegularExpressionValidator ID="valPassword" Display="Dynamic" runat="server" ControlToValidate="txtPwd"
                            ErrorMessage="Password must be 8 characters long with at least one numeric,one upper case character and one special character." ForeColor="Red" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$#^@$!%*?&])[A-Za-z\d$@$#^!%*?&]{8,64}" />--%>
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
                                        <asp:TextBox runat="server" ID="txtCaptcha" TabIndex="3" ToolTip="enter captcha (case sensitive)"
                                            class="form-control text-uppercase" autocomplete="off" placeholder="Captcha" required=""></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="col-sm-9">
                                        <asp:Image ID="imgCaptcha" runat="server" />
                                    </div>
                                    <div class="col-sm-1" style="margin-top: 15px;">
                                        <asp:LinkButton ID="btnCaptchaNew" runat="server" class="" CausesValidation="false" OnClick="btnCaptchaNew_Click"><i class="fas fa-sync-alt fa-lg"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <p style="margin-top: 15px;">
                                    Captcha is case sensitive
                                </p>
                            </div>
                        </div>
                        <div class="clearfix" style="border-bottom: 1px solid #fff; margin-top: 10px;">
                        </div>
                        <br />
                        <asp:LinkButton ID="btnLogin" runat="server" TabIndex="4" CssClass="btn btn-info" ToolTip="After validate your username or password we will redirect to your dashboard."
                            OnClick="btnLogin_Click" Text="Login"></asp:LinkButton>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <div class="clearfix" style="border-bottom: 1px solid #fff; margin-top: 15px; text-align: right;">
                                    <asp:LinkButton runat="server" ID="lblforgotpass" Text="Forgot Password ?" OnClick="lblforgotpass_Click" Style="text-decoration: none;"></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="clearfix" style="margin-top: 15px;">
                        </div>
                        <p>Note:- For better site experience please use Google Chrome, Mozilla FireFox, Internet Edge, Safari.</p>

                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="modal fade" id="changePass" role="dialog" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog" style="width: 400px;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header modal-header1">
                        <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Forgot Password</h4>
                    </div>
                    <form class="form-horizontal changepassword" role="form">
                        <div class="modal-body clearfix" style="padding: 0 20px;">
                            <div class="form-group" style="margin: 0">
                                <label for="uname" class=" tetLable">
                                    Email
                                </label>
                                <asp:TextBox runat="server" ID="txtforgotemailid" class="form-control" autocomplete="off" type="email" placeholder="Email" ToolTip="Please enter valid registered email id." required="" autofocus=""></asp:TextBox>
                            </div>
                            <div class="form-group" style="margin: 0">
                                <asp:Button runat="server" ID="btnsendmail" Text="Get Reset Password Link" CausesValidation="False" UseSubmitBehavior="False" CssClass="btn btn-primary pull-right forgot-pass-btn" ToolTip="Get Reset password link on your registered email id." OnClick="btnsendmail_Click" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <div id="alertPopup" class="alert-overlay alert-overlay-error" style="display: none">
            <div class="alert-box">
                <div class="box">
                    <div class="error-checkmark">
                        <i class="fa fa-exclamation-triangle" aria-hidden="true"></i>
                    </div>
                    <div class="alert alertMsg">
                    </div>
                    <button class="btn btn-success close_alert">Close</button>
                </div>
            </div>
        </div>

        <div id="footer1" class="container-fluid" style="min-height: 50px; text-align: center; background: #373f50;">
            <div class="container">
                <div class="row">
                    <div class="col-12" style="padding-top: 10px; color: white;">
                        ©2020 <a href="https://srijandefence.gov.in/ProductList" style="color: white;">www.srijandefence.gov.in</a> | All Right Reserved. | Designed, Developed and Hosted by Department of Defence Production                           
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="assets/js/jquery-3.4.1.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/custom.js"></script>
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
        }
    </script>
</body>
</html>
