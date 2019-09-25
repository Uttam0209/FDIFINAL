<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreatePasswordCompany.aspx.cs" Inherits="Admin_CreatePasswordCompany" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link type="text/css" rel="shortcut icon" href="~/assets/images/favicon.ico">
    <link rel="shortcut icon" href="assets/images/favicon.ico">
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500,700" rel="stylesheet">
    <link href="~/assets/css/bootstrap.css" rel="stylesheet">
    <link href="~/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/custom.css" rel="stylesheet" type="text/css">
    <script src="assets/js/jquery-3.4.1.js"></script>
    <script>
        function SuccessfullPop(data) {
            $("body").addClass('CaptchaError');
            $("#alertPopupS").show();
            $("#alertPopupS .alertMsg").append(data);
            return false;
        }
        //Hide Alert Pop up
        $('.close_alert').on('click', function () {
            $("body").css('overflow', 'visible');
            $('.alert-overlay-successful').hide();
        });
    </script>
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
    <form id="form1" runat="server">
        <div class="loginBg clearfix">
            <h3>Create New Password</h3>
            <label for="pass" class="tetLable">
                Password
            </label>
            <asp:TextBox runat="server" ID="txtpassword" TextMode="Password" MaxLength="15" CssClass="form-control"></asp:TextBox>
            <div style="margin-top: 5px;"></div>
            <asp:RegularExpressionValidator ID="valPassword" runat="server" ControlToValidate="txtpassword"
                ErrorMessage="Minimum Length is (8) charactor" ForeColor="Red" ValidationExpression="^[\s\S]{8,15}$" />
            <label for="pass" class="tetLable">
                Repeat Password
            </label>
            <asp:TextBox runat="server" ID="txttnewpass" TextMode="Password" MaxLength="15" CssClass="form-control"></asp:TextBox>
            <div style="margin-top: 5px;"></div>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txttnewpass"
                ErrorMessage="Minimum Length is (8) charactor" ForeColor="Red" ValidationExpression="^[\s\S]{8,15}$" />
            <asp:Button runat="server" ID="btnchangepass" Text="Submit" CssClass="btn btn-primary createLoginPass" OnClick="btnchangepass_Click" />
        </div>
        <!-----Alert Box Success Fail Massage Popup ------>
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
        <div id="alertPopupS" class="alert-overlay alert-overlay-successful" style="display: none">
            <div class="alert-box">
                <div class="box">
                    <div class="success-checkmark">
                        <div class="check-icon">
                            <span class="icon-line line-tip"></span>
                            <span class="icon-line line-long"></span>
                            <div class="icon-circle"></div>
                            <div class="icon-fix"></div>
                        </div>
                    </div>
                    <div class="alert alertMsg">
                    </div>
                    <button class="btn btn-success close_alert">Close</button>
                </div>
            </div>
        </div>
        <!-----End Alert Box ------>
    </form>
</body>
</html>
