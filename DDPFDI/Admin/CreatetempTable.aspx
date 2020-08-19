﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreatetempTable.aspx.cs" Inherits="Admin_CreatetempTable" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel runat="server" ID="updatepan">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                            <div id="ContentPlaceHolder1_divHeadPage">
                                <ul class="breadcrumb">
                                    <li class=""><span>Update User Product</span></li>

                                </ul>
                            </div>
                        </div>
                        <div class="col-md-12" style="text-align: center; margin-top: 200px;">
                            <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-primary text-center" Text="Click me to update record" OnClick="btnupdate_Click" />
                            <div class="clearfix mt10"></div>
                            <asp:Label ID="lbl" runat="server" Text="" CssClass="text-center"></asp:Label>
                            <div class="clearfix mt10"></div>
                            <asp:LinkButton ID="lbldownloadexcel" runat="server" CssClass="fa fa-download btn btn-success" OnClick="lbldownloadexcel_Click">&nbsp;Download Excel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepan">
        <ProgressTemplate>
            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p>Processing</p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
