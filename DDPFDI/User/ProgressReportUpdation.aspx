<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProgressReportUpdation.aspx.cs" MasterPageFile="~/User/MasterPage.master" Inherits="User_ProgressReportUpdation" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="head">
    <link rel="stylesheet" type="text/css" href="User/Uassets/css/jquery.dataTables.min.css" />
    <link href='https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/ui-lightness/jquery-ui.css' rel='stylesheet' />
    <style>
        #ContentPlaceHolder1_grdinttotal tr th:nth-child(2) {
            width: 400px !important;
        }

        #ContentPlaceHolder1_grdinttotal tr th:nth-child(6) {
            width: 150px !important;
        }

        .modal-footer {
            border-top: 0px solid #e3e9ef;
        }

        #ContentPlaceHolder1_gvProgress th:nth-child(10) table td {
            border-bottom: 0px solid #111;
        }

        #ContentPlaceHolder1_divcontentproduct {
            padding-right: 0px !important;
            padding-left: 0px !important;
        }

            #ContentPlaceHolder1_divcontentproduct div {
                padding-right: 0px !important;
                padding-left: 0px !important;
            }

        #ContentPlaceHolder1_gvProgress th {
            color: white;
            text-align: center;
            padding: 2px;
        }

        #ContentPlaceHolder1_gvProgress td {
            text-align: center;
            padding: 2px;
        }

        .pagination li {
            color: white;
            padding: 10px 15px;
            border-radius: 5px;
        }

            .pagination li a {
                color: white;
                padding: 0 !important;
                border: none;
            }

                .pagination li a:hover {
                    background: #6915cf !important;
                    border: none !important;
                    color: white;
                }

        #ContentPlaceHolder1_th2 span {
            color: white !important;
        }

        #divbox {
            font-weight: 600;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ID="content2" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row d-flex justify-content-center">
                <div class="col-lg-11">
                    <div class="row d-flex">
                        <div class="col-12 d-flex justify-content-end py-3">
                            <asp:HiddenField runat="server" ID="CheckProductsClick" />
                            <asp:HiddenField runat="server" ID="CheckIntrestClick" />
                            <asp:HiddenField runat="server" ID="CheckEOIClick" />
                            <asp:HiddenField runat="server" ID="CheckSupplyClick" />
                            <asp:HiddenField runat="server" ID="CheckSuccessClick" />
                            <asp:HiddenField runat="server" ID="CheckInHouse" />
                            <asp:HiddenField runat="server" ID="CheckTarget" />
                            <asp:HiddenField runat="server" ID="CheckNoOfint" />
                            <asp:HiddenField runat="server" ID="CheckpendInt" />
                            <asp:HiddenField runat="server" ID="Checkpendeoi" />
                            <asp:HiddenField runat="server" ID="CheckPendSupp" />
                            <asp:HiddenField runat="server" ID="CheckPenIndig" />
                            <asp:HiddenField runat="server" ID="Checknilaction" />
                            <asp:HiddenField runat="server" ID="Checknotneeded" />
                            <asp:HiddenField runat="server" ID="Checkunderprocess" />
                            <asp:HiddenField runat="server" ID="Checkvendsuit" />
                            <asp:HiddenField runat="server" ID="Checkeoi" />
                            <asp:HiddenField runat="server" ID="Checkotheraction" />
                        </div>
                        <div class="col-10">
                            <ul class="nav nav-tabs" style="border-bottom: none;">
                                <li class="nav-item my-0 py-0" style="width: 130px; background: rgb(0, 102, 153); color: white; font-weight: bold; text-align: center;">Products
                                    <div class="dropdown-divider"></div>
                                    <asp:LinkButton runat="server" ID="lblProducts" ForeColor="White" OnClick="lblProducts_Click"></asp:LinkButton>
                                </li>
                                <i class="fa fa-arrow-circle-right" style="font-size: 25px; margin-top: 15px; color: darkgray;" data-toggle="tab" aria-hidden="true"></i>
                                <li class="nav-item my-0 py-0" style="width: 130px; background: #999966; color: white; font-weight: bold; text-align: center;">Total Interests
                               <div class="dropdown-divider"></div>
                                    <asp:LinkButton runat="server" ID="lblInterest" ForeColor="White" OnClick="lblInterest_Click"></asp:LinkButton>
                                    &nbsp
                                 
                                    (<asp:Label runat="server" ID="lblIntrProdPer" Text=""></asp:Label>%)
                                </li>
                                <i class="fa fa-arrow-circle-right" style="font-size: 25px; margin-top: 15px; color: darkgray"
                                    data-toggle="tab" aria-hidden="true"></i>
                                <li class="nav-item my-0 py-0" style="width: 150px; background: #00CC66; color: white; font-weight: bold; text-align: center;">EOI/RFP Status
                                  
                                    <div class="dropdown-divider"></div>
                                    <asp:LinkButton runat="server" ID="lblEOIRFP" ForeColor="White" OnClick="lblEOIRFP_Click"></asp:LinkButton>
                                    &nbsp(<asp:Label runat="server" ID="lblEoiPerc" Text=""></asp:Label>%)
                                </li>
                                <i class="fa fa-arrow-circle-right" style="font-size: 25px; margin-top: 15px; color: darkgray" data-toggle="tab" aria-hidden="true"></i>
                                <li class="nav-item my-0 py-0" style="width: 130px; background: #008036; color: white; font-weight: bold; text-align: center;">Supply Order
                                    <div class="dropdown-divider"></div>
                                    <asp:LinkButton runat="server" ID="lblsupply" ForeColor="White" OnClick="lblsupply_Click"></asp:LinkButton>
                                    &nbsp(<asp:Label runat="server" ID="lbSupplyPer" Text=""></asp:Label>%)
                                </li>
                                <i class="fa fa-arrow-circle-right" style="font-size: 25px; margin-top: 15px; color: darkgray" data-toggle="tab" aria-hidden="true"></i>
                                <li class="nav-item my-0 py-0" style="width: 130px; background: #004D28; color: white; font-weight: bold; text-align: center;">Success Story
                                    <div class="dropdown-divider"></div>
                                    <asp:LinkButton runat="server" ID="lblindiginized" ForeColor="White" OnClick="lblindiginized_Click"></asp:LinkButton>
                                    &nbsp(<asp:Label runat="server" ID="lblIndiPerc" Text=""></asp:Label>%)
                                </li>
                            </ul>
                        </div>
                        <div class="col-2 d-flex justify-content-end">
                            <asp:LinkButton runat="server" ID="lbproddetail" ToolTip="A brief detail of company wise product" data-toggle="tooltip" OnClick="lbproddetail_Click"><i class="fa fa-eye"></i>&nbsp;More Details</asp:LinkButton>
                        </div>
                        <div id="divInfo" runat="server" class="col-9 d-flex justify-content-center" visible="false">
                            <table class="table table-responsive text-center text-white">
                                <thead>
                                    <tr id="tabcolrchng" runat="server" style="padding: 0PX;">
                                        <th id="th9" runat="server" visible="false">
                                            <asp:LinkButton ID="lbltrgtyear" runat="server" ForeColor="White" Text=""></asp:LinkButton>
                                        </th>
                                        <th id="th10" runat="server" visible="false">
                                            <asp:LinkButton ID="lblttlint" runat="server" ForeColor="White" Text=""></asp:LinkButton>
                                        </th>
                                        <th id="th11" runat="server" visible="false">
                                            <asp:LinkButton ID="lblpendintshown" runat="server" ForeColor="White" Text=""></asp:LinkButton>
                                        </th>
                                        <th id="th12" runat="server" visible="false">
                                            <asp:LinkButton ID="lblpendeoi" runat="server" ForeColor="White" Text=""></asp:LinkButton>
                                        </th>
                                        <th id="th13" runat="server" visible="false">
                                            <asp:LinkButton ID="lblpendsupporder" runat="server" ForeColor="White" Text=""></asp:LinkButton>
                                        </th>
                                        <th id="th14" runat="server" visible="false">
                                            <asp:LinkButton ID="lblpendindig" runat="server" ForeColor="White" Text=""></asp:LinkButton>
                                        </th>
                                        <th id="th15" runat="server" visible="false">
                                            <asp:Label ID="searchResultLabel" runat="server" ForeColor="White" Text="" />
                                        </th>
                                        <th id="th1" runat="server" visible="false">
                                            <asp:LinkButton ID="lbltotother" runat="server" ForeColor="White" OnClick="lbltotother_Click" Text=""></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton ID="lblNoOfProducts" runat="server" ForeColor="White" OnClick="lblNoOfProducts_Click" Text=""></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton ID="lbltotalintrestshowprod" runat="server" ForeColor="White" OnClick="lbltotalintrestshowprod_Click" Text=""></asp:LinkButton>
                                        </th>
                                        <th id="th2" runat="server" visible="false">
                                            <asp:Label ID="lblother1" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th id="th6" runat="server" visible="false">
                                            <asp:Label ID="lbltarget" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th id="th7" runat="server" visible="false">
                                            <asp:Label ID="lblintrec" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th id="th8" runat="server" visible="false">
                                            <asp:Label ID="lblintshown" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th id="th3" runat="server" visible="false">
                                            <asp:LinkButton ID="lbleoissue" runat="server" ForeColor="White" OnClick="lbleoissue_Click" Text=""></asp:LinkButton>
                                        </th>
                                        <th id="th4" runat="server" visible="false">
                                            <asp:LinkButton ID="lblsuppyissue" runat="server" ForeColor="White" OnClick="lblsuppyissue_Click" Text=""></asp:LinkButton>
                                        </th>
                                        <th id="th5" runat="server" visible="false">
                                            <asp:LinkButton ID="lblindig" runat="server" ForeColor="White" OnClick="lblindig_Click" Text=""></asp:LinkButton>
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <div id="divbox" runat="server" class="col-12 d-flex justify-content-center" visible="false">
                            <div class="table-responsive text-center">
                                <table class="table table-bordered" style="background: #999966;">
                                    <tbody>
                                        <tr class="text-white">
                                            <td>NIL Action</td>
                                            <td>Unable to contact vendor</td>
                                            <td>Response Awaited</td>
                                            <td>Response U/evaluation</td>
                                            <td>Vendor not Qualified</td>
                                            <td>Vendor Qualified</td>
                                            <td>Item not required</td>
                                            <td>Indigenization U/Process with other vendor</td>
                                            <td>EOI done/SO Placed/Indigenized</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnknil" runat="server" ForeColor="White" OnClick="lnknil_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkunbconvend" runat="server" ForeColor="White" OnClick="lnkunbconvend_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkresawait" runat="server" ForeColor="White" OnClick="lnkresawait_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkrespeval" runat="server" ForeColor="White" OnClick="lnkrespeval_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkvendnotfit" runat="server" ForeColor="White" OnClick="lnkvendnotfit_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkvendfit" runat="server" ForeColor="White" OnClick="lnkvendfit_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkitemnotreq" runat="server" ForeColor="White" OnClick="lnkitemnotreq_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkindUproc" runat="server" ForeColor="White" OnClick="lnkindUproc_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkeoidone" runat="server" ForeColor="White" OnClick="lnkeoidone_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="eoifilters" runat="server" visible="false" class="row mb-3">
                        <div class="col-lg-11">
                            <div class="row d-flex justify-content-start">
                                <asp:HiddenField runat="server" ID="hidType" />
                                <asp:HiddenField runat="server" ID="hfcomprefno" />
                                <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
                                <div class="col-md-3" runat="server">
                                    <div class="form-group">
                                        <label id="lblcomp" runat="server">Select Company:</label>
                                        <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-md-3" runat="server">
                                       <div class="form-group">
                                             <label id="lblfinyear" runat="server">Select Financial Year:</label>
                                            <asp:DropDownList runat="server" ID="ddlyearofindiginization" TabIndex="22" AutoPostBack="true" OnSelectedIndexChanged="ddlyearofindiginization_SelectedIndexChanged" class="form-control"></asp:DropDownList>
                                       </div>
                                </div>
                                <div class="col-md-3" visible="false" runat="server" id="lblselectdivison">
                                    <div class="form-group">
                                        <label>Select Division/Plant:</label>
                                        <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" visible="false" runat="server" id="lblselectunit">
                                    <div class="form-group">
                                        <label>Select Unit:</label>
                                        <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row d-flex justify-content-between">
                        <div class="col-sm-3 mb-sm-0 mb-2 d-flex">
                            <asp:LinkButton runat="server" ID="btnExcel" CssClass="btn" Style="background: #6915cf!important; margin-right: 5px!important; color: #fff;" OnClick="btnExcel_Click">Excel</asp:LinkButton>
                            <asp:TextBox ID="txtgosearch" runat="server" Style="max-height: 45px; max-width: 100px;" CssClass="form-control appended-form-control" AutoCompleteType="Search" Placeholder="Page No."></asp:TextBox>
                            <asp:LinkButton ID="btngoto" runat="server" CssClass="btn" Style="background: #6915cf!important; color: #fff;" OnClick="btngosearch_Click">Go to</asp:LinkButton>
                        </div>

                        <div class="col-sm-9 d-flex justify-content-end">
                            <asp:TextBox ID="txtsearch" runat="server" Style="max-height: 45px; max-width: 420px;"
                                ToolTip="search tab with all criteria using words." onblur="SaveData('txtsearch')" OnTextChanged="txtsearch_TextChanged" CssClass="form-control appended-form-control"
                                Placeholder="Search (type min three character)"></asp:TextBox>
                            <asp:Button runat="server" ID="btnsearch" Style="background: #6915cf!important; color: #fff;" CssClass="btn" Text="Search" OnClick="btnsearch_Click" />
                        </div>
                        <div class="col-12" id="DivmPrint">
                            <table class="table table-responsive">
                                <asp:GridView runat="server" ID="gvProgress" Width="100%" Class="table table-hover table-bordered" AutoGenerateColumns="False"
                                    OnRowCreated="gvProgress_RowCreated" OnRowCommand="gvProgress_RowCommand"
                                    OnRowDataBound="gvProgress_RowDataBound" CellPadding="4" GridLines="None" ForeColor="#333333">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="80px">
                                            <ItemTemplate>
                                                <%#Eval("row_no") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbldate" Text='<%#Eval("VendorDate","{0: dd-MMM-yyyy}") %>'>                                                       
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblcompshow" ToolTip='<%#Eval("ProductRefNo") %>' Text='<%#Eval("CompanyName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Division/Unit" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldivsion" runat="server" Text='<%# Eval("FactoryName") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblunit" runat="server" Text='<%# Eval("UnitName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                        <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="220px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblitemDesc" runat="server" data-toggle="tooltip" ToolTip='<%# Eval("ProductRefNo") %>'
                                                    Text='<%# Eval("ProductDescription").ToString().Length > 35? (Eval("ProductDescription") as string).Substring(0,35) + ".." : Eval("ProductDescription")  %>' CommandArgument='<%# Eval("ProductRefNo") %>' CommandName="Product">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="ProductRefNo" HeaderStyle-Width="130px" HeaderText="Item Code" />
                                        <asp:BoundField DataField="NSCCode" HeaderText="NSC Code" HeaderStyle-Width="100px" />
                                        <asp:TemplateField HeaderText="Shown Interest (Y/N)" HeaderStyle-Width="100px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblintstatus2" Text='<%# Eval("IntShownStatus")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Annual value of import negated(Rs Lakh)" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkannimpneg1" Text='<%# Eval("estimate") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Starting Year" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkstartyear1" Text='<%# Eval("IndTargetYear") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recent Vendor Name" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblepold" runat="server"
                                                    Text='<%# Eval("VendorName").ToString().Length > 39 ? (Eval("VendorName") as string).Substring(0,39) + ".." : Eval("VendorName")  %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. Of Interest" HeaderStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="totalintstatus" Text='<%# Eval("Total")%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action initiated" HeaderStyle-ForeColor="#333333" HeaderStyle-Width="200px">
                                            <HeaderTemplate>
                                                <table class="table-borderless">
                                                    <tr>
                                                        <td colspan="14" style="background: #507CD1!important; color: white;">Action Initiated</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background: #507CD1!important; color: white;">Yes
                                                        </td>
                                                        <td style="background: #507CD1!important; color: white;">No
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:LinkButton runat="server" ID="lblintstatus" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="Status" Text='<%# Eval("IntYes")%>'></asp:LinkButton>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:LinkButton runat="server" ID="lblintstatus1" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="Status1" Text='<%# Eval("IntNo")%>'></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EOI" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="EOI" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="EOI" Text='<%#Eval("EOIStatus") %>'></asp:LinkButton>
                                                <asp:HiddenField runat="server" ID="hfeoiurl" Value='<%#Eval("EOIURL") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supply Order" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="mSupplyorder" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="SupplyOrd" Text='<%#Eval("SupplyOrderStatus") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Success Story" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="successstory" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="Success_story" Text='<%#Eval("IsIndeginized") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Starting Year" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkstartyear" Text='<%# Eval("IndTargetYear") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Indigenization Year" HeaderStyle-Width="60px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblindegyr" runat="server" Text='<%# Eval("YearofIndiginization") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reporting Month" HeaderStyle-Width="100px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonth" runat="server" Text='<%# Eval("MONTH") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Annual value of import negated(Rs Lakh)" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkannimpneg" Text='<%# Eval("estimate") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Make In India Category" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hfproc" Value='<%# Eval("PurposeofProcurement") %>' />
                                                <asp:LinkButton runat="server" ID="lnkmakeindcat" Text='<%# Eval("MakeInIndiaCategory") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EOI Start Date" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkeoistartdate" Text='<%# Eval("EOIStartDate") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EOI End Date" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkeoienddate" Text='<%# Eval("EOIEndDate") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SupplyOrderDate" HeaderStyle-Width="200px" Visible="false" HeaderText="Supply Order Date/Month/Year" NullDisplayText="NA" />
                                        <asp:BoundField DataField="SupplyManfutureName" HeaderStyle-Width="150px" Visible="false" HeaderText="Manufacture Name" NullDisplayText="NA" />
                                        <asp:BoundField DataField="SupplyManfutureAddress" HeaderStyle-Width="150px" Visible="false" HeaderText="Manufacture Address" NullDisplayText="NA" />
                                        <asp:BoundField DataField="SupplyOrderValue" HeaderStyle-Width="150px" Visible="false" HeaderText="SO value in Rs(Lakh)" NullDisplayText="NA" />
                                        <asp:BoundField DataField="IsIndeginized" HeaderStyle-Width="150px" Visible="false" HeaderText="Indigenized (Y/N)" NullDisplayText="NA" />
                                         <asp:TemplateField HeaderText="Active" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblactive" Text='<%# Eval("IsActive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Archive" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblarchive" Text='Archive'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="60px" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="EOIaction" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="EOI" Text="Edit"></asp:LinkButton>
                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="mSupplyorderaction" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="SupplyOrd" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="200px" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="successstoryaction" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="Success_story" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                    
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </table>
                        </div>
                        <div class="col-12 " id="divcontentproduct" runat="server">
                            <nav class="d-flex justify-content-between" aria-label="Page navigation">
                                <div class="col-6 d-flex justify-content-start">
                                    <ul class="pagination">
                                        <li class="page-item" runat="server" visible="false">
                                            <asp:LinkButton ID="LinkButton1" runat="server" class="page-link" OnClick="lnkbtnPgPrevious_Click"><i class="fa fa-chevron-left mr-2"></i>Prev</asp:LinkButton>
                                        </li>
                                        <li class="page-item" style="background: #6915cf!important;">
                                            <span style="text-align: center;">Showing                                       
                                               
                                                <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                                                products of 
                                               
                                                <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
                                                products  
                                            </span>
                                        </li>
                                        <li class="page-item" runat="server" visible="false">
                                            <asp:LinkButton ID="LinkButton2" runat="server" class="page-link" OnClick="lnkbtnPgNext_Click"> Next<i class="fa fa-chevron-right ml-2"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-6 d-flex justify-content-end">
                                    <ul class="pagination">
                                        <li class="page-item" style="background: #6915cf!important;">
                                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" class="page-link" OnClick="lnkbtnPgPrevious_Click"><i class="fa fa-chevron-left mr-2"></i> Prev</asp:LinkButton>
                                        </li>
                                        <li class="page-item" style="background: #6915cf!important;">
                                            <asp:Label ID="lblpaging" runat="server"></asp:Label>
                                        </li>
                                        <li class="page-item" style="background: #6915cf!important;">
                                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" class="page-link" OnClick="lnkbtnPgNext_Click"> Next<i class="fa fa-chevron-right ml-2"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </nav>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="ProductCompany1" tabindex="-1">
                <div class="modal-dialog modal-xl" style="max-width: 1000px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h5 class="modal-title text-white">Progress Report Details</h5>
                        </div>
                         <div>
                              <h6 class="modal-title text-Red" style="font-family:Arial;font-size:medium; color:red; font:bold;margin-top:3%">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Remark : The Total value has been by taking max value of import taking into date of previous 3 year & the current year 2021-22.</h6>
                        </div>
                        <div class="modal-body" style="padding: 20px 40px 18px 40px;" id="printarea">
                            <asp:GridView runat="server" ID="gvhover" Class="table table-striped table-bordered table-responsive overflow-auto" Width="100%" AutoGenerateColumns="false" CellPadding="4"
                                ForeColor="#333333" GridLines="None" ShowFooter="true">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="CompName" HeaderText="Company" />
                                    <asp:BoundField DataField="TotalProduct" HeaderText="Product" />
                                    <asp:BoundField DataField="Interest" HeaderText="Total Interest" />
                                    <asp:BoundField DataField="EoiProductIntrest" HeaderText="EOI/RFp Status" />
                                    <asp:BoundField DataField="eoistatus" HeaderText="Total EOI/RFP" />
                                    <asp:BoundField DataField="supplyorder" HeaderText="Total Supply-Order" />
                                    <asp:BoundField DataField="Indiginized" HeaderText="Total Indiginized" />
                                    <asp:BoundField DataField="IndigTarget" HeaderText="Target Year" />
                                    <asp:BoundField DataField="Category" HeaderText="Indiginized Category" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer" style="padding-top: 0px!important;">
                            <input id="btnprint" type="button" runat="server" onclick="PrintDiv()" style="width: 70px; background: #507CD1!important;" class="btn btn-primary"
                                value="Print" />
                            <asp:LinkButton ID="LinkButton4" runat="server" Text="Close" Style="background: #507CD1!important;" class="btn btn-primary"
                                ClientIDMode="Static" ToolTip="Update Data" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="ProductCompany" tabindex="-1">
                <div class="modal-dialog modal-xl" style="max-width: 900px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h5 class="modal-title text-white">Import Item Details</h5>
                        </div>
                        <div class="modal-body" style="padding: 20px 40px 18px 40px;">
                            <div class="simplebar-content">
                                <!-- Categories-->
                                <div class="widget widget-categories mb-4">
                                    <div class="accordion mt-n1" id="shop-categories">
                                        <div id="printarea1">
                                            <div class="card" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                <div class="card-header">
                                                    <h3 class="accordion-heading mb-2">
                                                        <a class="collapsed" href="#shoes" role="button" data-toggle="collapse" aria-expanded="false"
                                                            aria-controls="shoes">Item Description <span class="accordion-indicator iconupanddown">
                                                                <i class="fas fa-chevron-up"></i></span></a>
                                                    </h3>
                                                </div>
                                                <div class="collapse" id="shoes" data-parent="#shop-categories">
                                                    <div class="card-body card-custom ">
                                                        <h6 class="tablemidhead">DPSUs,OFB & SHQs Details</h6>
                                                        <table class="table mb-2">
                                                            <tbody>
                                                                <tr>
                                                                    <th scope="row">DPSU/OFB/SHQ:
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblcompname" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="one">
                                                                    <th scope="row">Division/Plant:Unit
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbldiviname" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="lblunitnamepro" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="two" visible="false" class="d-none">
                                                                    <th scope="row" class="d-none">Unit:
                                                                    </th>
                                                                    <td></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <h6 class="tablemidhead">Item Description</h6>
                                                        <table class="table mb-2">
                                                            <tbody>
                                                                <tr>
                                                                    <th scope="row">Item Id (Portal)
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblrefnoview" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="Tr23" style="color: blue;">
                                                                    <th>Item Name</th>
                                                                    <td>
                                                                        <asp:Label ID="lblitemname1" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="three">
                                                                    <th scope="row">DPSU Part Number
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbldpsupartno" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="Tr8">
                                                                    <th scope="row">NIN Code
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblnincode" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="four">
                                                                    <th scope="row">HSN Code
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblhsncode8digit" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">Industry Domain
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="prodIndustryDomain" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="ProdIndusSubDomain" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <h6 class="tablemidhead">OEM Details</h6>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="seven">
                                                                    <th scope="row">OEM Name:OEM Country
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbloemname" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="lbloemcountry" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="eight">
                                                                    <th scope="row">OEM Part Number
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbloempartno" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="nine" visible="false" class="d-none">
                                                                    <th scope="row" class="d-none">OEM Country
                                                                    </th>
                                                                    <td></td>
                                                                </tr>
                                                                <tr runat="server" id="twentyfive" visible="false">
                                                                    <th scope="row">OEM Address
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbloemaddress" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <h6 class="tablemidhead">Item Classification (NATO Group & Class)</h6>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">NATO Supply Group:
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblnsngroup" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">NATO Supply Class:
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblnsngroupclass" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">Item Name Code:
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="six">
                                                                    <th scope="row">NSC Code (4 digit):
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblnsccode4digit" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                <div class="card-header">
                                                    <h3 class="accordion-heading mb-2">
                                                        <a class="collapsed" href="#ItemSpecification" role="button" data-toggle="collapse"
                                                            aria-expanded="false" aria-controls="shoes">Item Specification <span class="accordion-indicator iconupanddown">
                                                                <i class="fas fa-chevron-up"></i></span></a>
                                                    </h3>
                                                </div>
                                                <div class="collapse" id="ItemSpecification" data-parent="#shop-categories">
                                                    <div class="card-body card-custom ">
                                                        <table class="table mb-2">
                                                            <tbody>
                                                                <tr runat="server" id="eleven" style="color: blue;">
                                                                    <th>Item Name</th>
                                                                    <td>
                                                                        <asp:Label ID="itemname2" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="twele">
                                                                    <th scope="row">Document
                                                                    </th>
                                                                    <td>
                                                                        <asp:GridView runat="server" ID="gvpdf" AutoGenerateColumns="false" Class="table table-responsive table-bordered">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblpathname" runat="server" Text='<%#Eval("ImageName").ToString().Substring(7) %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="View or Download">
                                                                                    <ItemTemplate>
                                                                                        <a href='<%#Eval("ImageName") %>' target="_blank" title="Click on icon for download pdf">View or downlaod</a>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="thirteen">
                                                                    <th scope="row">Image
                                                                    </th>
                                                                    <td>
                                                                        <asp:DataList ID="dlimage" runat="server" RepeatColumns="4" Visible="true" RepeatDirection="Horizontal"
                                                                            RepeatLayout="Flow">
                                                                            <ItemTemplate>
                                                                                <div class="col-sm-3">
                                                                                    <a data-fancybox="Prodgridviewgellry" target="_blank" href='<%#Eval("[ImageName]") %>'>
                                                                                        <asp:Image ID="imgprodimage" runat="server" CssClass="img-responsive img-container"
                                                                                            Height="90px" Width="110px" src='<%#Eval("[ImageName]") %>' />
                                                                                    </a>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="twentysix">
                                                                    <th scope="row">Quality Assurance Agency 
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbqa" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                <div class="card-header">
                                                    <h3 class="accordion-heading mb-2">
                                                        <a class="collapsed" href="#Estimated" role="button" data-toggle="collapse" aria-expanded="false"
                                                            aria-controls="shoes">Import Value, Quantity <span class="accordion-indicator iconupanddown">
                                                                <i class="fas fa-chevron-up"></i></span></a>
                                                    </h3>
                                                </div>
                                                <div class="collapse" id="Estimated" data-parent="#shop-categories">
                                                    <div class="card-body card-custom ">
                                                        <div runat="server" id="fifteen">
                                                            <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false"
                                                                CssClass="table table-hover">
                                                                <Columns>
                                                                    <asp:BoundField DataField="FYear" HeaderText="Year of Import" />
                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                        <ItemTemplate>
                                                                            <%# Eval("EstimatedQty").ToString() == "0" ? "*" : Eval("EstimatedQty").ToString()%>
                                                                            <%-- <%# Eval("EstimatedQty").ToString() == "0" ? "*" : "*"%>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Import value in million Rs (Qty*Price)" DataFormatString="{0:f2}" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div runat="server" id="five">
                                                            <b>Import value during last 3 year (million Rs) :</b>
                                                            <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                                                        &nbsp;<asp:Label ID="lblvalueimport" runat="server"
                                                                                            Text="0"></asp:Label>&nbsp;
                                                       
                                                        </div>
                                                        <div runat="server" id="ten">
                                                            <asp:GridView ID="gvestimatequanold" runat="server" AutoGenerateColumns="false"
                                                                Class="table table-responsive table-bordered">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Year of Import" DataField="FYear" />
                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                        <ItemTemplate>
                                                                            <%# Eval("EstimatedQty").ToString() == "0" ? "*" : Eval("EstimatedQty").ToString()%>
                                                                            <%-- <%# Eval("EstimatedQty").ToString() == "0" ? "*" : "*"%>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                    <asp:BoundField HeaderText="Imported value in million Rs (Qty*Price)" DataField="EstimatedPrice" DataFormatString="{0:f2}" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <h6 class="tablemidhead">Status of Indigenization</h6>
                                                        <table class="table mb-2">
                                                            <tbody>
                                                                <tr runat="server" id="Tr25">
                                                                    <th scope="row">Indigenization starting Year
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblindtrgyr" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="Tr1">
                                                                    <th scope="row">Indigenization started
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblindstart" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="sixteen">
                                                                    <th scope="row">Make in India Category
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblindicate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="seventeen">
                                                                    <th scope="row">EoI/RFP
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbleoirep" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="eighteen">
                                                                    <th scope="row">EoI/RFP URL
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbleoilink" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </tbody>
                                                        </table>
                                                        <h6 class="tablemidhead">Contact Details</h6>
                                                        <table class="table mb-2" runat="server" id="nineteen">
                                                            <tbody>
                                                                <tr>
                                                                    <th scope="row">Name
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblempname" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">Designation
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">E-Mail ID
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblemailidpro" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row">Phone Number
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblphonenumber" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;" runat="server" visible="false">
                                                <div class="card-header">
                                                    <h3 class="accordion-heading mb-2">
                                                        <a class="collapsed" href="#AdditionalValue" role="button" data-toggle="collapse" aria-expanded="false"
                                                            aria-controls="shoes">Additional Details <span class="accordion-indicator iconupanddown">
                                                                <i class="fas fa-chevron-up"></i></span></a>
                                                    </h3>
                                                </div>
                                                <div class="collapse" id="AdditionalValue" data-parent="#shop-categories">
                                                    <div class="card-body card-custom ">
                                                        <table class="table mb-2">
                                                            <tbody>
                                                                <tr runat="server" id="twenty">
                                                                    <th scope="row">End User 
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblenduser" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="twentyone">
                                                                    <th scope="row">Defence Paltform 
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbldefenceplatform" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="twentytwo">
                                                                    <th scope="row">Name of Defence Platform 
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblnameofdefplat" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="twentyfour" visible="false">
                                                                    <th scope="row"></th>
                                                                    <td runat="server">
                                                                        <asp:Label ID="lblisshowgeneral" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th scope="row"></th>
                                                                    <td runat="server">
                                                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LinkButton8" runat="server" Text="Close" Style="background: #507CD1!important;" class="btn btn-primary"
                                ClientIDMode="Static" ToolTip="Update Data" data-dismiss="modal" />
                            <input id="Button1" type="button" runat="server" visible="false" onclick="PrintDiv1()" style="width: 70px;" class="btn btn-primary  pull-right"
                                value="Print" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="action" role="dialog">
                <div class="modal-dialog  modal-xl" style="max-width: 400px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div runat="server" id="Div2">
                            <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                                <h6 class="modal-title text-white">*****</h6>
                                <span id="Span5" runat="server"></span>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton ID="LinkButton9" runat="server" Text="Close" Style="background: #507CD1!important;" class="btn btn-primary"
                                    ClientIDMode="Static" data-dismiss="modal" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="divstatus" role="dialog">
                <div class="modal-dialog  modal-xl" style="max-width: 400px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div runat="server" id="Div15">
                            <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                                <h6 class="modal-title text-white">EOI Status</h6>
                                <span id="success" runat="server"></span>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <asp:HiddenField ID="hfprorefno" runat="server" />
                                <table style="position: center;" class="table table-responsive">
                                    <tr>
                                        <td>EOI/RFP:</td>
                                        <td>
                                            <asp:RadioButtonList ID="rbeoimake2" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No" style="margin-left: 10px;">No</asp:ListItem>
                                                <asp:ListItem Value="Archive" style="margin-left: 10px;">Archive</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Link:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="mhylink" Height="80px" CssClass="form-control" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Start Date:
                                        </td>
                                        <td>

                                            <%--<asp:TextBox ID="lblstartdate" runat="server" CssClass="my_date_picker2"
                                                                        autocomplete="off" placeholder="Start date (format:- 01-jan-1900)"></asp:TextBox>--%>
                                            <asp:TextBox runat="server" ID="lblstartdate" type="text" class="my_date_picker2 form-control" placeholder="01-jan-1900)"></asp:TextBox>

                                            <%-- <input type="text" id="lblstartdate" class="my_date_picker2" placeholder="01-01-1990">--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>EndDate: </td>
                                        <td>

                                            <%--<asp:TextBox ID="lblenddate" runat="server" CssClass="my_date_picker2" autocomplete="off"
                                                                        placeholder="End date (format:- 01-jan-1900)"></asp:TextBox>--%>
                                            <asp:TextBox runat="server" ID="lblenddate" type="text" class="my_date_picker2 form-control" placeholder="01-jan-1900)"></asp:TextBox>
                                            <%--<input type="text" id="lblenddate" class="my_date_picker2" placeholder="01-01-1990">--%>
                                        </td>
                                        <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Date" ForeColor="Red" runat="server"
                                            ControlToValidate="lblstartdate" ControlToCompare="lblenddate" Operator="LessThan" Type="Date"
                                            ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                                    </tr>

                                    <tr>
                                        <td />
                                        <td class="d-flex justify-content-end">
                                            <asp:LinkButton ID="btnUpdate" runat="server" class="btn btn-primary  mr-2" Style="background: #507CD1!important;"
                                                ToolTip="Update Data" OnClick="btnUpdate_Click"><i class="fa fa-edit"></i>&nbsp;Update</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton6" runat="server" Text="Close" Style="background: #507CD1!important;" class="btn btn-primary"
                                                ClientIDMode="Static" ToolTip="Update Data" data-dismiss="modal" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="Supplyorder" role="dialog">
                <div class="modal-dialog  modal-xl" style="max-width: 400px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div runat="server" id="Div17">
                            <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                                <h6 class="modal-title text-white">Supply Order</h6>
                                <span id="successmsg" runat="server"></span>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <asp:HiddenField ID="hfprorefno1" runat="server" />
                                <table style="position: center;" class="table table-responsive">
                                    <%--     <asp:RegularExpressionValidator ID="regexpression" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$" ErrorMessage="Please enter valid decimal number with 2 decimal places." ForeColor="Red"  ControlToValidate="txtsupplyvalue"></asp:RegularExpressionValidator>--%>
                                    <tr>
                                        <td>SO Placed:</td>
                                        <td>
                                            <asp:RadioButtonList ID="rdblsoplaced" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No" style="margin-left: 10px;">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Supply Manufacture Name:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsupplyname" CssClass="form-control" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Supply Manufacture Address:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsupplyaddress" CssClass="form-control" TextMode="MultiLine" Height="100px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Supply Order Value(Rs Lakhs):
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtsupplyvalue" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" placeholder="Supply Order Value in (Rs Lakhs)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Supply Order Date:
                                        </td>
                                        <td>
                                            <%--   <asp:TextBox ID="txtsupplyorderdate" runat="server" CssClass="form-control" type="date" ></asp:TextBox>--%>
                                            <asp:TextBox ID="txtsupplyorderdate" runat="server" CssClass="my_date_picker2"
                                                autocomplete="off" placeholder="Start date (format:- 01-jan-1900)"></asp:TextBox>
                                            <%--<input type="text" id="txtsupplyorderdate" class="my_date_picker2" placeholder="01-01-1990">--%>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Delivery Compliance Date:
                                        </td>
                                        <td>
                                            <%-- <asp:TextBox ID="txtsupplydelivrydate" runat="server" CssClass="form-control" TYPE="DATE"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtsupplydelivrydate" runat="server" CssClass="my_date_picker2"
                                                autocomplete="off" placeholder="Start date (format:- 01-jan-1900)"></asp:TextBox>
                                            <%-- <input type="text" id="txtsupplydelivrydate" class="my_date_picker2" placeholder="01-01-1990"> --%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td />
                                        <td class="d-flex justify-content-end">

                                            <asp:LinkButton ID="lnkupate" runat="server" Style="background: #507CD1!important;" class="btn btn-primary mr-2"
                                                ClientIDMode="Static" OnClick="lnkupate_Click" ToolTip="Update Data"><i class="fa fa-edit"></i>&nbsp;Update</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton5" runat="server" Text="Close" Style="background: #507CD1!important;" class="btn btn-primary"
                                                ClientIDMode="Static" data-dismiss="modal" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="successtory" role="dialog">
                <div class="modal-dialog  modal-xl" style="max-width: 400px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div runat="server" id="Div1">
                            <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                                <h6 class="modal-title text-white">Success Story</h6>
                                <span id="Span1" runat="server"></span>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <asp:HiddenField ID="hfprorefno2" runat="server" />
                                <asp:HiddenField ID="hfisindegized" runat="server" />
                                <table style="position: center;" class="table table-responsive">

                                    <tr>
                                        <td>Indigenized Year:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtyear" Text="" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Supply Manufacture Name:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsuppman" CssClass="form-control" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Supply Manufacture Address:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsuppadrr" CssClass="form-control" TextMode="MultiLine" Height="100px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td />
                                        <td class="d-flex justify-content-end">
                                            <asp:LinkButton ID="LinkButton7" runat="server" Text="Update" Style="background: #507CD1!important;" class="btn btn-primary mr-2"
                                                ClientIDMode="Static" OnClick="lnkupatesuccess_Click" ToolTip="Update Data"><i class="fa fa-edit"></i>&nbsp;Update</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton10" runat="server" Text="Close" Style="background: #507CD1!important;" class="btn btn-primary"
                                                ClientIDMode="Static" data-dismiss="modal" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="modal-quick-view modal fade" id="inttotalshown" role="dialog">
                <div class="modal-dialog  modal-xl" style="max-width: 1367px  !important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h6 class="modal-title text-white">Interest Shown Status</h6>
                            <span id="Span3" runat="server"></span>
                        </div>
                        <div class="modal-body overflow-auto">
                            <asp:GridView ID="grdinttotal" runat="server" AutoGenerateColumns="false"
                                class="table table-hover table-responsive">
                                <Columns>
                                    <asp:TemplateField Visible="false" HeaderText="ProdRefNo">
                                        <ItemTemplate>
                                            <asp:Label ID="pref1" runat="server" Text='<%#Eval("ProductRefNo") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="hfprodref1" Value='<%#Eval("ProductRefNo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RequestDate" HeaderText="Date" DataFormatString="{0: dd-MMM-yyyy}" />
                                    <asp:BoundField DataField="RequestCompName" HeaderText="Company" />
                                    <asp:BoundField DataField="RequestAddress" HeaderText="Address" />
                                    <asp:BoundField DataField="RequestBy" HeaderText="Name" />
                                    <asp:BoundField DataField="RequestMobileNo" HeaderText="Mobile" />
                                    <asp:BoundField DataField="RequestEmail" HeaderText="Email" />
                                    <asp:BoundField DataField="IntShownReason" HeaderText="Remarks" NullDisplayText="NA" />
                                </Columns>
                            </asp:GridView>
                            <div class="modal-footer">
                                <asp:Button ID="Button2" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                                <div class="clearfix"></div>
                            </div>
                            <div><b><span style="color: red">Duplicacy of Show Interest has been removed based on Name, Email Id and Mobile No.!!!</span></b></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="unabcontvendor" role="dialog">
                <div class="modal-dialog  modal-xl" style="width: 930px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header mod al-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h6 class="modal-title text-white">Unable to contact vendor</h6>
                            <span id="Span13" runat="server"></span>
                        </div>
                        <div class="modal-body overflow-auto">
                            <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="false" OnRowCreated="GridView9_RowCreated"
                                class="table table-hover table-responsive">
                                <Columns>
                                    <asp:BoundField DataField="ProductRefNo" HeaderText="Item Code" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="IntShownReason" HeaderText="Remarks" HeaderStyle-Width="100px" NullDisplayText="NA" />

                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                             <asp:LinkButton runat="server" ID="LinkButton17" CssClass="btn btn-primary" Style=" margin-right: 5px!important; color: #fff;" OnClick="LinkButton17_Click">Excel</asp:LinkButton>
                            <asp:Button ID="Button11" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="reqnotneeded" role="dialog">
                <div class="modal-dialog  modal-xl" style="width: 930px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header mod al-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h6 class="modal-title text-white">Response Awaited</h6>
                            <span id="Span6" runat="server"></span>
                        </div>
                        <div class="modal-body overflow-auto">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" OnRowCreated="GridView2_RowCreated"
                                class="table table-hover table-responsive">
                                <Columns>
                                    <asp:BoundField DataField="ProductRefNo" HeaderText="Item Code" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="IntShownReason" HeaderText="Remarks" HeaderStyle-Width="100px" NullDisplayText="NA" />

                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                             <asp:LinkButton runat="server" ID="LinkButton16" CssClass="btn btn-primary" Style=" margin-right: 5px!important; color: #fff;" OnClick="LinkButton16_Click">Excel</asp:LinkButton>
                            <asp:Button ID="Button4" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="underprocess" role="dialog">
                <div class="modal-dialog  modal-xl" style="width: 950px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header mod al-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h6 class="modal-title text-white">Response U/Evaluation</h6>
                            <span id="Span7" runat="server"></span>
                        </div>
                        <div class="modal-body overflow-auto">
                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" OnRowCreated="GridView3_RowCreated"
                                class="table table-hover table-responsive">
                                <Columns>
                                    <asp:BoundField DataField="ProductRefNo" HeaderText="Item Code" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="IntShownReason" HeaderText="Remarks" HeaderStyle-Width="100px" NullDisplayText="NA" />
                                </Columns>
                            </asp:GridView>
                            <div class="modal-footer">
                                  <asp:LinkButton runat="server" ID="LinkButton15" CssClass="btn btn-primary" Style=" margin-right: 5px!important; color: #fff;" OnClick="LinkButton15_Click">Excel</asp:LinkButton>
                                <asp:Button ID="Button5" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="vendorsuit" role="dialog">
                <div class="modal-dialog  modal-xl" style="width: 950px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header mod al-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h6 class="modal-title text-white">Vendor Not Fit</h6>
                            <span id="Span8" runat="server"></span>
                        </div>
                        <div class="modal-body overflow-auto">
                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" OnRowCreated="GridView4_RowCreated"
                                class="table table-hover table-responsive">
                                <Columns>
                                    <asp:BoundField DataField="ProductRefNo" HeaderText="Item Code" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="IntShownReason" HeaderText="Remarks" HeaderStyle-Width="100px" NullDisplayText="NA" />

                                </Columns>
                            </asp:GridView>
                            <div class="modal-footer">
                                  <asp:LinkButton runat="server" ID="LinkButton14" CssClass="btn btn-primary" Style=" margin-right: 5px!important; color: #fff;" OnClick="LinkButton14_Click">Excel</asp:LinkButton>
                                <asp:Button ID="Button6" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="eoi" role="dialog">
                <div class="modal-dialog  modal-xl" style="max-width: 950px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header mod al-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h6 class="modal-title text-white">Vendor Fit</h6>
                            <span id="Span9" runat="server"></span>
                        </div>
                        <div class="modal-body overflow-auto">
                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="false" OnRowCreated="GridView5_RowCreated"
                                class="table table-hover table-responsive">
                                <Columns>
                                    <asp:BoundField DataField="ProductRefNo" HeaderText="Item Code" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="IntShownReason" HeaderText="Remarks" HeaderStyle-Width="100px" NullDisplayText="NA" />
                                </Columns>
                            </asp:GridView>
                            <div class="modal-footer">
                                  <asp:LinkButton runat="server" ID="LinkButton13" CssClass="btn btn-primary" Style=" margin-right: 5px!important; color: #fff;" OnClick="LinkButton13_Click">Excel</asp:LinkButton>
                                <asp:Button ID="Button7" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="other_action" role="dialog">
                <div class="modal-dialog  modal-xl" style="max-width: 950px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header mod al-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h6 class="modal-title text-white">Item Not Required</h6>
                            <span id="Span10" runat="server"></span>
                        </div>
                        <div class="modal-body overflow-auto">
                            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="false" OnRowCreated="GridView6_RowCreated"
                                class="table table-hover table-responsive">
                                <Columns>
                                    <asp:BoundField DataField="ProductRefNo" HeaderText="Item Code" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="IntShownReason" HeaderText="Remarks" HeaderStyle-Width="100px" NullDisplayText="NA" />
                                </Columns>
                            </asp:GridView>
                            <div class="modal-footer">
                                  <asp:LinkButton runat="server" ID="LinkButton12" CssClass="btn btn-primary" Style=" margin-right: 5px!important; color: #fff;" OnClick="LinkButton12_Click">Excel</asp:LinkButton>
                                <asp:Button ID="Button8" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="reqdoesnotexist" role="dialog">
                <div class="modal-dialog  modal-xl" style="width: 950px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header mod al-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h6 class="modal-title text-white">Indigenization U/Process with other</h6>
                            <span id="Span11" runat="server"></span>
                        </div>
                        <div class="modal-body overflow-auto">
                            <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="false" Onrowcreated="GridView7_RowCreated"
                                class="table table-hover table-responsive">
                                <Columns>
                                    <asp:BoundField DataField="ProductRefNo" HeaderText="Item Code" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="IntShownReason" HeaderText="Remarks" HeaderStyle-Width="100px" NullDisplayText="NA" />
                                </Columns>
                            </asp:GridView>
                            <div class="modal-footer">
                               <asp:LinkButton runat="server" ID="LinkButton3" CssClass="btn btn-primary" Style=" margin-right: 5px!important; color: #fff;" OnClick="LinkButton3_Click">Excel</asp:LinkButton>
                                <asp:Button ID="Button9" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="soplaced" role="dialog">
                <div class="modal-dialog  modal-xl" style="width: 950px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header mod al-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h6 class="modal-title text-white">EOI done/SO Placed/Indigenized</h6>
                            <span id="Span12" runat="server"></span>
                        </div>
                        <div class="modal-body overflow-auto">
                            <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="false" Onrowcreated="GridView8_RowCreated"
                                class="table table-hover table-responsive">
                                <Columns>
                                    <asp:BoundField DataField="ProductRefNo" HeaderText="Item Code" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="IntShownReason" HeaderText="Remarks" HeaderStyle-Width="100px" NullDisplayText="NA" />
                                </Columns>
                            </asp:GridView>
                            <div class="modal-footer">
                                 <asp:LinkButton runat="server" ID="LinkButton11" CssClass="btn btn-primary" Style=" margin-right: 5px!important; color: #fff;" OnClick="LinkButton11_Click">Excel</asp:LinkButton>
                                <asp:Button ID="Button10" Text="Close" class="btn btn-primary" ClientIDMode="Static" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

             <div class="modal-quick-view modal fade" id="nil" role="dialog">
                <div class="modal-dialog  modal-xl" style="max-width: 950px!important; z-index: 9999999999;">
                    <div class="modal-content">
                        <div class="modal-header mod al-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h6 class="modal-title text-white">Nil Action</h6>
                            <span id="Span14" runat="server"></span>
                        </div>
                        <div class="modal-body overflow-auto">
                            <p class="pull-right mr-1" runat="server" id="P9"></p>
                            <div class="clearfix"></div>
                            <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="false" class="table table-hover">
                                <Columns>
                                    <asp:BoundField DataField="ProductRefNo" HeaderText="Item Code" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="100px" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="100px" />
                                    <asp:TemplateField HeaderText="Division/Unit" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldivsion" runat="server" Text='<%# Eval("FactoryName") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblunit" runat="server" Text='<%# Eval("UnitName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                   
                                </Columns>
                            </asp:GridView>
                            <div class="modal-footer">
                                <asp:LinkButton runat="server" ID="LinkButton18" CssClass="btn btn-primary" Style="margin-right: 5px!important; background: #507CD1!important;" OnClick="LinkButton18_Click">Excel</asp:LinkButton>
                                <asp:Button ID="Button12" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="LinkButton3" />
            <asp:PostBackTrigger ControlID="LinkButton11" />
            <asp:PostBackTrigger ControlID="LinkButton12" />
            <asp:PostBackTrigger ControlID="LinkButton13" />
            <asp:PostBackTrigger ControlID="LinkButton14" />
            <asp:PostBackTrigger ControlID="LinkButton15" />
            <asp:PostBackTrigger ControlID="LinkButton16" />
             <asp:PostBackTrigger ControlID="LinkButton17" />

        </Triggers>
    </asp:UpdatePanel>
    <div class="modal-quick-view modal fade" id="InterestShownStatus" role="dialog">
        <div class="modal-dialog  modal-xl" style="max-width: 1550px !important; z-index: 9999999999;">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <asp:Panel ID="Panel2" runat="server">
                            <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                                <h6 class="modal-title text-white">Interest Shown Status - Yes</h6>
                                <span id="Span2" runat="server"></span>
                            </div>
                            <div class="modal-body overflow-auto" style="font-size: 14px;">
                                <asp:GridView ID="grdintshown" runat="server" AutoGenerateColumns="false"
                                    class="table table-hover table-responsive" OnRowDataBound="grdintshown_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField Visible="false" HeaderText="ProdRefNo">
                                            <ItemTemplate>
                                                <asp:Label ID="pref" runat="server" Text='<%#Eval("ProductRefNo") %>'></asp:Label>
                                                <asp:HiddenField runat="server" ID="hfprodref" Value='<%#Eval("ProductRefNo") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Box (√)">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="SelectCheckBox" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RequestID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrequestid" runat="server" Text='<%#Eval("RequestID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RequestDate" HeaderText="Date" DataFormatString="{0: dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="RequestCompName" HeaderText="Company" />
                                        <asp:BoundField DataField="RequestAddress" HeaderText="Address" />
                                        <asp:BoundField DataField="RequestBy" HeaderText="Name" />
                                        <asp:BoundField DataField="RequestMobileNo" HeaderText="Mobile" />
                                        <asp:BoundField DataField="RequestEmail" HeaderText="Email" />
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="txtremark" runat="server" Text='<%#Eval("IntShownReason") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlstatus1" Text='<%#Eval("Reasonid") %>' AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlstatus1_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Unable to contact vendor" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Response Awaited" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Response U/Evaluation" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Vendor Not Qualified" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Vendor Qualified" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Indigenization U/Process with other vendor" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="EOI done/SO Placed/Indigenized" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Item Not Required" Value="8"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason" Visible="false">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlreason_SelectedIndexChanged" Enabled="false" ID="ddlreason" Width="150px" Style="overflow: auto;">
                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox runat="server" ID="TxtBxDesc" Text='<%#Eval("UserReason") %>' TextMode="MultiLine" Rows="5" Visible="false" class="form-control" placeholder="Type Your Reason" required></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="lnkupdate" class="btn btn-primary" OnClick="lnkupdate_Click1" runat="server" Style="background: #507CD1!important;"><i class="fa fa-edit"></i>&nbsp;Update</asp:LinkButton>
                                    <asp:Button ID="lnkclose" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal-quick-view modal fade" id="InterestShownStatus1" role="dialog">
        <div class="modal-dialog  modal-xl" style="max-width: 1550px !important; z-index: 9999999999;">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="upd">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="modal-header mod al-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                                <h6 class="modal-title text-white">Interest Shown Status - No</h6>
                                <span id="Span4" runat="server"></span>
                            </div>
                            <div class="modal-body overflow-auto">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                    class="table table-hover table-responsive">
                                    <Columns>
                                        <asp:TemplateField Visible="false" HeaderText="ProdRefNo">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("ProductRefNo") %>'></asp:Label>
                                                <asp:HiddenField runat="server" ID="HiddenField1" Value='<%#Eval("ProductRefNo") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Box (√)">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RequestID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("RequestID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RequestDate" HeaderText="Date" DataFormatString="{0: dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="RequestCompName" HeaderText="Company" />
                                        <asp:BoundField DataField="RequestAddress" HeaderText="Address" />
                                        <asp:BoundField DataField="RequestBy" HeaderText="Name" />
                                        <asp:BoundField DataField="RequestMobileNo" HeaderText="Mobile" />
                                        <asp:BoundField DataField="RequestEmail" HeaderText="Email" />
                                        <asp:BoundField DataField="IntShownReason" HeaderText="Remarks" NullDisplayText="NA" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlstatus" Text='<%#Eval("Reasonid") %>' AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Unable to contact vendor" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Response awaited" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Response U/evaluation" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Vendor not qualified" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="vendor qualified" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Indigenization U/Process with other vendor" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="EoI done /SO placed/Indigenized" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Item not required" Value="8"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason" Visible="false">
                                            <ItemTemplate>
                                                <asp:DropDownList Visible="false" runat="server" Text='<%#Eval("IntShownReason") %>' ID="DropDownList1" AutoPostBack="true" OnSelectedIndexChanged="ddlreason_SelectedIndexChanged1" Width="150px" Style="overflow: auto;">
                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox runat="server" Visible="false" ID="TextBox1" Text='<%#Eval("UserReason") %>' TextMode="MultiLine" Rows="5" class="form-control" placeholder="Type Your Reason" required></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="lnkupdreason" class="btn btn-primary" OnClick="lnkupdreason_Click" runat="server" Style="background: #507CD1!important;"><i class="fa fa-edit"></i>&nbsp;Update</asp:LinkButton>
                                    <asp:Button ID="Button3" Text="Close" class="btn btn-primary" data-dismiss="modal" runat="server" Style="background: #507CD1!important;" />
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="dialog" title="Dialog Popup">
        <asp:Label ID="lblDialog" runat="server" ForeColor="Red" Font-Bold="true" />
    </div>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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

    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <script src="User/Uassets/js/all.min.js"></script>
    <script src="User/Uassets/js/jquery-ui.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>

    <script type="text/javascript">
        function showPopup() {
            $('#ProductCompany').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup4() {
            $('#InterestShownStatus').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup8() {
            $('#InterestShownStatus1').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup9() {
            $('#reqnotneeded').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup10() {
            $('#underprocess').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup11() {
            $('#vendorsuit').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup12() {
            $('#eoi').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup13() {
            $('#other_action').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup14() {
            $('#reqdoesnotexist').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup15() {
            $('#soplaced').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup16() {
            $('#unabcontvendor').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#divstatus').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup3() {
            $('#Supplyorder').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup5() {
            $('#successtory').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup2() {
            $('#ProductCompany1').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup6() {
            $('#divupdate').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup7() {
            $('#inttotalshown').modal('show');
        }
    </script>


     <script type="text/javascript">
         function showPopup19() {
             $('#nil').modal('show');
         }
    </script>



    <script>
        $(document).ready(function () {
            $("#adnce_search").click(function () {
                $("#adnce_search_box").toggle(400);
            });
        });
    </script>

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
                        url: 'User/U_ProductList.aspx/GetSearchKeyword',
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
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>
    <script type="text/javascript">
        function PrintDiv1() {
            var divToPrint = document.getElementById('printarea1');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>
    <script src="User/Uassets/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        //On Page Load
        $(function () {
            $('#ContentPlaceHolder1_gvProgress').dataTable({
                "bPaginate": false,
                "bFilter": false,
                "bInfo": false
                 ContentPlaceHolder1_gvProgress.destroy();
            });

        });
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#ContentPlaceHolder1_gvProgress').dataTable({
                        "bPaginate": false,
                        "bFilter": false,
                        "bInfo": false
                    });
                }
            });
        };
    </script>
    <script> 
        $(document).ready(function () {
            $(function () {

            });
        })
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(".my_date_picker2").datepicker(
                        {
                            dateFormat: 'dd-M-yy'

                        });
                }
            });
        };
    </script>



</asp:Content>

