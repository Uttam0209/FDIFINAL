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
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
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
                        <asp:UpdatePanel runat="server" ID="updrop">
                            <ContentTemplate>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="clearfix"></div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-wraper" style="overflow-x: auto;">
                                    <asp:GridView ID="gvproduct" runat="server" Width="100%" Class="commonAjaxTbl master-company-table ViewProductTable table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                        OnPageIndexChanging="OnPageIndexChanging" PageSize="25" AllowSorting="true" OnSorting="OnSorting" OnRowCommand="gvproduct_RowCommand">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="-" />
                                            <asp:BoundField DataField="FactoryName" HeaderText="Division" NullDisplayText="-" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" NullDisplayText="-" />
                                            <asp:TemplateField HeaderText="Product Reference No." Visible="False">
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
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DPSUPartNumber" Visible="false">
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
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
            </div>
            <div class="modal fade" id="changePass" role="dialog">
                <div class="modal-dialog" style="width: 1200px; z-index: 9999999999;">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Product Detail</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body sideBg">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="faq-secion product-view">
                                            <div class="accordion" id="accordion">
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h2>Description 
                                                            
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
                                                                                    <td>Product Refrence No:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblprodrefno" runat="server" Text=""></asp:Label></td>
                                                                                </tr>

                                                                            </table>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <table>
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
                                                                                    <td>Product Description:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblproductdescription" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <table>
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
                                                                                    <td>HSN Code:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblhsncode" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>End User Part Number:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblenduserpartno" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>End User:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblenduser" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <table>
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
                                                                                    <td>Product Already Indeginized</td>
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
                                                                                    <td>Is Product Imported</td>
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
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq2" aria-expanded="false" aria-controls="faq2">Product Specification
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>
                                                    <div id="faq2" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div runat="server" id="itemdocument">
                                                                        <div class="col-sm-4">
                                                                            <label>
                                                                            Document related to item
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <a href="#" target="_blank" runat="server" id="a_downitem" class="fa fa-download"></a>
                                                                            <span data-toggle="tooltip" class="fa fa-question" title="Click on icon for downloaf"></span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="clearfix"></div>
                                                                    <div class="col-sm-3">
                                                                        <label>Product Image</label>
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:DataList ID="dlimage" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                            <ItemTemplate>
                                                                                <div class="col-sm-3">
                                                                                    <asp:Image ID="imgprodimage" runat="server" Height="90px" Width="110px" src='<%#Eval("[ImageName]") %>' />
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </div>
                                                                    <div class="clearfix"></div>
                                                                    <div class="col-sm-3">
                                                                        <label>Features & Details</label>
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:Label ID="lblfeaturesanddetail" runat="server" Text=""></asp:Label>
                                                                    </div>

                                                                    <div class="clearfix"></div>
                                                                    <div class="col-sm-3">Product Information</div>
                                                                    <div class="col-sm-9">
                                                                        <asp:GridView ID="gvProdInfo" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="NameOfSpec" HeaderText="Name of Specification" />
                                                                                <asp:BoundField DataField="Value" HeaderText="Value " />
                                                                                <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                            </Columns>

                                                                        </asp:GridView>
                                                                    </div>
                                                                    <div class="clearfix"></div>
                                                                    <div class="col-sm-3">Additional Information</div>
                                                                    <div class="col-sm-9">
                                                                        <asp:Label ID="lbladditionalinfo" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq3" aria-expanded="false" aria-controls="faq3">Estimated Quantity & Price
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>
                                                    <div id="faq3" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="col-sm-4">Estimate Quantity or Price</div>
                                                                    <div class="col-sm-8">
                                                                        <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="FYear" HeaderText="Year" />
                                                                                <asp:BoundField DataField="EstimatedQty" HeaderText="Estimated Quantity" />
                                                                                <asp:BoundField DataField="Unit" HeaderText="Measuring Unit" />
                                                                                <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price / LPP" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                    <div class="clearfix"></div>
                                                                    <div class="col-md-4">
                                                                        PROCURMENT CATEGORY
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:Label ID="lblpurposeofprocurement" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                    <div class="clearfix"></div>
                                                                    <div class="col-md-4">
                                                                        <td>
                                                                        PROCURMENT CATEGORY REMARK
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:Label ID="lblprocremarks" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq4" aria-expanded="false" aria-controls="faq4">Testing & Certification
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>
                                                    <div id="faq4" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>QA Agency:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblqaagency" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblremarks" runat="server" Text=""></asp:Label></td>
                                                                        </tr>

                                                                    </table>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Testing:</td>
                                                                            <td>
                                                                                <asp:Label ID="lbltesting" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>
                                                                                <asp:Label ID="lbltestingremarks" runat="server" Text=""></asp:Label></td>
                                                                        </tr>

                                                                    </table>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Certification:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblcertification" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblcertificationremarks" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq6" aria-expanded="false" aria-controls="faq6">Technical & Financial Support
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>
                                                    <div id="faq6" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Technical Support provided by DPSU:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblsupportprovidedbydpsu" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblremarksdpsu" runat="server" Text=""></asp:Label></td>
                                                                        </tr>

                                                                    </table>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Financial Support provided by DPSU:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblfinancial" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblfinancialRemark" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq9" aria-expanded="false" aria-controls="faq9">Tender
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>

                                                    <div id="faq9" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Tender Status:</td>
                                                                            <td>
                                                                                <asp:Label ID="lbltenderstatus" runat="server" Text=""></asp:Label></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Tender Submission:</td>
                                                                            <td>
                                                                                <asp:Label ID="lbltendersubmission" runat="server" Text=""></asp:Label></td>
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
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                        </form>
                    </div>
                </div>
            </div>
            </label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up">
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
</asp:Content>
