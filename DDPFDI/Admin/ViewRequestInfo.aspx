<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequestInfo.aspx.cs" Inherits="Admin_ViewRequestInfo" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
                <div class="col-md-12">
                    <div class="clearfix"></div>
                    <div style="margin-top: 5px;">
                        <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="clearfix" style="margin-bottom: 10px;"></div>
            <div class="addfdi">
                <asp:UpdatePanel runat="server" ID="updatepan">
                    <ContentTemplate>
                        <div class="pull-right mr10">
                            <asp:LinkButton ID="btnexcelexport" runat="server" CssClass="btn btn-primary" Text="Export Excel" OnClick="btnexcelexport_Click"></asp:LinkButton>
                        </div>
                        <div class="clearfix mt10"></div>
                        <div class="col-sm-4" runat="server" id="">
                            <p>Select Company</p>
                            <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control" OnSelectedIndexChanged="ddlComp_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-sm-4"  runat="server" id="divlblselectdivison">
                            <p>Select Division/Factory</p>
                            <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-sm-4"  runat="server" id="divlblselectunit">
                            <p>Select Unit</p>
                            <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control" OnSelectedIndexChanged="ddlunit_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="clearfix mt10"></div>
                        <div class="col-sm-4">
                            <p>Enter Search Keyword</p>
                            <asp:TextBox runat="server" ID="txtsearch" CssClass="form-control" placeholder="Serach"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <p>Start Date</p>
                            <asp:TextBox runat="server" ID="txtstartdate" CssClass="form-control" type="date"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <p>End Date</p>
                            <asp:TextBox runat="server" ID="txtenddate" CssClass="form-control" type="date"></asp:TextBox>
                        </div>
                        <div class="pull-right mr10">
                            <asp:LinkButton runat="server" ID="btnsearch" CssClass="btn btn-success" Style="margin-top: 5px; margin-right: 15px;" Text="Seach" OnClick="btnsearch_Click"></asp:LinkButton>
                        </div>
                        <div class="clearfix mt10"></div>
                        <span style="text-align: center;">Showing                                   
                                        <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                            intrest of                               
                                        <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                            products  
                        </span>

                        <div class="clearfix mt10"></div>
                        <asp:HiddenField runat="server" ID="hfType" />
                        <asp:HiddenField runat="server" ID="hfCompRefNo" />
                        <asp:DataList runat="server" ID="dlrequest" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemCommand="dlrequest_ItemCommand">
                            <ItemTemplate>
                                <div class="col-sm-3">
                                    <div class="card product-card" style="box-shadow: 0 0.3rem 1.525rem -0.375rem rgba(0, 0, 0, 0.1);">
                                        <div class="card-body py-2" style="height: 300px;">
                                            <h2 class="m-0 text-center">
                                                <b class="text-center">
                                                    <asp:Label runat="server" ID="lblcompshow" class="product-meta d-block font-size-xs pb-1 text-center mt5" Style="color: #6915cf; font-size: 20px!important; margin-top: 10px;">
                                                                <%#Eval("RequestBy") %>
                                                    </asp:Label>
                                                </b>
                                            </h2>
                                            <h3 class="product-title font-size-sm  text-center mt5">
                                                <asp:Label runat="server" ID="lbldesc" title='<%#Eval("RequestCompName") %>'>
                                                                <%# Eval("RequestCompName").ToString().Length > 100? (Eval("RequestCompName") as string).Substring(0,100) + ".." : Eval("RequestCompName")  %>
                                                </asp:Label>
                                            </h3>
                                            <table class="table" style="font-size: 14px;">
                                                <tbody>
                                                    <tr id="Tr3" runat="server">
                                                        <td colspan="2" style="padding: 8px; font-size: 12px;">mobile Number :- <b>
                                                            <%#Eval("RequestMobileNo") %></b>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr17" runat="server">
                                                        <td colspan="2" style="padding: 8px; font-size: 12px;">Address :- <b>
                                                            <%#Eval("RequestAddress") %></b>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr18" runat="server">
                                                        <td colspan="2" style="padding: 8px; font-size: 12px;">Email :- <b>
                                                            <asp:Label ID="lblemail" runat="server" Text=' <%#Eval("RequestEmail") %>'></asp:Label>
                                                        </b>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr233" runat="server">
                                                        <td class="text-right" colspan="2" style="padding: 8px; font-size: 11px;">Date :- 
                                                                       
                                                                        <%#Eval("RequestDate","{0:dd-MMM-yyyy}") %> &nbsp;Time:- <%#Eval("RequestTime") %>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="text-center btn btn-primary btn-block " style="padding-bottom: 10px;">
                                            <asp:LinkButton ID="lbview" runat="server" class="nav-link-style font-size-ms" CommandName="View"
                                                CommandArgument='<%#Eval("RequestDate","{0:yyyy-MM-dd}") %>'>  
                                                <i class="fas fa-eye align-middle mr-1"></i>
                                                   <span style="color:white; margin-bottom:0px;">
                                                    More Details
                                                   </span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <div class="clearfix mt10">
                        </div>
                        <div class="text-center">
                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-success" OnClick="lnkbtnPgPrevious_Click">Prev</asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn btn-success" OnClick="lnkbtnPgNext_Click">
                            Next</asp:LinkButton>
                            <div style="margin-top: 15px;">
                                <asp:Label ID="lblpaging" runat="server"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnexcelexport" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepan">
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
                <div class="modal fade" id="aboutus" tabindex="-1" role="dialog">
                    <div class="modal-dialog modal-dialog-centered" role="document" style="width: 1000px;">
                        <div class="modal-content" style="z-index: 9999999999;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:Panel ID="pan1" runat="server">
                                        <div class="modal-header">
                                            <ul class="nav nav-tabs card-header-tabs" role="tablist" style="border: none!important;">
                                                <li class="nav-item"><a class="nav-link active" href="#" data-toggle="tab" role="tab"
                                                    aria-selected="true">Intrested Product Detail</a></li>
                                                <li class="liclose">
                                                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span></button>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="modal-body tab-content p-0">
                                            <asp:GridView ID="gvViewNodalOfficerAdd" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
                                    responsive no-wrap table-hover manage-user Grid"
                                                AutoGenerateColumns="false" OnRowCreated="gvRequestInfo_RowCreated" OnRowCommand="gvViewNodalOfficerAdd_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="More Details">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbview" runat="server" class="nav-link-style font-size-ms fas fa-eye" CommandName="View"
                                                                CommandArgument='<%#Eval("ProductRefNo") %>'>     
                                                            </asp:LinkButton>
                                                            <asp:HiddenField runat="server" ID="hfrole" Value='<%#Eval("Role") %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProductRefno" HeaderText="Reference No" NullDisplayText="#" />
                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" Visible="False" NullDisplayText="#" />
                                                    <asp:BoundField DataField="ProductDescription" HeaderText="Description" NullDisplayText="#" />
                                                    <asp:BoundField DataField="NSNGroup" HeaderText="NSN Group" NullDisplayText="-" />
                                                    <asp:BoundField DataField="NSCCode" HeaderText="NSC Code" NullDisplayText="-" />
                                                    <asp:BoundField DataField="NSNGroupClass" HeaderText="NSN Group Class" NullDisplayText="#" />
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="panview" runat="server" Visible="false">
                                        <div class="modal-header modal-header1">
                                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Import Item Detail</h4>
                                        </div>
                                        <form class="form-horizontal changepassword" role="form">
                                            <div class="modal-body">
                                                <div class="simplebar-content">
                                                    <!-- Categories-->
                                                    <div class="widget widget-categories mb-4">
                                                        <div class="accordion mt-n1" id="shop-categories">
                                                            <div id="printarea">
                                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                                    <div class="card-header">
                                                                        <h6 class="accordion-heading mb-2">
                                                                            <a class="collapsed" href="#ItemSpecification" role="button" data-toggle="collapse"
                                                                                aria-expanded="false" aria-controls="shoes">Item Specification <span class="accordion-indicator iconupanddown">
                                                                                    <i class="fas fa-chevron-up"></i></span></a>
                                                                        </h6>
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
                                                                                    <tr id="Tr10" runat="server" visible="false">
                                                                                        <th scope="row">Specification
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblitemspecification" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="fourteen">
                                                                                        <th scope="row">Features & Details
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblfeaturesanddetail" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="Tr11" runat="server" visible="false">
                                                                                        <th scope="row">Information
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:GridView ID="gvProdInfo" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="NameOfSpec" HeaderText="Name of Specification" />
                                                                                                    <asp:BoundField DataField="Value" HeaderText="Value " />
                                                                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="Tr12" runat="server" visible="false">
                                                                                        <th scope="row">Additional Information
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lbladditionalinfo" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="card" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                                    <div class="card-header">
                                                                        <h6 class="accordion-heading mb-2">
                                                                            <a class="collapsed" href="#shoes" role="button" data-toggle="collapse" aria-expanded="false"
                                                                                aria-controls="shoes">Item Description <span class="accordion-indicator iconupanddown">
                                                                                    <i class="fas fa-chevron-up"></i></span></a>
                                                                        </h6>
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
                                                                                        <th scope="row">Division/Plant:
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lbldiviname" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="two">
                                                                                        <th scope="row">Unit:
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblunitnamepro" runat="server" Text=""></asp:Label>
                                                                                        </td>
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
                                                                                            /
                                                     
                                                                                            <asp:Label ID="ProdIndusSubDomain" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="Tr9" runat="server" visible="false">
                                                                                        <th scope="row">Search keywords
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblsearchkeywords" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                            <h6 class="tablemidhead">OEM Details</h6>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="seven">
                                                                                        <th scope="row">OEM Name
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lbloemname" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="eight">
                                                                                        <th scope="row">OEM Part Number
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lbloempartno" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="nine">
                                                                                        <th scope="row">OEM Country
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lbloemcountry" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="twentyfive">
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

                                                                            <%--<h6 class="tablemidhead">Imported During Last 3 years</h6>--%>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                                    <div class="card-header">
                                                                        <h6 class="accordion-heading mb-2">
                                                                            <a class="collapsed" href="#Estimated" role="button" data-toggle="collapse" aria-expanded="false"
                                                                                aria-controls="shoes">Import Value, Quantity <span class="accordion-indicator iconupanddown">
                                                                                    <i class="fas fa-chevron-up"></i></span></a>
                                                                        </h6>
                                                                    </div>
                                                                    <div class="collapse" id="Estimated" data-parent="#shop-categories">
                                                                        <div class="card-body card-custom ">
                                                                            <table class="table" width="100%">
                                                                                <tbody>
                                                                                    <tr id="Tr13" runat="server" visible="false">
                                                                                        <th scope="row">PROCURMENT CATEGORY REMARK
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblprocremarks" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="fifteen">
                                                                                        <td>
                                                                                            <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false"
                                                                                                CssClass="table table-hover">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="FYear" HeaderText="Year of Import" />
                                                                                                    <asp:BoundField DataField="EstimatedQty" HeaderText="Quantity" />
                                                                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Import value in Rs lakh (Qty*Price)" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table class="table mb-2">
                                                                                                <tbody>
                                                                                                    <tr runat="server" id="five">
                                                                                                        <td colspan="2">
                                                                                                            <b>Import value during last 3 year (Rs lakhs) :</b>
                                                                                                            <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                                      &nbsp;<asp:Label ID="lblvalueimport" runat="server"
                                                                          Text="0"></asp:Label>&nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr runat="server" id="ten">
                                                                                                        <td colspan="2" style="border-top: 0px;">
                                                                                                            <asp:GridView ID="gvestimatequanold" runat="server" AutoGenerateColumns="false" Class="table table-responsive table-bordered">
                                                                                                                <Columns>
                                                                                                                    <asp:BoundField HeaderText="Year of Import" DataField="FYear" />
                                                                                                                    <asp:BoundField HeaderText="Quantity" DataField="EstimatedQty" />
                                                                                                                    <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                                                                    <asp:BoundField HeaderText="Imported value in Rs lakh (Qty*Price)" DataField="EstimatedPrice" />
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <h6 class="tablemidhead">Status of Indigenization</h6>
                                                                            <table class="table mb-2">
                                                                                <tbody>
                                                                                    <tr runat="server" id="sixteen">
                                                                                        <th scope="row">Indigenization Category
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
                                                                                        <th scope="row">Link
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lbleoilink" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="Tr1" runat="server" visible="false">
                                                                                        <th scope="row">Tendor Uploaded
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lbltendor" runat="server" Text=""></asp:Label>
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
                                                                                    <tr id="Tr14" runat="server" visible="false">
                                                                                        <th scope="row">Mobile Number
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblmobilenumber" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <th scope="row">Phone Number
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblphonenumber" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="Tr15" runat="server" visible="false">
                                                                                        <th scope="row">Fax
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblfaxpro" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                                                    <div class="card-header">
                                                                        <h6 class="accordion-heading mb-2">
                                                                            <a class="collapsed" href="#AdditionalValue" role="button" data-toggle="collapse" aria-expanded="false"
                                                                                aria-controls="shoes">Additional Details <span class="accordion-indicator iconupanddown">
                                                                                    <i class="fas fa-chevron-up"></i></span></a>
                                                                        </h6>
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
                                                                                    <tr runat="server" id="twentythree" visible="false">
                                                                                        <th scope="row"></th>
                                                                                        <td id="Td1" runat="server" visible="false">
                                                                                            <asp:Label ID="lbldeclaration" runat="server" Text="No IPR issue, No violation of TOT agreement, No violation of Security Concern"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="twentyfour" visible="false">
                                                                                        <th scope="row"></th>
                                                                                        <td id="Td2" runat="server" visible="false">
                                                                                            <asp:Label ID="lblisshowgeneral" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="Tr19" visible="false">
                                                                                        <th id="Th1" scope="row" runat="server" visible="false">Is Indigenised 
                                                                                        </th>
                                                                                        <td id="Td3" runat="server" visible="false">
                                                                                            <asp:Label ID="lblisindigenised" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="Tr20" visible="false">
                                                                                        <th scope="row">Indian Manufacturer
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblmanuname" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="Tr21" visible="false">
                                                                                        <th scope="row">Address
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblmanuaddress" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="Tr22" visible="false">
                                                                                        <th scope="row">Year of Make in India
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblyearofindi" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="Tr24" visible="false">
                                                                                        <th scope="row">Indigenization Process started
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblprocstart" runat="server" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr runat="server" id="Tr25" visible="false">
                                                                                        <th scope="row">Indigenization Target Year
                                                                                        </th>
                                                                                        <td>
                                                                                            <asp:Label ID="lblindtrgyr" runat="server" Text=""></asp:Label>
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
                                            <div class="clearfix mt10">
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button runat="server" ID="btnback" CssClass="btn btn-warning fas fa-backward" Text=" Back" OnClick="btnback_Click" />
                                            </div>
                                        </form>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function showPopup() {
            $('#aboutus').modal('show');
        }
    </script>
</asp:Content>
