<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorRegistration.aspx.cs" Inherits="Vendor_Notes_gethashforesign_VendorRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Raksha Udyami Portal</title>
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
                                        <div class="col-md-12 p-3">
                                            <h6 class="text-right mb-0 mt-0"><a href="VendorLogin" title="Back to Login Page"><i class="fa fa-arrow-left"></i>&nbsp;Back to Login</a></h6>
                                            <%-- <h2 class="text-primary mb-1 text-center">Raksha Udymi Portal
                                            </h2>--%>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="row" style="padding: 10px;">                                                
                                                <div class="mb-2 form-group">
                                                    <div>
                                                        <label class="mb-1">DPSU/Defence Organisation you want to apply&nbsp;*</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddldpsu" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class=" mb-2 form-group">
                                                    <label for="exampleFormControlInput1">
                                                        GSTIN&nbsp;*
                                                    </label>
                                                    <asp:TextBox ID="txtpanno" runat="server" MaxLength="15" Style="text-transform: uppercase;" AutoPostBack="true"
                                                        CssClass="form-control" OnTextChanged="txtpanno_TextChanged"></asp:TextBox>
                                                    <span runat="server" id="panverifi"></span>
                                                    <asp:Label ID="lblmsgpan" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                                    <asp:HiddenField ID="hfpanname" runat="server" />
                                                </div>
                                                <div class=" mb-2 form-group">
                                                    <label for="exampleFormControlInput1">
                                                        Name of Firm&nbsp;*
                                                    </label>
                                                    <asp:TextBox ID="txtbusinessname" runat="server" AutoPostBack="true" Style="text-transform: uppercase;"
                                                        CssClass="form-control" OnTextChanged="txtbusinessname_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="lblbusinessname" runat="server" ForeColor="Green"></asp:Label>
                                                </div>
                                                <div class=" mb-2 form-group">
                                                    <label for="exampleFormControlInput1">
                                                        Nodal officer Name&nbsp;*
                                                    </label>
                                                    <asp:TextBox ID="txtnodalname" runat="server" MaxLength="100" required="" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class=" mb-2 form-group">
                                                    <label for="exampleFormControlInput1">
                                                        Nodal officer email (treated as username)&nbsp;*
                                                    </label>
                                                    <%--AutoPostBack="true" OnTextChanged="txtnodelemail_TextChanged"--%>

                                                    <asp:TextBox ID="txtnodelemail" runat="server" MaxLength="70" TextMode="Email"
                                                         placeholder="ex: myemail@example.com" required=""
                                                        AutoCompleteType="Email" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ControlToValidate="txtnodelemail" ForeColor="Red" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class=" mb-2 form-group">
                                                    <label for="exampleFormControlInput1">
                                                        Authority letter to be issued from the organization&nbsp;*
                                                    </label>
                                                    <div class="row">
                                                        <div class="col-sm-8">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control mdi-file-upload" />
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton runat="server" ID="lbuploadfile" CssClass="btn btn-info" OnClick="lbuploadfile_Click"><i class=" fa fa-file"></i>&nbsp;Upload</asp:LinkButton>
                                                        </div>
                                                        <p>
                                                            <asp:Label runat="server" ID="fupmsg" ForeColor="Green"></asp:Label>
                                                        </p>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 mb-2 mt-1">
                                                    <div class="text-sm-end">
                                                        <asp:LinkButton runat="server" ID="btnclear" CssClass="btn btn-primary" OnClick="btnclear_Click"><i class="fa fa-spinner"></i>&nbsp;Reset</asp:LinkButton>
                                                        &nbsp;&nbsp;
                                                <asp:LinkButton ID="lbnext" runat="server" Enabled="false" OnClick="lbnext_Click" CssClass="btn btn-primary" Style="float: right;">
                                         <i class="fas fa-save"></i>&nbsp;Submit</asp:LinkButton>

                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                                <p class="mb-0 text-left">
                                                    1.&nbsp;Please provide all required details to register your business with us
                                                </p>
                                                <p class="mb-0 text-left">
                                                    2.&nbsp;Please fill all details below. After Submission you will get email an for your login credentials's
                                                </p>
                                                <p class="mb-0 text-left">3.&nbsp;(*) Marks are mandatory fields.</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end row -->
                        </div>
                    </div>
                    <!-- end container -->
                </div>
                <footer class="bg-dark p-2 text-center">
                    <p class="text-light mb-0">© 2021 www.srijandefence.gov.in | All Right Reserved. | Designed, Developed and Hosted by Department of Defence Production</p>
                </footer>
                <div class="modal fade" id="verifyemail" tabindex="-1">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">OTP</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <asp:UpdatePanel runat="server" ID="a" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <div class="mb-2">
                                            <label for="exampleInputEmail1" class="form-label">OTP (Received on your typed email-on)</label>
                                            <asp:TextBox runat="server" ID="txtotp" class="form-control" TabIndex="1"
                                                placeholder="Six digit OTP" ToolTip="Please enter valid six digit otp." required="" autofocus=""></asp:TextBox>
                                        </div>
                                        <div class="clearfix mt-1"></div>
                                        <asp:Label ID="lblmsgotp" runat="server"></asp:Label>

                                        </form>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                        <asp:LinkButton runat="server" ID="btnValidateEmailId" TabIndex="2" CausesValidation="False"
                                            UseSubmitBehavior="False" CssClass="btn btn-primary"
                                            ToolTip="Validate Email id." OnClick="btnValidateEmailId_Click">Verify Email-Id</asp:LinkButton>
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
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lbuploadfile" />
            </Triggers>
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
            $('#verifyemail').modal('show', function () {
            });
        }
    </script>
    <script src="Vendor/VendorAssets/js/jQuery.js"></script>
    <script src="Vendor/VendorAssets/js/bootstrap.bundle.js"></script>
    <script src="Vendor/VendorAssets/js/app.min.js"></script>
    <script src="Vendor/VendorAssets/fontawesome-free-5.7.2-web/js/all.min.js"></script>
    <script src="Vendor/VendorAssets/js/dashboard.js"></script>
</body>
</html>
