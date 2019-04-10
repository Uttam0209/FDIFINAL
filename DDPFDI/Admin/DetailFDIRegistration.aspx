<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailFDIRegistration.aspx.cs" Inherits="Admin_DetailFDIRegistration" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <style>
        .GridPager a,
        .GridPager span {
            display: inline-block;
            padding: 0px 9px;
            margin-right: 4px;
            border-radius: 3px;
            border: solid 1px #c0c0c0;
            background: #e9e9e9;
            box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
            font-size: .875em;
            font-weight: bold;
            text-decoration: none;
            color: #717171;
            text-shadow: 0px 1px 0px rgba(255,255,255, 1);
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }

        .GridPager span {
            background: #616161;
            box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
            color: #f0f0f0;
            text-shadow: 0px 0px 3px rgba(0,0,0, .5);
            border: 1px solid #3AC0F2;
        }
    </style>
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="col-mod-12">
                        <ul class="breadcrumb">
                            <li><a href="home">Dashboard</a></li>
                            <li class="active">FDI Registration Detail</li>
                        </ul>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-wrapper">
                                    <br />
                                    <div class="col-sm-4 row">
                                        <asp:TextBox ID="txtserch" runat="server" CssClass="form-cascade-control form-control" Placeholder="Search"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 row">
                                        <asp:LinkButton runat="server" ID="btnsearch" class="text-black btn btn-warning pull-left btn-md" OnClick="Search_Click" Text="Search"></asp:LinkButton>
                                    </div>

                                    <a href='<%=ResolveUrl("~/FDIRegistration") %>' class="text-black btn btn-warning pull-right btn-md">Add FDI</a>

                                    <br />
                                    <br />
                                    <asp:GridView ID="gvdetail" runat="server" Width="100%" Class="commonAjaxTbl table display responsive no-wrap table-hover manage-user" AutoGenerateColumns="false" AllowPaging="true"
                                        OnPageIndexChanging="OnPageIndexChanging" PageSize="10" AllowSorting="true" OnSorting="OnSorting" OnRowCommand="gvdetail_RowCommand">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" NullDisplayText="#" SortExpression="CompanyName" />
                                            <asp:BoundField DataField="ApprovalNo" HeaderText="Approved Code" NullDisplayText="#" SortExpression="ApprovalNo" />
                                            <asp:BoundField DataField="CodeofBusiness" HeaderText="Business Code" NullDisplayText="#" SortExpression="CodeofBusiness" />
                                            <asp:BoundField DataField="BriefDescription" HeaderText="Brief Description" NullDisplayText="#" SortExpression="BriefDescription" />
                                            <asp:BoundField DataField="CountryName" HeaderText="Country" NullDisplayText="#" SortExpression="CountryName" />
                                            <asp:BoundField DataField="Currency" HeaderText="Currency" NullDisplayText="#" SortExpression="Currency" />
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbledit" runat="server" CssClass="fa fa-edit" CommandName="Edit" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="View" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbldel" runat="server" CssClass="fa fa-trash" CommandName="Delete" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
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
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title" modal-title1>Detail of FDI Registration</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body">
                                <div class="tab-pane fade active in" id="add-form">
                                    <table class="table table-bordered">
                                        <tbody>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <h3 class="hhead">Indian Company Details</h3>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td width="30%" class="pass">Joint Venture </td>
                                                <td>
                                                    <asp:Label ID="lbljointventure" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Company Name</td>
                                                <td>
                                                    <asp:Label ID="lblcompanyname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Address</td>
                                                <td>
                                                    <asp:Label ID="lbladdress" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">State</td>
                                                <td>
                                                    <asp:Label ID="lblstate" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="pass">Pin Code</td>
                                                <td>
                                                    <asp:Label ID="lblpincode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">CEO Email</td>
                                                <td>
                                                    <asp:Label ID="lblceoemail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">CEO Name</td>
                                                <td>
                                                    <asp:Label ID="lblceoname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Contact Person  Name</td>
                                                <td>
                                                    <asp:Label ID="lblcontactpersonname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Contact Person Contact No</td>
                                                <td>
                                                    <asp:Label ID="lblcontactpersonmobno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">CIN No</td>
                                                <td>
                                                    <asp:Label ID="lblcinno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">PAN No. </td>
                                                <td>
                                                    <asp:Label ID="lblpanno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">GST No. (If you use multiple gst no use with comma seprated)</td>
                                                <td>
                                                    <asp:Label ID="lblgstno" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="pass">Company engaged in Defence Activities </td>
                                                <td>
                                                    <asp:Label ID="lbldefactivity" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Contact Person Email ID</td>
                                                <td>
                                                    <asp:Label ID="lblcontactperemailid" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td>
                                                    <h3 class="hhead">National Industrial Classification(NIC)</h3>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="pass">Code of Business.</td>
                                                <td>
                                                    <asp:Label ID="lblcodeofbuis" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">In Case of. </td>
                                                <td>
                                                    <asp:Label ID="lblcaseof" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Approval No. </td>
                                                <td>
                                                    <asp:Label ID="lblaprrovalno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Approval Date.</td>
                                                <td>
                                                    <asp:Label ID="lblapprovaldate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <h3 class="hhead">Foreign Investor Details</h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Foreign Company Name.</td>
                                                <td>
                                                    <asp:Label ID="lblforeigncompname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Address</td>
                                                <td>
                                                    <asp:Label ID="lbladdress1" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Country. </td>
                                                <td>
                                                    <asp:Label ID="lblcountry" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Zip Code.</td>
                                                <td>
                                                    <asp:Label ID="lblzipcode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Foreign engaged in Defence Activities.</td>
                                                <td>
                                                    <asp:Label ID="lblfordefactivity" runat="server"></asp:Label>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td></td>
                                                <td>
                                                    <h3 class="hhead">FDI Value </h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">FDI Value Type</td>
                                                <td>
                                                    <asp:Label ID="lblfdivalue" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Period of reporting </td>
                                                <td>
                                                    <asp:Label ID="lblperiodofreprot" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Currency.</td>
                                                <td>
                                                    <asp:Label ID="lblcureency" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Total FDI Inflow.</td>
                                                <td>
                                                    <asp:Label ID="lbltotalfdiinflow" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Equivalent INR @ Monthly Average Exchange Rate of RBI.</td>
                                                <td>
                                                    <asp:Label ID="lbleqinr" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">After Exchange Total Amount.</td>
                                                <td>
                                                    <asp:Label ID="lbltotamount" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td>
                                                    <h3 class="hhead">Source of Information collected</h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Select Source of Information..</td>
                                                <td>
                                                    <asp:Label ID="lblsourceinfo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Date of receiving information</td>
                                                <td>
                                                    <asp:Label ID="lblrecinfo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Authenticity of Information.</td>
                                                <td>
                                                    <asp:Label ID="lblauthinfo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Remarks</td>
                                                <td>
                                                    <asp:Label ID="lblremark" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Document attached if any (Email/Letter etc.)</td>
                                                <td>
                                                    <a runat="server" id="linkpath" target="_blank">Click to See Document</a>
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" data-dismiss="modal">Cancel</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
