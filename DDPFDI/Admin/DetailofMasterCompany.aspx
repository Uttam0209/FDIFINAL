﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailofMasterCompany.aspx.cs" Inherits="Admin_DetailofMasterCompany" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
        }
    </script>

    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        };
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
                            <li><a href='<%=ResolveUrl("~/Detail-Company") %>'>Dashboard</a></li>
                            <li class="active">Companies Detail</li>
                        </ul>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-wrapper">
                                    <br />
                                    <div class="col-sm-4 row">
                                        <asp:TextBox ID="txtserch" runat="server" CssClass="form-cascade-control form-control" Placeholder="Type keyword to search"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 row">
                                        <asp:LinkButton runat="server" ID="btnsearch" class="text-black btn btn-warning pull-left btn-md" OnClick="Search_Click" Text="Search"></asp:LinkButton>
                                    </div>

                                    <a href="<%=ResolveUrl("~/Add-Company") %>" class="text-black btn btn-warning pull-right btn-md">Add company</a>

                                    <div class="clearfix"></div>
                                    <div class="text-center" style="font-size: 16px; margin-top: 10px;">
                                        Total number of  active companies :<strong>
                                            <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label></strong>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="table-wraper">
                                        <asp:GridView ID="gvcompanydetail" runat="server" Width="100%" Class="commonAjaxTbl table display responsive no-wrap table-hover manage-user" AutoGenerateColumns="false" AllowPaging="true"
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="25" AllowSorting="true" OnSorting="OnSorting" OnRowCommand="gvcompanydetail_RowCommand">
                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reference No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyRefNo") %>' NullDisplayText="#" SortExpression="CompanyRefNo"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="#" SortExpression="CompanyName" />
                                                <asp:BoundField DataField="IsJointVenture" HeaderText="Joint Venture" NullDisplayText="#" SortExpression="IsJointVenture" />
                                                <asp:TemplateField HeaderText="Defence Activities">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("IsDefenceActivity") %>' NullDisplayText="#" SortExpression="IsDefenceActivity"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblisactive" runat="server" Text='<%#Eval("IsActive") %>' NullDisplayText="#" SortExpression="IsActive"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfemail" Value='<%#Eval("ContactPersonEmailID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbledit" runat="server" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbldel" runat="server" CssClass="fa fa-trash" CommandName="DeleteComp" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Send Login">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbllogindetail" runat="server" CssClass=" fa fa-paper-plane" CommandName="SendLogin" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
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
                            <h4 class="modal-title">Company Detail</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body">
                                <div class="tab-pane fade active in" id="add-form">
                                    <table class="table table-bordered">
                                        <tbody>
                                            <caption>

                                                <tr>
                                                    <td class="pass" width="30%">Joint Venture </td>
                                                    <td>
                                                        <asp:Label ID="lbljointventure" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <caption>

                                                    <tr>
                                                        <td class="pass">Defence Activities </td>
                                                        <td>
                                                            <asp:Label ID="lbldefactivity" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pass" width="30%">Reference No </td>
                                                        <td>
                                                            <asp:Label ID="lblrefno" runat="server"></asp:Label>
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
                                                        <td class="pass">CEO Name</td>
                                                        <td>
                                                            <asp:Label ID="lblceoname" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pass">CEO Email</td>
                                                        <td>
                                                            <asp:Label ID="lblceoemail" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pass">Contact Person Name</td>
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
                                                        <td class="pass">Contact Person Email ID</td>
                                                        <td>
                                                            <asp:Label ID="lblcontactperemailid" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pass">CIN No</td>
                                                        <td>
                                                            <asp:Label ID="lblcinno" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="pass">PAN No</td>
                                                        <td>
                                                            <asp:Label ID="lblpanno" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td class="pass">GST No (If you use multiple gst no use with comma seprated)</td>
                                                        <td>
                                                            <asp:Label ID="lblgstno" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                                </caption>
                                            </caption>
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

