<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="Admin_AddProduct" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <!----------------------------------jquery Show image on load------------------------------------------------>

    <style>
        .gallery img {
            width: 100px;
            margin-right: 10px;
            border: 2px solid #333;
        }
    </style>
    <!-------------------------------------------image show end------------------------------->
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
    <asp:HiddenField ID="hidType" runat="server" />
    <div class="content oem-content">
        <div class="sideBg">

            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
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
                    <li class="active"><a data-toggle="tab" href="#pd">Description</a></li>
                    <li><a data-toggle="tab" href="#pimg">Image</a></li>
                    <li><a data-toggle="tab" href="#test">Testing</a></li>
                    <li><a data-toggle="tab" href="#cer">Certification</a></li>
                    <li><a data-toggle="tab" href="#spd">Technical Support</a></li>
                    <li><a data-toggle="tab" href="#spdpfin">Financial Support</a></li>
                    <li><a data-toggle="tab" href="#qpt">Quantity Required</a></li>
                    <li><a data-toggle="tab" href="#tnd">Tender</a></li>
                    <li><a data-toggle="tab" href="#cd">Contact</a></li>
                </ul>
                <div class="tab-content">
                    <asp:HiddenField runat="server" ID="hfprodid" />
                    <asp:HiddenField runat="server" ID="hfprodrefno" />
                    <asp:HiddenField runat="server" ID="hfcomprefno" />

                    <div id="pd" class="tab-pane fade in active">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="section-pannel">
                                        <asp:UpdatePanel runat="server" ID="upproduct">
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
                                                            <div class="col-md-12 padding_0">
                                                                <label>Nato Stock Number (NSN)</label>
                                                            </div>
                                                            <div class="col-sm-3 padding_0">
                                                                <label style="font-size: 14px !important;">
                                                                    NSC Code (4 digit)
                                                                        <span data-toggle="tooltip" class="fa fa-question" title="NSC Code = NSN Group (2 digit) + NSN Group Class (2 digit)"></span>
                                                                </label>
                                                                <asp:TextBox runat="server" ID="txtnsccode" ReadOnly="True" MaxLength="4" TabIndex="4" CssClass="form-cascade-control form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-9 padding_0">
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
                                                            <label>Item Description </label>
                                                            <span class="mandatory">* (Editable)</span>  <span data-toggle="tooltip" class="fa fa-question" title="If item description is not relevant, edit the item description."></span>
                                                            <asp:TextBox runat="server" ID="txtproductdescription" required="" TextMode="MultiLine" TabIndex="6" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Upload document releted to item </label>
                                                    <span class="mandatory">(Only pdf file)</span>
                                                    <asp:FileUpload runat="server" ID="fuitemdescriptionfile" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
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
                                                    <asp:DropDownList ID="ddlcountry" runat="server" TabIndex="9" Style="text-transform: uppercase !important;" CssClass="form-control form-cascade-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblenduser" Text="End User"></asp:Label><span class="mandatory"> *</span>
                                                    <asp:DropDownList runat="server" ID="ddlenduser" Style="text-transform: uppercase !important;" class="form-control" TabIndex="10">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>End User Part Number</label>
                                                    <asp:TextBox runat="server" ID="txtenduserpartnumber" TabIndex="11" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>DPSU Part Number</label>
                                                    <asp:TextBox runat="server" ID="txtdpsupartnumber" TabIndex="12" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>HSN Code</label>
                                                    <asp:TextBox runat="server" ID="txthsncode" TabIndex="13" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>DPSU Reference No</label>
                                                    <asp:TextBox runat="server" ID="txterprefno" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <asp:UpdatePanel runat="server" ID="uptechnology">
                                                <ContentTemplate>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>DEFENCE PLATFORM<span class="mandatory">*</span></label>
                                                            <asp:DropDownList runat="server" ID="ddlplatform" AutoPostBack="True" TabIndex="17" Style="text-transform: uppercase !important;" class="form-control" OnSelectedIndexChanged="ddlplatform_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" style="margin-top: 8px;">
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblNomenclature" Text="NAME OF DEFENCE PLATFORM"></asp:Label><span class="mandatory"> *</span>
                                                            <asp:DropDownList runat="server" ID="ddlnomnclature" class="form-control" Style="text-transform: uppercase !important;" TabIndex="18" />
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>PRODUCT (INDUSTRY DOMAIN)<span class="mandatory">*</span></label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="If you not display your category in this section, please add in Category Master >> Category Dropdown"></span>
                                                            <asp:DropDownList runat="server" ID="ddltechnologycat" class="form-control" TabIndex="14" Style="text-transform: uppercase !important;" AutoPostBack="True" OnSelectedIndexChanged="ddltechnologycat_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>PRODUCT (INDUSTRY SUB DOMAIN)<span class="mandatory">*</span></label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Product Level 1, if you not see product level 2 please add in Category master >> level 2 "></span>
                                                            <asp:DropDownList runat="server" ID="ddlsubtech" class="form-control" TabIndex="15" Style="text-transform: uppercase !important;" AutoPostBack="True" OnSelectedIndexChanged="ddlsubtech_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>PRODUCT (INDUSTRY 2nd SUB DOMAIN)</label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Product Level 2, if you not see product level 3 please add in Category master >> level 3 "></span>
                                                            <asp:DropDownList runat="server" ID="ddltechlevel3" TabIndex="16" Style="text-transform: uppercase !important;" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
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
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>PROCURMENT CATEGORY<span class="mandatory">*</span></label>
                                                    <asp:DropDownList runat="server" ID="ddlprocurmentcategory" TabIndex="20" Style="text-transform: uppercase !important;" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="Label1" CssClass="form-label " Text="PROCURMENT CATEGORY REMARK"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtremarkspro" TabIndex="21" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="Div1" class="col-md-4" runat="server" visible="False">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblprodrequir" CssClass="form-label " Text="Procurement Time Frame"></asp:Label><span class="mandatory"> *</span>
                                                    <asp:DropDownList runat="server" ID="ddlproctimeframe" TabIndex="22" Style="text-transform: uppercase !important;" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <asp:UpdatePanel runat="server" ID="upindwginized">
                                                <ContentTemplate>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label class="live-status-box productalreadylabel ">
                                                                Product already indigenized :
                                                                <asp:RadioButtonList runat="server" ID="rbisindinised" RepeatColumns="2" TabIndex="23" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbisindinised_CheckedChanged ">
                                                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                                    <asp:ListItem Value="Y" class="yes">Yes</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </label>
                                                            <div class="clearfix" style="margin-top: 10px;"></div>
                                                            <div class="row">
                                                                <div runat="server" id="divisIndigenized" visible="False">
                                                                    <div class="col-sm-4">
                                                                        <label>Enter Manufacturer name</label>
                                                                        <asp:TextBox runat="server" ID="txtmanufacturename" TabIndex="24" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <label>Address</label>
                                                                        <asp:TextBox runat="server" ID="txtmanifacaddress" TabIndex="25" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <label>Year of Indiginization</label>
                                                                        <asp:DropDownList runat="server" ID="ddlyearofindiginization" TabIndex="26" class="form-control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Search keywords (To add more than one search keyword please use comma(,))</label>
                                                            <asp:TextBox runat="server" ID="txtsearchkeyword" TextMode="MultiLine" TabIndex="27" class="form-control"></asp:TextBox>
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
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="pimg" class="tab-pane fade in">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Product Image</label>
                                        <div class="fr">
                                            <asp:FileUpload ID="files" runat="server" CssClass="uploadimage form-control" TabIndex="28" type="file" name="image_file_arr[]" Multiple="Multiple" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!-------uplode photo----------->
                            <div class="gallery"></div>
                            <div class="clearfix" style="margin-top: 5px;"></div>
                            <span>Only .jpeg, .png, .jpg files of  maximum 1 Mb can be uploaded.(Maximum 4 files)</span>
                            <br />
                            <div runat="server" id="divimgdel" visible="False">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:DataList runat="server" ID="dlimage" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemCommand="dlimage_ItemCommand">
                                            <ItemTemplate>
                                                <div class="col-sm-3">
                                                    <asp:Image runat="server" ID="imgprodimage" class="image img-responsive img-rounded" Height="120px" Width="120" src='<%#Eval("ImageName") %>' />
                                                    <div class="clearfix"></div>
                                                    <asp:LinkButton runat="server" ID="lblremoveimg" class="fa fa-trash text-center" CommandName="removeimg" CommandArgument='<%#Eval("ImageId") %>'></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="spd" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView runat="server" ID="gvservices" AutoGenerateColumns="False" class=" table responsive no-wrap table-hover manage-user Grid">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Confirm if support provided by DPSU">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chk" TabIndex="29" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblservices" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                <asp:HiddenField runat="server" ID="hfservicesid" Value='<%#Eval("SCategoryId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtRemarks" TabIndex="30" class="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="spdpfin" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView runat="server" ID="gvfinancialsupp" AutoGenerateColumns="False" class=" table responsive no-wrap table-hover manage-user Grid">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Confirm if support provided by DPSU">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkfinan" TabIndex="31" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblfinancialservices" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                <asp:HiddenField runat="server" ID="hffinanciailserviceid" Value='<%#Eval("SCategoryId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtfinancialRemarks" TabIndex="32" class="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="qpt" class="tab-pane fade">
                        <div class="section-pannel">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Estimated Quantity</label>
                                                <asp:TextBox runat="server" ID="txtestimatequantity" TabIndex="33" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Estimated Price / LPP</label>
                                                <asp:TextBox runat="server" ID="txtestimateprice" TabIndex="34" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
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
                    </div>
                    <div id="tnd" class="tab-pane fade">
                        <div class="section-pannel">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group ">
                                                <label>
                                                    Tender Status</label>
                                                <asp:DropDownList runat="server" ID="ddltendorstatus" class="form-control" TabIndex="35" AutoPostBack="True" OnSelectedIndexChanged="ddltendorstatus_SelectedIndexChanged">
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
                                                <asp:RadioButtonList runat="server" ID="rbtendordateyesno" RepeatDirection="Horizontal" TabIndex="36" AutoPostBack="True" RepeatColumns="2" RepeatLayout="Flow" OnSelectedIndexChanged="rbtendordateyesno_CheckedChanged">
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
                                                    <asp:TextBox runat="server" ID="txttendordate" type="date" TabIndex="37" class="form-control inputbox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Tender URL</label>
                                                    <asp:TextBox runat="server" ID="txttendorurl" TabIndex="38" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
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
                    <div id="cd" class="tab-pane fade">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <div class="section-pannel" runat="server" id="divnodal">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 class="page-header secondary">Contact Detail 1  </h4>
                                            <div class="form-group contactD1Select">
                                                <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail" class="form-control" TabIndex="39" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="contactFormRow" runat="server" id="contactpanel1" visible="False">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Employee Code</label>
                                                    <asp:TextBox runat="server" ID="txtempcode" name="" Enabled="false" TabIndex="40" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Designation</label>
                                                    <asp:TextBox runat="server" ID="txtDesignation" name="" Enabled="false" TabIndex="41" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>E-Mail ID</label>
                                                    <asp:TextBox runat="server" ID="txtNEmailId" name="" Enabled="false" TabIndex="42" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Mobile Number</label>
                                                    <asp:TextBox runat="server" ID="txtmobnodal" Enabled="false" TabIndex="43" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtNTelephone" name="" Enabled="false" TabIndex="44" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Fax</label>
                                                    <asp:TextBox runat="server" ID="txtNFaxNo" name="" Enabled="false" TabIndex="45" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
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
                                                <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail2" class="form-control" TabIndex="46" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail2_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="contactFormRow" runat="server" id="contactpanel2" visible="False">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Employee Code</label>
                                                    <asp:TextBox runat="server" ID="txtempcode2" name="" Enabled="false" TabIndex="47" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Designation</label>
                                                    <asp:TextBox runat="server" ID="txtdesignationnodal2" name="" TabIndex="48" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>E-Mail ID</label>
                                                    <asp:TextBox runat="server" ID="txtNEmailId2" name="" Enabled="false" TabIndex="49" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Mobile Number</label>
                                                    <asp:TextBox runat="server" ID="txtmobnodal2" name="" Enabled="false" TabIndex="50" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtNTelephone2" name="" Enabled="false" TabIndex="51" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Fax</label>
                                                    <asp:TextBox runat="server" ID="txtNFaxNo2" name="" Enabled="false" TabIndex="52" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
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
                    <div id="test" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView runat="server" ID="gvtesting" AutoGenerateColumns="False" class=" table responsive no-wrap table-hover manage-user Grid">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Confirm if testing needed">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chktesting" TabIndex="53" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbltesting" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                <asp:HiddenField runat="server" ID="hftestingid" Value='<%#Eval("SCategoryId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txttestingRemarks" TabIndex="54" class="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="cer" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView runat="server" ID="gvCertification" AutoGenerateColumns="False" class=" table responsive no-wrap table-hover manage-user Grid">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Confirm if certification needed">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkcertification" TabIndex="55" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblcertification" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                <asp:HiddenField runat="server" ID="hfcertification" Value='<%#Eval("SCategoryId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtCertificationRemarks" TabIndex="56" class="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsubmitpanel1">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:UpdatePanel runat="server" ID="UPSUBMIT">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnsubmitpanel1" class="btn btn-primary pull-right" TabIndex="57" Text="Save" OnClick="btnsubmitpanel1_Click" OnClientClick="return confirm('Are you sure you want to save this product?');" />
                                            <asp:Button runat="server" Visible="false" ID="btncancelpanel1" class="btn btn-default pull-right" TabIndex="58" Style="margin-right: 10px;" Text="Back" OnClick="btncancelpanel1_Click" />
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
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
    </script>
</asp:Content>
