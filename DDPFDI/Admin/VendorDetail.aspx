<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorDetail.aspx.cs" Inherits="Admin_VendorDetail" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function showPopup() {
            $('#alert').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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
            <div class="addfdi">
                <div class="section-pannel">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:HiddenField ID="hfmtype" runat="server" />
                            <div class="text-center">
                                <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="clearfix" style="margin-bottom: 10px;"></div>
                            <asp:UpdatePanel ID="up" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive" runat="server" style="overflow: scroll;">
                                        <asp:GridView ID="gvvendor" runat="server" Width="100%" Class="table table-bordered table-hover table-responsive"
                                            AutoGenerateColumns="false" OnRowCreated="gvvendor_RowCreated" OnRowCommand="gvvendor_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("V_CompName") %>' NullDisplayText="#"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GST No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblcompanyrole" Text='<%#Eval("GSTNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Width="150px" DataField="City" HeaderText="City" />
                                                <asp:TemplateField HeaderText="Nodal Officer Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("NodalOfficerEmail") %>' NullDisplayText="#"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" ToolTip="View detail of Vendor company" CommandArgument='<%#Eval("VendorRefNo") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="clearfix mt5"></div>
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
                                        <!-----------------------------------------end code for page indexing----------------------------------------------------->
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="modal fade" id="alert" role="dialog">
                                        <div class="modal-dialog" style="width: 1200px; z-index: 9999999999;">
                                            <!-- Modal content-->
                                            <div class="modal-content">
                                                <div class="modal-header modal-header1">
                                                    <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                                    <h4 class="modal-title">Vendor Detail</h4>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="col-sm-12">
                                                                <tr>
                                                                    <td>Refrence No:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblcomprefno" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                                                <tr>
                                                                    <td>Company:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblcompname" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                                                <tr>
                                                                    <td>Address:</td>
                                                                    <td>
                                                                        <asp:Label ID="lbladdress" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                                                <tr>
                                                                    <td>GST No:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblgst" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                                                <tr>
                                                                    <td>Contact No:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblcontact" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="col-sm-12">

                                                                <tr>
                                                                    <td>City:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblcity" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                                                <tr>
                                                                    <td>Zip/Pin Code:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblzipcode" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                                                <tr>
                                                                    <td>Nodal officer Email:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblnodalemail" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                                                <tr>
                                                                    <td>Is MSME:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblmsme" runat="server" Text=""></asp:Label></td>
                                                                </tr>
                                                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                                                <tr>
                                                                    <td>DPSU Name:</td>
                                                                    <td>
                                                                        <asp:GridView ID="gvdpsu" runat="server" Class="table table-responsive" AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DPSUName" HeaderText="DPSU Name" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <div class="clearfix" style="margin-bottom: 10px;"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
