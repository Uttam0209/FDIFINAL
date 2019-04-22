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
                    <li class="active">Add Master Company</li>
                </ul>
            </div>
            <div class="col-sm-12">
                <asp:HiddenField ID="hfid" runat="server" />
                <asp:HiddenField ID="hfrole" runat="server" />

                <div class="addfdi">
                    <div class="col-md-12 col-mod-12">
                        <div class="row">
                            <asp:UpdatePanel ID="upfdival" runat="server">
                                <ContentTemplate>
                                    <div class="form-group" runat="server" id="mastercompany" visible="False">
                                        <label for="businesscode" id="lblmaster" class="control-label">
                                            <asp:Label ID="lblMastcompany" runat="server" Text=""></asp:Label>
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlmaster" AutoPostBack="True" CssClass="form-control form-cascade-control" OnSelectedIndexChanged="ddlmaster_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group" runat="server" id="masterfacotry" visible="False">
                                        <label for="businesscode" id="lblfactory" class="control-label">
                                            <asp:Label ID="lblfactoryName" runat="server" Text=""></asp:Label>
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlfacotry" AutoPostBack="True" CssClass="form-control form-cascade-control" >
                                        </asp:DropDownList>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="fdi-add-content">
                                                <div runat="server">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label for="businesscode" class="control-label">
                                                                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                                                </label>
                                                                <asp:TextBox ID="txtcomp" runat="server" class="form-control form-cascade-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label for="businesscode" class=" control-label">Nodel officer email</label>
                                                                <asp:TextBox ID="txtemail" runat="server" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$" class="form-control form-cascade-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>



                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" id="Intrested" visible="False" runat="server">
                                            <div class="fdi-add-content">

                                                <div class="form-group">
                                                    <h3 for="fdiinflow" class="secondary-heading">Intrested Area</h3>
                                                    <asp:CheckBoxList ID="chkintrestedarea" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" id="MenuAlot" visible="False" runat="server">
                                            <div class="fdi-add-content">
                                                <div class="form-group">
                                                    <h3 for="fdiinflow" class="secondary-heading">Master Menu Alot</h3>
                                                    <asp:CheckBoxList ID="chkmastermenuallot" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" id="Role" visible="False" runat="server">
                                            <div class="fdi-add-content">
                                                <div class="form-group">
                                                    <h3 for="fdiinflow" class="secondary-heading">Role</h3>
                                                    <asp:CheckBoxList ID="chkrole" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem Value="SuperAdmin">SuperAdmin</asp:ListItem>
                                                        <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                                        <asp:ListItem Value="Company">Company</asp:ListItem>
                                                        <%-- <asp:ListItem Value="Factory">Factory</asp:ListItem>
                                                    <asp:ListItem Value="Unit">Unit</asp:ListItem>--%>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="fdi-add-content">
                                                <div class="form-group">
                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm pull-right" OnClick="btncancel_Click" />
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit & Save" CssClass="btn btn-primary btn-sm pull-right" OnClick="btnsubmit_Click" />
                                                </div>
                                            </div>
                                            <div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>
            <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
        </div>
    </div>
</asp:Content>
