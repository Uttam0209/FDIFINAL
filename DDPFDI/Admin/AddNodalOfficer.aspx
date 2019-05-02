<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNodalOfficer.aspx.cs" Inherits="Admin_AddNodalOfficer" MasterPageFile="~/Admin/MasterPage.master" %>
<asp:Content ID="AddNodal" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
     <div class="content oem-content">
        <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <div class="sideBg">
        <div class="row">
         <div class="col-mod-12 padding_0">
               <ul class="breadcrumb">
                    <li class="active"><asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></li>
                </ul>
            </div>
         </div>
    <div class="NodalOfficer">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <select class="form-control">
                        <option>Select</option>
                    </select>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <select class="form-control">
                        <option>Select</option>
                    </select>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <select class="form-control">
                        <option>Select</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>Name</label>
                    <input type="text" class="form-control" />
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Designation</label>
                    <input type="text" class="form-control" />
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Department</label>
                    <input type="text" class="form-control" />
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
                     <asp:LinkButton ID="btnsub" runat="server" Text="Save" class="btn btn-primary pull-right"></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    </div>
    </div>

</asp:Content>