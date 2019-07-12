<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorRegistrationStep1.aspx.cs" Inherits="Vendor_VendorRegistrationStep1" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="ConHead" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Innercontent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="container">
                <div class="cacade-forms">
                    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:Panel ID="panstep1" runat="server">
                                <h3>DDP Vendor Registration</h3>
                                <p>Please provide all required details to register your business with us</p>
                                <div class="clearfix mt10"></div>

                                <div class="form-group">
                                    <div class="col-sm-5">
                                        Are you Registered with GST 
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlregisterdgst" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlregisterdgst_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Y">YES</asp:ListItem>
                                            <asp:ListItem Value="N">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="divgst" visible="false">
                                    <div class="col-sm-5">
                                        GSTIN No 
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtgstno" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-5">
                                        Business Name 
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtbusinessname" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-5">
                                        Nodal Officer <span class="mandatory">*</span>
                                    </div>
                                    <div class="col-sm-7">
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddltittle" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                                    <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                                    <asp:ListItem Value="Miss">Miss.</asp:ListItem>
                                                    <asp:ListItem Value="Dr">Dr.</asp:ListItem>
                                                </asp:DropDownList>
                                                <p>Prefix</p>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtfirstname" runat="server" required="" CssClass="form-control"></asp:TextBox>
                                                <p>First Name</p>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtmiddlename" runat="server" CssClass="form-control"></asp:TextBox>
                                                <p>Middle Name</p>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtlastname" runat="server" CssClass="form-control"></asp:TextBox>
                                                <p>Last Name</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-5">
                                        Type of Business
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddltypeofbusiness" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-5">
                                        Business Sector
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlbusinesssector" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-5">
                                        Address
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtstreetaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                        <p>Street Address</p>
                                        <div class="clearfix mt10"></div>
                                        <asp:TextBox ID="txtstreetaddressline2" runat="server" CssClass="form-control"></asp:TextBox>
                                        Street Address Line 2   
                                <div class="clearfix mt10"></div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtcity" runat="server" CssClass="form-control"></asp:TextBox>
                                                <p>City</p>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtstateprovince" runat="server" CssClass="form-control"></asp:TextBox>
                                                <p>State / Province</p>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtpostalzipcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                <p>Postal / Zip Code</p>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlcountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <p>Country</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-5">
                                        Email <span class="mandatory">*</span>
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtemail" runat="server" TextMode="Email" placeholder="ex: myemail@example.com" required="" AutoCompleteType="Email" CssClass="form-control"></asp:TextBox>
                                        <p>ex: myemail@example.com</p>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-5">
                                        Contact No
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtstdcode" runat="server" CssClass="form-control"></asp:TextBox>

                                        <p>STD Code</p>
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                                        <p>Phone Number</p>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-5">
                                        Fax No
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtfaxstdcode" runat="server" CssClass="form-control"></asp:TextBox>
                                        <p>STD Code</p>
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtfaxphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                                        <p>Phone Number</p>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary pull-right" Text="Submit Registration" OnClick="btnsubmit_Click" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="up">
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
