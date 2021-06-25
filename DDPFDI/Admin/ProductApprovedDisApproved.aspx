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
                                        <label>Select Division/Plant</label>
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
                                    <div style="overflow: scroll;">
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <div class="modal-content">
                                        <div class="modal-header modal-header1">
                                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Item Detail</h4>
                                        </div>
                                        <form class="form-horizontal changepassword" role="form">
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

                                                                                </tbody>
                                                                            </table>
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
                                            <div class="clearfix mt10">
                                            </div>
                                        </form>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
