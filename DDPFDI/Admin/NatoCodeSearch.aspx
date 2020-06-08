<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NatoCodeSearch.aspx.cs" Inherits="Admin_NatoCodeSearch" ValidateRequest="false" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:HiddenField ID="hfcomprefno" runat="server" />
    <asp:HiddenField ID="hidType" runat="server" />
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
                <div class="col-md-12">
                    <div class="add-profile">
                        <div class="section-pannel">
                            <asp:UpdatePanel runat="server" ID="upproduct" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    NATO SUPPLY GROUP <span class="mandatory">*</span>
                                                    <span data-toggle="tooltip" class="fa fa-question" title="Number in bracket indicates NATO SUPPLY GROUP"></span>
                                                </label>
                                                <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" Style="text-transform: uppercase !important;" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>NATO SUPPLY CLASS<span class="mandatory">*</span></label>
                                                <span data-toggle="tooltip" class="fa fa-question" title="Number in bracket indicates NATO SUPPLY CLASS"></span>
                                                <asp:DropDownList runat="server" ID="ddlsubcategory" AutoPostBack="True" TabIndex="2" class="form-control" Style="text-transform: uppercase !important;" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    Item Code<span class="mandatory">*</span></label>
                                                <span data-toggle="tooltip" class="fa fa-question" title="Item code indicate item name code in NSN"></span>
                                                <div class="col-md-11 row">
                                                    <asp:DropDownList runat="server" ID="ddllevel3product" AutoPostBack="True" TabIndex="3" Style="text-transform: uppercase !important;" class="form-control" OnSelectedIndexChanged="ddllevel3product_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblfinalname" runat="server" CssClass="label label-big label-info"></asp:Label>
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
