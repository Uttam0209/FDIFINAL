﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="col-mod-12">
                <h3 class="page-header"><i class="fas fa-folder-plus"></i>Change Password </h3>
                <div class="col-md-12 col-mod-12">
                    <div class="row">

                        <div class="col-sm-6">
                            <div class="indiacompanydetails">
                                <div class="form-group">
                                    <label for="activityname" class="control-label">Old Password <span class="mandatory">*</span></label>
                                    <asp:TextBox runat="server" ID="txtoldpass" type="password" MaxLength="15" TabIndex="1" class="form-control form-cascade-control input-small" placeholder="Enter old password">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="activityname" class="control-label">Confirm Password <span class="mandatory">*</span></label>
                                    <asp:TextBox runat="server" ID="txtreppass" type="password" MaxLength="15" TabIndex="3" class="form-control  form-cascade-control input-small" placeholder="Confirm New Password"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="tcompanyname" class=" control-label">New Password <span class="mandatory">*</span></label>
                                <asp:TextBox runat="server" ID="txtnewpass" type="password" MaxLength="15" TabIndex="2" class="form-control  form-cascade-control input-small" placeholder="Enter New Password"></asp:TextBox>
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:LinkButton runat="server" ID="btncan" class="btn btn-warning pull-right" TabIndex="5" data-dismiss="modal" Text="Cancel" OnClick="btncan_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnsub" class="btn btn-primary pull-right" Style="margin-right: 15px;" TabIndex="4" Text="Update Password" OnClick="btnsub_Click"></asp:LinkButton>

                            </div>

                        </div>
                        <div class="clearfix"></div>
                        <div class="form-group" runat="server" id="divmsg">
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
        <div class="footer">� 2019 <a href="#">Department of Defence Production</a> </div>
    </div>
</asp:Content>