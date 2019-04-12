<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterdAs.aspx.cs" Inherits="Admin_RegisterdAs" MasterPageFile="~/Admin/MasterPage.master" %>

    <asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    </asp:Content>
    <asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <div class="content oem-content">
            <div class="sideBg">
                <div class="col-mod-12">
                    <h3 class="page-header"><i class="fas fa-folder-plus"></i>Registerd As</h3>
                    <div class="UserInnerpage">
                        <div class="resitered">
                            <form>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            
                                            <label><input type="checkbox">  <span></span>FDI Received</label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            
                                            <label><input type="checkbox"> <span></span>Indigenisation </label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            
                                            <label><input type="checkbox"><span></span>Defence Export </label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">

                                        <div class="form-group">
                                            
                                            <label><input type="checkbox"><span></span>Defence Production </label>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            
                                            <label><input type="checkbox"> <span></span>Export Lead </label>
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