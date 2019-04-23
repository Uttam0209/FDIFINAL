<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="Admin_AddProduct" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header"><i class="fas fa-folder-plus"></i><asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></h3>
                
                 </div>
                 <div class="row">
                     <div class="col-md-12">
                         <div class="add-profile">
                             <form>
                                 <div class="form-group">
                                     <div class="col-md-3">
                                         <label>Product Nomenclature :</label>
                                     </div>
                                     <div class="col-md-9">
                                         <input type="text" class="form-control">
                                     </div>
                                 </div>
                                 <div class="form-group">
                                     <div class="col-md-3">
                                         <label>Product Description :</label>
                                     </div>
                                     <div class="col-md-9">
                                         <textarea class="form-control"></textarea>
                                     </div>
                                 </div>
                                  <div class="form-group">
                                     <div class="col-md-3">
                                         <label>Product Image :</label>
                                     </div>
                                     <div class="col-md-9">
                                         <input type="file" class="btn btn-primary">
                                     </div>
                                 </div>
                                 <div class="form-group">
                                     <div class="col-md-3">
                                         <label>Product Category :</label>
                                     </div>
                                     <div class="col-md-9">
                                         <div class="row">
                                            <div class="col-md-6">
                                                <select class="form-control">
                                                    <option>Select Category</option>
                                                </select>
                                            </div>
                                            <div class="col-md-6">
                                                <select class="form-control">
                                                    <option>Select Sub Category</option>
                                                </select>
                                            </div>
                                         </div>
                                            
                                     </div>
                                 </div>
                                  <div class="form-group">
                                     <div class="col-md-3">
                                         <label>Technology Category :</label>
                                     </div>
                                     <div class="col-md-9">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <select class="form-control">
                                                    <option>Select Category</option>
                                                </select>
                                            </div>
                                            <div class="col-md-6">
                                                <select class="form-control">
                                                    <option>Select Sub Category</option>
                                                </select>
                                            </div>
                                        </div>
                                            
                                     </div>
                                 </div>
                                 <div class="form-group">
                                     <div class="col-md-3">
                                         <label>End User :</label>
                                     </div>
                                     <div class="col-md-9">
                                        <select class="form-control">
                                            <option>Select</option>
                                        </select>
                                     </div>
                                 </div>
                                 <div class="form-group">
                                     <div class="col-md-3">
                                         <label>Product Already Indeginized :</label>
                                     </div>
                                     <div class="col-md-9">
                                        <div class="radio-box">
                                            <input type="radio">Yes
                                        </div>
                                        <div class="radio-box">
                                            <input type="radio">No
                                        </div>
                                        <p><strong>NOTE:</strong> If Yes, please give manufacturer name</p>
                                        <input type="text" class="form-control">
                                     </div>
                                 </div>
                                 <div class="form-group">
                                     <div class="col-md-3">
                                         <label>Level of Aggregation :</label>
                                     </div>
                                     <div class="col-md-9">
                                         <select class="form-control">
                                             <option>Select</option>
                                         </select>
                                     </div>
                                 </div>
                                 <div class="form-group">
                                     <div class="col-md-3">
                                         <label>Purpose of Procurement :</label>
                                     </div>
                                     <div class="col-md-9">
                                         <select class="form-control">
                                             <option>Select</option>
                                         </select>
                                     </div>
                                 </div>
                                 <div class="form-group">
                                     <div class="col-md-3">
                                         <label>Aggregate Requirement :</label>
                                     </div>
                                     <div class="col-md-9">
                                         <select class="form-control">
                                             <option>Select</option>
                                         </select>
                                     </div>
                                 </div>
                                 <div class="form-group">
                                     <div class="col-md-3">
                                         <label>Search Keywords :</label>
                                     </div>
                                     <div class="col-md-9">
                                         <input type="search" class="form-control">
                                     </div>
                                 </div>
                                 <div class="form-group">
                                     <div class="col-md-12">
                                         <button class="btn btn-danger pull-right">Cancel</button>
                                         <input type="submit" value="Save & Next" class="btn btn-primary pull-right">
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