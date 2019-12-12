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
                                    <asp:GridView ID="gvproductItem" runat="server" Width="100%" Class="table table-bordered table-wraper table-hover manage-user"
                                        AutoGenerateColumns="false" OnRowCommand="gvproductItem_RowCommand" OnRowCreated="gvproductItem_RowCreated" OnRowDataBound="gvproductItem_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
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
                                            <asp:BoundField DataField="ProductDescription" HeaderText="Item Description" ItemStyle-Wrap="true" ItemStyle-Width="150" NullDisplayText="#" SortExpression="Description" />
                                            <asp:BoundField DataField="CompanyRefNo" HeaderText="Company Reference No" Visible="false" NullDisplayText="#" SortExpression="CompanyRefNo" />
                                            <asp:TemplateField HeaderText="OEM PartNumber" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblcompanyrole" Text='<%#Eval("OEMPartNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Updated">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblLastUpdated" Text='<%#Eval("LastUpdated") %>'></asp:Label>
                                                    <asp:HiddenField ID="hfrole" runat="server" Value='<%#Eval("Role") %>' />
                                                    <asp:HiddenField ID="hfcomprefno" runat="server" Value='<%#Eval("CompanyRefNo") %>' />
                                                    <asp:HiddenField ID="hfdivisionrefno" runat="server" Value='<%#Eval("FactoryRefNo") %>' />
                                                    <asp:HiddenField ID="hfunitrefno" runat="server" Value='<%#Eval("UnitRefNo") %>' />
                                                    <asp:HiddenField ID="hfisaaproved" runat="server" Value='<%#Eval("IsApproved") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DPSUPartNumber">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("DPSUPartNumber") %>' NullDisplayText="#" SortExpression="DPSUPartNumber"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                                                                                    <asp:Label ID="lblunitname" runat="server" Text=""></asp:Label></td>
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
                                                                                                    <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label>
                                                                                                    <asp:LinkButton ID="lblviwitem" runat="server" Visible="false" CssClass="fa fa-eye" Text="View" OnClick="lblviwitem_Click"></asp:LinkButton>
                                                                                                </td>
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
                                                                                                <td>Item Description:</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblproductdescription" runat="server" Text=""></asp:Label></td>
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
                                                                                    <div class="clearfix" style="margin-top: 10px;"></div>
                                                                                    <div class="row" runat="server" id="Panel2" visible="false">
                                                                                        <div class="col-md-12">
                                                                                            <div class="table-wraper table-responsive">
                                                                                                <asp:GridView ID="gvproditemdetail" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
           responsive no-wrap table-hover manage-user Grid"
                                                                                                    OnRowCreated="gvproditemdetail_RowCreated" AutoGenerateColumns="false">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="Sr.No">
                                                                                                            <ItemTemplate>
                                                                                                                <%#Container.DataItemIndex+1 %>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField runat="server" DataField="ProductRefNo" HeaderText="Reference No" />
                                                                                                        <asp:BoundField runat="server" DataField="MRC_TITLE" HeaderText="MRC Title" />
                                                                                                        <asp:BoundField runat="server" DataField="Remarks" HeaderText="Remarks" />
                                                                                                        <asp:BoundField runat="server" DataField="Remarks2" HeaderText="Detail Remarks" />
                                                                                                        <asp:BoundField runat="server" DataField="Remarks3" HeaderText="Mandatory" />
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <div class="clearfix mt10"></div>
                                                                                            </div>
                                                                                        </div>
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
                                                                                            <asp:DataList ID="dlimage" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                                <ItemTemplate>
                                                                                                    <div class="col-sm-3">
                                                                                                        <a data-fancybox="Prodgridviewgellry" href='<%#Eval("[ImageName]") %>'>
                                                                                                            <asp:Image ID="imgprodimage" runat="server" Height="90px" Width="110px" src='<%#Eval("[ImageName]") %>' />
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
                                                                                        <td>Item Information</td>
                                                                                        <td>&nbsp;</td>
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
                                                            <div class="card">
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
                                                                                                <asp:Label ID="lblemailid" runat="server" Text=""></asp:Label>
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
                                                                                                <asp:Label ID="lblfax" runat="server" Text=""></asp:Label>
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

</asp:Content>
