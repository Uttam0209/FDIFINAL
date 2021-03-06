﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntrestedProduct.aspx.cs" Inherits="User_IntrestedProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product List</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta charset="utf-8">
    <link rel="icon" href="media/fevi.png">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/theme.min.css">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/font-awesome-4.5.0/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/style.css">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/jquery-ui.css" />
    <style>
        .adnce_box {
            display: none;
        }

        .btn.dropdown-toggle::after {
            float: right;
            margin-top: 8px;
        }

        .cart-btns {
            padding: 4px 20px;
            font-size: 15px;
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
            transition: background-color .2s ease-in-out;
        }

            .btn span {
                position: relative;
                white-space: nowrap;
            }

        .btn-content {
            opacity: 1;
            transition: opacity .4s ease-in-out;
        }

        .btn:focus {
            outline: none;
        }

        .btn:before {
            content: "";
            position: absolute;
            display: inline-block;
            background-color: #3c0184;
            width: 0;
            height: 100%;
            left: 0;
            top: 0;
            transition: width .25s ease-in-out;
            z-index: 0;
        }

        #shop-sidebar {
            min-height: 1180px;
            box-shadow: 0 0 5px #6915cf !important;
        }

        .btn:hover {
            width: 100%;
            color: #fff;
            background-color: #3c0184;
        }

        .btn.state-change:before {
            width: 0;
        }

        .btn.state-change .btn-content {
            opacity: 0;
        }

        .btn.loading-state .btn-loading {
            opacity: 1;
        }

        .btn.success-state {
            background-color: #00B729;
        }

            .btn.success-state .btn-success {
                opacity: 1;
                transform: translateX(-50%) translateY(-50%);
            }

        .btn.error-state {
            background-color: #D93A40;
        }

            .btn.error-state .btn-error {
                opacity: 1;
                transform: translateX(-50%) translateY(-50%);
            }

        .btn .btn-loading {
            position: absolute;
            left: 50%;
            margin-left: -11px;
            opacity: 0;
            transition: opacity .25s ease-in-out;
        }

        .btn .btn-error {
            position: absolute;
            top: 50%;
            left: 50%;
            opacity: 0;
            transform: translateX(-100%) translateY(-50%);
            transition: opacity 200ms ease-in-out, transform 500ms cubic-bezier(0.68, -0.55, 0.265, 1.55);
        }

        .btn .btn-success {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translateX(-50%) translateY(-300%);
            transition: opacity 200ms ease-in-out, transform 0.3s cubic-bezier(0.68, -0.55, 0.265, 1.55);
        }

        .btn:hover {
            color: #ffffff;
        }
    </style>
    <script type="text/javascript">
        function MutExChkList(checkbox) {
            var checkBoxList = checkbox.parentNode.parentNode.parentNode;
            var list = checkBoxList.getElementsByTagName("input");
            for (var i = 0; i < list.length; i++) {
                if (list[i] != checkbox && checkbox.checked) {
                    list[i].checked = false;
                }
            }
        }
    </script>
    <style>
        #chktendor input {
            margin-right: 10px;
        }

        #chktendor label {
            font-size: 13px;
        }

        #rbindustryspecification_6 {
            margin-top: 5px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="sc" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="update" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="row " style="padding: 8px;">
                        <div class="col-sm-2 col-3 ">
                            <img src="user/ddp_logo.png" class="img-fluid" />
                        </div>
                        <div class="col-sm-8 topheadline col-7">
                            <h2 class="mb-0" style="color: #6915cf;">Opportunities for Make in India in Defence</h2>

                        </div>
                        <div class="col-sm-2 topheadline col-2 ml-auto pt-4">
                            <b><a href="#" style="color: blue; font-size: 12px;">
                                <asp:Label ID="linkusername" runat="server" Visible="false" ToolTip="Login UserName"></asp:Label></a></b>
                        </div>
                    </div>
                </div>
                <div class="page-title-overlap bg-dark pt-4">
                    <div class="container d-lg-flex justify-content-between">
                        <div class="order-lg-2 mb-3 mb-lg-0 pt-lg-2">
                            <div class="navbar-tool dropdown ml-5">
                            </div>
                        </div>
                        <div class="order-lg-1 pr-lg-4 text-center text-lg-left">
                            <h1 class="h3 text-light mb-0">Products imported by Defence PSUs/OFB/SHQ </h1>
                        </div>
                    </div>
                    <div class="order-lg-1 pr-lg-4 text-center text-lg-right" style="margin-top: 10px;">
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
                                        <div runat="server" id="Div13" >
                                            <li class="nav-item dropdown">
                                                <a class="nav-link dropdown-toggle" href="#" id="navbardrop3" data-toggle="dropdown">Contact Us&nbsp;<i class="fa fa-chevron-down" aria-hidden="true"></i>
                                                </a>
                                                <div class="dropdown-menu bg-dark">
                                                    <b><a href="~/FeedBack" runat="server" id="lnkfeedback" class="nav-link">FeedBack</a></b>
                                                      <b><a href="~/GHelpDesk" runat="server" id="a8" class="nav-link">HelpDesk</a></b>
                                                    <b><a href="~/GOfficialLogin" runat="server" id="ai" class="nav-link">HelpDesk Login</a></b>
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
                    </div>
                </div>
                <div class="container pb-5 mb-2 mb-mdsd-4">
                    <div class="row">
                        <aside class="col-lg-4">
                            <!-- Sidebar-->
                            <div class="cz-sidebar rounded-lg box-shadow-lg" id="shop-sidebar">
                                <div class="cz-sidebar-header box-shadow-sm">
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
                                                            <div class="col-6">
                                                            </div>
                                                            <div class="col-6 p-0">
                                                                <div runat="server" id="Div1" class="input-group" visible="false" style="justify-content: flex-end;">
                                                                    <asp:Button runat="server" ID="btnreset" Style="padding: 3px 20px 3px 20px; width: auto;" CssClass="btn btn-info btn-sm pull-right" Text="Reset" OnClick="btnreset_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="Div11" class="widget widget-categories mb-3">
                                                            <label><b>Year of Import</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="rbsort" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="rbsort_SelectedIndexChanged">
                                                                    <asp:ListItem Value="EstimatePrice18" style="margin-left: 5px !important;">&nbsp;2018-19</asp:ListItem>
                                                                    <asp:ListItem Value="EstimatePrice" Selected="True" style="margin-left: 5px !important;">&nbsp;2019-20</asp:ListItem>
                                                                    <asp:ListItem Value="EstimatePricefuture" style="margin-left: 5px !important;">&nbsp;2020-21</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="Div12" class="widget widget-categories mb-3">
                                                            <label><b>Annual Import Value (Rs)</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="chkimportvalue" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="chkimportvalue_SelectedIndexChanged">
                                                                    <asp:ListItem Value="5" style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; Below 0.5 Million</asp:ListItem>
                                                                    <asp:ListItem Value="4" style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; 0.5 - 5 Million</asp:ListItem>
                                                                    <asp:ListItem Value="1" style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; 5 - 10 Million</asp:ListItem>
                                                                    <asp:ListItem Value="2" style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; 10 - 50 Million</asp:ListItem>
                                                                    <asp:ListItem Value="3" style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; 50 Million and above</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3 " runat="server">
                                                            <div id="Div5" runat="server">
                                                                <label><b>Company</b></label>
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
                                                        <div id="Div16" class="widget widget-categories mb-3" runat="server">
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
                                                            <label><b>Nato Supply Group</b></label><asp:LinkButton runat="server" ID="lblviewnato" OnClick="lblviewnato_Click"><i class="fa fa-eye pull-right mr-1" style="margin-top:5px;"></i></asp:LinkButton>
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
                                                            <label><b>Industry Domain</b></label>
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
                                                                    <asp:ListItem Value="2020-21" style="margin-left: 5px !important;">&nbsp;2020-21</asp:ListItem>
                                                                    <asp:ListItem Value="2021-22" style="margin-left: 5px !important;">&nbsp;2021-22</asp:ListItem>
                                                                    <asp:ListItem Value="2022-23" style="margin-left: 5px !important;">&nbsp;2022-23</asp:ListItem>
                                                                    <asp:ListItem Value="2023-24" style="margin-left: 5px !important;">&nbsp;2023-24</asp:ListItem>
                                                                    <asp:ListItem Value="2024-25" style="margin-left: 5px !important;">&nbsp;2024-25</asp:ListItem>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="simplebar-placeholder" style="width: auto; height: 1727px;"></div>
                                </div>
                                <div class="simplebar-track simplebar-horizontal" style="visibility: hidden;">
                                    <div class="simplebar-scrollbar" style="transform: translate3d(0px, 0px, 0px); display: none;"></div>
                                </div>
                                <div class="simplebar-track simplebar-vertical" style="visibility: hidden;">
                                    <div class="simplebar-scrollbar" style="height: 25px; transform: translate3d(0px, 0px, 0px); display: none;"></div>
                                </div>
                            </div>
                        </aside>
                        <div class="col-lg-8">
                            <div class="d-flex justify-content-center justify-content-sm-between align-items-center pt-2 pb-4 pb-sm-5">
                                <div class="col-md-9">
                                    <div class="input-group-overlay d-none d-lg-flex mx-4">
                                        <asp:TextBox ID="txtsearch" runat="server" Style="max-height: 40px;"
                                            ToolTip="search tab with all criteria using words." onblur="SaveData('txtsearch')" CssClass="form-control appended-form-control"
                                            Placeholder="Search (type min three character)"></asp:TextBox>
                                        <asp:Button runat="server" ID="btnsearch" Style="padding: 3px 20px 3px 20px; width: auto;" CssClass="btn btn-info btn-sm pull-right" Text="Search" OnClick="btnsearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div>
                                <b>
                                    <asp:Label ID="lbltotal" runat="server" CssClass="pull-left"></asp:Label>
                                    <b>
                                        <asp:Label ID="lbltotalleft" runat="server" CssClass="pull-right"></asp:Label></b>
                                </b>
                                <div class="clearfix">
                                </div>
                                <asp:LinkButton ID="totoalmore" runat="server" CssClass="pull-right" OnClick="totoalmore_Click">  <span class="fa fa-eye"></span>  More details</asp:LinkButton>
                            </div>

                            <div style="float: left;">
                                <b>
                                    <p style="float: left; color: #FF00FF;">
                                        <asp:Label ID="lblyearvalue" runat="server"></asp:Label>
                                        (in million Rs) -
                                   
                                       

                                        <asp:Label ID="lblestimateprice" runat="server"></asp:Label>
                                    </p>
                                </b>
                                <p style="float: left;" runat="server" visible="false">
                                    Future requirements in lakhs -
                               
                                   

                                    <asp:Label ID="lblfuturepurchase" runat="server" Text="0"></asp:Label>
                                </p>
                            </div>
                            <div class="clearfix">
                            </div>
                            <br />
                            <div id="divcontentproduct" runat="server">

                                <nav class="d-flex justify-content-between pt-2" aria-label="Page navigation">
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:LinkButton ID="LinkButton1" runat="server" class="page-link" OnClick="lnkbtnPgPrevious_Click"><i class="fas fa-chevron-left mr-2"></i>Prev</asp:LinkButton>
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
                            Next<i class="fas fa-chevron-right ml-2"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </nav>
                                <div class="clearfix mt10">
                                </div>
                                <div class="row mx-n2">
                                    <asp:DataList runat="server" ID="dlproduct" RepeatColumns="3" RepeatLayout="Flow"
                                        RepeatDirection="Horizontal" OnItemCommand="dlproduct_ItemCommand" OnItemDataBound="dlproduct_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="">
                                                <div class="card product-card" style="box-shadow: 0 0.3rem 1.525rem -0.375rem rgba(0, 0, 0, 0.1);">
                                                    <asp:LinkButton runat="server" ID="lbimagesgow" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="View"
                                                        class="card-img-top d-block overflow-hidden" Style="text-align: center;">
                                                        <img src='<%#Eval("TopImages") %>' alt="Product" style="max-width: 100%; width: 50%; height: 90px;">
                                                    </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                   
                                                    <div class="card-body py-2" style="height: 200px;">
                                                        <b>
                                                            <asp:LinkButton runat="server" ID="lblcompshow" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="View" class="product-meta d-block font-size-xs pb-1" Style="color: #6915cf; font-size: 16px!important;">
                                                                <%#Eval("CompanyName") %>
                                                            </asp:LinkButton>
                                                        </b>
                                                        <h3 class="product-title font-size-sm">
                                                            <asp:LinkButton runat="server" ID="lbldesc" title='<%#Eval("ProductDescription") %>' CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="View">
                                                                <%# Eval("ProductDescription").ToString().Length > 15? (Eval("ProductDescription") as string).Substring(0,15) + ".." : Eval("ProductDescription")  %>
                                                            </asp:LinkButton>

                                                        </h3>
                                                        <table class="table" style="font-size: 14px;">
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
                                                                    <td colspan="2" style="padding: 8px; font-size: 11px; color: #6915cf;">Annual Import (in million Rs) <b>
                                                                        <asp:Label ID="lblepold" Visible="false" runat="server" Text='<%# Eval("EstimatePrice") %>'></asp:Label>
                                                                        <asp:Label ID="lblepfu" Visible="false" runat="server" Text='<%# Eval("EstimatePricefuture") %>'></asp:Label>
                                                                        <asp:Label ID="lblepold17" Visible="false" runat="server" Text='<%# Eval("EstimatePrice17") %>'></asp:Label>
                                                                        <asp:Label ID="lblepold18" Visible="false" runat="server" Text='<%# Eval("EstimatePrice18") %>'></asp:Label>
                                                                    </b>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr2">
                                                                    <td colspan="2" style="padding: 5px; font-size: 12px;">Nato Supply Group Class :-
                                                                      
                                                                       

                                                                        <p><b>[<%#Eval("NSCCode") %>] -  <%# Eval("NSNGroupClass").ToString().Length > 35? (Eval("NSNGroupClass") as string).Substring(0,35) + ".." : Eval("NSNGroupClass")  %></b></p>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr233" runat="server">
                                                                    <td class="text-right" colspan="2" style="padding: 8px; font-size: 11px;">Last Updated :- 
                                                                       
                                                                       

                                                                        <%#Eval("LastUpdated","{0:dd-MMM-yyyy}") %>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                    <div class="text-center">
                                                        <asp:LinkButton runat="server" ID="lbview" class="btn btn-sm btn-block" CommandArgument='<%#Eval("ProductRefNo") %>'
                                                            CommandName="View">  <i class="fas fa-eye align-middle mr-1"></i>   More Details </asp:LinkButton>
                                                    </div>
                                                    <asp:HiddenField ID="hfr" runat="server" Value='<%#Eval("ProductRefNo") %>' />
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                                <div class="clearfix">
                                </div>
                                <nav class="d-flex justify-content-between pt-2" aria-label="Page navigation">
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" class="page-link" OnClick="lnkbtnPgPrevious_Click"><i class="fas fa-chevron-left mr-2"></i>Prev</asp:LinkButton>
                                        </li>
                                    </ul>
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" class="page-link" OnClick="lnkbtnPgNext_Click">
                            Next<i class="fas fa-chevron-right ml-2"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </nav>
                                <div style="margin-top: 10px;">
                                    <asp:Label ID="lblpaging" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-quick-view modal fade" id="ProductCompany" tabindex="-1">
                    <div class="modal-dialog modal-xl" style="max-width: 900px!important; z-index: 9999999999;">
                        <div class="modal-content">
                            <div class="modal-header modelhead">
                                <h4 class="modal-title product-title" style="font-size: 25px;">Import Item Details
                                </h4>
                                <button class="close" style="padding-right: 45px;" type="button" data-dismiss="modal"
                                    aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body" style="padding: 20px 40px 18px 40px;">
                                <div class="simplebar-content">
                                    <!-- Categories-->
                                    <div class="widget widget-categories mb-4">
                                        <div class="accordion mt-n1" id="shop-categories">
                                            <div id="printarea">
                                                <div class="card" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2">
                                                            <a class="collapsed" href="#shoes" role="button" data-toggle="collapse" aria-expanded="false"
                                                                aria-controls="shoes">Item Description <span class="accordion-indicator iconupanddown">
                                                                    <i class="fas fa-chevron-up"></i></span></a>
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
                                                                        <th scope="row">Division/Plant:
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbldiviname" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="two">
                                                                        <th scope="row">Unit:
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblunitnamepro" runat="server" Text=""></asp:Label>
                                                                        </td>
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
                                                                        <th scope="row">OEM Name
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbloemname" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="eight">
                                                                        <th scope="row">OEM Part Number
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbloempartno" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="nine">
                                                                        <th scope="row">OEM Country
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbloemcountry" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="twentyfive">
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
                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2">
                                                            <a class="collapsed" href="#ItemSpecification" role="button" data-toggle="collapse"
                                                                aria-expanded="false" aria-controls="shoes">Item Specification <span class="accordion-indicator iconupanddown">
                                                                    <i class="fas fa-chevron-up"></i></span></a>
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
                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2">
                                                            <a class="collapsed" href="#Estimated" role="button" data-toggle="collapse" aria-expanded="false"
                                                                aria-controls="shoes">Import Value, Quantity <span class="accordion-indicator iconupanddown">
                                                                    <i class="fas fa-chevron-up"></i></span></a>
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
                                                                        <th scope="row">Link
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbleoilink" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr25">
                                                                        <th scope="row">Make in India (starting) Target Year
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblindtrgyr" runat="server" Text=""></asp:Label>
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
                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;" runat="server" visible="false">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2">
                                                            <a class="collapsed" href="#AdditionalValue" role="button" data-toggle="collapse" aria-expanded="false"
                                                                aria-controls="shoes">Additional Details <span class="accordion-indicator iconupanddown">
                                                                    <i class="fas fa-chevron-up"></i></span></a>
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
                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2">
                                                            <a class="collapsed" href="#Intrest" role="button" data-toggle="collapse" aria-expanded="false"
                                                                aria-controls="shoes">Interest Shown - Industry<span class="accordion-indicator iconupanddown">
                                                                    <i class="fas fa-chevron-up"></i></span></a>
                                                        </h3>
                                                    </div>
                                                    <div class="collapse" id="Intrest" data-parent="#shop-categories">
                                                        <div class="card-body card-custom ">
                                                            <table class="table" width="100%">
                                                                <tbody>
                                                                    <tr runat="server" id="Tr3">

                                                                        <td>
                                                                            <b>Total Industry Shown Interest :-
                                                                           
                                                                                <asp:Label runat="server" ID="lblIntrCount"></asp:Label></b>
                                                                            <div class="clearfix mt-1"></div>
                                                                            <asp:GridView ID="gvRequester" runat="server" AutoGenerateColumns="false"
                                                                                CssClass="table table-hover">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="RequestDate" HeaderText="Date" DataFormatString="{0: dd-MMM-yyyy}" />
                                                                                    <asp:BoundField DataField="RequestCompName" HeaderText="Company" />
                                                                                    <asp:BoundField DataField="RequestAddress" HeaderText="Address" />
                                                                                    <asp:BoundField DataField="RequestBy" HeaderText="Name" />
                                                                                    <asp:BoundField DataField="RequestMobileNo" HeaderText="Mobile" />
                                                                                    <asp:BoundField DataField="RequestEmail" HeaderText="Email" />
                                                                                    <%--<asp:BoundField DataField="RequestTime" HeaderText="Time" />--%>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <div class="clearfix mt-1"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix mt10">
                            </div>
                            <div class="modal-footer">
                                <input id="btnprint" type="button" runat="server" visible="false" onclick="PrintDiv()" style="width: 70px;" class="btn btn-primary  pull-right"
                                    value="Print" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="divCompany" role="dialog">
                    <div class="modal-dialog" style="max-width: 900px;">
                        <div class="modal-content">
                            <div runat="server" id="mPrint">
                                <div class="modal-header modal-header1">
                                    <h6 class="modal-title">
                                        <div>
                                            (as on 
                                           
                                            <asp:Label runat="server" ID="atime"></asp:Label>
                                            ) 
                                       
                                        </div>
                                        <button class="close close1 btn btn-info" data-dismiss="modal" type="button">
                                            ×                                       
                                       
                                        </button>
                                        <h6></h6>
                                        <h6></h6>
                                    </h6>
                                </div>
                                <div class="modal-body" style="text-align: center;">
                                    <b><span>2018-19 (Annual Import Value (in million Rs))</span></b>
                                    <div class="clearfix mt10"></div>
                                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" Class="table table-bordered table-responsive" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                            <asp:BoundField DataField="TotalProd" HeaderStyle-Width="200" HeaderText="Items Imported" NullDisplayText="" />
                                            <asp:BoundField DataField="TotalPrice1819" HeaderStyle-Width="200" HeaderText="Value (in million Rs)" NullDisplayText="" />
                                            <asp:BoundField DataField="ProdLess5" HeaderStyle-Width="200" HeaderText="Items Imported (0.5-5 million Rs)" NullDisplayText="" />
                                            <asp:BoundField DataField="ProdLess10" HeaderStyle-Width="200" HeaderText="Items Imported (5-10 million Rs)" NullDisplayText="" />
                                            <asp:BoundField DataField="ProdLess50" HeaderStyle-Width="200" HeaderText="Items Imported (10-50 million Rs)" NullDisplayText="" />
                                            <asp:BoundField DataField="ProdAbove50" HeaderStyle-Width="200" HeaderText="Items Imported (Above 50 million Rs)" NullDisplayText="" />
                                        </Columns>
                                    </asp:GridView>
                                    <div class="clearfix mt10"></div>
                                    <b><span>2019-20 (Annual Import Value (in million Rs))</span></b>
                                    <div class="clearfix mt10"></div>
                                    <asp:GridView ID="gvPrdoct" runat="server" AutoGenerateColumns="false" Class="table table-bordered table-responsive" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                            <asp:BoundField DataField="TotalProd" HeaderStyle-Width="200" HeaderText="Items Imported" NullDisplayText="" />
                                            <asp:BoundField DataField="TotalPrice1920" HeaderStyle-Width="200" HeaderText="Value (in million Rs)" NullDisplayText="" />
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
                                    <div class="clearfix mt10"></div>
                                    <b><span style="text-align: center;">2020-21 (Annual Import Value (in million Rs))</span></b>
                                    <asp:GridView ID="DataList1" runat="server" AutoGenerateColumns="false" Class="table table-bordered table-responsive" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                            <asp:BoundField DataField="TotalProd" HeaderStyle-Width="200" HeaderText="Items Imported" NullDisplayText="" />
                                            <asp:BoundField DataField="TotalPrice2021" HeaderStyle-Width="200" HeaderText="Value (in million Rs)" NullDisplayText="" />
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
                                    <div class="clearfix mt10"></div>
                                    <b><span style="text-align: center;">Make in India Category (Number of Items)</span></b>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Class="table table-bordered table-responsive" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                            <asp:BoundField DataField="TotalProd" HeaderText="Items Imported" NullDisplayText="" Visible="false" />
                                            <asp:BoundField DataField="TotalPrice1920" HeaderText="Annual Import Value (in million Rs)" NullDisplayText="" Visible="false" />
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
                                    <div class="clearfix mt10"></div>
                                    <div runat="server" visible="false">
                                        <b><span style="text-align: center;">Make in India Category 2020-21 (Number of Items)</span></b>
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" Class="table table-bordered table-responsive" ShowFooter="true">
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
                                    <div class="clearfix mt10"></div>
                                    <b><span style="text-align: center;">Make in India Target Year (Number of Items)</span></b>
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" Class="table table-bordered table-responsive" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompName" HeaderStyle-Width="200" HeaderText="Organization" NullDisplayText="" />
                                            <asp:BoundField DataField="TargetIndig2021" HeaderStyle-Width="200" HeaderText="2020-21" NullDisplayText="" />
                                            <asp:BoundField DataField="TargetIndig2022" HeaderStyle-Width="200" HeaderText="2021-22" NullDisplayText="" />
                                            <asp:BoundField DataField="TargetIndig2023" HeaderStyle-Width="200" HeaderText="2022-23" NullDisplayText="" />
                                            <asp:BoundField DataField="TargetIndig2024" HeaderStyle-Width="200" HeaderText="2023-24" NullDisplayText="" />
                                            <asp:BoundField DataField="TargetIndig2025" HeaderStyle-Width="200" HeaderText="2024-25" NullDisplayText="" />
                                            <asp:BoundField DataField="TargetIndigNILL" HeaderStyle-Width="200" HeaderText="Make in India Target Year NIL" NullDisplayText="" Visible="false" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="col-sm-3 pull-right">
                                    <input id="Button1" type="button" runat="server" onclick="Printm()" style="width: 70px;" class="btn btn-primary  pull-right"
                                        value="Print" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!----------------------------------------------------Code For Nato Popup------------------------------>
                <div class="modal fade" id="divNatoPopup" role="dialog">
                    <div class="modal-dialog" style="max-width: 900px;">
                        <div class="modal-content">
                            <div runat="server" id="Div15">
                                <div class="modal-header modal-header1">
                                    <h6 class="modal-title">
                                        <button class="close close1 btn btn-info" data-dismiss="modal" type="button">
                                            ×                                       
                                       
                                        </button>
                                    </h6>
                                </div>
                                <div class="modal-body" style="text-align: center;">
                                    <b><span style="text-align: center;">DPSU's Top 10 Product of Nato Group Wise</span></b>
                                    <asp:GridView runat="server" ID="gvnatopop" AutoGenerateColumns="false" Class="table table-bordered table-responsive">
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
                                            <asp:BoundField DataField="SHQ_AIR_FORCE" HeaderText="SHQ (AIR FORCE)" />
                                            <asp:BoundField DataField="SHQ_ARMY" HeaderText="SHQ (ARMY)" />
                                            <asp:BoundField DataField="SHQ_NAVY" HeaderText="SHQ (NAVY)" />

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="col-sm-3 pull-right">
                                    <input id="Button2" type="button" runat="server" onclick="Prints()" style="width: 70px;" class="btn btn-primary  pull-right"
                                        value="Print" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!----------------------------------------------------Code end For Nato Popup------------------------------>

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
        <script src="User/Uassets/js/all.min.js"></script>
        <script src="User/Uassets/js/bootstrap.bundle.min.js"></script>
        <script src="User/Uassets/js/theme.min.js"></script>
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
        <script src="User/Uassets/js/jquery-ui.min.js"></script>
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
                            url: 'User/U_ProductList.aspx/GetSearchKeyword',
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
            var d = new Date();
            document.getElementById("demo").innerHTML = d;
        </script>

        <script>
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });
        </script>
    </form>
</body>
</html>
