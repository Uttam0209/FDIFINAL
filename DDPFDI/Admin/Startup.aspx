<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Startup.aspx.cs" Inherits="Admin_Startup" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="col-mod-12">
                <h3 class="page-header"><i class="fas fa-folder-plus"></i>Startup</h3>
                     <div class="UserInnerpage">
                        <div class="resitered">
                                <form>
                                    <div class="form-group">
                                        <label>Are You resitered with Start up India ? <label style="margin-right: 20px; margin-left: 10px;"><input type="checkbox" name="startup"><span></span>Yes</label><label><input type="checkbox" name="startup"><span></span>No</label></label> 
                                    </div>
                                </form>

                                <form>
                                    <div class="form-group">
                                        <div class="col-md-4"> <label>DIPP Number</label></div>
                                        <div class="col-md-8"> <input type="text" class="form-control"></div>
                                       
                                       
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-4"> <label>DIPP Linked Mobile </label></div>
                                        <div class="col-md-8"> <input type="text" class="form-control"></div>
                                        
                                    </div>
                                   <div class="col-md-12">
                                        <div class="form-submit">
                                         <input type="submit" value="Cancel" class="btn btn-danger pull-right">
                                        <input type="submit" value="Save" class="btn btn-primary pull-right">
                                       
                                    </div>
                                </form>
                        </div>
                     </div>
            </div>
            <div class="clearfix"></div>
        </div>
        <div class="footer">� 2019 <a href="#">Department of Defence Production</a> </div>
    </div>
</asp:Content>
