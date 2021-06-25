<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProductFilter.aspx.cs" Inherits="Admin_ViewProductFilter" ViewStateEncryptionMode="Always" %>

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
    <link href="~/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/aaryan.css" rel="stylesheet" />
    <link href="~/assets/css/custom.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="assets/js/jquery-3.4.1.js"></script>
    <style>
        * {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }

        a {
            text-decoration: none !important;
        }

        .modal-content {
            max-width: 600px;
        }

        .widget-categories .accordion-heading > a .accordion-indicator {
            position: absolute;
            right: 20px;
            width: 1.375rem;
            height: 1.375rem;
            margin-top: .6875rem;
            background-color: rgba(254, 105, 106, 0.1);
            font-size: 8px;
            line-height: 1.375rem;
        }

        .accordion .collapsed .accordion-indicator {
            -webkit-transform: rotate(-180deg);
            transform: rotate(-180deg);
        }

        .fa {
            font-size: 16px;
        }

        #ProductCompany a {
            text-decoration: none;
        }

        #gvproduct tbody tr > th {
            text-align: center !important;
        }

        #gvproduct tbody tr > td {
            text-align: center !important;
        }

        .table, th, td {
            border: 1px solid;
            padding: 5px;
        }

        table {
            border-spacing: 15px;
        }

        #myTopnav {
            background: #373f50;
            padding: 0px !important;
            z-index: 0 !important;
        }

            #myTopnav li {
                padding: 20px;
                text-transform: uppercase;
            }

                #myTopnav li a {
                    color: #fff;
                }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="site-holder">
            <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
            <nav class="navbar" role="navigation">
                <div class="collapse navbar-collapse">
                    <ul class="nav navbar-nav">
                        <li>
                            <a href="ProductList" style="padding: 0px !important;">
                                <img src="../ddp_logo.png" class="img-fluid" style="height: 70px;" /></a></li>
                    </ul>
                    <ul class="nav navbar-nav" style="margin: 10px 0px 0px 260px;">
                        <li class="nav-toggle"></li>
                    </ul>
                    <ul class="nav navbar-nav user-menu navbar-right ">
                        <li><a href="javascript:void(0)" class="user dropdown-toggle comp_fact_unit" data-toggle="dropdown">
                            <div id="DivCompanyName" runat="server">
                                <span style='display: inline-block; margin-right: 30px;'>
                                    <asp:Label ID="lblmastercompany" runat="server"></asp:Label>
                                    <asp:Label ID="lblfactory" runat="server"></asp:Label>
                                    <asp:Label ID="lblunit" runat="server"></asp:Label>
                                </span>
                            </div>
                            <span class="header-user-box">
                                <asp:Label ID="lblusername" runat="server"></asp:Label>
                                <i class="fa fa-user-circle"></i>
                                &nbsp;&nbsp;<i class="fa fa-sort-down"></i></span> &nbsp;</a>
                            <ul class="dropdown-menu user-login-dropdown" style="z-index: 999999;">
                                <li><a href='<%=ResolveUrl("~/ChangePassword") %>'><i class="fa fa-key" aria-hidden="true"></i>Change Password</a></li>
                                <li>
                                    <asp:LinkButton ID="lbllogout" runat="server" class="" OnClick="lbllogout_Click"><i class="fa fa-lock" aria-hidden="true"></i>Logout</asp:LinkButton></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </nav>
            <nav id="myTopnav" class="navbar navbar-inverse">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                    </div>
                    <div class="collapse navbar-collapse" id="myNavbar">
                        <ul class="nav navbar-nav navbar-right">
                            <li><b><a runat="server" href="~/ProductList"><i class="fa fa-home" aria-hidden="true"></i></a></b></li>
                            <li><b><a href="About" class="nav-link">About us </a></b></li>
                            <li><b><a href="~/PReport2" id="A3" runat="server" class="nav-link">Progress Report</a></b></li>
                            <li><b><a href="~/FeedBack" runat="server" id="A1" class="nav-link">FeedBack</a></b></li>
                            <li><b><a href="../UserManual.pdf" runat="server" id="A2" target="_blank" class="nav-link">User Manual</a></b></li>
                            <li><b><a href="https://www.makeinindiadefence.gov.in/" target="_blank" class="nav-link" onclick="return confirm('You are being redirected to https://www.makeinindiadefence.gov.in');">Make In India Defence Portal </a></b></li>
                        </ul>

                    </div>
                </div>
            </nav>
            <asp:UpdatePanel ID="update" runat="server">
                <ContentTemplate>
                    <div class="contentWithFilter">
                        <div class="filter-sidebar">
                            <!--Accordion wrapper-->
                            <h2>Filters</h2>
                            <div class="common-filter-box" runat="server" id="div2">
                                <h3>Item Id (Portal)</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:TextBox ID="txtitemportalid" runat="server" CssClass="form-control" Style="margin-top: 5px;" AutoPostBack="true" OnTextChanged="txtitemportalid_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" id="divfiltercompany">
                                <h3>Company</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlcomp" runat="server" CssClass="form-control" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" id="divfilterdivision" visible="false">
                                <h3>Division</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddldivision" runat="server" CssClass="form-control" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" id="divfilterunit" visible="false">
                                <h3>Unit</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlunit" runat="server" CssClass="form-control" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlunit_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" visible="false">
                                <h3>Nato Supply Group</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlnsg" runat="server" CssClass="form-control" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlnsg_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" id="divnsc" visible="false">
                                <h3>Nato Supply Class</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlnsc" runat="server" CssClass="form-control" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlnsc_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" id="divic" visible="false">
                                <h3>Item Code</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlic" runat="server" CssClass="form-control" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlic_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box">
                                <h3>Industry Domain</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlprodindustrydomain" runat="server" CssClass="form-control" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlprodindustrydomain_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box">
                                <h3>Procurment Category</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlprocurmentcatgory" runat="server" AutoPostBack="true" Style="margin-top: 5px;" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlprocurmentcatgory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" visible="true">
                                <h3>Indigenized</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlisindezinized" runat="server" AutoPostBack="true" Style="margin-top: 5px;" CssClass="form-control" OnSelectedIndexChanged="ddlisindezinized_SelectedIndexChanged">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" visible="true">
                                <h3>EOI Status </h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddldeclaration" runat="server" AutoPostBack="true" Style="margin-top: 5px;" CssClass="form-control" OnSelectedIndexChanged="ddldeclaration_SelectedIndexChanged">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="YES">YES</asp:ListItem>
                                        <asp:ListItem Value="NO">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" visible="true">
                                <h3>Supply Order Status</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlimported" runat="server" AutoPostBack="true" Style="margin-top: 5px;" CssClass="form-control" OnSelectedIndexChanged="ddlimported_SelectedIndexChanged">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="YES">YES</asp:ListItem>
                                        <asp:ListItem Value="NO">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box">
                                <h3>Description (Min three word)</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:TextBox ID="ddlsearchkeywordsfilter" runat="server" AutoPostBack="true" Style="margin-top: 5px;" ToolTip="For search please type minimum three word and press enter or tab" CssClass="form-control"
                                        OnTextChanged="ddlsearchkeywordsfilter_TextChanged" Placeholder="Description (type min three word)">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="box-holder">
                            <div class="content oem-content">
                                <div class="sideBg">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="lblback" runat="server" class="fa fa-arrow-circle-left pull-right" OnClick="lblback_Click">Back</asp:LinkButton>
                                        </div>
                                        <div class="clearfix" style="margin-bottom: 10px;"></div>
                                        <asp:HiddenField runat="server" ID="hfmtype" />
                                        <asp:HiddenField runat="server" ID="hfmref" />
                                        <div class="col-md-12" id="divProductGrid" runat="server">
                                            <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                                            <div runat="server" id="divpageindex1" visible="false">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lnkbtnPgPre" runat="server" CssClass="btn btn-info btn-sm pull-left"
                                                                OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4 text-center" style="display: flex">
                                                            <asp:TextBox runat="server" ID="txtsea" CssClass="form-control" Width="250px" AutoCompleteType="Search" Placeholder="Please enter no of page"></asp:TextBox>
                                                            <asp:LinkButton ID="btngo" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btngoto_Click">Go to</asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lnkbtnne" runat="server" CssClass="btn btn-info btn-sm pull-right"
                                                                OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div class="col-sm-12">
                                                    <div class="pull-left">
                                                        <asp:Label ID="lblcountpgindex" runat="server" class="btn btn-primary" Text=""></asp:Label>&nbsp;
                                                        <asp:Label ID="lbltotal" runat="server" CssClass="btn btn-primary" Text=""></asp:Label>&nbsp;                                                        
                                                             <asp:LinkButton ID="lbclearfilter" runat="server" Visible="false"
                                                                 CssClass="btn btn-danger" OnClick="lbclearfilter_Click"><i class="fa fa-times"></i>&nbsp;Clear Filter</asp:LinkButton>&nbsp;
                                                             <asp:LinkButton ID="lbldownloadexcel" runat="server" ToolTip="Download Excel" Class="btn btn-primary"
                                                                 OnClick="lbldownloadexcel_Click"><i class="fa fa-file"></i>&nbsp;Download Excel</asp:LinkButton>&nbsp;                                                        
                                                        <span style="margin-right: 30px; font-weight: bold; color: red; margin-bottom: -20px;">Note : Click on any column to Sort</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <!-----------------------------------------end code for page indexing----------------------------------------------------->
                                            <div class="col-md-12" style="overflow: scroll;">
                                                <asp:GridView ID="gvproduct" runat="server" Class="table table-responsive table-hover table-bordered"
                                                    AutoGenerateColumns="false" AllowSorting="true" OnSorting="OnSorting" OnRowCommand="gvproduct_RowCommand"
                                                    OnRowDataBound="gvproduct_RowDataBound">
                                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("ProductRefNo") %>'></asp:LinkButton>
                                                                <asp:HiddenField ID="hfroleProd" runat="server" Value='<%#Eval("Role") %>' />
                                                                <asp:HiddenField ID="hfcomprefno" runat="server" Value='<%#Eval("CompanyRefNo") %>' />
                                                                <asp:HiddenField ID="hfisaaproved" runat="server" Value='<%#Eval("IsApproved") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProductRefNo" HeaderText="Item Id (Portal)" NullDisplayText="#" SortExpression="ItemDescriptionPDFFile" />
                                                        <asp:TemplateField HeaderText="PDF" SortExpression="ItemDescriptionPDFFile">
                                                            <ItemTemplate>
                                                                <asp:HyperLink runat="server" ID="lbpdffile" Target="_blank" NavigateUrl='<%#Eval("TopPdf") %>' CssClass="fa fa-file-pdf"></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Image" SortExpression="TopImages">
                                                            <ItemTemplate>
                                                                <a data-fancybox="Prodgridviewgellry" href='<%#Eval("TopImages") %>'>
                                                                    <asp:Image ID="imgtop" runat="server" Height="80px" Width="80px" src='<%#Eval("TopImages") %>' />
                                                                </a>
                                                                <asp:HiddenField ID="hfimagehide" runat="server" Value='<%#Eval("TopImages") %>' />
                                                                <asp:Label ID="lblimagena" runat="server" Text="NA"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="-" SortExpression="CompanyName" />
                                                        <asp:BoundField DataField="ProductDescription" HeaderText="Description" NullDisplayText="" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="ProductDescription" />
                                                        <asp:BoundField DataField="NSNGroup" HeaderText="Nato Supply Group" NullDisplayText="-" SortExpression="NSNGroup" />
                                                        <asp:BoundField DataField="NSNGroupClass" HeaderText="Nato Supply Class" NullDisplayText="-" SortExpression="NSNGroupClass" />
                                                        <asp:BoundField DataField="1718" HeaderText="2017-18" NullDisplayText="-" SortExpression="1718" />
                                                        <asp:BoundField DataField="1819" HeaderText="2018-19" NullDisplayText="-" SortExpression="1819" />
                                                        <asp:BoundField DataField="1920" HeaderText="2019-20" NullDisplayText="-" SortExpression="1920" />
                                                        <asp:BoundField DataField="2021" HeaderText="2020-21" NullDisplayText="-" SortExpression="2021" />
                                                        <asp:BoundField DataField="Make2Category" HeaderText="Make In India Category" NullDisplayText="-" SortExpression="Make2Category" />
                                                        <asp:BoundField DataField="LastUpdated" HeaderText="Last Updated" NullDisplayText="" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="LastUpdated" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                                            <div runat="server" id="divpageindex" visible="false">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-info  btn-sm"
                                                                OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4" style="display: flex">
                                                            <asp:TextBox runat="server" ID="txtpageno" CssClass="form-control" Width="250px" AutoCompleteType="Search" Placeholder="Please enter no of page"></asp:TextBox>
                                                            <asp:LinkButton ID="btngoto" runat="server" CssClass="btn btn-primary" OnClick="btngoto_Click">Go to</asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn btn-info btn-sm pull-right" Style="margin-right: 3px;"
                                                                OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div class="text-center">
                                                    <asp:Label ID="lblpaging" runat="server" class="btn btn-primary" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <!-----------------------------------------end code for page indexing----------------------------------------------------->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-quick-view modal fade" id="ProductCompany" tabindex="-1" style="z-index: 9999999;">
                            <div class="modal-dialog modal-xl" style="max-width: 800px !important;">
                                <div class="modal-content">
                                    <div class="modal-header d-flex justify-content-center" style="background: #507CD1!important;">
                                        <h3 class="modal-title text-white text-center">Import Item Details</h3>
                                    </div>
                                    <div class="modal-body" style="padding: 20px">
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
                                                                    <h4 class="tablemidhead">DPSUs,OFB & SHQs Details</h4>
                                                                    <table class="table mb-2 table-bordered">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>DPSU/OFB/SHQ:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblcompname" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="one">
                                                                                <td>Division/Plant:Unit
                                                                                </td>
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
                                                                    <h4 class="tablemidhead">Item Description</h4>
                                                                    <table class="table mb-2">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>Item Id (Portal)
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblrefnoview" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="Tr23" style="color: blue;">
                                                                                <td>Item Name</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblitemname1" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="three">
                                                                                <td>DPSU Part Number
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbldpsupartno" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="Tr8">
                                                                                <td>NIN Code
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblnincode" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="four">
                                                                                <td>HSN Code
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblhsncode8digit" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Industry Domain
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="prodIndustryDomain" runat="server" Text=""></asp:Label>
                                                                                    /
                                                                            <asp:Label ID="ProdIndusSubDomain" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <h4 class="tablemidhead">OEM Details</h4>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="seven">
                                                                                <td>OEM Name:Country
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbloemname" runat="server" Text=""></asp:Label>
                                                                                    :&nbsp;<asp:Label ID="lbloemcountry" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="eight">
                                                                                <td>OEM Part Number
                                                                                </td>
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
                                                                                <td>OEM Address
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbloemaddress" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <h4 class="tablemidhead">Item Classification (NATO Group & Class)</h4>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>NATO Supply Group:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblnsngroup" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>NATO Supply Class:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblnsngroupclass" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Item Name Code:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="six">
                                                                                <td>NSC Code (4 digit):
                                                                                </td>
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
                                                                                <td>Item Name</td>
                                                                                <td>
                                                                                    <asp:Label ID="itemname2" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="twele">
                                                                                <td>Document
                                                                                </td>
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
                                                                                <td>Image
                                                                                </td>
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
                                                                                <td>Quality Assurance Agency 
                                                                                </td>
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
                                                                    <h4 class="tablemidhead">Status of Indigenization</h4>
                                                                    <table class="table mb-2">
                                                                        <tbody>
                                                                            <tr runat="server" id="Tr25">
                                                                                <td>Indigenization starting  Year
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblindtrgyr" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="Tr1">
                                                                                <td>Indigenization started
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblindstart" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="sixteen">
                                                                                <td>Make in India Category
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblindicate" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="seventeen">
                                                                                <td>EoI/RFP
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbleoirep" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="eighteen">
                                                                                <td>EoI/RFP URL
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbleoilink" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <h4 class="tablemidhead">Contact Details</h4>
                                                                    <table class="table mb-2" runat="server" id="nineteen">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>Name
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblempname" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Designation
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>E-Mail ID
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblemailidpro" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Phone Number
                                                                                </td>
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
                                                                                <td>End User 
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblenduser" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="twentyone">
                                                                                <td>Defence Paltform 
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbldefenceplatform" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="twentytwo">
                                                                                <td>Name of Defence Platform 
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblnameofdefplat" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="twentyfour" visible="false">
                                                                                <td></td>
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
                                        <asp:LinkButton ID="LinkButton5" runat="server" Text="Close" Style="width: 80px; background: #507CD1!important; color: #fff!important;" class="btn"
                                            ClientIDMode="Static" ToolTip="Update Data" data-dismiss="modal" />
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
                    <asp:PostBackTrigger ControlID="lbldownloadexcel" />
                    <asp:PostBackTrigger ControlID="lbclearfilter" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="update">
                <ProgressTemplate>
                    <div class="overlay-progress">
                        <div class="custom-progress-bar blue stripes">
                            <span></span>
                            <p>Processing</p>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </form>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/custom.js"></script>
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
        function showPopup4() {
            $('#ProductCompany').modal('show');
        }
    </script>
</body>
</html>










