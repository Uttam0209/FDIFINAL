<%@ Page Language="C#" AutoEventWireup="true" CodeFile="S_Story2update.aspx.cs" Inherits="User_S_Story2" MasterPageFile="~/User/MasterPage.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

    <style>
        #ContentPlaceHolder1_ddlcompany {
            display: flex;
            padding-left: 15px;
        }

            #ContentPlaceHolder1_ddlcompany span .box1 {
                padding: 12px 12px;
                margin-left: 15px;
            }

        .box2 {
            box-shadow: 0 0 5px;
        }

        label {
            padding-left: 3px !important;
            margin-right: 10px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divHeadPage" runat="server">
            </div>
            <div class="row d-flex justify-content-center">
                <div class="col-lg-11 d-flex justify-content-end py-3">
                    <asp:HiddenField runat="server" ID="chktargetclick" />
                    <asp:HiddenField runat="server" ID="chkindigclick" />
                    <asp:HiddenField runat="server" ID="chkcategclick" />
                    <asp:HiddenField runat="server" ID="chknameclick" />
                    <asp:HiddenField runat="server" ID="chkaddressclick" />
                    <asp:HiddenField runat="server" ID="mRefNo" />
                </div>
            </div>
            <div class="mt-1"></div>
            <div class="row d-flex justify-content-center mb-3">
                <div class="col-lg-11 mx-2">
                    <h4 style="text-align: center;">Success Story</h4>
                    <div class="clearfix"></div>
                    <p><span class="alert" style="color: red;">Note: For more information regarding functionalities please move cursor at respective functions</span></p>
                    <div class="clearfix"></div>
                    <div style="padding-right: 15px; padding-left: 15px;">
                        <div class="row d-flex" id="successreport">
                            <div class="col-2" style="background: #5843e0; color: white; font-weight: 500; text-align: center; padding: 10px 0">
                                <asp:Label ID="Label7" runat="server" data-toggle="tooltip" ToolTip="shown columns" Text="Column"></asp:Label>
                                <hr />
                                <asp:Label ID="Label8" runat="server" data-toggle="tooltip" ToolTip="its shown pending value" Text="Pending"></asp:Label>
                            </div>
                            <div class="col-2" style="background: rgb(0, 102, 153); color: white; font-weight: 500; text-align: center; padding: 10px 0">
                                <asp:Label ID="lblstart" runat="server" data-toggle="tooltip" ToolTip="Starting year means year of indigenization year" Text="Starting Year"></asp:Label>
                                <hr />
                                <asp:LinkButton runat="server" ID="lblTarget" OnClick="lblTarget_Click" ToolTip="Starting year means year of indigenization year" ForeColor="White"></asp:LinkButton>
                            </div>
                            <div class="col-2" style="background: #999966; color: white; font-weight: 500; text-align: center; padding: 10px 0">
                                <asp:Label ID="Label3" runat="server" data-toggle="tooltip" ToolTip="Indigenization year means which year product was indigenized" Text="Indegenization Year"></asp:Label>
                                <hr />
                                <asp:LinkButton runat="server" ID="lblIndiG" OnClick="lblIndiG_Click" ToolTip="Indigenization year means which year product was indigenized" ForeColor="White"></asp:LinkButton>
                            </div>
                            <div class="col-2" style="background: #00CC66; color: white; font-weight: 500; text-align: center; padding: 10px 0">
                                <asp:Label ID="Label4" runat="server" data-toggle="tooltip" ToolTip="Display Sub Categories of Make In India" Text="Make In India Category"></asp:Label>
                                <hr />
                                <asp:LinkButton runat="server" ID="lblcat" OnClick="lblcat_Click" ToolTip="Display Sub Categories of Make In India" ForeColor="White"></asp:LinkButton>
                            </div>
                            <div class="col-2" style="background: #008036; color: white; font-weight: 500; text-align: center; padding: 10px 0">
                                <asp:Label ID="Label5" runat="server" data-toggle="tooltip" ToolTip="it show the by which company product was manufactured" Text="Manufacturing Name"></asp:Label>
                                <hr />
                                <asp:LinkButton runat="server" ID="lblNM" OnClick="lblNM_Click" ToolTip="it show the by which company product was manufactured" ForeColor="White"></asp:LinkButton>
                            </div>
                            <div class="col-2" style="background: #004D28; color: white; font-weight: 500; text-align: center; padding: 10px 0">
                                <asp:Label ID="Label6" runat="server" data-toggle="tooltip" ToolTip="It give information about where is the address of that company which is manufactured that product." Text=" Manufacturing Address"></asp:Label>
                                <hr />
                                <asp:LinkButton runat="server" ID="lbladd" OnClick="lbladd_Click" ToolTip="It give information about where is the address of that company which is manufactured that product." ForeColor="White"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="mt-1"></div>
            <div class="row d-flex justify-content-center mb-3">
                <div class="col-lg-11 mx-2">
                    <asp:HiddenField runat="server" ID="hidType" />
                    <asp:HiddenField runat="server" ID="hfcomprefno" />
                    <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
                    <asp:DataList ID="ddlcompany" runat="server" RepeatColumns="8" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemCommand="ddlcompany_ItemCommand" onitem>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lblc" CssClass="btnPageSize btn-default box1" ForeColor="White" CommandArgument='<%#Eval("CompanyRefno") %>' ToolTip="DPSU user clicking on the company name, the data company wise displays.." CommandName="comp"><%#Eval("CompanyName") %></asp:LinkButton>

                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div class="mt-1"></div>
            <div class="row d-flex justify-content-center mb-3">
                <div class="col-lg-11">
                    <div class="row">
                        <div class="col-sm-4">
                            <label>Select Year</label>
                            <div class="clearfix"></div>
                            <asp:RadioButtonList runat="server" ID="RdoYear" OnSelectedIndexChanged="RdoYear_SelectedIndexChanged" data-toggle="tooltip"
                                ToolTip="Select the year for year wise for specific details" AutoPostBack="true" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="1" Text="Previous Year"></asp:ListItem>
                                <asp:ListItem Value="2020-21" Text="2020-21"></asp:ListItem>
                                <asp:ListItem Value="2021-22" Text="2021-22"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-sm-4">
                            <label>Select Month</label>
                            <asp:DropDownList runat="server" ID="DDLMonth" CssClass="form-control form-cascade-control" data-toggle="tooltip" ToolTip="Select the month for  month wise for specific details" AutoPostBack="true" OnSelectedIndexChanged="DDLMonth_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-4" style="margin-top: 28px;">
                            <div class="row">
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtsearch" runat="server" ToolTip="search tab with all criteria using text based search."
                                        onblur="SaveData('txtsearch')" CssClass="form-control appended-form-control" Placeholder="Search (type min three character)"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button runat="server" ID="btnsearch" class="btn btn-primary" Text="Search" ToolTip="Click here for search" OnClick="btnsearch_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="mt-1"></div>
            <div class="row d-flex justify-content-center">
                <div class="col-lg-11">
                    <div class="row">
                        <div class="col-sm-2">
                            <span>PageSize:</span>
                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control btn-block" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" data-toggle="tooltip" ToolTip="DPSU User will enter that number as many of the records they want to pull together.">
                                <asp:ListItem Text="100" Value="100" />
                                <asp:ListItem Text="500" Value="500" />
                                <asp:ListItem Text="1000" Value="1000" />
                                <asp:ListItem Text="2500" Value="2500" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1 mt-4">
                            <asp:Button ID="btnpdf" runat="server" Text="Print" ToolTip="Click on print button to Print the details of Product " class="btn btn-primary" OnClientClick="doPrint();" />
                        </div>
                        <div class="col-sm-1 mt-4">
                            <asp:LinkButton runat="server" ID="btnExcel" class="btn btn-primary" ToolTip="To download the details of Product in excel sheet press the excel button"
                                OnClick="btnExcel_Click">Excel</asp:LinkButton>
                        </div>
                    </div>
                    <div class="clearfix mt-2"></div>
                    <div class="table table-responsive">
                        <asp:GridView runat="server" ID="gveoi" Class="table table-hover table-bordered " AutoGenerateColumns="False"
                            OnRowCreated="gveoi_RowCreated" OnRowDataBound="gveoi_RowDataBound" OnRowCommand="gveoi_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="50px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Header" data-toggle="tooltip" ToolTip="Display Sr.No.." runat="server" Text="Sr.No"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("row_no") %>
                                        <asp:HiddenField runat="server" ID="IndeginizedDate" Value='<%#Eval("IndeginizedDate","{0:dd-MMM-yyyy}") %>' />
                                        <asp:HiddenField runat="server" ID="IndeginizedMaxValue" Value='<%#Eval("IndeginizedMaxValue") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Header1" data-toggle="tooltip" ToolTip="Display Company Name." runat="server" Text="Company"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompany" runat="server" Data-toggle="tooltip" ToolTip="Display Company name.." Text='<%# Eval("CompanyName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Company" DataField="CompanyName" NullDisplayText="NA" HeaderStyle-Width="70px" />--%>
                                <asp:TemplateField HeaderText="Division/Unit" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdivunit" data-toggle="tooltip" ToolTip="Display Division/Unit Name." runat="server" Text="Division/Unit"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldivsion" runat="server" data-toggle="tooltip" ToolTip="Display Division Name" Text='<%# Eval("FactoryName") %>'>
                                        </asp:Label>
                                        <asp:Label ID="lblunit" runat="server" data-toggle="tooltip" ToolTip="Display Unit Name" Text='<%# Eval("UnitName") %>'>
                                        </asp:Label>
                                        <asp:HiddenField ID="hfisindegized" runat="server" Value='<%# Eval("IsIndeginized") %>' />
                                        <asp:HiddenField ID="hfindproc" runat="server" Value='<%# Eval("IndProcess") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="70px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerditem" data-toggle="tooltip" ToolTip="Display Product Name" runat="server" Text="Item Name"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemName" runat="server" data-toggle="tooltip" ToolTip="Display Product Name" Text='<%# Eval("Itemname") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Item Name"  DataField="Itemname" NullDisplayText="NA" HeaderStyle-Width="300px"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Item Code" HeaderStyle-Width="70px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerditemcode" data-toggle="tooltip" ToolTip="Display Product ID" runat="server" Text="Item Code"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemcode" runat="server" data-toggle="tooltip" ToolTip="Display Product ID" Text='<%# Eval("Itemcode") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="NSC Code" DataField="NSCCode" NullDisplayText="NA" Visible="false" HeaderStyle-Width="60px" />
                                <asp:TemplateField HeaderText="Starting Year" Visible="false" HeaderStyle-Width="60px">

                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdyear" data-toggle="tooltip" ToolTip="Display Indiginized target year" runat="server" Text="Starting Year"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltargtyr" runat="server" data-toggle="tooltip" ToolTip="This is the Indiginized target year" Text='<%# Eval("IndTargetYear") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make In India Category" HeaderStyle-Width="90px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdmake" data-toggle="tooltip" ToolTip="Display Sub Categories of Make In India" runat="server" Text="Make In India Category"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfproc" Value='<%# Eval("PurposeofProcurement") %>' />
                                        <asp:Label ID="lblindiacategory" runat="server" data-toggle="tooltip" ToolTip="Display Sub Categories of Make In India" Text='<%# Eval("MakeInIndiaCategory") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Indegenization Year" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="HeaderdIndegenization" data-toggle="tooltip" ToolTip="Display year of Indigenisation " runat="server" Text="Indegenization Year"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblindegyr" runat="server" data-toggle="tooltip" ToolTip="Display year of Indigenisation " Text='<%# Eval("IndegenizationYear") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reporting Month" HeaderStyle-Width="100px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdmonth" data-toggle="tooltip" ToolTip="Display the month of reporting (Indigenisation details) " runat="server" Text="Reporting Month"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblmonth" runat="server" data-toggle="tooltip" ToolTip="Display the month of reporting (Indigenisation details)" Text='<%# Eval("ReportingMonth") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reporting Month2" HeaderStyle-Width="100px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmonth2" runat="server" Text='<%# Eval("MonthSearch") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approx Annual Negated Value in Rs (Lakh)" HeaderStyle-Width="100px">
                                    <HeaderTemplate>
                                        <asp:Label ID="HeaderdApprox" data-toggle="tooltip" ToolTip="Approx Annual Negated Value in Rs (Lakh)" runat="server" Text="Approx Annual Negated Value in Rs (Lakh)"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblapprox" runat="server" data-toggle="tooltip" ToolTip="Approx Annual Negated Value in Rs (Lakh)" Text='<%# Eval("IndeginizedMaxValue") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Atmnirbhar Data" HeaderStyle-Width="100px">
                                    <HeaderTemplate>
                                        <asp:Label ID="HeaderdAtmnirbhar" data-toggle="tooltip" ToolTip="Display the Atmnirbhar data month  " runat="server" Text="Atmnirbhar Data"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblmonthh" runat="server" data-toggle="tooltip" ToolTip="Display the month of reporting (Indigenisation details)" Text='<%# Eval("IndeginizedDate","{0:MMM-yyyy}") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IndTargetYear" HeaderStyle-Width="100px" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="lblIndtary" runat="server" Text='<%# Eval("IndTargetYear") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annual Value Negated" HeaderStyle-Width="100px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdestimate" data-toggle="tooltip" ToolTip="Maximum Value taken of all the import values entered year wise" runat="server" Text="Annual Value Negated"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblestimate" runat="server" data-toggle="tooltip" ToolTip="Maximum Value taken of all the import values entered year wise" Text='<%# Eval("MaximumImportValue") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              

                                <asp:TemplateField HeaderText="Manufacturing Name" Visible="true" HeaderStyle-Width="100px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdmanufname" data-toggle="tooltip" ToolTip="Name of the Manufacturing  Firm" runat="server" Text="Manufacturing Name"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblmanufname" runat="server" data-toggle="tooltip" ToolTip="Name of the Manufacturing  Firm" Text='<%# Eval("ManufactureName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manufacturing Address" Visible="true" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdmanufaddress" data-toggle="tooltip" ToolTip="Address of Manufacturing Firm" runat="server" Text="Manufacturing Address"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblmanufaddress" runat="server" data-toggle="tooltip" ToolTip="Address of Manufacturing Firm" Text='<%# Eval("ManufactureAddress") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdaction" data-toggle="tooltip" ToolTip="Edit details of Indigenisation" runat="server" Text="Action"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkaction" runat="server" Text="Edit" data-toggle="tooltip" ToolTip="Edit details of Indigenisation " CommandArgument='<%# Eval("Itemcode") %>' CommandName="ed"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="mt-1"></div>
            <div class="row d-flex justify-content-center">
                <div class="col-lg-11 " id="divcontentproduct" runat="server">
                    <nav class="d-flex justify-content-between" aria-label="Page navigation">
                        <div class="col-6 d-flex justify-content-start">
                            <ul class="pagination">

                                <li class="page-item" style="background: #6915cf!important; color: white!important; padding: 8px!important; border-radius: 5px;">
                                    <span style="text-align: center;">Showing
                                       
                                            <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                                        products of
                                       
                                            <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                                        products  
                                    </span>
                                </li>
                            </ul>
                        </div>
                        <div class="col-6 d-flex justify-content-end">
                            <ul class="pagination">
                                <li class="page-item" style="background: #6915cf!important; color: white!important; padding: 8px!important; border-radius: 5px;">
                                    <asp:LinkButton ID="lnkbtnPgPrevious" Style="background: #6915cf!important; color: white!important; padding: 8px!important; border-radius: 5px;" runat="server" class="page-link" OnClick="lnkbtnPgPrevious_Click"><i class="fa fa-chevron-left mr-2"></i> Prev</asp:LinkButton>
                                </li>
                                <li class="page-item" style="background: #6915cf!important; color: white!important; padding: 8px!important; border-radius: 5px;">
                                    <asp:Label ID="lblpaging" runat="server"></asp:Label>
                                </li>
                                <li class="page-item" style="background: #6915cf!important; color: white!important; padding: 5px!important; border-radius: 5px;">
                                    <asp:LinkButton ID="lnkbtnPgNext" Style="background: #6915cf!important; color: white!important; padding: 8px!important; border-radius: 5px;" runat="server" class="page-link" OnClick="lnkbtnPgNext_Click"> Next<i class="fa fa-chevron-right ml-2"></i></asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </nav>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="up">
        <ProgressTemplate>
            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p style="margin-left: 200px; padding-bottom: 10px;">
                        <b>Processing...</b>
                    </p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="modal-quick-view modal fade" id="divupdate" role="dialog">
        <div class="modal-dialog  modal-xl" style="max-width: 875px!important; z-index: 9999999999;">
            <asp:UpdatePanel ID="upn" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="modal-content">
                        <div runat="server" id="Div15">
                            <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                                <h5 style="color: white;">Update Your Item</h5>
                                <span id="success" runat="server"></span>
                            </div>
                            <div class="modal-body">
                                <asp:HiddenField ID="hfprorefno" runat="server" />
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Item Code:-</label>
                                            <asp:Label ID="lblprorefcode" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Starting Year:-</label>
                                            <div class="clearfix"></div>
                                            <asp:RadioButtonList ID="chkinditargetyear" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="NIL">&nbsp;NIL</asp:ListItem>
                                                <asp:ListItem Value="2020-21">&nbsp;2020-21</asp:ListItem>
                                                <asp:ListItem Value="2021-22">&nbsp;2021-22</asp:ListItem>
                                                <asp:ListItem Value="2022-23">&nbsp;2022-23</asp:ListItem>
                                                <asp:ListItem Value="2023-24">&nbsp;2023-24</asp:ListItem>
                                                <asp:ListItem Value="2024-25">&nbsp;2024-25</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group">
                                            <label>Indegenization Process Started:-</label>
                                            <div class="clearfix"></div>
                                            <asp:RadioButtonList ID="rdblindegprocess" runat="server" AutoPostBack="true" RepeatColumns="2"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rdblindegprocess_SelectedIndexChanged">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No" style="margin-left: 10px;">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group table">
                                            <label>Make In India Category:-</label>
                                            <div class="clearfix"></div>
                                            <asp:RadioButtonList ID="rbIgCategory" runat="server" class="radio radio-inline" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group" runat="server" id="itemindeg">
                                            <label>Item Indegenized:-</label>
                                            <div class="clearfix"></div>
                                            <asp:RadioButtonList ID="rdblisindig" runat="server" AutoPostBack="true" RepeatColumns="2" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow" OnSelectedIndexChanged="rdblisindig_SelectedIndexChanged">
                                                <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                                                <asp:ListItem Value="N" style="margin-left: 10px;">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                       
                                        <div class="form-group">
                                            <label>Import Negated value:-</label>
                                            <asp:label ID="Txtnegated" runat="server" Placeholder="Only Number Allowed" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:label>
                                        </div>                                    
                                         <div class="form-group">
                                            <label>Atmnirbhar Data:-</label>
                                            <asp:TextBox ID="txtinddate" runat="server" CssClass="form-control my_date_picker2"
                                                autocomplete="off" placeholder="Start date (format:- 01-jan-1900)"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Approx Annual Negated Value in Rs(Lakh)-</label>
                                            <asp:TextBox ID="TxtApproxvalue" runat="server" Placeholder="Only Number Allowed" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group" runat="server" id="manuf">
                                            <label>Manufacturing Name:-</label>
                                            <asp:TextBox ID="txtmanufacturngname" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group" runat="server" id="manufadd">
                                            <label>Manufacturing Address:-</label>
                                            <asp:TextBox ID="txtmanufacturngadress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group" runat="server" id="yearindeg">
                                            <label>Year of Indegenization:- </label>
                                            <asp:DropDownList runat="server" ID="ddlyearofindiginization" TabIndex="22" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 mt-2 ">
                                        <div class="pull-right">
                                            <asp:LinkButton ID="btnupdate" runat="server" CssClass="btn btn-primary " OnClick="btnupdate_Click">
                                            <i class="fa fa-edit"></i>&nbsp;Update</asp:LinkButton>
                                            <asp:Button ID="btnclose" runat="server" Text="Close" CssClass="btn btn-default" data-dismiss="modal" />
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
    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <script src="User/Uassets/js/bootstrap.bundle.min.js"></script>
    <!-- pace -->
    <script type="text/javascript">
        function showPopup2() {
            $('#divupdate').modal('show');
        }
    </script>
    <script src="User/Uassets/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        //On Page Load
        $(function () {
            $('#ContentPlaceHolder1_gveoi').dataTable({
                "bPaginate": false,
                "bFilter": false,
                "bInfo": false,
                destroy: true
            });
        });
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#ContentPlaceHolder1_gveoi').dataTable({
                        "bPaginate": false,
                        "bFilter": false,
                        "bInfo": false,
                        destroy: true
                    });
                }
            });
        };
    </script>
    <script src="User/Uassets/js/jquery-ui.min.js"></script>



    <script type="text/javascript">
        $(function () {
            SetAutoComplete();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetAutoComplete();
                }
            });
        };
        function SetAutoComplete() {
            $("[id$=txtsearch]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'User/S_Story2.aspx/GetSearchKeyword',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    val: item
                                };
                            }))
                        }
                    });
                },
                minLength: 1
            });
        }
    </script>
    <script type="text/javascript">  
        function PrintPage() {
            var divContents = document.getElementById("divhome").innerHTML;
            var printWindow = window.open('', '', 'height=1000,width=1000');
            printWindow.document.write('<html><head><title>Success Story Data</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);

            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>
    <script type="text/javascript">
        function doPrint() {
            var prtContent = document.getElementById('<%= gveoi.ClientID %>');
            prtContent.border = 1; //set no border here
            var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
            WinPrint.document.write(prtContent.outerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
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

