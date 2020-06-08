<%@ Page Language="C#" AutoEventWireup="true" CodeFile="U_ProductList.aspx.cs" Inherits="User_U_ProductList" ViewStateEncryptionMode="Always" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product List</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta charset="utf-8">
    <link rel="icon" href="media/fevi.png">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/theme.min.css">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/font-awesome-4.5.0/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/style.css">

    <style>
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

        .btn:hover:before {
            width: 100%;
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
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="update" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="row " style="padding: 8px;">
                        <div class="col-sm-2 col-3 ">
                            <img src="user/ddp_logo.png" alt="" class="img-fluid" />
                        </div>
                        <div class="col-sm-10 topheadline col-9">
                            <h1 class="mb-0">Srijan Defence</h1>
                        </div>
                    </div>
                </div>
                <div class="page-title-overlap bg-dark pt-4">
                    <div class="container d-lg-flex justify-content-between">
                        <div class="order-lg-2 mb-3 mb-lg-0 pt-lg-2">
                            <div class="navbar-tool dropdown ml-5">
                                <asp:LinkButton ID="lbtotalcart" runat="server" class="navbar-tool-icon-box bg-secondary dropdown-toggle" OnClick="lbtotalcart_Click">
                                    <span class="navbar-tool-label" runat="server" id="totalno"></span>
                                    <i class="navbar-tool-icon fas fa-cart-plus" style="margin-top: 13px;"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbcart" runat="server" Style="color: white; margin-left: 10px;" OnClick="lbcart_Click">
                                     Cart
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="order-lg-1 pr-lg-4 text-center text-lg-left">
                            <h1 class="h3 text-light mb-0">Product List</h1>
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
                                                        <div class="widget widget-categories mb-3 ">
                                                            <h3 class="widget-title">Search</h3>
                                                            <label>Company</label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlcomp" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3 " runat="server" id="divfilterdivision" visible="false">
                                                            <label>Division</label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddldivision" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3 " runat="server" id="divfilterunit" visible="false">
                                                            <label>Unit</label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlunit" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="ddlunit_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3">
                                                            <label>Nato Supply Group</label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlnsg" runat="server" CssClass="custom-select" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlnsg_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3" runat="server" id="divnsc" visible="false">
                                                            <label>Nato Supply Class</label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlnsc" runat="server" CssClass="custom-select" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlnsc_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3" runat="server" id="divic" visible="false">
                                                            <label>Item Code</label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlic" runat="server" CssClass="custom-select" Style="margin-top: 5px;" AutoPostBack="true" OnSelectedIndexChanged="ddlic_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3">
                                                            <label>Industry Domain</label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlprodindustrydomain" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="ddlprodindustrydomain_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3">
                                                            <label>Procurment Category </label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlprocurmentcatgory" runat="server" AutoPostBack="true" CssClass="custom-select"
                                                                    OnSelectedIndexChanged="ddlprocurmentcatgory_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label>Indigenized </label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlisindezinized" runat="server" AutoPostBack="true" CssClass="custom-select" OnSelectedIndexChanged="ddlisindezinized_SelectedIndexChanged">
                                                                    <asp:ListItem Value="">Select</asp:ListItem>
                                                                    <asp:ListItem Value="Y">YES</asp:ListItem>
                                                                    <asp:ListItem Value="N">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label>Declaration </label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddldeclaration" runat="server" AutoPostBack="true" CssClass="custom-select" OnSelectedIndexChanged="ddldeclaration_SelectedIndexChanged">
                                                                    <asp:ListItem Value="">Select</asp:ListItem>
                                                                    <asp:ListItem Value="Y">YES</asp:ListItem>
                                                                    <asp:ListItem Value="N">NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="widget widget-categories mb-3" runat="server" visible="false">
                                                            <label>Imported During last 5 years </label>
                                                            <div class="input-group">
                                                                <asp:DropDownList ID="ddlimported" runat="server" AutoPostBack="true" CssClass="custom-select" OnSelectedIndexChanged="ddlimported_SelectedIndexChanged">
                                                                    <asp:ListItem Value="">Select</asp:ListItem>
                                                                    <asp:ListItem Value="Y">YES</asp:ListItem>
                                                                    <asp:ListItem Value="N">NO</asp:ListItem>
                                                                </asp:DropDownList>
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
                                        <asp:TextBox ID="ddlsearchkeywordsfilter" runat="server" AutoPostBack="true" Style="max-height: 40px;" ToolTip="For search please type minimum three character and press enter or tab" CssClass="form-control appended-form-control"
                                            OnTextChanged="ddlsearchkeywordsfilter_TextChanged" Placeholder="Description (type min three character)"></asp:TextBox>
                                        <div class="input-group-append-overlay">
                                            <span class="input-group-text"><i class="fas fa-search"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div>
                                <b>
                                    <asp:LinkButton ID="totoalmore" runat="server" CssClass="pull-right" OnClick="totoalmore_Click"></asp:LinkButton></b>
                            </div>
                            <div class="clearfix">
                            </div>
                            <br />
                            <div class="row mx-n2">
                                <asp:DataList runat="server" ID="dlproduct" RepeatColumns="3" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemCommand="dlproduct_ItemCommand" OnItemDataBound="dlproduct_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="">
                                            <div class="card product-card" style="box-shadow: 0 0.3rem 1.525rem -0.375rem rgba(0, 0, 0, 0.1);">
                                                <a class="card-img-top d-block overflow-hidden" href="#" style="text-align: center;">
                                                    <img src='<%#Eval("TopImages") %>' alt="Product" style="max-width: 100%; width: 50%; height: 90px;">
                                                </a>
                                                <div class="card-body py-2" style="height: 160px;">
                                                    <b><a class="product-meta d-block font-size-xs pb-1" href="#" style="color: #6915cf; font-size: 16px!important;"><%#Eval("CompanyName") %></a></b>
                                                    <h3 class="product-title font-size-sm"><a href="#" title='<%#Eval("ProductDescription") %>'>
                                                        <%# Eval("ProductDescription").ToString().Length > 25? (Eval("ProductDescription") as string).Substring(0,25) + ".." : Eval("ProductDescription")  %>
                                                    </a></h3>
                                                    <table class="table" style="font-size: 14px;">
                                                        <tbody>
                                                            <tr runat="server" visible="false">
                                                                <td style="padding: 8px;">NSN Group</td>
                                                                <td style="padding: 8px;" class="text-right" title='<%#Eval("NSNGroup") %>'><%# Eval("NSNGroup").ToString().Length > 15? (Eval("NSNGroup") as string).Substring(0,15) + ".." : Eval("NSNGroup")  %>
                                                                    <asp:HiddenField ID="hfrole" runat="server" Value='<%#Eval("Role") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="padding: 8px; font-size: 12px;">Nato Supply Code/Group/Class :-</td>

                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="padding: 8px; font-size: 12px; font-weight: 900; border-top: 0px;"
                                                                    title='<%#Eval("NSNGroup") %> / <%#Eval("NSNGroupClass") %>'>

                                                                    <%#Eval("NSCCode") %> / <%# Eval("NSNGroup").ToString().Length > 15? (Eval("NSNGroup") as string).Substring(0,15) + ".." : Eval("NSNGroup")  %> / <%# Eval("NSNGroupClass")  %> 
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td colspan="2" style="padding: 8px; font-size: 12px;">Industry Domain :-</td>

                                                            </tr>
                                                            <tr runat="server" visible="false">

                                                                <td colspan="2" style="padding: 8px; font-size: 12px; font-weight: 900; border-top: 0px;"
                                                                    title='<%#Eval("ProdIndustryDoamin") %>'><%# Eval("ProdIndustryDoamin")  %> / <%#Eval("ProdIndustrySubDomain") %>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td style="padding: 8px;">Quantity</td>
                                                                <td style="padding: 8px;" class="text-right"><%#Eval("EstimateQu") %></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="text-center">
                                                    <asp:LinkButton ID="lbview" runat="server" class="nav-link-style font-size-ms" CommandName="View" CommandArgument='<%#Eval("ProductRefNo") %>'>                                                
                                                    <i class="fas fa-eye align-middle mr-1"></i>
                                                    More Detail
                                                    </asp:LinkButton>
                                                </div>
                                                <asp:Button runat="server" ID="lbaddcart" class="btn btn-sm btn-block" Text="Add to show intrest"
                                                    CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="addcart"></asp:Button>
                                                <asp:HiddenField ID="hfr" runat="server" Value='<%#Eval("ProductRefNo") %>' />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div class="clearfix"></div>
                            <nav class="d-flex justify-content-between pt-2" aria-label="Page navigation">
                                <ul class="pagination">
                                    <li class="page-item">
                                        <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" class="page-link" OnClick="lnkbtnPgPrevious_Click"><i class="fas fa-chevron-left mr-2"></i>Prev</asp:LinkButton>
                                    </li>
                                </ul>
                                <ul class="pagination">
                                    <asp:TextBox ID="txtpageno" runat="server" AutoPostBack="true" ToolTip="Enter No (Number could not be 0 or either -1)" CssClass="form-control"
                                        Placeholder="Enter PageNo" OnTextChanged="btngoto_Click"></asp:TextBox>
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
                                <b>
                                    <asp:Label ID="lbltotal" runat="server" CssClass="pull-right"></asp:Label></b>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-quick-view modal fade" id="ProductCompany" tabindex="-1">
                    <div class="modal-dialog modal-xl" style="max-width: 800px!important; z-index: 9999999999;">
                        <div class="modal-content">
                            <div class="modal-header modelhead">
                                <h4 class="modal-title product-title" style="font-size: 25px;">Item Detail
                                </h4>
                                <button class="close" style="padding-right: 45px;" type="button" data-dismiss="modal" aria-label="Close">
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
                                                        <h3 class="accordion-heading mb-2"><a class="collapsed" href="#shoes" role="button" data-toggle="collapse" aria-expanded="false" aria-controls="shoes">Item Description 
                      <span class="accordion-indicator iconupanddown"><i class="fas fa-chevron-up"></i></span>
                                                        </a></h3>
                                                    </div>
                                                    <div class="collapse" id="shoes" data-parent="#shop-categories">
                                                        <div class="card-body card-custom ">
                                                            <h6 class="tablemidhead">DPSU's & OFB Details</h6>
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr>
                                                                        <th scope="row">DPSU's & OFB:	</th>
                                                                        <td>
                                                                            <asp:Label ID="lblcompname" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Division/Plant:	</th>
                                                                        <td>
                                                                            <asp:Label ID="lbldiviname" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Unit:	</th>
                                                                        <td>
                                                                            <asp:Label ID="lblunitnamepro" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <h6 class="tablemidhead">Item Description</h6>
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr>
                                                                        <th scope="row">Item Id</th>
                                                                        <td>
                                                                            <asp:Label ID="lblrefnoview" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Item Name</th>
                                                                        <td>
                                                                            <asp:Label ID="lblproductdescription" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Part Number</th>
                                                                        <td>
                                                                            <asp:Label ID="lbldpsupartno" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">HSN Code (8-digit)</th>
                                                                        <td>
                                                                            <asp:Label ID="lblhsncode8digit" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">NATO SUPPLY GROUP</th>
                                                                        <td>
                                                                            <asp:Label ID="lblnsngroup" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">NATO SUPPLY CLASS</th>
                                                                        <td>
                                                                            <asp:Label ID="lblnsngroupclass" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">CLASS CODE</th>
                                                                        <td>
                                                                            <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">INDUSTRY DOMAIN</th>
                                                                        <td>
                                                                            <asp:Label ID="prodIndustryDomain" runat="server" Text=""></asp:Label>
                                                                            /
                                                                            <asp:Label ID="ProdIndusSubDomain" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <%--<h6 class="tablemidhead">Imported During Last 3 years</h6>--%>
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr>
                                                                        <th scope="row">Imported During Last 3 Years</th>
                                                                        <td>
                                                                            <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>

                                                                        <td colspan="2" style="border-top: 0px;">
                                                                            <asp:GridView ID="gvestimatequanold" runat="server" AutoGenerateColumns="false" Class="table table-responsive table-bordered">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Year" DataField="FYear" />
                                                                                    <asp:BoundField HeaderText="Estimated Quantity" DataField="EstimatedQty" />
                                                                                    <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                                    <asp:BoundField HeaderText="Estimated/Last Purchase Price(In Rs)" DataField="EstimatedPrice" />
                                                                                </Columns>

                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2"><a class="collapsed" href="#ItemSpecification" role="button" data-toggle="collapse" aria-expanded="false" aria-controls="shoes">Item Specification
                      <span class="accordion-indicator iconupanddown"><i class="fas fa-chevron-up"></i></span>
                                                        </a></h3>
                                                    </div>
                                                    <div class="collapse" id="ItemSpecification" data-parent="#shop-categories">
                                                        <div class="card-body card-custom ">
                                                            <table class="table mb-2">
                                                                <tbody>

                                                                    <tr>
                                                                        <th scope="row">Document related to item</th>
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
                                                                                            <a href='<%#Eval("ImageName","0:http://srijandefence.gov.in/Upload/") %>' target="_blank" class="fa fa-download"></a>
                                                                                            <span data-toggle="tooltip" class="fa fa-question" title="Click on icon for downloaf"></span>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Item Image</th>
                                                                        <td>
                                                                            <asp:DataList ID="dlimage" runat="server" RepeatColumns="4" Visible="true" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                <ItemTemplate>
                                                                                    <div class="col-sm-3">
                                                                                        <a data-fancybox="Prodgridviewgellry" href='<%#Eval("[ImageName]") %>'>
                                                                                            <asp:Image ID="imgprodimage" runat="server" CssClass="img-responsive img-container" Height="90px" Width="110px" src='<%#Eval("[ImageName]") %>' />
                                                                                        </a>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:DataList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Item Specification</th>
                                                                        <td>
                                                                            <asp:Label ID="lblitemspecification" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Features & Details</th>
                                                                        <td>
                                                                            <asp:Label ID="lblfeaturesanddetail" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Item Information</th>
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
                                                                    <tr>
                                                                        <th scope="row">Additional Information</th>
                                                                        <td>
                                                                            <asp:Label ID="lbladditionalinfo" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                    <div class="card-header">
                                                        <h3 class="accordion-heading mb-2"><a class="collapsed" href="#Estimated" role="button" data-toggle="collapse" aria-expanded="false" aria-controls="shoes">Estimated Procurment Quantity details & Contact
                      <span class="accordion-indicator iconupanddown"><i class="fas fa-chevron-up"></i></span>
                                                        </a></h3>
                                                    </div>
                                                    <div class="collapse" id="Estimated" data-parent="#shop-categories">
                                                        <div class="card-body card-custom ">
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr>
                                                                        <th scope="row">Indigenization Category</th>
                                                                        <td>
                                                                            <asp:Label ID="lblindicate" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <h6 class="tablemidhead">Status of Indigenization</h6>
                                                                    <tr>
                                                                        <th scope="row">EoI/RFP</th>
                                                                        <td>
                                                                            <asp:Label ID="lbleoirep" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Link</th>
                                                                        <td>
                                                                            <asp:Label ID="lbleoilink" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">PROCURMENT CATEGORY REMARK</th>
                                                                        <td>
                                                                            <asp:Label ID="lblprocremarks" runat="server" Text=""></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <th scope="row">Estimate Quantity</th>
                                                                        <td>
                                                                            <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="Year" HeaderText="FYear" />
                                                                                    <asp:BoundField DataField="EstimatedQty" HeaderText="Estimated Quantity" />
                                                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price/Last Purchase Price (in Rs)" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <h6 class="tablemidhead">Contact Details</h6>
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr>
                                                                        <th scope="row">Employee Name</th>
                                                                        <td>
                                                                            <asp:Label ID="lblempname" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Designation</th>
                                                                        <td>
                                                                            <asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">E-Mail ID</th>
                                                                        <td>
                                                                            <asp:Label ID="lblemailidpro" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Mobile Number</th>
                                                                        <td>
                                                                            <asp:Label ID="lblmobilenumber" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Phone Number</th>
                                                                        <td>
                                                                            <asp:Label ID="lblphonenumber" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th scope="row">Fax</th>
                                                                        <td>
                                                                            <asp:Label ID="lblfaxpro" runat="server" Text=""></asp:Label>
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
                            <div class="clearfix mt10"></div>
                            <div class="modal-footer">
                                <input id="btnprint" type="button" onclick="PrintDiv()" style="width: 70px;" class="btn btn-primary  pull-right" value="Print" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="divCompany" role="dialog">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header modal-header1">

                                <h6 class="modal-title">Item Uploded Detail</h6>
                                <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <asp:GridView ID="gvPrdoct" runat="server" Width="100%" AutoGenerateColumns="false" Class="table  table-hover">
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
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lnkbtnPgPrevious" />
                <asp:PostBackTrigger ControlID="txtpageno" />
                <asp:PostBackTrigger ControlID="lnkbtnPgNext" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="update">
            <ProgressTemplate>
                <div class="overlay-progress">
                    <div class="custom-progress-bar blue stripes">
                        <span></span>
                        <p style="margin-left: 200px;">Processing</p>
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
                    </p>
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
    </form>

</body>
</html>
