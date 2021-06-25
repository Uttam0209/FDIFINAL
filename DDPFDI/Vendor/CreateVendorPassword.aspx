<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateVendorPassword.aspx.cs" Inherits="Vendor_CreateVendorPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                                <a href="#" style="color: white;"><i class="fa fa-envelope mr-3" aria-hidden="true"></i>helpdesk-dpit@ddpmod.gov.in</a>
                            </div>
                            <div class="col-6 text-right">
                                <a href="#" style="color: white;"><i class="fa fa-phone-square mr-3" aria-hidden="true"></i>011-20836145</a>
                            </div>
                        </div>
                    </div>

                    <div class="container-fluid mb-4" style="background: white; border-bottom: 1px solid #0000001c;">
                        <div class="row py-2">
                            <div class="col-md-2">
                                <a href="ProductList">
                                    <img src="vendor/VendorAssets/media/images/ddp_logo_dark.png" style="max-height: 60px" />
                                </a>
                            </div>
                            <div class="col-md-8 d-flex justify-content-center align-items-center">
                                <h2 class="text-center mb-0 mt-0 hero_heading" style="color: #6915cf;">RAKSHA UDYOG MITRA PORTAL </h2>
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                    </div>

                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-xxl-4 mb-5 mt-5 col-lg-5">
                                <div class="card">
                                    <div class="card-body p-3">
                                        <div class="text-center w-75 m-auto">
                                            <h2 class="text-dark-50 mb-1 text-center pb-0 fw-bold text-primary">Create Password</h2>

                                        </div>
                                        <div>
                                            <div class="mb-3">
                                                <label for="emailaddress" class="form-label">Password</label>
                                                <asp:TextBox runat="server" ID="txtpassword" TextMode="Password" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                                <div style="margin-top: 5px;"></div>
                                                <asp:RegularExpressionValidator ID="valPassword" Display="Dynamic" runat="server" ControlToValidate="txtpassword"
                                                    ErrorMessage="Password must be 8 characters long with at least one numeric,</br>one upper case character and one special character."
                                                    ForeColor="Red" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$#^@$!%*?&])[A-Za-z\d$@$#^!%*?&]{8,64}" />
                                            </div>
                                            <div class="mb-3">
                                                <label for="emailaddress" class="form-label">Repeat Password</label>
                                                <asp:TextBox runat="server" ID="txttnewpass" TextMode="Password" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                                <div style="margin-top: 5px;"></div>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ControlToValidate="txttnewpass"
                                                    ErrorMessage="Password must be 8 characters long with at least one numeric,</br>one upper case character and one special character."
                                                    ForeColor="Red" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$#^@$!%*?&])[A-Za-z\d$@$#^!%*?&]{8,64}" />
                                            </div>
                                            <div class="mb-3 mb-0 text-center">
                                                <asp:LinkButton runat="server" ID="btncreatepassword" CssClass="btn btn-primary" OnClick="btncreatepassword_Click"><i class="fa fa-lock"></i>&nbsp;Reset Password</asp:LinkButton>&nbsp;
                                                  <a runat="server" id="lblogin" href="~/VendorLogin" class="btn btn-primary" visible="false"><i class="fa fa-key"></i>&nbsp;Back to Login</a>
                                            </div>
                                            <p class="text-danger">Password Should contain (Alpha Numeric and Special charactor)</p>
                                            <p class="text-danger">Password should be minimum 8 or maximum 15 lenth</p>
                                        </div>
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
    <script src="Vendor/VendorAssets/js/jQuery.js"></script>
    <script src="Vendor/VendorAssets/js/app.min.js"></script>
    <script src="Vendor/VendorAssets/fontawesome-free-5.7.2-web/js/all.min.js"></script>
    <script src="Vendor/VendorAssets/js/dashboard.js"></script>
</body>
</html>
