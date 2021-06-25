<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_DetailofDefence.aspx.cs" Inherits="Vendor_V_DetailofDefence" MasterPageFile="~/Vendor/VendorMaster.master" %>

<%@ Register Src="~/Vendor/InnerMenu.ascx" TagName="Head" TagPrefix="Menu" %>
<asp:Content runat="server" ID="msthead" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
            <div class="container-fluid">
                <div class="card-body">
                    <Menu:Head ID="inner" runat="server" />
                    <div class="tab-content">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box">
                                    <h4 class="page-title">Company Name :
                        <asp:Label runat="server" ID="lbcomp"></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card card-h-100">
                                    <div class="card-body">
                                        <h4 class="header-title mb-1">Products Details
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvproddetail" runat="server" CssClass="table table-bordered table-centered mb-0" ShowFooter="true"
                                                    AutoGenerateColumns="false" OnRowCreated="gvproddetail_RowCreated">
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNumberProd" HeaderText="SR. No" />
                                                        <asp:TemplateField HeaderText="Product Nomenclature">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtproductnomen" runat="server" CssClass="form-control" onkeyup="autobind(this);"></asp:TextBox>
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
                                                                <asp:TextBox ID="txthsnno" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txthsnno_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="btnProductDetailAddMore" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnProductDetailAddMore_Click"></asp:LinkButton>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbProductDetail" runat="server" CssClass="btn btn-sm btn-warning"
                                                                    OnClick="lbProductDetail_Click"><i class="fa fa-times"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="gvproddetailedit" runat="server" CssClass="table table-bordered table-centered mb-0"
                                                    ShowFooter="true" AutoGenerateColumns="false" OnRowCommand="gvproddetailedit_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProductNomenClature" HeaderText="Product Nomenclature" />
                                                        <asp:BoundField DataField="NG" HeaderText="Nato Group" />
                                                        <asp:BoundField DataField="NC" HeaderText="Nato Class" />
                                                        <asp:BoundField DataField="mItemCode" HeaderText="Item Code" />
                                                        <asp:BoundField DataField="HSNCode" HeaderText="HSN Code" />
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>

                                                                <asp:HiddenField runat="server" ID="HiddenField1" Value='<%#Eval("NatoGroup") %>' />
                                                                <asp:HiddenField runat="server" ID="HiddenField2" Value='<%#Eval("NatoClass") %>' />
                                                                <asp:HiddenField runat="server" ID="HiddenField4" Value='<%#Eval("ItemCode") %>' />
                                                                <asp:HiddenField runat="server" ID="hf1" Value='<%#Eval("VDetailDefenceId") %>' />
                                                                <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" CssClass="btn btn-sm btn-success"
                                                                    CommandArgument='<%#Eval("VDetailDefenceId") %>' CommandName="addnewmfe"><i Class="fa fa-plus-circle"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" CssClass="btn btn-sm btn-warning"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updatenewmfe"><i Class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" CssClass="btn btn-sm btn-danger"
                                                                    CommandArgument='<%#Eval("VDetailDefenceId") %>' CommandName="deletenewmfe"><i Class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <h4 class="header-title mb-1 mt-1">Technology Details (At Max 3 type of technologies)
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvtechnology" runat="server" CssClass="table table-bordered table-centered mb-0" ShowFooter="true"
                                                    AutoGenerateColumns="false" OnRowCreated="gvtechnology_RowCreated">
                                                    <Columns>
                                                        <asp:BoundField DataField="RowNumberTech" HeaderText="SR. No" />
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
                                                                <asp:DropDownList ID="ddltech2" runat="server" CssClass="form-control">
                                                                    <%--OnSelectedIndexChanged="txttech2_SelectedIndexChanged"--%>
                                                                </asp:DropDownList>
                                                                 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Technology 3" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltech3" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtechremove" runat="server" CssClass="btn btn-sm btn-warning"
                                                                    OnClick="lbtechremove_Click"><i class="fa fa-times"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="gvtechnologyedit" runat="server" CssClass="table table-bordered table-centered mb-0" ShowFooter="true"
                                                    AutoGenerateColumns="false" OnRowCommand="gvtechnologyedit_RowCommand">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="TechNomenclature" HeaderText="Product Nomenclature" />
                                                        <asp:BoundField DataField="Tech1" HeaderText="Technology 1" />
                                                        <asp:BoundField DataField="Tech2" HeaderText="Technology 2" />
                                                        <%--<asp:BoundField DataField="Technology3" HeaderText="Technology 3" Visible="false" />--%>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                 <asp:HiddenField runat="server" ID="hf1tech" Value='<%#Eval("Technology1") %>' />
                                                                <asp:HiddenField runat="server" ID="hf2tech" Value='<%#Eval("Technology2") %>' />
                                                                <asp:HiddenField runat="server" ID="hf2" Value='<%#Eval("VTechId") %>' />
                                                                <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="btn btn-sm btn-success" CommandArgument='<%#Eval("VTechId") %>' CommandName="addtechedit"><i Class="fa fa-plus-circle"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="btn btn-sm btn-warning" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updttechedit"><i Class="fa fa-edit"></i> </asp:LinkButton>
                                                                <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" Class="btn btn-sm btn-danger" CommandArgument='<%#Eval("VTechId") %>' CommandName="deleedittech"><i Class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <h4 class="header-title mb-1 mt-1">Source of Raw Material
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvSourceofRawMaterial" runat="server" CssClass="table table-bordered table-centered mb-0"
                                                    AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="gvSourceofRawMaterial_RowCreated">
                                                    <Columns>
                                                        <asp:BoundField DataField="SrNoRawMeterail" HeaderText="SR. No" />
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
                                                        <asp:TemplateField HeaderText="Name of  Raw Material Suppliers">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtmaterialsupplier" runat="server" CssClass="form-control">                                           
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="btnAddRawMeterial" runat="server" CssClass="btn btn-primary pull-right"
                                                                    Text="Add New Row"
                                                                    OnClick="btnAddRawMeterial_Click"></asp:LinkButton>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbmeterailremove" runat="server" CssClass="btn btn-sm btn-warning"
                                                                    OnClick="lbmeterailremove_Click"><i class="fa fa-times"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="gvSourceofRawMaterialedit" runat="server" CssClass="table table-bordered table-centered mb-0" ShowFooter="true"
                                                    AutoGenerateColumns="false" OnRowCommand="gvSourceofRawMaterialedit_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Items" HeaderText="Items" />
                                                        <asp:BoundField DataField="BasicRawMeterial" HeaderText="Basic Raw Material" />
                                                        <asp:TemplateField HeaderText="Source of material">
                                                            <ItemTemplate>
                                                                <asp:Label Text='<%# Eval("SourceofMaterial").ToString() == "1" ? "Indigenized" : "Imported" %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Major_Raw_Material_Suppliers" HeaderText="Name of Major Raw Material Suppliers" />
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hf3" Value='<%#Eval("VRawMaterialId") %>' />
                                                                <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="btn btn-sm btn-success" CommandArgument='<%#Eval("VRawMaterialId") %>' CommandName="addsrmedit"><i Class="fa fa-plus-circle"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="btn btn-sm btn-warning" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updateaddsrmedit"><i Class="fa fa-edit"></i> </asp:LinkButton>
                                                                <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" Class="btn btn-sm btn-danger" CommandArgument='<%#Eval("VRawMaterialId") %>' CommandName="deleaddsrmedit"><i Class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <h3 class="mb-2">Successfully Completed Supply Orders in last 3 years to reputed Customers
                                        </h3>
                                        <h4 class="header-title mb-1 mt-1">Item Produced and Supplied
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvItemProducedandSupplied" runat="server" CssClass="table table-bordered table-centered mb-0"
                                                    AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="gvItemProducedandSupplied_RowCreated">
                                                    <Columns>
                                                        <asp:BoundField DataField="SrNoSpplied" HeaderText="SR. No" />
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
                                                        <asp:TemplateField HeaderText="S.O. No.	">
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
                                                        <asp:TemplateField HeaderText="Date Last Supply">
                                                            <ItemTemplate>
                                                                <div class="input-append date" id="datePicker" data-date="12-02-2012" data-date-format="dd-mm-yyyy" style="margin-top: -15px;">
                                                                    <span class="add-on"><i class="icon-th"></i></span>
                                                                    <asp:TextBox ID="txtdateoflastsupplie" runat="server" type="date" CssClass="form-control datePicker" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="btnAddSupplied" runat="server" CssClass="btn btn-primary pull-right"
                                                                    Text="Add New Row"
                                                                    OnClick="btnAddSupplied_Click"></asp:LinkButton>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbSuplliedremove" runat="server" CssClass="btn btn-sm btn-warning"
                                                                    OnClick="lbSuplliedremove_Click"><i class="fa fa-times"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="gvItemProducedandSuppliededit" runat="server" CssClass="table table-bordered table-centered mb-0" ShowFooter="true"
                                                    AutoGenerateColumns="false" OnRowCommand="gvItemProducedandSuppliededit_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Reputed_Customer" HeaderText="Name of Reputed Customer" />
                                                        <asp:BoundField DataField="Description" HeaderText="Description of Store Supplied" />
                                                        <asp:BoundField DataField="SupplyNoDate" HeaderText="S.O. No.and Date" />
                                                        <asp:BoundField DataField="OrderQuantity" HeaderText="Order Qty." />
                                                        <asp:BoundField DataField="SuppliedQuantity" HeaderText="Value Qty Supplied" />
                                                        <asp:BoundField DataField="Date2" HeaderText="Date of Last Supply" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hf4" Value='<%#Eval("VSupplyId") %>' />
                                                                <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="btn btn-sm btn-success" CommandArgument='<%#Eval("VSupplyId") %>' CommandName="additemnew"><i Class="fa fa-plus-circle"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="btn btn-sm btn-warning" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updateitemedit"><i Class="fa fa-edit"></i> </asp:LinkButton>
                                                                <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" Class="btn btn-sm btn-danger" CommandArgument='<%#Eval("VSupplyId") %>' CommandName="deleitemedit"><i Class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <h4 class="header-title mb-1 mt-1">Item Produced and Supplied
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvItemSuppliedbutnotproduced" runat="server" CssClass="table table-bordered table-centered mb-0"
                                                    AutoGenerateColumns="false" ShowFooter="true" OnRowCreated="gvItemSuppliedbutnotproduced_RowCreated">
                                                    <Columns>
                                                        <asp:BoundField DataField="SrNoSpplied1" HeaderText="SR. No" />
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
                                                        <asp:TemplateField HeaderText="S.O. No.	">
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
                                                        <asp:TemplateField HeaderText="Date Last Supply">
                                                            <ItemTemplate>
                                                                <div class="input-append date" id="datePicker" data-date="12-02-2012" data-date-format="dd-mm-yyyy" style="margin-top: -15px;">
                                                                    <span class="add-on"><i class="icon-th"></i></span>
                                                                    <asp:TextBox ID="txtdateoflastsupplie1" runat="server" type="date" CssClass="form-control datePicker" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                                </div>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="btnAddSupplied1" runat="server" CssClass="btn btn-primary pull-right"
                                                                    Text="Add New Row"
                                                                    OnClick="btnAddSupplied1_Click"></asp:LinkButton>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbSuplliedremove1" runat="server" CssClass="btn btn-sm btn-warning"
                                                                    OnClick="lbSuplliedremove1_Click"><i class="fa fa-times"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="gvItemSuppliedbutnotproducededit" runat="server"
                                                    CssClass="table table-bordered table-centered mb-0" ShowFooter="true"
                                                    AutoGenerateColumns="false" OnRowCommand="gvItemSuppliedbutnotproducededit_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Reputed_Customer" HeaderText="Name of Reputed Customer" />
                                                        <asp:BoundField DataField="Description" HeaderText="Description of Store Supplied" />
                                                        <asp:BoundField DataField="SupplyNoDate" HeaderText="S.O. No.and Date" />
                                                        <asp:BoundField DataField="OrderQuantity" HeaderText="Order Qty." />
                                                        <asp:BoundField DataField="SuppliedQuantity" HeaderText="Value Qty Supplied" />
                                                        <asp:BoundField DataField="Date2" HeaderText="Date of Last Supply" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hf5" Value='<%#Eval("VSupplyId") %>' />
                                                                <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" CssClass="btn btn-sm btn-success" CommandArgument='<%#Eval("VSupplyId") %>' CommandName="editsuppprodedit"><i Class="fa fa-plus-circle"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" class="btn btn-sm btn-warning" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updtsuppedit"><i Class="fa fa-edit"></i> </asp:LinkButton>
                                                                <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" Class="btn btn-sm btn-danger" CommandArgument='<%#Eval("VSupplyId") %>' CommandName="deleedit"><i Class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-right mt-3">
                                            <asp:LinkButton ID="btnPrev" runat="server" Text="Previous" CssClass="btn btn-secondary"
                                                OnClick="btnPrev_Click"> </asp:LinkButton>
                                            <asp:Button ID="btnsubmit" runat="server" Text="Save" CssClass="btn btn-success"
                                                OnClick="btnsubmit_Click" />
                                            <asp:LinkButton ID="btnNext" runat="server" Text="Next"
                                                CssClass="btn btn-primary" OnClick="btnNext_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="modelmsg" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-bell "></i>&nbsp;Alert</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server" ID="uep2" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <h4>
                                        <asp:Label runat="server" ID="lblmsg" ForeColor="Red"></asp:Label></h4>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="divmodal1" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-info "></i>&nbsp;PRODUCTS DETAILS</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="HiddenField9" runat="server" />
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Product Nomenclature
                                        </label>
                                        <asp:TextBox runat="server" ID="txtname" placeholder="Product Nomenclature" onkeyup="autobind(this);" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Nato Group
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlnatogroup" Class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlnatogroup_SelectedIndexChanged1"></asp:DropDownList>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Nato Class
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlnatoclassedit" Class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlnatoclassedit_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Item Code
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlitemcodeedit" Class="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            HSN Code
                                        </label>
                                        <asp:TextBox runat="server" ID="txthsnedit" placeholder="HSN Code" onkeypress="return isNumberKey(event)" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="text-right mt-2">
                                            <asp:LinkButton ID="lbsub" runat="server" Text="Edit & Update" CssClass="btn btn-primary" OnClick="lbsub_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="divmodal2" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-info "></i>&nbsp;Technology Details (At Max 3 type of technologies)</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Product Nomenclature
                                        </label>
                                        <asp:TextBox runat="server" ID="txtnomentech" placeholder=" Product Nomenclature" onblur="SaveData('txtnomentech')" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Technology 1	
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddltech1edit" AutoPostBack="true" OnSelectedIndexChanged="ddltech1edit_SelectedIndexChanged" Class="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Technology 2
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddltech2edit" Class="form-control"></asp:DropDownList>
                                    </div>
                                    <%--<div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Technology 3
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddltech3edit" Class="form-control"></asp:DropDownList>
                                    </div>--%>
                                    <div class="col-md-12">
                                        <div class="text-right mt-2">
                                            <asp:LinkButton ID="lbsub2" runat="server" Text="Edit & Update" CssClass="btn btn-primary" OnClick="lbsub2_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="divmodal3" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-info "></i>&nbsp;Source of Raw Material</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Items
                                        </label>
                                        <asp:TextBox runat="server" ID="txtitemsedit" placeholder="Items" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Basic Raw Material
                                        </label>
                                        <asp:TextBox runat="server" ID="txtbasicedit" placeholder="Basic Raw Material" TextMode="MultiLine" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Source of material
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlsourceedit" Class="form-control">
                                            <asp:ListItem Value="1">Indigenized</asp:ListItem>
                                            <asp:ListItem Value="2">Imported</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Name of Major Raw Material Suppliers
                                        </label>
                                        <asp:TextBox runat="server" ID="txtmajorname" placeholder="Name of Major Raw Material Suppliers" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="text-right mt-2">
                                            <asp:LinkButton ID="lb3" runat="server" Text="Edit & Update" CssClass="btn btn-primary" OnClick="lb3_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="divmodal4" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-info "></i>&nbsp;Item Produced and Supplied</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Name of Reputed Customer
                                        </label>
                                        <asp:TextBox runat="server" ID="txtrepcustedit" placeholder="Name of Reputed Customer" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Description of Store Supplied
                                        </label>
                                        <asp:TextBox runat="server" ID="txtstoreedit" placeholder="Description of Store Supplied" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            S.O. No.and Date
                                        </label>
                                        <asp:TextBox runat="server" ID="txtsnoedit" Palceholder="S.O. No.and Date" Class="form-control">
                                      
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Order Qty.
                                        </label>
                                        <asp:TextBox runat="server" ID="txtorderqtyedit" placeholder="Order Qty" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Value Qty Supplied
                                        </label>
                                        <asp:TextBox runat="server" ID="txtvalueqtyedit" placeholder="Value Qty Supplied" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Date of Last Supply
                                        </label>
                                        <asp:TextBox runat="server" ID="txtlastsuppedit" type="date" placeholder="Date of Last Supply" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="text-right mt-2">
                                            <asp:LinkButton ID="lb4" runat="server" Text="Edit & Update" CssClass="btn btn-primary" OnClick="lb4_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>
            <div class="modal fade" id="divmodal5" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-info "></i>&nbsp;Item Supplied but not produced</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="HiddenField7" runat="server" />
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Name of Reputed Customer
                                        </label>
                                        <asp:TextBox runat="server" ID="txtrepcustedit1" placeholder="Name of Reputed Customer" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Description of Store Supplied
                                        </label>
                                        <asp:TextBox runat="server" ID="txtstoredit1" placeholder="Description of Store Supplied" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            S.O. No.and Date
                                        </label>
                                        <asp:TextBox runat="server" ID="txtdateedit1" Placeholder="S.O. No.and Date" Class="form-control">                                     
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Order Qty.
                                        </label>
                                        <asp:TextBox runat="server" ID="txtirderedit1" placeholder="Order Qty." Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Value Qty Supplied
                                        </label>
                                        <asp:TextBox runat="server" ID="txtqtysubedit1" placeholder="Value Qty Supplied" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Date of Last Supply
                                        </label>
                                        <asp:TextBox runat="server" ID="txtdatesupedit1" type="date" placeholder=" Date of Last Supply" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="text-right mt-2">
                                            <asp:LinkButton ID="lb5" runat="server" Text="Edit & Update" CssClass="btn btn-primary" OnClick="lb5_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function showPopup() {
            $('#divmodal1').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#divmodal2').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup2() {
            $('#divmodal3').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup3() {
            $('#divmodal4').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup4() {
            $('#divmodal5').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup5() { $('#modelmsg').modal('show', function () { }); }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function autobind(obj1) {
            $("[id$=" + obj1.id + "]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("V_DetailofDefence.aspx/GetCustomers") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0]
                                    // val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                minLength: 1
            });

        }
    </script>
</asp:Content>
