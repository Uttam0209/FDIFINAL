<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="Admin_AddProduct" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">

   
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="tabing-section">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#pd">Product Description</a></li>
                    <li><a data-toggle="tab" href="#pimg">Product Image</a></li>
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
                                                    <input type="text" class="form-control">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>HSN Code</label>
                                                    <input type="text" class="form-control">
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>NATO Code</label>
                                                    <input type="text" class="form-control">
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>ERP Reference No</label>
                                                    <input type="text" class="form-control">
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
                                                    <label>Product Level 1</label>
                                                    <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>

                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Product Level 2</label>
                                                    <asp:DropDownList runat="server" ID="ddlsubcategory" class="form-control"></asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Product Description </label>
                                                    <textarea class="form-control"></textarea>
                                                </div>
                                            </div>
                   
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Technology Level 1 </label>
                                                    <asp:DropDownList runat="server" ID="ddltechnologycat" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddltechnologycat_SelectedIndexChanged"></asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Technology Level 2</label>
                                                    <asp:DropDownList runat="server" ID="ddlsubtech" class="form-control"></asp:DropDownList>
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
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Platform :</label>
                                                    <asp:DropDownList runat="server" ID="ddlplatform" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlplatform_SelectedIndexChanged"></asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Purpose of Procurement :</label>

                                                    <asp:DropDownList runat="server" ID="ddlplatformsubcat" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Product Requirement :</label>

                                                    <asp:DropDownList runat="server" ID="ddlprodreqir" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                          
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                          <div class="row">
                                              <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Search Keywords :</label>
                                                    <input type="search" class="form-control">
                                                </div>

                                            </div>
                                              <div class="col-md-8">
                                                 
                                                <div class="form-group">
                                                    <label class="live-status-box">
                                                        Product Already Indeginized :
                                                                <input type="radio" name="manufacture" class="yes">Yes
                                                                <input type="radio" name="manufacture">No

                                                        <span>(<strong>NOTE:</strong> If Yes, please give manufacturer name)</span>
                                                    </label>
                                                    <input type="text" class="form-control Turl_Tdate" style="display:none">

                                                </div>
                                                 
                                            </div>
                                        
                                          </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnsubmitpanel1" class="btn btn-primary pull-right" Text="Save" />
                                                <asp:Button runat="server" ID="btncancelpanel1" class="btn btn-default pull-right" Style="margin-right: 10px;" Text="Back" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="pimg" class="tab-pane fade in">
                        <div class="section-pannel">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Product Image</label>
                                    <input type="file"  class="form-control"/>
                                </div>
                            </div>
                        </div>

                        </div>
                         <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="Button1" class="btn btn-primary pull-right" Text="Save" />
                                    <asp:Button runat="server" ID="Button2" class="btn btn-default pull-right" Text="Back" Style="margin-right: 10px;" />
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
                                        <td>
                                            <textarea class="form-control" style="height:30px; resize:none; overflow:hidden"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>Demosntration</td>
                                        <td>
                                            <input type="checkbox"></td>
                                        <td>
                                            <textarea class="form-control" style="height:30px; resize:none; overflow:hidden"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td>Technical Specification</td>
                                        <td>
                                            <input type="checkbox"></td>
                                        <td>
                                            <textarea class="form-control" style="height:30px; resize:none; overflow:hidden"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td>Drawing</td>
                                        <td>
                                            <input type="checkbox"></td>
                                        <td>
                                            <textarea class="form-control" style="height:30px; resize:none; overflow:hidden"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>4</td>
                                        <td>Sample</td>
                                        <td>
                                            <input type="checkbox"></td>
                                        <td>
                                            <textarea class="form-control" style="height:30px; resize:none; overflow:hidden"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>5</td>
                                        <td>Funding Support for Development</td>
                                        <td>
                                            <input type="checkbox"></td>
                                        <td>
                                            <textarea class="form-control" style="height:30px; resize:none; overflow:hidden"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>6</td>
                                        <td>Funding Support for Testing</td>
                                        <td>
                                            <input type="checkbox"></td>
                                        <td>
                                            <textarea class="form-control" style="height:30px; resize:none; overflow:hidden"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>7</td>
                                        <td>Financial Support</td>
                                        <td>
                                            <input type="checkbox"></td>
                                        <td>
                                           <textarea class="form-control" style="height:30px; resize:none; overflow:hidden"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>8</td>
                                        <td>Assurance of Procurement</td>
                                        <td>
                                            <input type="checkbox"></td>
                                        <td>
                                            <textarea class="form-control" style="height:30px; resize:none; overflow:hidden"></textarea>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnsavepanel2" class="btn btn-primary pull-right" Text="Save" />
                                    <asp:Button runat="server" ID="btnbackpanel2" class="btn btn-default pull-right" Text="Back" Style="margin-right: 10px;" />
                                </div>
                                
                            </div>
                        </div>
                    </div>
                    <div id="qpt" class="tab-pane fade">
                        <div class="section-pannel">
                             <div class="row">
                            <%--<div class="col-md-4">
                                        <div class="form-group">
                                            <label>Part Number</label>
                                            <input type="text" class="form-control">
                                        </div>
                                    </div>--%>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Estimated Quantity</label>
                                    <input type="text" class="form-control">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Estimated Price / LLP</label>
                                    <input type="text" class="form-control">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group ">
                                    <label>
                                        Tender Status</label>
                                    <select class="form-control">
                                        <option>Live</option>
                                        <option>Archive</option>
                                        <option>Not Floated</option>
                                        <option>To be Floated shortly</option>
                                    </select>
                                   
                                </div>

                            </div>
                        </div>
                        <div class="row">

                            <div class="col-md-12">
                                <div class="form-group live-status-box">
                                    <label>
                                        <strong>Note:</strong> If live, please fill last date of tender submission. 
                                            <span class="checkbox-box">
                                                <input type="radio" name="tender" class="yes">Yes
                                                <input type="radio" name="tender" class="no">No
                                            </span>
                                    </label>


                                </div>
                            </div>
                            <div class="Turl_Tdate" style="display:none">   
                              <div class="col-md-6">
                                <div class="form-group">
                                    <label>Tender Date</label>
                                    <input type="date" class="form-control inputbox">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Tender URL</label>
                                    <input type="text" class="form-control">
                                </div>
                            </div>
                            </div>
                         
                        </div>
                        </div>
                       
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnsavepanel3" class="btn btn-primary pull-right" Text="Save" />
                                    <asp:Button runat="server" ID="btnbackpanel3" class="btn btn-default pull-right" Text="Back" Style="margin-right: 10px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="cd" class="tab-pane fade">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <div class="section-pannel">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 class="page-header secondary">Contact Detail 1  </h4>
                                            <div class="form-group contactD1Select">
                                                <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail_SelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="contactFormRow" style="display: none">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Name</label>
                                                    <asp:TextBox runat="server" ID="txtNName" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Employee Code</label>
                                                   <input type="text" class="form-control" />

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Designation</label>
                                                    <asp:TextBox runat="server" ID="txtDesignation" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Department</label>
                                                    <asp:TextBox runat="server" ID="txtDepartment" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Mobile Number</label>
                                                    <input type="text" class="form-control" />

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtNTelephone" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Fax</label>
                                                    <asp:TextBox runat="server" ID="txtNFaxNo" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>E-Mail ID</label>
                                                    <asp:TextBox runat="server" ID="txtNEmailId" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="showMore">
                                        <a href="javascript:void(0)" class="showMoreLink">Show Details</a>
                                    </div>
                                </div>
                                <div class="section-pannel">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 class="page-header secondary">Contact Detail 2</h4>
                                            <div class="form-group select-box">
                                                <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail2" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail2_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="contactFormRow" style="display: none">

                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Name</label>
                                                    <asp:TextBox runat="server" ID="txtNName2" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Employee Code</label>
                                                   <input type="text" class="form-control" />

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Designation</label>
                                                    <asp:TextBox runat="server" ID="TextBox7" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Department</label>
                                                    <asp:TextBox runat="server" ID="TextBox8" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Mobile Number</label>
                                                   <input type="text" class="form-control" />

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtNTelephone2" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Fax</label>
                                                    <asp:TextBox runat="server" ID="txtNFaxNo2" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>E-Mail ID</label>
                                                    <asp:TextBox runat="server" ID="txtNEmailId2" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="showMore">
                                        <a href="javascript:void(0)" class="showMoreLink">Show Details</a>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnsavepanel4" class="btn btn-primary pull-right" Text="Save" />
                                            <asp:Button runat="server" ID="btnbackpanel4" class="btn btn-default pull-right" Text="Back" Style="margin-right: 10px;" />
                                        </div>
                                    </div>
                                </div>
                               
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

