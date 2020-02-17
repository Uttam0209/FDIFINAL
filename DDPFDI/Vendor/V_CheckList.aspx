<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_CheckList.aspx.cs" Inherits="Vendor_V_CheckList" MasterPageFile="~/Vendor/VendorMaster.master" %>

<asp:Content ID="ConHead" runat="server" ContentPlaceHolderID="head">
  
</asp:Content>

<asp:Content ID="Innercontent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
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
                    <div id="chkList" class="tab-pane">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="panchklist" runat="server">
                                    <asp:DropDownList ID="ddltypeofchk" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddltypeofchk_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div class="clearfix mt10"></div>
                                    <asp:CheckBoxList ID="CheckBoxList3" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Visible="false" RepeatLayout="Flow">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                                <div class="clearfix"></div>
                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary" OnClick="btnsubmit_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
