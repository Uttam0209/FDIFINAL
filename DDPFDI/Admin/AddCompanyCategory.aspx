<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCompanyCategory.aspx.cs" Inherits="Admin_AddCompanyCategory" MasterPageFile="MasterPage.master" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:ScriptManager ID="sn" runat="server"></asp:ScriptManager>
        <asp:HiddenField runat="server" ID="hidType" />
        <asp:HiddenField runat="server" ID="hfcomprefno" />
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="row">
                <asp:UpdatePanel runat="server" ID="updrop">
                    <ContentTemplate>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Select Company</label>
                                <asp:DropDownList runat="server" ID="ddlcompany" class="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblselectdivison">
                            <div class="form-group">
                                <label>Select Division/Palnt</label>
                                <asp:DropDownList runat="server" ID="ddldivision" class="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblselectunit">
                            <div class="form-group">
                                <label>Select Unit</label>
                                <asp:DropDownList runat="server" ID="ddlunit" class="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="section-pannel">
                <div class="row">
                    <asp:UpdatePanel runat="server" ID="up">
                        <ContentTemplate>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Dropdown Label</label>
                                    <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-md-6" id="level1" runat="server" Visible="False">
                                <div class="form-group">
                                    <h3 class="secondary-heading">Level 1</h3>
                                    <asp:CheckBoxList ID="chkSubCategory" runat="server" CssClass="checkbox-inline" RepeatColumns="25" RepeatDirection="Vertical"
                                        RepeatLayout="Flow">
                                    </asp:CheckBoxList>

                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <asp:LinkButton ID="btndemofirst" runat="server" CssClass="btn btn-primary pull-right" Style="margin-left: 10px;" Text="Save"
                                        OnClick="btndemofirst_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
        </div>
    </div>
</asp:Content>
