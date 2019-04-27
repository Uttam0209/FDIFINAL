<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="Admin_AddProduct" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" ChildrenAsTriggers="True">
        <ContentTemplate>
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
        </ContentTemplate>
        <Triggers></Triggers>
    </asp:UpdatePanel>
</asp:Content>
