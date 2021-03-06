﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_AddProduct" CodeFile="AddProduct.aspx.cs" ValidateRequest="false" MasterPageFile="~/Admin/MasterPage.master" ViewStateEncryptionMode="Always" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>

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

        .tox-notifications-container, .tox-statusbar {
            display: none !important;
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
                <p style="position: absolute; right: 35px;">Mark with <span class="mandatory">*</span> is mandatory field.</p>
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
                                                    <asp:HiddenField ID="dup" runat="server" />
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>
                                                                NATO SUPPLY GROUP <span class="mandatory">*</span>
                                                                <span data-toggle="tooltip" class="fa fa-question" title="Number in bracket indicates NATO SUPPLY GROUP"></span>
                                                            </label>
                                                            <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" Style="text-transform: uppercase !important;" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>
                                                            <span><a href="NATO Supply Group-Class.xlsx" class="fa fa-download mt5">&nbsp;Guide for selecting correct NATO Supply- Group Class.</a></span>
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
                                                            <div class="row" style="margin-top: 10px;">
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
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 row" runat="server">
                                                        <div class="form-group">
                                                            <label>Item Name</label>
                                                            <span class="mandatory">* (Editable)</span>  <span data-toggle="tooltip" class="fa fa-question" title="If item name is not relevant, edit the item name."></span>
                                                            <asp:TextBox runat="server" ID="txtproductdescription" Height="100px" MaxLength="250" TabIndex="5" AutoPostBack="true" class="form-control" OnTextChanged="txtproductdescription_TextChanged"></asp:TextBox>
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
                                                            <%-- <asp:TextBox runat="server" ID="txtfeaturesanddetails" Style="background-color: #fff !important;" TabIndex="1"
                                                                Width="1000" Height="70px" placeholder="Ductile,Tensile,Lusture" MaxLength="250"></asp:TextBox>
                                                            <asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txtfeaturesanddetails">
                                                            </asp:HtmlEditorExtender>--%>
                                                            <%--<asp:TextBox runat="server" ID="txtfeaturesanddetails" Style="background-color: #fff !important;" TabIndex="1"
                                                                Width="1000" Height="70px" placeholder="Ductile,Tensile,Lusture" MaxLength="250"></asp:TextBox>--%>
                                                            <%--  <asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txtfeaturesanddetails">
                                                            </asp:HtmlEditorExtender>--%>
                                                            <%--<script src='https://cdn.tiny.cloud/1/no-api-key/tinymce/5.4.2-90/tinymce.min.js'></script>--%>
                                                            <script src="../assets/js/tinymce.min.js"></script>
                                                            <form method='post'>
                                                                <asp:TextBox runat="server" ID="txtfeaturesanddetails" Width="100%" Height="100px" MaxLength="250"></asp:TextBox>
                                                            </form>
                                                            <script>
                                                                tinymce.init({ selector: '#ContentPlaceHolder1_txtfeaturesanddetails' });
                                                            </script>
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
                                                    <label>OEM Part Number</label><span></span>
                                                    <asp:TextBox runat="server" ID="txtoempartnumber" TabIndex="9" class="form-control"></asp:TextBox>
                                                    <span><b style="margin-top: 5px;">Available for general public viewing unless prohibited by specific contract clauses on Non Disclosure of information such as name of OEM</b></span>
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
                                            <div class="clearfix mt10"></div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>OEM Address</label>
                                                    <asp:TextBox runat="server" ID="txtoemaddress" Height="75px" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblenduser" Text="End User"></asp:Label>
                                                    <span class="mandatory">*</span>
                                                    <div class="clearfix"></div>
                                                    <asp:CheckBoxList ID="ddlenduser" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                                <ContentTemplate>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>DEFENCE PLATFORM<span class="mandatory">* </span></label>
                                                            <asp:DropDownList runat="server" ID="ddlplatform" AutoPostBack="True" TabIndex="13" Style="text-transform: uppercase !important;" class="form-control" OnSelectedIndexChanged="ddlplatform_SelectedIndexChanged"></asp:DropDownList>
                                                            <span><b style="margin-top: 5px;">Please get the correct or additional options inserted in drop down menu through category master by CIOs</b></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6" style="margin-top: 8px;">
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
                                                            <asp:DropDownList runat="server" ID="ddlsubtech" class="form-control" TabIndex="16" Style="text-transform: uppercase !important;"></asp:DropDownList>
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

                                                        <div class="col-sm-12">
                                                            <div class="form-group">
                                                                <label class="checkbox-box productalreadylabel">
                                                                    Imported During last 3 years  <span class="mandatory">*</span>
                                                                </label>
                                                                <asp:RadioButtonList runat="server" ID="rbproductImported" RepeatColumns="2" TabIndex="23" RepeatLayout="Flow"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Y" Selected="True" class="yes" style="margin-left: 10px;">Yes</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div class="clearfix mt5"></div>
                                                            <div>
                                                                <div class="section-pannel">
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                                                        <ContentTemplate>
                                                                            <table class="table table-responsive">
                                                                                <tr>
                                                                                    <th>Year</th>
                                                                                    <th>Imported Quantity <span class="mandatory">#</span></th>
                                                                                    <th>Unit</th>
                                                                                    <th>Imported value in Rs Lakh (Qty*Price) <span class="mandatory">*</span></th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:HiddenField ID="EstimateQunOldID4" runat="server" />
                                                                                        <asp:DropDownList ID="ddlyearestimate4" runat="server" CssClass="form-control">
                                                                                            <asp:ListItem Value="4">2020-21</asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtestquan4" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Imported Quantity (Only number allowed)"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlunit4" runat="server" CssClass="form-control">
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
                                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtpriceestimate4" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="Imported value in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:HiddenField ID="EstimateQunOldID3" runat="server" />
                                                                                        <asp:DropDownList ID="ddlyearestimate3" runat="server" CssClass="form-control">
                                                                                            <asp:ListItem Value="3">2019-20</asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtestquan3" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Imported Quantity (Only number allowed)"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlunit3" runat="server" CssClass="form-control">
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
                                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtpriceestimate3" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="Imported value in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:HiddenField ID="EstimateQunOldID2" runat="server" />
                                                                                        <asp:DropDownList ID="ddlyearestimate2" runat="server" CssClass="form-control">
                                                                                            <asp:ListItem Value="2">2018-19</asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtestquan2" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Imported Quantity (Only number allowed)"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlunit2" runat="server" CssClass="form-control">
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
                                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtpriceestimate2" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="Imported value in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr runat="server" visible="false">
                                                                                    <td>
                                                                                        <asp:HiddenField ID="EstimateQunOldID" runat="server" />
                                                                                        <asp:DropDownList ID="ddlyearestimate1" runat="server" CssClass="form-control">
                                                                                            <asp:ListItem Value="1">2017-18</asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtestquan1" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Imported Quantity (Only number allowed)"></asp:TextBox></td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlunit1" runat="server" CssClass="form-control">
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
                                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtpriceestimate1" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="Imported value in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                                                </tr>



                                                                            </table>
                                                                            <p class="pull-left mr10">
                                                                                <span class="mandatory">#Quantity may be entered as 0 if DPSU doesn't want to show
                                                                                quantity of the imported item on the public portal.
                                                                                </span>
                                                                                <div class="clearfix"></div>
                                                                                <span class="mandatory">&nbsp;&nbsp;#Do not copy paste Imported value in Rs Lakh (Qty*Price).
                                                                                </span>
                                                                            </p>
                                                                            <div class="clearfix mt5"></div>
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
                    <div id="qpt" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="section-pannel">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <h5>Future requirement next 5 years <span class="mandatory">#</span></h5>
                                                <table class="table table-responsive">
                                                    <tr>
                                                        <th>Year</th>
                                                        <th>Quantity <span class="mandatory"></span></th>
                                                        <th>Unit</th>
                                                        <th>Import Value as these are future values in Rs Lakh (Qty*Price) <span class="mandatory"></span></th>
                                                    </tr>
                                                    <tr runat="server" visible="false">
                                                        <td>
                                                            <asp:HiddenField ID="EstimateQunFutureID" runat="server" />
                                                            <asp:DropDownList ID="ddlfutyear1" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="1">2020-21</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutQuantity1" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Quantity (Only number allowed)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlfutunit1" runat="server" CssClass="form-control">
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
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutvalue1" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="Import Value as these are future values in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="EstimateQunFutureID2" runat="server" />
                                                            <asp:DropDownList ID="ddlfutyear2" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="2">2021-22</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutQuantity2" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Quantity (Only number allowed)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlfutunit2" runat="server" CssClass="form-control">
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
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutvalue2" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="Import Value as these are future values in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="EstimateQunFutureID3" runat="server" />
                                                            <asp:DropDownList ID="ddlfutyear3" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="3">2022-23</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutQuantity3" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Quantity (Only number allowed)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlfutunit3" runat="server" CssClass="form-control">
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
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutvalue3" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="Import Value as these are future values in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="EstimateQunFutureID4" runat="server" />
                                                            <asp:DropDownList ID="ddlfutyear4" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="4">2023-24</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutQuantity4" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Quantity (Only number allowed)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlfutunit4" runat="server" CssClass="form-control">
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
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutvalue4" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="Import Value as these are future values in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="EstimateQunFutureID5" runat="server" />
                                                            <asp:DropDownList ID="ddlfutyear5" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="5">2024-25</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutQuantity5" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Quantity (Only number allowed)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlfutunit5" runat="server" CssClass="form-control">
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
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutvalue5" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="Import Value as these are future values in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="EstimateQunFutureID7" runat="server" />
                                                            <asp:DropDownList ID="ddlfutyear7" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="6">2025-26</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutQuantity7" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Quantity (Only number allowed)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlfutunit7" runat="server" CssClass="form-control">
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
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutvalue7" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="Import Value as these are future values in Rs lakh (Qty*Price)"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="EstimateQunFutureID6" runat="server" />
                                                            <asp:DropDownList ID="ddlfutyear6" runat="server" CssClass="form-control" ToolTip="Probable future import (with tentative value in Rs lakh)">
                                                                <asp:ListItem Value="7">Probable future import (with tentative value in rupees lakh)</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutQuantity6" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKeyOutDecimal(event)" Placeholder="Quantity (Only number allowed)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlfutunit6" runat="server" CssClass="form-control">
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
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfutvalue6" oncopy="showError()" onpaste="showError()" onkeypress="return isNumberKey(event)" Placeholder="tentative value in Rs lakh"></asp:TextBox></td>
                                                    </tr>
                                                </table>

                                                <p class="pull-left mr10">
                                                    <span class="mandatory">#Quantity may be entered as 0 if DPSU doesn't want to show
                                                                                quantity of the imported item on the public portal.
                                                    </span>
                                                </p>
                                                <div class="clearfix mt5"></div>
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
                                            <div class="col-sm-12">
                                                <h4>Status of Indigenization <span class="mandatory">*</span></h4>
                                            </div>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                                <ContentTemplate>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <h5>Starting Indigenization Target Year <span class="mandatory">*</span></h5>
                                                            <asp:RadioButtonList ID="chkinditargetyear" runat="server" RepeatColumns="6" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                <asp:ListItem Value="NIL" style="margin-left: 10px;">&nbsp;NIL</asp:ListItem>
                                                                <%--<asp:ListItem Value="2020-21" style="margin-left: 10px;">&nbsp;2020-21</asp:ListItem>--%>
                                                                <asp:ListItem Value="2021-22" style="margin-left: 10px;">&nbsp;2021-22</asp:ListItem>
                                                                <asp:ListItem Value="2022-23" style="margin-left: 10px;">&nbsp;2022-23</asp:ListItem>
                                                                <asp:ListItem Value="2023-24" style="margin-left: 10px;">&nbsp;2023-24</asp:ListItem>
                                                                <asp:ListItem Value="2024-25" style="margin-left: 10px;">&nbsp;2024-25</asp:ListItem>
                                                                <asp:ListItem Value="2025-26" style="margin-left: 10px;">&nbsp;2025-26</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <h5>Indigenization Process started</h5>
                                                                    <asp:RadioButtonList ID="chkindiprocstart" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                        RepeatLayout="Flow" OnSelectedIndexChanged="chkindiprocstart_SelectedIndexChanged">
                                                                        <asp:ListItem Value="Yes" style="margin-left: 10px;" Selected="True">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="No" style="margin-left: 10px;">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="clearfix mt10"></div>

                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group" runat="server" id="indicatchk">
                                                                    <h5>Make in India Category</h5>
                                                                    <asp:RadioButtonList ID="rbIgCategory" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="clearfix mt10"></div>
                                                                <div class="form-group">
                                                                    <h5>Quality Assurance Agency <span class="mandatory">*</span></h5>
                                                                    <asp:CheckBoxList ID="chkQAA" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="clearfix mt10"></div>
                                                            </div>


                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress11" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
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
                                    <div class="section-pannel2">
                                        <div class="row">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                                <ContentTemplate>
                                                    <div class="section-pannel row" style="margin: 14px;">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <h5>EoI/RFP </h5>
                                                                <asp:RadioButtonList ID="rbeoimake2" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="rbeoimake2_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Yes" style="margin-left: 10px;">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No" Selected="True" style="margin-left: 10px;">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div runat="server" id="eoi" visible="false">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group" runat="server">
                                                                        <asp:RadioButtonList ID="rbleoi" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="Archive" style="margin-left: 10px;">Archive</asp:ListItem>
                                                                            <asp:ListItem Value="Active" style="margin-left: 10px;">Active</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group" runat="server">
                                                                        <label>Link</label>
                                                                        (if yes) <span class="mandatory">*</span>
                                                                        <asp:TextBox ID="txteoilink" runat="server" CssClass="form-control" placeholder="Link of eoi/rfp tender"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label>Start Date <span class="mandatory">*</span></label>
                                                                        <asp:TextBox ID="txteoistartdate" runat="server" CssClass="form-control datepicker"
                                                                            autocomplete="off" placeholder="Start date (format:- 01-jan-1900)"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <label>End Date <span class="mandatory">*</span> </label>
                                                                        <asp:TextBox ID="txteoienddate" runat="server" CssClass="form-control datepicker" autocomplete="off"
                                                                            placeholder="End date (format:- 01-jan-1900)"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="clearfix"></div>
                                                            <div class="form-group">
                                                                <h5>Supply Order Placed </h5>
                                                                <asp:RadioButtonList ID="rbSuuplyOrder" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                                    AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="rbSuuplyOrder_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Yes" style="margin-left: 10px;">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No" Selected="True" style="margin-left: 10px;">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div class="clearfix"></div>
                                                            <div runat="server" id="supplyorder" visible="false">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label>Manufacture Name <span class="mandatory">*</span></label>
                                                                        <asp:TextBox ID="txtsupplumanufacturename" runat="server" CssClass="form-control" placeholder="Manufacture Name"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>Supply Order Value in (Rs Lakhs) <span class="mandatory">*</span></label>
                                                                        <asp:TextBox ID="txtsupplyorderrplkh" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"
                                                                            placeholder="Supply Order Value in (Rs Lakhs)"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>Delivery (Compliance Date)<span class="mandatory">*</span></label>
                                                                        <%--  <asp:TextBox ID="txtdeliverycompdate" runat="server" CssClass="form-control" type="date"
                                                                        placeholder="Delivery (Compliance Date)"></asp:TextBox>--%>
                                                                        <asp:TextBox runat="server" ID="txtdeliverycompdate" placeholder="Delivery (Compliance Date) (format:- 01-jan-1900)"
                                                                            autocomplete="off" Class="form-control datepicker"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label>Address <span class="mandatory">*</span></label>
                                                                        <asp:TextBox ID="txtsupplymanufactureaddress" runat="server" CssClass="form-control" TextMode="MultiLine" Height="125px" placeholder="Address (Max length 250 words)"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>Supply Order Date<span class="mandatory">*</span></label>
                                                                        <asp:TextBox ID="txtsodate" runat="server" autocomplete="off" CssClass="form-control datepicker"
                                                                            placeholder="Supply Order Date (format:- 01-jan-1900)"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divsanctionproject" runat="server" visible="false">
                                                                <h5>Project Sanction order (Nil Value) </h5>
                                                                <asp:RadioButtonList ID="RBLProjectSanction" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="RBLProjectSanction_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Yes" style="margin-left: 10px;">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No" Selected="True" style="margin-left: 10px;">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div class="clearfix"></div>
                                                            <div id="divrblsanction" runat="server" class="form-group" visible="false">

                                                                <asp:RadioButtonList ID="Rblsanction" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="RBLProjectSanction_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Nil Value project sanction order(FC-NC)" style="margin-left: 10px;">Nil Value project sanction order(FC-NC)</asp:ListItem>
                                                                    <asp:ListItem Value="NC-NC Order" style="margin-left: 10px;">NC-NC Order</asp:ListItem>
                                                                    <asp:ListItem Value="Development Order" style="margin-left: 10px;">Development Order</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div runat="server" id="divsanction" visible="false">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label>Project Manufacture Name <span class="mandatory">*</span></label>
                                                                        <asp:TextBox ID="txtnamesanction" runat="server" CssClass="form-control" placeholder="Manufacture Name"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>Order Date<span class="mandatory">*</span></label>
                                                                        <asp:TextBox ID="Txtorderdate" runat="server"
                                                                            placeholder="Sanction Order Date (format:- 01-jan-1900)" autocomplete="off" Class="form-control datepicker"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label>Project Address <span class="mandatory">*</span></label>
                                                                        <asp:TextBox ID="TxtAddresssanction" runat="server" CssClass="form-control" TextMode="MultiLine" Height="110px" placeholder="Address (Max length 250 words)"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <h5>
                                                                    <b>Item indigenized </b></h5>
                                                                <asp:RadioButtonList runat="server" ID="rbisindinised" RepeatColumns="3" TabIndex="19" RepeatLayout="Flow"
                                                                    RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbisindinised_CheckedChanged ">
                                                                    <asp:ListItem Value="Y" class="yes" style="margin-left: 10px;">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="N" Selected="True" style="margin-left: 10px;">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <div class="clearfix" style="margin-top: 10px;"></div>
                                                                <div runat="server" class="col-sm-12 mt10 row" id="divisIndigenized" visible="false">
                                                                    <div class="form-group">
                                                                        <label>Enter Manufacturer name</label>
                                                                        <asp:TextBox runat="server" ID="txtmanufacturename" TabIndex="20" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>Address</label>
                                                                        <asp:TextBox runat="server" ID="txtmanifacaddress" MaxLength="250" Height="135px" placeholder="Max Length 250 words only." TextMode="MultiLine" TabIndex="21" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>Year of Indiginization</label>
                                                                        <asp:DropDownList runat="server" ID="ddlyearofindiginization" Height="35px" TabIndex="22" class="form-control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="clearfix mt10"></div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
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
                                    <asp:UpdatePanel runat="server" ID="mmm" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <div class="section-pannel" runat="server" id="DivAtmnirbhar" visible="false">
                                                <h4>Atmnirbhar Data</h4>
                                                <asp:HiddenField runat="server" ID="hfvaluelast3years" />
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <label>Month</label>
                                                        <asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="Select">Month</asp:ListItem>
                                                            <asp:ListItem Value="Jan">January</asp:ListItem>
                                                            <asp:ListItem Value="Feb">Feburay</asp:ListItem>
                                                            <asp:ListItem Value="Mar">march</asp:ListItem>
                                                            <asp:ListItem Value="Apr">April</asp:ListItem>
                                                            <asp:ListItem Value="May">May</asp:ListItem>
                                                            <asp:ListItem Value="Jun">June</asp:ListItem>
                                                            <asp:ListItem Value="Jul">July</asp:ListItem>
                                                            <asp:ListItem Value="Aug">August</asp:ListItem>
                                                            <asp:ListItem Value="Sep">September</asp:ListItem>
                                                            <asp:ListItem Value="Oct">Octomber</asp:ListItem>
                                                            <asp:ListItem Value="Nov">November</asp:ListItem>
                                                            <asp:ListItem Value="Dec">Decomber</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Year</label>
                                                        <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">Year</asp:ListItem>
                                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                                            <asp:ListItem Value="2021">2021</asp:ListItem>
                                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                                            <asp:ListItem Value="2023">2023</asp:ListItem>
                                                            <asp:ListItem Value="2024">2024</asp:ListItem>
                                                            <asp:ListItem Value="2025">2025</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Approx Annual Negated Value in Rs (Lakh)</label>
                                                        <asp:TextBox runat="server" ID="txtmaxvalue" Text="0" ToolTip="This is your max value of indigenized year you can 
                                                                                change it if it is not according to you"
                                                            CssClass="form-control" onkeypress="return isNumberKey(event)" placeholder="Indigenized Max Value"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                                                        <asp:ListItem Value="No IPR issue" Selected="True">1. No IPR issue</asp:ListItem>
                                                                        <asp:ListItem Value="No violation of TOT agreement" Selected="True">2. No violation of TOT agreement</asp:ListItem>
                                                                        <asp:ListItem Value="No violation of Security Concern" Selected="True">3. No violation of Security Concern</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>
                                                                    <span class="mandatory">*</span> Product is eligible to be displayed for general viewing (without registration) and hereby respective DPSU/OFB/SHQ provides consent 
                                                                    for onward display of the relevant information at srijandefence.gov.in..
                                                                </label>
                                                                <div class="fr">
                                                                    <asp:RadioButtonList ID="rbeligible" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                        <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="N" style="margin-left: 10px;">No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>
                                                                    View Only Reason
                                                                </label>
                                                                <div class="fr">
                                                                    <asp:RadioButtonList ID="rbViewOnlyStatus" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                        <asp:ListItem Value="ITEM NOT REQUIRE">&nbsp;ITEM NOT REQUIRE</asp:ListItem>
                                                                        <asp:ListItem Value="INDIGENIZATION WITH OTHER" style="margin-left: 10px;">&nbsp;INDIGENIZATION WITH OTHER</asp:ListItem>
                                                                        <%--   <asp:ListItem Value="ITEM OF SMALL QUANTITY" style="margin-left: 10px;">&nbsp;ITEM OF SMALL QUANTITY</asp:ListItem>
                                                                        <asp:ListItem Value="ITEM ONE TIME BUY" style="margin-left: 10px;">&nbsp;ITEM ONE TIME BUY</asp:ListItem>
                                                                        <asp:ListItem Value="INTEREST NOTED FOR FUTURE" style="margin-left: 10px;">&nbsp;INTEREST NOTED FOR FUTURE</asp:ListItem>--%>
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
                    <asp:HiddenField runat="server" ID="hfprodid" />
                    <asp:HiddenField runat="server" ID="hfprodrefno" />
                    <asp:HiddenField runat="server" ID="hfcomprefno" />
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
        function isNumberKeyOutDecimal(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <script>
        function showError() {
            alert('you are not allowed to cut,copy or paste here');
        }
        $('.form-control').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtdeliverycompdate').datepicker({
                dateFormat: "dd-M-yy"
            });
            $('#ContentPlaceHolder1_txteoistartdate').datepicker({
                dateFormat: "dd-M-yy"
            });
            $('#ContentPlaceHolder1_txteoienddate').datepicker({
                dateFormat: "dd-M-yy"
            });
            $('#ContentPlaceHolder1_txtsodate').datepicker({
                dateFormat: "dd-M-yy"
            });
            $('#ContentPlaceHolder1_Txtorderdate').datepicker({
                dateFormat: "dd-M-yy"
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $('#ContentPlaceHolder1_txtdeliverycompdate').datepicker({
                    dateFormat: "dd-M-yy"
                });
                $('#ContentPlaceHolder1_txteoistartdate').datepicker({
                    dateFormat: "dd-M-yy"
                });
                $('#ContentPlaceHolder1_txteoienddate').datepicker({
                    dateFormat: "dd-M-yy"
                });
                $('#ContentPlaceHolder1_txtsodate').datepicker({
                    dateFormat: "dd-M-yy"
                });
                $('#ContentPlaceHolder1_Txtorderdate').datepicker({
                    dateFormat: "dd-M-yy"
                });
            });
        });
    </script>


</asp:Content>
