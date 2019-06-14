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
                                                        <td>C0004</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Company:</td>
                                                        <td>HAL</td>
                                                    </tr>
                                                     <tr>
                                                        <td>Division/Palnt:</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Unit:</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Product Refrence No:</td>
                                                        <td>PRO0213</td>
                                                    </tr>
                                                         </table>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <table>
                                                            <tr>
                                                        <td>NSN GROUP:</td>
                                                        <td>Hardware and Abrasives(53)</td>
                                                    </tr>
                                                    <tr>
                                                        <td>NSN GROUP CLASS:</td>
                                                        <td>O-Ring(31)</td>
                                                    </tr>
                                                    <tr>
                                                        <td>CLASS ITEM:</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>NSC Code (4 digit):</td>
                                                        <td>5331</td>
                                                    </tr>
                                                    <tr>
                                                        <td>NIIN Code (9-digit):</td>
                                                        <td>720324606</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Product Description:</td>
                                                        <td>Rubber sealing ring</td>
                                                    </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="row two-col">
                                                    <div class="col-md-6">
                                                        <table>
                                                             <tr>
                                                        <td>OEM Part Number:</td>
                                                        <td>2267A-154-2</td>
                                                    </tr>
                                                    <tr>
                                                        <td>OEM Name:</td>
                                                        <td>Rosoboron Export</td>
                                                    </tr>
                                                    <tr>
                                                        <td>OEM Country:</td>
                                                        <td>Russia</td>
                                                    </tr>
                                                    <tr>
                                                        <td>DPSU Part Number:</td>
                                                        <td>K-2267A-154-2</td>
                                                    </tr>
                                                    <tr>
                                                        <td>End User:</td>
                                                        <td>INDIAN AIRFORCE</td>
                                                    </tr>
                                                    <tr>
                                                        <td>End User Part Number:</td>
                                                        <td>K-2267A-154-2</td>
                                                    </tr>
                                                    <tr>
                                                        <td>HSN Code:</td>
                                                        <td>88033000</td>
                                                    </tr>

                                                        </table>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <table>
                                                            <tr>
                                                        <td>DEFENCE PLATFORM:</td>
                                                        <td>FIGHTER AIRCRAFT</td>
                                                    </tr>
                                                    <tr>
                                                        <td>NAME OF DEFENCE PLATFORM:</td>
                                                        <td>MiG</td>

                                                    </tr>
                                                    <tr>
                                                        <td>PRODUCT (INDUSTRY DOMAIN):</td>
                                                        <td>MECHANICAL</td>
                                                    </tr>
                                                    <tr>
                                                        <td>PRODUCT (INDUSTRY SUB DOMAIN):</td>
                                                        <td>Machining</td>
                                                    </tr>
                                                    <tr>
                                                        <td>PRODUCT (INDUSTRY 2nd SUB DOMAIN):</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>PROCURMENT CATEGORY:</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>PROCURMENT CATEGORY REMARK</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Product Already Indeginized</td>
                                                        <td>No</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Search Keywords:</td>
                                                        <td></td>
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
                                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq2" aria-expanded="false" aria-controls="faq2">Image
                                                            <i class="fa fa-plus pull-right"></i>
                                                    </h2>
                                                </div>

                                                <div id="faq2" class="collapse">
                                                    <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="card">
                                                <div class="card-header">
                                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq3" aria-expanded="false" aria-controls="faq3">Imported products
                                                            <i class="fa fa-plus pull-right"></i>
                                                    </h2>
                                                </div>

                                                <div id="faq3" class="collapse">
                                                    <div class="card-body">
                                                        <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Is Product Imported:</td>
                                                                            <td>N</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Year of Import:</td>
                                                                            <td>All (Expect Last Five Year)</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>All (Expect Last Five Year)</td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="card">
                                                <div class="card-header">
                                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq4" aria-expanded="false" aria-controls="faq4">Testing
                                                            <i class="fa fa-plus pull-right"></i>
                                                    </h2>
                                                </div>

                                                <div id="faq4" class="collapse">
                                                    <div class="card-body">
                                                         <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Testing:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>All (Expect Last Five Year)</td>
                                                                        </tr>
                                                                        
                                                                    </table>
                                                                </div>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="card">
                                                <div class="card-header">
                                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq5" aria-expanded="false" aria-controls="faq5">Certification
                                                            <i class="fa fa-plus pull-right"></i>
                                                    </h2>
                                                </div>

                                                <div id="faq5" class="collapse">
                                                    <div class="card-body">
                                                        <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Certification:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        
                                                                    </table>
                                                                </div>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="card">
                                                <div class="card-header">
                                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq6" aria-expanded="false" aria-controls="faq6">Technical Support
                                                            <i class="fa fa-plus pull-right"></i>
                                                    </h2>
                                                </div>

                                                <div id="faq6" class="collapse">
                                                    <div class="card-body">
                                                         <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Technical Support:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        
                                                                    </table>
                                                                </div>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="card">
                                                <div class="card-header">
                                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq7" aria-expanded="false" aria-controls="faq7">Financial Support
                                                            <i class="fa fa-plus pull-right"></i>
                                                    </h2>
                                                </div>

                                                <div id="faq7" class="collapse">
                                                    <div class="card-body">
                                                        <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Financial Support:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        
                                                                    </table>
                                                                </div>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="card">
                                                <div class="card-header">
                                                    <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq8" aria-expanded="false" aria-controls="faq8">Quantity Required
                                                            <i class="fa fa-plus pull-right"></i>
                                                    </h2>
                                                </div>

                                                <div id="faq8" class="collapse">
                                                    <div class="card-body">
                                                        <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Estimated Quantity:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Estimated Quantity In:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td>Estimated Price / LLP:</td>
                                                                            <td></td>
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
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Tender Submission:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td>Tender Date:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Tender URL:</td>
                                                                            <td></td>
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
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Employee Code:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Employee Name:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td>Designation:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>E-Mail ID:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Mobile Number:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Phone Number:</td>
                                                                            <td></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td>Fax:</td>
                                                                            <td></td>
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
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
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
