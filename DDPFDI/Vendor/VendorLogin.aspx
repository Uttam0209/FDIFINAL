<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorLogin.aspx.cs" Inherits="Vendor_VendorLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Raksha Udyami Portal Login</title>
    <!---------Linked CSS files-------->
    <link href="~/Vendor/VendorAssets/css/Bootstrap.min.css" rel="stylesheet" />
    <link href="~/Vendor/VendorAssets/css/icons.min.css" rel="stylesheet" />
    <link href="~/Vendor/VendorAssets/css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
                <div class="account-pages login_bg">
                    <div class="container-fluid">
                        <div id="top" class="row p-1 bg-dark">
                            <div class="col-6">
                                <a href="#" style="color: white;"><i class="fa fa-envelope mr-3" aria-hidden="true"></i>&nbsp;helpdesk-dpit@ddpmod.gov.in</a>
                            </div>
                            <div class="col-6 text-right">
                                <a href="#" style="color: white;"><i class="fa fa-phone-square mr-3" aria-hidden="true"></i>&nbsp;011-20836145</a>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid mb-4" style="background: white; border-bottom: 1px solid #0000001c;">
                        <div class="row py-1">
                            <div class="col-md-2" style="align-items: center; display: flex;">
                                <a href="ProductList">
                                    <img src="vendor/VendorAssets/media/images/ddp_logo_dark.png" style="max-height: 60px" />
                                </a>
                            </div>
                            <div class="col-md-8 d-flex justify-content-center align-items-center">
                                <h2 class="text-center mb-0 mt-0 hero_heading" style="color: #6915cf;">RAKSHA UDYAMI PORTAL</h2>
                            </div>
                            <div class="col-md-2 text-right">
                                <img src="vendor/VendorAssets/media/images/raksha%20udymi%20number-02.png" width="110" />
                            </div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-xxl-4 mb-3 mt-2 col-lg-5">
                                <div class="card" style="border-radius: 30px;">
                                    <div class="card-body p-3">
                                        <div class="text-center w-75 m-auto">
                                            <h2 class="text-dark-50 mb-3 text-center pb-0 fw-bold text-primary">Sign In</h2>
                                        </div>
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">
                                            <div class="mb-3">
                                                <label for="emailaddress" class="form-label">Email address</label>
                                                <asp:TextBox runat="server" ID="txtUserName" class="form-control"
                                                    type="email" TabIndex="1" focus="true" autocomplete="off"
                                                    placeholder="Email" ToolTip="Please enter valid registerd email id." required="" autofocus=""></asp:TextBox>
                                            </div>
                                            <div class="mb-3">
                                                <small class="float-end">Password is case sensitive
                                                </small>
                                                <label for="password" class="form-label">Password</label>
                                                <div class="input-group input-group-merge">
                                                    <asp:TextBox runat="server" ID="txtPwd" TabIndex="2" name="txtPwd" class="form-control"
                                                        autocomplete="off" ToolTip="Please enter valid password (case sensitive)."
                                                        placeholder="Password" type="password" required=""></asp:TextBox>
                                                    <div class="input-group-text" data-password="false">
                                                        <span class="far fa-eye"></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <label for="psw" class=" tetLable">
                                                Captcha
                                            </label>
                                            <div class="mb-3">
                                                <div class="row">
                                                    <div class="col-sm-7">
                                                        <asp:TextBox runat="server" ID="txtCaptcha" TabIndex="3" ToolTip="enter captcha (case sensitive)" class="form-control"
                                                            autocomplete="off" placeholder="Captcha" required=""></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <div class="row">
                                                            <div class="col-sm-9">
                                                                <asp:Image ID="imgCaptcha" runat="server" Height="35px" />
                                                            </div>
                                                            <div class="col-sm-3" style="margin-top: 8px;">
                                                                <asp:LinkButton ID="btnCaptchaNew" runat="server" class="" OnClick="btnCaptchaNew_Click" CausesValidation="false">
                                                            <i class="fas fa-sync-alt fa-lg"></i></asp:LinkButton>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:LinkButton runat="server" ID="lbforgot" OnClick="lbforgot_Click" class="text-primary float-end">                                                
                                                    Forget your Password ?                                                
                                            </asp:LinkButton>
                                            <div class="mb-3 mb-3">

                                                <div class="form-check">
                                                    <asp:CheckBox runat="server" class="form-check-input" Style="margin-left: 3px;" ID="remberme" Checked="true" />
                                                    <label class="form-check-label" for="checkbox-signin">Remember me</label>
                                                </div>
                                            </div>
                                            <div class="mb-3 mb-0 text-center">
                                                <asp:LinkButton ID="btnLogin" runat="server" TabIndex="4" CssClass="btn btn-primary"
                                                    ToolTip="After validate your username or password we will redirect to your dashboard."
                                                    OnClick="btnLogin_Click" Text="Log In"></asp:LinkButton>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-12 text-center">
                                                    <p class="mb-0">Don't have an account? <a href="Registration" class="ms-1"><b>Register</b></a></p>
                                                </div>
                                                <!-- end col -->
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <!-- end card-body -->
                                </div>
                                <!-- end card -->
                            </div>
                            <!-- end col -->
                        </div>
                        <!-- end row -->
                    </div>
                    <!-- end container -->
                </div>
                <footer class="bg-dark p-2 text-center">
                    <p class="text-light mb-0">© 2021 www.srijandefence.gov.in | All Right Reserved. | Designed, Developed and Hosted by Department of Defence Production</p>
                </footer>
                <!-- Modal for forget Password-->
                <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel"
                    aria-hidden="true" data-backdrop="static" data-keyboard="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <asp:UpdatePanel runat="server" ID="upn" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">Forget Password</h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <div>
                                            <div class="mb-2">
                                                <label for="exampleInputEmail1" class="form-label">Email address</label>
                                                <asp:TextBox runat="server" ID="txtforgotemailid" class="form-control" TabIndex="8" autocomplete="off" type="email"
                                                    placeholder="Email" ToolTip="Please enter valid registerd email id." required="" autofocus=""></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                        <asp:LinkButton runat="server" ID="btnsendmail" TabIndex="9" CausesValidation="False"
                                            UseSubmitBehavior="False" CssClass="btn btn-primary"
                                            ToolTip="Get Reset password link on your registerd email id." OnClick="btnsendmail_Click">Get Reset Password Link</asp:LinkButton>
                                        <div class="clearfix"></div>
                                        <h4 class="alert alert-info" runat="server" id="am" visible="false">
                                            <asp:Label runat="server" ID="lbmsgforgot"></asp:Label></h4>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="modelmsg" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title"><i class="fa fa-bell "></i>&nbsp;Alert</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <h4>
                                    <asp:Label runat="server" ID="lblmsg" ForeColor="Red"></asp:Label></h4>
                                <asp:HiddenField runat="server" ID="hfredirec" />
                                <asp:Timer ID="timer1" runat="server" Interval="500" Enabled="false" EnableViewState="True" OnTick="timer1_Tick"></asp:Timer>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
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
    <script type="text/javascript">
        function showPopup() {
            $('#modelmsg').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#exampleModal').modal('show', function () {
            });
        }
    </script>
    <script src="Vendor/VendorAssets/js/jQuery.js"></script>
    <script src="Vendor/VendorAssets/js/app.min.js"></script>
    <script src="Vendor/VendorAssets/fontawesome-free-5.7.2-web/js/all.min.js"></script>
    <script src="Vendor/VendorAssets/js/dashboard.js"></script>
</body>
</html>
