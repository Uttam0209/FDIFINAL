<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_RegistrationNo.aspx.cs" Inherits="Vendor_V_RegistrationNo" MasterPageFile="~/Vendor/VendorMaster.master" %>

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
                    <div id="qpt" class="tab-pane">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="panstep3" runat="server">
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Do you have TAN
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddldetailofpantan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldetailofpantan_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">NO</asp:ListItem>
                                                <asp:ListItem Value="2">YES</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="divpantan" visible="false">
                                        <div class="col-sm-5">
                                            <asp:Label ID="lblpantan" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtpantan" runat="server" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            GSTIN
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtgstin" runat="server" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            UAM
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtUAM" runat="server" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            <asp:Label ID="lblcin" runat="server" Text="CIN"></asp:Label>
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtCIN" runat="server" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Are you registered with Govt. Department/Undertaking/PSU under Ministry of Defence/Gem
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlDepartmentUndertakingPSU" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlDepartmentUndertakingPSU_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">YES</asp:ListItem>
                                                <asp:ListItem Value="2">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div runat="server" id="divgovtundertaking" visible="false">
                                        <asp:GridView runat="server" ID="gvgovtundertaking" CssClass="table table-hover" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowCreated="gvgovtundertaking_RowCreated"
                                            CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="SrNoGovt" HeaderText="Sr.No" />
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtnameundertaking" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Registration No">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtregisnogovtpsu" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Certificate valid upto">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtcertificatevalidupto" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Please Upload Registration Certificate">
                                                    <ItemTemplate>
                                                        <asp:FileUpload ID="furegiscerti" runat="server" CssClass="form-control" />
                                                        <asp:HiddenField ID="hffuregiscerti" runat="server" />
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnAddmoreGovtpsu" runat="server" CssClass="btn btn-primary pull-right"
                                                            Text="Add New Row"
                                                            OnClick="btnAddmoreGovtpsu_Click" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbremoveGOvtPSU" runat="server" CssClass="fa fa-times"
                                                            OnClick="lbremoveGOvtPSU_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                        <asp:GridView runat="server" ID="gvgovtundertakingedit" CssClass="table table-hover" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowCreated="gvgovtundertaking_RowCreated"
                                            CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Name_PSU_UnderGovt" HeaderText="Name" />
                                                <asp:BoundField DataField="RegistrationNo" HeaderText="Registration No" />
                                                <asp:BoundField DataField="Certificate_valid_upto" HeaderText="Certificate Valid Upto" />
                                                <asp:TemplateField HeaderText="Uploaded Registration Certificate">
                                                    <ItemTemplate>
                                                        <asp:Image runat="server" ID="img" src='<%#Eval("Upload_Registration_Certificate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblsave" runat="server" CssClass="fa fa-save" CommandName="newsave" CommandArgument='<%#Eval("MasterId") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="lblupdate" runat="server" CssClass="fa fa-edit" CommandName="newedit" CommandArgument='<%#Eval("MasterId") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="lbldelete" runat="server" CssClass="fa fa-trash" CommandName="newdel" CommandArgument='<%#Eval("MasterId") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                        <div class="clearfix mt10"></div>
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right mr10" OnClick="btnsubmit_Click" />
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnsubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
