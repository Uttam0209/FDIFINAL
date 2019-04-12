<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MSME.aspx.cs" Inherits="Admin_MSME" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="col-mod-12">
                <h3 class="page-header"><i class="fas fa-folder-plus"></i>MSME</h3>
                <div class="UserInnerpage">
                        <div class="resitered">
                                <form>
                                    <div class="form-group">
                                        <label> <input type="checkbox" name="startup"><span></span>Are you registerd with MSME as a micro or small Enterprise</label> 
                                    </div>
                                </form>

                                <form>
                                    <div class="form-group">
                                        <div class="col-md-3"> <label>VAM</label></div>
                                        <div class="col-md-9"> <input type="text" class="form-control"></div>
                                       
                                       
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3"> <label>Aadhar /Mobile</label></div>
                                        <div class="col-md-9"> <input type="text" class="form-control"></div>
                                        
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
