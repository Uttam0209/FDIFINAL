<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterdAs.aspx.cs" Inherits="Admin_RegisterdAs" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="col-mod-12">
                <h3 class="page-header"><asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></h3>
                <div class="UserInnerpage">
                    <div class="resitered">
                             <div class="row">
                     
                            <form>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Category 1</label>
                                        <input type="text" class="form-control">
                                     </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                         <label>Category 2</label>
                                         <input type="text" class="form-control">
                                     </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                       <label>Category 3</label>
                                       <input type="text" class="form-control">
                                    </div>
                                </div>
                                 <div class="col-md-12">
                                     <div class="form-group">
                                        <button type="button" value="Cancel" class="btn btn-danger pull-right" style="margin-right:0 !important;">Cancel</button>
                                        <input type="submit" value="Save" class="btn btn-primary pull-right">
                                </div>
                                 </div>
                            
                            </form>
         
                    </div>
                    
                    <div class="row">
                    
                            <form>
                                 <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Category 1</label>
                                        <select class="form-control">
                                            <option>Category 1</option>
                                            <option>Category 2</option>
                                        </select>
                                    </div>
                                 </div>
                                 <div class="col-md-4">
                                <div class="form-group">
                                    <label>Category 2</label>
                                      <select class="form-control">
                                        <option>Category 1</option>
                                        <option>Category 2</option>
                                     </select>
                                 </div>
                                 </div>
                                 <div class="col-md-4">
                                <div class="form-group">
                                    <label>Category 3</label>
                                     <select class="form-control">
                                        <option>Category 1</option>
                                        <option>Category 2</option>
                                     </select>
                                 </div>
                                 </div>
                                <div class="col-md-12">
                                <div class="form-group">
                                    <button type="button" value="Cancel" class="btn btn-danger pull-right" style="margin-right:0 !important;">Cancel</button>
                                    <input type="submit" value="Save" class="btn btn-primary pull-right">
                                </div>
                                </div>
                            </form>
                           
                        
                    </div>

                    </div>
                </div>

            </div>
            <div class="clearfix"></div>
        </div>
        <div class="footer">� 2019 <a href="#">Department of Defence Production</a> </div>
    </div>
</asp:Content>
