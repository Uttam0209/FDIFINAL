<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMasterCompany.aspx.cs" Inherits="Admin_AddMasterCompany" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
        <div class="sideBg">
            <div class="col-mod-12">
                <ul class="breadcrumb">
                    <li><a href="home">Dashboard</a></li>
                    <li class="active">Add FDI Inflow</li>
                </ul>
            </div>
            <div class="col-sm-12">
                <asp:HiddenField ID="hfid" runat="server" />
                <div id="demo1" runat="server">
                    <div class="addfdi">
                        <div class="col-md-12 col-mod-12">
                            <div class="row">
                                <asp:UpdatePanel ID="upfdival" runat="server" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                        <div class="col-md-6">
                                            <div class="fdi-add-content">
                                                <div runat="server">
                                                    <div class="form-group">
                                                        <label for="businesscode" class="control-label">Company Name </label>
                                                        <asp:TextBox ID="txtcomp" runat="server" class="form-control form-cascade-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="businesscode" class=" control-label">Email ID</label>
                                                        <asp:TextBox ID="txtemail" runat="server" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$" class="form-control form-cascade-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="fdiinflow" class="control-label">Intrested Area</label>
                                                        <asp:CheckBoxList ID="chkintrestedarea" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="fdi-add-content">
                                                <div class="form-group">
                                                    <label for="fdiinflow" class="control-label">Master Menu Alot</label>
                                                    <asp:CheckBoxList ID="chkmastermenuallot" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    </asp:CheckBoxList>
                                                </div>
                                                <div class="form-group">
                                                    <label for="fdiinflow" class="control-label">Role</label>
                                                    <asp:CheckBoxList ID="chkrole" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem Value="SuperAdmin">SuperAdmin</asp:ListItem>
                                                        <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                                        <asp:ListItem Value="ParentCompany">ParentCompany</asp:ListItem>
                                                        <asp:ListItem Value="SubCompany">SubCompany</asp:ListItem>
                                                        <asp:ListItem Value="Unit">Unit</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-warning btn-sm pull-right" />
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit & Save" CssClass="btn btn-success btn-sm pull-right" />
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
        </div>
    </div>
</asp:Content>
