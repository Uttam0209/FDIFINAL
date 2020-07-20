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
                                                        <div class="simplebar-content">
                                                            <!-- Categories-->
                                                            <div class="widget widget-categories mb-4">
                                                                <div class="accordion mt-n1" id="shop-categories">
                                                                    <div id="printarea">
                                                                        <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                                            <div class="card-header">
                                                                                <h6 class="accordion-heading mb-2">
                                                                                    <a class="collapsed" href="#ItemSpecification" role="button" data-toggle="collapse"
                                                                                        aria-expanded="false" aria-controls="shoes">Item Specification <span class="accordion-indicator iconupanddown">
                                                                                            <i class="fas fa-chevron-up"></i></span></a>
                                                                                </h6>
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
                                                                        <div class="card" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                                            <div class="card-header">
                                                                                <h6 class="accordion-heading mb-2">
                                                                                    <a class="collapsed" href="#shoes" role="button" data-toggle="collapse" aria-expanded="false"
                                                                                        aria-controls="shoes">Item Description <span class="accordion-indicator iconupanddown">
                                                                                            <i class="fas fa-chevron-up"></i></span></a>
                                                                                </h6>
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
                                                                                <h6 class="accordion-heading mb-2">
                                                                                    <a class="collapsed" href="#Estimated" role="button" data-toggle="collapse" aria-expanded="false"
                                                                                        aria-controls="shoes">Import Value, Quantity <span class="accordion-indicator iconupanddown">
                                                                                            <i class="fas fa-chevron-up"></i></span></a>
                                                                                </h6>
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
                                                                                                <th scope="row">Indigenization Category
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
                                                                                <h6 class="accordion-heading mb-2">
                                                                                    <a class="collapsed" href="#AdditionalValue" role="button" data-toggle="collapse" aria-expanded="false"
                                                                                        aria-controls="shoes">Additional Details <span class="accordion-indicator iconupanddown">
                                                                                            <i class="fas fa-chevron-up"></i></span></a>
                                                                                </h6>
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
                                                                                                <td id="Td1" runat="server" visible="false">
                                                                                                    <asp:Label ID="lbldeclaration" runat="server" Text="No IPR issue, No violation of TOT agreement, No violation of Security Concern"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr runat="server" id="twentyfour" visible="false">
                                                                                                <th scope="row"></th>
                                                                                                <td id="Td2" runat="server" visible="false">
                                                                                                    <asp:Label ID="lblisshowgeneral" runat="server" Text=""></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr runat="server" id="Tr19" visible="false">
                                                                                                <th id="Th1" scope="row" runat="server" visible="false">Is Indigenised 
                                                                                                </th>
                                                                                                <td id="Td3" runat="server" visible="false">
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
                                                                                                <th scope="row">Indigenization Process started
                                                                                                </th>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblprocstart" runat="server" Text=""></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr runat="server" id="Tr25" visible="false">
                                                                                                <th scope="row">Indigenization Target Year
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
                                                    <div class="clearfix mt10"></div>
                                                    <div class="modal-footer">
                                                        <input id="btnprint" type="button" onclick="PrintDiv()" style="width: 70px;" class="btn btn-primary  pull-right"
                                                            value="Print" />
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










