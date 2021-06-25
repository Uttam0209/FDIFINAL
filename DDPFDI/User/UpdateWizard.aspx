<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateWizard.aspx.cs" Inherits="User_UpdateWizard" MasterPageFile="~/User/MasterPage.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
        <ContentTemplate>
            <style>
                .box_pro {
                    border: 1px solid #0000003d;
                    box-shadow: 0px -1px 7px 3px #00000014;
                }
            </style>
            <div class="container" id="divwizard" runat="server">
                <div class="row box-shadow" style="justify-content: space-between;">
                    <div class="col-md-3 bg-success p-4 box_pro">
                        <label>
                            <b>Total Interest Product</b>
                        </label>
                        <span>
                            <h3><b>
                                <asp:LinkButton runat="server" ID="lbltotalinterest" Text="0" CssClass="pull-right" OnClick="lbltotalinterest_Click"></asp:LinkButton></b></h3>
                        </span>

                    </div>
                    <div class="col-md-3 bg-info p-4 box_pro">
                        <label>
                            <b>Product not display in market place :</b>
                        </label>
                        <span>
                            <h3><b>
                                <asp:LinkButton runat="server" ID="lblnotdisplayinmarketplace" Text="0" CssClass="pull-right" ForeColor="Red" OnClick="lblnotdisplayinmarketplace_Click"></asp:LinkButton></b></h3>
                        </span>
                    </div>
                    <div class="col-md-3 bg-warning p-4 box_pro">
                        <label>
                            <b>Total Public Doamin Product</b>
                        </label>
                        <span>
                            <h3><b>
                                <asp:LinkButton runat="server" ID="lbltotalproduct" Text="0" ForeColor="Green" CssClass="pull-right" OnClick="lbltotalproduct_Click"></asp:LinkButton></b></h3>
                        </span>
                    </div>
                </div>
                <div class="row" style="justify-content: space-between;"></div>
                <div class="row boxshadow" style="justify-content: space-between;">
                    <div class="col-md-3 bg-info p-4 box_pro">
                        <label>
                            <b>Total Interest EOI Product (to be update)</b>
                        </label>
                        <span>
                            <h3><b>
                                <asp:LinkButton runat="server" ID="lbtotaleoiinterest" Text="0" CssClass="pull-right" OnClick="lbtotaleoiinterest_Click"></asp:LinkButton></b></h3>
                        </span>
                    </div>
                    <div class="col-md-3 bg-success p-4 box_pro">
                        <label>
                            <b>Total Interest Supply Order Product (to be update)</b>

                            <span>
                                <h3><b>
                                    <asp:LinkButton runat="server" ID="lbtotalintsupplyorder" Text="0" CssClass="pull-right" OnClick="lbtotalintsupplyorder_Click"></asp:LinkButton></b></h3>
                            </span>
                    </div>
                    <div class="col-md-3 bg-warning p-4 box_pro">
                        <label>
                            <b>Total Interest Indiginized/Success Story (to be update)</b>
                        </label>
                        <span>
                            <h3><b>
                                <asp:LinkButton runat="server" ID="lbtotalsuccessstory" Text="0" CssClass="pull-right" OnClick="lbtotalsuccessstory_Click"></asp:LinkButton></b></h3>
                        </span>
                    </div>
                </div>


                <div class="col-sm-10" runat="server">
                    <div class="clearfix mt-1"></div>
                    <div class="row border p-1">



                        <div class="col-sm-12 mt-0 p-1 border-right d-none">
                            <label>
                                <b>Total Public Doamin Product</b>
                            </label>
                            <span>
                                <h3><b>
                                    <asp:LinkButton runat="server" ID="LinkButton1" Text="0" ForeColor="Green" CssClass="pull-right" OnClick="lbltotalproduct_Click"></asp:LinkButton></b></h3>
                            </span>
                            <div class="clearfix"></div>

                            <hr />

                            <div class="clearfix"></div>
                        </div>
                        <div class="col-sm-12 mt-0 p-1 border-right d-none">

                            <div class="clearfix"></div>

                            <hr />

                            <div class="clearfix"></div>
                        </div>
                        <div class="col-sm-12 mt-0 p-1 border-right d-none">
                            <label>
                                <b>Total EOI Product (to be update)</b>
                            </label>
                            <span>
                                <h3><b>
                                    <asp:LinkButton runat="server" ID="lbtotaleoi" Text="0" CssClass="pull-right" OnClick="lbtotaleoi_Click"></asp:LinkButton></b></h3>
                            </span>
                            <div class="clearfix"></div>

                            <hr />

                            <div class="clearfix"></div>
                        </div>
                        <div class="col-sm-12 mt-0 p-1 border-right d-none">
                            <label>
                                <b>Total Supply Order Product (to be update)</b>
                            </label>
                            <span>
                                <h3><b>
                                    <asp:LinkButton runat="server" ID="lbtotalsupplyorder" Text="0" CssClass="pull-right" OnClick="lbtotalsupplyorder_Click"></asp:LinkButton></b></h3>
                            </span>
                            <div class="clearfix"></div>

                        </div>
                        <hr />


                        <div class="clearfix"></div>
                        <%-- ------------*******************  Div for EOI UPDATE  ******************--------------------%>
                        <div class="row" id="divupdates" runat="server">
                            <div class="col-md-12 ">
                                <div class="panel panel-primary list-panel" id="Div2" style="border-color: #a9a7a8 !important">
                                    <div class="panel-heading list-panel-heading headergrid">
                                        <h1 class="panel-title list-panel-title text-left">EOI to be Updates :                         
                                                <asp:LinkButton ID="lnksetback" runat="server" Text="Back" Style="text-align: right" OnClick="lnksetback_Click"
                                                    Font-Bold="True" ForeColor="white"></asp:LinkButton>
                                        </h1>
                                        <p style="float: right;">
                                            Showing
                                    <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                                            products of
                                <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                                            products  
                                        </p>
                                        <div class="row">
                                            <div class="col-sm-1">
                                                <label>Index</label>
                                                <asp:DropDownList ID="ddlsort" runat="server" Width="83px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsort_SelectedIndexChanged">
                                                    <asp:ListItem>100</asp:ListItem>
                                                    <asp:ListItem>200</asp:ListItem>
                                                    <asp:ListItem>400</asp:ListItem>
                                                    <asp:ListItem>500</asp:ListItem>
                                                    <asp:ListItem>1000</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-sm-4">
                                                <label>Search</label>
                                                <asp:TextBox runat="server" ID="txtsearch" CssClass="form-control" Placehodler="Search (Productrefno,Company,division,unit)"
                                                    OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="table table-responsive">
                                                <asp:GridView runat="server" ID="gvEoi" AutoGenerateColumns="false" class="table table-hover table-dark" Width="100%"
                                                    OnRowDataBound="gvEoi_RowDataBound" OnRowCommand="gvEoi_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No.">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkRow" />&nbsp;&nbsp;
                                                <%#Eval("row_no") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProductRefNo" HeaderText="Item No" NullDisplayText="#" HeaderStyle-Wrap="true" />
                                                        <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="300px" NullDisplayText="#" HeaderStyle-Wrap="true" />
                                                        <asp:BoundField DataField="EOIStatus" HeaderText="EOI Status" NullDisplayText="#" HeaderStyle-Width="150px" HeaderStyle-Wrap="true" />
                                                        <asp:BoundField DataField="EOIStartDate" HeaderText="EOI Start Date" NullDisplayText="#" HeaderStyle-Width="150px" HeaderStyle-Wrap="true" />
                                                        <asp:BoundField DataField="EOIEndDate" HeaderText="EOI End Date" NullDisplayText="#" HeaderStyle-Width="150px" HeaderStyle-Wrap="true" />
                                                        <asp:BoundField DataField="EOIUrl" HeaderText="EOI Url" NullDisplayText="#" HeaderStyle-Wrap="true" />
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbupdate" ForeColor="White" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="VEOI"><i class="fa fa-edit"></i>Update</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>


                                            </div>

                                            <div class="clearfix mt-1"></div>
                                            <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-dark btn-sm"
                                                                OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn  btn-dark btn-sm pull-right"
                                                                OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="text-center">
                                                        <asp:Label ID="lblpaging" runat="server" class="btn btn-dark text-center" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-----------------------------------------end code for page indexing----------------------------------------------------->

                                        </div>

                                    </div>
                                </div>
                            </div>


                        </div>
                        <%-- ------------******************* End Div for EOI UPDATE  ******************--------------------%>

                        <div class="clearfix"></div>
                        <%-- ------------*******************  Div for Supply order UPDATE  ******************--------------------%>
                                       <p style="float: right;">
                                Showing
                                    <asp:Label runat="server" ID="Label1"></asp:Label>
                                products of
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                products  
                            </p>
                            <div class="row">
                                <div class="col-sm-1">
                                    <label>Index</label>
                                    <asp:DropDownList ID="ddlsupplyorder" runat="server" Width="83px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsupplyorder_SelectedIndexChanged">
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>400</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                        <asp:ListItem>1000</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label>Search</label>
                                    <asp:TextBox runat="server" ID="Txtsupply" CssClass="form-control" Placehodler="Search (Productrefno,Company,division,unit)" OnTextChanged="Txtsupply_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix mt-1"></div>
                            <div class="table table-responsive">
                                <asp:GridView runat="server" ID="gvSupplyOrder" AutoGenerateColumns="false" class="table table-hover table-dark" OnRowDataBound="gvSupplyOrder_RowDataBound" OnRowCommand="gvSupplyOrder_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="80px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkRow" />&nbsp;&nbsp;
                                                <%#Eval("row_no") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProductRefNo" HeaderText="Item No" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="SupplyOrderStatus" HeaderText="Status" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="200px" />
                                        <asp:BoundField DataField="SupplyManfutureName" HeaderText="Manufacture" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="180px" />
                                        <asp:BoundField DataField="SupplyManfutureAddress" HeaderText="Address" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="SupplyOrderValue" HeaderText="SO-Value in (Rs Lakhs) " NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="250px" />
                                        <asp:BoundField DataField="SupplyDeliveryDate" HeaderText="Delivery (Compliance Date)" DataFormatString="{0:dd-MMM-yyyy}" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="250px" />
                                        <asp:BoundField DataField="SupplyOrderDate" HeaderText="SO-Date" NullDisplayText="#" DataFormatString="{0:dd-MMM-yyyy}" HeaderStyle-Wrap="true" HeaderStyle-Width="250px" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbupdate" ForeColor="White" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="VEOI"><i class="fa fa-edit"></i>Update</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="clearfix mt-1"></div>
                            <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="Lnksupply" runat="server" CssClass="btn btn-dark btn-sm"
                                                OnClick="Lnksupply_Click">Previous</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="Lnksupplynext" runat="server" CssClass="btn  btn-dark btn-sm pull-right"
                                                OnClick="Lnksupplynext_Click">Next</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="text-center">
                                        <asp:Label ID="Label3" runat="server" class="btn btn-dark text-center" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <!-----------------------------------------end code for page indexing----------------------------------------------------->

                         <%-- ------------******************* End Div for Supply order UPDATE  ******************--------------------%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-----------------------------------------Modal popup for EOI Update----------------------------------------------------->
    <div class="modal-quick-view modal fade" id="modeleoi" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-xl" style="max-width: 500px!important; z-index: 9999999999;">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header d-flex justify-content-center" style="background: #507CD1!important">
                            <h4 class="modal-title">EOI-Updation</h4>
                        </div>
                        <div class="modal-body">
                            <p>All field fill mandatory. After successfully update record will remove from here and display in success stroy page.</p>
                            <div class="form-group">
                                <label>EOI Status</label>&nbsp;<span style="color: red">*</span>
                                <asp:DropDownList ID="ddleoistatus" runat="server" TabIndex="1" ToolTip="Please enter the status of your eoi" AutoPostBack="true" OnSelectedIndexChanged="ddleoistatus_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem Value="">Select</asp:ListItem>
                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                    <%--<asp:ListItem Value="No">No</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div runat="server" id="diveoishow" visible="false">
                                <div class="form-group">
                                    <label>EOI Status</label>&nbsp;<span style="color: red">*</span>
                                    <asp:DropDownList ID="ddleoitype" runat="server" TabIndex="2" ToolTip="select the status of eoi" CssClass="form-control">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="Yes">Active</asp:ListItem>
                                        <asp:ListItem Value="Archive">Archive</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>EOI Start Date</label>&nbsp;<span style="color: red">*</span>
                                    <asp:TextBox runat="server" ID="txtstartdate" TabIndex="3" type="date" ToolTip="eoi start date (eoi start date could not be greater then eoi end date)"
                                        CssClass="form-control datepicker" autocomplete="off" placeholder="Start date (format:- 01-jan-1900)"></asp:TextBox>
                                    <p>Date will store in this format:- DD-MMM-YYYY (01-Mar-2021)</p>
                                </div>
                                <div class="form-group">
                                    <label>EOI End Date</label>&nbsp;<span style="color: red">*</span>
                                    <asp:TextBox runat="server" ID="txtenddate" TabIndex="4" type="date" AutoPostBack="true" OnTextChanged="txtenddate_TextChanged" ToolTip="eoi end date (eoi end date could not be less then eoi start date)"
                                        CssClass="form-control datepicker" autocomplete="off" placeholder="Start date (format:- 01-jan-1900)"></asp:TextBox>
                                    <p>Date will store in this format:- DD-MMM-YYYY (01-Mar-2021)</p>
                                </div>
                                <div class="form-group">
                                    <label>EOI URL</label>&nbsp;<span style="color: red">*</span>
                                    <asp:TextBox runat="server" ID="txturl" TabIndex="5" CssClass="form-control" ToolTip="eoi url" Placeholder="EOI URL" TextMode="Url"></asp:TextBox>

                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary  btn-sm" tabindex="7" data-dismiss="modal">Close</button>
                            <div class="form-group">
                                <asp:LinkButton runat="server" ID="lbsubmit" TabIndex="6" Visible="false" ToolTip="after fill all text box or select dorpdown your form will submit and update status of eoi"
                                    CssClass="btn btn-primary btn-sm" OnClick="lbsubmit_Click"><i class="fa fa-save"></i>&nbsp;Submit & Update</asp:LinkButton>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lbsubmit" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-----------------------------------------End Modal popup for EOI Update----------------------------------------------------->
    <!-----------------------------------------Modal popup for Supply Update----------------------------------------------------->
      <div class="modal-quick-view modal fade" id="modelsupplyorder" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-xl" style="max-width: 500px!important; z-index: 9999999999;">
            <div class="modal-content">
                <div class="modal-header d-flex justify-content-center" style="background: #507CD1!important">
                    <h4 class="modal-title">Supply Order -Updation</h4>
                </div>
                <div class="modal-body">
                    <p>All field fill mandatory. After successfully update record will remove from here and display in success stroy page.</p>
                    <div class="form-group">
                        <label>Supply Order Placed</label>
                        <asp:DropDownList ID="DropDownList1" runat="server" TabIndex="1" ToolTip="Please enter the status of your supply order" CssClass="form-control">
                            <asp:ListItem Value="">Select</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Manufacture Name</label>
                        <asp:TextBox runat="server" ID="txtmanufacname" placeholder="Manufacture Name" TabIndex="2" ToolTip="Please enter name of manufacture name" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Address </label>
                        <asp:TextBox runat="server" ID="txtmanuadd" TabIndex="3" ToolTip="Manufacture address" CssClass="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Supply Order Value in (Rs Lakhs)</label>
                        <asp:TextBox runat="server" ID="txtsupplyordervalue" TabIndex="4" CssClass="form-control" onkeypress="return isNumberKey(event)" ToolTip="Supply order value in (Rs Lakhs) deciaml allowed"
                            Placeholder="Supply Order Value in (Rs Lakhs)"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Delivery (Compliance Date)</label>
                        <asp:TextBox runat="server" ID="txtdelivery" TabIndex="5" ToolTip="Delivery (Compliance Date)" CssClass="form-control" type="date"></asp:TextBox>
                        <p>Date will store in this format:- DD-MMM-YYYY (01-Mar-2021)</p>
                    </div>
                    <div class="form-group">
                        <label>Supply Order Date</label>
                        <asp:TextBox runat="server" ID="txtsupplyorderdate" TabIndex="6" ToolTip="Supply order  date" CssClass="form-control" type="date"></asp:TextBox>
                        <p>Date will store in this format:- DD-MMM-YYYY (01-Mar-2021)</p>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary  btn-sm" tabindex="8" data-dismiss="modal">Close</button>
                    <div class="form-group">
                        <asp:LinkButton runat="server" ID="LinkButton2" TabIndex="7" ToolTip="after fill all text box or select dorpdown your 
                            form will submit and update status of supply order" CssClass="btn btn-primary btn-sm" OnClick="lbsubmit_Click"><i class="fa fa-save"></i>&nbsp;Submit & Update</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript">
        function showPopup() {
            $('#modelsupplyorder').modal('show');
        }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <!-----------------------------------------End Modal popup for Supply Update----------------------------------------------------->
    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript">
        function showPopup() {
            $('#modeleoi').modal('show');
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtstartdate').datepicker({
                dateFormat: "dd-M-yy"
            });
            $('#ContentPlaceHolder1_txtenddate').datepicker({
                dateFormat: "dd-M-yy"
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $('#ContentPlaceHolder1_txtstartdate').datepicker({
                    dateFormat: "dd-M-yy"
                });
                $('#ContentPlaceHolder1_txtenddate').datepicker({
                    dateFormat: "dd-M-yy"
                });
            });
        });
    </script>
</asp:Content>



