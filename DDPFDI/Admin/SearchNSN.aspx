<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchNSN.aspx.cs" Inherits="Admin_SearchNSN" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="clearfix"></div>
                                <div style="margin-top: 5px;">
                                    <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                                </div>
                                <div class="clearfix"></div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>Search Item wise nsn code</label>
                                        <asp:DropDownList runat="server" ID="ddlitem" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlitem_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>Search Item wise nsn group class</label>
                                        <asp:DropDownList runat="server" ID="ddlnsnclass" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlnsnclass_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>Search Item wise nsn group</label>
                                        <asp:DropDownList runat="server" ID="ddlnsngroup" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlnsngroup_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <asp:Button runat="server" ID="btnsearch" Text="Search" OnClick="btnsearch_Click" />
                                </div>
                                <div class="table-wrapper">
                                    <div class="table-wraper">
                                    </div>
                                </div>
                            </div>
                        </div> 
                    </form>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
