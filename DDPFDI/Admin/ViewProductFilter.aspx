<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProductFilter.aspx.cs" Inherits="Admin_ViewProductFilter" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <title>View Filter Items</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <link rel="shortcut icon" href="assets/images/favicon.ico">
    <link href="~/assets/css/bootstrap.css" rel="stylesheet">
    <link href="~/assets/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/select2.min.css" rel="stylesheet">
    <link href="~/assets/css/responsive.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/assets/css/aaryan.css" rel="stylesheet" />
    <link href="~/assets/css/jquery.fancybox.min.css" rel="stylesheet" />
    <link href="~/assets/css/multiselect.css" rel="stylesheet" />
    <script src="assets/js/multiselect.min.js"></script>
    <link href="~/assets/css/custom.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/summernote-bs4.css" rel="stylesheet" />
    <link href="~/assets/css/select2.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500,700" rel="stylesheet">
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 300px;
            border: 3px solid #d95459;
            border-radius: 12px;
            padding: 0;
            z-index: 999999 !important;
        }

            .modalPopup .header {
                background-color: #d95459;
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
                padding: 10px;
                min-height: 50px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }
    </style>
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
    <script type="text/javascript">
        function datatable() {
            $('#ContentPlaceHolder1_gvproduct').DataTable({
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup4() {
            $('#ProductCompany').modal('show');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div class="site-holder">
            <nav class="navbar" role="navigation">
                <div class="navbar-header">
                    <a class="navbar-brand" href="javascript:void(0)"><i class="fa fa-list btn-nav-toggle-responsive text-white"></i>
                        <span class="main-logo" title="Department of Defense Product">MOD</span>
                    </a>
                </div>
                <div class="collapse navbar-collapse">
                    <ul class="nav navbar-nav" style="margin: 10px 0px 0px 260px;">
                        <li class="nav-toggle"></li>
                    </ul>
                    <ul class="nav navbar-nav user-menu navbar-right ">
                        <li><a href="javascript:void(0)" class="user dropdown-toggle comp_fact_unit" data-toggle="dropdown">
                            <span>
                                <asp:Label ID="lblusername" runat="server"></asp:Label>
                                &nbsp;&nbsp;<i class="fa fa-sort-down"></i></span> &nbsp;</a>
                            <ul class="dropdown-menu user-login-dropdown">
                                <li><a href='<%=ResolveUrl("~/ChangePassword") %>'><i class="fa fa-key" aria-hidden="true"></i>Change Password</a></li>
                                <li>
                                    <asp:LinkButton ID="lbllogout" runat="server" class="" OnClick="lbllogout_Click"><i class="fa fa-lock" aria-hidden="true"></i>Logout</asp:LinkButton></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </nav>
            <b>Filters
                                                <asp:LinkButton ID="lbclearfilter" runat="server" Text="Clear Filter" CssClass="pull-right fa fa-cross" Style="margin-right: 10px; color: red;" OnClick="lbclearfilter_Click"></asp:LinkButton></b>
            <div class="clearfix mt10"></div>
            <div class="cat-common-box">
                <!-- Basic dropdown -->
                <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                    aria-haspopup="true" aria-expanded="false">
                    Indigenized  <i class="fa fa-caret-down" aria-hidden="true"></i>
                </button>
                <div class="dropdown-menu">
                    <a class="dropdown-item" href="#">
                        <div class="custom-control custom-checkbox">
                            <asp:RadioButtonList ID="rbisindezinized" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbisindezinized_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </a>
                </div>
            </div>
            <div class="cat-common-box">
                <!-- Basic dropdown -->
                <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                    aria-haspopup="true" aria-expanded="false">
                    Make II  <i class="fa fa-caret-down" aria-hidden="true"></i>
                </button>
                <div class="dropdown-menu">
                    <a class="dropdown-item" href="#">
                        <div class="custom-control custom-checkbox">
                            <asp:RadioButtonList ID="rbismake2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbismake2_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                <asp:ListItem Value="25">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </a>
                </div>
            </div>
            <div class="cat-common-box">
                <!-- Basic dropdown -->
                <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                    aria-haspopup="true" aria-expanded="false">
                    Contact<i class="fa fa-caret-down" aria-hidden="true"></i>
                </button>
                <div class="dropdown-menu">
                    <a class="dropdown-item" href="#">
                        <div class="custom-control custom-checkbox">
                            <asp:RadioButtonList ID="rbiscontact" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbiscontact_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                <asp:ListItem Value="Y">Available</asp:ListItem>
                                <asp:ListItem Value="N">Not Available</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </a>
                </div>
            </div>
            <div class="cat-common-box">
                <!-- Basic dropdown -->
                <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                    aria-haspopup="true" aria-expanded="false">
                    Tender<i class="fa fa-caret-down" aria-hidden="true"></i>
                </button>
                <div class="dropdown-menu">
                    <a class="dropdown-item" href="#">
                        <div class="custom-control custom-checkbox">
                            <asp:RadioButtonList ID="rbistender" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbistender_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                <asp:ListItem Value="Y">Live</asp:ListItem>
                                <asp:ListItem Value="N">Not Live</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </a>
                </div>
            </div>
            <div class="cat-filter">
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        Company <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>
                    <div class="dropdown-menu">
                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="chkcomp" runat="server" RepeatColumns="1" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="chkcomp_SelectedIndexChanged" RepeatLayout="Table"></asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        Division <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>
                    <div class="dropdown-menu">
                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="chkdivision" runat="server" RepeatColumns="1" AutoPostBack="true" OnSelectedIndexChanged="chkdivision_SelectedIndexChanged" RepeatDirection="Vertical" RepeatLayout="Table"></asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        Unit <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>
                    <div class="dropdown-menu">
                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="chkunit" runat="server" RepeatColumns="1" AutoPostBack="true" OnSelectedIndexChanged="chkunit_SelectedIndexChanged" RepeatDirection="Vertical" RepeatLayout="Table"></asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        End User <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>
                    <div class="dropdown-menu">
                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:CheckBoxList ID="chkenduser" runat="server" RepeatColumns="1" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="chkenduser_SelectedIndexChanged" RepeatLayout="Table">
                                </asp:CheckBoxList>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        DEFENCE PLATFORM <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>

                    <div class="dropdown-menu">

                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="chkdefenceplatform" runat="server" RepeatColumns="1" AutoPostBack="true" OnSelectedIndexChanged="chkdefenceplatform_SelectedIndexChanged" RepeatDirection="Vertical" RepeatLayout="Table">
                                </asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        NAME OF DEFENCE PLATFORM <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>
                    <div class="dropdown-menu">
                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="chknameofdefenceplatform" runat="server" RepeatColumns="1" AutoPostBack="true" OnSelectedIndexChanged="chknameofdefenceplatform_SelectedIndexChanged" RepeatDirection="Vertical" RepeatLayout="Table">
                                </asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        PRODUCT (INDUSTRY DOMAIN)<i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>
                    <div class="dropdown-menu">

                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="chkprodindustrydomain" runat="server" RepeatColumns="1" AutoPostBack="true" OnSelectedIndexChanged="chkprodindustrydomain_SelectedIndexChanged" RepeatDirection="Vertical" RepeatLayout="Table">
                                </asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        PRODUCT (INDUSTRY SUB DOMAIN) <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>

                    <div class="dropdown-menu">

                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="chkprodindustrysubdomain" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkprodindustrysubdomain_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                </asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        NSC Code <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>
                    <div class="dropdown-menu">

                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="rbnsccode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbnsccode_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                </asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        HSN Code (4-8 digit) <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>

                    <div class="dropdown-menu">

                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="rbhsncode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbhsncode_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                </asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>

                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        OEM Country <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>

                    <div class="dropdown-menu">

                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="rboemcountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rboemcountry_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                </asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="cat-common-box">
                    <!-- Basic dropdown -->
                    <button class="btn btn-primary dropdown-toggle mr-4" type="button" data-toggle="dropdown"
                        aria-haspopup="true" aria-expanded="false">
                        Search keywords <i class="fa fa-caret-down" aria-hidden="true"></i>
                    </button>
                    <div class="dropdown-menu">

                        <a class="dropdown-item" href="#">
                            <div class="custom-control custom-checkbox">
                                <asp:RadioButtonList ID="rbsearchkeywords" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbsearchkeywords_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Table">
                                </asp:RadioButtonList>
                            </div>
                        </a>
                    </div>
                </div>

            </div>
            <div class="clearfix mt10"></div>
            <div class="box-holder">
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="content oem-content">
                            <div class="sideBg">

                                <div class="row">
                                    <div class="col-md-12 padding_0">
                                        <div id="divHeadPage" runat="server"></div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="clearfix"></div>
                                        <div style="margin-top: 5px;">
                                            <asp:LinkButton ID="lblback" runat="server" class="fa fa-arrow-circle-left pull-right" OnClick="lblback_Click">Back</asp:LinkButton>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                <div class="addfdi">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="clearfix"></div>
                                            <div id="divTotalNumber" class="text-center" style="font-size: 16px; margin-top: 10px;" runat="server">
                                                <asp:Label ID="lbltotal" runat="server" CssClass="lable label-info " Text=""></asp:Label>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <asp:HiddenField runat="server" ID="hfmtype" />
                                            <asp:HiddenField runat="server" ID="hfmref" />
                                            <div class="clearfix"></div>
                                            <div class="table-responsive" id="divProductGrid" runat="server" visible="False" style="overflow: scroll;">
                                                <div class="row">
                                                    <div class="col-md-12">

                                                        <asp:LinkButton ID="lbldownloadexcel" runat="server" ToolTip="Download Excel" Style="color: red;" Class=" fa fa-file-excel" Text="Download Excel" OnClick="lbldownloadexcel_Click"></asp:LinkButton>
                                                        <div class="clearfix mt10"></div>
                                                        <asp:GridView ID="gvproduct" runat="server" Width="100%" Class="table table-hover manage-user"
                                                            Style="overflow: scroll;"
                                                            AutoGenerateColumns="false" OnRowCommand="gvproduct_RowCommand" OnRowCreated="gvproduct_RowCreated" OnRowDataBound="gvproduct_RowDataBound">
                                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No.">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("ProductRefNo") %>'></asp:LinkButton>
                                                                        <asp:HiddenField ID="hfroleProd" runat="server" Value='<%#Eval("Role") %>' />
                                                                        <asp:HiddenField ID="hfcomprefno" runat="server" Value='<%#Eval("CompanyRefNo") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PDF">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink runat="server" ID="lbpdffile" Target="_blank" NavigateUrl='<%#Eval("ItemDescriptionPDFFile","~/Upload/{0}") %>' CssClass="fa fa-file-pdf"></asp:HyperLink>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Image">
                                                                    <ItemTemplate>
                                                                        <a data-fancybox="Prodgridviewgellry" href='<%#Eval("TopImages") %>'>
                                                                            <asp:Image ID="imgtop" runat="server" Height="80px" Width="80px" src='<%#Eval("TopImages") %>' />
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="-" />

                                                                <asp:BoundField DataField="FactoryName" Visible="false" HeaderText="Division" NullDisplayText="-" />
                                                                <asp:BoundField DataField="UnitName" Visible="false" HeaderText="Unit" NullDisplayText="-" />
                                                                <asp:TemplateField HeaderText="Item Reference No." Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("ProductRefNo") %>' NullDisplayText="#" SortExpression="ProductRefNo"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ProductDescription" HeaderText="Item Description" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="ProductDescription" />
                                                                <asp:BoundField DataField="ProdIndustryDoamin" HeaderText="Product (Industry Domain)" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="ProdIndustryDoamin" />
                                                                <asp:BoundField DataField="NSNGroup" HeaderText="NSN Group" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="NameDefencePlatform" />
                                                                <asp:BoundField DataField="NSNGroupClass" HeaderText="NSN GROUP CLASS" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="NameDefencePlatform" />

                                                                <asp:BoundField DataField="DefencePlatform" HeaderText="Defence Platform" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="DefencePlatform" />
                                                                <asp:TemplateField HeaderText="OEM PartNumber" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblcompanyrole" Text='<%#Eval("OEMPartNumber") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DPSUPartNumber">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("DPSUPartNumber") %>' NullDisplayText="#" SortExpression="DPSUPartNumber"></asp:Label>
                                                                        <asp:HiddenField ID="hfisaaproved" runat="server" Value='<%#Eval("IsApproved") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="IsIndeginized" HeaderText="Item Indeginized" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="IsIndeginized" />
                                                                <asp:TemplateField HeaderText="Last Updated">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblLastUpdated" Text='<%#Eval("LastUpdated") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="clearfix mt10"></div>
                                                    <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                                                    <div class="row" runat="server" id="divpageindex" visible="false">
                                                        <div class="col-sm-9">
                                                            <div class="col-sm-4 row">
                                                                <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-info  btn-sm"
                                                                    OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-4" style="display: flex">
                                                                <asp:TextBox runat="server" ID="txtpageno" CssClass="form-control btn-defualt text-center red" AutoCompleteType="Search" Placeholder="Please enter no of page"></asp:TextBox>
                                                                <asp:LinkButton ID="btngoto" runat="server" CssClass="btn btn-primary" OnClick="btngoto_Click">Go to</asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-4 row">
                                                                <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn btn-info btn-sm pull-right" Style="margin-right: 3px;"
                                                                    OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <div class="pull-right">
                                                                <asp:Label ID="lblpaging" runat="server" class="btn btn-primary text-center" Text=""></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-----------------------------------------end code for page indexing----------------------------------------------------->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
                                </div>
                                <div class="modal fade" id="ProductCompany" role="dialog">
                                    <div class="modal-dialog" style="width: 1200px; z-index: 9999999999;">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-header modal-header1">
                                                <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title">Item Detail</h4>
                                            </div>
                                            <div class="form-horizontal changepassword">
                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="faq-secion product-view">
                                                                <div class="accordion" id="accordion">
                                                                    <div class="card">
                                                                        <div class="card-header">
                                                                            <h2 data-toggle="collapse" data-parent="#accordion" data-target="#faq1" aria-expanded="false" aria-controls="faq1">Description 
                                                            <i class="fa fa-minus pull-right"></i>
                                                                            </h2>
                                                                        </div>
                                                                        <div id="faq1" class="collapse in" aria-labelledby="headingOne">
                                                                            <div class="card-body">
                                                                                <ul>
                                                                                    <li>
                                                                                        <div class="row two-col">
                                                                                            <div class="col-md-6">
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td>Refrence No:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblcomprefno" runat="server" Text=""></asp:Label>
                                                                                                        </td>

                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Company:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblcompname" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>

                                                                                                    <tr>
                                                                                                        <td>Division/Palnt:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lbldiviname" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Unit:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblunitnamepro" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Item Description:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblproductdescription" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Item Refrence No:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblprodrefno" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>OEM Part Number:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lbloempartnumber" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>OEM Name:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lbloemname" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>OEM Country:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lbloemcountry" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>DPSU Part Number:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lbldpsupartno" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>HSN Code (8-digit)</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblhsncode8digit" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>End User:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblenduser" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>NSN GROUP:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblnsngroup" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>NSN GROUP CLASS:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblnsngroupclass" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>CLASS ITEM:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>

                                                                                                </table>
                                                                                            </div>
                                                                                            <div class="col-md-6">
                                                                                                <table>

                                                                                                    <tr>
                                                                                                        <td>NSC Code (4 digit):</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblnsccode" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>NIIN Code (9-digit):</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblniincode" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>

                                                                                                    <tr>
                                                                                                        <td>DEFENCE PLATFORM:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lbldefenceplatform" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>NAME OF DEFENCE PLATFORM:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblnameofdefenceplatform" runat="server" Text=""></asp:Label></td>

                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>PRODUCT (INDUSTRY DOMAIN):</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="prodIndustryDomain" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>PRODUCT (INDUSTRY SUB DOMAIN):</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="ProdIndusSubDomain" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>PRODUCT (INDUSTRY 2nd SUB DOMAIN):</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="ProdIndus2SubDomain" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Search Keywords:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblsearchkeyword" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Item Already Indeginized</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblprodalredyindeginized" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <div runat="server" id="tableIsIndiginized">
                                                                                                        <tr>
                                                                                                            <td>Manufacturer Name</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblmanufacturename" runat="server" Text=""></asp:Label></td>

                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>Address</td>

                                                                                                            <td>
                                                                                                                <asp:Label ID="lblmanaddress" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>Year of Indiginization</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblyearofindiginization" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>

                                                                                                    </div>
                                                                                                    <tr>
                                                                                                        <td>Is Item Imported</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Year of Import</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblyearofimport" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Remarks</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblremarksproductimported" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </div>
                                                                                        </div>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card">
                                                                        <div class="card-header">
                                                                            <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq2" aria-expanded="false" aria-controls="faq2">Item Specification
                                                            <i class="fa fa-plus pull-right"></i>
                                                                            </h2>
                                                                        </div>
                                                                        <div id="faq2" class="collapse">
                                                                            <div class="card-body">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>Document related to item</td>
                                                                                                <div runat="server" id="itemdocument">
                                                                                                    <td>
                                                                                                        <a href="#" target="_blank" runat="server" id="a_downitem" class="fa fa-download"></a>
                                                                                                        <span data-toggle="tooltip" class="fa fa-question" title="Click on icon for downloaf"></span>
                                                                                                    </td>
                                                                                                </div>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Item Image</td>
                                                                                                <td>
                                                                                                    <asp:DataList ID="dlimage" runat="server" RepeatColumns="4" Visible="true" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                                        <ItemTemplate>
                                                                                                            <div class="col-sm-3">
                                                                                                                <a data-fancybox="Prodgridviewgellry" href='<%#Eval("[ImageName]") %>'>
                                                                                                                    <asp:Image ID="imgprodimage" runat="server" CssClass="img-responsive img-container" Height="90px" Width="110px" src='<%#Eval("[ImageName]") %>' />
                                                                                                                </a>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:DataList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Features & Details</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblfeaturesanddetail" runat="server" Text=""></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Item Specification</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblitemspecification" runat="server" Text=""></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Additional Information</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbladditionalinfo" runat="server" Text=""></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Item Information
                                                                                                </td>
                                                                                                <td>&nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    <asp:GridView ID="gvProdInfo" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="NameOfSpec" HeaderText="Name of Specification" />
                                                                                                            <asp:BoundField DataField="Value" HeaderText="Value " />
                                                                                                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                                        </Columns>

                                                                                                    </asp:GridView>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div id="Div1" class="card" runat="server">
                                                                        <div class="card-header">
                                                                            <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq3" aria-expanded="false" aria-controls="faq3">Estimated Quantity
                                                            <i class="fa fa-plus pull-right"></i>
                                                                            </h2>
                                                                        </div>
                                                                        <div id="faq3" class="collapse">
                                                                            <div class="card-body">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <table>

                                                                                            <tr>
                                                                                                <td>PROCURMENT CATEGORY</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblpurposeofprocurement" runat="server" Text=""></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>PROCURMENT CATEGORY REMARK</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblprocremarks" runat="server" Text=""></asp:Label></td>
                                                                                            </tr>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td>Type:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbltendersubmission" runat="server" Text=""></asp:Label></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Tender Status:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbltenderstatus" runat="server" Text=""></asp:Label></td>
                                                                                                </tr>
                                                                                                <table runat="server" id="tenderstatus">
                                                                                                    <tr>
                                                                                                        <td>Tender Date:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lbltenderdate" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Tender URL:</td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lbltenderurl" runat="server" Text=""></asp:Label></td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </table>
                                                                                            <hr />
                                                                                            <tr>
                                                                                                <td>Estimate Quantity or Price</td>
                                                                                                <td>&nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="FYear" HeaderText="Year" />
                                                                                                            <asp:BoundField DataField="EstimatedQty" HeaderText="Estimated Quantity" />
                                                                                                            <asp:BoundField DataField="Unit" HeaderText="Measuring Unit" />
                                                                                                            <%--<asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price / LPP" />--%>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="card">
                                                                        <div class="card-header">
                                                                            <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq10" aria-expanded="false" aria-controls="faq10">Contact
                                                            <i class="fa fa-plus pull-right"></i>
                                                                            </h2>
                                                                        </div>

                                                                        <div id="faq10" class="collapse">
                                                                            <div class="card-body">
                                                                                <div class="row">
                                                                                    <div class="col-md-6">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>Nodel Detail -1</td>
                                                                                                <tr>
                                                                                                    <td>Employee Code:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblempcode" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Employee Name:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblempname" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Designation:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>E-Mail ID:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblemailidpro" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Mobile Number:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblmobilenumber" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Phone Number:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblphonenumber" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Fax:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblfaxpro" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tr>

                                                                                        </table>
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>Nodel Detail -2</td>
                                                                                                <tr>
                                                                                                    <td>Employee Code:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblempcode2" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Employee Name:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblempname2" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Designation:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbldesignation2" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>E-Mail ID:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblemailid2" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Mobile Number:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblmobilenumber2" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Phone Number:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblphonenumber2" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Fax:</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblfax2" runat="server" Text=""></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvproduct" EventName="RowCommand" />
                        <asp:PostBackTrigger ControlID="lnkbtnPgPrevious" />
                        <asp:PostBackTrigger ControlID="btngoto" />
                        <asp:PostBackTrigger ControlID="lnkbtnPgNext" />
                        <asp:PostBackTrigger ControlID="chkcomp" />
                        <asp:PostBackTrigger ControlID="chkdivision" />
                        <asp:PostBackTrigger ControlID="chkunit" />
                        <asp:PostBackTrigger ControlID="chkenduser" />
                        <asp:PostBackTrigger ControlID="chkdefenceplatform" />
                        <asp:PostBackTrigger ControlID="chknameofdefenceplatform" />
                        <asp:PostBackTrigger ControlID="chkprodindustrydomain" />
                        <asp:PostBackTrigger ControlID="chkprodindustrysubdomain" />
                        <asp:PostBackTrigger ControlID="lbclearfilter" />
                    </Triggers>
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
        <script type="text/javascript">
            function SessionExpireAlert(timeout) {
                var seconds = timeout / 1000;
                document.getElementsByName("secondsIdle").innerHTML = seconds;
                document.getElementsByName("seconds").innerHTML = seconds;
                setInterval(function () {
                    seconds--;
                    document.getElementById("seconds").innerHTML = seconds;
                    document.getElementById("secondsIdle").innerHTML = seconds;
                }, 1000);
                setTimeout(function () {
                    //Show Popup before 20 seconds of timeout.
                    $find("mpeTimeout").show();
                }, timeout - 20 * 1000);
                setTimeout(function () {
                    window.location = "Login";
                }, timeout);
            };
            function ResetSession() {
                window.location = window.location.href;
            }
        </script>
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
    <script src="assets/js/jquery-3.4.1.js"></script>
    <script src="assets/js/jquery-ui.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <link href="assets/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="assets/js/jquery.dataTables.js"></script>
    <script src="assets/js/select2.min.js"></script>
    <script src="assets/js/jquery.fancybox.min.js"></script>
    <script src="assets/js/multiselect.min.js"></script>
    <script src="assets/js/summernote-bs4.js"></script>
    <script src="assets/js/custom.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.summernote').summernote({
                height: 100,
                tabsize: 2
            });
        });
    </script>
    <!--------------------------Gallery----------------------->
    <script>
        $(function () {
            // Multiple images preview in browser
            var imagesPreview = function (input, placeToInsertImagePreview) {

                if (input.files) {
                    var filesAmount = input.files.length;

                    for (i = 0; i < filesAmount; i++) {
                        var reader = new FileReader();

                        reader.onload = function (event) {
                            $($.parseHTML('<img>')).attr('src', event.target.result).appendTo(placeToInsertImagePreview);
                        }

                        reader.readAsDataURL(input.files[i]);
                    }
                }

            };

            $('.uploadimage').on('change', function () {
                imagesPreview(this, 'div.gallery');
            });
        });
    </script>
    <!--------------------------Only number----------------------->
    <script type="text/javascript">
        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    alert("Only number allowed");
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
    <!--------------------------End Only Number----------------------->
</body>
</html>










