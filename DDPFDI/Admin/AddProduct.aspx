<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="Admin_AddProduct" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-header">
                        <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></h3>

                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="add-profile">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Product Nomenclature </label>
                                    <input type="text" class="form-control">
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Product Category </label>
                                    <select class="form-control">
                                        <option>Select Category</option>
                                        <option>Standard Parts</option>
                                        <option>Raw Material</option>
                                        <option>Tools & Testers</option>
                                        <option>Spares</option>
                                         <option>Maintenance/Repair</option>
                                        <option>Modules</option>
                                        <option>Components</option>
                                         <option>Assembly/System</option>
                                         <option>Sub-Assembly</option>
                                        <option>Ground Equipments</option>
                                        <option>Bearings</option>
                                       
                                    </select>
                                </div>

                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Product Sub Category </label>
                                    <select class="form-control">
                                        <option>--Standard Parts--</option>
                                         <option>Bolt</option>
                                         <option>Nut</option>
                                         <option>Screw</option>
                                         <option>Washer</option>
                                         <option>--Assembly/System--</option>
                                        <option>Radar</option>
                                        <option>Flight Control Systen</option>
                                        <option>Flight Data Reader</option>
                                        <option>Fire Saftey System</option>
                                        <option>Fuel & Engine</option>
                                        <option>Landing Gear</option>
                                        <option>Air Conditining System</option>
                                          <option>Pilot Seat</option>
                                         <option>--Component--</option>
                                        <option>Transister</option>
                                        <option>Amplifier</option>
                                        <option>Diode</option>
                                        <option></option>
                                        <%--<option>Fuel & Engine</option>
                                        <option>Landing Gear</option>
                                        <option>Air Conditining System</option>
                                          <option>Pilot Seat</option>--%>
                                         
                                    </select>
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

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Product Already Indeginized :
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
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Product Requirement :</label>
                                    <select class="form-control">
                                        <option>Select</option>
                                        <option>Short-Term</option>
                                        <option>Mid-Term</option>
                                        <option>Long-Term</option>

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

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <button class="btn btn-danger pull-right">Cancel</button>
                                    <input type="submit" value="Save & Next" class="btn btn-primary pull-right">
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>

        <div class="clearfix"></div>
    </div>

    <div class="footer">� 2019 <a href="#">Department of Defence Production</a> </div>
    </div>
</asp:Content>
