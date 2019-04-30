<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterdAs.aspx.cs" Inherits="Admin_RegisterdAs" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" ChildrenAsTriggers="True">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="col-mod-12">
                        <asp:HiddenField runat="server" id="hfCatID"/>
                        <asp:HiddenField runat="server" id="hfSubCatID"/>
                        <h3 class="page-header"><i class="fas fa-folder-plus"></i>
                            <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></h3>
                        <div class="UserInnerpage">
                            <div class="resitered">
                                <div class="row">
                                    <div class="col-md-4" runat="server" id="divcategory1textbox" visible="False">
                                        <div class="form-group">
                                            <label>Product Category</label>
                                            <asp:TextBox class="form-control" runat="server" ID="txtmastercategory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4" runat="server" id="divcategory1dropdown" visible="False">
                                        <div class="form-group">
                                            <label>Main Category</label>
                                            <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4" runat="server" id="divcategory2textbox" visible="False">
                                        <div class="form-group">
                                            <label>Level 1 Category</label>
                                            <asp:TextBox class="form-control" runat="server" ID="txtsubcategory"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4" runat="server" id="divcategory2ddl" visible="False">
                                        <div class="form-group">
                                            <label>Level 1 Category</label>
                                            <asp:DropDownList runat="server" ID="ddlcategroy2" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4" runat="server" id="divcategory3textbox" visible="False">
                                        <div class="form-group">
                                            <label>Level 2 Category</label>
                                            <asp:TextBox class="form-control" runat="server" ID="txtcategory3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:LinkButton ID="btncancle" runat="server" class="btn btn-danger pull-right" Text="Cancel" Style="margin-right: 0 !important;" OnClick="btncancle_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="btnsave" runat="server" Text="Save Category" class="btn btn-primary pull-right" OnClick="btnsave_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="footer">� 2019 <a href="#">Department of Defence Production</a> </div>
            </div>
        </ContentTemplate>
        <Triggers></Triggers>
    </asp:UpdatePanel>
</asp:Content>
