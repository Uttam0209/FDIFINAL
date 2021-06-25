﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="User_U_ProductList1" CodeFile="U_ProductList.aspx.cs" ViewStateEncryptionMode="Always" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product List</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta charset="utf-8" />
    <meta name="google-site-verification" content="LPR-H1MjCk1oRRTFC79124pkSLiyG35I_YJ0kuICRuI" />
    <link rel="icon" href="~/assets/images/icon.png" />
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/bootstrap.min.css" />
    <link href="../assets/fontawesome-free-5.15.2/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/style.css" />
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/jquery-ui.css" />
    <style>
        .filter_btn {
            background: #e5e6e5;
        }

            .filter_btn:focus {
                background: #e5e6e5 !important;
            }

        #navbarNav2 a {
            padding-left: 10px !important;
            padding-right: 10px !important;
            font-size: 16px
        }

        .indigenized {
            color: #ff0000a3;
            transform: rotate(-52deg);
            font-size: 53px;
            position: absolute;
            margin-bottom: 0;
            font-weight: 400;
            top: 136px;
            left: -40px
        }

        .btn {
            background-color: #6915cf;
            color: #fff;
            padding: 10px 0;
            overflow: hidden;
            position: relative;
            width: 100%;
            font-size: 16px;
            border: none;
            transition: background-color .2s ease-in-out
        }

            .btn:hover {
                color: #fff
            }

        #lbtotalcart {
            position: relative
        }

        #totalno {
            position: absolute;
            top: -.3125rem;
            right: -.3125rem;
            width: 1.25rem;
            height: 1.25rem;
            border-radius: 50%;
            background-color: #6915cf;
            color: #fff;
            font-size: .75rem;
            font-weight: 500;
            text-align: center;
            line-height: 1.25rem
        }

        #lbcart {
            margin-top: 10px;
            margin-left: 10px;
            color: #fff
        }

        #lbtotalcart .fa-cart-plus {
            margin-top: 15px
        }

        #top h2 {
            color: #6915cf
        }

        #menubar a {
            color: #f1faee !important;
            text-transform: uppercase;
            border-bottom: 2px solid #373f50
        }

            #menubar a:hover {
                border-bottom: 2px solid #f1faee;
                transition: .1s
            }

        #shop-sidebar {
            min-height: 1180px;
            box-shadow: 0 0 5px #6915cf !important
        }

        #dlproduct {
            padding-left: 15px !important
        }

            #dlproduct span > div {
                box-shadow: 0 0 2px #6915cf !important
            }

        #mycard {
            padding: 10px
        }

        #footer1 {
            background: #373f50;
            color: #f1faee !important;
            text-align: center
        }

            #footer1 a {
                color: #f1faee !important;
                text-decoration: none !important
            }

        #chktendor input {
            margin-right: 10px
        }

        #chktendor label {
            font-size: 13px
        }

        #rbindustryspecification_6 {
            margin-top: 5px
        }

        @media only screen and (max-width:1036px) {
            #navbarNav2 a {
                padding-left: 8px !important;
                padding-right: 8px !important;
                font-size: 14px
            }
        }

        @media only screen and (max-width:998px) {
            #navbarNav2 a {
                padding-left: 5px !important;
                padding-right: 5px !important;
                font-size: 12px
            }
        }

        @media screen and (max-width:1106px) {
            #top h2 {
                font-size: 24px
            }

            #topbar2 h4 {
                font-size: 20px
            }
        }

        @media screen and (max-width:868px) {
            #top h2 {
                font-size: 20px
            }

            #topbar2 h4 {
                font-size: 16px
            }
        }

        @media screen and (max-width:684px) {
            #dlproduct span {
                max-width: 45% !important
            }
        }

        @media screen and (max-width:440px) {
            #mycard .btn {
                font-size: 12px !important
            }

            #mycard .fa-cart-plus {
                font-size: 12px !important
            }
        }

        @media screen and (max-width:400px) {
            #dlproduct span {
                max-width: 100%
            }

            #mycard .btn {
                font-size: 15px !important
            }

            #mycard .fa-cart-plus {
                font-size: 15px !important
            }
        }

        #divcontentproduct .card {
            border-radius: 0 !important;
        }
    </style>
    <style>
        .tooltip {
            position: relative;
            display: inline-block;
            border-bottom: 1px dotted black;
        }

            .tooltip .tooltiptext {
                visibility: hidden;
                width: 120px;
                background-color: black;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px 0;
                /* Position the tooltip */
                position: absolute;
                z-index: 1;
            }

            .tooltip:hover .tooltiptext {
                visibility: visible;
            }
    </style>
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-178148370-1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());
        gtag('config', 'UA-178148370-1');
    </script>
    <!-- Google Analytics -->
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-XXXXX-Y', 'auto');
        ga('send', 'pageview');
    </script>
    <!-- End Google Analytics -->
</head>
<body oncut="return false;" oncopy="return false;" onpaste="return false;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sc" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="update" runat="server">
            <ContentTemplate>
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
                <div class="container-fluid bg-dark py-3" id="topbar2">
                    <div class="row d-flex">
                        <div class="col-sm-6 d-flex justify-content-sm-start justify-content-center">
                            <h4 class="text-white">Products imported by Defence PSUs/OFB/SHQ</h4>
                        </div>
                        <div class="col-sm-6 d-flex justify-content-end pr-4">
                            <asp:LinkButton ID="lbtotalcart" runat="server" class="navbar-tool-icon-box bg-secondary dropdown-toggle"
                                OnClick="lbtotalcart_Click">
                                <span class="navbar-tool-label" runat="server" id="totalno"></span><i class="navbar-tool-icon fa fa-cart-plus"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbcart" runat="server" OnClick="lbcart_Click"> Show Interest</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row d-flex justify-content-end" id="menubar">
                        <div class="col navbar navbar-expand-md px-md-3 bg-dark">
                            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav2" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                                <i class="fa fa-bars" aria-hidden="true" style="color: white;"></i>
                            </button>
                            <div class="collapse navbar-collapse" id="navbarNav2">
                                <ul class="navbar-nav ml-auto">
                                    <li class="nav-item active">
                                        <a class="nav-link" runat="server" href="~/ProductList"><i class="fa fa-home" aria-hidden="true"></i></a>
                                    </li>
                                    <a href="About" class="nav-link">About us </a>
                                    <a href="~/Participate" runat="server" id="mhwparti" class="nav-link">How to Participate</a>

                                    <a href="~/Dashboard" id="lblmis" runat="server" class="nav-link" visible="false" data-toggle="tooltip"
                                        tooltip="DPSU's Dashboard Page Link (Click here to go back dashboard for add product)">CMS</a>
                                    <div runat="server" id="reportdiv" visible="false">
                                        <li class="nav-item dropdown">
                                            <a class="nav-link dropdown-toggle" href="#" id="navbardrop1" data-toggle="dropdown">Reports&nbsp;<i class="fa fa-chevron-down" aria-hidden="true"></i>
                                            </a>
                                            <div class="dropdown-menu bg-dark">
                                                <a href="~/PReport2" id="PR" runat="server" class="nav-link">Progress Report</a>
                                                <a href="~/SuccessStoryupdate" id="lbSuccesstory" runat="server" class="nav-link" visible="false">Success Story</a>
                                                <a href="~/Summery" id="A11" runat="server" class="nav-link">Summary Details</a>
                                                <a href="~/Make2Report" id="A2" runat="server" class="nav-link dropdown-item">Make-II Report</a>
                                                <a href="~/CategoryWiseRep" id="A10" runat="server" class="nav-link dropdown-item">Category Wise Report</a>
                                                <a href="~/SONOIndig" id="A1" runat="server" class="nav-link dropdown-item">Supply Order/No-Indiginized</a>
                                                <a href="~/EOINOSOINDIG" id="A12" runat="server" class="nav-link dropdown-item">EOI/No-SO/Indiginized</a>
                                            </div>
                                        </li>
                                    </div>
                                    <li class="nav-item dropdown">
                                        <a class="nav-link dropdown-toggle" href="#" id="navbardrop" data-toggle="dropdown">Documentation&nbsp;<i class="fa fa-chevron-down" aria-hidden="true"></i>
                                        </a>
                                        <div class="dropdown-menu bg-dark">
                                            <a href="../Policy&framwork.pdf" runat="server" target="_blank" class="nav-link dropdown-item">Policy & Frame work</a>
                                            <a href="../UserManualPublicDomain.pdf" runat="server" target="_blank" class="nav-link dropdown-item">User Manual</a>
                                            <a href="~/FAQs" runat="server" class="nav-link dropdown-item">FAQ</a>
                                        </div>
                                    </li>
                                    <b><a href="https://www.makeinindiadefence.gov.in/" target="_blank" class="nav-link" onclick="return confirm('You are being redirected to https://www.makeinindiadefence.gov.in');">Make In India Defence Portal </a></b>
                                    <b><a href="~/Login" id="linklogin" runat="server" class="nav-link" visible="false">DPSU Login</a> </b>
                                    <div runat="server" id="Div13">
                                        <li class="nav-item dropdown">
                                            <a class="nav-link dropdown-toggle" href="#" id="navbardrop3" data-toggle="dropdown">Contact Us&nbsp;<i class="fa fa-chevron-down" aria-hidden="true"></i>
                                            </a>
                                            <div class="dropdown-menu bg-dark">
                                                <b><a href="~/FeedBack" runat="server" id="lnkfeedback" class="nav-link">FeedBack</a></b>
                                                 <b><a href="~/GHelpDesk" runat="server" id="a8" class="nav-link">HelpDesk</a></b>
                                                <b><a href="~/GOfficialLogin" runat="server" id="ai" class="nav-link">HelpDesk Login</a></b>
                                                <%--~/Login?mhelpdesk=GRv9ZoStVr7DfCuZKHzgow==--%>
                                            </div>
                                        </li>
                                    </div>
                                    <div runat="server" id="mhide" visible="false">
                                        <li class="nav-item dropdown">
                                            <a class="nav-link dropdown-toggle" href="#" id="navbardrop2" data-toggle="dropdown">All Test Links&nbsp;<i class="fa fa-chevron-down" aria-hidden="true"></i>
                                            </a>
                                            <div class="dropdown-menu bg-dark">
                                                <a href="~/PUpdate" id="A3" runat="server" class="nav-link" visible="false">Product Update</a>
                                                <a href="~/ItemStatus" id="A4" runat="server" class="nav-link dropdown-item" visible="false">Item Status</a>
                                                <a href="~/TestSimiler" id="A5" runat="server" class="nav-link dropdown-item" visible="false">Similar Product</a>
                                                <b><a href="~/SuccessStory2" id="A6" runat="server" class="nav-link" visible="false" style="display: none;">Success Story 2.0</a></b>
                                            </div>
                                        </li>
                                    </div>
                                    <a href="~/SiteMap" id="A7" runat="server" class="nav-link" data-toggle="tooltip"
                                        tooltip="Sitemap">SiteMap</a>
                                    <b>
                                        <asp:LinkButton runat="server" ID="lbllogout" Visible="false" class="nav-link" OnClick="lbllogout_Click">&nbsp;Log Out</asp:LinkButton></b>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="row d-flex justify-content-center">
                        <div class="col-8">
                            <div class="input-group-overlay d-none d-lg-flex">
                                <asp:DropDownList runat="server" ID="ddlfiltersearch" CssClass="form-control mr-1" Width="45%" Height="40px"></asp:DropDownList>
                                <asp:TextBox ID="txtsearch" runat="server" Style="max-height: 40px;"
                                    ToolTip="search tab with all criteria using words." onblur="SaveData('txtsearch')" CssClass="form-control mr-1 appended-form-control"
                                    Placeholder="Search (type min three character)"></asp:TextBox>
                                <asp:Button runat="server" ID="btnsearch" Style="padding: 3px 20px 3px 20px; width: 25%;" Height="40px"
                                    CssClass="btn btn-info btn-sm pull-right" Text="Search" OnClick="btnsearch_Click" />
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lnktooltip" runat="server" Text="How to Search" ForeColor="White" Width="32%" Height="40px" CssClass="ml-3 btn btn-info btn-sm pull-right" OnClick="lnktooltip_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- </div>--%>
                <div class="container my-3">
                    <div class="row">
                        <aside class="col-lg-4">
                            <!-- Sidebar-->
                            <div class="cz-sidebar rounded-lg box-shadow-lg" id="shop-sidebar">
                                <div class="cz-sidebar-header">
                                    <button class="close ml-auto" type="button" data-dismiss="sidebar" aria-label="Close"><span class="d-inline-block font-size-xs font-weight-normal align-middle">Close sidebar</span><span class="d-inline-block align-middle ml-2" aria-hidden="true">×</span></button>
                                </div>
                                <div class="cz-sidebar-body" data-simplebar="init" data-simplebar-auto-hide="true">
                                    <div class="simplebar-wrapper" style="margin: -30px -16px -30px -30px;">
                                        <div class="simplebar-height-auto-observer-wrapper">
                                            <div class="simplebar-height-auto-observer"></div>
                                        </div>
                                        <div class="simplebar-mask">
                                            <div class="simplebar-offset" style="right: 0px; bottom: 0px;">
                                                <div class="simplebar-content-wrapper" style="height: auto; padding-right: 20px; padding-bottom: 0px; overflow: hidden;">
                                                    <div class="simplebar-content" style="padding: 30px 16px 30px 30px;">
                                                        <div class="row">
                                                            <div class="col-12 p-0 d-flex justify-content-center">
                                                                <h5>Criteria for filter</h5>
                                                            </div>

                                                            <div class="col-12 p-0 d-flex justify-content-end">
                                                                <div runat="server" id="Div4" class="input-group" visible="false" style="justify-content: flex-end;">
                                                                    <asp:Button runat="server" ID="Button3" Style="padding: 3px 20px 3px 20px; width: auto;"
                                                                        CssClass="btn btn-info btn-sm pull-right" Text="Reset" OnClick="btnreset_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-6">
                                                            </div>
                                                            <div class="col-6 p-0">
                                                                <div runat="server" id="Div1" class="input-group" visible="false" style="justify-content: flex-end;">
                                                                    <asp:Button runat="server" ID="btnreset" Style="padding: 3px 20px 3px 20px; width: auto;"
                                                                        CssClass="btn btn-info btn-sm pull-right" Text="Reset" OnClick="btnreset_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="Div11" class="widget widget-categories mb-3">
                                                            <label><b>Year of Import</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="rbsort" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="rbsort_SelectedIndexChanged">

                                                                    <asp:ListItem Value="EstimatePrice18" style="margin-left: 5px !important;">&nbsp;Previous Years <2019-20</asp:ListItem>
                                                                    <asp:ListItem Value="EstimatePrice" Selected="True" style="margin-left: 5px !important;">&nbsp;2019-20</asp:ListItem>
                                                                    <asp:ListItem Value="EstimatePrice21" style="margin-left: 5px !important;">&nbsp;2020-21</asp:ListItem>
                                                                    <asp:ListItem Value="EstimatePricefuture" style="margin-left: 5px !important;">&nbsp;2021-22</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="Div12" class="widget widget-categories mb-3">
                                                            <label><b>Annual Import Value (Rs)</b></label>

                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="chkimportvalue" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="chkimportvalue_SelectedIndexChanged">
                                                                    <%--<asp:ListItem Value="6" style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; Probable future import items</asp:ListItem>--%>
                                                                    <asp:ListItem Value="5" style="margin-left: 5px !important;">&nbsp; Below 0.5 Million</asp:ListItem>
                                                                    <asp:ListItem Value="4" style="margin-left: 5px !important;">&nbsp; 0.5 - 5 Million</asp:ListItem>
                                                                    <asp:ListItem Value="1" style="margin-left: 5px !important;">&nbsp; 5 - 10 Million</asp:ListItem>
                                                                    <asp:ListItem Value="2" style="margin-left: 5px !important;">&nbsp; 10 - 50 Million</asp:ListItem>
                                                                    <asp:ListItem Value="3" style="margin-left: 5px !important;">&nbsp; 50 Million and above</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3 " runat="server">
                                                            <div id="Div5" runat="server">
                                                                <label><b>Company</b></label>
                                                                <asp:LinkButton runat="server" ID="lblviewcomp" Visible="false" OnClick="lblviewcomp_Click"><i class="fa fa-eye pull-right mr-1" style="margin-top: 5px;"></i></asp:LinkButton>
                                                                <div class="input-group">
                                                                    <asp:DropDownList ID="ddlcomp" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3 " runat="server" id="divfilterdivision" visible="false">
                                                            <label><b>Division</b></label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddldivision" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3 " runat="server" id="divfilterunit" visible="false">
                                                            <label><b>Unit</b></label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlunit" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="ddlunit_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div id="Div16" class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label><b>Industry Specifics (NATO Class)</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="rbindustryspecification" runat="server" CssClass="custom-radio" AutoPostBack="true" OnSelectedIndexChanged="rbindustryspecification_SelectedIndexChanged">
                                                                    <asp:ListItem Value="'%15%' or NSNGroup like '%16%' or NSNGroup like '%17%'" style="margin-left: 5px !important;">&nbsp;AeroSpace</asp:ListItem>
                                                                    <asp:ListItem Value="'%95%'  or NSNGroup like '%96%' or ItemCode like '%20324%'" style="margin-left: 5px !important;">&nbsp;Critical & Strategic Raw Material</asp:ListItem>
                                                                    <asp:ListItem Value="'%59%' or NSNGroup like '%61%'" style="margin-left: 5px !important;">&nbsp;Electrical and Electronic</asp:ListItem>
                                                                    <asp:ListItem Value="'%31%'" style="margin-left: 5px !important;">&nbsp;Bearings</asp:ListItem>
                                                                    <asp:ListItem Value="'%48'%" style="margin-left: 5px !important;">&nbsp;Valves</asp:ListItem>
                                                                    <asp:ListItem Value="'%20%'" style="margin-left: 5px !important;">&nbsp;Shipping and Marine</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="Div6" class="widget widget-categories mb-3" runat="server">

                                                            <label><b>Nato Supply Group Classification</b></label><asp:LinkButton runat="server" ID="lblviewnato" OnClick="lblviewnato_Click" ToolTip="Click here for view Details"><i class="fa fa-eye pull-right mr-1"  style="margin-top: 5px;"></i></asp:LinkButton>
                                                            <%-- <span class="tooltiptext">View Only</span>--%>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlnsg" runat="server" CssClass="custom-select" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlnsg_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3" runat="server" id="divnsc" visible="false">
                                                            <label><b>Nato Supply Class</b></label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlnsc" runat="server" CssClass="custom-select" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlnsc_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3" runat="server" id="divic" visible="false">
                                                            <label><b>Item Code</b></label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlic" runat="server" CssClass="custom-select" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlic_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div id="Div7" class="widget widget-categories mb-3" runat="server" runat="server">
                                                            <label><b>Industry Domain (Simple_Way)</b></label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlprodindustrydomain" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="ddlprodindustrydomain_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3" runat="server" id="divisd" visible="false">
                                                            <label><b>Industry Sub-Domain</b></label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlindustrysubdoamin" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="ddlindustrysubdoamin_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div id="Div14" class="widget widget-categories mb-3" runat="server">
                                                            <label><b>Make in India Target (starting) Year</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="rbindigtarget" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="ddldeclaration_SelectedIndexChanged">
                                                                    <asp:ListItem Value="2021-22" style="margin-left: 5px !important;">&nbsp;2021-22</asp:ListItem>
                                                                    <asp:ListItem Value="2022-23" style="margin-left: 5px !important;">&nbsp;2022-23</asp:ListItem>
                                                                    <asp:ListItem Value="2023-24" style="margin-left: 5px !important;">&nbsp;2023-24</asp:ListItem>
                                                                    <asp:ListItem Value="2024-25" style="margin-left: 5px !important;">&nbsp;2024-25</asp:ListItem>
                                                                    <asp:ListItem Value="2025-26" style="margin-left: 5px !important;">&nbsp;2025-26</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="Div3" class="widget widget-categories mb-3" runat="server">
                                                            <label><b>Make in India Category</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="chktendor" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="chktendor_SelectedIndexChanged">
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="Div2" class="widget widget-categories mb-3" runat="server">
                                                            <label><b>Is Indegenized</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="rbIsIndeginized" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="rbIsIndeginized_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Y" style="margin-left: 5px !important;">&nbsp;Yes</asp:ListItem>
                                                                    <asp:ListItem Value="N" style="margin-left: 5px !important;">&nbsp;No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="DivA" class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label><b>Supply Order</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="rblsupplyOrder" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="rblsupplyOrder_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Yes" style="margin-left: 5px !important;">&nbsp;Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No" style="margin-left: 5px !important;">&nbsp;No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="Div10" class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label><b>EOI</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="rbeoistatus" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="rbeoistatus_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Yes" style="margin-left: 5px !important;">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No" style="margin-left: 5px !important;">No</asp:ListItem>
                                                                    <asp:ListItem Value="Archive" style="margin-left: 5px !important;">Archive</asp:ListItem>
                                                                    <asp:ListItem Value="Active" style="margin-left: 5px !important;">Active</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="divIntrested" class="widget widget-categories mb-3 d-none" runat="server" visible="false">
                                                            <div class="input-group" visible="false">
                                                                <a href="~/IntrestedProduct" runat="server" id="lbShownIntrested" class="btn btn-primary"><i class="fa fa-life-ring"></i>&nbsp; Show Interested Product</a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="simplebar-track simplebar-horizontal" style="visibility: hidden;">
                                    <div class="simplebar-scrollbar" style="transform: translate3d(0px, 0px, 0px); display: none;">
                                    </div>
                                </div>
                                <div class="simplebar-track simplebar-vertical" style="visibility: hidden;">
                                    <div class="simplebar-scrollbar" style="height: 25px; transform: translate3d(0px, 0px, 0px); display: none;">
                                    </div>
                                </div>
                            </div>
                        </aside>
                        <div class="col-lg-8" id="cardsection">
                            <div class="row d-flex">
                                <div id="col1" class="col-sm-6">
                                    <h6>
                                        <asp:Label ID="lbltotal" runat="server"></asp:Label></h6>
                                </div>
                                <div id="col2" class="col-sm-6 d-flex justify-content-sm-end justify-content-start">
                                    <h6>
                                        <asp:Label ID="lbltotalleft" runat="server"></asp:Label></h6>
                                </div>
                                <div id="col3" class="col-sm-8">
                                    <h6 style="color: #FF00FF;">
                                        <asp:Label ID="lblyearvalue" runat="server"></asp:Label>
                                        (in million Rs) -
                                <asp:Label ID="lblestimateprice" runat="server"></asp:Label></h6>
                                </div>
                                <div id="col4" class="col-sm-4 d-flex justify-content-sm-end justify-content-start">
                                    <h6>
                                        <asp:LinkButton ID="totoalmore" runat="server" OnClick="totoalmore_Click" ToolTip="Click here for More Details">  <span class="fa fa-eye"></span>  More details</asp:LinkButton></h6>
                                    <p runat="server" visible="false">
                                        Future requirements in lakhs -
                                <asp:Label ID="lblfuturepurchase" runat="server" Text="0"></asp:Label>

                                    </p>
                                </div>
                            </div>

                            <div class="clearfix"></div>

                            <div class="row justify-content-end">
                                <div class="col-3">
                                    <asp:DropDownList ID="ddlsort" runat="server" CssClass="form-control filter_btn" AutoPostBack="true" OnSelectedIndexChanged="ddlsort_SelectedIndexChanged">
                                        <asp:ListItem Value="Sort by">Sort by</asp:ListItem>
                                        <asp:ListItem Value="TotalInterest" style="display: none;">Popularity</asp:ListItem>
                                        <asp:ListItem Value="NonPopuler">opportunity</asp:ListItem>
                                        <asp:ListItem Value="LastUpdated">Newest</asp:ListItem>
                                        <asp:ListItem Value="LastUpdated asc">Oldest</asp:ListItem>
                                        <asp:ListItem Value="Higest Value">Higest Value</asp:ListItem>
                                        <asp:ListItem Value="Lowest Value">Lowest Value</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div id="divcontentproduct" runat="server">
                                <nav class="d-flex justify-content-between pt-2" aria-label="Page navigation">
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:LinkButton ID="LinkButton1" runat="server" class="page-link" OnClick="lnkbtnPgPrevious_Click"><i class="fa fa-chevron-left mr-2"></i>Prev</asp:LinkButton>
                                        </li>
                                    </ul>
                                    <span style="text-align: center;">Showing
                                            <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                                        products of
                                            <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                                        products  
                                    </span>
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:LinkButton ID="LinkButton2" runat="server" class="page-link" OnClick="lnkbtnPgNext_Click">
                            Next<i class="fa fa-chevron-right ml-2"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </nav>
                                <div class="row my-2">

                                    <asp:DataList runat="server" ID="dlproduct" RepeatColumns="3" RepeatLayout="Flow"
                                        RepeatDirection="Horizontal" OnItemCommand="dlproduct_ItemCommand" OnItemDataBound="dlproduct_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="card" id="mycard">
                                                <div style="z-index: 9999!important; position: relative;">
                                                    <a href="#" class="indigenized" runat="server" id="indevisi" style="color: #ff0000a3" visible="false">INDEGENIZED                                                       
                                                    </a>
                                                    <a href="#" class="indigenized" runat="server" id="SupplyOr" style="color: #ff0000a3; font-size: xx-large; font: bold; text-align: center" visible="false">Supply Order Yes
                                                    </a>
                                                    <a href="#" class="indigenized" runat="server" id="inhouse" style="color: #ff0000a3; font-size: -webkit-xxx-large; margin-left: 58px; font: bold; text-align: right" visible="false">In House
                                                    </a>
                                                    <a href="#" class="indigenized" runat="server" id="IGA" style="color: #ff0000a3; font-size: -webkit-xxx-large; margin-left: 121px; font: bold; text-align: center" visible="false">IGA
                                                    </a>
                                                    <asp:Label runat="server" ID="SupplyOr1" Visible="false" Text='<%#Eval("SupplyOrderStatus") %>'></asp:Label>
                                                    <asp:Label runat="server" ID="ISINDI" Visible="false" Text='<%#Eval("IsIndeginized") %>'></asp:Label>
                                                    <asp:Label runat="server" ID="LBPURPOSEOFPROC" Visible="false" Text='<%#Eval("PurposeofProcurement") %>'></asp:Label>
                                                </div>
                                                <div id="card-image d-flex justify-content-center">
                                                    <asp:LinkButton runat="server" ID="lbimagesgow" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="View"
                                                        class="card-img-top d-block overflow-hidden" Style="text-align: center;">
                                                        <img src='<%#Eval("TopImages") %>' alt="Product" style="max-width: 100%; width: 80%; height: 90px;">
                                                    </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                </div>
                                                <div class="card-body" style="min-height: 300px;">
                                                    <b>
                                                        <asp:LinkButton runat="server" ID="lblcompshow" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="View" class="product-meta d-block font-size-xs pb-1" Style="color: #6915cf; font-size: 16px !important;">
                                                                <%#Eval("CompanyName") %>
                                                        </asp:LinkButton>
                                                    </b>
                                                    <p class="product-title" title='<%# Eval("ProductDescription")%>'>
                                                        <%# Eval("ProductDescription").ToString().Length > 15? (Eval("ProductDescription") as string).Substring(0,15) + "..." : Eval("ProductDescription")  %>
                                                    </p>
                                                    <table class="table table-responsive">
                                                        <tbody>
                                                            <tr id="Tr1" runat="server" visible="false">
                                                                <td style="padding: 8px;">NSN Group
                                                                </td>
                                                                <td style="padding: 8px;" class="text-right" title='<%#Eval("NSNGroup") %>'>
                                                                    <%# Eval("NSNGroup").ToString().Length > 15? (Eval("NSNGroup") as string).Substring(0,15) + ".." : Eval("NSNGroup")  %>
                                                                    <asp:HiddenField ID="hfrole" runat="server" Value='<%#Eval("Role") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="font-size: 11px; color: #6915cf;">Annual Import (in million Rs) <b>
                                                                    <asp:Label ID="lblepold" Visible="false" runat="server" Text='<%# Eval("EstimatePrice") %>'></asp:Label>
                                                                    <asp:Label ID="lblepfu" Visible="false" runat="server" Text='<%# Eval("EstimatePricefuture") %>'></asp:Label>
                                                                    <asp:Label ID="lblepold17" Visible="false" runat="server" Text='<%# Eval("EstimatePrice17") %>'></asp:Label>
                                                                    <asp:Label ID="lblepold18" Visible="false" runat="server" Text='<%# Eval("EstimatePrice18") %>'></asp:Label>
                                                                </b>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr2" runat="server">
                                                                <td colspan="2" style="font-size: 12px;">Nato Supply Group Class :-
                                                                        <p><b>[<%#Eval("NSCCode") %>] -  <%# Eval("NSNGroupClass").ToString().Length > 35? (Eval("NSNGroupClass") as string).Substring(0,35) + ".." : Eval("NSNGroupClass")  %></b></p>
                                                                    <asp:HiddenField runat="server" ID="hfnsc" Value='<%#Eval("NSCCode") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr233" runat="server">
                                                                <td colspan="2" style="font-size: 11px;">Last Updated :- 
                                                                        <%#Eval("LastUpdated","{0:dd-MMM-yyyy}") %>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr3" runat="server">
                                                                <td colspan="2" style="font-size: 11px;">
                                                                    <asp:LinkButton ID="lbview" runat="server" class="nav-link-style font-size-ms" ToolTip="Click here for Product More Details" CommandName="View"
                                                                        CommandArgument='<%#Eval("ProductRefNo") %>' Style="color: #6915cf!important;">                                                
                                                    <i class="fa fa-eye align-middle mr-1"></i>
                                                    More Details
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="font-size: 11px;">
                                                                    <a id="LinkButton6" runat="server" target="_blank" class="nav-link-style font-size-ms" tooltip="Click here for View Similar Product"
                                                                        style="color: #6915cf!important;">
                                                                        <i class=" fa fa-arrow-circle-o-right align-middle mr-1"></i>
                                                                        Similar-Products 
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <asp:LinkButton runat="server" ID="lbaddcart" class="btn btn-block" CommandArgument='<%#Eval("ProductRefNo") %>'
                                                    CommandName="addcart"><i class="fa fa-cart-plus"></i> Add to show interest </asp:LinkButton>
                                                <asp:HiddenField ID="hfr" runat="server" Value='<%#Eval("ProductRefNo") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                                <nav class="d-flex justify-content-between pt-2" aria-label="Page navigation">
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" class="page-link" OnClick="lnkbtnPgPrevious_Click"><i class="fa fa-chevron-left mr-2"></i>Prev</asp:LinkButton>
                                        </li>
                                    </ul>
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:Label ID="lblpaging" runat="server"></asp:Label>
                                        </li>
                                    </ul>
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" class="page-link" OnClick="lnkbtnPgNext_Click">
                            Next<i class="fa fa-chevron-right ml-2"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </nav>

                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-quick-view modal fade" id="ProductCompany" tabindex="-1" style="z-index: 9999999;">
                    <div class="modal-dialog modal-xl" style="max-width: 800px !important;">
                        <div class="modal-content">
                            <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                                <h5 class="modal-title text-white">Import Item Details</h5>
                            </div>
                            <div class="modal-body" style="padding: 20px 40px 18px 40px;">
                                <div class="simplebar-content">
                                    <!-- Categories-->
                                    <div class="widget widget-categories mb-4">
                                        <div class="accordion mt-n1" id="shop-categories">
                                            <div id="printarea">
                                                <div class="card" style="border-bottom: solid 1.4px #e5e5e5 !important;">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2">
                                                            <a class="collapsed" href="#shoes" role="button" data-toggle="collapse" aria-expanded="false"
                                                                aria-controls="shoes">Item Description <span class="accordion-indicator iconupanddown">
                                                                    <i class="fa fa-chevron-up"></i></span></a>
                                                        </h3>
                                                    </div>
                                                    <div class="collapse" id="shoes" data-parent="#shop-categories">
                                                        <div class="card-body card-custom ">
                                                            <h6 class="tablemidhead">DPSUs,OFB & SHQs Details</h6>
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr>
                                                                        <th scope="row">DPSU/OFB/SHQ:
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblcompname" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="one">
                                                                        <th scope="row">Division/Plant:Unit
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbldiviname" runat="server" Text=""></asp:Label>
                                                                            &nbsp:
                                                                            <asp:Label ID="lblunitnamepro" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="two" visible="false" class="d-none">
                                                                        <th scope="row" class="d-none">Unit:
                                                                        </th>
                                                                        <td></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <h6 class="tablemidhead">Item Description</h6>
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr>
                                                                        <th scope="row">Item Id (Portal)
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblrefnoview" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr23" style="color: blue;">
                                                                        <th>Item Name</th>
                                                                        <td>
                                                                            <asp:Label ID="lblitemname1" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="three">
                                                                        <th scope="row">DPSU Part Number
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbldpsupartno" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr8">
                                                                        <th scope="row">NIN Code
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblnincode" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="four">
                                                                        <th scope="row">HSN Code
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblhsncode8digit" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Industry Domain
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="prodIndustryDomain" runat="server" Text=""></asp:Label>
                                                                            /
                                                                            <asp:Label ID="ProdIndusSubDomain" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <h6 class="tablemidhead">OEM Details</h6>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="seven">
                                                                        <th scope="row">OEM Name:Country
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbloemname" runat="server" Text=""></asp:Label>
                                                                            :&nbsp;<asp:Label ID="lbloemcountry" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="eight">
                                                                        <th scope="row">OEM Part Number
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbloempartno" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="nine" visible="FALSE" class="d-none">
                                                                        <th scope="row" class="d-none">OEM Country
                                                                        </th>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr runat="server" id="twentyfive" visible="FALSE">
                                                                        <th scope="row">OEM Address
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbloemaddress" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <h6 class="tablemidhead">Item Classification (NATO Group & Class)</h6>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">NATO Supply Group:
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblnsngroup" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">NATO Supply Class:
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblnsngroupclass" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Item Name Code:
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="six">
                                                                        <th scope="row">NSC Code (4 digit):
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblnsccode4digit" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5 !important;">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2">
                                                            <a class="collapsed" href="#ItemSpecification" role="button" data-toggle="collapse"
                                                                aria-expanded="false" aria-controls="shoes">Item Specification <span class="accordion-indicator iconupanddown">
                                                                    <i class="fa fa-chevron-up"></i></span></a>
                                                        </h3>
                                                    </div>
                                                    <div class="collapse" id="ItemSpecification" data-parent="#shop-categories">
                                                        <div class="card-body card-custom ">
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr runat="server" id="eleven" style="color: blue;">
                                                                        <th>Item Name</th>
                                                                        <td>
                                                                            <asp:Label ID="itemname2" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr4" style="color: blue;">
                                                                        <th>Feature & Details</th>
                                                                        <td>
                                                                            <asp:Label ID="lblfeature" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="twele">
                                                                        <th scope="row">Document
                                                                        </th>
                                                                        <td>
                                                                            <asp:GridView runat="server" ID="gvpdf" AutoGenerateColumns="false" Class="table table-responsive table-bordered">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblpathname" runat="server" Text='<%#Eval("ImageName").ToString().Substring(7) %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="View or Download">
                                                                                        <ItemTemplate>
                                                                                            <a href='<%#Eval("ImageName") %>' target="_blank" title="Click on icon for download pdf">View or downlaod</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="thirteen">
                                                                        <th scope="row">Image
                                                                        </th>
                                                                        <td>
                                                                            <asp:DataList ID="dlimage" runat="server" RepeatColumns="4" Visible="true" RepeatDirection="Horizontal"
                                                                                RepeatLayout="Flow">
                                                                                <ItemTemplate>
                                                                                    <div class="col-sm-3">
                                                                                        <a data-fancybox="Prodgridviewgellry" target="_blank" href='<%#Eval("[ImageName]") %>'>
                                                                                            <asp:Image ID="imgprodimage" runat="server" CssClass="img-responsive img-container"
                                                                                                Height="90px" Width="110px" src='<%#Eval("[ImageName]") %>' />
                                                                                        </a>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:DataList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="twentysix">
                                                                        <th scope="row">Quality Assurance Agency 
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbqa" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5 !important;">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2">
                                                            <a class="collapsed" href="#Estimated" role="button" data-toggle="collapse" aria-expanded="false"
                                                                aria-controls="shoes">Import Value, Quantity <span class="accordion-indicator iconupanddown">
                                                                    <i class="fa fa-chevron-up"></i></span></a>
                                                        </h3>
                                                    </div>
                                                    <div class="collapse" id="Estimated" data-parent="#shop-categories">
                                                        <div class="card-body card-custom ">
                                                            <table class="table" width="100%">
                                                                <tbody>

                                                                    <tr runat="server" id="fifteen">
                                                                        <td>
                                                                            <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false"
                                                                                CssClass="table table-hover">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="FYear" HeaderText="Year of Import" />
                                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                                        <ItemTemplate>
                                                                                            <%# Eval("EstimatedQty").ToString() == "0" ? "*" : Eval("EstimatedQty").ToString()%>
                                                                                            <%-- <%# Eval("EstimatedQty").ToString() == "0" ? "*" : "*"%>--%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Import value in million Rs (Qty*Price)" DataFormatString="{0:f2}" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="table mb-2">
                                                                                <tbody>
                                                                                    <tr runat="server" id="five">
                                                                                        <td colspan="2">
                                                                                            <b>Import value during last 3 year (million Rs) :</b>
                                                                                            <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                                                        &nbsp;<asp:Label ID="lblvalueimport" runat="server"
                                                                                            Text="0"></asp:Label>&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="ten">
                                                                                        <td colspan="2" style="border-top: 0px;">
                                                                                            <asp:GridView ID="gvestimatequanold" runat="server" AutoGenerateColumns="false"
                                                                                                Class="table table-responsive table-bordered">
                                                                                                <Columns>
                                                                                                    <asp:BoundField HeaderText="Year of Import" DataField="FYear" />
                                                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                                                        <ItemTemplate>
                                                                                                            <%# Eval("EstimatedQty").ToString() == "0" ? "*" : Eval("EstimatedQty").ToString()%>
                                                                                                            <%-- <%# Eval("EstimatedQty").ToString() == "0" ? "*" : "*"%>--%>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                                                    <asp:BoundField HeaderText="Imported value in million Rs (Qty*Price)" DataField="EstimatedPrice" DataFormatString="{0:f2}" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <h6 class="tablemidhead">Status of Indigenization</h6>
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr runat="server" id="Tr25" class="d-none">
                                                                        <th scope="row">Indigenization starting  Year
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblindtrgyr" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr1">
                                                                        <th scope="row">Indigenization started
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblindstart" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="sixteen">
                                                                        <th scope="row">Make in India Category
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblindicate" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="seventeen">
                                                                        <th scope="row">EoI/RFP
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbleoirep" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="eighteen">
                                                                        <th scope="row">EoI/RFP URL
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbleoilink" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <h6 class="tablemidhead">Contact Details</h6>
                                                            <table class="table mb-2" runat="server" id="nineteen">
                                                                <tbody>
                                                                    <tr>
                                                                        <th scope="row">Name
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblempname" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Designation
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">E-Mail ID
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblemailidpro" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Phone Number
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblphonenumber" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5 !important;" runat="server" visible="false">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2">
                                                            <a class="collapsed" href="#AdditionalValue" role="button" data-toggle="collapse" aria-expanded="false"
                                                                aria-controls="shoes">Additional Details <span class="accordion-indicator iconupanddown">
                                                                    <i class="fa fa-chevron-up"></i></span></a>
                                                        </h3>
                                                    </div>
                                                    <div class="collapse" id="AdditionalValue" data-parent="#shop-categories">
                                                        <div class="card-body card-custom ">
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr runat="server" id="twenty">
                                                                        <th scope="row">End User 
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblenduser" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="twentyone">
                                                                        <th scope="row">Defence Paltform 
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbldefenceplatform" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="twentytwo">
                                                                        <th scope="row">Name of Defence Platform 
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblnameofdefplat" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="twentyfour" visible="false">
                                                                        <th scope="row"></th>
                                                                        <td runat="server">
                                                                            <asp:Label ID="lblisshowgeneral" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <input id="btnprint" type="button" runat="server" visible="false" onclick="PrintDiv()" style="width: 70px;" class="btn btn-primary  pull-right"
                                    value="Print" />
                                <asp:LinkButton ID="LinkButton5" runat="server" Text="Close" Style="width: 80px; background: #507CD1!important;" class="btn"
                                    ClientIDMode="Static" ToolTip="Update Data" data-dismiss="modal" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="divCompany" role="dialog" style="z-index: 9999999;">
                    <div class="modal-dialog" style="max-width: 900px;">
                        <div class="modal-content">
                            <div runat="server" id="mPrint">
                                <div class="modal-header modal-header1" style="background: #507CD1!important;">
                                    <h5 class="text-white">As on
                                        <asp:Label runat="server" ID="atime"></asp:Label></h5>
                                    <asp:LinkButton ID="LinkButton7" runat="server" Text="Close" Style="width: 80px; background-color: white!important; color: blue!important;" class="btn btn-primary"
                                        ClientIDMode="Static" ToolTip="Update Data" data-dismiss="modal" />
                                </div>
                                <div class="modal-body" style="text-align: center;">
                                    <h6>2018-19 (Annual Import Value (in million Rs))</h6>
                                    <div class="clearfix mt10"></div>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" Class="table table-bordered" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                                <asp:BoundField DataField="TotalProd" HeaderStyle-Width="200" HeaderText="Items Imported" NullDisplayText="" />
                                                <asp:BoundField DataField="TotalPrice1819" HeaderStyle-Width="200" HeaderText="Value (in million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdBelow05" HeaderStyle-Width="200" HeaderText="Items Imported below (0.0-0.5 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdLess5" HeaderStyle-Width="200" HeaderText="Items Imported (0.5-5 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdLess10" HeaderStyle-Width="200" HeaderText="Items Imported (5-10 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdLess50" HeaderStyle-Width="200" HeaderText="Items Imported (10-50 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdAbove50" HeaderStyle-Width="200" HeaderText="Items Imported (Above 50 million Rs)" NullDisplayText="" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <h6><span>2019-20 (Annual Import Value (in million Rs))</span></h6>
                                    <div class="clearfix mt10"></div>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvPrdoct" runat="server" AutoGenerateColumns="false" Class="table table-bordered" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                                <asp:BoundField DataField="TotalProd" HeaderStyle-Width="200" HeaderText="Items Imported" NullDisplayText="" />
                                                <asp:BoundField DataField="TotalPrice1920" HeaderStyle-Width="200" HeaderText="Value (in million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdBelow05" HeaderStyle-Width="200" HeaderText="Items Imported below (0.0-0.5 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdLess5" HeaderStyle-Width="200" HeaderText="Items Imported (0.5-5 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceLess5" HeaderText="Annual Import Value 1-5 million" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="ProdLess10" HeaderStyle-Width="200" HeaderText="Items Imported (5-10 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceLess10" HeaderText="Annual Import Value 5-10 million" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="ProdLess50" HeaderStyle-Width="200" HeaderText="Items Imported (10-50 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceLess50" HeaderText="Annual Import Value 10-50 million" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="ProdAbove50" HeaderStyle-Width="200" HeaderText="Items Imported (Above 50 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceAbove50" HeaderText="Annual Import Value 50 million above" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="TargetIndig2020" HeaderStyle-Width="200" HeaderText="Make in India Target Year" NullDisplayText="" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <h6><span style="text-align: center;">2020-21 (Annual Import Value (in million Rs))</span></h6>
                                    <div class="table-responsive">
                                        <asp:GridView ID="DataList1" runat="server" AutoGenerateColumns="false" Class="table table-bordered" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                                <asp:BoundField DataField="TotalProd" HeaderStyle-Width="200" HeaderText="Items Imported" NullDisplayText="" />
                                                <asp:BoundField DataField="TotalPrice2021" HeaderStyle-Width="200" HeaderText="Value (in million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdBelow05" HeaderStyle-Width="200" HeaderText="Items Imported below (0.0-0.5 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdLess5" HeaderStyle-Width="200" HeaderText="Items Imported (0.5-5 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceLess5" HeaderText="Annual Import Value 1-5 million" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="ProdLess10" HeaderStyle-Width="200" HeaderText="Items Imported (5-10 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceLess10" HeaderText="Annual Import Value 5-10 million" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="ProdLess50" HeaderStyle-Width="200" HeaderText="Items Imported (10-50 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceLess50" HeaderText="Annual Import Value 10-50 million" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="ProdAbove50" HeaderStyle-Width="200" HeaderText="Items Imported (Above 50 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceAbove50" HeaderText="Annual Import Value 50 million above" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="TargetIndig2021" HeaderStyle-Width="200" HeaderText="Make in India Target Year" NullDisplayText="" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="clearfix mt10"></div>

                                    <h6><span style="text-align: center;">2021-22 (Annual Import Value (in million Rs))</span></h6>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="false" Class="table table-bordered" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                                <asp:BoundField DataField="TotalProd" HeaderStyle-Width="200" HeaderText="Items Imported" NullDisplayText="" />
                                                <asp:BoundField DataField="TotalPrice2021" HeaderStyle-Width="200" HeaderText="Value (in million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdBelow05" HeaderStyle-Width="200" HeaderText="Items Imported below (0.0-0.5 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="ProdLess5" HeaderStyle-Width="200" HeaderText="Items Imported (0.5-5 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceLess5" HeaderText="Annual Import Value 1-5 million" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="ProdLess10" HeaderStyle-Width="200" HeaderText="Items Imported (5-10 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceLess10" HeaderText="Annual Import Value 5-10 million" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="ProdLess50" HeaderStyle-Width="200" HeaderText="Items Imported (10-50 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceLess50" HeaderText="Annual Import Value 10-50 million" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="ProdAbove50" HeaderStyle-Width="200" HeaderText="Items Imported (Above 50 million Rs)" NullDisplayText="" />
                                                <asp:BoundField DataField="PriceAbove50" HeaderText="Annual Import Value 50 million above" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="TargetIndig2021" HeaderStyle-Width="200" HeaderText="Make in India Target Year" NullDisplayText="" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="clearfix mt10"></div>



                                    <h6><span style="text-align: center;">Make in India Category (Number of Items)</span></h6>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Class="table table-bordered" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                                <asp:BoundField DataField="TotalProd" HeaderText="Items Imported" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="TotalPrice1920" HeaderText="Annual Import Value (in million Rs)" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="ProductTotal" HeaderStyle-Width="200" HeaderText="Total Products" NullDisplayText="" />
                                                <asp:BoundField DataField="MAKE2Total" HeaderStyle-Width="200" HeaderText=" MAKE-II" NullDisplayText="" />
                                                <asp:BoundField DataField="MAKE2Price" HeaderText="Annual Import Value MakeII" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="IDExTotal" HeaderStyle-Width="200" HeaderText="IDEX/AI/INNOVATION/R&D" NullDisplayText="" />
                                                <asp:BoundField DataField="IDExPriceTotal" HeaderText="Annual Import Value IDEX/AI/INNOVATION/R&D" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="IGATotal" HeaderStyle-Width="200" HeaderText="IGA" NullDisplayText="" />
                                                <asp:BoundField DataField="IGAPrice" HeaderText="Annual Import Value IGA" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="OTHERTHANMAKE2Total" HeaderStyle-Width="200" HeaderText="OTHER THAN MAKE-II" NullDisplayText="" />
                                                <asp:BoundField DataField="OTHERTHANMAKE2Price" HeaderText="Annual Import Value OTHER THAN MAKE-II" NullDisplayText="" Visible="false" />
                                                <asp:BoundField DataField="HOUSETotal" HeaderStyle-Width="200" HeaderText="IN HOUSE" NullDisplayText="" />
                                                <asp:BoundField DataField="HOUSEPrice" HeaderText="Annual Import Value IN HOUSE" NullDisplayText="" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <div runat="server" visible="false">
                                        <h6><span style="text-align: center;">Make in India Category 2020-21 (Number of Items)</span></h6>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" Class="table table-bordered" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CompName" HeaderText="Organization" NullDisplayText="" />
                                                    <asp:BoundField DataField="TotalProd" HeaderText="Items Imported" NullDisplayText="" />
                                                    <asp:BoundField DataField="TotalPrice2021" HeaderText="Annual Import Value (in million Rs)" NullDisplayText="" Visible="false" />
                                                    <asp:BoundField DataField="MAKE2Total" HeaderText=" MAKE-II" NullDisplayText="" />
                                                    <asp:BoundField DataField="MAKE2Price" HeaderText="Annual Import Value MakeII" NullDisplayText="" Visible="false" />
                                                    <asp:BoundField DataField="IDExTotal" HeaderText="IDEX/AI/INNOVATION/R&D" NullDisplayText="" />
                                                    <asp:BoundField DataField="IDExPriceTotal" HeaderText="Annual Import Value IDEX/AI/INNOVATION/R&D" NullDisplayText="" Visible="false" />
                                                    <asp:BoundField DataField="IGATotal" HeaderText="IGA" NullDisplayText="" />
                                                    <asp:BoundField DataField="IGAPrice" HeaderText="Annual Import Value IGA" NullDisplayText="" Visible="false" />
                                                    <asp:BoundField DataField="OTHERTHANMAKE2Total" HeaderText="OTHER THAN MAKE-II" NullDisplayText="" />
                                                    <asp:BoundField DataField="OTHERTHANMAKE2Price" HeaderText="Annual Import Value OTHER THAN MAKE-II" NullDisplayText="" Visible="false" />
                                                    <asp:BoundField DataField="HOUSETotal" HeaderText="IN HOUSE" NullDisplayText="" />
                                                    <asp:BoundField DataField="HOUSEPrice" HeaderText="Annual Import Value IN HOUSE" NullDisplayText="" Visible="false" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <h6><span style="text-align: center;">Make in India Target Year (Number of Items)</span></h6>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" Class="table table-bordered" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                                <asp:BoundField DataField="TargetIndig2022" HeaderStyle-Width="200" HeaderText="2021-22" NullDisplayText="" />
                                                <asp:BoundField DataField="TargetIndig2023" HeaderStyle-Width="200" HeaderText="2022-23" NullDisplayText="" />
                                                <asp:BoundField DataField="TargetIndig2024" HeaderStyle-Width="200" HeaderText="2023-24" NullDisplayText="" />
                                                <asp:BoundField DataField="TargetIndig2025" HeaderStyle-Width="200" HeaderText="2024-25" NullDisplayText="" />
                                                <asp:BoundField DataField="TargetIndig2026" HeaderStyle-Width="200" HeaderText="2025-26" NullDisplayText="" />
                                                <asp:BoundField DataField="TargetIndigNILL" HeaderStyle-Width="200" HeaderText="Make in India Target Year NIL" NullDisplayText="" Visible="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="d-flex justify-content-end">
                                    <input id="Button1" type="button" runat="server" onclick="Printm()" style="width: 80px; background: #507CD1!important" class="btn btn-primary"
                                        value="Print" />
                                    <asp:LinkButton type="button" ID="LinkButton4" runat="server" Text="Close" Style="width: 80px; background: #507CD1!important" class="btn btn-primary ml-1"
                                        ClientIDMode="Static" ToolTip="Update Data" data-dismiss="modal" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!----------------------------------------------------Code For Nato Popup------------------------------>
                <div class="modal fade" id="divNatoPopup" role="dialog" style="z-index: 9999999;">
                    <div class="modal-dialog" style="max-width: 900px;">
                        <div class="modal-content">
                            <div runat="server" id="Div15">
                                <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                                    <h6 class="modal-title text-white">Top 10 Products based on Indicative Nato classification</h6>
                                </div>
                                <div class="modal-body">
                                    <div class="table-responsive">

                                        <asp:GridView runat="server" ID="gvnatopop" AutoGenerateColumns="false" Class="table table-bordered " ShowFooter="true">
                                            <Columns>
                                                <asp:BoundField DataField="NSNGroup" HeaderText="NSN Group" />
                                                <asp:BoundField DataField="Total" HeaderText="Total" />
                                                <asp:BoundField DataField="HAL" HeaderText="HAL" />
                                                <asp:BoundField DataField="BDL" HeaderText="BDL" />
                                                <asp:BoundField DataField="BEML" HeaderText="BEML" />
                                                <asp:BoundField DataField="BEL" HeaderText="BEL" />
                                                <asp:BoundField DataField="GRSE" HeaderText="GRSE" />
                                                <asp:BoundField DataField="GSL" HeaderText="GSL" />
                                                <asp:BoundField DataField="MDL" HeaderText="MDL" />
                                                <asp:BoundField DataField="MIDHANI" HeaderText="MIDHANI" />
                                                <asp:BoundField DataField="OFB" HeaderText="OFB" />
                                                <asp:BoundField DataField="SHQ_AIR_FORCE" Visible="false" HeaderText="SHQ (AIR FORCE)" />
                                                <asp:BoundField DataField="SHQ_ARMY" Visible="false" HeaderText="SHQ (ARMY)" />
                                                <asp:BoundField DataField="SHQ_NAVY" Visible="false" HeaderText="SHQ (NAVY)" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="modal-footer d-flex justify-content-end">
                                    <input id="Button2" type="button" runat="server" onclick="Prints()" style="width: 80px; background: #507CD1!important;" class="btn"
                                        value="Print" />
                                    <asp:LinkButton type="button" ID="LinkButton3" runat="server" Text="Close" Style="width: 80px; background: #507CD1!important;" class="btn"
                                        ClientIDMode="Static" ToolTip="Update Data" data-dismiss="modal" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!----------------------------------------------------Code For How to search------------------------------>
                <div class="modal-quick-view modal fade" id="searchtooltip" tabindex="-1">
                    <div class="modal-dialog modal-xl" style="max-width: 900px!important; z-index: 9999999999;">
                        <div class="modal-content">
                            <div class="modal-header d-flex justify-content-center" style="background: #507CD1!important">
                                <h4 class="modal-title">How To Search</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <b>Video</b>
                                    <div>
                                        <video width="100%" height="150" autoplay controls>
                                            <source src="../Videos/how%20to%20search.mp4" autoplay="1" type="video/mp4">
                                        </video>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <p>There are <b>three ways</b> to search product(s) of your interest on srijandefence portal.</p>
                                <br />
                                <p><b>Search by typing minimum three characters. </p>
                                </b>
                                        
                                        <p>(i)&nbsp;&nbsp;&nbsp;&nbsp; <b>Universal Search on the Top.</b></p>
                                <img src="/User/Uassets/Images/img1-1.jpg" />
                                <p>--------------------------------------------------------------------------------------------------</p>
                                <p>
                                    (ii.)<b>&nbsp;NATO classification based search in the left column</b>
                                </p>
                                <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i>In three steps.</i></p>
                                <p>
                                </p>
                                <p>
                                    <b>Step1 : drop down menu</b>- NATO Supply Group
                                </p>
                                <p>
                                    Select “Electrical and Electronic Equipment Components (59)” which is shown in below image.
                                </p>
                                <img src="/User/Uassets/Images/img2-1.jpg" />
                                <p>
                                    <b>Step2 :drop down menu</b> -Nato Supply Class
                                </p>
                                <p>For example: Search Electronic Item like: <i>Resistor</i></p>
                                <p>
                                    <i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select“Resistor (05)” </i>.
                                </p>
                                <img src="/User/Uassets/Images/img2-2.jpg" />

                                <p>
                                    <b>Step3 : drop down menu</b> – Item Code
                                </p>
                                <p>
                                    <i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select“Resistor, Fixed (A2178)”</i>
                                </p>
                                <img src="/User/Uassets/Images/img2-3.jpg" />
                                <p>--------------------------------------------------------------------------------------------------</p>
                                <p>
                                    <b>(iii).Simple way defined “Industry Domain” and “Industry Sub-Domain” search</b>
                                </p>
                                <p>
                                    <b>Step 1 - drop down menu</b>- Industry Domain
                                </p>
                                <p>
                                    <i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select“Electronics”</i>
                                </p>
                                <img src="/User/Uassets/Images/img3-1.jpg" />
                                <p>
                                    <b>Step2 : drop down menu</b> – Industry Sub-Domain
                                </p>
                                <p>
                                    <i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select “Resistor”</i>
                                </p>
                                <img src="/User/Uassets/Images/img3-2.jpg" />
                                <p>--------------------------------------------------------------------------------------------------</p>
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="LinkButton8" runat="server" class="btn btn-primary ml-1" ClientIDMode="Static" data-dismiss="modal" Style="width: 80px; background: #507CD1!important" Text="Close" type="button" />
                                </div>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <!----------------------------------------------------Code For Nato Popup slider------------------------------>
                <div class="modal fade" id="divcompmisorportal" role="dialog" style="z-index: 9999999;">
                    <div class="modal-dialog" style="max-width: 900px;">
                        <div class="modal-content">
                            <div runat="server" id="Div8">
                                <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                                    <h6 class="modal-title text-white">Company Wise Product (Portal/MIS) or Value</h6>
                                </div>
                                <div class="modal-body">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="GridView5" AutoGenerateColumns="false" Class="table table-bordered " ShowFooter="true">
                                            <Columns>
                                                <asp:BoundField DataField="CompName" HeaderText="Comp Name" />
                                                <asp:BoundField DataField="TotalProd" HeaderText="Total Product On MIS" />
                                                <asp:BoundField DataField="TotalProdPortal" HeaderText="Total Product on Portal" />
                                                <asp:BoundField DataField="ValueInYear" HeaderText="Total Portal Product Value (In million Rs)" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="modal-footer d-flex justify-content-end">
                                    <asp:LinkButton type="button" ID="LinkButton9" runat="server" Text="Close" Style="width: 80px; background: #507CD1!important;" class="btn"
                                        ClientIDMode="Static" ToolTip="close Data" data-dismiss="modal" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!----------------------------------------------------Code end For Nato Popup------------------------------>

                <!----------------------------------------------------Banner popup------------------------------>
                <%--<div class="modal fade" id="divbannerpopup" role="dialog" style="z-index: 9999999;">
                    <div class="modal-dialog" style="max-width: 900px;">
                        <div class="modal-content">
                            <div runat="server" id="Div9">
                                document.body.onload = addBanner;

                                 function addBanner () { 
                                // create a new div element 
                               // and give it popup content 
                                var newDiv = document.createElement("div"); 
                                  newDiv.setAttribute("style", "position: fixed; width: 100%; height: 100%;z-index: 9999991;");	
                                  newDiv.setAttribute("id", "popup");	
                                  newDiv.innerHTML +='<div style="position: fixed; background: #000; width: 100%; height: 100%; opacity: .6; z-index: 9999992;"></div>
                                <div style="position: absolute; width: 88%; z-index: 9999993; display: block; top: 0px; background-color: rgb(255, 255, 255); border: 1px solid rgb(221, 221, 221); border-radius: 5px; box-shadow: rgb(170, 170, 170) 0px 2px 8px; overflow: hidden; margin: auto; left: 0; right: 0; text-align: center; margin-top: 138px;">
                                    <button class="open_button" onclick="closeMIBPopup1()" style="position: absolute; background: #fff; color: #000; right: 3px; top: 5px; border-radius: 50%; width: 32px; height: 32px; line-height: 20px; font-size: 18px; font-weight: bold; cursor: pointer; box-shadow: 0 0 10px 0 #000;">X</button>
                                    <div class="popup_body" style="">
                                        <a href="https://mib.gov.in/" style="curser: pointer;" target="_blank">
                                            <img src="https://mib.gov.in/sites/default/files/banner2.png?12345" alt="Banner" style="width: 100%;" /></a>                                                                               
                                    </div>
                                </div>
                                ';   

                              // add the newly created element and its content into the DOM 
                              //var currentDiv = document.getElementById("main_container"); 
                                document.body.insertBefore(newDiv, document.body.firstChild); 

                             // open popup onload
                         openMIBPopup1();
                             }

                         function openMIBPopup1() {
	                   //alert('sdf');
                           var el = document.getElementById('popup');
                           el.style.display = 'block';
 
                         // Updates: set window background color black
                            document.body.style.background = '#353333';
                              }

                            function closeMIBPopup1() {
                          var el = document.getElementById('popup');
                                       el.style.display = 'none';
                                  document.body.style.background = 'white';
                                     }
                                <div class="modal-footer d-flex justify-content-end">
                                    <asp:LinkButton type="button" ID="LinkButton10" runat="server" Text="Close" Style="width: 80px; background: #507CD1!important;" class="btn"
                                        ClientIDMode="Static" ToolTip="close Data" data-dismiss="modal" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                --%><!----------------------------------------------------Banner popup------------------------------>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnreset" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="update">
            <ProgressTemplate>
                <div class="overlay-progress">
                    <div class="custom-progress-bar blue stripes">
                        <span></span>
                        <p style="margin-left: 200px; padding-bottom: 10px;">
                            <b>Processing...</b>
                        </p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <!----------------------------------------------------Code end For Simlier Popup------------------------------>
        <div id="footer1" class="container-fluid py-3">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        © 2020 <a href="https://srijandefence.gov.in/ProductList">www.srijandefence.gov.in</a> | All Right Reserved. | Designed, Developed and Hosted by Department of Defence Production | Visitor Counter 
                        <a href="#">
                            <img src="https://hitwebcounter.com/counter/counter.php?page=7649671&style=0006&nbdigits=6&type=page&initCount=0" border="0" /></a>
                    </div>
                </div>
            </div>
        </div>
        <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
        <script src="User/Uassets/js/bootstrap.bundle.min.js"></script>
        <script src="../assets/fontawesome-free-5.15.2/js/all.min.js"></script>
        <script src="User/Uassets/js/jquery-ui.min.js"></script>
        <%--<script type="text/javascript">
            document.body.onload = addBanner;
            function addBanner() {
                // create a new div element 
                // and give it popup content 
                var newDiv = document.createElement("div");
                newDiv.setAttribute("style", "position: fixed; width: 100%; height: 100%;z-index: 9999991;");
                newDiv.setAttribute("id", "popup");
                newDiv.innerHTML += '<div style=" position: fixed; background: #000; width: 100%;height: 100%;opacity: .6; z-index: 9999992;"></div><div style="  position: absolute;width: 88%; z-index: 9999993;display: block; top:0px; background-color: rgb(255, 255, 255); border: 1px solid rgb(221, 221, 221); border-radius: 5px;  box-shadow: rgb(170, 170, 170) 0px 2px 8px; overflow: hidden; margin: auto; left: 0;  right: 0;  text-align: center;margin-top: 138px;"><button class="open_button" onClick="closeMIBPopup1()" style="position: absolute; background: #fff;color: #000;right: 3px; top: 5px; border-radius: 50%; width: 32px; height: 32px; line-height: 20px; font-size: 18px; font-weight: bold; cursor: pointer;    box-shadow: 0 0 10px 0 #000;">X</button><div class="popup_body" style=" "><a href="https://mib.gov.in/" style="curser:pointer;" target="_blank"><img src="https://mib.gov.in/sites/default/files/banner2.png?12345" alt="Banner" style=" width: 100%;"/></a></div></div>';

                // add the newly created element and its content into the DOM 
                //var currentDiv = document.getElementById("main_container"); 
                document.body.insertBefore(newDiv, document.body.firstChild);

                // open popup onload
                openMIBPopup1();
            }
            function openMIBPopup1() {
                //alert('sdf');
                var el = document.getElementById('popup');
                el.style.display = 'block';

                // Updates: set window background color black
                document.body.style.background = '#353333';
            }
            function closeMIBPopup1() {
                var el = document.getElementById('popup');
                el.style.display = 'none';
                document.body.style.background = 'white';
            }
        </script>--%>

        <script type="text/javascript">
            function showPopup() {
                $('#ProductCompany').modal('show');
            }
        </script>
        <script type="text/javascript">
            function showPopup1() {
                $('#divCompany').modal('show');
            }
        </script>
        <script type="text/javascript">
            function showPopup2() {
                $('#divNatoPopup').modal('show');
            }
        </script>
        <script type="text/javascript">
            function showPopup3() {
                $('#divInfoIdex').modal('show');
            }
        </script>
        <script type="text/javascript">
            function showPopup4() {
                $('#searchtooltip').modal('show');
            }
        </script>
        <script type="text/javascript">
            function showPopup10() {
                $('#divcompmisorportal').modal('show');
            }
        </script>
        <script type="text/javascript">
            function PrintDiv() {
                var divToPrint = document.getElementById('printarea');
                var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
                popupWin.document.open();
                popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
                popupWin.document.close();
            }
        </script>
        <script type="text/javascript">
            function Printm() {
                var divToPrint = document.getElementById('mPrint');
                var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
                popupWin.document.open();
                popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
                popupWin.document.close();
            }
        </script>
        <script type="text/javascript">
            function Prints() {
                var divToPrint = document.getElementById('Div15');
                var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
                popupWin.document.open();
                popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
                popupWin.document.close();
            }
        </script>
        <script>
            $(document).ready(function () {
                $("#adnce_search").click(function () {
                    $("#adnce_search_box").toggle(400);
                });
            });
        </script>

        <script type="text/javascript">
            $(function () {
                SetAutoComplete();
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        SetAutoComplete();
                    }
                });
            };
            function SetAutoComplete() {
                $("[id$=txtsearch]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: 'User/U_ProductList1.aspx/GetSearchKeyword',
                            data: "{ 'prefix': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item,
                                        val: item
                                    };
                                }))
                            }
                        });
                    },
                    minLength: 1

                });
            }
        </script>
        <script>
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });
        </script>
    </form>
</body>
</html>
