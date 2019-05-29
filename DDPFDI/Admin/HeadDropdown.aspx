<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HeadDropdown.aspx.cs" Inherits="Admin_HeadDropdown" MasterPageFile="MasterPage.master" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updrop">
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblcompname" runat="server" CssClass="form-label">Select Company/Organization</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-cascade-control form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" runat="server" id="divlblselectdivison">
                                    <div class="form-group">
                                        <asp:Label ID="lbldivisionname" runat="server" CssClass="form-label">Select Division/Plant</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-cascade-control form-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" runat="server" id="divlblselectunit">
                                    <div class="form-group">
                                        <asp:Label ID="lblunitname" runat="server" CssClass="form-label">Select Unit</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-cascade-control form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updrop">
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
