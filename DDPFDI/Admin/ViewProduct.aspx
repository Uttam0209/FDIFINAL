﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProduct.aspx.cs" Inherits="Admin_ViewProduct" MasterPageFile="~/Admin/MasterPage.master" %>

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
                    </div>
                    <div class="clearfix"></div>
                    <div id="Div3">
                        <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" CssClass="btn btn-primary pull-right" OnClick="btnAddProduct_Click" />

                    </div>
                    <div class="clearfix"></div>
                    <div class="row">
                        <asp:UpdatePanel runat="server" ID="updrop">
                            <ContentTemplate>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Select Company</label>
                                        <asp:DropDownList runat="server" ID="ddlcompany" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" runat="server" id="lblselectdivison">
                                    <div class="form-group">
                                        <label>Select Division/Palnt</label>
                                        <asp:DropDownList runat="server" ID="ddldivision" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" runat="server" id="lblselectunit">
                                    <div class="form-group">
                                        <label>Select Unit</label>
                                        <asp:DropDownList runat="server" ID="ddlunit" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="clearfix"></div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-wraper">
                                    <asp:GridView ID="gvproduct" runat="server" Width="100%" Class="commonAjaxTbl master-company-table ViewProductTable table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                        OnPageIndexChanging="OnPageIndexChanging" PageSize="25" AllowSorting="true" OnSorting="OnSorting" OnRowCommand="gvproduct_RowCommand">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Reference No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("ProductRefNo") %>' NullDisplayText="#" SortExpression="ProductRefNo"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="#" SortExpression="Company" />--%>
                                            <asp:BoundField DataField="CompanyRefNo" HeaderText="Company Reference No" NullDisplayText="#" SortExpression="CompanyRefNo" />
                                            <asp:TemplateField HeaderText="OEM PartNumber">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblcompanyrole" Text='<%#Eval("OEMPartNumber") %>'></asp:Label>
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
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
            </div>
            <div class="modal fade" id="changePass" role="dialog">
                <div class="modal-dialog" style="width: 1000px; z-index: 9999999999;">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Product Detail</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body sideBg">
                                <div class="tabing-section">
                                    <ul class="nav nav-tabs" style="margin-top: 10px;">
                                        <li class="active"><a data-toggle="tab" href="#pd">Product Description</a></li>
                                        <li><a data-toggle="tab" href="#pimg">Product Image</a></li>
                                        <li><a data-toggle="tab" href="#spd">Support Provided by DPSU</a></li>
                                        <li><a data-toggle="tab" href="#qpt">Quantity Required</a></li>
                                        <li><a data-toggle="tab" href="#tdr">Tender Status</a></li>
                                        <li><a data-toggle="tab" href="#cd">Contact Details</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="pd" class="tab-pane fade in active">
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>Company Refrence No</th>
                                                        <th>Company Name</th>
                                                        <th>Product Refrence No</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblcomprefno" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblcompname" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblprodrefno" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>OEM Part Number</th>
                                                        <th>DPSU Part Number</th>
                                                        <th>End User Part Number</th>
                                                        <th>HSN Code</th>
                                                        <th>NATO Code</th>
                                                        <th>ERP Reference No</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbloempartnumber" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbldpsupartno" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblenduserpartno" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblhsncode" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblnatocode" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblerprefno" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>Nomenclature of main system</th>
                                                        <th>Product Level 1</th>
                                                        <th>Product Level 2</th>
                                                        <th>Product Description</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblnomenclatureofmainsystem" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblprodlevel1" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="productlevel2" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblproductdescription" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>Technology Level 1</th>
                                                        <th>Technology Level 2</th>
                                                        <th>End User</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbltechlevel1" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbltechlevel2" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblenduser" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>Platform :</th>
                                                        <th>Purpose of Procurement :</th>
                                                        <th>Product Time Frame</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblplatform" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblpurposeofprocurement" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblprodtimeframe" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>Search Keywords :</th>
                                                        <th>Product Already Indeginized </th>
                                                        <th runat="server" id="tablemanufacturename">Manufacturer Name</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblsearchkeyword" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblprodalredyindeginized" runat="server"></asp:Label></td>
                                                        <td runat="server" id="tablemanufacturename1">
                                                            <asp:Label ID="lblmanufacturename" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div id="pimg" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:DataList runat="server" ID="dlimage" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <ItemTemplate>
                                                            <div class="col-sm-3">
                                                                <asp:Image runat="server" ID="imgprodimage" class="image img-responsive img-rounded" Height="120px" Width="120" src='<%#Eval("ImageName") %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="spd" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass" width="30%">Support Provided by DPSU</th>
                                                                <th class="pass" width="30%">Remarks</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblsupportprovidedbydpsu" runat="server"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblremarks" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="qpt" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass">Estimated Quantity</th>
                                                                <th class="pass">Estimated Price / LLP</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblestimatedquantity" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblestimatedprice" runat="server"></asp:Label>
                                                                </td>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tdr" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass">Tender Status</th>
                                                                <th class="pass">Tender Submission</th>
                                                                <th class="pass">Tender Date</th>
                                                                <th class="pass">Tender URL</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbltenderstatus" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltendersubmission" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltenderdate" runat="server"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbltenderurl" runat="server"></asp:Label>
                                                                </td>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="cd" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass">Employee Code</th>
                                                                <th class="pass">Designation</th>
                                                                <th class="pass">E-Mail ID</th>
                                                                <th class="pass">Mobile Number</th>
                                                                <th class="pass">Phone Number</th>
                                                                <th class="pass">Fax</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblempcode" runat="server"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbldesignation" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblemailid" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblmobilenumber" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblphonenumber" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblfax" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered" runat="server" id="tablenodal2">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass">Employee Code</th>
                                                                <th class="pass">Designation</th>
                                                                <th class="pass">E-Mail ID</th>
                                                                <th class="pass">Mobile Number</th>
                                                                <th class="pass">Phone Number</th>
                                                                <th class="pass">Fax</th>

                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblempcode2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbldesignation2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblemailid2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblmobileno2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblphoneno2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblfax2" runat="server"></asp:Label>
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
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
