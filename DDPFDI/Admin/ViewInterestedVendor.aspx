<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewInterestedVendor.aspx.cs" Inherits="Admin_ViewInterestedVendor" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
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
                        <asp:HiddenField runat="server" ID="hidType" />
                        <asp:HiddenField runat="server" ID="hfcomprefno" />
                        <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Select Company</label>
                                <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" runat="server" id="lblselectdivison">
                            <div class="form-group">
                                <label>Select Division/Plant</label>
                                <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" runat="server" id="lblselectunit">
                            <div class="form-group">
                                <label>Select Unit</label>
                                <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <%--  <div class="col-sm-3">
                            <div class="form-group" >
                                <label>Item Id (Portal)</label>
                                <asp:TextBox ID="txtsearchbyrefid" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtsearchbyrefid_TextChanged"></asp:TextBox>
                            </div>
                        </div>--%>
                    </div>
                    <div>
                        <asp:Button ID="btnexcel" runat="server" CssClass="btn btn-primary" Text="download excel" OnClick="btnexcel_Click" />
                    </div>
                    <div class="clearfix"></div>

                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive" style="overflow-x: auto;" id="divproductgridview" visible="false" runat="server">
                                    <p style="float: right;">
                                        Showing
                                    <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                                        products of
                                <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                                        products  
                                    </p>
                                    <div class="clearfix mt10"></div>
                                    <asp:GridView ID="gvproductItem" runat="server" Width="100%" Class="table table-bordered table-wraper table-hover manage-user"
                                        AutoGenerateColumns="false" OnRowCommand="gvproductItem_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                  
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ProductRefNo" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductRefNo" runat="server" Text='<%# Eval("ProductRefNo") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductRefNo" runat="server" Text='<%# Eval("RequestCompName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Division " HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductRefNo" runat="server" Text='<%# Eval("FactoryName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductRefNo" runat="server" Text='<%# Eval("UnitName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductRefNo" runat="server" Text='<%# Eval("RequestBy") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductRefNo" runat="server" Text='<%# Eval("RequestMobileNo") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmailID" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductRefNo" runat="server" Text='<%# Eval("RequestEmail") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address" HeaderStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductRefNo" runat="server" Text='<%# Eval("RequestAddress") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="RequestCompName" HeaderText="Company" />
                                            <asp:BoundField DataField="RequestAddress" HeaderText="Address" />
                                            <asp:BoundField DataField="RequestBy" HeaderText="Name" />
                                            <asp:BoundField DataField="RequestMobileNo" HeaderText="Mobile" />
                                            <asp:BoundField DataField="RequestEmail" HeaderText="Email" />

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                                <div class="row" runat="server" id="divpageindex" visible="false">
                                    <div class="col-sm-12">
                                        <div class="col-sm-6 row">
                                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-info  btn-sm"
                                                OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-6 row">
                                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn  btn-info btn-sm pull-right"
                                                OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="text-center">
                                        <asp:Label ID="lblpaging" runat="server" class="btn btn-primary text-center" Text=""></asp:Label>
                                    </div>
                                </div>
                                <!-----------------------------------------end code for page indexing----------------------------------------------------->
                            </div>
                        </div>
                </div>
                </form>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnPgPrevious" />
            <asp:PostBackTrigger ControlID="lnkbtnPgNext" />
             <asp:PostBackTrigger ControlID="btnExcel" />
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

