<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ViewProductFilter.aspx.cs" Inherits="Admin_ViewProductFilter" ViewStateEncryptionMode="Always" %>

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
        table, th, td {
            border: 1px solid black;
            padding: 5px;
        }

        table {
            border-spacing: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="site-holder">
            <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
            <nav class="navbar" role="navigation">
                <div class="navbar-header">
                    <a class="navbar-brand" href="javascript:void(0)"><i class="fa fa-list btn-nav-toggle-responsive text-white"></i>
                        <span class="logo" title="Department of Defense Product">MOD</span>
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
                            <div class="common-filter-box">
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
                            <div class="common-filter-box" runat="server" visible="false">
                                <h3>Indigenized</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlisindezinized" runat="server" AutoPostBack="true" Style="margin-top: 5px;" CssClass="form-control" OnSelectedIndexChanged="ddlisindezinized_SelectedIndexChanged">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" visible="false">
                                <h3>Declaration </h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddldeclaration" runat="server" AutoPostBack="true" Style="margin-top: 5px;" CssClass="form-control" OnSelectedIndexChanged="ddldeclaration_SelectedIndexChanged">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="common-filter-box" runat="server" visible="false">
                                <h3>Imported during last 5 years</h3>
                                <div class="custom-control custom-checkbox">
                                    <asp:DropDownList ID="ddlimported" runat="server" AutoPostBack="true" Style="margin-top: 5px;" CssClass="form-control" OnSelectedIndexChanged="ddlimported_SelectedIndexChanged">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
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
                                    </div>
                                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                                    <div class="addfdi">
                                        <asp:HiddenField runat="server" ID="hfmtype" />
                                        <asp:HiddenField runat="server" ID="hfmref" />
                                        <div class="col-md-12">
                                            <div class="table-responsive" id="divProductGrid" runat="server" visible="False" style="overflow-x: auto;">
                                                <div id="divTotalNumber" class="text-center" style="font-size: 16px; margin-top: 5px;" runat="server">
                                                    <asp:Label ID="lbltotal" runat="server" CssClass="lable label-info " Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div runat="server" id="divclearfilorexceldown" visible="false">
                                                    <asp:LinkButton ID="lbclearfilter" runat="server" Text="  Clear Filter" Visible="false" CssClass="pull-right mr10 fa fa-times btn btn-danger" OnClick="lbclearfilter_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldownloadexcel" runat="server" ToolTip="Download Excel" Class=" fa fa-file-excel btn btn-primary"
                                                        Text="  Download Excel" OnClick="lbldownloadexcel_Click"></asp:LinkButton>
                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div class="row">
                                                    <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                                                    <div runat="server" id="divpageindex1" visible="false">
                                                        <div class="col-sm-9">
                                                            <div class="col-sm-4">
                                                                <asp:LinkButton ID="lnkbtnPgPre" runat="server" CssClass="btn btn-info  btn-sm"
                                                                    OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-4" style="display: flex">
                                                                <asp:TextBox runat="server" ID="txtsea" CssClass="form-control btn-defualt text-center red" AutoCompleteType="Search" Placeholder="Please enter no of page"></asp:TextBox>
                                                                <asp:LinkButton ID="btngo" runat="server" CssClass="btn btn-primary" OnClick="btngoto_Click">Go to</asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:LinkButton ID="lnkbtnne" runat="server" CssClass="btn btn-info btn-sm pull-right" Style="margin-right: 3px;"
                                                                    OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <div class="pull-right">
                                                                <asp:Label ID="lblcountpgindex" runat="server" class="btn btn-primary text-center" Text=""></asp:Label>
                                                                <div class="clearfix mt10"></div>
                                                                <div class="row">
                                                                    <p style="margin-right: 30px; font-weight: bold; color: red; margin-bottom: -20px;">Note : Click on any column to Sort</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix mt10"></div>
                                                    <!-----------------------------------------end code for page indexing----------------------------------------------------->
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="gvproduct" runat="server" Width="100%" Class="table table-bordered table-striped table-hover manage-user gvapprovereject"
                                                            Style="overflow: scroll;" AutoGenerateColumns="false" AllowSorting="true" OnSorting="OnSorting" OnRowCommand="gvproduct_RowCommand" OnRowCreated="gvproduct_RowCreated" OnRowDataBound="gvproduct_RowDataBound">
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
                                                                        <%--<asp:LinkButton ID="lbPrintView" runat="server" CssClass="fa fa-print" CommandName="Viewmprint" CommandArgument='<%#Eval("ProductRefNo") %>'></asp:LinkButton>--%>
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
                                                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" NullDisplayText="-" SortExpression="ItemCode" />
                                                                <asp:BoundField DataField="ProdIndustryDoamin" HeaderText="Industry" Visible="false" NullDisplayText="" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="ProdIndustryDoamin" />
                                                                <asp:BoundField DataField="EstimateQu" HeaderText="Estimated Quantity" NullDisplayText="" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="EstimateQu" />
                                                                <asp:BoundField DataField="EstimatePrice" HeaderText="Estimated Price" Visible="false" NullDisplayText="" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="EstimatePrice" />
                                                                <asp:BoundField DataField="DPSUPartNumber" HeaderText="Part Number" Visible="false" NullDisplayText="" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="DPSUPartNumber" />
                                                                <asp:BoundField DataField="NodalOfficerEmail" HeaderText="Email" Visible="false" NullDisplayText="" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="NodalOfficerEmail" />
                                                                 <asp:BoundField DataField="LastUpdated" HeaderText="Last Updated" NullDisplayText="" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="LastUpdated" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="clearfix mt10"></div>
                                                    <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                                                    <div runat="server" id="divpageindex" visible="false">
                                                        <div class="col-sm-9">
                                                            <div class="col-sm-4">
                                                                <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-info  btn-sm"
                                                                    OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-4" style="display: flex">
                                                                <asp:TextBox runat="server" ID="txtpageno" CssClass="form-control btn-defualt text-center red" AutoCompleteType="Search" Placeholder="Please enter no of page"></asp:TextBox>
                                                                <asp:LinkButton ID="btngoto" runat="server" CssClass="btn btn-primary" OnClick="btngoto_Click">Go to</asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-4">
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
                                                    <div class="clearfix mt10"></div>
                                                    <!-----------------------------------------end code for page indexing----------------------------------------------------->
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix mt10"></div>
                                        <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
                                    </div>
                                    <div class="modal fade" id="ProductCompany" role="dialog">
                                        <div class="modal-dialog" style="width: 1200px; z-index: 9999999999;">
                                            <!-- Modal content-->
                                            <div class="modal-content">
                                                <div class="modal-header modal-header1">
                                                    <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                                    <h4 class="modal-title">Import Item Detail</h4>
                                                </div>
                                                <div class="form-horizontal changepassword">
                                                    <div class="modal-body">
                                                        <div class="col-md-12">
                                                            <div class="faq-secion product-view">
                                                                <div class="accordion" id="accordion">
                                                                    <div id="printarea">
                                                                        <div class="card">
                                                                            <div class="card-header">
                                                                                <h2 data-toggle="collapse" data-parent="#accordion" data-target="#faq1" aria-expanded="false" aria-controls="faq1">Import Item Description  
                                                            <i class="fa fa-minus pull-right"></i>
                                                                                </h2>
                                                                            </div>
                                                                            <div id="faq1" class="collapse in" aria-labelledby="headingOne">
                                                                                <div class="card-body">
                                                                                    <ul>
                                                                                        <li>
                                                                                            <div class="row two-col">
                                                                                                <div class="col-md-12">
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td colspan="2" style="background-color: beige; font-weight: 900;">DPSU's & OFB Details</td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>DPSU's & OFB:</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblcompname" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>Division/Plant:</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lbldiviname" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>Unit:</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblunitnamepro" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2" style="background-color: beige; font-weight: 900;">Item Description</td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>Item Id (Portal)</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblrefnoview" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>Item Name</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblproductdescription" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>Part Number:</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lbldpsupartno" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>HSN Code (8-digit)</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblhsncode8digit" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>Industry Domain:</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="prodIndustryDomain" runat="server" Text=""></asp:Label>
                                                                                                                / 
                                                                                                                <asp:Label ID="ProdIndusSubDomain" runat="server" Text=""></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr runat="server" visible="false">
                                                                                                            <td>Search keywords</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblsearchkeywords" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2" style="background-color: beige; font-weight: 900;">Iteam Classification (NATO Group & Class)</td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                           <td>NATO Supply Group:</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblnsngroup" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>NATO Supply Class:</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblnsngroupclass" runat="server" Text=""></asp:Label></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                           <td>Iteam Code:</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>

                                                                                                        <tr>
                                                                                                            <td>Imported, Last 3 years</td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                                                                                (import value during last 3 year &nbsp;<asp:Label ID="lblvalueimport" runat="server" Text="0"></asp:Label>&nbsp;lakhs )
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2">
                                                                                                                <asp:GridView ID="gvestimatequanold" runat="server" AutoGenerateColumns="false" Class="table table-responsive table-hover table-bordered">
                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField HeaderText="Year" DataField="FYear" />
                                                                                                                        <asp:BoundField HeaderText="Imported Quantity" DataField="EstimatedQty" />
                                                                                                                        <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                                                                        <asp:BoundField HeaderText="Imported value in Rs lakh (Qty*Price)" DataField="EstimatedPrice" />
                                                                                                                    </Columns>
                                                                                                                </asp:GridView>
                                                                                                            </td>
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
                                                                                                    <td>Item Document</td>
                                                                                                    <td>
                                                                                                        <asp:GridView runat="server" ID="gvpdf" AutoGenerateColumns="false" Class="table table-responsive table-hover">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="View or Download">
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
                                                                                                <tr runat="server" visible="false">
                                                                                                    <td>Item Specification</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblitemspecification" runat="server" Text=""></asp:Label></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Item Features & Details</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblfeaturesanddetail" runat="server" Text=""></asp:Label></td>
                                                                                                </tr>
                                                                                                <tr runat="server" visible="false">
                                                                                                    <td>Item Information
                                                                                                    </td>
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
                                                                                                <tr runat="server" visible="false">
                                                                                                    <td>Additional Information</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbladditionalinfo" runat="server" Text=""></asp:Label></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="card" runat="server" id="Div1">
                                                                            <div class="card-header">
                                                                                <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq3" aria-expanded="false" aria-controls="faq3">Import Quantity & Contact
                                                            <i class="fa fa-plus pull-right"></i>
                                                                                </h2>
                                                                            </div>
                                                                            <div id="faq3" class="collapse">
                                                                                <div class="card-body">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td colspan="2" style="background-color: beige; font-weight: 900;">Status of Indigenization</td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Indigenization Category</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblindicate" runat="server" Text=""></asp:Label></td>
                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <td>EoI/RFP Status</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbleoirep" runat="server" Text=""></asp:Label></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Link</td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lbleoilink" runat="server" Text=""></asp:Label></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                     <td>Import Quantity</td>
                                                                                                    <td>
                                                                                                        <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="FYear" HeaderText="Year" />
                                                                                                                <asp:BoundField DataField="EstimatedQty" HeaderText="Import Quantity" />
                                                                                                                <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                                                <asp:BoundField DataField="EstimatedPrice" HeaderText="Import value in Rs lakh (Qty*Price)" />
                                                                                                            </Columns>
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="2" style="background-color: beige; font-weight: 900;">Contact Detail</td>
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
                                                    <div class="clearfix mt10"></div>
                                                    <div class="modal-footer">
                                                        <input id="btnprint" type="button" onclick="PrintDiv()" class="btn btn-primary" value="Print" />
                                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                    </div>
                                                </div>
                                            </div>
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










