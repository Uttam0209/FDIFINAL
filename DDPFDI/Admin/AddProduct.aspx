<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="Admin_AddProduct" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>

    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label>
                    </h3>

                </div>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                <ContentTemplate>
                    <div class="tabing-section">
                        <ul class="nav nav-tabs">
                            <li class="active"><a data-toggle="tab" href="#pd">Product Description</a></li>
                            <li><a data-toggle="tab" href="#spd">Support Provided by DPSU</a></li>
                            <li><a data-toggle="tab" href="#qpt">Quantity Required</a></li>
                            <li><a data-toggle="tab" href="#cd">Contact Details</a></li>
                        </ul>
                        <div class="tab-content">
                            <div id="pd" class="tab-pane fade in active">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="add-profile">
                                            <div class="section-pannel">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>OEM Part Number</label>
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>DPSU Part Number</label>
                                                        <input type="text" class="form-control">
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>End User Part Number</label>
                                                        <textarea class="form-control"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                            <div class="section-pannel"> 
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Nomenclature of main system </label>
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Product Category </label>
                                                        <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>

                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Product Sub Category </label>
                                                        <asp:DropDownList runat="server" ID="ddlsubcategory" class="form-control" AutoPostBack="True"></asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Product Description </label>
                                                        <textarea class="form-control"></textarea>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Product Image </label>
                                                        <input type="file" class="btn">
                                                    </div>

                                                </div>


                                            </div>
                                            </div>
                                             <div class="section-pannel">
                                            <div class="row">

                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Technology Category </label>
                                                        <select class="form-control">
                                                            <option>Select Category</option>
                                                            <option>Mechanical</option>
                                                            <option>Electrical</option>
                                                            <option>Electronics</option>
                                                            <option>Electro-Mechanical</option>
                                                            <option>Casting</option>
                                                            <option>Instrumentation</option>
                                                            <option>Avionics</option>
                                                            <option>Hydraulics</option>
                                                            <option>Pneumatics</option>
                                                            <option>Armaments</option>

                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Technology Sub Category</label>
                                                        <select class="form-control">
                                                            <option>Select Sub Category</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>End User</label>
                                                        <select class="form-control">
                                                            <option>Select</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                             </div>
                                              <div class="section-pannel">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>
                                                            Product Already Indeginized :
                                                                <input type="radio">Yes
                                                                <input type="radio">No</label>

                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <p><strong>NOTE:</strong> If Yes, please give manufacturer name</p>
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                              </div>
                                               <div class="section-pannel">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Platform :</label>
                                                        <select class="form-control">
                                                            <option>Select</option>
                                                            <option>Ships</option>
                                                            <option>Tanks</option>
                                                            <option>Submarines</option>
                                                            <option>Armaments</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Purpose of Procurement :</label>
                                                        <select class="form-control">
                                                            <option>Select</option>
                                                            <option>Alternate Source</option>
                                                            <option>Import</option>
                                                            <option>Substitution</option>
                                                            <option>Prototype Development</option>
                                                            <option>Research & Development</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Product Requirement :</label>
                                                        <select class="form-control">
                                                            <option>Select</option>
                                                            <option>Short-Term 1-2 Years</option>
                                                            <option>Mid-Term 3-5 Years</option>
                                                            <option>Long-Term 5-7 Years</option>

                                                        </select>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Search Keywords :</label>
                                                        <input type="search" class="form-control">
                                                    </div>

                                                </div>
                                            </div>
                                               </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <button class="btn btn-danger pull-right">Cancel</button>
                                                        <input type="submit" value="Save" class="btn btn-primary pull-right">
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="spd" class="tab-pane fade">
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class=" table responsive no-wrap table-hover manage-user Grid">
                                            <tr>
                                                <th>S.No</th>
                                                <th>Services</th>
                                                <th>Checkbox</th>
                                                <th>Remarks</th>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>Drawing</td>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Demosntration</td>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>Technical Specification</td>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>Drawing</td>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>4</td>
                                                <td>Sample</td>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>5</td>
                                                <td>Funding Support for Development</td>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>6</td>
                                                <td>Funding Support for Testing</td>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>7</td>
                                                <td>Financial Support</td>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>8</td>
                                                <td>Assurance of Procurement</td>
                                                <td>
                                                    <input type="checkbox"></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <input type="submit" value="Save" class="btn btn-primary pull-right">
                                        <input type="submit" value="Back" class="btn btn-default pull-right" style="margin-right: 10px;">
                                    </div>
                                </div>
                            </div>
                            <div id="qpt" class="tab-pane fade">
                                <div class="row">
                                    <%--<div class="col-md-4">
                                        <div class="form-group">
                                            <label>Part Number</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>--%>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Estimated Quantity</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Estimated Price / LLP</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label style="display:block">
                                                Tender Status</label>
                                                <input type="checkbox">Live
                                                <input type="checkbox" style="margin-left: 20px;">Archive
                                            <input type="checkbox" style="margin-left: 20px;">Not Floated
                                            <input type="checkbox" style="margin-left: 20px;">To be Floated shortly
                                        </div>

                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <p>If live, please fill last date of tender submission.</p>
                                            <label>Last Date of Tender Submission</label>
                                            <input type="date" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Tender URL</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                        <input type="submit" value="Save" class="btn btn-primary pull-right">
                                        <input type="submit" value="Back" class="btn btn-default pull-right" style="margin-right: 10px;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="cd" class="tab-pane fade">
                                <div class="section-pannel">
                                <h4 class="page-header secondary">Contact Detail 1</h4>
                               
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Officer's Name</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Designation</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Phone Number</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Fax</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>E-Mail ID</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                </div>
                                </div>
                                 <div class="section-pannel">
                                <h4 class="page-header secondary">Contact Detail 2</h4>
                               
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Officer's Name</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Designation</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Phone Number</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Fax</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>E-Mail ID</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>
                                </div>
                                 </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                        <input type="submit" value="Save" class="btn btn-primary pull-right">
                                        <input type="submit" value="Back" class="btn btn-default pull-right" style="margin-right: 10px;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

