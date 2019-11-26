<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="Admin_AddProduct" ValidateRequest="false" MasterPageFile="~/Admin/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <!----------------------------------jquery Show image on load------------------------------------------------>
    <style>
        .gallery img {
            width: 100px;
            margin-right: 10px;
            border: 2px solid #333;
        }

        .unselectable {
            width: 100% !important;
            height: 200px !important;
        }

        .ajax__html_editor_extender_texteditor {
            height: 200px !important;
        }
    </style>
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
        }
    </script>

    <!-------------------------------------------image show end------------------------------->
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:hiddenfield id="hidCompanyRefNo" runat="server" />
    <asp:hiddenfield id="hidType" runat="server" />
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
            <div class="clearfix" style="margin-bottom: 10px;">
            </div>
            <div class="row">
                <p style="position: absolute; right: 35px;">Mark with <span class="mandatory">*</span> is manadatory field.</p>
                <asp:updatepanel runat="server" id="updrop">
                    <ContentTemplate>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="form-label">Select Company/Organization</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-cascade-control form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="divlblselectdivison">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="form-label">Select Division/Plant</asp:Label>
                                <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-cascade-control form-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="divlblselectunit">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="form-label">Select Unit</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-cascade-control form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:updatepanel>
            </div>
            <div class="tabing-section">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#pd">Item description</a></li>
                    <li><a data-toggle="tab" href="#pimg">Item Specification</a></li>
                    <li><a data-toggle="tab" href="#qpt">Estimated Quantity</a></li>
                    <li><a data-toggle="tab" href="#test" runat="server" visible="false">Testing & Certification</a></li>
                    <li><a data-toggle="tab" href="#spd" runat="server" visible="false">Technical & Financial Support</a></li>
                    <li><a data-toggle="tab" href="#tnd" runat="server" visible="false">Tender and EOI</a></li>
                    <li><a data-toggle="tab" href="#cd">Contact</a></li>
                </ul>
                <div class="tab-content">
                    <div id="pd" class="tab-pane fade in active">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="section-pannel">
                                        <asp:updatepanel runat="server" id="upproduct" updatemode="Conditional">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>
                                                                NSN GROUP <span class="mandatory">*</span>
                                                                <span data-toggle="tooltip" class="fa fa-question" title="Number in bracket indicates NSN Group"></span>
                                                            </label>
                                                            <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" Style="text-transform: uppercase !important;" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>NSN GROUP CLASS<span class="mandatory">*</span></label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="Number in bracket indicates NSN Group class"></span>
                                                            <asp:DropDownList runat="server" ID="ddlsubcategory" AutoPostBack="True" TabIndex="2" class="form-control" Style="text-transform: uppercase !important;" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>
                                                                Item Code</label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="Item code indicate item name code in NSN"></span>
                                                            <div class="col-md-11 row">
                                                                <asp:DropDownList runat="server" ID="ddllevel3product" AutoPostBack="True" TabIndex="3" Style="text-transform: uppercase !important;" class="form-control" OnSelectedIndexChanged="ddllevel3product_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-1 row">
                                                                <asp:LinkButton ID="lblviewitemcode" runat="server" class="fa fa-eye mr10 mt10" Visible="false" OnClick="lblviewitemcode_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        <div class="form-group">
                                                            <div class="col-md-3 padding_0" style="margin-top: 30px;">
                                                                <label>Nato Stock Number (NSN)</label>
                                                            </div>
                                                            <div class="col-sm-3 padding_0">
                                                                <label style="font-size: 14px !important;">
                                                                    NSC Code (4 digit)
                                                                        <span data-toggle="tooltip" class="fa fa-question" title="NSC Code = NSN Group (2 digit) + NSN Group Class (2 digit)"></span>
                                                                </label>
                                                                <asp:TextBox runat="server" ID="txtnsccode" ReadOnly="True" MaxLength="4" CssClass="form-cascade-control form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-6 padding_0">
                                                                <label>
                                                                    NIIN Code (9-digit)
                                                                    <span data-toggle="tooltip" class="fa fa-question" title="Please enter if NIIN code is available"></span>
                                                                </label>
                                                                <asp:TextBox runat="server" ID="txtniincode" TabIndex="4" MaxLength="9" class="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="clearfix mt10"></div>
                                                            <div class="form-group">
                                                                <label>Search keywords (To add more than one search keyword please use comma(,)) <span class="mandatory">*</span></label>
                                                                <asp:TextBox runat="server" ID="txtsearchkeyword" MaxLength="50" required="" TabIndex="18" class="form-control"></asp:TextBox>
                                                                <div class="clearfix" style="margin-top: 5px;"></div>
                                                                <span>(Max length 50 words only)</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Item brief description</label>
                                                            <span class="mandatory">* (Editable)</span>  <span data-toggle="tooltip" class="fa fa-question" title="If item description is not relevant, edit the item description."></span>
                                                            <asp:TextBox runat="server" ID="txtproductdescription" required="" Height="100px" MaxLength="250" TabIndex="5" class="form-control"></asp:TextBox>
                                                            <div class="clearfix" style="margin-top: 5px;"></div>
                                                            <span>(Max length 250 words only)</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Upload document related to item </label>
                                                            <span class="mandatory">(only pdf file of maximum 5 Mb can be uploaded.)</span>
                                                            <asp:FileUpload runat="server" ID="fuitemdescriptionfile" CssClass="form-control" TabIndex="6" />
                                                            <div class="clearfix mt5"></div>
                                                            <asp:Label runat="server" ID="lblfuitemdescriptionfile" Visible="False"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Item Image</label>
                                                            <span class="mandatory">(only .jpeg, .png, .jpg files of each max 5 Mb.(max 4 files))</span>
                                                            <div class="fr">
                                                                <asp:FileUpload ID="fuimages" runat="server" CssClass="uploadimage form-control" AllowMultiple="true" TabIndex="7" />
                                                            </div>
                                                        </div>
                                                        <!-------uplode photo----------->
                                                        <div class="gallery"></div>
                                                        <br />
                                                    </div>
                                                    <div class="clearfix mt5"></div>
                                                    <div class="col-sm-6"></div>
                                                    <div class="col-sm-6">
                                                        <div runat="server" id="divimgdel" visible="False">
                                                            <div class="row">
                                                                <asp:DataList runat="server" ID="dlimage" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemCommand="dlimage_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <div class="col-sm-3" style="border: 1px solid #ccc">
                                                                            <asp:Image runat="server" ID="imgprodimage" class="image img-responsive img-rounded" Height="90px" Width="90" src='<%#Eval("ImageName") %>' />
                                                                            <div class="clearfix"></div>
                                                                            <asp:LinkButton runat="server" ID="lblremoveimg" class="fa fa-trash text-center imgdel" CommandName="removeimg" CommandArgument='<%#Eval("ImageId") %>'></asp:LinkButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:updatepanel>
                                        <asp:updateprogress id="UpdateProgress3" runat="server" associatedupdatepanelid="upproduct">
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
                                        </asp:updateprogress>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <label>
                                                        HSN Code (4-8 digit) <%--<span class="mandatory">*</span>--%> <a href="https://www.cbic-gst.gov.in/gst-goods-services-rates.html" target="_blank">(For finding hsn code please click here to get redirected to gst website.)<span data-toggle="tooltip" class="fa fa-question" title="HSN Code (4-8 digit ) and Link for find hsn code"></span></a>
                                                    </label>
                                                    <asp:textbox runat="server" id="txthsncodereadonly" tabindex="8" maxlength="8" class="form-control"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>DPSU Part Number</label>
                                                    <asp:textbox runat="server" id="txtdpsupartnumber" tabindex="9" class="form-control"></asp:textbox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="section-pannel" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-md-4" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>
                                                        Sub heading No.(HSN) (digit) (Hidden)
                                                     <span data-toggle="tooltip" class="fa fa-question" title="Please enter if HSN code is available"></span>
                                                    </label>
                                                    <asp:textbox runat="server" id="txthsncode" maxlength="9" class="form-control"></asp:textbox>
                                                </div>
                                            </div>
                                            <asp:updatepanel runat="server" id="UpdatePanel7">
                                                <ContentTemplate>
                                                    <div class="col-md-3" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label>HS Code (Hidden)</label>
                                                            <span class="mandatory">*</span>
                                                            <asp:DropDownList runat="server" ID="ddlHSNCode" class="form-control" Height="35px" Style="text-transform: uppercase !important;"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label>HS Chapter</label>
                                                            <span class="mandatory">*</span>
                                                            <asp:DropDownList runat="server" ID="ddlhschapter" AutoPostBack="true" OnSelectedIndexChanged="ddlhschapter_SelectedIndexChanged" class="form-control" Height="35px" Style="text-transform: uppercase !important;"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label>HS Heading No</label>
                                                            <span class="mandatory">*</span>
                                                            <asp:DropDownList runat="server" ID="ddlhsncodelev1" AutoPostBack="true" OnSelectedIndexChanged="ddlhsncodelev1_SelectedIndexChanged" class="form-control" Height="35px" Style="text-transform: uppercase !important;"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label>Description</label>
                                                            <span class="mandatory">*</span>
                                                            <asp:DropDownList runat="server" ID="ddlhsncodelevel2" AutoPostBack="true" OnSelectedIndexChanged="ddlhsncodelevel2_SelectedIndexChanged" class="form-control" Height="35px" Style="text-transform: uppercase !important;"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label>HSN Code</label>
                                                            <span class="mandatory">*</span>
                                                            <asp:DropDownList runat="server" ID="ddlhsncodelevel3" AutoPostBack="true" OnSelectedIndexChanged="ddlhsncodelevel3_SelectedIndexChanged" class="form-control" Height="35px" Style="text-transform: uppercase !important;"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>


                                                    <div class="col-sm-3" runat="server" visible="false">
                                                        <label style="font-size: 14px !important;">
                                                            HS Code (4 digit)
                                                                        <span data-toggle="tooltip" class="fa fa-question" title="HS Code 4 digit"></span>
                                                        </label>
                                                        <asp:TextBox runat="server" ID="txthscodereadonly" ReadOnly="True" MaxLength="4" CssClass="form-cascade-control form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3" runat="server" visible="false">
                                                    </div>

                                                </ContentTemplate>
                                            </asp:updatepanel>
                                            <asp:updateprogress id="UpdateProgress10" runat="server" associatedupdatepanelid="upproduct">
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
                                            </asp:updateprogress>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Part Number</label>
                                                    <asp:textbox runat="server" id="txtoempartnumber" tabindex="9" class="form-control"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Name</label>
                                                    <asp:textbox runat="server" id="txtoemname" tabindex="10" class="form-control"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Country</label>
                                                    <span class="mandatory">*</span>
                                                    <asp:dropdownlist id="txtcountry" runat="server" height="35px" tabindex="11" class="form-control">
                                                    </asp:dropdownlist>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-4" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>End User Part Number</label>
                                                    <asp:textbox runat="server" id="txtenduserpartnumber" class="form-control"></asp:textbox>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:label runat="server" id="lblenduser" text="End User"></asp:label>
                                                    <span class="mandatory">*</span>
                                                    <div class="clearfix"></div>
                                                    <asp:listbox runat="server" id="ddlenduser" selectionmode="Multiple" style="text-transform: uppercase !important;" class="form-control ui fluid dropdown" tabindex="12"></asp:listbox>
                                                </div>
                                            </div>
                                            <asp:updatepanel runat="server" id="UpdatePanel6">
                                                <ContentTemplate>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>DEFENCE PLATFORM<span class="mandatory">*</span></label>
                                                            <asp:DropDownList runat="server" ID="ddlplatform" AutoPostBack="True" TabIndex="13" Style="text-transform: uppercase !important;" class="form-control" OnSelectedIndexChanged="ddlplatform_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" style="margin-top: 8px;">
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblNomenclature" Text="NAME OF DEFENCE PLATFORM"></asp:Label><span class="mandatory"> *</span>
                                                            <asp:DropDownList runat="server" ID="ddlnomnclature" class="form-control" Style="text-transform: uppercase !important;" TabIndex="14" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:updatepanel>
                                            <asp:updateprogress id="UpdateProgress9" runat="server" associatedupdatepanelid="UpdatePanel6">
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
                                            </asp:updateprogress>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <asp:updatepanel runat="server" id="uptechnology">
                                                <ContentTemplate>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>PRODUCT (INDUSTRY DOMAIN)<span class="mandatory">*</span></label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="If you not display your category in this section, please add in Category Master >> Category Dropdown"></span>
                                                            <asp:DropDownList runat="server" ID="ddltechnologycat" class="form-control" TabIndex="15" Style="text-transform: uppercase !important;" AutoPostBack="True" OnSelectedIndexChanged="ddltechnologycat_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>PRODUCT (INDUSTRY SUB DOMAIN)<span class="mandatory">*</span></label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Item Level 1, if you not see product level 2 please add in Category master >> level 2 "></span>
                                                            <asp:DropDownList runat="server" ID="ddlsubtech" class="form-control" TabIndex="16" Style="text-transform: uppercase !important;" AutoPostBack="True" OnSelectedIndexChanged="ddlsubtech_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>PRODUCT (INDUSTRY 2nd SUB DOMAIN)</label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Item Level 2, if you not see product level 3 please add in Category master >> level 3 "></span>
                                                            <asp:DropDownList runat="server" ID="ddltechlevel3" TabIndex="17" Style="text-transform: uppercase !important;" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>

                                                </ContentTemplate>
                                                <Triggers>
                                                </Triggers>
                                            </asp:updatepanel>
                                            <asp:updateprogress id="UpdateProgress4" runat="server" associatedupdatepanelid="uptechnology">
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
                                            </asp:updateprogress>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:updatepanel runat="server" id="UpdatePanel4">
                                                    <ContentTemplate>
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label class="mlive-status-box productalreadylabel">
                                                                    Item already indigenized :                                                               
                                                                <asp:RadioButtonList runat="server" ID="rbisindinised" RepeatColumns="2" TabIndex="19" RepeatLayout="Flow"
                                                                    RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbisindinised_CheckedChanged ">
                                                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                                    <asp:ListItem Value="Y" class="yes">Yes</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                </label>

                                                                <div class="clearfix" style="margin-top: 10px;"></div>
                                                                <div runat="server" class="row" id="divisIndigenized" visible="false">
                                                                    <div class="col-sm-4">
                                                                        <label>Enter Manufacturer name</label>
                                                                        <asp:TextBox runat="server" ID="txtmanufacturename" TabIndex="20" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <label>Address</label>
                                                                        <asp:TextBox runat="server" ID="txtmanifacaddress" MaxLength="250" TabIndex="21" class="form-control"></asp:TextBox>
                                                                        <div class="clearfix" style="margin-top: 5px;"></div>
                                                                        <span>(Max length 250 words only)</span>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <label>Year of Indiginization</label>
                                                                        <asp:DropDownList runat="server" ID="ddlyearofindiginization" Height="35px" TabIndex="22" class="form-control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="clearfix mt10"></div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="checkbox-box productalreadylabel">
                                                                    Is Item imported in last 5 years?
                                                                </label>
                                                                <asp:RadioButtonList runat="server" ID="rbproductImported" RepeatColumns="2" TabIndex="23" RepeatLayout="Flow"
                                                                    RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbproductImported_CheckedChanged ">
                                                                    <asp:ListItem Value="N" Selected="True" style="margin-left: 5px;">No</asp:ListItem>
                                                                    <asp:ListItem Value="Y" class="yes" style="margin-left: 10px;">Yes</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div class="clearfix mt5"></div>
                                                            <div runat="server" id="divyearofimportYes" visible="False">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="form-group">
                                                                            <label>Year of import</label>
                                                                            <div class="clearfix"></div>
                                                                            <asp:CheckBoxList runat="server" class="mr-3 ProddisplayInline" ID="chklistimportyearfive" RepeatDirection="Horizontal" TabIndex="24" RepeatLayout="Flow" RepeatColumns="5">
                                                                                <asp:ListItem Value="2019-20" Text="2019-20"></asp:ListItem>
                                                                                <asp:ListItem Value="2018-19" Text="2018-19"></asp:ListItem>
                                                                                <asp:ListItem Value="2017-18" Text="2017-18"></asp:ListItem>
                                                                                <asp:ListItem Value="2016-17" Text="2016-17"></asp:ListItem>
                                                                                <asp:ListItem Value="2015-16" Text="2015-16"></asp:ListItem>
                                                                            </asp:CheckBoxList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label>Remarks</label>
                                                                <asp:TextBox runat="server" ID="txtremarksyearofimportyes" Height="70px" TabIndex="25" MaxLength="250" class="form-control"></asp:TextBox>
                                                                <div class="clearfix" style="margin-top: 5px;"></div>
                                                                <span>(Max length 250 words only)</span>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                                <asp:updateprogress id="UpdateProgress7" runat="server" associatedupdatepanelid="UpdatePanel4">
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
                                                </asp:updateprogress>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="pimg" class="tab-pane">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Features & Details</label><span class="mandatory"> (Editable)</span>
                                                    <asp:textbox runat="server" id="txtfeaturesanddetails" style="background-color: #fff !important;" tabindex="1"
                                                        width="1000" height="70px" placeholder="Ductile,Tensile,Lusture" maxlength="250"></asp:textbox>
                                                    <asp:htmleditorextender id="HtmlEditorExtender1" runat="server" targetcontrolid="txtfeaturesanddetails">
                                                    </asp:htmleditorextender>
                                                    <div class="clearfix" style="margin-top: 35px;"></div>
                                                    <span>(Max length 250)</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label><strong>Type of item specification</strong></label>
                                                    <div class="clearfix mt5"></div>
                                                    <asp:radiobuttonlist id="rbitemspecification" runat="server" repeatcolumns="1" tabindex="2" repeatdirection="Horizontal" repeatlayout="Flow">
                                                        <asp:ListItem Value="Specification are available in open source/DPSU spec">Specification are available in open source/DPSU spec</asp:ListItem>
                                                        <asp:ListItem Value="Items with OEM specification">Items with OEM specification</asp:ListItem>
                                                        <asp:ListItem Value="Items where no specification are available">Items where no specification are available</asp:ListItem>
                                                    </asp:radiobuttonlist>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:updatepanel runat="server" id="UpdatePanel5">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <label>Item Information</label>
                                                            <div class="clearfix mt5"></div>
                                                            <div class="table table-responsive">
                                                                <table border="0" cellpadding="0" cellspacing="0" tabindex="3" class="gridFormTable" style="border-collapse: collapse; width: 100%">
                                                                    <tr>
                                                                        <td style="width: 320px">
                                                                            <label>Name of Specification</label>
                                                                            <asp:TextBox ID="txtNameOfSpecificationAdd" CssClass="form-control" runat="server" />
                                                                        </td>
                                                                        <td style="width: 320px">
                                                                            <label>Dimension</label>
                                                                            <asp:TextBox ID="TxtValueProdAdd" runat="server" CssClass="form-control" />
                                                                        </td>
                                                                        <td style="width: 320px">
                                                                            <label>Unit</label>
                                                                            <asp:TextBox ID="txtUnitProdAdd" runat="server" CssClass="form-control" />
                                                                        </td>
                                                                        <td style="width: 320px">
                                                                            <asp:LinkButton ID="btnAdd" runat="server" Style="margin-top: 18px;" CssClass="btn btn-primary pull-right" Text="Add" OnClick="Insert"></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <asp:GridView ID="gvProductInformation" runat="server" AutoGenerateColumns="false" Class="table table-hover manage-user gridFormTableResult" OnRowDataBound="OnRowDataBound"
                                                                    DataKeyNames="ProdInfoId" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" PageSize="5" AllowPaging="true" OnPageIndexChanging="OnPaging"
                                                                    OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added."
                                                                    Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name of Specification (Editable)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNameofspec" runat="server" Text='<%# Eval("NameofSpec") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtNameofspeci" CssClass="form-control" runat="server" Text='<%# Eval("NameofSpec") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Dimention (Editable)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblvalueProd" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtValueProd" CssClass="form-control" runat="server" Text='<%# Eval("Value") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit (Editable)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUnitProd" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtUnitProd" CssClass="form-control" runat="server" Text='<%# Eval("Unit") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:CommandField ButtonType="Link" ShowEditButton="true" HeaderText="Action" ShowDeleteButton="true" />
                                                                    </Columns>
                                                                </asp:GridView>

                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:updatepanel>
                                                <asp:updateprogress id="UpdateProgress8" runat="server" associatedupdatepanelid="UpdatePanel5">
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
                                                </asp:updateprogress>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Additional Information</label><span class="mandatory"> (Editable)</span>
                                                    <asp:textbox runat="server" id="txtadditionalinfo" height="70px" tabindex="4" class="form-control" placeholder="Warranty,Guarantee"></asp:textbox>
                                                    <div class="clearfix" style="margin-top: 5px;"></div>
                                                    <span>(Max length 250)</span>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="qpt" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="section-pannel">
                                        <asp:updatepanel runat="server" id="UpdatePanel1">
                                            <ContentTemplate>
                                                <div class="table table-responsive">
                                                    <table border="0" cellpadding="0" class="gridFormTable EstimateGridview" tabindex="1" cellspacing="0" style="border-collapse: collapse; width: 100%">
                                                        <tr>
                                                            <td>
                                                                <label>Year:</label>
                                                                <asp:DropDownList ID="ddlYearEstimate" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">2019-20</asp:ListItem>
                                                                    <asp:ListItem Value="2">2020-21</asp:ListItem>
                                                                    <asp:ListItem Value="3">2021-22</asp:ListItem>
                                                                    <asp:ListItem Value="4">2022-23</asp:ListItem>
                                                                    <asp:ListItem Value="5">2023-24</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <label>Estimated Quantity</label>
                                                                <asp:TextBox ID="txtestimateQuantity" runat="server" CssClass="form-control" />
                                                            </td>
                                                            <td>
                                                                <label>Measuring Unit</label>
                                                                <asp:DropDownList ID="ddlMeasuringUnit" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                    <asp:ListItem Value="Centimeter">Centimeter</asp:ListItem>
                                                                    <asp:ListItem Value="Gram">Gram</asp:ListItem>
                                                                    <asp:ListItem Value="Inch">Inch</asp:ListItem>
                                                                    <asp:ListItem Value="Kg">Kg</asp:ListItem>
                                                                    <asp:ListItem Value="Mtr">Mtr</asp:ListItem>
                                                                    <asp:ListItem Value="Inch">Inch</asp:ListItem>
                                                                    <asp:ListItem Value="Number">Number</asp:ListItem>
                                                                    <asp:ListItem Value="Ounce">Ounce</asp:ListItem>
                                                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                    <asp:ListItem Value="Pound">Pound</asp:ListItem>
                                                                    <asp:ListItem Value="Ton">Ton</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td runat="server" visible="false">
                                                                <label>Estimated Price / LPP (Only Numbers)</label>
                                                                <asp:TextBox ID="txtestimatePriceLLp" runat="server" CssClass="form-control" />
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="btnAddEstimate" runat="server" Style="margin-top: 18px;" CssClass="btn btn-primary" Text="Add" OnClick="btnAddEstimate_Click"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:GridView ID="GvEstimateQuanPrice" runat="server" CssClass="table table-hover EstimateGridviewResult manage-user" Style="width: 100%" AutoGenerateColumns="false" OnRowDataBound="GvEstimateQuanPrice_RowDataBound"
                                                        DataKeyNames="ProdQtyPriceId" OnRowEditing="GvEstimateQuanPrice_RowEditing" OnRowCancelingEdit="GvEstimateQuanPrice_RowCancelingEdit" PageSize="8" AllowPaging="true" OnPageIndexChanging="GvEstimateQuanPrice_Paging"
                                                        OnRowUpdating="GvEstimateQuanPrice_RowUpdating" OnRowDeleting="GvEstimateQuanPrice_RowDeleting" EmptyDataText="No records has been added."
                                                        Width="450">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Year (Editable)" ItemStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblYear" runat="server" Text='<%# Eval("FYear") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlYearEstimateGrid" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                        <asp:ListItem Value="1">2019-20</asp:ListItem>
                                                                        <asp:ListItem Value="2">2019-21</asp:ListItem>
                                                                        <asp:ListItem Value="3">2019-22</asp:ListItem>
                                                                        <asp:ListItem Value="4">2019-23</asp:ListItem>
                                                                        <asp:ListItem Value="5">2019-24</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estimated Quantity (Editable)" ItemStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblestimateQuantityGrid" runat="server" Text='<%# Eval("EstimatedQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtEstimateQuantityGrid" runat="server" CssClass="form-control">                                                                        
                                                                    </asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Measuring Unit (Editable)" ItemStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMeasuringUnitGrid" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlEstimateUnit" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Centimeter">Centimeter</asp:ListItem>
                                                                        <asp:ListItem Value="Gram">Gram</asp:ListItem>
                                                                        <asp:ListItem Value="Inch">Inch</asp:ListItem>
                                                                        <asp:ListItem Value="Kg">Kg</asp:ListItem>
                                                                        <asp:ListItem Value="Mtr">Mtr</asp:ListItem>
                                                                        <asp:ListItem Value="Inch">Inch</asp:ListItem>
                                                                        <asp:ListItem Value="Number">Number</asp:ListItem>
                                                                        <asp:ListItem Value="Ounce">Ounce</asp:ListItem>
                                                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                        <asp:ListItem Value="Pound">Pound</asp:ListItem>
                                                                        <asp:ListItem Value="Ton">Ton</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estimated Price / LPP (Only Numbers) (Editable)" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblestimatePriceLLpGrid" runat="server" Text='<%# Eval("EstimatedQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtEstimatePriceLLpGrid" runat="server" CssClass="form-control">                                                                        
                                                                    </asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ButtonType="Link" ShowEditButton="true" ItemStyle-Width="15%" ShowDeleteButton="true" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:updatepanel>
                                        <asp:updateprogress id="UpdateProgress2" runat="server" associatedupdatepanelid="UpdatePanel1">
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
                                        </asp:updateprogress>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>PROCURMENT CATEGORY</label>
                                                    <div class="clearfix"></div>
                                                    <asp:gridview runat="server" id="gvprocurmentcategory" tabindex="2" class="table-responsive table" autogeneratecolumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" ID="chkprocurmentcategory" />
                                                                    <asp:HiddenField runat="server" ID="hfproccateid" Value='<%#Eval("SCategoryId") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField runat="server" DataField="SCategoryName" />
                                                        </Columns>
                                                    </asp:gridview>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div id="divmake2status" class="col-md-12 row" runat="server" style="display: none">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Type</label>
                                                        <asp:dropdownlist id="ddlteneoi" runat="server" cssclass="form-control">
                                                            <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Preparing technical specifications</asp:ListItem>
                                                            <asp:ListItem Value="2">Tender floated</asp:ListItem>
                                                            <asp:ListItem Value="3">EOI floated</asp:ListItem>
                                                            <asp:ListItem Value="4">Capability & capacity asessment of  the vendors</asp:ListItem>
                                                            <asp:ListItem Value="5">Prototype development</asp:ListItem>
                                                            <asp:ListItem Value="6">Trial & test of prototype</asp:ListItem>
                                                            <asp:ListItem Value="7">Price bid opening</asp:ListItem>
                                                            <asp:ListItem Value="8">Order placement</asp:ListItem>
                                                        </asp:dropdownlist>
                                                    </div>
                                                </div>
                                                <div runat="server" id="divtimedateurl" style="display: none;">
                                                    <div class="col-md-3">
                                                        <div class="form-group ">
                                                            <label>Status</label>
                                                            <asp:dropdownlist runat="server" id="ddlstatus" class="form-control" tabindex="1">
                                                                <asp:ListItem Value="Not Floated" Selected="True">Not Floated</asp:ListItem>
                                                                <asp:ListItem Value="Archive">Archive</asp:ListItem>
                                                                <asp:ListItem Value="Live">Live</asp:ListItem>
                                                                <asp:ListItem Value="To be Floated shortly">To be Floated shortly</asp:ListItem>
                                                            </asp:dropdownlist>
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="extimedatevisible" style="display: none;">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>Last date of submission</label>
                                                                <asp:textbox runat="server" id="txtdate" type="date" tabindex="3" class="form-control inputbox"></asp:textbox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>URL</label>
                                                                <asp:textbox runat="server" id="txtendorurl" tabindex="4" class="form-control"></asp:textbox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix mt5"></div>
                                            <div id="Div1" class="col-md-12" runat="server">
                                                <div class="form-group">
                                                    <asp:label runat="server" id="Label1" cssclass="form-label " text="PROCURMENT CATEGORY REMARK"></asp:label>
                                                    <asp:textbox runat="server" id="txtremarksprocurmentCategory" tabindex="3" maxlength="250" height="70px" class="form-control"></asp:textbox>
                                                    <div class="clearfix" style="margin-top: 5px;"></div>
                                                    <span>(Max length 250 words only)</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="cd" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <asp:updatepanel runat="server" id="UpdatePanel2">
                                        <ContentTemplate>
                                            <div class="section-pannel" runat="server" id="divnodal">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h4 class="page-header secondary">Contact Detail 1  </h4>
                                                        <div class="form-group contactD1Select">
                                                            <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail" class="form-control" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="contactFormRow" runat="server" id="contactpanel1" visible="False">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Employee Code</label>
                                                                <asp:TextBox runat="server" ID="txtempcode" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Designation</label>
                                                                <asp:TextBox runat="server" ID="txtDesignation" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>E-Mail ID</label>
                                                                <asp:TextBox runat="server" ID="txtNEmailId" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Mobile Number</label>
                                                                <asp:TextBox runat="server" ID="txtmobnodal" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Phone Number</label>
                                                                <asp:TextBox runat="server" ID="txtNTelephone" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Fax</label>
                                                                <asp:TextBox runat="server" ID="txtNFaxNo" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="section-pannel" runat="server" id="divnodal2" visible="False">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h4 class="page-header secondary">Contact Detail 2</h4>
                                                        <div class="form-group select-box">
                                                            <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail2" class="form-control" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail2_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="contactFormRow" runat="server" id="contactpanel2" visible="False">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Employee Code</label>
                                                                <asp:TextBox runat="server" ID="txtempcode2" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Designation</label>
                                                                <asp:TextBox runat="server" ID="txtdesignationnodal2" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>E-Mail ID</label>
                                                                <asp:TextBox runat="server" ID="txtNEmailId2" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Mobile Number</label>
                                                                <asp:TextBox runat="server" ID="txtmobnodal2" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Phone Number</label>
                                                                <asp:TextBox runat="server" ID="txtNTelephone2" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Fax</label>
                                                                <asp:TextBox runat="server" ID="txtNFaxNo2" name="" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:updatepanel>
                                    <asp:updateprogress id="UpdateProgress" runat="server" associatedupdatepanelid="UpdatePanel2">
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
                                    </asp:updateprogress>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:panel id="Panel1" runat="server" defaultbutton="btnsubmitpanel1">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:UpdatePanel runat="server" ID="UPSUBMIT">
                                        <ContentTemplate>
                                            <div runat="server" id="myhtmldiv"></div>
                                            <asp:Button runat="server" ID="btnsubmitpanel1" CssClass="btn btn-primary pull-right" TabIndex="72" Text="Save" OnClick="btnsubmitpanel1_Click" OnClientClick="return confirm('Are you sure you want to save this product?');" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger runat="server" ControlID="btnsubmitpanel1" />
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
                    </asp:panel>
                    <asp:hiddenfield runat="server" id="hfprodid" />
                    <asp:hiddenfield runat="server" id="hfprodrefno" />
                    <asp:hiddenfield runat="server" id="hfcomprefno" />
                    <div class="modal fade" id="changePass" role="dialog">
                        <div class="modal-dialog" style="width: 1200px; z-index: 9999999999;">
                            <asp:updatepanel id="upn" runat="server" childrenastriggers="true">
                                <ContentTemplate>
                                    <div class="modal-content">
                                        <div class="modal-header modal-header1">
                                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Item Detail</h4>
                                        </div>
                                        <form class="form-horizontal changepassword" role="form">
                                            <div class="modal-body">
                                                <div class="tabing-section">
                                                    <ul class="nav nav-tabs">
                                                        <li class="active"><a data-toggle="tab" href="#LGY">FIIG - Yes</a></li>
                                                        <li><a data-toggle="tab" href="#LGN">FIIG - NO</a></li>
                                                    </ul>
                                                    <div class="tab-content">
                                                        <div id="LGY" class="tab-pane fade in active">
                                                            <asp:Panel ID="pan" runat="server" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="table-wraper table-responsive">
                                                                            <asp:GridView ID="gvproditemdetail" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
           responsive no-wrap table-hover manage-user Grid"
                                                                                OnRowCreated="gvproductItem_RowCreated" AutoGenerateColumns="false">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Sr.No">
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                            <asp:BoundField runat="server" DataField="SCategoryName" HeaderText="MRC Title" />
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtinfonsnfig" runat="server"  required="" MaxLength="250" Placeholder="Remarks (Max Length 250)" CssClass="form-control"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            <div class="clearfix mt10"></div>
                                                                            <asp:TextBox ID="txtfiigtype" runat="server" CssClass="form-control" required="" MaxLength="500" TextMode="MultiLine" Height="100px" placeholder="Enter brief remarks here (Max Length 500)"></asp:TextBox>
                                                                            <div class="clearfix mt10"></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div id="LGN" class="tab-pane">
                                                            <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="table-wraper table-responsive">
                                                                            <asp:GridView ID="GridView1" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
           responsive no-wrap table-hover manage-user Grid"
                                                                                OnRowCreated="gvproductItem_RowCreated" AutoGenerateColumns="false">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Sr.No">
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                        <asp:BoundField runat="server" DataField="SCategoryName" HeaderText="MRC Title" />
                                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtremNoFiig" runat="server"  required="" MaxLength="250" Placeholder="Remarks (Max Length 250)" CssClass="form-control"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        <div class="clearfix mt10"></div>
                                                                            <asp:TextBox ID="txtfiigno" runat="server" CssClass="form-control" required="" TextMode="MultiLine" MaxLength="500" Height="100px" placeholder="Enter brief remarks here (Max Length 500)"></asp:TextBox>
                                                                            <div class="clearfix mt10"></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>                                                      
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </ContentTemplate>
                            </asp:updatepanel>
                            <asp:updateprogress id="UpdateProgress1" runat="server" associatedupdatepanelid="upn">
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
                            </asp:updateprogress>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
