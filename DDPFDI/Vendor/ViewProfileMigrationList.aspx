<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/VendorMaster2.master" AutoEventWireup="true" CodeFile="ViewProfileMigrationList.aspx.cs" Inherits="Vendor_ViewProfileMigrationList" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <%--   <div class="content oem-content">--%>
    <div class="right_col" role="main">
        <div class="">
            <div class="clearfix"></div>
            <div class="row">
                <div class="col-md-12 col-sm-12 ">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>View Profile Migration List&nbsp;
                            <div class="clearfix"></div>
                        </div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <% if (CVRUsertype == "Vendor")
                                        { %>
                                    <div class="card-box table-responsive">
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" class="table table-striped table-bordered">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NodalOfficerName" HeaderText="Nodal Officer Name" />
                                                <asp:BoundField DataField="NodalOfficerEmail" HeaderText="Nodal Officer Email" />
                                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                                <asp:BoundField DataField="StreetAddress" HeaderText="Address Line1" />
                                                <asp:BoundField DataField="StreetAddressLine2" HeaderText="Address Line2" />
                                                <asp:BoundField DataField="City" HeaderText="City" />
                                                <asp:BoundField DataField="vState" HeaderText="State" />
                                                <asp:BoundField DataField="ZipCode" HeaderText="Pincode" />
                                                <%--<asp:BoundField DataField="" HeaderText="Edit" />--%>
                                                <%-- <asp:BoundField DataField="Password" HeaderText="User Password" />--%>
                                            </Columns>

                                        </asp:GridView>
                                        <%--<button type="button" class="btn btn-success">View</button>
                                        <button type="button" class="btn btn-success">Edit</button>--%>
                                    </div>
                                    <%} %>

                                    <%else if(CVRUsertype == "Admin")
                                        { %>
                                    <div class="card-box table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-bordered">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:BoundField DataField="NodalOfficerName" HeaderText="Nodal Officer Name" />
                                                <asp:BoundField DataField="NodalOfficerEmail" HeaderText="Nodal Officer Email" />
                                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                                <asp:BoundField DataField="StreetAddress" HeaderText="Address Line1" />
                                                <asp:BoundField DataField="StreetAddressLine2" HeaderText="Address Line2" />
                                                <asp:BoundField DataField="City" HeaderText="City" />
                                                <asp:BoundField DataField="vState" HeaderText="State" />
                                                <asp:BoundField DataField="ZipCode" HeaderText="Pincode" />
                                                
                                            </Columns>

                                        </asp:GridView>
                                        <%--<button type="button" class="btn btn-success">View</button>
                                        <button type="button" class="btn btn-success">Edit</button>--%>
                                    </div>
                                    <%}%>

                                    <%else if(CVRUsertype == "User")
                                        { %>
                                    <div class="card-box table-responsive">
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" class="table table-striped table-bordered">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:BoundField DataField="NodalOfficerName" HeaderText="Nodal Officer Name" />
                                                <asp:BoundField DataField="NodalOfficerEmail" HeaderText="Nodal Officer Email" />
                                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                                <asp:BoundField DataField="StreetAddress" HeaderText="Address Line1" />
                                                <asp:BoundField DataField="StreetAddressLine2" HeaderText="Address Line2" />
                                                <asp:BoundField DataField="City" HeaderText="City" />
                                                <asp:BoundField DataField="vState" HeaderText="State" />
                                                <asp:BoundField DataField="ZipCode" HeaderText="Pincode" />
                                            </Columns>

                                        </asp:GridView>
                                        <%--<button type="button" class="btn btn-success">View</button>
                                        <button type="button" class="btn btn-success">Edit</button>--%>
                                    </div>
                                    <%}%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </>
    <asp:HiddenField runat="server" ID="hfotp" />
    <div class="modal fade" id="modelotp" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered" role="document" style="max-width: 600px;">
            <div class="modal-content">
                <div class="modal-header">
                    <ul class="nav nav-tabs card-header-tabs" role="tablist">
                        <li class="nav-item"><a class="nav-link active" href="#" data-toggle="tab" role="tab"
                            aria-selected="true">OTP</a></li>
                    </ul>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body tab-content py-4">
                    <p class="text-justify">
                        <asp:TextBox runat="server" ID="txtotp" placeholder="OTP (6 Digit)" TabIndex="1" CssClass="form-control"></asp:TextBox>
                        <div class="clearfix mt-1">
                        </div>
                        <span>Please enter otp received on your given email id.</span>
                        <div class="clearfix mt-1">
                        </div>
                        <asp:LinkButton ID="lbsubmit" runat="server" CssClass="btn btn-primary btn-shadow pull-right" TabIndex="2" Text="Submit"
                            ToolTip="Thank you for showing intrest in these product mail will send to admin and also you will recieved a copy." OnClick="lbsubmit_Click"></asp:LinkButton>
                        <asp:LinkButton runat="server" Text="Resend OTP" ID="lbresendotp" TabIndex="3" CssClass="pull-right mr-1 p-2" OnClick="lbresendotp_Click"></asp:LinkButton>
                        <p>
                        </p>
                    </p>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

