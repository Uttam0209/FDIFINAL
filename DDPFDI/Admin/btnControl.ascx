<%@ Control Language="C#" AutoEventWireup="true" CodeFile="btnControl.ascx.cs" Inherits="Admin_btnControl" %>
<div class="row">
    <div class="col-md-12">
        <div class="fdi-add-content">
            <div class="form-group">
                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm pull-right"   />
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm pull-right"  />
                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary btn-sm pull-right"  />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-primary btn-sm pull-right" />
            </div>
        </div>
        <div>
        </div>
    </div>
</div>
