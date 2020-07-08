<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_AddProduct" CodeFile="AddProduct.aspx.cs" ValidateRequest="false" MasterPageFile="~/Admin/MasterPage.master" ViewStateEncryptionMode="Always" %>

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
            <div class="clearfix" style="margin-bottom: 10px;"></div>
            <div class="row">
                <p style="position: absolute; right: 35px;">Mark with <span class="mandatory">*</span> is manadatory field.</p>
                <asp:UpdatePanel runat="server" ID="updrop">
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="col-sm-4" runat="server" visible="false" id="portalid">
                                <b>Item Id (Portal):- 
                                    <asp:Label ID="lblrefnoforinfo" runat="server"></asp:Label></b>
                            </div>
                        </div>
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
                    <li class="active"><a data-toggle="tab" href="#pd">Item description</a></li>
                    <li><a data-toggle="tab" href="#pimg" runat="server" visible="false">Item Specification</a></li>
                    <li><a data-toggle="tab" href="#qpt">Import Quantity</a></li>
                    <li><a data-toggle="tab" href="#cd">Contact & Declaration</a></li>
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
                                                    <div class="col-md-8" runat="server">
                                                        <div class="form-group">
                                                            <div class="col-md-3 padding_0" style="margin-top: 30px;">
                                                                <label>Nato Stock Number (NSN)</label>
                                                            </div>
                                                            <div class="col-sm-3 padding_0">
                                                                <label style="font-size: 14px !important;">
                                                                    NSC Code (4 digit)
                                                                        <span data-toggle="tooltip" class="fa fa-question" title="NSC Code = NATO SUPPLY GROUP (2 digit) + NATO SUPPLY CLASS (2 digit)"></span>
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
                                                            <div class="row" style="margin-top: 45px; margin-left: 5px;">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label>
                                                                            HSN Code (4-8 digit) <%--<span class="mandatory">*</span>--%>
                                                                            <%--<a href="https://www.cbic-gst.gov.in/gst-goods-services-rates.html" target="_blank">(For finding hsn code please click here to get redirected to gst website.)<span data-toggle="tooltip" class="fa fa-question" title="HSN Code (4-8 digit ) and Link for find hsn code"></span></a>--%>
                                                                        </label>
                                                                        <asp:TextBox runat="server" ID="txthsncodereadonly" TabIndex="8" MaxLength="8" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label>DPSU Part Number</label>
                                                                        <asp:TextBox runat="server" ID="txtdpsupartnumber" TabIndex="9" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="clearfix mt10"></div>
                                                            <div class="form-group" runat="server" visible="false">
                                                                <label>Search keywords (Item Name)<span class="mandatory">*</span></label>
                                                                <asp:TextBox runat="server" ID="txtsearchkeyword" MaxLength="50" required="" TabIndex="18" class="form-control"></asp:TextBox>
                                                                <div class="clearfix" style="margin-top: 5px;"></div>
                                                                <span>(Max length 50 words only)</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 row" runat="server">
                                                        <div class="form-group">
                                                            <label>Item Name</label>
                                                            <span class="mandatory">* (Editable)</span>  <span data-toggle="tooltip" class="fa fa-question" title="If item name is not relevant, edit the item name."></span>
                                                            <asp:TextBox runat="server" ID="txtproductdescription" required="" Height="100px" MaxLength="250" TabIndex="5" class="form-control"></asp:TextBox>
                                                            <div class="clearfix" style="margin-top: 5px;"></div>
                                                            <span style="margin-right: 5px; float: right;">(Max length 250 words only)</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Upload document related to item </label>
                                                            <span class="mandatory">(pdf of maximum 5 Mb can be uploaded.(max 4 files))</span>
                                                            <asp:FileUpload runat="server" ID="fuitemdescriptionfile" AllowMultiple="true" CssClass="form-control" TabIndex="6" />
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
                                                    <div class="col-sm-6">
                                                        <div runat="server" id="divPdf" visible="False">
                                                            <asp:GridView runat="server" ID="dlpdf" OnRowCommand="dlpdf_OnRowCommand" AutoGenerateColumns="false" Class="table table-responsive">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="File Name" DataField="ImageName" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="lblremovepdf" class="fa fa-trash text-center imgdel" CommandName="removepdf" CommandArgument='<%#Eval("ImageId") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div runat="server" id="divimgdel" visible="False">
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
                                                    <div class="clearfix mt5"></div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Features & Details</label><span class="mandatory"> (Editable)</span>
                                                            <asp:TextBox runat="server" ID="txtfeaturesanddetails" Style="background-color: #fff !important;" TabIndex="1"
                                                                Width="1000" Height="70px" placeholder="Ductile,Tensile,Lusture" MaxLength="250"></asp:TextBox>
                                                            <asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txtfeaturesanddetails">
                                                            </asp:HtmlEditorExtender>
                                                            <div class="clearfix" style="margin-top: 35px;"></div>
                                                            <span>(Max length 250)</span>
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
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Part Number</label>
                                                    <asp:TextBox runat="server" ID="txtoempartnumber" TabIndex="9" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Name</label>
                                                    <asp:TextBox runat="server" ID="txtoemname" TabIndex="10" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Country</label>
                                                    <span class="mandatory">*</span>
                                                    <asp:DropDownList ID="txtcountry" runat="server" Height="35px" TabIndex="11" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblenduser" Text="End User"></asp:Label>
                                                    <span class="mandatory">*</span>
                                                    <div class="clearfix"></div>
                                                    <asp:ListBox runat="server" ID="ddlenduser" SelectionMode="Multiple" Style="text-transform: uppercase !important;" class="form-control ui fluid dropdown" TabIndex="12"></asp:ListBox>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
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
                                                            <asp:DropDownList runat="server" ID="ddlsubtech" class="form-control" TabIndex="16" Style="text-transform: uppercase !important;" AutoPostBack="false" OnSelectedIndexChanged="ddlsubtech_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" runat="server" visible="false">
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
                                            <div class="col-md-12">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
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
                                                        <div class="col-sm-12">
                                                            <div class="form-group">
                                                                <label class="checkbox-box productalreadylabel">
                                                                    Imported During last 3 years
                                                                </label>
                                                                <asp:RadioButtonList runat="server" ID="rbproductImported" RepeatColumns="2" TabIndex="23" RepeatLayout="Flow"
                                                                    RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbproductImported_CheckedChanged ">
                                                                    <asp:ListItem Value="N" Selected="True" style="margin-left: 5px;">No</asp:ListItem>
                                                                    <asp:ListItem Value="Y" class="yes" style="margin-left: 10px;">Yes</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div class="clearfix mt5"></div>
                                                            <div runat="server" id="divyearofimportYes" visible="False">
                                                                <div class="section-pannel">
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                                                        <ContentTemplate>
                                                                            <div class="table table-responsive">
                                                                                <table class="table table-responsive" runat="server" id="xxesti">
                                                                                    <tr>
                                                                                        <th>Year</th>
                                                                                        <th>Imported Quantity</th>
                                                                                        <th>Unit</th>
                                                                                        <th>Imported value in Rs Lakh (Qty*Price)</th>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlyearestimate1" runat="server" CssClass="form-control">
                                                                                                <asp:ListItem Value="1">2017-18</asp:ListItem>
                                                                                            </asp:DropDownList></td>
                                                                                        <td>
                                                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtestquan1" onkeypress="return isNumberKey(event)" Placeholder="Imported Quantity"></asp:TextBox></td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlunit1" runat="server" CssClass="form-control">
                                                                                                <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                                                <asp:ListItem Value="number">number</asp:ListItem>
                                                                                                <asp:ListItem Value="sets">sets</asp:ListItem>
                                                                                                <asp:ListItem Value="milligrams(mg)">milligrams(mg)</asp:ListItem>
                                                                                                <asp:ListItem Value="grams(g)">grams(g)</asp:ListItem>
                                                                                                <asp:ListItem Value="kilograms(kg)">kilograms(kg),</asp:ListItem>
                                                                                                <asp:ListItem Value="tons(t)">tons(t)</asp:ListItem>
                                                                                                <asp:ListItem Value="metric tons (mt)">metric tons (mt)</asp:ListItem>
                                                                                                <asp:ListItem Value="pounds(lb)">pounds(lb)</asp:ListItem>
                                                                                                <asp:ListItem Value="ounces(oz)">ounces(oz)</asp:ListItem>
                                                                                                <asp:ListItem Value="centimeters(cm)">centimeters(cm)</asp:ListItem>
                                                                                                <asp:ListItem Value="meters(m)">meters(m)</asp:ListItem>
                                                                                                <asp:ListItem Value="kilometers(km)">kilometers(km)</asp:ListItem>
                                                                                                <asp:ListItem Value="inches(in)">inches(in)</asp:ListItem>
                                                                                                <asp:ListItem Value="feet(ft)">feet(ft)</asp:ListItem>
                                                                                                <asp:ListItem Value="yard(yd)">yard(yd)</asp:ListItem>
                                                                                                <asp:ListItem Value="miles(mi)">miles(mi)</asp:ListItem>
                                                                                                <asp:ListItem Value="square meters">square meters</asp:ListItem>
                                                                                                <asp:ListItem Value="square inches">square inches</asp:ListItem>
                                                                                                <asp:ListItem Value="square feets">square feets</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic">cubic</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic centimeters">cubic centimeters</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic meters">cubic meters</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic inches">cubic inches</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic feets">cubic feets</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic yards">cubic yards</asp:ListItem>
                                                                                            </asp:DropDownList></td>
                                                                                        <td>
                                                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtpriceestimate1" onkeypress="return isNumberKey(event)" Placeholder="Imported value in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlyearestimate2" runat="server" CssClass="form-control">
                                                                                                <asp:ListItem Value="2">2018-19</asp:ListItem>
                                                                                            </asp:DropDownList></td>
                                                                                        <td>
                                                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtestquan2" onkeypress="return isNumberKey(event)" Placeholder="Imported Quantity"></asp:TextBox></td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlunit2" runat="server" CssClass="form-control">
                                                                                                <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                                                <asp:ListItem Value="number">number</asp:ListItem>
                                                                                                <asp:ListItem Value="sets">sets</asp:ListItem>
                                                                                                <asp:ListItem Value="milligrams(mg)">milligrams(mg)</asp:ListItem>
                                                                                                <asp:ListItem Value="grams(g)">grams(g)</asp:ListItem>
                                                                                                <asp:ListItem Value="kilograms(kg)">kilograms(kg),</asp:ListItem>
                                                                                                <asp:ListItem Value="tons(t)">tons(t)</asp:ListItem>
                                                                                                <asp:ListItem Value="metric tons (mt)">metric tons (mt)</asp:ListItem>
                                                                                                <asp:ListItem Value="pounds(lb)">pounds(lb)</asp:ListItem>
                                                                                                <asp:ListItem Value="ounces(oz)">ounces(oz)</asp:ListItem>
                                                                                                <asp:ListItem Value="centimeters(cm)">centimeters(cm)</asp:ListItem>
                                                                                                <asp:ListItem Value="meters(m)">meters(m)</asp:ListItem>
                                                                                                <asp:ListItem Value="kilometers(km)">kilometers(km)</asp:ListItem>
                                                                                                <asp:ListItem Value="inches(in)">inches(in)</asp:ListItem>
                                                                                                <asp:ListItem Value="feet(ft)">feet(ft)</asp:ListItem>
                                                                                                <asp:ListItem Value="yard(yd)">yard(yd)</asp:ListItem>
                                                                                                <asp:ListItem Value="miles(mi)">miles(mi)</asp:ListItem>
                                                                                                <asp:ListItem Value="square meters">square meters</asp:ListItem>
                                                                                                <asp:ListItem Value="square inches">square inches</asp:ListItem>
                                                                                                <asp:ListItem Value="square feets">square feets</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic">cubic</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic centimeters">cubic centimeters</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic meters">cubic meters</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic inches">cubic inches</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic feets">cubic feets</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic yards">cubic yards</asp:ListItem>
                                                                                            </asp:DropDownList></td>
                                                                                        <td>
                                                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtpriceestimate2" onkeypress="return isNumberKey(event)" Placeholder="Imported value in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlyearestimate3" runat="server" CssClass="form-control">
                                                                                                <asp:ListItem Value="3">2019-20</asp:ListItem>
                                                                                            </asp:DropDownList></td>
                                                                                        <td>
                                                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtestquan3" onkeypress="return isNumberKey(event)" Placeholder="Imported Quantity"></asp:TextBox></td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlunit3" runat="server" CssClass="form-control">
                                                                                                <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                                                <asp:ListItem Value="number">number</asp:ListItem>
                                                                                                <asp:ListItem Value="sets">sets</asp:ListItem>
                                                                                                <asp:ListItem Value="milligrams(mg)">milligrams(mg)</asp:ListItem>
                                                                                                <asp:ListItem Value="grams(g)">grams(g)</asp:ListItem>
                                                                                                <asp:ListItem Value="kilograms(kg)">kilograms(kg),</asp:ListItem>
                                                                                                <asp:ListItem Value="tons(t)">tons(t)</asp:ListItem>
                                                                                                <asp:ListItem Value="metric tons (mt)">metric tons (mt)</asp:ListItem>
                                                                                                <asp:ListItem Value="pounds(lb)">pounds(lb)</asp:ListItem>
                                                                                                <asp:ListItem Value="ounces(oz)">ounces(oz)</asp:ListItem>
                                                                                                <asp:ListItem Value="centimeters(cm)">centimeters(cm)</asp:ListItem>
                                                                                                <asp:ListItem Value="meters(m)">meters(m)</asp:ListItem>
                                                                                                <asp:ListItem Value="kilometers(km)">kilometers(km)</asp:ListItem>
                                                                                                <asp:ListItem Value="inches(in)">inches(in)</asp:ListItem>
                                                                                                <asp:ListItem Value="feet(ft)">feet(ft)</asp:ListItem>
                                                                                                <asp:ListItem Value="yard(yd)">yard(yd)</asp:ListItem>
                                                                                                <asp:ListItem Value="miles(mi)">miles(mi)</asp:ListItem>
                                                                                                <asp:ListItem Value="square meters">square meters</asp:ListItem>
                                                                                                <asp:ListItem Value="square inches">square inches</asp:ListItem>
                                                                                                <asp:ListItem Value="square feets">square feets</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic">cubic</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic centimeters">cubic centimeters</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic meters">cubic meters</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic inches">cubic inches</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic feets">cubic feets</asp:ListItem>
                                                                                                <asp:ListItem Value="cubic yards">cubic yards</asp:ListItem>
                                                                                            </asp:DropDownList></td>
                                                                                        <td>
                                                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtpriceestimate3" onkeypress="return isNumberKey(event)" Placeholder="Imported value in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                                                    </tr>
                                                                                </table>
                                                                                <asp:GridView ID="GridView3" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                                                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="GridView3_RowCommand">
                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Sr.No" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItemIndex+1 %>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="FYear" HeaderText="Year" />
                                                                                        <asp:BoundField DataField="EstimatedQty" HeaderText="Quantity" />
                                                                                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                        <asp:BoundField DataField="EstimatedPrice" HeaderText="Imported value in Rs Lakh (Qty*Price)" />
                                                                                        <asp:TemplateField HeaderText="Action">
                                                                                            <ItemTemplate>
                                                                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("Year") %>' />
                                                                                                <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("ProdQtyPriceId") %>' />
                                                                                                <asp:LinkButton ID="LinkButton3" runat="server" Class="fa fa-plus-circle" CommandArgument='<%#Eval("ProdQtyPriceId") %>' CommandName="addnewmfe1"></asp:LinkButton>
                                                                                                <asp:LinkButton ID="LinkButton4" runat="server" Class="fa fa-edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="updatenewmfe1"></asp:LinkButton>
                                                                                                <asp:LinkButton ID="LinkButton5" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("ProdQtyPriceId") %>' CommandName="deletenewmfe1"></asp:LinkButton>
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
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
                                                        <div class="clearfix mt5"></div>
                                                        <div class="col-sm-12" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label>Remarks</label>
                                                                <asp:TextBox runat="server" ID="txtremarksyearofimportyes" Height="70px" TabIndex="25" MaxLength="250" class="form-control"></asp:TextBox>
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
                                                    <label><strong>Type of item specification</strong></label>
                                                    <div class="clearfix mt5"></div>
                                                    <asp:RadioButtonList ID="rbitemspecification" runat="server" RepeatColumns="1" TabIndex="2" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem Value="Item Specification are available in open source">Item Specification are available in open source</asp:ListItem>
                                                        <asp:ListItem Value="Items with DPSU specifications">Items with DPSU specifications</asp:ListItem>
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
                                                    <asp:TextBox runat="server" ID="txtadditionalinfo" Height="70px" TabIndex="4" class="form-control" placeholder="Warranty,Guarantee"></asp:TextBox>
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
                                                <h5>Import Quantity</h5>
                                                <div class="table table-responsive">
                                                    <asp:GridView ID="GvEstiateQuanPrice" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="GvEstiateQuanPrice_RowCreated">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SNo" HeaderText="Raw Number" Visible="false" />
                                                            <asp:TemplateField HeaderText="Year">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlYearEstimate" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="1">2020-21</asp:ListItem>
                                                                        <%--<asp:ListItem Value="2">2021-22</asp:ListItem>
                                                                        <asp:ListItem Value="3">2022-23</asp:ListItem>
                                                                        <asp:ListItem Value="4">2023-24</asp:ListItem>
                                                                        <asp:ListItem Value="5">2024-25</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtestimateQuantity" onkeypress="return isNumberKey(event)" runat="server" CssClass="form-control" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlMeasuringUnit" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                        <asp:ListItem Value="number">number</asp:ListItem>
                                                                        <asp:ListItem Value="sets">sets</asp:ListItem>
                                                                        <asp:ListItem Value="milligrams(mg)">milligrams(mg)</asp:ListItem>
                                                                        <asp:ListItem Value="grams(g)">grams(g)</asp:ListItem>
                                                                        <asp:ListItem Value="kilograms(kg)">kilograms(kg),</asp:ListItem>
                                                                        <asp:ListItem Value="tons(t)">tons(t)</asp:ListItem>
                                                                        <asp:ListItem Value="metric tons (mt)">metric tons (mt)</asp:ListItem>
                                                                        <asp:ListItem Value="pounds(lb)">pounds(lb)</asp:ListItem>
                                                                        <asp:ListItem Value="ounces(oz)">ounces(oz)</asp:ListItem>
                                                                        <asp:ListItem Value="centimeters(cm)">centimeters(cm)</asp:ListItem>
                                                                        <asp:ListItem Value="meters(m)">meters(m)</asp:ListItem>
                                                                        <asp:ListItem Value="kilometers(km)">kilometers(km)</asp:ListItem>
                                                                        <asp:ListItem Value="inches(in)">inches(in)</asp:ListItem>
                                                                        <asp:ListItem Value="feet(ft)">feet(ft)</asp:ListItem>
                                                                        <asp:ListItem Value="yard(yd)">yard(yd)</asp:ListItem>
                                                                        <asp:ListItem Value="miles(mi)">miles(mi)</asp:ListItem>
                                                                        <asp:ListItem Value="square meters">square meters</asp:ListItem>
                                                                        <asp:ListItem Value="square inches">square inches</asp:ListItem>
                                                                        <asp:ListItem Value="square feets">square feets</asp:ListItem>
                                                                        <asp:ListItem Value="cubic">cubic</asp:ListItem>
                                                                        <asp:ListItem Value="cubic centimeters">cubic centimeters</asp:ListItem>
                                                                        <asp:ListItem Value="cubic meters">cubic meters</asp:ListItem>
                                                                        <asp:ListItem Value="cubic inches">cubic inches</asp:ListItem>
                                                                        <asp:ListItem Value="cubic feets">cubic feets</asp:ListItem>
                                                                        <asp:ListItem Value="cubic yards">cubic yards</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Import value in Rs lakh (Qty*Price)">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtestimatePriceLLp" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" />
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="btnAddEstimate" runat="server" Text="Add New Row" Visible="false" CssClass="btn btn-primary pull-right" OnClick="btnAddEstimate_Click"></asp:LinkButton>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbRemoveestimate" runat="server" CssClass="fa fa-times" OnClick="lbRemoveestimate_Click"></asp:LinkButton>
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
                                                    <asp:GridView ID="GvEstimateQuanPriceEdit" runat="server" CssClass="table table-hover" CellPadding="4" ShowFooter="true"
                                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="GvEstimateQuanPriceEdit_RowCommand">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No" Visible="false">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="FYear" HeaderText="Year" />
                                                            <asp:BoundField DataField="EstimatedQty" HeaderText="Quantity" />
                                                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                            <asp:BoundField DataField="EstimatedPrice" HeaderText="Imported value in Rs Lakh (Qty*Price)" />
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hfyearid" runat="server" Value='<%#Eval("Year") %>' />
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("ProdQtyPriceId") %>' />
                                                                    <asp:LinkButton ID="lbladdnewmanufacilityedit" runat="server" Class="fa fa-plus-circle" Visible="false" CommandArgument='<%#Eval("ProdQtyPriceId") %>' CommandName="addnewmfe"></asp:LinkButton>
                                                                    <asp:LinkButton ID="lblupdatenewmanufacilityedit" runat="server" Class="fa fa-edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="updatenewmfe"></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbldeletenewmanufacilityedit" runat="server" Class="fa fa-trash" CommandArgument='<%#Eval("ProdQtyPriceId") %>' CommandName="deletenewmfe"></asp:LinkButton>
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
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <h5>Indigenization Category</h5>
                                                    <asp:CheckBoxList ID="rbIgCategory" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem Value="23">Item Under Extant Procedure</asp:ListItem>
                                                        <asp:ListItem Value="25">Item Under Make-II</asp:ListItem>
                                                        <asp:ListItem Value="24">Item Under IGA</asp:ListItem>
                                                        <asp:ListItem Value="26">Item Under iDEx/AI/Innovation/R&D</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <h5>Status of Indigenization</h5>
                                                    <label>EoI/RFP</label>
                                                    <asp:RadioButtonList ID="rbeoimake2" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem Value="Yes" style="margin-left: 10px;">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No" style="margin-left: 10px;">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="form-group">
                                                    <label>Link</label>
                                                    (if yes) <span class="mandatory">*</span>
                                                    <asp:TextBox ID="txteoilink" runat="server" CssClass="form-control" placeholder="Link of Tendor"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-4" runat="server" visible="false">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="Label1" CssClass="form-label " Text="Quality Assurance"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtremarksprocurmentCategory" TabIndex="3" MaxLength="250" Height="115px" class="form-control"></asp:TextBox>
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
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
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
                                            <div class="section-pannel" runat="server" id="div1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h4 class="page-header secondary">Declaration</h4>
                                                        <div class="col-sm-12">
                                                            <div class="form-group">
                                                                <label><span class="mandatory">*</span> While uploading drawing and specification of the item on indigenization portal , please ensure that there is</label>

                                                                <div class="fr">
                                                                    <asp:CheckBoxList ID="chklistdeclarationimage" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                        <asp:ListItem Value="No IPR issue">1. No IPR issue</asp:ListItem>
                                                                        <asp:ListItem Value="No violation of TOT agreement">2. No violation of TOT agreement</asp:ListItem>
                                                                        <asp:ListItem Value="No violation of Security Concern">3. No violation of Security Concern</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>
                                                                    <span class="mandatory">*</span> Product is eligible to be displayed for general viewing (without registration) and hereby respective DPSU/OFB/SHQ provides consent 
                                                                    for onward display of the relevant information at defenceimports.gov.in.
                                                                    
                                                                </label>
                                                                <div class="fr">
                                                                    <asp:RadioButtonList ID="rbeligible" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="N" style="margin-left: 10px;">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
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
                    <div>
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsubmitpanel1">
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
                        </asp:Panel>
                    </div>
                    <asp:HiddenField runat="server" ID="hfprodid" />
                    <asp:HiddenField runat="server" ID="hfprodrefno" />
                    <asp:HiddenField runat="server" ID="hfcomprefno" />
                    <div class="modal fade" id="changePass" role="dialog">
                        <div class="modal-dialog" style="width: 1200px; z-index: 9999999999;">
                            <asp:UpdatePanel ID="upn" runat="server" ChildrenAsTriggers="true">
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
                                                        <li class="active"><a data-toggle="tab" href="#LGY">FIIG - Mandatory</a></li>
                                                        <li><a data-toggle="tab" href="#LGN">FIIG - Optional</a></li>
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
                                                                                    <asp:BoundField runat="server" DataField="MRC_TITLE" HeaderText="MRC Title" />
                                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtinfonsnfig" runat="server" required="" MaxLength="250" Placeholder="Remarks (Max Length 250)" CssClass="form-control"></asp:TextBox>
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
                                                                                    <asp:BoundField runat="server" DataField="MRC_TITLE" HeaderText="MRC Title" />
                                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtremNoFiig" runat="server" MaxLength="250" Placeholder="Remarks (Max Length 250)" CssClass="form-control"></asp:TextBox>
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
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upn">
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
                    <div class="modal fade" id="divbank" role="dialog" data-keyboard="false" data-backdrop="static">
                        <div class="modal-dialog" style="width: 400px;">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <div class="modal-content">
                                        <div class="modal-header modal-header1">
                                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Estimated Quantity</h4>
                                        </div>
                                        <div class="modal-body clearfix" style="padding: 0 20px;">
                                            <div class="form-group" style="margin: 0">
                                                <label for="uname" class=" tetLable">
                                                    Year
                                                </label>
                                                <asp:DropDownList runat="server" ID="txtestimateyearu" Class="form-control">
                                                    <asp:ListItem Value="1">2020-21</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group" style="margin: 0">
                                                <label for="uname" class=" tetLable">
                                                    Estimated Quantity
                                                </label>
                                                <asp:TextBox runat="server" ID="txtestimatequanu" placeholder="Estimated Quantity" onkeypress="return isNumberKey(event)" Class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group" style="margin: 0">
                                                <label for="uname" class=" tetLable">
                                                    Unit
                                                </label>
                                                <asp:DropDownList ID="ddlestimateunitu" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                    <asp:ListItem Value="number">number</asp:ListItem>
                                                    <asp:ListItem Value="sets">sets</asp:ListItem>
                                                    <asp:ListItem Value="milligrams(mg)">milligrams(mg)</asp:ListItem>
                                                    <asp:ListItem Value="grams(g)">grams(g)</asp:ListItem>
                                                    <asp:ListItem Value="kilograms(kg)">kilograms(kg),</asp:ListItem>
                                                    <asp:ListItem Value="tons(t)">tons(t)</asp:ListItem>
                                                    <asp:ListItem Value="metric tons (mt)">metric tons (mt)</asp:ListItem>
                                                    <asp:ListItem Value="pounds(lb)">pounds(lb)</asp:ListItem>
                                                    <asp:ListItem Value="ounces(oz)">ounces(oz)</asp:ListItem>
                                                    <asp:ListItem Value="centimeters(cm)">centimeters(cm)</asp:ListItem>
                                                    <asp:ListItem Value="meters(m)">meters(m)</asp:ListItem>
                                                    <asp:ListItem Value="kilometers(km)">kilometers(km)</asp:ListItem>
                                                    <asp:ListItem Value="inches(in)">inches(in)</asp:ListItem>
                                                    <asp:ListItem Value="feet(ft)">feet(ft)</asp:ListItem>
                                                    <asp:ListItem Value="yard(yd)">yard(yd)</asp:ListItem>
                                                    <asp:ListItem Value="miles(mi)">miles(mi)</asp:ListItem>
                                                    <asp:ListItem Value="square meters">square meters</asp:ListItem>
                                                    <asp:ListItem Value="square inches">square inches</asp:ListItem>
                                                    <asp:ListItem Value="square feets">square feets</asp:ListItem>
                                                    <asp:ListItem Value="cubic">cubic</asp:ListItem>
                                                    <asp:ListItem Value="cubic centimeters">cubic centimeters</asp:ListItem>
                                                    <asp:ListItem Value="cubic meters">cubic meters</asp:ListItem>
                                                    <asp:ListItem Value="cubic inches">cubic inches</asp:ListItem>
                                                    <asp:ListItem Value="cubic feets">cubic feets</asp:ListItem>
                                                    <asp:ListItem Value="cubic yards">cubic yards</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group" style="margin: 0">
                                                <label for="uname" class=" tetLable">
                                                    Estimated Price/Last Purchase Price (in Rs)
                                                </label>
                                                <asp:TextBox ID="txtestimatepriceu" runat="server" palceholder="Estimated Price/Last Purchase Price (in Rs)" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <div class="form-group" style="margin: 0">
                                                <asp:LinkButton ID="lblsub2" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lblsub2_Click"></asp:LinkButton>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="modal fade" id="divbank2" role="dialog" data-keyboard="false" data-backdrop="static">
                        <div class="modal-dialog" style="width: 400px;">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="modal-content">
                                        <div class="modal-header modal-header1">
                                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Estimated Quantity</h4>
                                        </div>
                                        <div class="modal-body clearfix" style="padding: 0 20px;">
                                            <div class="form-group" style="margin: 0">
                                                <label for="uname" class=" tetLable">
                                                    Year
                                                </label>
                                                <asp:DropDownList runat="server" ID="DropDownList3" Class="form-control">
                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">2017-18</asp:ListItem>
                                                    <asp:ListItem Value="2">2018-19</asp:ListItem>
                                                    <asp:ListItem Value="3">2019-20</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group" style="margin: 0">
                                                <label for="uname" class=" tetLable">
                                                    Estimated Quantity
                                                </label>
                                                <asp:TextBox runat="server" ID="TextBox3" placeholder="Estimated Quantity" onkeypress="return isNumberKey(event)" Class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group" style="margin: 0">
                                                <label for="uname" class=" tetLable">
                                                    Unit
                                                </label>
                                                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                    <asp:ListItem Value="number">number</asp:ListItem>
                                                    <asp:ListItem Value="sets">sets</asp:ListItem>
                                                    <asp:ListItem Value="milligrams(mg)">milligrams(mg)</asp:ListItem>
                                                    <asp:ListItem Value="grams(g)">grams(g)</asp:ListItem>
                                                    <asp:ListItem Value="kilograms(kg)">kilograms(kg),</asp:ListItem>
                                                    <asp:ListItem Value="tons(t)">tons(t)</asp:ListItem>
                                                    <asp:ListItem Value="metric tons (mt)">metric tons (mt)</asp:ListItem>
                                                    <asp:ListItem Value="pounds(lb)">pounds(lb)</asp:ListItem>
                                                    <asp:ListItem Value="ounces(oz)">ounces(oz)</asp:ListItem>
                                                    <asp:ListItem Value="centimeters(cm)">centimeters(cm)</asp:ListItem>
                                                    <asp:ListItem Value="meters(m)">meters(m)</asp:ListItem>
                                                    <asp:ListItem Value="kilometers(km)">kilometers(km)</asp:ListItem>
                                                    <asp:ListItem Value="inches(in)">inches(in)</asp:ListItem>
                                                    <asp:ListItem Value="feet(ft)">feet(ft)</asp:ListItem>
                                                    <asp:ListItem Value="yard(yd)">yard(yd)</asp:ListItem>
                                                    <asp:ListItem Value="miles(mi)">miles(mi)</asp:ListItem>
                                                    <asp:ListItem Value="square meters">square meters</asp:ListItem>
                                                    <asp:ListItem Value="square inches">square inches</asp:ListItem>
                                                    <asp:ListItem Value="square feets">square feets</asp:ListItem>
                                                    <asp:ListItem Value="cubic">cubic</asp:ListItem>
                                                    <asp:ListItem Value="cubic centimeters">cubic centimeters</asp:ListItem>
                                                    <asp:ListItem Value="cubic meters">cubic meters</asp:ListItem>
                                                    <asp:ListItem Value="cubic inches">cubic inches</asp:ListItem>
                                                    <asp:ListItem Value="cubic feets">cubic feets</asp:ListItem>
                                                    <asp:ListItem Value="cubic yards">cubic yards</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group" style="margin: 0">
                                                <label for="uname" class=" tetLable">
                                                    Estimated Price/Last Purchase Price (in Rs)
                                                </label>
                                                <asp:TextBox ID="TextBox4" runat="server" palceholder="Estimated Price/Last Purchase Price (in Rs)" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                            <div class="form-group" style="margin: 0">
                                                <asp:LinkButton ID="lblsub3" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lblsub3_Click"></asp:LinkButton>
                                            </div>
                                            <div class="clearfix mt10"></div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function showPopup1() {
            $('#divbank').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup2() {
            $('#divbank2').modal('show', function () {
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
    <script type="text/javascript">
        $(function () {
            $("[id$=txtoemname]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("AddProduct.aspx/GetOEMName") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
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
        });
    </script>
</asp:Content>
