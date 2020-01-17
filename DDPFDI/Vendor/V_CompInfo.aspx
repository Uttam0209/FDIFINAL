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
                <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
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
                                <asp:GridView ID="gvmanufacilityedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
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
                                                <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewmfe"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="fa fa-edit" CommandArgument='<%#Eval("MasterId") %>' CommandName="updatenewmfe"></asp:LinkButton>
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
                                <asp:GridView ID="gvareadetailedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Area_Factory_Name" HeaderText="Name of Factory" />
                                        <asp:BoundField DataField="PRODUCTION_AREA" HeaderText="PRODUCTION AREA" />
                                        <asp:BoundField DataField="INSPECTION_AREA" HeaderText="INSPECTION AREA" />
                                        <asp:BoundField DataField="TOTAL_COVERED_AREA" HeaderText="TOTAL COVERED AREA" />
                                        <asp:BoundField DataField="Total_Area" HeaderText="Total Area" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbladdnewareadetailedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewad"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewareadetailedit" runat="server" Class="fa fa-edit" CommandArgument='<%#Eval("MasterId") %>' CommandName="updatenewad"></asp:LinkButton>
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
                                <asp:GridView ID="gvplantandmachinesedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
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
                                                <asp:LinkButton ID="lbladdnewplantandmachinesedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewplantandmachinesedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewplantandmachinesedit" runat="server" Class="fa fa-edit" CommandArgument='<%#Eval("MasterId") %>' CommandName="updatenewplantandmachinesedit"></asp:LinkButton>
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
                                <asp:GridView ID="gvempCompInfoedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
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
                                                <asp:LinkButton ID="lbladdnewempCompInfoedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewempCompInfoedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewempCompInfoedit" runat="server" Class="fa fa-edit" CommandArgument='<%#Eval("MasterId") %>' CommandName="updatenewempCompInfoedit"></asp:LinkButton>
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
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
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
                                                <asp:Image runat="server" ID="imgeditcerti" ImageUrl='<%#Eval("Path") %>' Height="80px" Width="80px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbladdnewcertificateedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewecertificateedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewcertificateedit" runat="server" Class="fa fa-edit" CommandArgument='<%#Eval("MasterId") %>' CommandName="updatenewcertificateedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbldeletenewcertificateedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deletenewcertificateedit"></asp:LinkButton>
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
                                <asp:GridView ID="gvtestfacilitiesedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
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
                                        <asp:BoundField DataField="CERTIFICATION_YEAR" HeaderText="CERTIFICATION YEAR" />
                                        <asp:BoundField DataField="Year_of_purchase" HeaderText="Year of purchase" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbladdnewtestfacilitiesedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewtestfacilitiesedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewtestfacilitiesedit" runat="server" Class="fa fa-edit" CommandArgument='<%#Eval("MasterId") %>' CommandName="updatenewtestfacilitiesedit"></asp:LinkButton>
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
                                        <asp:TextBox ID="txtdate" runat="server" Type="date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix mt10"></div>
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
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
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
                                                <asp:Image runat="server" ID="imgeditqcerti" ImageUrl='<%#Eval("Path") %>' Height="80px" Width="80px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbladdnewchkqualitycertificateedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewechkqualitycertificateedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewchkqualitycertificateedit" runat="server" Class="fa fa-edit" CommandArgument='<%#Eval("MasterId") %>' CommandName="updatenewchkqualitycertificateedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbldeletenewchkqualitycertificateedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("MasterId") %>' CommandName="deletenewchkqualitycertificateedit"></asp:LinkButton>
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
                                                        State / Province
                                                    </p>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtpincode" runat="server" CssClass="form-control"></asp:TextBox>
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
                                            <asp:TextBox ID="txtcontactno" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Fax Number
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtfaxno" runat="server" CssClass="form-control"></asp:TextBox>

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
                                    <asp:GridView ID="gvauthdealaddressedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DistributorStreetAddress" HeaderText="StreetAddress" />
                                            <asp:BoundField DataField="DistributorState" HeaderText="State" />
                                            <asp:BoundField DataField="DistributorPincode" HeaderText="PinCode" />
                                            <asp:BoundField DataField="DistributorPhone" HeaderText="Phone" />
                                            <asp:BoundField DataField="DistributorFax" HeaderText="Fax" />
                                            <asp:BoundField DataField="DistributorEmail" HeaderText="Email" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbladdnewauthdealaddressedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewauthdealaddressedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lblupdatenewauthdealaddressedit" runat="server" Class="fa fa-edit" CommandArgument='<%#Eval("MasterId") %>' CommandName="updatenewauthdealaddressedit"></asp:LinkButton>
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
                                    <asp:GridView ID="gvoutsourcefacilityedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
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
                                                    <asp:LinkButton ID="lbladdnewoutsourcefacilityedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewoutsourcefacilityedit"></asp:LinkButton>
                                                    <asp:LinkButton ID="lblupdatenewoutsourcefacilityedit" runat="server" Class="fa fa-edit" CommandArgument='<%#Eval("MasterId") %>' CommandName="updatenewoutsourcefacilityedit"></asp:LinkButton>
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
                                <asp:GridView ID="gvjointventureedit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">
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
                                                <asp:LinkButton ID="lbladdnewjointventureedit" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("MasterId") %>' CommandName="addnewjointventureedit"></asp:LinkButton>
                                                <asp:LinkButton ID="lblupdatenewjointventureedit" runat="server" Class="fa fa-edit" CommandArgument='<%#Eval("MasterId") %>' CommandName="updatenewjointventureedit"></asp:LinkButton>
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
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right mr10" OnClick="btnsubmit_Click" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-primary pull-right mr10" OnClick="btncancel_Click" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnsubmit" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
