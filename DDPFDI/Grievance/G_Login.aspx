<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G_Login.aspx.cs" Inherits="Grievance_G_Login" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HelpDesk Login</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="~/assets/images/favicon.ico">
    <link href="~/assets/css/bootstrap.css" rel="stylesheet">
    <link href="~/assets/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/custom.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/responsive.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/jquery.bxslider.css" rel="stylesheet" type="text/css">
    <style>
        .swal2-container.swal2-center.swal2-shown {
            z-index: 22222;
        }

        .modal {
            margin-top: 70px !important;
        }
    </style>
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
    </style>
</head>
<body style="min-height: 100vh; background-image: linear-gradient(to right, #e83e8c, #6610f2);">
    <form id="form1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
                <div id="top" style="margin-left: 34px; height: 25px; margin-top: 6px;">
                    <a href="#" style="color: white;"><i class="fa fa-envelope" aria-hidden="true"></i>&nbsp; helpdesk-dpit@ddpmod.gov.in</a>
                    <a href="#" style="color: white;"><i class="fa fa-phone-square" aria-hidden="true"></i>&nbsp; 011-20836145 &nbsp;</a>
                </div>

                <div id="top2" class="container-fluid" style="background: white;">
                    <div class="row py-3">
                        <div class="col-md-2">
                            <a href="ProductList">
                                <img src="ddp_logo.png" class="img-fluid" style="max-height: 70px"></a>
                        </div>
                        <div class="col-md-8">
                            <h2 class="text-center" style="color: #6915cf;">Grievance Portal Login </h2>
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="loginBg VendorloginBg clearfix">
                                <div class="col-sm-7">
                                    <h6 class="py-2 text-center my-2" style="height: 34px; background-image: linear-gradient(to right, #e83e8c, #6610f2); color: white; border-radius: 5px; font-size: 12px;">
                                        <br />
                                        <b>Grievance Portal Login</b></h6>
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
                                                        <asp:TextBox runat="server" ID="txtCaptcha" TabIndex="3" ToolTip="enter captcha (case sensitive)" class="form-control" autocomplete="off" placeholder="Captcha" required=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="col-sm-9">
                                                        <asp:Image ID="imgCaptcha" runat="server" />
                                                    </div>
                                                    <div class="col-sm-1" style="margin-top: 15px;">
                                                        <asp:LinkButton ID="btnCaptchaNew" runat="server" class="" OnClick="btnCaptchaNew_Click" CausesValidation="false"><i class="fas fa-sync-alt fa-lg"></i></asp:LinkButton>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix" style="border-bottom: 1px solid #fff; margin-top: 15px;">
                                        </div>
                                        <br>
                                        <div class="clearfix" style="border-bottom: 1px solid #fff; margin-top: 15px; text-align: left;">
                                            <asp:LinkButton runat="server" ID="lblforgotpass" Text="Forgot Password ?" OnClick="lblforgotpass_Click"></asp:LinkButton>
                                        </div>
                                        <br />
                                        <div class="clearfix p10">
                                        </div>
                                        <asp:LinkButton ID="btnLogin" runat="server" TabIndex="4" CssClass="btn btn-info" ToolTip="After validate your username or password we will redirect to your dashboard."
                                            OnClick="btnLogin_Click" Text="Login"></asp:LinkButton>
                                        <br />
                                        <br />
                                        <div>
                                            <asp:LinkButton CssClass="btn btn-primary pull-right" ID="lbgregis" runat="server" Text="Register" OnClick="lbgregis_Click" Style="width: 324px; border-radius: 4px"></asp:LinkButton>
                                        </div>
                                        <div class="form-group pt-2 d-flex justify-content-center">
                                            <label for="pwd" style="margin-left: 63px;">© 2021. All RIGHT RESERVED.</label>
                                        </div>
                                        <span class="psw"></span>
                                    </asp:Panel>
                                </div>
                                <div class="col-sm-5" runat="server" id="test" visible="false">
                                    <h3>Already Registered?</h3>
                                    <div class="clearfix" style="margin-top: 20px;"></div>
                                    <p>Login using your username and password to :-</p>
                                    <p>
                                        If not registered, register for
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="changePass" role="dialog" data-keyboard="false" data-backdrop="static">
                    <div class="modal-dialog" style="width: 400px;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-content" runat="server" id="p1">
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
                                                <asp:TextBox runat="server" ID="txtforgotemailid" class="form-control" TabIndex="8" autocomplete="off" type="email" placeholder="Email" ToolTip="Please enter valid registerd email id." required="" autofocus=""></asp:TextBox>
                                            </div>
                                            <div class="form-group" style="margin: 0">
                                                <asp:LinkButton runat="server" ID="btnsendmail" Text="Get Reset Password Link" TabIndex="9" CausesValidation="False" UseSubmitBehavior="False" CssClass="btn btn-primary pull-right forgot-pass-btn"
                                                    ToolTip="Get Reset password link on your registerd email id." OnClick="btnsendmail_Click"></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </form>
                                </div>
                                <div class="modal-content" runat="server" style="width: 400px; z-index: 99999;" id="divregistration" visible="false">
                                    <div class="modal-header modal-header1">
                                        <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                        <h4 class="py-2 text-center my-2" style="background-image: linear-gradient(to right, #e83e8c, #6610f2); color: white; border-radius: 5px; margin-top: 20px; height: 25px;"><b>Grievance Protal Registration</b></h4>

                                        <div class="col-12">
                                            <div class="d-flex">
                                                <div class="form-group">
                                                    <label>
                                                        Type 
                                                    </label>
                                                    <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                                        <asp:ListItem Value="Manager">Manager</asp:ListItem>
                                                        <asp:ListItem Value="Developer">Developer</asp:ListItem>
                                                        <asp:ListItem Value="Helpdesk">Helpdesk</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Name  <span class="mandatory">*</span>
                                                    </label>
                                                    <asp:TextBox ID="txtname" runat="server" MaxLength="150" placeholder="Name" required="" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Nodal officer email (treated as username) <span class="mandatory">*</span>
                                                    </label>
                                                    <asp:TextBox ID="txtnodelemail" runat="server" MaxLength="70" TextMode="Email" placeholder="ex: myemail@example.com" required="" AutoCompleteType="Email" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Mobile-No <span class="mandatory">*</span>
                                                    </label>
                                                    <asp:TextBox ID="txtmobileNo" runat="server" MaxLength="10" onkeypress="return isNumberKey(event)" TextMode="Phone" placeholder="ex: 1234567890" required="" AutoCompleteType="HomePhone" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="d-flex">
                                                <asp:LinkButton runat="server" ID="btnsubmit" CssClass="btn btn-primary pull-right" Style="margin-right: 10px;" OnClick="btnsubmit_Click"><i class="fa fa-plus-circle"></i>&nbsp;Submit</asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnsubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                </b>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="overlay-progress" style="z-index: 99999;">
                    <div class="custom-progress-bar blue stripes">
                        <span></span>
                        <p>Processing</p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
    <script src="assets/js/jquery-1.12.4.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/jquery.bxslider.min.js"></script>
    <script src="assets/js/custom.js"></script>
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show', function () {
            });
        }
    </script>
    <style>
        .modal.and.carousel {
            position: fixed;
        }

        .carousel-control-prev {
            position: absolute;
            left: 0;
            bottom: 0;
        }

            .carousel-control-prev i {
                font-size: 30px;
                font-weight: bold;
            }

        .carousel-control-next {
            position: absolute;
            right: 0;
            bottom: 0;
        }

            .carousel-control-next i {
                font-size: 30px;
                font-weight: bold;
            }

        .carousel-inner {
            padding: 0 20px;
        }

        .mt10 {
            padding-top: 10px;
        }
    </style>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
</body>
</html>
