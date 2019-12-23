<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorRegistrationStep1.aspx.cs" Inherits="Vendor_VendorRegistrationStep1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="~/assets/images/favicon.ico">
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500,700" rel="stylesheet">
    <link href="~/assets/css/bootstrap.css" rel="stylesheet">
    <link href="~/assets/css/select2.min.css" rel="stylesheet">
    <link href="~/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/custom.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/responsive.css" rel="stylesheet" type="text/css">
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link href="~/assets/css/aaryan.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="site-holder">
            <div id="preloader">
                <img src="~/assets/images/preloader.gif" alt="" />
            </div>
            <nav class="navbar" role="navigation">
                <div class="navbar-header">
                    <a class="navbar-brand" href="javascript:void(0)"><i class="fa fa-list btn-nav-toggle-responsive text-white"></i>
                        <span class="main-logo" title="Department of Defense Product">MOD</span>
                    </a>
                </div>
            </nav>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="cacade-forms">
                            <a href="VendorLogin" class="fa fa-backward pull-right mr10">Back</a>
                            <div class="clearfix mt10"></div>
                            <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Panel ID="panstep1" runat="server">
                                        <h3>MOD Vendor Registration</h3>
                                        <p>Please provide all required details to register your business with us</p>
                                        <div class="clearfix mt10"></div>

                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Are you Registered with Pan 
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlpan" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlpan_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="Y">YES</asp:ListItem>
                                                    <asp:ListItem Value="N">NO</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="divpan" visible="false">
                                            <div class="col-sm-5">
                                                PAN No 
                                            </div>
                                            <div class="col-sm-7">

                                                <asp:TextBox ID="txtpanno" runat="server" MaxLength="10" AutoPostBack="true" OnTextChanged="txtpanno_TextChanged" CssClass="form-control"></asp:TextBox>
                                                <span runat="server" id="panverifi"></span>
                                                <asp:Label ID="lblmsgpan" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hfpanname" runat="server" />
                                            </div>
                                        </div>

                                        <%--  <div class="form-group">
                                            <div class="col-sm-5">
                                                Are you Registered with GST 
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlregisterdgst" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlregisterdgst_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="Y">YES</asp:ListItem>
                                                    <asp:ListItem Value="N">NO</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="divgst" visible="false">
                                            <div class="col-sm-5">
                                                GSTIN No 
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtgstno" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>--%>

                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Name of firm/company 
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtbusinessname" runat="server" AutoPostBack="true" OnTextChanged="txtbusinessname_TextChanged" CssClass="form-control"></asp:TextBox>
                                                <span runat="server" id="check"></span>
                                                <asp:Label ID="lblbusinessname" runat="server" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Nodal Officer <span class="mandatory">*</span>
                                            </div>
                                            <div class="col-sm-7">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="ddltittle" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                                            <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                                            <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                            <asp:ListItem Value="Dr">Dr.</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <p>Prefix</p>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtfirstname" runat="server" required="" CssClass="form-control"></asp:TextBox>
                                                        <p>First Name</p>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtmiddlename" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>Middle Name</p>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtlastname" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>Last Name</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Type of Ownership
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddltypeofbusiness" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Business Sector
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlbusinesssector" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Registration Number
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtregno" runat="server" CssClass="form-control">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Registration Authority
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtregautho" runat="server" CssClass="form-control">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Registered Office Address
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtstreetaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                <p>Street Address</p>
                                                <div class="clearfix mt10"></div>
                                                <asp:TextBox ID="txtstreetaddressline2" runat="server" CssClass="form-control"></asp:TextBox>
                                                Street Address Line 2   
                                <div class="clearfix mt10"></div>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtcity" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>City</p>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtstateprovince" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>State</p>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtpostalzipcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>Pin Code</p>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlcountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <p>Country</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Email <span class="mandatory">*</span>
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtemail" runat="server" TextMode="Email" placeholder="ex: myemail@example.com" required="" AutoCompleteType="Email" CssClass="form-control"></asp:TextBox>
                                                <p>ex: myemail@example.com</p>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Contact No
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtstdcode" runat="server" CssClass="form-control"></asp:TextBox>

                                                <p>STD Code</p>
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                                                <p>Phone Number</p>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                Fax No
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtfaxstdcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                <p>STD Code</p>
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtfaxphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                                                <p>Phone Number</p>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <a href="VendorLogin" class="fa fa-backward pull-right mr10 btn btn-warning">Back</a>
                                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary pull-right" Text="Submit Registration" OnClick="btnsubmit_Click" />
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="up">
                                <ProgressTemplate>
                                    <!---Progress Bar ---->
                                    <div class="overlay-progress">
                                        <div class="custom-progress-bar blue stripes">
                                            <span></span>
                                            <p>Processing</p>
                                        </div>
                                    </div>
                                    <!---Progress Bar ---->
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="changePass" role="dialog" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog" style="width: 400px;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header modal-header1">
                        <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Alert</h4>
                    </div>
                    <form class="form-horizontal changepassword" role="form">
                        <div class="modal-body clearfix" style="padding: 0 20px;">
                            <asp:Panel ID="pansuccess" runat="server">
                                <div style="text-align: center;">
                                    <div class="clearfix pb15"></div>
                                    <img src="/assets/images/right.jpg" alt="" height="150px" width="150px" />
                                    <div class="clearfix pb15"></div>
                                    <h4>Registration succssfully done.A login releted mail send to your registerd mail id.</h4>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <div class="modal fade" id="modelfail" role="dialog" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog" style="width: 400px;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header modal-header1">
                        <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Alert</h4>
                    </div>
                    <form class="form-horizontal changepassword" role="form">
                        <div class="modal-body clearfix" style="padding: 0 20px;">

                            <asp:Panel ID="panfail" runat="server">
                                <div style="text-align: center;">
                                    <div class="clearfix pb15"></div>
                                    <img src="/assets/images/Wrong.png" alt="" height="100px" width="100px" />
                                    <div class="clearfix pb15"></div>
                                    <h4>Record not save, Please try again, or mail us on defence production</h4>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </form>
    <script src="assets/js/jquery-1.12.4.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap.min.css">
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>
    <script src="assets/js/select2.min.js"></script>
    <script src="assets/js/custom.js"></script>
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#modelfail').modal('show');
        }
    </script>
</body>
</html>

