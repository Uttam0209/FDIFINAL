﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeFile="AddDesignation.aspx.cs" Inherits="Admin_AddDesignation" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">

</asp:Content>

<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:ScriptManager ID="sn" runat="server"></asp:ScriptManager>
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="addfdi">
                    <div class="col-md-12">
                        <div class="row">
                           <%-- <asp:UpdatePanel ID="upfdival" runat="server">
                                <ContentTemplate>--%>
                                    <div class="form-group" runat="server" id="mastercompany">
                                        <asp:HiddenField ID="hdid" runat="server" />
                                        <asp:Label ID="lblMastcompany" runat="server" Text="" CssClass="form-label"></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlmaster" AutoPostBack="True" CssClass="form-control form-cascade-control">
                                        </asp:DropDownList>
                                    </div>
                                    
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="fdi-add-content">
                                                <div id="Div1" runat="server">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class=" control-label">Designation </label>
                                                                <asp:TextBox ID="txtDesignation" runat="server" class="form-control form-cascade-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="fdi-add-content">
                                                <div class="form-group">
                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger pull-right" OnClick="btncancel_Click" />
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Save" CssClass="btn btn-primary pull-right" OnClick="btnsubmit_Click" OnClientClick="return Validate()" />
                                                </div>
                                            </div>
                                            <div>
                                            </div>
                                        </div>
                                    </div>
                                <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
        </div>
    </div>
</asp:Content>
