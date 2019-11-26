﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductNSNWise.aspx.cs" Inherits="Admin_ProductNSNWise" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <asp:HiddenField runat="server" ID="hidType" />
    <asp:HiddenField runat="server" ID="mRefNo" />
    <asp:HiddenField runat="server" ID="sundomaintype" />
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-mod-12 padding_0">
                    <ul class="breadcrumb">
                        <li>
                            <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label>
                        </li>
                    </ul>
                </div>
                <div class="col-md-12">
                    <div class="clearfix"></div>
                    <div style="margin-top: 5px;">
                        <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="addfdi">
                <div class="row">
                    <div class="col-md-12">
                        <div class="text-center">
                            <asp:Label ID="lblmsg" runat="server" CssClass="label label-default text-center"></asp:Label>
                        </div>
                        <div class="clearfix mt10"></div>
                        <asp:Panel ID="pan1" runat="server">
                            <div class="text-center" style="overflow-x: auto;">
                                <asp:GridView ID="gvnsngroup" runat="server" Class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                  display responsive no-wrap table-hover manage-user Grid table-responsive"
                                    AutoGenerateColumns="false" OnRowCreated="gvnsngroup_RowCreated" OnRowCommand="gvnsngroup_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Total" HeaderText="Total" />
                                        <asp:TemplateField HeaderText="NSN GROUP">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnsngroup" runat="server" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblnsngroupview" runat="server" class="fa fa-eye" CommandArgument='<%#Eval("SCategoryName") %>' CommandName="view"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <div class="clearfix mt10"></div>
                        <asp:Panel ID="pan2" runat="server" Visible="false">
                            <div class="text-center">
                                <asp:GridView ID="gvnsngroupclass" runat="server" AutoGenerateColumns="false"
                                    Class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                  display responsive no-wrap table-hover manage-user Grid table-responsive"
                                    OnRowCommand="gvnsngroupclass_RowCommand" OnRowCreated="gvnsngroupclass_RowCreated" OnRowDataBound="gvnsngroupclass_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Total" HeaderText="Total" />
                                        <asp:TemplateField HeaderText="NSN GROUP CLASS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnsngroupclass" runat="server" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Download Excel">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblnsngroupclassview" runat="server" Class="fa fa-file-excel" CommandArgument='<%#Eval("SCategoryName") %>' CommandName="exview"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
