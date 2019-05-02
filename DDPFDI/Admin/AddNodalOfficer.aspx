<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNodalOfficer.aspx.cs" Inherits="Admin_AddNodalOfficer" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="AddNodal" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:scriptmanager id="sc" runat="server"></asp:scriptmanager>
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
                            <label>Select Division/Palnt</label>
                            <asp:dropdownlist runat="server" id="ddldivision" class="form-control" autopostback="True" onselectedindexchanged="ddldivision_OnSelectedIndexChanged"></asp:dropdownlist>
                        </div>
                    </div>
                    <div class="col-md-4" runat="server" id="lblselectunit">
                        <div class="form-group">
                            <label>Select Unit</label>
                            <asp:dropdownlist runat="server" id="ddlunit" class="form-control" autopostback="True"></asp:dropdownlist>
                        </div>
                    </div>
                </div>
                <div class="add-profile">
                    <div class="section-pannel">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Name</label>
                                    <input type="text" class="form-control" />
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Department</label>
                                    <asp:dropdownlist runat="server" id="ddldepartment" class="form-control" />
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
                                    <input type="text" class="form-control" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Mobile</label>
                                    <input type="text" class="form-control" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Fax </label>
                                    <input type="text" class="form-control" />
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:linkbutton id="btnsub" runat="server" text="Save" class="btn btn-primary pull-right"></asp:linkbutton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
