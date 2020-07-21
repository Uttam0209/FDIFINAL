<%@ Page Language="C#" AutoEventWireup="true" Inherits="User_U_ProductList" CodeFile="U_ProductList.aspx.cs" ViewStateEncryptionMode="Always" %>

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
    </style>
</head>
<body>
    <div class="modal fade" id="aboutus-modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document" style="max-width: 600px;">
            <div class="modal-content">
                <div class="modal-header">
                    <ul class="nav nav-tabs card-header-tabs" role="tablist">
                        <li class="nav-item"><a class="nav-link active" href="#" data-toggle="tab" role="tab"
                            aria-selected="true">About us</a></li>
                    </ul>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body tab-content py-4">
                    <p class="text-justify">
                        Indigenization Portal <b>(srijandefence.gov.in)</b> is on online market place platform(non-transactional). It is set up by DDP/MoD/Government of India to boost up the 
                        indigenization of defence imported items/components/spares/equipment by the Indian Industries through their own or through Joint Venture with OEMs.
                    </p>
                    <p class="text-justify">
                        DPSUs/OFB/SHQs will share their list of items on this portal with the Indian Industry, which DPSUs/OFB/SHQs have imported last year (2019-20) and which they 
                        are going to import in the current year (2020-21), each item has a total annual import value of more than Rs 50 Lakh. 
                        They will also share their list of items which have been targeted in the current year (2020-21) for indigenization.
                    </p>
                    <p class="text-justify">
                        The Indian Industry will show their interest in those items for which they have capability to design develop & manufacture either on their own or through JVs with OEMs.
                         Thereafter, the concerned DPSUs/OFB/SHQs, based on their requirement of the items and their respective guidelines & procedures will interact with the interested Indian
                         Industry for indigenization.The interested Indian Industry can also interact with concerned DPSUs/OFB/SHQs for their queries related to indigenization.
                    </p>
                </div>
            </div>
        </div>
    </div>
    <!-- contact us modal-->
    <div class="modal fade" id="contact_us_modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document" style="max-width: 600px;">
            <div class="modal-content">
                <div class="modal-header">
                    <ul class="nav nav-tabs card-header-tabs" role="tablist">
                        <li class="nav-item"><a class="nav-link active" href="#" data-toggle="tab" role="tab"
                            aria-selected="true">Contact us</a></li>
                    </ul>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body tab-content py-4">
                    <div class="row">
                        <div class="col-md-6 mb-grid-gutter">
                            <div class="card">
                                <div class="card-body text-center">
                                    <i class="fas fa-map-marker-alt h3 mt-2 mb-4 text-primary"></i>
                                    <h3 class="h6 mb-3">Address</h3>
                                    <ul class="list-unstyled font-size-sm mb-0">
                                        <p class="font-size-sm text-muted">
                                            Example adress here
                                        </p>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 mb-grid-gutter">
                            <div class="card">
                                <div class="card-body text-center">
                                    <i class="fas fa-headset h3 mt-2 mb-4 text-primary"></i>
                                    <h3 class="h6 mb-3">Phone numbers & Email</h3>
                                    <ul class="list-unstyled font-size-sm mb-0">
                                        <li><span class="text-muted mr-1">For customers:</span><a class="nav-link-style"
                                            href="#">+91xxxxxxxxxx</a></li>
                                        <li class="mb-0"><span class="text-muted mr-1">Email</span><a class="nav-link-style">=Mail@exmaple.com</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sc" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="update" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="row " style="padding: 8px;">
                        <div class="col-sm-2 col-3 ">
                            <img src="user/ddp_logo.png" alt="" class="img-fluid" />
                        </div>
                        <div class="col-sm-10 topheadline col-9">
                            <h2 class="mb-0" style="color: #6915cf;">Opportunities for Make in India in Defence</h2>
                        </div>
                    </div>
                </div>
                <div class="page-title-overlap bg-dark pt-4">
                    <div class="container d-lg-flex justify-content-between">
                        <div class="order-lg-2 mb-3 mb-lg-0 pt-lg-2">
                            <div class="navbar-tool dropdown ml-5">
                                <b><a href="#aboutus-modal" class="menu_" data-toggle="modal">About us </a></b><a
                                    href="#contact_us_modal" class="menu_" data-toggle="modal" id="a" runat="server"
                                    visible="false">Contact us </a>
                                <asp:LinkButton ID="lbtotalcart" runat="server" class="navbar-tool-icon-box bg-secondary dropdown-toggle"
                                    OnClick="lbtotalcart_Click">
                                    <span class="navbar-tool-label" runat="server" id="totalno"></span><i class="navbar-tool-icon fas fa-cart-plus"
                                        style="margin-top: 13px;"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbcart" runat="server" Style="color: white; margin-left: 10px;"
                                    OnClick="lbcart_Click">
                                     Show Interest
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="order-lg-1 pr-lg-4 text-center text-lg-left">
                            <h1 class="h3 text-light mb-0">Products imported by Defence PSUs/OFB/SHQ </h1>
                        </div>
                    </div>
                </div>
                <div class="container pb-5 mb-2 mb-md-4">
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
                                                                    <asp:ListItem Value="EstimatePricefuture" style="margin-left: 5px !important;">&nbsp;2020-21</asp:ListItem>
                                                                    <asp:ListItem Value="EstimatePrice" Selected="True" style="margin-left: 5px !important;">&nbsp;2019-20</asp:ListItem>
                                                                    <%--  <asp:ListItem Value="EstimatePrice18" style="margin-left: 5px !important;">&nbsp;2018-19</asp:ListItem>
                                                                    <asp:ListItem Value="EstimatePrice17" style="margin-left: 5px !important;">&nbsp;2017-18</asp:ListItem>--%>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="Div2" class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label><b>Imported During last 3 years</b></label>
                                                            <div class="input-group">
                                                                <asp:CheckBoxList ID="chklast5year" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="chklast5year_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Y" style="margin-left: 5px !important;">&nbsp; YES</asp:ListItem>
                                                                    <%--<asp:ListItem Value="N"  style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; NO</asp:ListItem>--%>
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                        <div id="Div12" class="widget widget-categories mb-3">
                                                            <label><b>Annual Import Value (Rs)</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="chkimportvalue" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="chkimportvalue_SelectedIndexChanged">
                                                                    <asp:ListItem Value="4" style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; 1 - 5 Million</asp:ListItem>
                                                                    <asp:ListItem Value="1" style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; 5 - 10 Million</asp:ListItem>
                                                                    <asp:ListItem Value="2" style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; 10 - 50 Million</asp:ListItem>
                                                                    <asp:ListItem Value="3" style="margin-left: 5px !important;" onClick="MutExChkList(this);">&nbsp; 50 Million and above</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="Div4" class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label><b>Future requirements</b></label>
                                                            <div class="input-group">
                                                                <asp:CheckBoxList ID="rberffpurchase" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="rberffpurchase_SelectedIndexChanged">
                                                                    <asp:ListItem Value="" style="margin-left: 5px !important;">&nbsp; YES</asp:ListItem>
                                                                    <%--<asp:ListItem Value="0" style="margin-left: 5px !important;" onclick="MutExChkList(this);">&nbsp; NO</asp:ListItem>--%>
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                        <%-- <h3 class="accordion-heading">
                                                            <asp:LinkButton class="collapsed" runat="server" ID="lbadvancesearch" OnClick="lbadvancesearch_Click">Advanced Filters</asp:LinkButton></h3>
                                                        <div class="widget widget-links cz-filter" runat="server" id="divhiddensearch" visible="false">--%>
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
                                                        <div id="Div6" class="widget widget-categories mb-3" runat="server" runat="server">
                                                            <label><b>Nato Supply Group</b></label>
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
                                                        <div id="Div8" class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label><b>Make in India Category</b></label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlprocurmentcatgory" runat="server" AutoPostBack="true" CssClass="custom-select"
                                                                    OnSelectedIndexChanged="ddlprocurmentcatgory_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div id="Div9" class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label><b>Indigenized</b></label>
                                                            <div class="input-group">
                                                                <asp:CheckBoxList ID="ddlisindezinized" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="ddlisindezinized_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Y" style="margin-left: 5px !important;" onclick="MutExChkList(this);">&nbsp;YES</asp:ListItem>
                                                                    <asp:ListItem Value="N" style="margin-left: 5px !important;" onclick="MutExChkList(this);">&nbsp;NO</asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                        <div id="Div3" class="widget widget-categories mb-3" runat="server">
                                                            <label><b>Make in India Category</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="chktendor" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="chktendor_SelectedIndexChanged">
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="Div10" class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label><b>Displayed for General</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="ddldeclaration" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="ddldeclaration_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Y" style="margin-left: 5px !important;">&nbsp; YES</asp:ListItem>
                                                                    <asp:ListItem Value="N" style="margin-left: 5px !important;">&nbsp; NO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="Div13" class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label><b>EoI/RFP</b></label>
                                                            <div class="input-group">
                                                                <asp:RadioButtonList ID="chkeoistatus" runat="server" AutoPostBack="true" CssClass="custom-checkbox" OnSelectedIndexChanged="ddldeclaration_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Yes" style="margin-left: 5px !important;">&nbsp; YES</asp:ListItem>
                                                                    <asp:ListItem Value="No" style="margin-left: 5px !important;">&nbsp; NO</asp:ListItem>
                                                                    <asp:ListItem Value="Archive" style="margin-left: 5px !important;">&nbsp; Archive</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <%--</div>--%>
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
                                        <asp:TextBox ID="txtsearch" runat="server" AutoPostBack="false" Style="max-height: 40px;"
                                            ToolTip="search tab with all criteria using words." CssClass="form-control appended-form-control"
                                            OnTextChanged="txtsearch_TextChanged" Placeholder="Search (type min three character)"></asp:TextBox>
                                        <div class="input-group-append-overlay">
                                            <span class="input-group-text"><i class="fas fa-search"></i></span>
                                        </div>
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
                            <div class="clearfix">
                            </div>
                            <div style="float: left;">
                                <p style="float: left;">
                                    Import value during<asp:Label ID="lblyearvalue" runat="server"></asp:Label>
                                    (in million Rs) -
                                    <asp:Label ID="lblestimateprice" runat="server"></asp:Label>
                                </p>
                                <div class="clearfix">
                                </div>
                                <p style="float: left;" runat="server" visible="false">
                                    Future requirements in lakhs -
                                <asp:Label ID="lblfuturepurchase" runat="server" Text="0"></asp:Label>
                                </p>
                            </div>
                            <div class="clearfix">
                            </div>
                            <br />
                            <div id="divcontentproduct" runat="server">
                                <p style="float: right;">
                                    Showing
                                    <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                                    products of
                                <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                                    products  
                                </p>
                                <div class="clearfix">
                                </div>
                                <div class="row mx-n2">
                                    <asp:DataList runat="server" ID="dlproduct" RepeatColumns="3" RepeatLayout="Flow"
                                        RepeatDirection="Horizontal" OnItemCommand="dlproduct_ItemCommand" OnItemDataBound="dlproduct_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="">
                                                <div class="card product-card" style="box-shadow: 0 0.3rem 1.525rem -0.375rem rgba(0, 0, 0, 0.1);">
                                                     <a class="card-img-top d-block overflow-hidden" href="#" style="text-align: center;">
                                                        <img src='<%#Eval("TopImages") %>' alt="Product" style="max-width: 100%; width: 50%; height: 90px;">
                                                    </a>&nbsp;&nbsp;&nbsp;
                                                    <div class="card-body py-2" style="height: 230px;">
                                                        <b>
                                                            <p class="product-meta d-block font-size-xs pb-1" style="color: #6915cf; font-size: 16px!important;">
                                                                <%#Eval("CompanyName") %>
                                                            </p>
                                                        </b>
                                                        <h3 class="product-title font-size-sm">
                                                            <p title='<%#Eval("ProductDescription") %>'>
                                                                <%# Eval("ProductDescription").ToString().Length > 25? (Eval("ProductDescription") as string).Substring(0,25) + ".." : Eval("ProductDescription")  %>
                                                            </p>
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
                                                                    <td colspan="2" style="padding: 8px; font-size: 11px; color: #6915cf;">Annual Import Value (in million Rs) <b>
                                                                        <asp:Label ID="lblepold" Visible="false" runat="server" Text='<%# Eval("EstimatePrice") %>'></asp:Label>
                                                                        <asp:Label ID="lblepfu" Visible="false" runat="server" Text='<%# Eval("EstimatePricefuture") %>'></asp:Label>
                                                                        <asp:Label ID="lblepold17" Visible="false" runat="server" Text='<%# Eval("EstimatePrice17") %>'></asp:Label>
                                                                        <asp:Label ID="lblepold18" Visible="false" runat="server" Text='<%# Eval("EstimatePrice18") %>'></asp:Label>
                                                                    </b>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" visible="false">
                                                                    <td colspan="2" style="padding: 8px; font-size: 12px;">Future requirements in lakhs <b>
                                                                        <asp:Label ID="lblep" runat="server" Text='<%# Eval("EstimatePricefuture") %>'></asp:Label>
                                                                    </b>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr16" runat="server" visible="false">
                                                                    <td colspan="2" style="padding: 8px; font-size: 12px;">Annual Import Quantity                                                                       
                                                                            <b>
                                                                                <asp:Label ID="Label2" runat="server" Style="margin-right: 0px;" Visible="false" Text='<%# Eval("EstimateQu") %>' />
                                                                                <asp:Label ID="Label7" runat="server" Style="margin-right: 0px;" Visible="false" Text='<%# Eval("EstimateQu17") %>' />
                                                                                <asp:Label ID="Label8" runat="server" Style="margin-right: 0px;" Visible="false" Text='<%# Eval("EstimateQu18") %>' />
                                                                                <asp:Label ID="lblestunitold" runat="server" Style="margin-right: 0px;" Visible="false" Text='<%# Eval("EstUnitOld") %>' />
                                                                                <asp:Label ID="Label3" runat="server" Visible="false" Style="margin-right: 0px;" Text='<%# Eval("EstimateQufuture")%>' />
                                                                                <asp:Label ID="lblestunitfut" runat="server" Style="margin-right: 0px;" Visible="false" Text='<%# Eval("EstUnitfuture") %>' />
                                                                            </b>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr3" runat="server" visible="false">
                                                                    <td colspan="2" style="padding: 8px; font-size: 12px;">OEM Part Number :- <b>
                                                                        <%#Eval("OEMPartNumber") %></b>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr17" runat="server" visible="false">
                                                                    <td colspan="2" style="padding: 8px; font-size: 12px;">OEM Name :- <b>
                                                                        <%#Eval("OEMName") %></b>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr18" runat="server" visible="false">
                                                                    <td colspan="2" style="padding: 8px; font-size: 12px;">OEM Country :- <b>
                                                                        <%#Eval("OEMCountry") %></b>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr2">
                                                                    <td colspan="2" style="padding: 5px; font-size: 12px;">Nato Supply Group Class :-
                                                                       <p><b>[<%#Eval("NSCCode") %>] -  <%# Eval("NSNGroupClass").ToString().Length > 35? (Eval("NSNGroupClass") as string).Substring(0,35) + ".." : Eval("NSNGroupClass")  %></b></p>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr4" runat="server" visible="false">
                                                                    <td colspan="2" style="padding: 8px; font-size: 12px;">Tender Status :- <b>
                                                                        <%#Eval("TenderStatus") %></b>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr5" runat="server" visible="false">
                                                                    <td colspan="2" style="padding: 8px; font-size: 12px;">Industry Domain :-
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr6" runat="server" visible="false">
                                                                    <td colspan="2" style="padding: 8px; font-size: 12px; font-weight: 900; border-top: 0px;"
                                                                        title='<%#Eval("ProdIndustryDoamin") %>'>
                                                                        <%# Eval("ProdIndustryDoamin")  %>
                                                                    /
                                                                    <%#Eval("ProdIndustrySubDomain") %>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr7" runat="server" visible="false">
                                                                    <td style="padding: 8px;">Quantity
                                                                    </td>
                                                                    <td style="padding: 8px;" class="text-right">
                                                                        <%#Eval("EstimateQu") %>
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
                                                        <asp:LinkButton ID="lbview" runat="server" class="nav-link-style font-size-ms" CommandName="View"
                                                            CommandArgument='<%#Eval("ProductRefNo") %>'>                                                
                                                    <i class="fas fa-eye align-middle mr-1"></i>
                                                    More Details
                                                        </asp:LinkButton>
                                                    </div>
                                                    <asp:LinkButton runat="server" ID="lbaddcart" class="btn btn-sm btn-block" CommandArgument='<%#Eval("ProductRefNo") %>'
                                                        CommandName="addcart"><i class="navbar-tool-icon fas fa-cart-plus"></i> Add to show interest </asp:LinkButton>
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
                                    <%-- <ul class="pagination">
                                        <asp:TextBox ID="txtpageno" runat="server" AutoPostBack="true" ToolTip="Enter No (Number could not be 0 or either -1)" CssClass="form-control"
                                            Placeholder="Enter PageNo" OnTextChanged="btngoto_Click"></asp:TextBox>
                                    </ul>--%>
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
                    <div class="modal-dialog modal-xl" style="max-width: 800px!important; z-index: 9999999999;">
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
                                                                    <tr id="Tr9" runat="server" visible="false">
                                                                        <th scope="row">Search keywords
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblsearchkeywords" runat="server" Text=""></asp:Label>
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

                                                            <%--<h6 class="tablemidhead">Imported During Last 3 years</h6>--%>
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
                                                                    <tr id="Tr10" runat="server" visible="false">
                                                                        <th scope="row">Specification
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblitemspecification" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="fourteen">
                                                                        <th scope="row">Features & Details
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblfeaturesanddetail" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr11" runat="server" visible="false">
                                                                        <th scope="row">Information
                                                                        </th>
                                                                        <td>
                                                                            <asp:GridView ID="gvProdInfo" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="NameOfSpec" HeaderText="Name of Specification" />
                                                                                    <asp:BoundField DataField="Value" HeaderText="Value " />
                                                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr12" runat="server" visible="false">
                                                                        <th scope="row">Additional Information
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbladditionalinfo" runat="server" Text=""></asp:Label>
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
                                                                    <tr id="Tr13" runat="server" visible="false">
                                                                        <th scope="row">PROCURMENT CATEGORY REMARK
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblprocremarks" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="fifteen">
                                                                        <td>
                                                                            <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false"
                                                                                CssClass="table table-hover">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="FYear" HeaderText="Year of Import" />
                                                                                    <asp:BoundField DataField="EstimatedQty" HeaderText="Quantity" />
                                                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Import value in Rs lakh (Qty*Price)" />
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
                                                                                            <b>Import value during last 3 year (Rs lakhs) :</b>
                                                                                            <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                                                        &nbsp;<asp:Label ID="lblvalueimport" runat="server"
                                                                                            Text="0"></asp:Label>&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="ten">
                                                                                        <td colspan="2" style="border-top: 0px;">
                                                                                            <asp:GridView ID="gvestimatequanold" runat="server" AutoGenerateColumns="false" Class="table table-responsive table-bordered">
                                                                                                <Columns>
                                                                                                    <asp:BoundField HeaderText="Year of Import" DataField="FYear" />
                                                                                                    <asp:BoundField HeaderText="Quantity" DataField="EstimatedQty" />
                                                                                                    <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                                                    <asp:BoundField HeaderText="Imported value in Rs lakh (Qty*Price)" DataField="EstimatedPrice" />
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
                                                                    <tr id="Tr1" runat="server" visible="false">
                                                                        <th scope="row">Tendor Uploaded
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lbltendor" runat="server" Text=""></asp:Label>
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
                                                                    <tr id="Tr14" runat="server" visible="false">
                                                                        <th scope="row">Mobile Number
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblmobilenumber" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Phone Number
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblphonenumber" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="Tr15" runat="server" visible="false">
                                                                        <th scope="row">Fax
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblfaxpro" runat="server" Text=""></asp:Label>
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
                                                                    <tr runat="server" id="twentythree" visible="false">
                                                                        <th scope="row"></th>
                                                                        <td runat="server">
                                                                            <asp:Label ID="lbldeclaration" runat="server" Text="No IPR issue, No violation of TOT agreement, No violation of Security Concern"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="twentyfour" visible="false">
                                                                        <th scope="row"></th>
                                                                        <td runat="server">
                                                                            <asp:Label ID="lblisshowgeneral" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr19" visible="false">
                                                                        <th scope="row" runat="server" visible="false">Is Indigenised 
                                                                        </th>
                                                                        <td runat="server" visible="false">
                                                                            <asp:Label ID="lblisindigenised" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr20" visible="false">
                                                                        <th scope="row">Indian Manufacturer
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblmanuname" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr21" visible="false">
                                                                        <th scope="row">Address
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblmanuaddress" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr22" visible="false">
                                                                        <th scope="row">Year of Make in India
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblyearofindi" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr24" visible="false">
                                                                        <th scope="row">Make in India Process started
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblprocstart" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="Tr25" visible="false">
                                                                        <th scope="row">Make in India Target Year
                                                                        </th>
                                                                        <td>
                                                                            <asp:Label ID="lblindtrgyr" runat="server" Text=""></asp:Label>
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
                            <div class="clearfix mt10">
                            </div>
                            <div class="modal-footer">
                                <input id="btnprint" type="button" onclick="PrintDiv()" style="width: 70px;" class="btn btn-primary  pull-right"
                                    value="Print" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="divCompany" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header modal-header1">
                                <h6 class="modal-title">Imported Items</h6>
                                <button type="button" class="close close1" data-dismiss="modal">
                                    &times;</button>
                            </div>
                            <div class="modal-body">
                                <asp:GridView ID="gvPrdoct" runat="server" Width="100%" AutoGenerateColumns="false"
                                    Class="table  table-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompName" HeaderText="ORGANIZATION" NullDisplayText="#" />
                                        <asp:BoundField DataField="TotalProd" HeaderText="Total ITEMS" NullDisplayText="#" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="modal-footer">
                                <div class="col-sm-3 pull-right">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">
                                        Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:PostBackTrigger ControlID="lnkbtnPgPrevious" />
                <asp:PostBackTrigger ControlID="lnkbtnPgNext" />
                <asp:PostBackTrigger ControlID="txtpageno" />--%>
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
        <div class="container-fluid" style="background-color: #000000;">
            <div class="row ">
                <div class="col-sm-2 col-3 ">
                </div>
                <div class="col-sm-10 col-9">
                    <p style="color: white; padding: 20px; text-align: center;">
                    Website content managed by : Department of Defence Production
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
            function PrintDiv() {
                var divToPrint = document.getElementById('printarea');
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
                                        label: item.split('-')[0],
                                        val: item.split('-')[1]
                                    };
                                }))
                            }
                        });
                    },
                    minLength: 1
                });
            }
        </script>
    </form>
</body>
</html>
