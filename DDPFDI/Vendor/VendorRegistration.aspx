<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorRegistration.aspx.cs" Inherits="Vendor_VendorRegistration" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="ConHead" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Innercontent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="container">
                <div class="cacade-forms">
                    <div class="clearfix mt10"></div>
                    <div class="tabing-section">
                        <ul class="nav nav-tabs">
                            <li class="active"><a data-toggle="tab" href="#pd">General Information</a></li>
                            <li><a data-toggle="tab" href="#pimg">Details of Defence Stores</a></li>
                            <li><a data-toggle="tab" href="#qpt">Registration no. Details</a></li>
                            <li><a data-toggle="tab" href="#test">Financial Information</a></li>
                            <li><a data-toggle="tab" href="#spd">Declarations</a></li>
                        </ul>
                        <div class="tab-content">
                            <div id="pd" class="tab-pane fade in active">
                                <asp:UpdatePanel ID="uppanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="panstep1" runat="server">
                                            <p>Please provide all required details to register your business with us</p>
                                            <div class="clearfix mt10"></div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    REGISTRATION CATEGORY <span class="mandatory">*</span>
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlregiscategory" runat="server" CssClass="form-control" required="required">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">MANUFACTURER</asp:ListItem>
                                                        <asp:ListItem Value="1">SERVICE SUB CONTRACTOR</asp:ListItem>
                                                        <asp:ListItem Value="1">AUTHORISED AGENT</asp:ListItem>
                                                        <asp:ListItem Value="1">TRADER</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Business Name
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtbusinessname" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Type of Business
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddltypeofbusiness" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Business Sector
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlbusinesssector" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Date of Incorportaion of the Company
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtdateofincorofthecompany" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Registered Office Address
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtregisterdofficeaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Street Address
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtstreetaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Street Address Line 2
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtstreetaddressline2" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    City
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtcity" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    State / Province
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtstateprovince" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Postal / Zip Code
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtpostalzipcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Email
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtemail" runat="server" placeholder="ex: myemail@example.com" CssClass="form-control"></asp:TextBox>
                                                    <p>ex: myemail@example.com</p>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Contact No
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtstdcode" runat="server" CssClass="form-control"></asp:TextBox>

                                                    <p>STD Code</p>
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>Phone Number</p>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Fax No
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtfaxstdcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>STD Code</p>
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtfaxphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>Phone Number</p>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Please Enter Name of
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlenternameof" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlenternameof_SelectedIndexChanged" CssClass="form-control">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Proprietor</asp:ListItem>
                                                        <asp:ListItem Value="2">Managing Director</asp:ListItem>
                                                        <asp:ListItem Value="3">Partner</asp:ListItem>
                                                        <asp:ListItem Value="4">Director</asp:ListItem>
                                                        <asp:ListItem Value="5">Holder of Power of Attorney</asp:ListItem>
                                                        <asp:ListItem Value="16">Other</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Name
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtfirstname" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>First Name</p>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtmiddlename" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>Middle Name</p>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtlastname" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>Last Name</p>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" visible="false" id="divdesignation">
                                                <div class="col-sm-5">
                                                    Designation
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    DIN No.
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtdinno" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Mobile No.
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtmobno" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Address
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>Street Address</p>
                                                    <asp:TextBox ID="txtaddress2" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>Street Address Line 2</p>
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtcity2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <p>City</p>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtstate2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <p>State / Province</p>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtpostalzipcode2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <p>
                                                                Postal / Zip Code
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnnext" runat="server" CssClass="btn btn-primary pull-right" Text="Save" OnClick="btnnext_Click" />
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="pimg" class="tab-pane">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="panstep2" runat="server">
                                            <p>Enter details of Regular Products being Manufactured</p>
                                            <div class="mt10 clearfix"></div>
                                            <p>
                                                Products Details
                                            </p>
                                            <asp:GridView ID="gvproddetail" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Nomenclature">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtproductnomen" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nato Group">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="txtnatogroup" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Hardware & Abrasive (53)</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nato Class">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="txtnatoclass" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="txtitemclass" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                            <div class="clearfix">
                                            </div>
                                            <asp:Button ID="btnaddmore" runat="server" CssClass="btn btn-primary pull-right" Text="Add More" />
                                            <div class="mt10 clearfix"></div>
                                            <p>
                                                Technology Details (At Max 3 type of technologies)
                                            </p>
                                            <asp:GridView ID="gvtechnology" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Nomenclature">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtproductnomen" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Technology 1">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttech1" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Technology 2">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttech2" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Technology 3">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttech3" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                            <div class="clearfix">
                                            </div>
                                            <asp:Button ID="btntectAddmore" runat="server" CssClass="btn btn-primary pull-right" Text="Add More" />
                                            <div class="mt10 clearfix"></div>
                                            <p>
                                                Source of Raw Material
                                            </p>
                                            <asp:GridView ID="gvSourceofRawMaterial" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Items">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtitems" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Basic Raw Material">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtbasicrawmeterial" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Source of material">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsourceofmaterial" runat="server" CssClass="form-control">
                                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name of Major Raw Material Suppliers">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtmaterialsupplier" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                            <div class="clearfix">
                                            </div>
                                            <asp:Button ID="btnSourceofRawMaterial" runat="server" CssClass="btn btn-primary pull-right" Text="Add More" />
                                            <div class="mt10 clearfix"></div>
                                            <h4>Successfully Completed Supply Orders in last 3 years to reputed Customers</h4>
                                            <div class="mt10 clearfix"></div>
                                            <p>
                                                Item Produced and Supplied
                                            </p>
                                            <asp:GridView ID="gvItemProducedandSupplied" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name of Reputed Customer">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtnameofrepcustomer" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Store Supplied">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtdescofstoresupp" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.O. No.and Date	">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtsonoanddate" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Qty.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtorderqty" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value Qty Supplied">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtvalueqtysupplied" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date of Last Supply">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtdateoflastsupplie" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                            <div class="clearfix">
                                            </div>
                                            <asp:Button ID="btnitemsupplied" runat="server" CssClass="btn btn-primary pull-right" Text="Add More" />

                                            <div class="mt10 clearfix"></div>
                                            <p>
                                                Item Supplied but not produced
                                            </p>
                                            <asp:GridView ID="gvItemSuppliedbutnotproduced" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name of Reputed Customer">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtnameofrepcustomer1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Store Supplied">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtdescofstoresupp1" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.O. No.and Date	">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtsonoanddate1" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Qty.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtorderqty1" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value Qty Supplied">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtvalueqtysupplied1" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date of Last Supply">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtdateoflastsupplie1" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                            <div class="clearfix">
                                            </div>
                                            <asp:Button ID="btnItemSuppliedbutnotproduced" runat="server" CssClass="btn btn-primary pull-right" Text="Add More" />

                                            <div class="clearfix mt10"></div>
                                            <asp:Button ID="btnnext2" runat="server" Text="Save" CssClass="btn btn-primary pull-right" OnClick="btnnext2_Click" />
                                            <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn-primary pull-right" Visible="false" Style="margin-right: 5px;" OnClick="btnback_Click" />
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="qpt" class="tab-pane">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="panstep3" runat="server">
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Do you have PAN/TAN
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddldetailofpantan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldetailofpantan_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">PAN</asp:ListItem>
                                                        <asp:ListItem Value="2">TAN</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="divpantan" visible="false">
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblpantan" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtpantan" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    GSTIN
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtgstin" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    UAM
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtUAM" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    CIN
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtCIN" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Are you registered with Govt. Department/Undertaking/PSU under Ministry of Defence
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlDepartmentUndertakingPSU" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlDepartmentUndertakingPSU_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                                        <asp:ListItem Value="2">NO</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div runat="server" id="divgovtundertaking" visible="false">
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Name of Govt. Department/Undertaking/PSU under Ministry of Defence
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="nameofgovt" runat="server" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Certificate valid upto
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="txtcertificatevalidupto" runat="server" Type="date" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Please Upload Registration Certificate
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:FileUpload ID="furegiscertificate" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <asp:Button ID="btnNextpanel3" runat="server" Text="Save" CssClass="btn btn-primary pull-right" Style="margin-left: 5px;" OnClick="btnNextpanel3_Click" />
                                            <asp:Button ID="btnbackpanel3" runat="server" Text="Back" CssClass="btn btn-primary pull-right" Visible="false" OnClick="btnbackpanel3_Click" />
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="test" class="tab-pane">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="panstep4" runat="server">
                                            <div class="form-group" runat="server" visible="false">
                                                <p>Choose Last 3 Financial Year for uploading Audited Balance Sheet and Profit & Loss account</p>
                                                <div class="clearfix"></div>
                                                <div class="col-sm-4">
                                                    <asp:CheckBoxList ID="rbfinnyear" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="rbfinnyear_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" style="margin-top: 10px;">2018-19</asp:ListItem>
                                                        <asp:ListItem Value="2" style="margin-top: 10px;">2017-18</asp:ListItem>
                                                        <asp:ListItem Value="3" style="margin-top: 10px;">2016-17</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                                <div class="col-sm-8">
                                                    <div runat="server" class="row" id="div19" visible="false">
                                                        <p>
                                                            2018-19 
                                                        </p>
                                                        <asp:FileUpload ID="fu19" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="clearfix mt5"></div>
                                                    <div runat="server" class="row" id="div18" visible="false">
                                                        <p>
                                                            2017-18
                                                        </p>
                                                        <asp:FileUpload ID="fu18" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="clearfix mt5"></div>
                                                    <div runat="server" class="row" id="div17" visible="false">
                                                        <p>
                                                            2016-17
                                                        </p>
                                                        <asp:FileUpload ID="fu17" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <p>
                                                TURN OVER DURING LAST 3 YEARS
                                            </p>
                                            <asp:GridView runat="server" ID="gvTURNOVERDURINGLAST3YEARS" CssClass="table table-hover" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Financial Year">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Financial Year">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtfinyear" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value of Current Assets">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcurrentasset" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value of Current Liabilites">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcurrentlibilites" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Profit/Loss">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtprofitloss" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Upload Audited Balance account sheet">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fufileprofitloss" runat="server" CssClass="form-control" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                            <div class="clearfix"></div>
                                            <asp:Button ID="btnturnover" runat="server" Text="Add More" CssClass="btn btn-primary pull-right" />
                                            <div class="clearfix"></div>
                                            <hr />


                                            <h3>Bank Details</h3>
                                            <div class="clearfix mt10"></div>
                                            <div class="form-group">
                                                <div class="col-sm-5">NAME OF BANK</div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtnameofbank" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">Type of Account</div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddltypeofaccount" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddltypeofaccount_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Saving Account</asp:ListItem>
                                                        <asp:ListItem Value="2">Current Account</asp:ListItem>
                                                        <asp:ListItem Value="3">Over Draft Account</asp:ListItem>
                                                        <asp:ListItem Value="4">Any Other Account</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group" runat="server" id="divbankdetail" visible="false">
                                                <div class="col-sm-5">Account Information</div>
                                                <div class="col-sm-7">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtaccountno" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <p>Account No.</p>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtmicrcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <p>MICR Code</p>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtifsccode" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <p>IFSC Code</p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">Copy of Valid Bank Solvency Certificate</div>
                                                <div class="col-sm-7">
                                                    <asp:FileUpload ID="fubankcertificate" runat="server" CssClass="form-control"></asp:FileUpload>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <asp:Button ID="btnpanel4next" runat="server" CssClass="btn btn-primary pull-right" Text="Save" Style="margin-left: 5px;" OnClick="btnpanel4next_Click" />
                                            <asp:Button ID="btnpanel4back" runat="server" CssClass="btn btn-primary pull-right" Visible="false" Text="Back" OnClick="btnpanel4back_Click" />
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="spd" class="tab-pane">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="panstep5" runat="server">
                                            <p>1.Has the firm declared insolvent in Receivership ,Bankrupt or being wounded up.</p>
                                            <asp:DropDownList ID="ddlwoundedup" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <p>2.Have firm affairs administered by a court or a judicial officer.</p>
                                            <asp:DropDownList ID="ddljudicialofficer" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <p>3.Is business activities suspended.</p>
                                            <asp:DropDownList ID="ddlbusinesssuspended" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <p>4.Is the firm subject of legal proceedings for any of the forging reasons.</p>
                                            <asp:DropDownList ID="ddlforgingreasone" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <p>5.Has the firm been debarred from Govt. Contracts</p>
                                            <asp:DropDownList ID="ddldebarredgovtcont" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <p>6.I/We note that registration ,does not carry with it the right to business with DPSUs/OFB.</p>
                                            <asp:DropDownList ID="ddlbusinessdpsuofb" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <p>
                                                7. I/We hereby declare that the information pertaining to my/our firm/Company including all enclosures is correct and true to the best of
                         my/our knowledge and belief as on date.
                                            </p>
                                            <asp:DropDownList ID="ddlpertainingdate" runat="server" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary pull-right" Text="Submit" OnClick="btnsubmit_Click" />
                                            <asp:Button ID="btnclear" runat="server" CssClass="btn btn-primary pull-right" Style="margin-right: 5px;" Text="Clear Form" OnClick="btnclear_Click" />
                                            <asp:Button ID="btnprint" runat="server" CssClass="btn btn-primary pull-right" Text="Print Form" Style="margin-right: 5px;" OnClick="btnprint_Click" />
                                            <asp:Button ID="btnbackpanel5" runat="server" CssClass="btn btn-primary pull-right" Style="margin-right: 5px;" Visible="false" Text="Back" OnClick="btnbackpanel5_Click" />
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
