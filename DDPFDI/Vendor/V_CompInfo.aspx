<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_CompInfo.aspx.cs" Inherits="Vendor_V_CompInfo" MasterPageFile="~/Vendor/VendorMaster.master" %>

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
                <asp:UpdatePanel ID="up" runat="server">
                    <ContentTemplate>
                        <div class="cacade-forms">
                            <div class="clearfix mt10"></div>
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
                                        <asp:TemplateField HeaderText="Factory GST No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXTFACGSTNO" runat="server" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Complete postal Address">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCAddrssMF" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Official Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtofficialNameMF" runat="server" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Telephone No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txttelephonenoMF" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fax No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfaxnoMF" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email Id">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtemailidMF" runat="server" TextMode="Email" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtemailidMF_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="btnAddManufac" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnAddManufac_Click"></asp:LinkButton>
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
                                <asp:GridView ID="gvmanufacilityedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvmanufacilityedit_RowCommand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name_of_Factory" HeaderText="Name of Factory" />
                                        <asp:BoundField DataField="Factory_GSTNo" HeaderText="Factory GST No" />
                                        <asp:BoundField DataField="Comp_Postal_Address" HeaderText="Complete postal Address" />
                                        <asp:BoundField DataField="Contact_Official_Name" HeaderText="Contact Official Name" />
                                        <asp:BoundField DataField="Telephone_No" HeaderText="Telephone No" />
                                        <asp:BoundField DataField="Fax_No" HeaderText="Fax No" />
                                        <asp:BoundField DataField="Email_Id" HeaderText="Email Id" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfmanuid" runat="server" Value='<%#Eval("MasterId") %>' />
                                                <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewmfe"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="fa fa-edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="updatenewmfe"></asp:LinkButton>
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
                                <p>Area Details</p>
                                <asp:GridView ID="gvareadetail" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="gvareadetail_RowCreated">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="Sno" HeaderText="Raw Number" />
                                        <asp:TemplateField HeaderText="Name of Factory">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAreaFactoryName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PRODUCTION AREA (sqm)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtprodarea" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INSPECTION AREA (sqm)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtinsarea" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TOTAL COVERED AREA (sqm)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txttotalcoverdarea" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Area (sqm)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txttotalarea" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                            <%--  <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="btnAddArea" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnAddArea_Click1"></asp:LinkButton>
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <%--   <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblRemoveArea" runat="server" CssClass="fa fa-times" OnClick="lblRemoveArea_Click1"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
                                <asp:GridView ID="gvareadetailedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvareadetailedit_RowCommand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Area_Factory_Name" HeaderText="Name of Factory" />
                                        <asp:BoundField DataField="PRODUCTION_AREA" HeaderText="PRODUCTION AREA (sqm)" />
                                        <asp:BoundField DataField="INSPECTION_AREA" HeaderText="INSPECTION AREA (sqm)" />
                                        <asp:BoundField DataField="TOTAL_COVERED_AREA" HeaderText="TOTAL COVERED AREA (sqm)" />
                                        <asp:BoundField DataField="Total_Area" HeaderText="Total Area (sqm)" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfareadetailedit" runat="server" Value='<%#Eval("MasterId")%>' />
                                                <asp:LinkButton ID="lbladdnewareadetailedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewad"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewareadetailedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updatenewad"></asp:LinkButton>
                                                <asp:LinkButton ID="lbldeletenewareadetailedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deletenewad"></asp:LinkButton>
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
                                                <asp:TextBox ID="txtQuanProdManu" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date of Purchase">
                                            <ItemTemplate>
                                                <div class="input-append date" id="datePicker" data-date="12-02-2012" data-date-format="dd-mm-yyyy" style="margin-top: -15px;">
                                                    <span class="add-on"><i class="icon-th"></i></span>
                                                    <asp:TextBox ID="txtplantmachiPurchase" runat="server" CssClass="form-control datePicker" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Usage">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPlantMachiUsage" runat="server" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="btnAddPlantorMachine" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnAddPlantorMachine_Click"></asp:LinkButton>
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
                                <asp:GridView ID="gvplantandmachinesedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvplantandmachinesedit_RowCommand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Description_Machine_Model_Specs" HeaderText="Description of Machine & Model Specs" />
                                        <asp:BoundField DataField="Make" HeaderText="Make" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="Date_of_Purchase" HeaderText="Date of Purchase" />
                                        <asp:BoundField DataField="Usage" HeaderText="Usage" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfplantmachineedit" runat="server" Value='<%#Eval("MasterId")%>' />
                                                <asp:LinkButton ID="lbladdnewplantandmachinesedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewplantandmachinesedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewplantandmachinesedit" runat="server" Class="fa fa-edit" CommandArgument='<%((GridViewRow) Container).RowIndex %>' CommandName="updatenewplantandmachinesedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbldeletenewplantandmachinesedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deletenewplantandmachinesedit"></asp:LinkButton>
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
                                            <%--  <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="btnEmpInfo" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnEmpInfo_Click"></asp:LinkButton>
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbRemoveEmp" runat="server" CssClass="fa fa-times" OnClick="lbRemoveEmp_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
                                <asp:GridView ID="gvempCompInfoedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvempCompInfoedit_RowCommand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TOTAL_Employees" HeaderText="TOTAL Employees" />
                                        <asp:BoundField DataField="ADMINISTRATIVE" HeaderText="ADMINISTRATIVE" />
                                        <asp:BoundField DataField="TECHNICAL" HeaderText="TECHNICAL" />
                                        <asp:BoundField DataField="NON_TECHNICAL" HeaderText="NON TECHNICAL" />
                                        <asp:BoundField DataField="QC_INSPECTION" HeaderText="QC/INSPECTION" />
                                        <asp:BoundField DataField="SKILLED_LABOUR" HeaderText="SKILLED LABOUR" />
                                        <asp:BoundField DataField="UNSKILLED_LABOUR" HeaderText="UNSKILLED LABOUR" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfcompinfo" runat="server" Value='<%#Eval("MasterId")%>' />
                                                <asp:LinkButton ID="lbladdnewempCompInfoedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewempCompInfoedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewempCompInfoedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updatenewempCompInfoedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbldeletenewempCompInfoedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deletenewempCompInfoedit"></asp:LinkButton>
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
                                        <asp:TemplateField HeaderText="Unit of MEASURMENT">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtunitofmeas" runat="server" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CERTIFICATION YEAR">
                                            <ItemTemplate>
                                                <div class="input-append date" id="datePicker1" data-date="12-02-2012" data-date-format="dd-mm-yyyy" style="margin-top: -15px;">
                                                    <span class="add-on"><i class="icon-th"></i></span>
                                                    <asp:TextBox ID="txtcertiyeartf" runat="server" CssClass="form-control datePicker" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Year of purchase">
                                            <ItemTemplate>
                                                <div class="input-append date" id="datePicker2" data-date="12-02-2012" data-date-format="dd-mm-yyyy" style="margin-top: -15px;">
                                                    <span class="add-on"><i class="icon-th"></i></span>
                                                    <asp:TextBox ID="txtyearofpurtf" runat="server" CssClass="form-control datePicker" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="btntestfacilities" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btntestfacilities_Click"></asp:LinkButton>
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
                                <asp:GridView ID="gvtestfacilitiesedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvtestfacilitiesedit_RowCommand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Type_of_GAUGE_Test_Equipment" HeaderText="Type of GAUGE / Test Equipment" />
                                        <asp:BoundField DataField="Test_Make" HeaderText="Make" />
                                        <asp:BoundField DataField="Least_Count" HeaderText="Least Count" />
                                        <asp:BoundField DataField="Range_of_MEASURMENT" HeaderText="Range of MEASURMENT" />
                                         <asp:BoundField DataField="Unit_of_MEASURMENT" HeaderText="Unit of MEASURMENT" />
                                        <asp:BoundField DataField="CERTIFICATION_YEAR" HeaderText="CERTIFICATION YEAR" />
                                        <asp:BoundField DataField="Year_of_purchase" HeaderText="Year of purchase" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hftestfacilities" runat="server" Value='<%#Eval("MasterId")%>' />
                                                <asp:LinkButton ID="lbladdnewtestfacilitiesedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewtestfacilitiesedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewtestfacilitiesedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updatenewtestfacilitiesedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbldeletenewtestfacilitiesedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deletenewtestfacilitiesedit"></asp:LinkButton>
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
                                        <div class="input-append date" id="datePicker3" data-date="12-02-2012" data-date-format="dd-mm-yyyy">
                                            <span class="add-on"><i class="icon-th"></i></span>
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control datePicker" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix mt10"></div>

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
                                            <asp:TextBox ID="txtlname" runat="server" CssClass="form-control"></asp:TextBox>
                                            <p>
                                                Name
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Sales/Marketing Office Address
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtstreetaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                            <p>Street Address</p>
                                            <div class="clearfix mt5"></div>
                                            <asp:TextBox ID="txtstreetaddressline2" runat="server" CssClass="form-control"></asp:TextBox>
                                            <p>Street Address Line 2</p>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtcity" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>
                                                        City
                                                    </p>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtstate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>
                                                        State
                                                    </p>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtpincode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <p>
                                                        Postal
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
                                            <asp:TextBox ID="txtcontactno" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Fax Number
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtfaxno" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Email
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
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
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                                    <asp:TextBox ID="txtDPhone" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fax">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDFax" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnautdeal" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnautdeal_Click"></asp:LinkButton>
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
                                    <asp:GridView ID="gvauthdealaddressedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvauthdealaddressedit_RowCommand">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DistributorName" HeaderText="Distributor Name" />
                                            <asp:BoundField DataField="DistributorStreetAddress" HeaderText="StreetAddress" />
                                            <asp:BoundField DataField="DistributorState" HeaderText="State" />
                                            <asp:BoundField DataField="DistributorPincode" HeaderText="PinCode" />
                                            <asp:BoundField DataField="DistributorPhone" HeaderText="Phone" />
                                            <asp:BoundField DataField="DistributorFax" HeaderText="Fax" />
                                            <asp:BoundField DataField="DistributorEmail" HeaderText="Email" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="hfauth" Value='<%#Eval("MasterId") %>' />
                                                    <asp:LinkButton ID="lbladdnewauthdealaddressedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewauthdealaddressedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lblupdatenewauthdealaddressedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updatenewauthdealaddressedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldeletenewauthdealaddressedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deletenewauthdealaddressedit"></asp:LinkButton>
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
                                                <asp:LinkButton ID="btnoutsourcefac" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnoutsourcefac_Click"></asp:LinkButton>
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
                                <asp:GridView ID="gvoutsourcefacilityedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvoutsourcefacilityedit_RowCommand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OutsourcingMainEquipment" HeaderText="Main Equipment" />
                                        <asp:BoundField DataField="OutsourcingTestEquip" HeaderText="Test Equipments" />
                                        <asp:BoundField DataField="OutsourcingProcessfacility" HeaderText="Process/facility" />
                                        <asp:BoundField DataField="OutsoursingNameAddressofSubContractor" HeaderText="Name & Address of Sub Contractor" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hfoutscr" Value='<%#Eval("MasterId") %>' />
                                                <asp:LinkButton ID="lbladdnewoutsourcefacilityedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewoutsourcefacilityedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewoutsourcefacilityedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updatenewoutsourcefacilityedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbldeletenewoutsourcefacilityedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deletenewoutsourcefacilityedit"></asp:LinkButton>
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
                                                <asp:TextBox ID="txtjvftele" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fax No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtjvffax" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email Id">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtjvfemail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="btnjointven" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnjointven_Click"></asp:LinkButton>
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
                                <asp:GridView ID="gvjointventureedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvjointventureedit_RowCommand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="JointVentureName" HeaderText="Name" />
                                        <asp:BoundField DataField="IsJointVentureCountry" HeaderText="Is Joint Venture Nature" />
                                        <asp:BoundField DataField="CompleteAddress" HeaderText="Complete Address" />
                                        <asp:BoundField DataField="ContOfficialName" HeaderText="Contact Official Name" />
                                        <asp:BoundField DataField="TelephoneNo" HeaderText="Telephone No" />
                                        <asp:BoundField DataField="FaxNo" HeaderText="Fax No" />
                                        <asp:BoundField DataField="EmailId" HeaderText="Email Id" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hfjoint" Value='<%#Eval("MasterId") %>' />
                                                <asp:LinkButton ID="lbladdnewjointventureedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewjointventureedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewjointventureedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updatenewjointventureedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbldeletenewjointventureedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deletenewjointventureedit"></asp:LinkButton>
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
                                <asp:GridView runat="server" ID="gvcertificate" CssClass="table table-hover"
                                    ShowFooter="true" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkcertificate" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamecertificate" runat="server" Text='<%#Eval("CertificateName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Upload Certificate">
                                            <ItemTemplate>
                                                <asp:FileUpload runat="server" Class="file-upload" ID="fuuploadcertificate" />
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
                                <asp:GridView ID="gvcertificateedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvcertificateedit_RowCommand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Uploaded Certificate">
                                            <ItemTemplate>
                                                <a href='<%#Eval("Path","https://srijandefence.gov.in/Upload/VendorImage/{0}") %>' runat="server" id="imgeditcerti" target="_blank"><%#Eval("Path") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbladdnewcertificateedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("ImageID") %>' CommandName="addnewecertificateedit"></asp:LinkButton>
                                                <%--<asp:LinkButton ID="lblupdatenewcertificateedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updatenewcertificateedit"></asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbldeletenewcertificateedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("ImageID") %>' CommandName="deletenewcertificateedit"></asp:LinkButton>
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
                                <asp:GridView runat="server" ID="gvchkqualitycertificate" CssClass="table table-hover"
                                    ShowFooter="true" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="Qchkcertificate" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnameQ" runat="server" Text='<%#Eval("QCertificateName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Upload Certificate">
                                            <ItemTemplate>
                                                <asp:FileUpload runat="server" Class="file-upload" ID="fuQuploadcertificate" />
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
                                <asp:GridView ID="gvchkqualitycertificateedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gvchkqualitycertificateedit_RowCommand">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Uploaded Certificate">
                                            <ItemTemplate>
                                                <a href='<%#Eval("Path","https://srijandefence.gov.in/Upload/VendorImage/{0}") %>' runat="server" id="imgeditqcerti" target="_blank"><%#Eval("Path") %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbladdnewchkqualitycertificateedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("ImageID") %>' CommandName="addnewechkqualitycertificateedit"></asp:LinkButton>
                                                <%--<asp:LinkButton ID="lblupdatenewchkqualitycertificateedit" runat="server" Class="fa fa-edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="updatenewchkqualitycertificateedit"></asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbldeletenewchkqualitycertificateedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("ImageID") %>' CommandName="deletenewchkqualitycertificateedit"></asp:LinkButton>
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
                            <div class="clearfix pb15"></div>
                            <asp:LinkButton ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right mr10" OnClick="btnsubmit_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-primary pull-right mr10" OnClick="btncancel_Click"></asp:LinkButton>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnsubmit" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up">
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
    <%---Modal Popup Starts of All type GridView--%>
    <div class="modal fade" id="changePass" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="p1">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">List of Manufacturing Facilities</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="hfGenInfoID" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Name of factory
                                    </label>
                                    <asp:TextBox ID="txtnameoffactorypopup" runat="server" CssClass="form-control" placeholder="Name of factory"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Factory GST No
                                    </label>
                                    <asp:TextBox runat="server" ID="txtfactorygstnopopup" class="form-control" required="" TabIndex="2" ToolTip="Factory GST No"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Complete postal Address
                                    </label>
                                    <asp:TextBox runat="server" ID="txtcompletepostaladdresspopup" class="form-control" required="" TextMode="MultiLine" TabIndex="3" ToolTip="Complete postal Address"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Contact Official Name
                                    </label>
                                    <asp:TextBox runat="server" ID="txtcontactofficialnamepopup" class="form-control" required="" TabIndex="4" ToolTip="Contact Official Name"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Telephone No
                                    </label>
                                    <asp:TextBox runat="server" ID="txttelephonepopup" class="form-control" required="" onkeypress="return isNumberKey(event)" TabIndex="5" ToolTip="Mobile No (123456789) Only Number"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Fax No
                                    </label>
                                    <asp:TextBox runat="server" ID="txtfaxnopopup" class="form-control" required="" onkeypress="return isNumberKey(event)" TabIndex="5" ToolTip="Mobile No (123456789) Only Number"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Email Id
                                    </label>
                                    <asp:TextBox runat="server" ID="txtemailidpopup" class="form-control" required="" TabIndex="5" ToolTip="Email Id"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="btnupdate" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="btnupdate_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divareadetail" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div2">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Area Details</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Name of factory
                                    </label>
                                    <asp:TextBox ID="txtnameoffactoryareadetailpopup" runat="server" CssClass="form-control" placeholder="Name of factory"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        PRODUCTION AREA
                                    </label>
                                    <asp:TextBox runat="server" ID="txtproductionareapopup" class="form-control" required="" TabIndex="2" ToolTip="PRODUCTION AREA"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        INSPECTION AREA
                                    </label>
                                    <asp:TextBox runat="server" ID="txtinspectionarea" class="form-control" required="" TabIndex="3" ToolTip=" INSPECTION AREA"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        TOTAL COVERED AREA
                                    </label>
                                    <asp:TextBox runat="server" ID="txttotalcoverdareapopup" class="form-control" required="" TabIndex="4" ToolTip="TOTAL COVERED AREA"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Total Area
                                    </label>
                                    <asp:TextBox runat="server" ID="txttotalarea" class="form-control" required="" TabIndex="5" ToolTip="Total Area"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="btnareadetails" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="btnareadetails_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="allplantormachine" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div3">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">List of All Plant and Machines</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Description of Machine & Model Specs
                                    </label>
                                    <asp:TextBox ID="txtmachineormodalpopup" runat="server" CssClass="form-control" placeholder="Description of Machine & Model Specs"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Make
                                    </label>
                                    <asp:TextBox runat="server" ID="txtMakepopup" class="form-control" required="" TabIndex="2" ToolTip="Make"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Quantity
                                    </label>
                                    <asp:TextBox runat="server" ID="txtQuantitypopup" class="form-control" required="" TabIndex="3" ToolTip="Quantity"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Date of Purchase
                                    </label>
                                    <div class="input-append date" id="datePicker4" data-date="12-02-2012" data-date-format="dd-mm-yyyy">
                                        <span class="add-on"><i class="icon-th"></i></span>
                                        <asp:TextBox ID="txtdateofpurchasepopup" runat="server" CssClass="form-control datePicker" data-date-format="dd/mm/yyyy" required="" TabIndex="4" ToolTip="Date of Purchase"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Usage
                                    </label>
                                    <asp:TextBox runat="server" ID="txtUsagepopup" class="form-control" required="" TabIndex="5" ToolTip="Usage"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lballpalntormachine" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lballpalntormachine_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divemployee" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div4">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Employees Details</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        TOTAL Employees
                                    </label>
                                    <asp:TextBox ID="txttotalemployeepopup" runat="server" CssClass="form-control" placeholder="TOTAL Employees"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        ADMINISTRATIVE
                                    </label>
                                    <asp:TextBox runat="server" ID="txtADMINISTRATIVEpopup" class="form-control" required="" TabIndex="2" ToolTip="ADMINISTRATIVE"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        TECHNICAL
                                    </label>
                                    <asp:TextBox runat="server" ID="txtTECHNICALpopup" class="form-control" required="" TabIndex="3" ToolTip="TECHNICAL"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        NON TECHNICAL
                                    </label>
                                    <asp:TextBox runat="server" ID="txtnontechpopup" class="form-control" required="" TabIndex="4" ToolTip="NON TECHNICAL"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        QC/INSPECTION
                                    </label>
                                    <asp:TextBox runat="server" ID="Textxtqcinspopup" class="form-control" required="" TabIndex="5" ToolTip="QC/INSPECTION"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        SKILLED LABOUR
                                    </label>
                                    <asp:TextBox runat="server" ID="txtskilledlabour" class="form-control" required="" TabIndex="5" ToolTip="SKILLED LABOUR"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        UNSKILLED LABOUR
                                    </label>
                                    <asp:TextBox runat="server" ID="txtUNSKILLEDLABOURpopup" class="form-control" required="" TabIndex="5" ToolTip="UNSKILLED LABOUR"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lbemployee" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lbemployee_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divqcfm" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div5">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">List of Test Facilities</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField4" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Type of GAUGE / Test Equipment
                                    </label>
                                    <asp:TextBox ID="txttestequippopup" runat="server" CssClass="form-control" placeholder="Type of GAUGE / Test Equipment"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Make
                                    </label>
                                    <asp:TextBox runat="server" ID="txtmakefacpopup" class="form-control" required="" TabIndex="2" ToolTip="Make"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Least Count
                                    </label>
                                    <asp:TextBox runat="server" ID="txtlcountpopup" class="form-control" required="" TabIndex="3" ToolTip="Least Count"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Range of MEASURMENT
                                    </label>
                                    <asp:TextBox runat="server" ID="txtrngmeasurpopup" class="form-control" required="" TabIndex="4" ToolTip="Range of MEASURMENT"></asp:TextBox>
                                </div>
                                  <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Unit of MEASURMENT
                                    </label>
                                    <asp:TextBox runat="server" ID="txtunitofmeasuredit" class="form-control" required="" TabIndex="4" ToolTip="Unit of MEASURMENT"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        CERTIFICATION YEAR
                                    </label>
                                    <div class="input-append date" id="datePicker5" data-date="12-02-2012" data-date-format="dd-mm-yyyy">
                                        <span class="add-on"><i class="icon-th"></i></span>
                                        <asp:TextBox ID="txtcertiyearpopup" runat="server" CssClass="form-control datePicker" data-date-format="dd/mm/yyyy" required="" TabIndex="5" ToolTip="CERTIFICATION YEAR"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Year of purchase
                                    </label>
                                    <div class="input-append date" id="datePicker6" data-date="12-02-2012" data-date-format="dd-mm-yyyy">
                                        <span class="add-on"><i class="icon-th"></i></span>
                                        <asp:TextBox ID="txtyrofpurpopup" runat="server" CssClass="form-control datePicker" data-date-format="dd/mm/yyyy" required="" TabIndex="5" ToolTip="Year of purchase"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lbQCF" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lbQCF_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divautdisdeal" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div6">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Authorised distributor/dealer</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField5" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Distributor Name
                                    </label>
                                    <asp:TextBox ID="txtdisname" runat="server" CssClass="form-control" placeholder="Distributor Name"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Street Addresss
                                    </label>
                                    <asp:TextBox ID="txtstreetaddpopup" runat="server" CssClass="form-control" placeholder="Street Addresss"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        State
                                    </label>
                                    <asp:TextBox runat="server" ID="txtstatepopup" class="form-control" required="" TabIndex="2" ToolTip="State"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        PinCode
                                    </label>
                                    <asp:TextBox runat="server" ID="txtPinCodepopup" class="form-control" required="" TabIndex="3" ToolTip="PinCode"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Phone
                                    </label>
                                    <asp:TextBox runat="server" ID="txtphonepopup" class="form-control" required="" TabIndex="4" ToolTip="Phone"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Fax
                                    </label>
                                    <asp:TextBox runat="server" ID="txtFaxpopup" class="form-control" required="" TabIndex="5" ToolTip="Fax"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Email
                                    </label>
                                    <asp:TextBox runat="server" ID="txtemailpopup" class="form-control" required="" TabIndex="5" ToolTip="Email"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lbldistri" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lbldistri_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divof" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div7">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Details of Outsourcing Facilites</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField6" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Main Equipment
                                    </label>
                                    <asp:TextBox ID="txtmainequippopup" runat="server" CssClass="form-control" placeholder="Main Equipment"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Test Equipments
                                    </label>
                                    <asp:TextBox runat="server" ID="txttestpopup" class="form-control" required="" TabIndex="2" ToolTip="Test Equipments"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Process/facility
                                    </label>
                                    <asp:TextBox runat="server" ID="txtprocfacli" class="form-control" required="" TabIndex="3" ToolTip="Process/facility"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Name & Address of Sub Contractor
                                    </label>
                                    <asp:TextBox runat="server" ID="txtnameaddpopup" class="form-control" required="" TabIndex="4" ToolTip="Name & Address of Sub Contractor"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lboutfac" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lboutfac_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divjvfaci" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div9">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">List of Joint-Venture Facility</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField7" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Name
                                    </label>
                                    <asp:TextBox ID="txtnamejvpop" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Is Joint Venture Nature
                                    </label>
                                    <asp:DropDownList runat="server" ID="ddljointvennature" class="form-control" required="" TabIndex="2">
                                        <asp:ListItem>Indian</asp:ListItem>
                                        <asp:ListItem>Foreign</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Complete Address
                                    </label>
                                    <asp:TextBox runat="server" ID="txtcomaddpopup" class="form-control" required="" TabIndex="3" ToolTip="Complete Address"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Contact Official Name
                                    </label>
                                    <asp:TextBox runat="server" ID="txtconoffnamepopup" class="form-control" required="" TabIndex="4" ToolTip="Contact Official Name"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Telephone No
                                    </label>
                                    <asp:TextBox runat="server" ID="txttelenopopup" class="form-control" required="" TabIndex="5" ToolTip="Telephone No"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Fax No
                                    </label>
                                    <asp:TextBox runat="server" ID="txtfaxjvpopup" class="form-control" required="" TabIndex="5" ToolTip="Fax No"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Email Id
                                    </label>
                                    <asp:TextBox runat="server" ID="txtemailpopupjv" class="form-control" required="" TabIndex="5" ToolTip="Email Id"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lbkjoinven" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lbkjoinven_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divCertificate1" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 500px;">
            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div8">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Certificate</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField8" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Select Certificate to Upload
                                    </label>
                                    <div class="clearfix"></div>
                                    <asp:RadioButtonList runat="server" ID="rbcer" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem>Factory Licence /Municipal Shop's & Establishment</asp:ListItem>
                                        <asp:ListItem>Registration Certificate from Labor Commissioner</asp:ListItem>
                                        <asp:ListItem>VAT Registration Certificate</asp:ListItem>
                                        <asp:ListItem>Excise Registration Certificate</asp:ListItem>
                                        <asp:ListItem>Any other Certificate</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Upload Certificate file
                                    </label>
                                    <asp:FileUpload ID="fucer" runat="server" CssClass="form-control" />
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lbcertificateadd" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lbcertificateadd_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="divcertificate2" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 500px;">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div10">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Certificate</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField9" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Select Certificate to Upload
                                    </label>
                                    <asp:RadioButtonList runat="server" ID="rb1" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem>IMS</asp:ListItem>
                                        <asp:ListItem>EnMS</asp:ListItem>
                                        <asp:ListItem>QMS</asp:ListItem>
                                        <asp:ListItem>Any other Certificate</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Upload Certificate file
                                    </label>
                                    <asp:FileUpload ID="fucertificate1" runat="server" CssClass="form-control" />
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="lbcertificate1" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lbcertificate1_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%----Java Script Of ConfigurationAll Modal Popup--%>
    <script type="text/javascript">
        function showPopup0() {
            $('#changePass').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#divareadetail').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup2() {
            $('#allplantormachine').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup3() {
            $('#divemployee').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup4() {
            $('#divqcfm').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup5() {
            $('#divautdisdeal').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup6() {
            $('#divof').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup7() {
            $('#divjvfaci').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup8() {
            $('#divCertificate1').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup9() {
            $('#divcertificate2').modal('show', function () {
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
