<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorLogin.aspx.cs" Inherits="Vendor_VendorLogin" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vendor Login</title>
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
<body>
    <form id="form1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
                <nav class="navbar" role="navigation">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="#">
                            <span class="main-logo" title="Department of Defense Product">DDP</span>
                        </a>
                    </div>
                </nav>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="loginBg VendorloginBg clearfix">
                                <div class="col-sm-7" style="border-right: solid; border-color: #ccc;">
                                    <h3>Login</h3>
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
                                                        <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="6"
                                                            CaptchaHeight="35" CaptchaWidth="125" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                                            FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                                                    </div>
                                                    <div class="col-sm-1" style="margin-top: 15px;">
                                                        <asp:LinkButton ID="btnCaptchaNew" runat="server" class="" CausesValidation="false"><i class="fas fa-sync-alt fa-lg"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix" style="border-bottom: 1px solid #fff; margin-top: 15px;">
                                        </div>
                                        <br>
                                        <asp:CustomValidator ID="CustomValidator1" ErrorMessage="" OnServerValidate="ValidateCaptcha"
                                            runat="server" />
                                        <div class="clearfix p10">
                                        </div>
                                        <asp:LinkButton ID="btnLogin" runat="server" TabIndex="4" CssClass="btn btn-info" ToolTip="After validate your username or password we will redirect to your dashboard."
                                            OnClick="btnLogin_Click" Text="Login"></asp:LinkButton>
                                        <span class="psw"></span>
                                        <div class="clearfix" style="border-bottom: 1px solid #fff; margin-top: 15px; text-align: right;">
                                            <asp:LinkButton runat="server" ID="lblforgotpass" Text="Forgot Password ?" OnClick="lblforgotpass_Click"></asp:LinkButton>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-sm-5">
                                    <h3>Buyer - Already Registered?</h3>
                                    <div class="clearfix" style="margin-top: 20px;"></div>
                                    <p>Login using your username and password to :-</p>
                                    <p>
                                        If not registered, register as a Buyer for
                                       <b>
                                           <asp:LinkButton ID="lbvenregis" runat="server" Text="Free" OnClick="lbvenregis_Click"></asp:LinkButton></b>
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
                                                <asp:LinkButton runat="server" ID="btnsendmail" Text="Get Reset Password Link" TabIndex="9" CausesValidation="False" UseSubmitBehavior="False" CssClass="btn btn-primary pull-right forgot-pass-btn" ToolTip="Get Reset password link on your registerd email id." OnClick="btnsendmail_Click"></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </form>
                                </div>
                                <div class="modal-content" runat="server" id="P3">
                                    <div class="modal-header modal-header1">
                                        <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">OTP</h4>
                                    </div>
                                    <form class="form-horizontal changepassword" role="form">
                                        <div class="modal-body clearfix" style="padding: 0 20px;">
                                            <div class="form-group">
                                                <label>Enter OTP <span class="mandatory">*</span></label>
                                                <asp:TextBox runat="server" ID="txtOTP" type="password" MaxLength="6" TabIndex="4" focus="true" class="form-control " placeholder="Enter OTP">
                                                </asp:TextBox>
                                                <asp:Label ID="lblmssg" runat="server"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="modal-footer">
                                                <asp:LinkButton runat="server" ID="lblresendotp" CausesValidation="False" UseSubmitBehavior="False" CssClass="btn btn-primary pull-right forgot-pass-btn" TabIndex="7" Text="Resend OTP" OnClick="lblresendotp_Click"></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbsubotp" CausesValidation="False" UseSubmitBehavior="False" Style="margin-right: 10px;" CssClass="btn btn-primary pull-right forgot-pass-btn" TabIndex="7" Text="Submit" OnClick="lbsubotp_Click"></asp:LinkButton>

                                            </div>
                                        </div>
                                    </form>
                                </div>
                                <div class="modal-content" runat="server" id="p2">
                                    <div class="modal-header modal-header1">
                                        <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Change Password</h4>
                                    </div>
                                    <form class="form-horizontal changepassword" role="form">
                                        <div class="modal-body clearfix" style="padding: 0 20px;">
                                            <div class="form-group">
                                                <label>Old Password <span class="mandatory">*</span></label>
                                                <asp:TextBox runat="server" ID="txtoldpass" type="password" MaxLength="15" TabIndex="4" focus="true" class="form-control " placeholder="Enter old password">
                                                </asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Confirm Password <span class="mandatory">*</span></label>
                                                <asp:TextBox runat="server" ID="txtreppass" type="password" MaxLength="15" TabIndex="5" class="form-control" placeholder="Confirm New Password"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>New Password <span class="mandatory">*</span></label>
                                                <asp:TextBox runat="server" ID="txtnewpass" type="password" MaxLength="15" TabIndex="6" class="form-control" placeholder="Enter New Password"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="modal-footer">
                                                <asp:LinkButton runat="server" ID="btnsubmitpassword" CausesValidation="False" UseSubmitBehavior="False" CssClass="btn btn-primary pull-right forgot-pass-btn" TabIndex="7" Text="Update Password" OnClick="btnsubmitpassword_Click"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                                <div class="modal-content" runat="server" style="width: 800px; margin-left: -200px; z-index: 99999;" id="divregistration" visible="false">
                                    <div class="modal-header modal-header1">
                                        <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title"><b>Vendor Registration</b></h4>
                                        <hr />
                                        <p>Please provide all required details to register your business with us</p>
                                        <p>Fill all details and submit your request after submit you will get e-mail of your login credential request.</p>
                                        <div class="clearfix mt10"></div>
                                    </div>
                                    <div id="carouselExampleControls" class="carousel slide" style="padding-bottom: 37px;" data-ride="carousel" data-interval="false">
                                        <div class="carousel-inner">
                                            <div class="item active">
                                                <div>
                                                    <div class="form-group">
                                                        <div class="col-sm-5">
                                                            DPSU you want to apply 
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:CheckBoxList ID="chkdpsu" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-5">
                                                            Are you Registered with Pan 
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlpan" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlpan_SelectedIndexChanged">
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
                                                    <div class="form-group">
                                                        <div class="col-sm-5">
                                                            Name of firm/company 
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtbusinessname" runat="server" AutoPostBack="true" Style="text-transform: uppercase;" OnTextChanged="txtbusinessname_TextChanged" CssClass="form-control"></asp:TextBox>
                                                            <span runat="server" id="check"></span>
                                                            <asp:Label ID="lblbusinessname" runat="server" ForeColor="Green"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-5">
                                                            REGISTRATION CATEGORY <span class="mandatory">*</span>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlregiscategory" runat="server" CssClass="form-control"
                                                                required="required">
                                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Value="MANUFACTURER">MANUFACTURER</asp:ListItem>
                                                                <asp:ListItem Value="SERVICE SUB CONTRACTOR">SERVICE SUB CONTRACTOR</asp:ListItem>
                                                                <asp:ListItem Value="AUTHORISED AGENT">AUTHORISED AGENT</asp:ListItem>
                                                                <asp:ListItem Value="TRADER">TRADER</asp:ListItem>
                                                                <asp:ListItem Value="OEM">OEM</asp:ListItem>
                                                                <asp:ListItem Value="Stockist">Stockist</asp:ListItem>
                                                                <asp:ListItem Value="Contractor">Contractor</asp:ListItem>
                                                                <asp:ListItem Value="Distributor">Distributor</asp:ListItem>
                                                                <asp:ListItem Value="Consortium">Consortium</asp:ListItem>
                                                            </asp:DropDownList>
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
                                                </div>
                                            </div>
                                            <div class="item">
                                                <div>
                                                    <div class="form-group">
                                                        <div class="col-sm-5">
                                                            Nodal officer Name<span class="mandatory">*</span>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtnodalname" runat="server" MaxLength="100" required="" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-5">
                                                            Nodal officer email (treated as username) <span class="mandatory">*</span>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtnodelemail" runat="server" MaxLength="70" TextMode="Email" placeholder="ex: myemail@example.com" required="" AutoCompleteType="Email" CssClass="form-control"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-5">
                                                            Nodal office Contact No
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtMobileNodal" runat="server" MaxLength="10" required="" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-5">
                                                            Registered Office Address
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtstreetaddress" runat="server" MaxLength="300" required="" CssClass="form-control"></asp:TextBox>
                                                            <p>Street Address</p>
                                                            <div class="clearfix mt10"></div>
                                                            <asp:TextBox ID="txtstreetaddressline2" MaxLength="300" runat="server" CssClass="form-control"></asp:TextBox>
                                                            Street Address Line 2   
                                                                <div class="clearfix mt10"></div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtcity" runat="server" MaxLength="35" required="" CssClass="form-control"></asp:TextBox>
                                                                    <p>City</p>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtstateprovince" runat="server" MaxLength="35" required="" CssClass="form-control"></asp:TextBox>
                                                                    <p>State</p>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtpostalzipcode" runat="server" MaxLength="7" required="" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <p>Pin Code</p>
                                                                </div>
                                                                <%-- <div class="col-sm-6">
                                                                    <asp:DropDownList ID="ddlcountry" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Selected="True" Value="India">India</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <p>Country</p>
                                                                </div>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="item">
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="clearfix mt10"></div>
                                                            <p>1.Has the firm declared insolvent in Receivership ,Bankrupt or being wounded up.</p>
                                                            <asp:DropDownList ID="ddlwoundedup" runat="server" CssClass="form-control">
                                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="clearfix mt10"></div>
                                                            <p>2.Have firm affairs administered by a court or a judicial officer.</p>
                                                            <asp:DropDownList ID="ddljudicialofficer" runat="server" CssClass="form-control">
                                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="clearfix mt10"></div>
                                                            <p>3.Is business activities suspended.</p>
                                                            <asp:DropDownList ID="ddlbusinesssuspended" runat="server" CssClass="form-control">
                                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="clearfix mt10"></div>
                                                            <p>4.Is the firm subject of legal proceedings for any of the forging reasons.</p>
                                                            <asp:DropDownList ID="ddlforgingreasone" runat="server" CssClass="form-control">
                                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="clearfix mt10"></div>
                                                            <p>5.Has the firm been debarred from Govt. Contracts</p>
                                                            <asp:DropDownList ID="ddldebarredgovtcont" runat="server" CssClass="form-control">
                                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="clearfix mt10"></div>
                                                            <asp:CheckBoxList ID="chkbuisness" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="checkbox">
                                                                <asp:ListItem>I/We note that registration ,does not carry with it the right to business with DPSUs/OFB, I/We hereby declare that the information pertaining to my/our firm/Company including all enclosures is correct and true to the best of
                                                                     my/our knowledge and belief as on date</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <div class="clearfix mt10"></div>
                                                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary pull-right" Text="Submit" UseSubmitBehavior="false" OnClick="btnsubmit_Click" OnClientClick="if (!confirm('Are you sure you want to save this record?')) return false;" />
                                                            <asp:Button ID="btnclear" runat="server" CssClass="btn btn-warning pull-right" UseSubmitBehavior="false" Style="margin-right: 10px;" Text="Clear" OnClick="btnclear_Click" OnClientClick="if (!confirm('Are you sure you want clear textfield?')) return false;" />
                                                            <div class="clearfix"></div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <a class="carousel-control-prev btn btn-primary " href="#carouselExampleControls" role="button" data-slide="prev">Previous
                                        </a>
                                        <a class="carousel-control-next btn btn-primary" href="#carouselExampleControls" role="button" data-slide="next">Next
                                        </a>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="overlay-progress" style="z-index:99999;">
                    <div class="custom-progress-bar blue stripes">
                        <span></span>
                        <p>Processing</p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="modal fade" id="Div1" role="dialog" data-keyboard="false" data-backdrop="static">
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
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/jquery.bxslider.min.js"></script>
      <script src="assets/js/custom.js"></script>
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#modelfail').modal('show');
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
