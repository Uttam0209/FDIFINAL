<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNodalOfficer.aspx.cs" Inherits="Admin_AddNodalOfficer" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="AddNodal" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:scriptmanager id="sc" runat="server"></asp:scriptmanager>
        <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
        <asp:HiddenField ID="hidType" runat="server" />
        <div class="sideBg">
            <div class="row">
                <div class="col-mod-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="NodalOfficer">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Select Company</label>
                            <asp:dropdownlist runat="server" id="ddlcompany" class="form-control" autopostback="True" onselectedindexchanged="ddlcompany_OnSelectedIndexChanged"></asp:dropdownlist>
                        </div>
                    </div>
                    <div class="col-md-4" runat="server" id="lblselectdivison">
                        <div class="form-group">
                            <label>Select Division/Plant</label>
                            <asp:dropdownlist runat="server" id="ddldivision" class="form-control" autopostback="True" onselectedindexchanged="ddldivision_OnSelectedIndexChanged"></asp:dropdownlist>
                        </div>
                    </div>
                    <div class="col-md-4" runat="server" id="lblselectunit">
                        <div class="form-group">
                            <label>Select Unit</label>
                            <asp:dropdownlist runat="server" id="ddlunit" class="form-control" autopostback="True" onselectedindexchanged="ddlunit_OnSelectedIndexChanged"></asp:dropdownlist>
                        </div>
                    </div>
                </div>
                <div class="add-profile">
                    <div class="section-pannel">
                        <div class="row">
                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Emp Code</label>
                                    <asp:textbox class="form-control" runat="server" id="txtEmpCode"></asp:textbox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Name</label>
                                    <asp:textbox class="form-control" runat="server" id="txtname"></asp:textbox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Designation</label>
                                    <asp:dropdownlist runat="server" id="ddldesignation" class="form-control" />
                                </div>
                            </div>
                            
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Email ID</label>
                                    <asp:textbox class="form-control" runat="server" id="txtemailid"></asp:textbox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Mobile</label>
                                    <asp:textbox class="form-control" runat="server" id="txtmobile"></asp:textbox>
                                </div>
                            </div>
                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Telephone </label>
                                    <asp:textbox class="form-control" runat="server" id="txttelephone"></asp:textbox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Fax </label>
                                    <asp:textbox class="form-control" runat="server" id="txtfax"></asp:textbox>
                                </div>
                            </div>

                           

                        </div>
                       
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:linkbutton id="btnsub" runat="server" text="Save" class="btn btn-primary pull-right" onclick="btnsub_Click"></asp:linkbutton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
