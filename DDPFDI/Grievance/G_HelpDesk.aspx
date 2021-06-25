<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G_HelpDesk.aspx.cs" Inherits="Grievance_G_HelpDesk" %>

<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HelpDesk</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta charset="utf-8" />
    <link rel="icon" href="~/assets/images/icon.png" />
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/bootstrap.min.css" />
    <link href="../assets/fontawesome-free-5.15.2/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/style.css" />
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="~/Grievance/ProgressStatus.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="Iusefor" />
        <div class="container-fluid" id="top">
            <div class="row">
                <div class="col-md-3 d-flex">
                    <a href="ProductList" class="d-flex justify-content-center">
                        <img src="../ddp_logo.png" class="img-fluid" style="height: 70px" /></a>
                </div>
                <div class="col-md-6 d-flex align-items-center justify-content-center">
                    <h2 class="mb-0">OPPORTUNITIES FOR MAKE IN INDIA DEFENCE</h2>
                </div>
                <div class="col-md-3 d-flex justify-content-center align-items-center">
                    <a class="nav-link" style="color: blue;">
                        <h6>
                            <asp:Label ID="linkusername" runat="server" Visible="false" ToolTip="Login UserName"></asp:Label>
                        </h6>
                    </a>
                </div>
            </div>
        </div>
        <nav id="top3" class="navbar navbar-expand-lg px-md-3 bg-dark">
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav2" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <i class="fa fa-bars" aria-hidden="true" style="color: white;"></i>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav2">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item active">
                        <a class="nav-link" runat="server" style="color: white;" href="~/ProductList"><i class="fa fa-home" aria-hidden="true"></i></a>
                    </li>
                    <a href="About" class="nav-link" style="color: white;">About us </a>
                    <a href="~/Participate" runat="server" id="mhwparti" class="nav-link" style="color: white;">How to Participate</a>
                    <a href="~/Dashboard" id="lblmis" runat="server" style="color: white;" class="nav-link" visible="false" data-toggle="tooltip"
                        tooltip="DPSU's Dashboard Page Link (Click here to go back dashboard for add product)">CMS</a>
                    <div runat="server" id="reportdiv" visible="false">
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" style="color: white;" id="navbardrop1" data-toggle="dropdown">Reports&nbsp;<i class="fa fa-chevron-down" aria-hidden="true"></i>
                            </a>
                            <div class="dropdown-menu bg-dark" style="color: white;">
                                <a href="~/PReport2" id="PR" runat="server" style="color: white;" class="nav-link">Progress Report</a>
                                <a href="~/SuccessStoryupdate" id="lbSuccesstory" style="color: white;" runat="server" class="nav-link" visible="false">Success Story</a>
                                <a href="~/Summery" id="A11" runat="server" style="color: white;" class="nav-link">Summary Details</a>
                                <a href="~/Make2Report" id="A2" runat="server" style="color: white;" class="nav-link dropdown-item">Make-II Report</a>
                                <a href="~/CategoryWiseRep" id="A10" runat="server" style="color: white;" class="nav-link dropdown-item">Category Wise Report</a>
                                <a href="~/SONOIndig" id="A1" runat="server" style="color: white;" class="nav-link dropdown-item">Supply Order/No-Indiginized</a>
                                <a href="~/EOINOSOINDIG" id="A12" runat="server" style="color: white;" class="nav-link dropdown-item">EOI/No-SO/Indiginized</a>
                            </div>
                        </li>
                    </div>
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbardrop" style="color: white;" data-toggle="dropdown">Documentation&nbsp;<i class="fa fa-chevron-down" aria-hidden="true"></i>
                        </a>
                        <div class="dropdown-menu bg-dark">
                            <a href="../Policy&framwork.pdf" runat="server" target="_blank" style="color: white;" class="nav-link dropdown-item">Policy & Frame work</a>
                            <a href="../UserManualPublicDomain.pdf" runat="server" style="color: white;" target="_blank" class="nav-link dropdown-item">User Manual</a>
                            <a href="~/FAQs" runat="server"  style="color: white;" class="nav-link dropdown-item">FAQ</a>
                        </div>
                    </li>
                    <b><a href="https://www.makeinindiadefence.gov.in/" target="_blank" style="color: white;" class="nav-link" onclick="return confirm('You are being redirected to https://www.makeinindiadefence.gov.in');">Make In India Defence Portal </a></b>
                    <b><a href="~/Login" id="linklogin" runat="server" class="nav-link" style="color: white;" visible="false">DPSU Login</a></b>
                    <div runat="server" id="Div13">
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" id="navbardrop3" style="color: white;" data-toggle="dropdown">Contact Us&nbsp;<i class="fa fa-chevron-down" aria-hidden="true"></i>
                            </a>
                            <div class="dropdown-menu bg-dark">
                                <b><a href="~/FeedBack" runat="server" id="lnkfeedback" style="color: white;" class="nav-link">FeedBack</a></b>
                                <b><a href="~/GHelpDesk" runat="server" id="a8" style="color: white;" class="nav-link">HelpDesk</a></b>
                                <b><a href="~/Login?mhelpdesk=GRv9ZoStVr7DfCuZKHzgow==" style="color: white;" runat="server" id="ai" class="nav-link">HelpDesk Login</a></b>
                            </div>
                        </li>
                    </div>
                    <a href="~/SiteMap" id="A7" runat="server" class="nav-link" style="color: white;" data-toggle="tooltip"
                        tooltip="Sitemap">SiteMap</a>
                    <b>
                        <asp:LinkButton runat="server" ID="lbllogout" Visible="false" class="nav-link" style="color: white;" OnClick="lbllogout_Click">&nbsp;Log Out</asp:LinkButton></b>
                </ul>
            </div>
        </nav>
        <div class="container my-3">
            <p class="text-center text-capitalize">
                <h3>Help-Desk&nbsp;<asp:Label runat="server" ID="lblfrom"></asp:Label></h3>
            </p>
            <div class="p-2 rounded" style="background-color: #fdfdfd;" runat="server" id="divbutton">
                <asp:LinkButton runat="server" ID="btnTicketStatus" CssClass="btn btn-primary" ToolTip="Check raise ticket status" OnClick="btnTicketStatus_Click">Check ticket status</i></asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbRiseaTicket" CssClass="btn btn-success" ToolTip="Raise a ticket" OnClick="lbRiseaTicket_Click">Raise a ticket</i></asp:LinkButton>
            </div>
            <div class="p-2 rounded" style="background-color: #fdfdfd;" runat="server" id="divsum">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="col-sm-12">
                            <label>Portal</label><span class="mandatory"></span>
                            <asp:DropDownList runat="server" ID="ddltype" CssClass="form-control" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged" ToolTip="Select your type">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-12">
                            <label>Query for</label><span class="mandatory"></span>
                            <div class="clearfix"></div>
                            <asp:RadioButtonList runat="server" ID="rbselectsub" RepeatColumns="1" Enabled="false" RepeatDirection="Horizontal" TabIndex="2" AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="rbselectsub_SelectedIndexChanged">
                                <asp:ListItem style="margin-left: 10px;" Value="FeedBack">&nbsp;FeedBack/Suggestion</asp:ListItem>
                                <asp:ListItem style="margin-left: 10px;" Value="Issue">&nbsp;Issue/Grievance/Compliance</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-12">
                                    <label>Subject</label><span class="mandatory"></span>
                                    <asp:DropDownList runat="server" ID="txtsubject" CssClass="form-control" required="" AutoPostBack="true"
                                        OnSelectedIndexChanged="txtsubject_SelectedIndexChanged" Enabled="false" ToolTip="Subject releted your issue" TabIndex="3">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-12">
                                    <label>Sub-Subject</label><span class="mandatory"></span>
                                    <asp:DropDownList runat="server" ID="ddlSubSubject" CssClass="form-control" required="" Enabled="false" ToolTip="Sub-Subject releted your issue"
                                        TabIndex="4">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    
                    
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                            <label>Issue/FeedBack</label><span class="mandatory"></span>
                            <asp:TextBox runat="server" ID="txtIssueorFeedback" required="" TextMode="MultiLine" TabIndex="5"
                                Enabled="false" CssClass="form-control" Placeholder="Please describe your all Issue/Feedback (We are happy to hear you. Max 500 words)"
                                Width="100%" Height="121px" MaxLength="500"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label>Upload Files (If you have any document Please upload)</label>
                            <asp:FileUpload runat="server" ID="fuupload" TabIndex="8" Enabled="false" CssClass="form-control custom-file" />
                            <p>File max lenth allowed (1MB).Only pdf or word allowed</p>
                            <asp:HiddenField runat="server" ID="hfimage" />
                        </div>
                        </div>
                    </div>

                    

                    <div class="col-sm-6">
                        <div class="col-sm-12">
                            <label>Name</label><span class="mandatory"></span>
                            <asp:TextBox runat="server" ID="txtname" CssClass="form-control" Enabled="false" required="" ToolTip="Name" TabIndex="9" Placeholder="Name"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <label>Email</label><span class="mandatory"></span>
                            <asp:TextBox runat="server" ID="txtemail" CssClass="form-control" required="" TextMode="Email" Enabled="false" ToolTip="Email" TabIndex="10" Placeholder="Email"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <label>Mobile No</label><span class="mandatory"></span>
                            <asp:TextBox runat="server" ID="txtmobile" CssClass="form-control" required="" TextMode="Phone" ToolTip="Mobile-No" Enabled="false" TabIndex="11" Placeholder="Mobile-No"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <label for="psw" class=" tetLable">
                                Captcha
                            </label>
                            <div class="row">
                                <div class="col-sm-8">
                                    <asp:TextBox runat="server" ID="txtCaptcha" TabIndex="12" Enabled="false" ToolTip="enter captcha (case sensitive)"
                                        class="form-control" autocomplete="off" placeholder="Captcha" required=""></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-9">
                                            <asp:Image ID="imgCaptcha" runat="server" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:LinkButton ID="btnCaptchaNew" runat="server" TabIndex="13" Enabled="false" class="" CausesValidation="false" OnClick="btnCaptchaNew_Click"><i class="fas fa-sync-alt fa-lg"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6">
                        <div class="col-sm-12">
                            <label>State</label><span class="mandatory"></span>
                            <asp:DropDownList runat="server" ID="txtstate" CssClass="form-control" required=""
                                ToolTip="State (if your state not display please enter)" TabIndex="6" Enabled="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-12">
                            <label>Address</label><span class="mandatory"></span>
                            <asp:TextBox runat="server" ID="txtaddress" CssClass="form-control" required="" Enabled="false"
                                Height="121px" TextMode="MultiLine" ToolTip="Address" Placeholder="Address" TabIndex="7"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-9">
                    </div>
                    <div class="col-sm-3">
                        <asp:LinkButton runat="server" ID="btnSubmit" TabIndex="14" Enabled="false" CssClass="btn btn-primary mr-2 float-right  text-center" OnClick="btnSubmit_Click"><i class="fa fa-info"></i>&nbsp;Submit</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="btnBack" TabIndex="15" CssClass="btn btn-primary float-right mr-1  text-center" OnClick="btnBack_Click"><i class="fa fa-recycle"></i>&nbsp;Reset</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="p-2 rounded" style="background-color: #fdfdfd; min-height: 335px;" runat="server" id="divotp">
                <asp:HiddenField runat="server" ID="hfotp" />
                <p class="text-justify">
                    <div class="row">
                        <div class="col-sm-9">
                            <asp:TextBox runat="server" ID="txtotp" placeholder="OTP (6 Digit)" TabIndex="1" CssClass="form-control"></asp:TextBox>
                            <span>Please enter otp for ticket&nbsp;<asp:Label runat="server" ID="lblrefno"></asp:Label>
                                &nbsp;received on your given email id.</span>
                        </div>
                        <div class="col-sm-3">
                            <asp:LinkButton ID="lbsubmit" runat="server" CssClass="btn btn-primary btn btn-block btn-shadow" TabIndex="2" Text="Submit"
                                ToolTip="Thank you for showing intrest in these product mail will send to admin and also you will recieved a copy."
                                OnClick="lbsubmit_Click"></asp:LinkButton>
                            <asp:LinkButton runat="server" Text="Resend OTP" ID="lbresendotp" CssClass="mr-0 fa-pull-right" TabIndex="3" OnClick="lbresendotp_Click"></asp:LinkButton>
                        </div>

                        <div class="clearfix mt-1"></div>
                    </div>
                </p>
            </div>
            <div class="p-2 rounded" style="background-color: #fdfdfd; min-height: 335px;" runat="server" id="divstatus">
                <div class="row">
                    <div class="col-sm-9">
                        <label title="Reference no you will get on your email when you rise your issue">Ticket No.</label>
                        <asp:TextBox runat="server" ID="txtrefeno" placeholder="Your Ticket No." TabIndex="1" CssClass="form-control"></asp:TextBox>
                        <div class="clearfix mt-1">
                        </div>
                        <span>Please enter ticket no received on your given email id.</span>
                    </div>
                    <div class="col-sm-3">
                        <asp:LinkButton ID="lbstatus" runat="server" Style="margin-top: 30px;" CssClass="btn btn-primary btn-block btn-shadow pull-right" TabIndex="2" Text="Submit"
                            ToolTip="Wait till we retrive your ticket" OnClick="lbstatus_Click"></asp:LinkButton>
                    </div>
                    <div class="clearfix" style="margin-top: 10px;">
                        <hr />
                    </div>

                    <div class="col-sm-12">
                        <asp:DataList runat="server" ID="dlissue" RepeatColumns="1" RepeatLayout="Flow" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <div class="table-bordered">
                                    <div class="bg-dark text-white" style="padding: 5px; 5px; 5px; 5px;">
                                        <b><span style="float: left; margin-left: 5px;">&nbsp;Ticket No :&nbsp;<b>
                                            <asp:Label runat="server" ID="ticketno" Text='<%#Eval("IssueRefno") %>'></asp:Label></b></span></b>
                                        <b><span style="text-align: center !important;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ticket Issue Date :&nbsp;<b><%#Eval("RiseDate","{0:dd-MMM-yyyy}") %>&nbsp;Time :&nbsp;<%#Eval("RiseTime") %></b></span></b>
                                        <b><span style="float: right; margin-right: 5px;">Ticket Status :&nbsp;<b><%#Eval("IsOpen") %></b></span></b>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-sm-12 row">
                                        <div class="col-sm-6" style="border-right: 1px solid">
                                            <div class="modal-title bg-info text-center" style="padding: 5px; 5px; 5px; 5px;">
                                                <h5>Your Detail</h5>
                                            </div>
                                            <hr />
                                            <p>Ticket Rise For&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("HFrom") %></b></p>
                                            <p>Purpose&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("QueryFor") %></b></p>
                                            <p>Name&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("Name") %></b></p>
                                            <p>Email&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("Email") %></b></p>
                                            <p>MobileNo&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("MobileNo") %></b></p>
                                            <p>State&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("State") %></b></p>
                                            <p>Address&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("Address") %></b></p>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="modal-title bg-info text-center" style="padding: 5px; 5px; 5px; 5px;">
                                                <h5>Ticket Generate for Details</h5>
                                            </div>
                                            <hr />
                                            <p>Subject&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("Subject") %></b></p>
                                            <p>Sub-Subject&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("SubSubjectName") %></b></p>
                                            <p>Issue&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("Issue") %></b></p>
                                            <asp:Panel runat="server" ID="closedetail">
                                                <%--<p>Close-Status&nbsp;-&nbsp;<b class="float-lg-right"><asp:Label runat="server" ID="closestatus" Text='<%#Eval("IsOpen") %>'></asp:Label></b></p>--%>
                                                <p>Close-By&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("IsCloseBy") %></b></p>
                                                <p>Close Date&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("IsCloseDate","{0:dd-MMM-yyyy}") %></b></p>
                                                <%-- <p runat="server" visible="false">Close Level&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("IsCloseLevel") %></b></p>--%>
                                                <p>Close Reasone&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("CloseReasone") %></b></p>
                                            </asp:Panel>
                                        </div>
                                        <div class="clearfix mt-1"></div>
                                        <div class="col-sm-12"><a href='<%#Eval("Files") %>' target="_blank" class="fa fa-download fa-pull-right mr-1"></a></div>
                                        <div class="clearfix mt-1"></div>
                                    </div>
                                    <div class="clearfix mt-1"></div>

                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <br />
                        <div class="card card-timeline px-2 border-none" runat="server" visible="false" id="divtrack">
                            <ul class="bs4-order-tracking">
                                <li runat="server" id="step1">
                                    <div><i class="fas fa-ticket-alt"></i></div>
                                    Tickets raises</li>

                                <li runat="server" id="step2">
                                    <div><i class="fas fa-laptop-house"></i></div>
                                    Ticket assigned</li>

                                <li runat="server" id="step3">
                                    <div><i class="fas fa-spinner"></i></div>
                                    Ticket in progress</li>

                                <li runat="server" id="step4">
                                    <div><i class="fas fa-check-double"></i></div>
                                    Ticket resolved</li>
                            </ul>
                        </div>
                        <div class="clearfix mt-1"></div>
                    </div>
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
        <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
        <script src="User/Uassets/js/bootstrap.bundle.min.js"></script>
        <script src="../assets/fontawesome-free-5.15.2/js/all.min.js"></script>
        <script src="User/Uassets/js/jquery-ui.min.js"></script>

    </form>
</body>
</html>
