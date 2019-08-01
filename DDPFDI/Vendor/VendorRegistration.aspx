<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorRegistration.aspx.cs" Inherits="Vendor_VendorRegistration" MasterPageFile="~/Vendor/VendorMaster.master" %>

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
                            <li><a data-toggle="tab" href="#pcd" runat="server" id="testcompinfo" visible="false">Company Information</a></li>
                            <li><a data-toggle="tab" href="#ocd" runat="server" id="othercate" visible="false">Company Information</a></li>
                            <li><a data-toggle="tab" href="#qpt">Registration no. Details</a></li>
                            <li><a data-toggle="tab" href="#test">Financial Information</a></li>
                            <li><a data-toggle="tab" href="#chkList">CheckList</a></li>
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
                                                    <asp:DropDownList ID="ddlregiscategory" runat="server" CssClass="form-control" AutoPostBack="true" required="required" OnSelectedIndexChanged="ddlregiscategory_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">MANUFACTURER</asp:ListItem>
                                                        <asp:ListItem Value="2">SERVICE SUB CONTRACTOR</asp:ListItem>
                                                        <asp:ListItem Value="3">AUTHORISED AGENT</asp:ListItem>
                                                        <asp:ListItem Value="4">TRADER</asp:ListItem>
                                                        <asp:ListItem Value="5">OEM</asp:ListItem>
                                                        <asp:ListItem Value="6">Stockist</asp:ListItem>
                                                        <asp:ListItem Value="7">Contractor</asp:ListItem>
                                                        <asp:ListItem Value="8">Distributor</asp:ListItem>
                                                        <asp:ListItem Value="9">Consortium</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Name of firm/company
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtbusinessname" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Type of OwnerShip
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddltypeofbusiness" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddltypeofbusiness_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <div class="clearfix mt10"></div>
                                                    <div runat="server" id="divmsmetypeofbuisness" visible="false">
                                                        <p>Scale of buisness</p>
                                                        <asp:DropDownList ID="ddlscaleofbuisness" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlscaleofbuisness_SelectedIndexChanged" CssClass="form-control">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Small</asp:ListItem>
                                                            <asp:ListItem Value="2">Medium</asp:ListItem>
                                                            <asp:ListItem Value="3">Micro</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="clearfix mt10"></div>
                                                        <div class="col-sm-7">
                                                            <p>Ownership</p>
                                                            <asp:CheckBoxList ID="chkownership" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkownership_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                <asp:ListItem Value="1">SC/ST</asp:ListItem>
                                                                <asp:ListItem Value="2">General</asp:ListItem>
                                                                <asp:ListItem Value="3">Women Organization</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <div runat="server" id="per1" visible="false">
                                                                <asp:TextBox ID="txtpercent1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <p>Percentage of ownership</p>
                                                            </div>
                                                            <div class="clearfix mt10"></div>
                                                            <div runat="server" id="per2" visible="false">
                                                                <asp:TextBox ID="txtpercent2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <p>Percentage of ownership</p>
                                                            </div>
                                                        </div>
                                                        <div class="clearfix mt10"></div>
                                                        <div runat="server" id="cermsme" visible="false">
                                                            <p>
                                                                MSME certificate issued by competent authorities (NSIC/ DIC/ KVIC/KVIB/ Coir Board, Directorate of Handicraft & Handlooms)
                                                            </p>
                                                            <asp:FileUpload runat="server" ID="fun" CssClass="form-control" />
                                                        </div>
                                                    </div>
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
                                                    <asp:TextBox ID="txtstreetaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                    Street Address
                                                     <div class="clearfix mt5"></div>
                                                    <asp:TextBox ID="txtstreetaddressline2" runat="server" CssClass="form-control"></asp:TextBox>
                                                    Street Address Line 2   
                                                     <div class="clearfix mt5"></div>
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtcity" runat="server" CssClass="form-control"></asp:TextBox>
                                                            City
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtstateprovince" runat="server" CssClass="form-control"></asp:TextBox>
                                                            State 
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtpostalzipcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                            Pincode  
                                                        </div>
                                                    </div>
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
                                                <asp:GridView ID="gridNameof" runat="server" AutoGenerateColumns="false" class="table table-responsive" ShowFooter="true" OnRowCreated="gridNameof_RowCreated">
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
                                                        <asp:TemplateField HeaderText="Enter Name of">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlenternameof" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    <asp:ListItem Value="Proprietor">Proprietor</asp:ListItem>
                                                                    <asp:ListItem Value="Managing Director">Managing Director</asp:ListItem>
                                                                    <asp:ListItem Value="Partner">Partner</asp:ListItem>
                                                                    <asp:ListItem Value="Director">Director</asp:ListItem>
                                                                    <asp:ListItem Value="Holder of Power of Attorney">Holder of Power of Attorney</asp:ListItem>
                                                                    <asp:ListItem Value="Promoter">Promoter</asp:ListItem>
                                                                    <asp:ListItem Value="Company secretary">Company secretary</asp:ListItem>
                                                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtEnterNameof" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DIN No">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtdinno" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile No">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtmobno" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="ButtonAddEnterNameof" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="ButtonAddEnterNameof_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="fa fa-times"
                                                                    OnClick="LinkButton1_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="ddlregiscategory" />
                                        <asp:PostBackTrigger ControlID="ddltypeofbusiness" />
                                    </Triggers>
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
                                            <asp:GridView ID="gvproddetail" runat="server" CssClass="table table-hover" ShowFooter="true" CellPadding="4"
                                                ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="gvproddetail_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="RowNumberProd" HeaderText="Row Number" />
                                                    <asp:TemplateField HeaderText="Product Nomenclature">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtproductnomen" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nato Group">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlnatogroup" runat="server" Width="180px" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlnatogroup_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nato Class">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlnatoclass" runat="server" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="ddlnatoclass_SelectedIndexChanged" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlitemcode" runat="server" Width="180px" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HSN Code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txthsnno" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnProductDetailAddMore" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnProductDetailAddMore_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbProductDetail" runat="server" CssClass="fa fa-times"
                                                                OnClick="lbProductDetail_Click"></asp:LinkButton>
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
                                            <div class="mt10 clearfix"></div>
                                            <p>
                                                Technology Details (At Max 3 type of technologies)
                                            </p>
                                            <asp:GridView ID="gvtechnology" runat="server" CssClass="table table-hover" ShowFooter="true" CellPadding="4"
                                                ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="gvtechnology_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="RowNumberTech" HeaderText="Row Number" />
                                                    <asp:TemplateField HeaderText="Product Nomenclature">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttechnomen" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Technology 1">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddltech1" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddltech1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Technology 2">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddltech2" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="txttech2_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Technology 3">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddltech3" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddTech" runat="server" CssClass="btn btn-primary pull-right"
                                                                Text="Add New Row"
                                                                OnClick="btnAddTech_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtechremove" runat="server" CssClass="fa fa-times"
                                                                OnClick="lbtechremove_Click"></asp:LinkButton>
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
                                            <div class="mt10 clearfix"></div>
                                            <p>
                                                Source of Raw Material
                                            </p>
                                            <asp:GridView ID="gvSourceofRawMaterial" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333"
                                                GridLines="None" AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="gvSourceofRawMaterial_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SrNoRawMeterail" HeaderText="Raw Number" />
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
                                                                <asp:ListItem Value="1">Indigenized</asp:ListItem>
                                                                <asp:ListItem Value="2">Imported</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name of Major Raw Material Suppliers">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtmaterialsupplier" runat="server" CssClass="form-control">                                           
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddRawMeterial" runat="server" CssClass="btn btn-primary pull-right"
                                                                Text="Add New Row"
                                                                OnClick="btnAddRawMeterial_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbmeterailremove" runat="server" CssClass="fa fa-times"
                                                                OnClick="lbmeterailremove_Click"></asp:LinkButton>
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
                                            <div class="mt10 clearfix"></div>
                                            <h4>Successfully Completed Supply Orders in last 3 years to reputed Customers</h4>
                                            <div class="mt10 clearfix"></div>
                                            <p>
                                                Item Produced and Supplied
                                            </p>
                                            <asp:GridView ID="gvItemProducedandSupplied" runat="server" CssClass="table table-hover" CellPadding="4"
                                                ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="gvItemProducedandSupplied_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SrNoSpplied" HeaderText="Raw Number" />
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
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddSupplied" runat="server" CssClass="btn btn-primary pull-right"
                                                                Text="Add New Row"
                                                                OnClick="btnAddSupplied_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbSuplliedremove" runat="server" CssClass="fa fa-times"
                                                                OnClick="lbSuplliedremove_Click"></asp:LinkButton>
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
                                            <div class="mt10 clearfix"></div>
                                            <p>
                                                Item Supplied but not produced
                                            </p>
                                            <asp:GridView ID="gvItemSuppliedbutnotproduced" runat="server" CssClass="table table-hover"
                                                CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="gvItemSuppliedbutnotproduced_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SrNoSpplied1" HeaderText="Raw Number" />
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
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddSupplied1" runat="server" CssClass="btn btn-primary pull-right"
                                                                Text="Add New Row"
                                                                OnClick="btnAddSupplied1_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbSuplliedremove1" runat="server" CssClass="fa fa-times"
                                                                OnClick="lbSuplliedremove1_Click"></asp:LinkButton>
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
                                            <div class="clearfix mt10"></div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="pcd" class="tab-pane">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="testcompinfo1" runat="server">
                                            <p>List of Manufacturing Facilities</p>
                                            <asp:GridView ID="gvmanufacility" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                                ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="gvmanufacility_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                                    <asp:TemplateField HeaderText="Name of Factory">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtmanofficename" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complete postal Address (Including Plot no. /street /City/State Postal Code ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCAddrssMF" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Official Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtofficialNameMF" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Telephone No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttelephonenoMF" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fax No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtfaxnoMF" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email Id">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtemailidMF" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddManufac" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnAddManufac_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblremovemanufac" runat="server" CssClass="fa fa-times" OnClick="lblremovemanufac_Click"></asp:LinkButton>
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
                                            <div class="mt10 clearfix"></div>
                                            <p>Area Details</p>
                                            <asp:GridView ID="gvareadetail" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                                ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="gvareadetail_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="Sno" HeaderText="Raw Number" />
                                                    <asp:TemplateField HeaderText="Name of Factory">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAreaFactoryName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PRODUCTION AREA">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtprodarea" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="INSPECTION AREA">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtinsarea" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TOTAL COVERED AREA">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttotalcoverdarea" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Area">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttotalarea" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddArea" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnAddArea_Click1" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblRemoveArea" runat="server" CssClass="fa fa-times" OnClick="lblRemoveArea_Click1"></asp:LinkButton>
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
                                            <div class="mt10 clearfix"></div>
                                            <p>List of All Plant and Machines</p>
                                            <asp:GridView ID="gvplantandmachines" runat="server" CssClass="table table-hover" ShowFooter="true"
                                                CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="gvplantandmachines_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="Sno" HeaderText="Raw Number" />
                                                    <asp:TemplateField HeaderText="Description of Machine & Model Specs">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPlantandMachineName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Make">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPlantMachine" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQuanProdManu" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date of Purchase">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtplantmachiPurchase" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Usage">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPlantMachiUsage" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAddPlantorMachine" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnAddPlantorMachine_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblRemovePlantMachine" runat="server" CssClass="fa fa-times" OnClick="lblRemovePlantMachine_Click"></asp:LinkButton>
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
                                            <div class="mt10 clearfix"></div>
                                            <p>Employees Details</p>
                                            <asp:GridView ID="gvempCompInfo" runat="server" CssClass="table table-hover"
                                                ShowFooter="true" OnRowCreated="gvempCompInfo_RowCreated" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                                    <asp:TemplateField HeaderText="TOTAL Employees">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttotalempCI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ADMINISTRATIVE">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtadministrativeCI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TECHNICAL">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttechCI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NON TECHNICAL">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNontechCI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QC/INSPECTION">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtqcCI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SKILLED LABOUR">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtskCI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UNSKILLED LABOUR">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtuLCI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnEmpInfo" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnEmpInfo_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbRemoveEmp" runat="server" CssClass="fa fa-times" OnClick="lbRemoveEmp_Click"></asp:LinkButton>
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
                                            <div class="clearfix mt10">
                                            </div>
                                            <div class="col-sm-5 row">
                                                <p>Do you have following certificates</p>
                                                <asp:CheckBoxList ID="chkcertificate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkcertificate_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    <asp:ListItem Value="1">Factory Licence /Municipal Shop's & Establishment</asp:ListItem>
                                                    <asp:ListItem Value="2">Registration Certificate from Labor Commissioner</asp:ListItem>
                                                    <asp:ListItem Value="3">VAT Registration Certificate</asp:ListItem>
                                                    <asp:ListItem Value="4">Excise Registration Certificate</asp:ListItem>
                                                    <asp:ListItem Value="5">Any other Certificate</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-sm-7 row">
                                                <div runat="server" id="div1certificate" visible="false">
                                                    <p>Factory Licence /Municipal Shop's & Establishment</p>
                                                    <asp:FileUpload ID="fucertificate1" runat="server" CssClass="form-control" />
                                                    <p>Please upload all factory licence</p>
                                                </div>
                                                <div runat="server" id="div2certificate" visible="false">
                                                    <p>Registration Certificate from Labor Commissioner</p>
                                                    <asp:FileUpload ID="fucertificate2" runat="server" CssClass="form-control" />
                                                    <p>Please upload all Registration Certifcate</p>
                                                </div>
                                                <div runat="server" id="div3certificate" visible="false">
                                                    <p>VAT Registration Certificate</p>
                                                    <asp:FileUpload ID="fucertificate3" runat="server" CssClass="form-control" />
                                                    <p>Please Upload all VAT Certificate</p>
                                                </div>
                                                <div runat="server" id="div4certificate" visible="false">
                                                    <p>Excise Registration Certificate</p>
                                                    <asp:FileUpload ID="fucertificate4" runat="server" CssClass="form-control" />
                                                    <p>Pleas upload all factory Excise Registration</p>
                                                </div>
                                                <div runat="server" id="div5certificate" visible="false">
                                                    <p>Name of Certificate</p>
                                                    <asp:TextBox ID="txtnameofcertificate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <div class="clearfix mt10">
                                                    </div>
                                                    <p>Any other certificate</p>
                                                    <asp:FileUpload ID="fucertificate5" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="clearfix mt10">
                                            </div>
                                            <h4>Details of Inspection and Quality Control of facilites</h4>
                                            <div class="clearfix mt10">
                                            </div>
                                            <p>List of Test Facilities</p>
                                            <asp:GridView ID="gvtestfacilities" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333"
                                                ShowFooter="true" OnRowCreated="gvtestfacilities_RowCreated" GridLines="None" AutoGenerateColumns="false">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                                    <asp:TemplateField HeaderText="Type of GAUGE / Test Equipment">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtnametestfesi" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Make">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtmaketf" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Least Count">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcounttf" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Range of MEASURMENT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtrangetf" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CERTIFICATION YEAR">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcertiyeartf" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year of purchase">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtyearofpurtf" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btntestfacilities" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btntestfacilities_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbRemovetestfacili" runat="server" CssClass="fa fa-times" OnClick="lbRemovetestfacili_Click"></asp:LinkButton>
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
                                            <div class="clearfix mt10"></div>
                                            <div class="form-group row">
                                                <div class="col-sm-5">
                                                    Is Lab accredited by NABL
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlnabl" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlnabl_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group  row" runat="server" id="divcertificatevalid" visible="false">
                                                <div class="col-sm-5">
                                                    If yes Certifiction is valid upto
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtdate" runat="server" Type="date" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <div class="col-sm-5">
                                                <p>Quality Certifications</p>
                                                <asp:CheckBoxList ID="chkqualitycertificate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkqualitycertificate_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    <asp:ListItem Value="1">IMS</asp:ListItem>
                                                    <asp:ListItem Value="2">EnMS</asp:ListItem>
                                                    <asp:ListItem Value="3">QMS</asp:ListItem>
                                                    <asp:ListItem Value="4">Any other Certificate</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-sm-7">
                                                <div runat="server" id="divqualitycertificate2" visible="false">
                                                    <asp:FileUpload ID="fuenms" runat="server" CssClass="form-control" />
                                                    <p>EnMS</p>
                                                </div>
                                                <div runat="server" id="divqualitycertificate3" visible="false">
                                                    <asp:FileUpload ID="fuQMS" runat="server" CssClass="form-control" />
                                                    <p>QMS</p>
                                                </div>
                                                <div runat="server" id="divqualitycertificate4" visible="false">
                                                    <p>Name of Certificate</p>
                                                    <asp:TextBox ID="txtanyother" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <div class="clearfix mt10"></div>
                                                    <asp:FileUpload ID="fuotherqualtitycertificate" runat="server" CssClass="form-control" />

                                                </div>

                                            </div>
                                            <div class="clearfix mt10">
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    <p>Details of R&D Facilities</p>
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtmss" runat="server" TextMode="MultiLine" Height="80px" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10">
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Do you have any Sales Office /Marketing Office
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddloffice" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddloffice_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10">
                                            </div>
                                            <div class="form-group" runat="server" visible="false" id="detailofoffcie">
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Name of Nodal Officer
                                                    </div>
                                                    <div class="col-sm-7 row">
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txfname" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <p>
                                                                First Name
                                                            </p>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtlname" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <p>
                                                                Last Name
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Sales/Marketing Office Address
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>Street Address</p>
                                                        <div class="clearfix mt5"></div>
                                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>Street Address Line 2</p>
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <p>
                                                                    City
                                                                </p>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <p>
                                                                    State / Province
                                                                </p>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <p>
                                                                    Postal / Zip Code
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Phone Number
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Fax Number
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Email
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>
                                                            example@example.com
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="clearfix mt10"></div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Do you have any authorised distributor/dealer
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddldistributoraddress" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldistributoraddress_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <div runat="server" id="gv3" visible="false">
                                                <asp:GridView ID="gvauthdealaddress" runat="server" CssClass="table table-hover" ShowFooter="true"
                                                    OnRowCreated="gvauthdealaddress_RowCreated" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                                        <asp:TemplateField HeaderText="StreetAddress">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDstreetaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtdState" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PinCode">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDPincode" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Phone">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fax">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDFax" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Email">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnautdeal" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnautdeal_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbRemoveAuthdel" runat="server" CssClass="fa fa-times" OnClick="lbRemoveAuthdel_Click"></asp:LinkButton>
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
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <div class="form-group">
                                                <p>
                                                    Details of Outsourcing Facilites
                                                </p>
                                                <asp:GridView ID="gvoutsourcefacility" runat="server" CssClass="table table-hover" ShowFooter="true" OnRowCreated="gvoutsourcefacility_RowCreated" CellPadding="4" ForeColor="#333333"
                                                    GridLines="None" AutoGenerateColumns="false">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                                        <asp:TemplateField HeaderText="Main Equipment">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtnameofsource" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Test Equipments">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txttestequipof" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Process/facility">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtprofaciof" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name & Address of Sub Contractor">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtnameaddof" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnoutsourcefac" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnoutsourcefac_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbRemoveOutfaci" runat="server" CssClass="fa fa-times" OnClick="lbRemoveOutfaci_Click"></asp:LinkButton>
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
                                            </div>
                                            <div class="clearfix mt10">
                                            </div>
                                            <p>List of Joint-Venture Facility</p>
                                            <asp:GridView ID="gvjointventure" runat="server" CssClass="table table-hover" ShowFooter="true"
                                                OnRowCreated="gvjointventure_RowCreated" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtjvfname" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Joint Venture Nature">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddljvfis" runat="server" CssClass="form-control">
                                                                <asp:ListItem>Indian</asp:ListItem>
                                                                <asp:ListItem>Foreign</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complete Address">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtjvfaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Official Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtjvfoffname" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Telephone No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtjvftele" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fax No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtjvffax" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email Id">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtjvfemail" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnjointven" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnjointven_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbRemovejointven" runat="server" CssClass="fa fa-times" OnClick="lbRemovejointven_Click"></asp:LinkButton>
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
                                            <div class="mt10 clearfix"></div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    <p>Future Plans</p>
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtfuture" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px" placeholder="Expansion program,Installation of new machinery, Additional Test facility">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10">
                                            </div>

                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="ocd" class="tab-pane">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <p>Name and Address of Product OEM</p>
                                            <asp:GridView ID="gvOEMNameadd" runat="server" CssClass="table table-hover" ShowFooter="true" CellPadding="4"
                                                ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="gvOEMNameadd_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtmanofficename1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OEM">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlOEM1" runat="server" CssClass="form-control">
                                                                <asp:ListItem>Indian</asp:ListItem>
                                                                <asp:ListItem>Foreign</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complete Address">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCAddrssMF1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Official Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtofficialNameMF1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Telephone No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttelephonenoMF1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fax No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtfaxnoMF1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email Id">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtemailidMF1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Authrization">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fuAUTHRIZATION1" runat="server" CssClass="form-control" />
                                                            <asp:HiddenField ID="hfauth1" runat="server" Value="" />
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnoem" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnoem_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbremoveoem" runat="server" CssClass="fa fa-times"
                                                                OnClick="lbremoveoem_Click"></asp:LinkButton>
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
                                            <div class="mt10 clearfix"></div>
                                            <p>Employees Details</p>
                                            <asp:GridView ID="gvemp1" runat="server" CssClass="table table-hover" ShowFooter="true"
                                                CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="gvemp1_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                                    <asp:TemplateField HeaderText="TOTAL Employees">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttotalempCI1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ADMINISTRATIVE">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtadministrativeCI1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TECHNICAL">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txttechCI1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NON TECHNICAL">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNontechCI1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QC/INSPECTION">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtqcCI1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SKILLED LABOUR">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtskCI1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UNSKILLED LABOUR">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtuLCI1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnempdetail" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnempdetail_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbempremove" runat="server" CssClass="fa fa-times"
                                                                OnClick="lbempremove_Click"></asp:LinkButton>
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
                                            <div class="clearfix mt10">
                                            </div>
                                            <div class="col-sm-5 row">
                                                <p>Do you have following certificates</p>
                                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    <asp:ListItem Value="1">Factory Licence /Municipal Shop's & Establishment</asp:ListItem>
                                                    <asp:ListItem Value="2">Registration Certificate from Labor Commissioner</asp:ListItem>
                                                    <asp:ListItem Value="3">VAT Registration Certificate</asp:ListItem>
                                                    <asp:ListItem Value="4">Excise Registration Certificate</asp:ListItem>
                                                    <asp:ListItem Value="5">Any other Certificate</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-sm-7 row">
                                                <div runat="server" id="div2" visible="false">
                                                    <p>Factory Licence /Municipal Shop's & Establishment</p>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                                    <p>Please upload all factory licence</p>
                                                </div>
                                                <div runat="server" id="div3" visible="false">
                                                    <p>Registration Certificate from Labor Commissioner</p>
                                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" />
                                                    <p>Please upload all Registration Certifcate</p>
                                                </div>
                                                <div runat="server" id="div4" visible="false">
                                                    <p>VAT Registration Certificate</p>
                                                    <asp:FileUpload ID="FileUpload3" runat="server" CssClass="form-control" />
                                                    <p>Please Upload all VAT Certificate</p>
                                                </div>
                                                <div runat="server" id="div5" visible="false">
                                                    <p>Excise Registration Certificate</p>
                                                    <asp:FileUpload ID="FileUpload4" runat="server" CssClass="form-control" />
                                                    <p>Pleas upload all factory Excise Registration</p>
                                                </div>
                                                <div runat="server" id="div6" visible="false">
                                                    <p>Name of Certificate</p>
                                                    <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <div class="clearfix mt10">
                                                    </div>
                                                    <p>Any other certificate</p>
                                                    <asp:FileUpload ID="FileUpload5" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="clearfix mt10">
                                            </div>
                                            <h4>Details of Inspection and Quality Control of facilites</h4>
                                            <div class="clearfix mt10">
                                            </div>
                                            <p>List of Test Facilities</p>
                                            <asp:GridView ID="gvtestfac1" runat="server" CssClass="table table-hover" ShowFooter="true"
                                                CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="gvtestfac1_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                                    <asp:TemplateField HeaderText="Type of GAUGE / Test Equipment">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtnametestfesi1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Make">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtmaketf1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Least Count">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcounttf1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Range of MEASURMENT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtrangetf1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CERTIFICATION YEAR">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcertiyeartf1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year of purchase">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtyearofpurtf1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btntestfaci" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btntestfaci_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbremovetestfacili" runat="server" CssClass="fa fa-times"
                                                                OnClick="lbremovetestfacili_Click"></asp:LinkButton>
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
                                            <div class="clearfix mt10"></div>
                                            <div class="form-group row">
                                                <div class="col-sm-5">
                                                    Is Lab accredited by NABL
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group  row" runat="server" id="div7" visible="false">
                                                <div class="col-sm-5">
                                                    If yes Certifiction is valid upto
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="TextBox10" runat="server" Type="date" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <div class="col-sm-5">
                                                <p>Quality Certifications</p>
                                                <asp:CheckBoxList ID="CheckBoxList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CheckBoxList2_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    <asp:ListItem Value="1">IMS</asp:ListItem>
                                                    <asp:ListItem Value="2">EnMS</asp:ListItem>
                                                    <asp:ListItem Value="3">QMS</asp:ListItem>
                                                    <asp:ListItem Value="4">Any other Certificate</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-sm-7">
                                                <div runat="server" id="div8" visible="false">
                                                    <asp:FileUpload ID="FileUpload6" runat="server" CssClass="form-control" />
                                                    <p>EnMS</p>
                                                </div>
                                                <div runat="server" id="div9" visible="false">
                                                    <asp:FileUpload ID="FileUpload7" runat="server" CssClass="form-control" />
                                                    <p>QMS</p>
                                                </div>
                                                <div runat="server" id="div10" visible="false">
                                                    <p>Name of Certificate</p>
                                                    <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <div class="clearfix mt10"></div>
                                                    <asp:FileUpload ID="FileUpload8" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="clearfix mt10">
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Do you have any Sales Office /Marketing Office
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <div class="form-group" runat="server" visible="false" id="Div11">
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Name of Nodal Officer
                                                    </div>
                                                    <div class="col-sm-7 row">
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <p>
                                                                First Name
                                                            </p>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <p>
                                                                Last Name
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Sales/Marketing Office Address
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>Street Address</p>
                                                        <div class="clearfix mt5"></div>
                                                        <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>Street Address Line 2</p>
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="TextBox17" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <p>
                                                                    City
                                                                </p>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="TextBox18" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <p>
                                                                    State / Province
                                                                </p>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="TextBox19" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <p>
                                                                    Postal / Zip Code
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Phone Number
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="TextBox20" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Fax Number
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="TextBox21" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-5">
                                                        Email
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="TextBox22" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>
                                                            example@example.com
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix mt10"></div>
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
                                                    <asp:Label ID="lblcin" runat="server" Text="CIN"></asp:Label>
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtCIN" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                    Are you registered with Govt. Department/Undertaking/PSU under Ministry of Defence/Gem
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
                                                <asp:GridView runat="server" ID="gvgovtundertaking" CssClass="table table-hover" AutoGenerateColumns="false"
                                                    ShowFooter="true" OnRowCreated="gvgovtundertaking_RowCreated"
                                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SrNoGovt" HeaderText="Sr.No" />
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtnameundertaking" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Registration No">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtregisnogovtpsu" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Certificate valid upto">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtcertificatevalidupto" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Please Upload Registration Certificate">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="furegiscerti" runat="server" CssClass="form-control" />
                                                                <asp:HiddenField ID="hffuregiscerti" runat="server" />
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnAddmoreGovtpsu" runat="server" CssClass="btn btn-primary pull-right"
                                                                    Text="Add New Row"
                                                                    OnClick="btnAddmoreGovtpsu_Click" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbremoveGOvtPSU" runat="server" CssClass="fa fa-times"
                                                                    OnClick="lbremoveGOvtPSU_Click"></asp:LinkButton>
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
                                                <div class="clearfix mt10"></div>
                                            </div>
                                            <div class="clearfix mt10"></div>
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
                                            <asp:GridView runat="server" ID="gvTURNOVERDURINGLAST3YEARS" CssClass="table table-hover" ShowFooter="true"
                                                AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="gvTURNOVERDURINGLAST3YEARS_RowCreated">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
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
                                                            <asp:HiddenField ID="hfprofitloss" runat="server" Value="" />
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnturnover" runat="server" Text="Add New Row"
                                                                CssClass="btn btn-primary pull-right" OnClick="btnturnover_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbturnover" runat="server" CssClass="fa fa-times"
                                                                OnClick="lbturnover_Click"></asp:LinkButton>
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
                                            <div class="clearfix mt10"></div>
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
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="chkList" class="tab-pane">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="panchklist" runat="server">
                                            <asp:DropDownList ID="ddltypeofchk" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddltypeofchk_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">MANUFACTURER</asp:ListItem>
                                                <asp:ListItem Value="2">SERVICE SUB CONTRACTOR</asp:ListItem>
                                                <asp:ListItem Value="3">Authorised Agent</asp:ListItem>
                                                <asp:ListItem Value="4">Trader </asp:ListItem>
                                                <asp:ListItem Value="5">OEM</asp:ListItem>
                                                <asp:ListItem Value="6">Stockist</asp:ListItem>
                                                <asp:ListItem Value="7">Contractor</asp:ListItem>
                                                <asp:ListItem Value="8">Distributor</asp:ListItem>
                                                <asp:ListItem Value="9">Consertium</asp:ListItem>
                                                <asp:ListItem Value="10">MSME</asp:ListItem>
                                                <asp:ListItem Value="11">start ups</asp:ListItem>
                                                <asp:ListItem Value="12">Individual</asp:ListItem>
                                                <asp:ListItem Value="13">Partnership</asp:ListItem>
                                                <asp:ListItem Value="14">Public Limited Company</asp:ListItem>
                                                <asp:ListItem Value="15">Private Limited Company</asp:ListItem>
                                                <asp:ListItem Value="16">Public Limited Company</asp:ListItem>
                                                <asp:ListItem Value="17">PSU/Govt. Undertaking</asp:ListItem>
                                                <asp:ListItem Value="18">Research Institutes</asp:ListItem>
                                                <asp:ListItem Value="19">Joint Ventures</asp:ListItem>
                                                <asp:ListItem Value="20">Trust </asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <asp:CheckBoxList ID="chklist" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" Visible="false" RepeatLayout="Flow">
                                                <asp:ListItem Value="1">MSME Certificate</asp:ListItem>
                                                <asp:ListItem Value="2">Proof of Address</asp:ListItem>
                                                <asp:ListItem Value="3">Certificate of Incoporation</asp:ListItem>
                                                <asp:ListItem Value="4">Authorized Distributor Certificate</asp:ListItem>
                                                <asp:ListItem Value="5">Contractor Licence</asp:ListItem>
                                                <asp:ListItem Value="6">Partnership Deed</asp:ListItem>
                                                <asp:ListItem Value="7">ISO Certificate</asp:ListItem>
                                                <asp:ListItem Value="8">IMS	QMS	ENMS</asp:ListItem>
                                                <asp:ListItem Value="9">Column24</asp:ListItem>
                                                <asp:ListItem Value="10">ITR of 3 Years</asp:ListItem>
                                                <asp:ListItem Value="11">Balance Sheet of Three years</asp:ListItem>
                                                <asp:ListItem Value="12">Bank Solvency Certificate</asp:ListItem>
                                                <asp:ListItem Value="13">VAT Certificate</asp:ListItem>
                                                <asp:ListItem Value="14">Factory Licence</asp:ListItem>
                                                <asp:ListItem Value="15">CIN certificate</asp:ListItem>
                                                <asp:ListItem Value="16">Registration Certifcate with Govt/PSU</asp:ListItem>
                                                <asp:ListItem Value="17">Excise Certificate</asp:ListItem>
                                                <asp:ListItem Value="18">Defence Licence</asp:ListItem>
                                            </asp:CheckBoxList>
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
                                            <asp:DropDownList ID="ddldebarredgovtcont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldebarredgovtcont_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <div class="col-sm-5">
                                                <asp:CheckBoxList ID="chkcontracts" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="chkcontracts_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    <asp:ListItem Value="1">Financial</asp:ListItem>
                                                    <asp:ListItem Value="2">Banning</asp:ListItem>
                                                    <asp:ListItem Value="3">Suspension</asp:ListItem>
                                                    <asp:ListItem Value="4">Tender holiday</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-sm-7">
                                                <div runat="server" id="divfin" visible="false">
                                                    <div class="">
                                                        <p>Applicable upto</p>
                                                        <asp:TextBox ID="txtdatestsrt" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div runat="server" id="div1" visible="false">
                                                    <div class="">
                                                        <p>Applicable upto</p>
                                                        <asp:TextBox ID="TextBox12" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div runat="server" id="div12" visible="false">
                                                    <div class="">
                                                        <p>Applicable upto</p>
                                                        <asp:TextBox ID="TextBox24" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div runat="server" id="div13" visible="false">
                                                    <div class="">
                                                        <p>Applicable upto</p>
                                                        <asp:TextBox ID="TextBox26" type="date" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="clearfix mt10"></div>
                                            <asp:CheckBoxList ID="chkbuisness" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="checkbox">
                                                <asp:ListItem>I/We note that registration ,does not carry with it the right to business with DPSUs/OFB, I/We hereby declare that the information pertaining to my/our firm/Company including all enclosures is correct and true to the best of
                                                   my/our knowledge and belief as on date</asp:ListItem>
                                            </asp:CheckBoxList>
                                            <div class="clearfix mt10"></div>

                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnsubmit">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:UpdatePanel runat="server" ID="UPSUBMIT">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary pull-right" Text="Save & Submit" OnClick="btnsubmit_Click" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger runat="server" ControlID="btnsubmit" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="UPSUBMIT">
                                                <ProgressTemplate>
                                                    <!---Progress Bar ---->
                                                    <div class="overlay-progress">
                                                        <div class="custom-progress-bar blue stripes">
                                                            <span></span>
                                                            <p>Processing</p>
                                                        </div>
                                                    </div>
                                                    <!---Progress Bar ---->
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
