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
    <!-------------------------------------------image show end------------------------------->
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
    <asp:HiddenField ID="hidType" runat="server" />
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
                <asp:UpdatePanel runat="server" ID="updrop">
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
                </asp:UpdatePanel>
            </div>
            <div class="tabing-section">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#pd"> Item brief description</a></li>
                    <li><a data-toggle="tab" href="#pimg">Item Specification</a></li>
                    <li><a data-toggle="tab" href="#qpt">Estimated Quantity</a></li>
                    <li><a data-toggle="tab" href="#test" runat="server" visible="false">Testing & Certification</a></li>
                    <li><a data-toggle="tab" href="#spd" runat="server" visible="false">Technical & Financial Support</a></li>
                    <li><a data-toggle="tab" href="#tnd">Tender and EOI</a></li>
                    <li><a data-toggle="tab" href="#cd">Contact</a></li>
                </ul>
                <div class="tab-content">
                    <div id="pd" class="tab-pane fade in active">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="section-pannel">
                                        <asp:UpdatePanel runat="server" ID="upproduct" UpdateMode="Conditional">
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
                                                            <asp:DropDownList runat="server" ID="ddllevel3product" AutoPostBack="True" TabIndex="3" Style="text-transform: uppercase !important;" class="form-control" OnSelectedIndexChanged="ddllevel3product_SelectedIndexChanged"></asp:DropDownList>
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
                                                                <asp:TextBox runat="server" ID="txtnsccode" ReadOnly="True" MaxLength="4" TabIndex="4" CssClass="form-cascade-control form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-6 padding_0">
                                                                <label>
                                                                    NIIN Code (9-digit)
                                                                    <span data-toggle="tooltip" class="fa fa-question" title="Please enter if NIIN code is available"></span>
                                                                </label>
                                                                <asp:TextBox runat="server" ID="txtniincode" TabIndex="5" MaxLength="9" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Item brief description</label>
                                                            <span class="mandatory">* (Editable)</span>  <span data-toggle="tooltip" class="fa fa-question" title="If item description is not relevant, edit the item description."></span>
                                                            <asp:TextBox runat="server" ID="txtproductdescription" required="" Height="70px" MaxLength="250" TabIndex="6" class="form-control"></asp:TextBox>
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
                                                            <asp:FileUpload runat="server" ID="fuitemdescriptionfile" CssClass="form-control" TabIndex="29" />
                                                            <div class="clearfix mt5"></div>
                                                            <asp:Label runat="server" ID="lblfuitemdescriptionfile" Visible="False"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Item Image</label>
                                                            <span class="mandatory">(only .jpeg, .png, .jpg files of each max 5 Mb.(max 4 files))</span>
                                                            <div class="fr">
                                                                <asp:FileUpload ID="fuimages" runat="server" CssClass="uploadimage form-control" AllowMultiple="true" TabIndex="30" />
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
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upproduct">
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
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <label>
                                                        HSN Code (4-8 digit) <%--<span class="mandatory">*</span>--%> <a href="https://www.cbic-gst.gov.in/gst-goods-services-rates.html" target="_blank">(For finding hsn code please click here to get redirected to gst website.)<span data-toggle="tooltip" class="fa fa-question" title="HSN Code (4-8 digit ) and Link for find hsn code"></span></a>
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txthsncodereadonly" MaxLength="8" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>DPSU Part Number</label>
                                                    <asp:TextBox runat="server" ID="txtdpsupartnumber" TabIndex="9" class="form-control"></asp:TextBox>
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
                                                    <asp:TextBox runat="server" ID="txthsncode" TabIndex="5" MaxLength="9" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel7">
                                                <ContentTemplate>
                                                    <div class="col-md-3" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label>HS Code (Hidden)</label>
                                                            <span class="mandatory">*</span>
                                                            <asp:DropDownList runat="server" ID="ddlHSNCode" TabIndex="2" class="form-control" Height="35px" Style="text-transform: uppercase !important;"></asp:DropDownList>
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
                                                            <asp:DropDownList runat="server" ID="ddlhsncodelev1" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlhsncodelev1_SelectedIndexChanged" class="form-control" Height="35px" Style="text-transform: uppercase !important;"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <label>Description</label>
                                                            <span class="mandatory">*</span>
                                                            <asp:DropDownList runat="server" ID="ddlhsncodelevel2" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlhsncodelevel2_SelectedIndexChanged" class="form-control" Height="35px" Style="text-transform: uppercase !important;"></asp:DropDownList>
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
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress10" runat="server" AssociatedUpdatePanelID="upproduct">
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
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Part Number</label>
                                                    <asp:TextBox runat="server" ID="txtoempartnumber" TabIndex="7" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Name</label>
                                                    <asp:TextBox runat="server" ID="txtoemname" TabIndex="8" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Country</label>
                                                    <span class="mandatory">*</span>
                                                    <asp:DropDownList ID="txtcountry" runat="server" Height="35px" TabIndex="9" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-4" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>End User Part Number</label>
                                                    <asp:TextBox runat="server" ID="txtenduserpartnumber" TabIndex="12" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblenduser" Text="End User"></asp:Label><span class="mandatory"> *</span>
                                                    <div class="clearfix"></div>
                                                    <asp:ListBox runat="server" ID="ddlenduser" SelectionMode="Multiple" Style="text-transform: uppercase !important;" class="form-control ui fluid dropdown" TabIndex="13"></asp:ListBox>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                                <ContentTemplate>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>DEFENCE PLATFORM<span class="mandatory">*</span></label>
                                                            <asp:DropDownList runat="server" ID="ddlplatform" AutoPostBack="True" TabIndex="14" Style="text-transform: uppercase !important;" class="form-control" OnSelectedIndexChanged="ddlplatform_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" style="margin-top: 8px;">
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblNomenclature" Text="NAME OF DEFENCE PLATFORM"></asp:Label><span class="mandatory"> *</span>
                                                            <asp:DropDownList runat="server" ID="ddlnomnclature" class="form-control" Style="text-transform: uppercase !important;" TabIndex="15" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel6">
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
                                    <div class="section-pannel">
                                        <div class="row">
                                            <asp:UpdatePanel runat="server" ID="uptechnology">
                                                <ContentTemplate>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>PRODUCT (INDUSTRY DOMAIN)<span class="mandatory">*</span></label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="If you not display your category in this section, please add in Category Master >> Category Dropdown"></span>
                                                            <asp:DropDownList runat="server" ID="ddltechnologycat" class="form-control" TabIndex="16" Style="text-transform: uppercase !important;" AutoPostBack="True" OnSelectedIndexChanged="ddltechnologycat_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>PRODUCT (INDUSTRY SUB DOMAIN)<span class="mandatory">*</span></label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Item Level 1, if you not see product level 2 please add in Category master >> level 2 "></span>
                                                            <asp:DropDownList runat="server" ID="ddlsubtech" class="form-control" TabIndex="17" Style="text-transform: uppercase !important;" AutoPostBack="True" OnSelectedIndexChanged="ddlsubtech_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>PRODUCT (INDUSTRY 2nd SUB DOMAIN)</label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Item Level 2, if you not see product level 3 please add in Category master >> level 3 "></span>
                                                            <asp:DropDownList runat="server" ID="ddltechlevel3" TabIndex="18" Style="text-transform: uppercase !important;" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>

                                                </ContentTemplate>
                                                <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="uptechnology">
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
                                    <div class="section-pannel">
                                        <div class="row">
                                            <asp:UpdatePanel runat="server" ID="upindwginized">
                                                <ContentTemplate>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label>Search keywords (To add more than one search keyword please use comma(,))</label>
                                                                    <asp:TextBox runat="server" ID="txtsearchkeyword" MaxLength="50" TabIndex="19" class="form-control"></asp:TextBox>
                                                                    <div class="clearfix" style="margin-top: 5px;"></div>
                                                                    <span>(Max length 50 words only)</span>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <label class="live-status-box productalreadylabel">
                                                                    Item already indigenized :
                                                                <asp:RadioButtonList runat="server" ID="rbisindinised" RepeatColumns="2" TabIndex="20" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbisindinised_CheckedChanged ">
                                                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                                    <asp:ListItem Value="Y" class="yes">Yes</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                </label>
                                                            </div>
                                                            <div class="clearfix" style="margin-top: 10px;"></div>
                                                            <div class="row">
                                                                <div runat="server" id="divisIndigenized" visible="False">
                                                                    <div class="col-sm-4">
                                                                        <label>Enter Manufacturer name</label>
                                                                        <asp:TextBox runat="server" ID="txtmanufacturename" TabIndex="21" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <label>Address</label>
                                                                        <asp:TextBox runat="server" ID="txtmanifacaddress" MaxLength="250" TabIndex="22" class="form-control"></asp:TextBox>
                                                                        <div class="clearfix" style="margin-top: 5px;"></div>
                                                                        <span>(Max length 250 words only)</span>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <label>Year of Indiginization</label>
                                                                        <asp:DropDownList runat="server" ID="ddlyearofindiginization" Height="35px" TabIndex="23" class="form-control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="upindwginized">
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
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                                    <ContentTemplate>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="checkbox-box productalreadylabel">
                                                                    Is Item imported in last 5 years?
                                                                </label>
                                                                <asp:RadioButtonList runat="server" ID="rbproductImported" RepeatColumns="2" TabIndex="24" RepeatLayout="Flow"
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
                                                                            <asp:CheckBoxList runat="server" class="mr-3 ProddisplayInline" ID="chklistimportyearfive" RepeatDirection="Horizontal" TabIndex="27" RepeatLayout="Flow" RepeatColumns="5">
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
                                                                <asp:TextBox runat="server" ID="txtremarksyearofimportyes" Height="70px" TabIndex="28" MaxLength="250" class="form-control"></asp:TextBox>
                                                                <div class="clearfix" style="margin-top: 5px;"></div>
                                                                <span>(Max length 250 words only)</span>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
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
                                                    <asp:TextBox runat="server" ID="txtfeaturesanddetails" Style="background-color: #fff !important;" TabIndex="31"
                                                        Width="1000" Height="70px" placeholder="Ductile,Tensile,Lusture" MaxLength="250"></asp:TextBox>
                                                    <asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txtfeaturesanddetails">
                                                    </asp:HtmlEditorExtender>
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
                                                    <asp:RadioButtonList ID="rbitemspecification" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem Value="Specification are available in open source/DPSU spec">Specification are available in open source/DPSU spec</asp:ListItem>
                                                        <asp:ListItem Value="Items with OEM specification">Items with OEM specification</asp:ListItem>
                                                        <asp:ListItem Value="Items where no specification are available">Items where no specification are available</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <label>Item Information</label>
                                                            <div class="clearfix mt5"></div>
                                                            <div class="table table-responsive">
                                                                <table border="0" cellpadding="0" cellspacing="0" class="gridFormTable" style="border-collapse: collapse; width: 100%">
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
                                                </asp:UpdatePanel>
                                                <asp:UpdateProgress ID="UpdateProgress8" runat="server" AssociatedUpdatePanelID="UpdatePanel5">
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
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Additional Information</label><span class="mandatory"> (Editable)</span>
                                                    <asp:TextBox runat="server" ID="txtadditionalinfo" Height="70px" TabIndex="37" class="form-control" placeholder="Warranty,Guarantee"></asp:TextBox>
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
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div class="table table-responsive">
                                                    <table border="0" cellpadding="0" class="gridFormTable EstimateGridview" cellspacing="0" style="border-collapse: collapse; width: 100%">
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
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>PROCURMENT CATEGORY</label>
                                                    <div class="clearfix"></div>
                                                    <asp:GridView runat="server" ID="gvprocurmentcategory" TabIndex="48" class="table-responsive table" AutoGenerateColumns="False">
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
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div id="Div1" class="col-md-12" runat="server">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="Label1" CssClass="form-label " Text="PROCURMENT CATEGORY REMARK"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtremarksprocurmentCategory" TabIndex="49" MaxLength="250" Height="70px" class="form-control"></asp:TextBox>
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
                    <div id="tnd" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="section-pannel">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group ">
                                                            <label>
                                                                Tender Status</label>
                                                            <asp:DropDownList runat="server" ID="ddltendorstatus" class="form-control" TabIndex="50" AutoPostBack="True" OnSelectedIndexChanged="ddltendorstatus_SelectedIndexChanged">
                                                                <asp:ListItem Value="Not Floated" Selected="True">Not Floated</asp:ListItem>
                                                                <asp:ListItem Value="Archive">Archive</asp:ListItem>
                                                                <asp:ListItem Value="Live">Live</asp:ListItem>
                                                                <asp:ListItem Value="To be Floated shortly">To be Floated shortly</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" runat="server" cssclass="hidden" id="divtendordate" visible="False">
                                                        <div class="form-group live-status-box">
                                                            <label>
                                                                <strong>Note:</strong> If live, please fill last date of tender submission. 
                                            <span class="checkbox-box productalreadylabel">
                                                <asp:RadioButtonList runat="server" ID="rbtendordateyesno" RepeatDirection="Horizontal" TabIndex="51" AutoPostBack="True" RepeatColumns="2" RepeatLayout="Flow" OnSelectedIndexChanged="rbtendordateyesno_CheckedChanged">
                                                    <asp:ListItem Value="N" runat="server" Selected="True">No</asp:ListItem>
                                                    <asp:ListItem Value="Y" class="yes">Yes</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </span>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="Turl_Tdate" runat="server" visible="False" id="divtdate">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Last date of tender submission</label>
                                                                <asp:TextBox runat="server" ID="txttendordate" type="date" TabIndex="52" class="form-control inputbox"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Tender URL</label>
                                                                <asp:TextBox runat="server" ID="txttendorurl" TabIndex="53" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group ">
                                                            <label>
                                                                EOI Status</label>
                                                            <asp:DropDownList runat="server" ID="ddleoi" class="form-control" TabIndex="50" AutoPostBack="True" OnSelectedIndexChanged="ddleoi_SelectedIndexChanged">
                                                                <asp:ListItem Value="Not Floated" Selected="True">Not Floated</asp:ListItem>
                                                                <asp:ListItem Value="Archive">Archive</asp:ListItem>
                                                                <asp:ListItem Value="Live">Live</asp:ListItem>
                                                                <asp:ListItem Value="To be Floated shortly">To be Floated shortly</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" runat="server" cssclass="hidden" id="diveoiyesno" visible="False">
                                                        <div class="form-group live-status-box">
                                                            <label>
                                                                <strong>Note:</strong> If live, please fill last date of eoi submission. </strong>
                                            <span class="checkbox-box productalreadylabel">
                                                <asp:RadioButtonList runat="server" ID="rbeoistatus" RepeatDirection="Horizontal" TabIndex="51" AutoPostBack="True"
                                                    RepeatColumns="2" RepeatLayout="Flow" OnSelectedIndexChanged="rbeoistatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="N" runat="server" Selected="True">No</asp:ListItem>
                                                    <asp:ListItem Value="Y" class="yes">Yes</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </span>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="Turl_Tdate" runat="server" visible="False" id="diveoidateurl">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Last date of eoi submission</label>
                                                                <asp:TextBox runat="server" ID="txtlastdateeoi" type="date" TabIndex="52" class="form-control inputbox"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>EOI URL</label>
                                                                <asp:TextBox runat="server" ID="txteoiurl" TabIndex="53" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress11" runat="server" AssociatedUpdatePanelID="UpdatePanel8">
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
                        </div>
                    </div>
                    <div id="cd" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                        <ContentTemplate>
                                            <div class="section-pannel" runat="server" id="divnodal">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h4 class="page-header secondary">Contact Detail 1  </h4>
                                                        <div class="form-group contactD1Select">
                                                            <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail" class="form-control" TabIndex="54" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="contactFormRow" runat="server" id="contactpanel1" visible="False">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Employee Code</label>
                                                                <asp:TextBox runat="server" ID="txtempcode" name="" Enabled="false" TabIndex="55" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Designation</label>
                                                                <asp:TextBox runat="server" ID="txtDesignation" name="" Enabled="false" TabIndex="56" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>E-Mail ID</label>
                                                                <asp:TextBox runat="server" ID="txtNEmailId" name="" Enabled="false" TabIndex="57" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Mobile Number</label>
                                                                <asp:TextBox runat="server" ID="txtmobnodal" Enabled="false" TabIndex="58" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Phone Number</label>
                                                                <asp:TextBox runat="server" ID="txtNTelephone" name="" Enabled="false" TabIndex="59" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Fax</label>
                                                                <asp:TextBox runat="server" ID="txtNFaxNo" name="" Enabled="false" TabIndex="60" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
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
                                                            <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail2" class="form-control" TabIndex="61" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail2_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="contactFormRow" runat="server" id="contactpanel2" visible="False">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Employee Code</label>
                                                                <asp:TextBox runat="server" ID="txtempcode2" name="" Enabled="false" TabIndex="62" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Designation</label>
                                                                <asp:TextBox runat="server" ID="txtdesignationnodal2" name="" TabIndex="63" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>E-Mail ID</label>
                                                                <asp:TextBox runat="server" ID="txtNEmailId2" name="" Enabled="false" TabIndex="64" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Mobile Number</label>
                                                                <asp:TextBox runat="server" ID="txtmobnodal2" name="" Enabled="false" TabIndex="65" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Phone Number</label>
                                                                <asp:TextBox runat="server" ID="txtNTelephone2" name="" Enabled="false" TabIndex="66" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Fax</label>
                                                                <asp:TextBox runat="server" ID="txtNFaxNo2" name="" Enabled="false" TabIndex="67" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
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
                    </div>
                    <div id="test" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView runat="server" ID="gvqaagency" AutoGenerateColumns="False" TabIndex="68" class=" table responsive no-wrap table-hover manage-user Grid">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Confirm if QA Agency">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkqaagency" TabIndex="1" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblqaagency" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfqaagency" Value='<%#Eval("SCategoryId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <div class="clearfix"></div>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="txtqaagencyremarks" MaxLength="50" TextMode="MultiLine" Height="50px" TabIndex="69" class="form-control"></asp:TextBox>
                                                    <div class="clearfix" style="margin-top: 5px;"></div>
                                                    <span>(Remarks max length 50 words only)</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView runat="server" ID="gvtesting" AutoGenerateColumns="False" TabIndex="68" class=" table responsive no-wrap table-hover manage-user Grid">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Confirm if testing needed">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chktesting" TabIndex="1" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lbltesting" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hftestingid" Value='<%#Eval("SCategoryId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                                <div class="clearfix"></div>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="txttestingremarks" MaxLength="50" TextMode="MultiLine" Height="50px" TabIndex="69" class="form-control"></asp:TextBox>
                                                    <div class="clearfix" style="margin-top: 5px;"></div>
                                                    <span>(Remarks max length 50 words only)</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView runat="server" ID="gvCertification" AutoGenerateColumns="False" TabIndex="70" class=" table responsive no-wrap table-hover manage-user Grid">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Confirm if certification needed">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkcertification" TabIndex="1" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblcertification" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfcertification" Value='<%#Eval("SCategoryId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <div class="clearfix"></div>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="txtcertificationremarks" MaxLength="50" Height="50px" TextMode="MultiLine" TabIndex="71" class="form-control"></asp:TextBox>
                                                    <div class="clearfix" style="margin-top: 5px;"></div>
                                                    <span>(Remarks max length 50 words only)</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="spd" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="section-pannel">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:GridView runat="server" ID="gvservices" AutoGenerateColumns="False" TabIndex="38" class=" table responsive no-wrap table-hover manage-user Grid">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Confirm if technical support provided by DPSU">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ID="chk" TabIndex="1" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblservices" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                                        <asp:HiddenField runat="server" ID="hfservicesid" Value='<%#Eval("SCategoryId") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <div class="clearfix"></div>
                                                        <div class="form-group">
                                                            <asp:TextBox runat="server" ID="txtservisesremarks" MaxLength="50" Height="50px" TabIndex="39" class="form-control"></asp:TextBox>
                                                            <div class="clearfix" style="margin-top: 5px;"></div>
                                                            <span>(Remarks max length 50 words only)</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="section-pannel">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:GridView runat="server" ID="gvfinancialsupp" AutoGenerateColumns="False" TabIndex="40" class=" table responsive no-wrap table-hover manage-user Grid">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Confirm if financial support provided by DPSU">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ID="chkfinan" TabIndex="1" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblfinancialservices" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                                        <asp:HiddenField runat="server" ID="hffinanciailserviceid" Value='<%#Eval("SCategoryId") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <div class="clearfix"></div>
                                                        <div class="form-group">
                                                            <asp:TextBox runat="server" ID="txtfinancialsuppRemarks" MaxLength="50" Height="50px" TabIndex="41" class="form-control"></asp:TextBox>
                                                            <div class="clearfix" style="margin-top: 5px;"></div>
                                                            <span>(Remarks max length 50 words only)</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsubmitpanel1">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:UpdatePanel runat="server" ID="UPSUBMIT">
                                        <ContentTemplate>
                                            <div runat="server" id="myhtmldiv"></div>
                                            <asp:Button runat="server" ID="btnsubmitpanel1" class="btn btn-primary pull-right" TabIndex="72" Text="Save" OnClick="btnsubmitpanel1_Click" OnClientClick="return confirm('Are you sure you want to save this product?');" />
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
                    </asp:Panel>
                    <asp:HiddenField runat="server" ID="hfprodid" />
                    <asp:HiddenField runat="server" ID="hfprodrefno" />
                    <asp:HiddenField runat="server" ID="hfcomprefno" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
    </script>
</asp:Content>
