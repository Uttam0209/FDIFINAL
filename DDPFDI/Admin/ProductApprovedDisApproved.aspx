<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductApprovedDisApproved.aspx.cs" Inherits="Admin_ProductApprovedDisApproved" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#updateitem').modal('show');
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Item Id (Portal)</label>
                                        <asp:TextBox runat="server" ID="txtserachitemidprotal" CssClass="form-control form-cascade-control" AutoPostBack="True" OnTextChanged="txtserachitemidprotal_TextChanged"></asp:TextBox>
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="clearfix mt10"></div>
                    <div class="text-center">
                        <asp:RadioButtonList ID="rbliststatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Flow" RepeatColumns="3" OnSelectedIndexChanged="rbliststatus_SelectedIndexChanged">
                            <asp:ListItem Value="A">Available for verification</asp:ListItem>
                            <asp:ListItem Value="Y" style="margin-left: 10px;">Approved</asp:ListItem>
                            <asp:ListItem Value="N" style="margin-left: 10px;">Disapproved</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="clearfix mt10"></div>
                    <div class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive" id="divproductgridview" runat="server">
                                    <asp:Label ID="lbltot" runat="server" CssClass="text-center"></asp:Label>
                                    <div class="clearfix mt10"></div>
                                    <div style="overflow:scroll;">
                                        <asp:GridView ID="gvproductItem" runat="server" Width="100%" Class="table table-bordered table-wraper table-hover manage-user"
                                            AutoGenerateColumns="false" OnRowCommand="gvproductItem_RowCommand" OnRowDataBound="gvproductItem_RowDataBound" OnRowCreated="gvproductItem_RowCreated">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProductRefNo" HeaderText="Item Id (Portal)" NullDisplayText="#" />
                                                <asp:BoundField DataField="NSNGroup" HeaderText="NATO Supply Group" NullDisplayText="#" />
                                                <asp:BoundField DataField="NSNGroupClass" HeaderText="NATO Supply Class" NullDisplayText="#" />
                                                <asp:BoundField DataField="ProdIndustryDoamin" HeaderText="Prod Indus Doamin" NullDisplayText="#" />
                                                <asp:BoundField DataField="ProdIndustrySubDomain" HeaderText="Prod Indus Sub Doamin" NullDisplayText="#" />
                                                <asp:BoundField DataField="ProductDescription" HeaderText="Item Description" ItemStyle-Wrap="true" ItemStyle-Width="150" NullDisplayText="#" />
                                                <asp:TemplateField HeaderText="DPSUPartNumber">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("DPSUPartNumber") %>' NullDisplayText="#"></asp:Label>
                                                        <asp:HiddenField ID="hfdivisionrefno" runat="server" Value='<%#Eval("FactoryRefNo") %>' />
                                                        <asp:HiddenField ID="hfunitrefno" runat="server" Value='<%#Eval("UnitRefNo") %>' />
                                                        <asp:Label ID="lblrefno" runat="server" Visible="false" Text='<%#Eval("ProductRefNo") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfrole" runat="server" Value='<%#Eval("Role") %>' />
                                                        <asp:HiddenField ID="hfcomprefno" runat="server" Value='<%#Eval("CompanyRefNo") %>' />
                                                        <asp:HiddenField ID="hfisaaproved" runat="server" Value='<%#Eval("IsApproved") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Approve">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblisapproved" runat="server" Text='<%#Eval("IsApproved") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("ProductRefNo") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="lbledit" runat="server" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("ProductRefNo") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
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
                    </div>
                    <div class="modal fade" id="changePass" role="dialog">
                        <div class="modal-dialog" style="width: 1200px; z-index: 9999999999;">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header modal-header1">
                                    <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Item Detail</h4>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-body">
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
                                                                                                <td>Item Id (Portal)</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblrefnoview" runat="server" Text=""></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr runat="server">
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
                                                                                            <tr runat="server" visible="false">
                                                                                                <td>Search keywords</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblsearchkeywords" runat="server" Text=""></asp:Label></td>
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
                                                                                                <td>ITEM CODE:</td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label>
                                                                                                </td>
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
                                                                                                            <asp:BoundField HeaderText="Imported Purchase Price per unit (in Rs)" DataField="EstimatedPrice" />
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
                                                                                        <td>Features & Details</td>
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
                                                                                            <asp:Label ID="lbleoirep" runat="server" Text=""></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Link</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lbleoilink" runat="server" Text=""></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Estimated price per unit (Rs)</td>
                                                                                        <td>
                                                                                            <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="FYear" HeaderText="Year" />
                                                                                                    <asp:BoundField DataField="EstimatedQty" HeaderText="Estimated Quantity" />
                                                                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Purchase Price per unit (in Rs)" />
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
                                            <div class="modal-footer">
                                                <asp:Panel ID="pancheck" runat="server" Visible="false">
                                                    <asp:TextBox ID="txtappdisappmssg" runat="server" CssClass="form-control" required="" Height="100px" TextMode="MultiLine" placeholder="Please enter details of changes you done in this item.">
                                                    </asp:TextBox>
                                                    <div class="clearfix mt10"></div>
                                                    <asp:LinkButton ID="btnapprove" runat="server" Text="Approve" CssClass="btn btn-success pull-left" OnClick="btnapprove_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="btndisapproved" runat="server" Text="Not Approve" CssClass="btn btn-danger pull-left" Style="marign-left: 10px;" OnClick="btndisapproved_Click"></asp:LinkButton>
                                                </asp:Panel>
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="updateitem" role="dialog">
                        <div class="modal-dialog" style="width: 750px; z-index: 9999999999;">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header modal-header1">
                                    <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Item Detail</h4>
                                </div>
                                <asp:UpdatePanel ID="upn" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-body">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        NSN GROUP <span class="mandatory">*</span>
                                                        <span data-toggle="tooltip" class="fa fa-question" title="Number in bracket indicates NSN Group"></span>
                                                    </label>
                                                    <asp:DropDownList ID="ddlnsn" runat="server" CssClass="form-control" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlnsn_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="clearfix mt5"></div>
                                                <div class="form-group">
                                                    <label>
                                                        NSN GROUP CLASS<span class="mandatory">*</span>
                                                        <span data-toggle="tooltip" class="fa fa-question" title="Number in bracket indicates NSN Group class"></span>
                                                    </label>
                                                    <asp:DropDownList ID="ddlnsnclass" runat="server" CssClass="form-control" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlnsnclass_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="clearfix mt5"></div>
                                                <div class="form-group">
                                                    <label>
                                                        Item Code</label>
                                                    <span data-toggle="tooltip" class="fa fa-question" title="Item code indicate item name code in NSN"></span>
                                                    <asp:DropDownList ID="ddlitemcode" runat="server" CssClass="form-control" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlitemcode_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="clearfix mt5"></div>
                                                <div class="form-group">
                                                    <div class="col-md-3 padding_0" style="margin-top: 30px;">
                                                        <label>Nato Stock Number (NSN)</label>
                                                    </div>
                                                    <div class="col-sm-3 padding_0">
                                                        <label style="font-size: 14px !important;">
                                                            NSC Code (4 digit)
                                                                        <span data-toggle="tooltip" class="fa fa-question" title="NSC Code = NSN Group (2 digit) + NSN Group Class (2 digit)"></span>
                                                        </label>
                                                        <asp:TextBox runat="server" ID="txtnsccode" ReadOnly="True" MaxLength="4" CssClass="form-cascade-control form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-6 padding_0">
                                                        <label>
                                                            NIIN Code (9-digit)
                                                                    <span data-toggle="tooltip" class="fa fa-question" title="Please enter if NIIN code is available"></span>
                                                        </label>
                                                        <asp:TextBox runat="server" ID="txtniincode" TabIndex="4" MaxLength="9" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Item brief description</label>
                                                    <span class="mandatory">* (Editable)</span>  <span data-toggle="tooltip" class="fa fa-question" title="If item description is not relevant, edit the item description."></span>
                                                    <asp:TextBox runat="server" ID="txtproductdescription" required="" Height="70px" MaxLength="250" TabIndex="5" class="form-control"></asp:TextBox>
                                                    <div class="clearfix" style="margin-top: 5px;"></div>
                                                    <span>(Max length 250 words only)</span>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:LinkButton ID="lblupdate" runat="server" Text="Update Item" TabIndex="6" CssClass="btn btn-success" OnClick="lblupdate_Click"></asp:LinkButton>
                                                <button type="button" class="btn btn-default" tabindex="7" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
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
