<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUploadExcel.aspx.cs" Inherits="frmUploadExcel"
    MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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
            <asp:UpdatePanel runat="server" ID="up">
                <ContentTemplate>
                    <div class="section-pannel">
                        <div class="row">
                            <div class="col-md-12">
                                <div>
                                    <h4>Upload Product Excel</h4>
                                </div>
                                <hr />
                                <div class="row">
                                    <asp:Label runat="server" Visible="false" ID="lblnsccode"></asp:Label>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <label class="form-control-label">
                                                Select File Type</label>&nbsp;
                                <asp:RadioButtonList ID="rbtypefile" runat="server" CssClass="" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="1">Excel Format</asp:ListItem>
                                    <%--<asp:ListItem Value="2" style="margin-left: 10px;">CSV Format</asp:ListItem>--%>
                                </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                NATO SUPPLY GROUP <span class="mandatory">*</span>
                                                <span data-toggle="tooltip" class="fa fa-question" title="Number in bracket indicates NATO SUPPLY GROUP"></span>
                                            </label>
                                            <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" Style="text-transform: uppercase !important;" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>NATO SUPPLY CLASS<span class="mandatory">*</span></label>
                                            <span data-toggle="tooltip" class="fa fa-question" title="Number in bracket indicates NATO SUPPLY CLASS"></span>
                                            <asp:DropDownList runat="server" ID="ddlsubcategory" AutoPostBack="True" TabIndex="2" class="form-control" Style="text-transform: uppercase !important;" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Item Name Code<span class="mandatory">*</span></label>
                                            <span data-toggle="tooltip" class="fa fa-question" title="Item code indicate item name code in NSN"></span>
                                            <asp:DropDownList runat="server" ID="ddllevel3product" AutoPostBack="True" TabIndex="3" Style="text-transform: uppercase !important;" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>OEM Country</label>
                                            <span class="mandatory">*</span>
                                            <asp:DropDownList ID="ddlcountry" runat="server" Height="35px" TabIndex="11" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblenduser" Text="End User"></asp:Label>
                                            <span class="mandatory">*</span>
                                            <div class="clearfix"></div>
                                            <asp:CheckBoxList ID="ddlenduser" runat="server" RepeatColumns="7" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>DEFENCE PLATFORM<span class="mandatory">* </span></label>
                                            <asp:DropDownList runat="server" ID="ddlplatform" AutoPostBack="True" TabIndex="13" Style="text-transform: uppercase !important;" class="form-control" OnSelectedIndexChanged="ddlplatform_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 8px;">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblNomenclature" Text="NAME OF DEFENCE PLATFORM"></asp:Label><span class="mandatory"> *</span>
                                            <asp:DropDownList runat="server" ID="ddlnomnclature" class="form-control" Style="text-transform: uppercase !important;" TabIndex="14" />
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>PRODUCT (INDUSTRY DOMAIN)<span class="mandatory">*</span></label>
                                            <span data-toggle="tooltip" class="fa fa-question" title="If you not display your category in this section, please add in Category Master >> Category Dropdown"></span>
                                            <asp:DropDownList runat="server" ID="ddltechnologycat" class="form-control" TabIndex="15" Style="text-transform: uppercase !important;" AutoPostBack="True" OnSelectedIndexChanged="ddltechnologycat_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>PRODUCT (INDUSTRY SUB DOMAIN)<span class="mandatory">*</span></label>
                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Item Level 1, if you not see product level 2 please add in Category master >> level 2 "></span>
                                            <asp:DropDownList runat="server" ID="ddlsubtech" class="form-control" TabIndex="16" Style="text-transform: uppercase !important;"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group" runat="server">
                                            <h5>Make in India Category&nbsp; <span class="mandatory">*</span></h5>
                                            <asp:RadioButtonList ID="rbIgCategory" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <h5>Quality Assurance Agency <span class="mandatory">*</span></h5>
                                            <asp:CheckBoxList ID="chkQAA" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix pb15"></div>
                                <div class="form-group row">
                                    <label class="col-sm-3 form-control-label">
                                        Select Excel File</label>
                                    <div class="col-sm-9">
                                        <asp:FileUpload runat="server" class="form-control" ID="fuexcel" />
                                    </div>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="col-sm-12">
                                    <asp:Button ID="btnProduct" runat="server" class="btn btn-primary btn-sm pull-right"
                                        Text="Upload Product" OnClick="btnexcelProduct_Click" Visible="false" />
                                    <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-sm pull-right"
                                        Text="Upload Product" OnClick="Button1_Click" />
                                </div>
                                <div class="clearfix mt10"></div>
                                <div>
                                    <p>1. Company/Division/Unit name should be correct and as per registration.</p>
                                    <p>2. All mandatory dropdown like (NSN GROUP,NSN GROUP CLASS,ITEM CODE,HS CODE etc) should be correct and as per master given data in website.</p>
                                    <p>
                                        3. Please download sample before upload product excel file &nbsp;                           
                                <a href="http://srijandefence.gov.in/Upload/Sample%20Format.xlsx" target="_blank" class="fa fa-download"></a>
                                    </p>
                                    <p>4. Headers are given in sample excel, please add rows as per your data.</p>
                                    <p>
                                        5.Always check excel sheet name should be Sheet1.
                                    </p>
                                    <p>
                                        <b>6.Product with same ItemName earlier enter in portal will not be added.</b>
                                    </p>
                                    <p>
                                        <b>7.EOI Start or End Date Format (1990-Jan-01).</b>
                                    </p>
                                    <p>
                                        <b>8.If - <b>IMPORTED VALUE IN RS LAKH (QTY*PRICE)</b> is blank,  record will not insert that year.</b>
                                    </p>
                                    <p>
                                        <b>9.Do not fill <b>IMPORTED VALUE IN RS LAKH (QTY*PRICE)</b> if you does not want that year details to fill.</b>
                                    </p>
                                    <p>
                                        <b>10.If you are facing any error while uploading excel feel free to contact us.</b>
                                    </p>
                                    <div class="clearfix mt10"></div>
                                    <div class="col-sm-12">
                                        <label class="form-control-label">
                                            <h4>
                                                <asp:Label ID="lblpathname" runat="server" class="label label-info"></asp:Label>
                                            </h4>
                                        </label>
                                        <label class="form-control-label">
                                            <h4>
                                                <asp:Label ID="lblRowCount" runat="server" class="label label-info"></asp:Label>
                                            </h4>
                                        </label>
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <div runat="server" id="diverror">
                                    </div>
                                    <div class="clearfix mt10"></div>
                                </div>
                            </div>
                        </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Button1" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
