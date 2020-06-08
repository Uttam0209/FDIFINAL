<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProduct.aspx.cs" Inherits="Admin_ViewProduct" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#divfactoryshow').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup2() {
            $('#divunitshow').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup3() {
            $('#divmodel2').modal('show');
        }
    </script>
    <style>
        .dot {
            height: 20px;
            width: 20px;
            background-color: #eee;
            border-radius: 50%;
            display: inline-block;
        }

        .dot1 {
            height: 20px;
            width: 20px;
            background-color: #f00;
            border-radius: 50%;
            display: inline-block;
        }

        .dot2 {
            height: 20px;
            width: 20px;
            background-color: #106419;
            border-radius: 50%;
            display: inline-block;
        }
    </style>
    <style>
        table, th, td {
            border: 1px solid black;
            padding: 5px;
        }

        table {
            border-spacing: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfcomprefno" runat="server" />
            <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
            <asp:HiddenField ID="hidType" runat="server" />
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                        <div class="col-md-12">
                            <div class="clearfix"></div>
                            <div style="margin-top: 5px;">
                                <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>

                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <div class="clearfix"></div>

                    <div id="Div3">
                        <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" Visible="False" CssClass="btn btn-primary pull-right" OnClick="btnAddProduct_Click" />
                    </div>
                    <div class="clearfix"></div>
                    <div class="row">

                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Select Company</label>
                                <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblselectdivison">
                            <div class="form-group">
                                <label>Select Division/Palnt</label>
                                <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblselectunit">
                            <div class="form-group">
                                <label>Select Unit</label>
                                <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="clearfix"></div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive" style="overflow-x: auto;" id="divproductgridview" runat="server">
                                    <asp:Label ID="lbltot" runat="server" CssClass="text-center"></asp:Label>
                                    <div class="clearfix mt10"></div>
                                    <div class="row">
                                        <div class="col-sm-1 text-center">
                                            <span class="dot text-center">
                                                <p style="margin-left: 30px;">Pending</p>
                                            </span>
                                        </div>
                                        <div class="col-sm-1 text-center">
                                            <span class="dot2 text-center">
                                                <p style="margin-left: 30px;">Approved</p>
                                            </span>
                                        </div>
                                        <div class="col-sm-1 text-center">
                                            <span class="dot1 text-center">
                                                <p style="margin-left: 30px;">Disapproved</p>
                                            </span>
                                        </div>

                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <asp:GridView ID="gvproductItem" runat="server" Width="100%" Class="table table-bordered table-wraper table-hover manage-user"
                                        AutoGenerateColumns="false" OnRowCommand="gvproductItem_RowCommand" OnRowCreated="gvproductItem_RowCreated" OnRowDataBound="gvproductItem_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:HiddenField ID="hfrole" runat="server" Value='<%#Eval("Role") %>' />
                                                    <asp:HiddenField ID="hfcomprefno" runat="server" Value='<%#Eval("CompanyRefNo") %>' />
                                                    <asp:HiddenField ID="hfdivisionrefno" runat="server" Value='<%#Eval("FactoryRefNo") %>' />
                                                    <asp:HiddenField ID="hfunitrefno" runat="server" Value='<%#Eval("UnitRefNo") %>' />
                                                    <asp:HiddenField ID="hfisaaproved" runat="server" Value='<%#Eval("IsApproved") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="-" />
                                            <asp:BoundField DataField="FactoryName" HeaderText="Division" NullDisplayText="-" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" NullDisplayText="-" />
                                            <asp:TemplateField HeaderText="Item Reference No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("ProductRefNo") %>' NullDisplayText="#" SortExpression="ProductRefNo"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProductDescription" HeaderText="Description" ItemStyle-Wrap="true" ItemStyle-Width="150" NullDisplayText="#" SortExpression="Description" />
                                            <asp:BoundField DataField="CompanyRefNo" HeaderText="Company Reference No" Visible="false" NullDisplayText="#" SortExpression="CompanyRefNo" />
                                            <asp:TemplateField HeaderText="OEM PartNumber" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblcompanyrole" Text='<%#Eval("OEMPartNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Updated" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblLastUpdated" Text='<%#Eval("LastUpdated") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Part Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("DPSUPartNumber") %>' NullDisplayText="#" SortExpression="DPSUPartNumber"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProdIndustryDoamin" HeaderText="Industry" NullDisplayText="" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="ProdIndustryDoamin" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("ProductRefNo") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbledit" runat="server" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("ProductRefNo") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldel" runat="server" CssClass="fa fa-trash" Visible="False" CommandName="DeleteComp" OnClientClick="return confirm('Are you sure you want to delete this product?');" CommandArgument='<%#Eval("ProductRefNo") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
                                                <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn  btn-info btn-sm pull-right" Style="margin-right: 3px;"
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
                    </form>
                </div>
                <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
            </div>
            <div class="modal fade" id="changePass" role="dialog">
                <div class="modal-dialog" style="width: 1200px; z-index: 9999999999;">
                    <asp:UpdatePanel ID="upn" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header modal-header1">
                                    <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Item Detail</h4>
                                </div>
                                <form class="form-horizontal changepassword" role="form">
                                    <div class="modal-body">
                                        <asp:Panel ID="Panel1" runat="server" Visible="false">
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
                                                                                                <td>Item Id</td>
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
                                                                                                <td>NATO SUPPLY GROUP:</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblnsngroup" runat="server" Text=""></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>NATO SUPPLY CLASS:</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblnsngroupclass" runat="server" Text=""></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>CLASS ITEM:</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label>
                                                                                                </td>
                                                                                            </tr>
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
                                                                                                <td>INDUSTRY DOMAIN:</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="prodIndustryDomain" runat="server" Text=""></asp:Label>
                                                                                                    / 
                                                                                                                <asp:Label ID="ProdIndusSubDomain" runat="server" Text=""></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>Imported During Last 3 years</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    <asp:GridView ID="gvestimatequanold" runat="server" AutoGenerateColumns="false" Class="table table-responsive table-hover table-bordered">
                                                                                                        <Columns>
                                                                                                            <asp:BoundField HeaderText="Year" DataField="FYear" />
                                                                                                            <asp:BoundField HeaderText="Estimated Quantity" DataField="EstimatedQty" />
                                                                                                            <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                                                            <asp:BoundField HeaderText="Estimated/Last Purchase Price(In Rs)" DataField="EstimatedPrice" />
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
                                                                                        <td>Document related to item</td>
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
                                                                                    <tr>
                                                                                        <td>Item Specification</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblitemspecification" runat="server" Text=""></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Features & Details</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblfeaturesanddetail" runat="server" Text=""></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
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
                                                                                    <tr>
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
                                                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq3" aria-expanded="false" aria-controls="faq3">Estimated Procurment Quantity details & Contact
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
                                                                                            <asp:Label ID="lbleoist" runat="server" Text=""></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Link</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lbleoiurl" runat="server" Text=""></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>PROCURMENT CATEGORY REMARK</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblprocremarks" runat="server" Text=""></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Estimate Quantity</td>
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
                                        </asp:Panel>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </form>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnPgPrevious" />
            <asp:PostBackTrigger ControlID="btngoto" />
            <asp:PostBackTrigger ControlID="lnkbtnPgNext" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up">
        <ProgressTemplate>
            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p>Processing</p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type='text/javascript'>
        $(function () {
            $('#txtsearch').keyup(function () {
                if ($(this).val() == '') {
                    $('.enableOnInput').prop('disabled', true);
                } else {
                    $('.enableOnInput').prop('disabled', false);
                }
            });
        });
    </script>
</asp:Content>
