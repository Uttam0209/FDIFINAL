<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_DetailofDefence.aspx.cs" Inherits="Vendor_V_DetailofDefence" MasterPageFile="~/Vendor/VendorMaster.master" %>

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
                    <div id="pimg" class="tab-pane">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                                    <asp:LinkButton ID="btnProductDetailAddMore" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnProductDetailAddMore_Click"></asp:LinkButton>
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
                                    <asp:GridView ID="gvproddetailedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvproddetailedit_RowCommand">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProductNomenClature" HeaderText="Product Nomenclature" />
                                            <asp:BoundField DataField="NatoGroup" HeaderText="Nato Group" />
                                            <asp:BoundField DataField="NatoClass" HeaderText="Nato Class" />
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                            <asp:BoundField DataField="HSNCode" HeaderText="HSN Code" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="hf1" Value='<%#Eval("MasterId") %>' />
                                                    <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewmfe"></asp:LinkButton>
                                                    <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updatenewmfe"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deletenewmfe"></asp:LinkButton>
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
                                                    <asp:LinkButton ID="btnAddTech" runat="server" CssClass="btn btn-primary pull-right"
                                                        Text="Add New Row"
                                                        OnClick="btnAddTech_Click"></asp:LinkButton>
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
                                    <asp:GridView ID="gvtechnologyedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvtechnologyedit_RowCommand">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProductNomenClature1" HeaderText="Product Nomenclature" />
                                            <asp:BoundField DataField="TechnologyLevel1" HeaderText="Technology 1" />
                                            <asp:BoundField DataField="TechnologyLevel2" HeaderText="Technology 2" />
                                            <asp:BoundField DataField="TechnologyLevel3" HeaderText="Technology 3" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="hf2" Value='<%#Eval("MasterId") %>' />
                                                    <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addtechedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updttechedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deleedittech"></asp:LinkButton>
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
                                                    <asp:LinkButton ID="btnAddRawMeterial" runat="server" CssClass="btn btn-primary pull-right"
                                                        Text="Add New Row"
                                                        OnClick="btnAddRawMeterial_Click"></asp:LinkButton>
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
                                    <asp:GridView ID="gvSourceofRawMaterialedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvSourceofRawMaterialedit_RowCommand">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Items" HeaderText="Items" />
                                            <asp:BoundField DataField="BasicRawMeterial" HeaderText="Basic Raw Material" />
                                            <asp:BoundField DataField="SourceofMaterial" HeaderText="Source of material" />
                                            <asp:BoundField DataField="Major_Raw_Material_Suppliers" HeaderText="Name of Major Raw Material Suppliers" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="hf3" Value='<%#Eval("MasterId") %>' />
                                                    <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addsrmedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updateaddsrmedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deleaddsrmedit"></asp:LinkButton>
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
                                                    <asp:LinkButton ID="btnAddSupplied" runat="server" CssClass="btn btn-primary pull-right"
                                                        Text="Add New Row"
                                                        OnClick="btnAddSupplied_Click"></asp:LinkButton>
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
                                    <asp:GridView ID="gvItemProducedandSuppliededit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvItemProducedandSuppliededit_RowCommand">
                                        <AlternatingRowStyle BackColor="White" />
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
                                            <asp:BoundField DataField="Date2" HeaderText="Date of Last Supply" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="hf4" Value='<%#Eval("MasterId") %>' />
                                                    <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="additemnew"></asp:LinkButton>
                                                    <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updateitemedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deleitemedit"></asp:LinkButton>
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
                                                    <asp:LinkButton ID="btnAddSupplied1" runat="server" CssClass="btn btn-primary pull-right"
                                                        Text="Add New Row"
                                                        OnClick="btnAddSupplied1_Click"></asp:LinkButton>
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
                                    <asp:GridView ID="gvItemSuppliedbutnotproducededit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvItemSuppliedbutnotproducededit_RowCommand">
                                        <AlternatingRowStyle BackColor="White" />
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
                                            <asp:BoundField DataField="Date2" HeaderText="Date of Last Supply" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="hf5" Value='<%#Eval("MasterId") %>' />
                                                    <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="editsuppprodedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updtsuppedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deleedit"></asp:LinkButton>
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
                                <div class="clearfix mt10"></div>
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right mr10" OnClick="btnsubmit_Click" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnsubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <div class="overlay-progress">
                                    <div class="custom-progress-bar blue stripes">
                                        <span></span>
                                        <p>Processing</p>
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%---Modal Popup Starts of All type GridView--%>
    <div class="modal fade" id="divmodal1" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div10">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Products Details</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField9" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Product Nomenclature
                                    </label>
                                    <asp:TextBox runat="server" ID="txtname" placeholder="Product Nomenclature" Class="form-control"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="txthsnedit" placeholder="HSN Code" Class="form-control"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lbsub" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lbsub_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divmodal2" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div2">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Technology Details (At Max 3 type of technologies)</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Product Nomenclature
                                    </label>
                                    <asp:TextBox runat="server" ID="txtnomentech" placeholder=" Product Nomenclature" Class="form-control"></asp:TextBox>
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
                                    <asp:DropDownList runat="server" ID="ddltech2edit" AutoPostBack="true" OnSelectedIndexChanged="ddltech2edit_SelectedIndexChanged" Class="form-control"></asp:DropDownList>

                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Technology 3
                                    </label>
                                    <asp:DropDownList runat="server" ID="ddltech3edit" Class="form-control"></asp:DropDownList>

                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lbsub2" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lbsub2_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divmodal3" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div4">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Source of Raw Material</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
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
                                        <asp:ListItem>Indigenized</asp:ListItem>
                                        <asp:ListItem>Imported</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Name of Major Raw Material Suppliers
                                    </label>
                                    <asp:TextBox runat="server" ID="txtmajorname" placeholder="Name of Major Raw Material Suppliers" Class="form-control"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lb3" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lb3_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divmodal4" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div6">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Item Produced and Supplied</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
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
                                    <asp:TextBox runat="server" ID="txtlastsuppedit" placeholder="Date of Last Supply" Class="form-control"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lb4" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lb4_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divmodal5" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div8">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Item Supplied but not produced</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
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
                                    <asp:TextBox runat="server" ID="txtdatesupedit1" placeholder=" Date of Last Supply" Class="form-control"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lb5" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lb5_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
</asp:Content>
