<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewVendorRegistrationDetail.aspx.cs" Inherits="Vendor_ViewVendorRegistrationDetail" MasterPageFile="~/Vendor/VendorMaster.master" %>

<asp:Content ID="ConHead" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function showPopup() {
            $('#VendorDetail').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Innercontent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="cacade-forms">
                            <div class="clearfix mt10"></div>
                            <div class="text-center">
                                <asp:Label ID="lbltotalcount" runat="server" CssClass="label label-info"></asp:Label>
                            </div>
                            <div class="clearfix mt10"></div>
                            <div class="table-wraper table-responsive">
                                <asp:GridView ID="gvVendorDetails" runat="server" Class="commonAjaxTbl master-company-table table display responsive
                                         no-wrap table-hover manage-user Grid"
                                    AutoGenerateColumns="false" OnRowCreated="gvVendorDetails_RowCreated" OnRowCommand="gvVendorDetails_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FirmCompanyName" HeaderText="Company" />
                                        <asp:BoundField DataField="RegistrationCategory" HeaderText="Category" />
                                        <asp:BoundField DataField="Ownership" HeaderText="OwnerShip" />
                                        <asp:BoundField DataField="FirstName" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" Text="View"
                                                    CommandArgument='<%#Eval("VendorDetailID") %>' CommandName="viewdetails"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal fade" id="VendorDetail" role="dialog">
                            <div class="modal-dialog">
                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header modal-header1">
                                        <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Vendor Registration details</h4>
                                    </div>
                                    <div class="modal-body">
                                        <table class="table table-bordered">
                                            <tbody>
                                                <tr>
                                                    <td class="pass">Reference no</td>
                                                    <td>
                                                        <asp:Label ID="lblrefno" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="pass">Category</td>
                                                    <td>
                                                        <asp:Label ID="lblcategory" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="pass">Company Name</td>
                                                    <td>
                                                        <asp:Label ID="lblcompane" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="pass">OwnerShip</td>
                                                    <td>
                                                        <asp:Label ID="lblownership" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <div runat="server" id="divown" visible="false">
                                                    <tr>
                                                        <td class="pass">Scale of Buisness</td>
                                                        <td>
                                                            <asp:Label ID="lblscaleofbuisness" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pass">OwnerShip</td>
                                                        <td>
                                                            <asp:Label ID="lblh_ownership" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pass">Percent of Ownership</td>
                                                        <td>
                                                            <asp:Label ID="lblper" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pass">Document of ownership</td>
                                                        <td>
                                                            <asp:Label ID="lbldocu" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pass">Buisness Sector</td>
                                                        <td>
                                                            <asp:Label ID="lblbuissector" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </div>
                                                <tr>
                                                    <td class="pass">Date of Incorportaion of Company</td>
                                                    <td>
                                                        <asp:Label ID="lblDate_Incorportaion_Company" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="pass">Address</td>
                                                    <td>
                                                        <asp:Label ID="lbladdress" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="pass">City</td>
                                                    <td>
                                                        <asp:Label ID="lblcity" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="pass">State</td>
                                                    <td>
                                                        <asp:Label ID="lblstate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="pass">Pincode</td>
                                                    <td>
                                                        <asp:Label ID="lblpincode" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="pass">Name</td>
                                                    <td>
                                                        <asp:Label ID="lblname" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="pass">Email</td>
                                                    <td>
                                                        <asp:Label ID="lblemail" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="pass">Contact no</td>
                                                    <td>
                                                        <asp:Label ID="lblcontactno" runat="server"></asp:Label>
                                                    </td>
                                                    <tr>
                                                        <td class="pass">Fax no</td>
                                                        <td>
                                                            <asp:Label ID="lblfaxno" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tr>
                                                <tr>
                                                    <asp:GridView ID="gvnameof" runat="server" CssClass="table table-hover table-responsive" AutoGenerateColumns="false" Style="overflow: scroll;">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="EnterNameof" HeaderText="Type" />
                                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                                                            <asp:BoundField DataField="DIN_No" HeaderText="DIN No" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="up">
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

