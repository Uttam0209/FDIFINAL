<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Specilization.aspx.cs" Inherits="Admin_Specilization" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="col-mod-12">
                <h3 class="page-header"><i class="fas fa-folder-plus"></i><asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></h3>
                 <div class="UserInnerpage">
                        <div class="resitered">
                            <form>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            
                                            <label><input type="checkbox">  <span></span>Mechanical</label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            
                                            <label><input type="checkbox"> <span></span>Electrical </label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            
                                            <label><input type="checkbox"><span></span>Civil</label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">

                                        <div class="form-group">
                                            
                                            <label><input type="checkbox"><span></span>Electronic</label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            
                                            <label><input type="checkbox"> <span></span>Chemical</label>
                                        </div>

                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            
                                            <label><input type="checkbox"> <span></span>Software Developement</label>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-submit">
                                        <input type="submit" value="Cancel" class="btn btn-danger pull-right">
                                        <input type="submit" value="Save" class="btn btn-primary pull-right">
                                         
                                    </div>
                                    </div>
                                    
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
