<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_Declaration.aspx.cs" Inherits="Vendor_V_Declaration" MasterPageFile="~/Vendor/VendorMaster.master" %>

<asp:Content ID="ConHead" runat="server" ContentPlaceHolderID="head"></asp:Content>
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
                    <div id="spd" class="tab-pane">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="panstep5" runat="server">
                                            <p>1.Has the firm declared insolvent in Receivership ,Bankrupt or being wounded up.</p>
                                            <asp:DropDownList ID="ddlwoundedup" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <p>2.Have firm affairs administered by a court or a judicial officer.</p>
                                            <asp:DropDownList ID="ddljudicialofficer" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <p>3.Is business activities suspended.</p>
                                            <asp:DropDownList ID="ddlbusinesssuspended" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <p>4.Is the firm subject of legal proceedings for any of the forging reasons.</p>
                                            <asp:DropDownList ID="ddlforgingreasone" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <p>5.Has the firm been debarred from Govt. Contracts</p>
                                            <asp:DropDownList ID="ddldebarredgovtcont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldebarredgovtcont_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <div class="col-sm-5">
                                                <asp:CheckBoxList ID="chkcontracts" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="chkcontracts_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    <asp:ListItem Value="1">Financial</asp:ListItem>
                                                    <asp:ListItem Value="2">Banning</asp:ListItem>
                                                    <asp:ListItem Value="3">Suspension</asp:ListItem>
                                                    <asp:ListItem Value="4">Tender holiday</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-sm-7">
                                                <div runat="server" id="divfin" visible="false">
                                                    <div class="">
                                                        <p>Applicable upto</p>
                                                        <asp:TextBox ID="txtdatestsrt" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div runat="server" id="div12" visible="false">
                                                    <div class="">
                                                        <p>Applicable upto</p>
                                                        <asp:TextBox ID="TextBox12" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div runat="server" id="div13" visible="false">
                                                    <div class="">
                                                        <p>Applicable upto</p>
                                                        <asp:TextBox ID="TextBox24" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div runat="server" id="div14" visible="false">
                                                    <div class="">
                                                        <p>Applicable upto</p>
                                                        <asp:TextBox ID="TextBox26" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <asp:CheckBoxList ID="chkbuisness" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="checkbox">
                                                <asp:ListItem>I/We note that registration ,does not carry with it the right to business with DPSUs/OFB, I/We hereby declare that the information pertaining to my/our firm/Company including all enclosures is correct and true to the best of
                                                   my/our knowledge and belief as on date</asp:ListItem>
                                            </asp:CheckBoxList>
                                            <div class="clearfix mt10"></div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
