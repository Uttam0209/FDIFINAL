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
                    <li><a data-toggle="tab" href="#spd">Support by DPSU</a></li>
                    <li><a data-toggle="tab" href="#qpt">Quantity Required</a></li>
                    <li><a data-toggle="tab" href="#tnd">Tender</a></li>
                    <li><a data-toggle="tab" href="#cd">Contact</a></li>
                    <li><a data-toggle="tab" href="#test">Testing</a></li>
                    <li><a data-toggle="tab" href="#cer">Certification</a></li>
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
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Part Number <%--<span class="red">*</span>--%></label>
                                                    <asp:TextBox runat="server" ID="txtoempartnumber" TabIndex="1" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>DPSU Part Number</label>
                                                    <asp:TextBox runat="server" ID="txtdpsupartnumber" TabIndex="2" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>End User Part Number</label>
                                                    <asp:TextBox runat="server" ID="txtenduserpartnumber" TabIndex="3" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>HSN Code</label>
                                                    <asp:TextBox runat="server" ID="txthsncode" TabIndex="4" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>NSN Code</label>
                                                    <asp:TextBox runat="server" ID="txtnatocode" TabIndex="5" class="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-md-4" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>DPSU Reference No</label>
                                                    <asp:TextBox runat="server" ID="txterprefno" TabIndex="6" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <asp:UpdatePanel runat="server" ID="upproduct">
                                                <ContentTemplate>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>
                                                                NSN GROUP <span class="mandatory">*</span>
                                                                <span data-toggle="tooltip" class="fa fa-question" title="If you not display your category in this section, please add in Category Master >> Category Dropdown"></span>
                                                            </label>
                                                            <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>NSN GROUP CLASS<span class="mandatory">*</span></label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Product Level 1, if you not see product level 2 please add in Category master >> level 2 "></span>
                                                            <asp:DropDownList runat="server" ID="ddlsubcategory" AutoPostBack="True" TabIndex="8" class="form-control" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>
                                                                CLASS ITEM</label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Product Level 2, if you not see product level 3 please add in Category master >> level 3 "></span>
                                                            <asp:DropDownList runat="server" ID="ddllevel3product" TabIndex="9" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Product Description </label>
                                                    <asp:TextBox runat="server" ID="txtproductdescription" TextMode="MultiLine" TabIndex="10" class="form-control"></asp:TextBox>
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
                                                            <label>Technology Level 1 <span class="mandatory">*</span></label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="If you not display your category in this section, please add in Category Master >> Category Dropdown"></span>
                                                            <asp:DropDownList runat="server" ID="ddltechnologycat" class="form-control" TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddltechnologycat_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Technology Level 2 <span class="mandatory">*</span></label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Product Level 1, if you not see product level 2 please add in Category master >> level 2 "></span>
                                                            <asp:DropDownList runat="server" ID="ddlsubtech" class="form-control" TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="ddlsubtech_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Technology Level 3</label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="It is a subcategory of Product Level 2, if you not see product level 3 please add in Category master >> level 3 "></span>
                                                            <asp:DropDownList runat="server" ID="ddltechlevel3" TabIndex="13" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblNomenclature" Text="Nomenclature of main system"></asp:Label><span class="mandatory"> *</span>
                                                    <asp:DropDownList runat="server" ID="ddlnomnclature" class="form-control" TabIndex="14" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblenduser" Text="End User"></asp:Label><span class="mandatory"> *</span>
                                                    <asp:DropDownList runat="server" ID="ddlenduser" class="form-control" TabIndex="15">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <asp:UpdatePanel runat="server" ID="upplatform">
                                                <ContentTemplate>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Platform <span class="mandatory">*</span></label>
                                                            <asp:DropDownList runat="server" ID="ddlplatform" TabIndex="16" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Purpose of Procurement <span class="mandatory">*</span></label>
                                                            <asp:DropDownList runat="server" ID="ddlplatformsubcat" TabIndex="17" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblprodrequir" CssClass="form-label " Text="Procurement Time Frame"></asp:Label><span class="mandatory"> *</span>
                                                    <asp:DropDownList runat="server" ID="ddlprodreqir" TabIndex="18" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Search Keywords (Add more then one search criteria please use ,)</label>
                                                    <asp:TextBox runat="server" ID="txtsearchkeyword" TabIndex="19" class="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                            <asp:UpdatePanel runat="server" ID="upindwginized">
                                                <ContentTemplate>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="live-status-box productalreadylabel ">
                                                                Product Already Indeginized :
                                                               <asp:RadioButtonList runat="server" ID="rbisindinised" RepeatColumns="2" TabIndex="20" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbisindinised_CheckedChanged ">
                                                                   <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                                   <asp:ListItem Value="Y" class="yes">Yes</asp:ListItem>
                                                               </asp:RadioButtonList>
                                                            </label>
                                                            <asp:TextBox runat="server" ID="txtmanufacturename" placeholder="Enter Manufacturer name" TabIndex="21" Visible="False" class="form-control Turl_Tdate"></asp:TextBox>
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
                    <div id="pimg" class="tab-pane fade in">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Product Image</label>
                                        <div class="fr">
                                            <asp:FileUpload ID="files" runat="server" CssClass="uploadimage form-control" TabIndex="22" type="file" name="image_file_arr[]" Multiple="Multiple" />
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
                                                <asp:CheckBox runat="server" ID="chk" TabIndex="23" />
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
                                                <asp:TextBox runat="server" ID="txtRemarks" TabIndex="24" class="form-control"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="txtestimatequantity" TabIndex="25" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Estimated Price / LPP</label>
                                                <asp:TextBox runat="server" ID="txtestimateprice" TabIndex="26" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                                                <asp:DropDownList runat="server" ID="ddltendorstatus" class="form-control" TabIndex="27" AutoPostBack="True" OnSelectedIndexChanged="ddltendorstatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="Live">Live</asp:ListItem>
                                                    <asp:ListItem Value="Archive">Archive</asp:ListItem>
                                                    <asp:ListItem Value="Not Floated">Not Floated</asp:ListItem>
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
                                                <asp:RadioButtonList runat="server" ID="rbtendordateyesno" RepeatDirection="Horizontal" TabIndex="28" AutoPostBack="True" RepeatColumns="2" RepeatLayout="Flow" OnSelectedIndexChanged="rbtendordateyesno_CheckedChanged">
                                                    <asp:ListItem Value="N" runat="server">No</asp:ListItem>
                                                    <asp:ListItem Value="Y" class="yes" Selected="True">Yes</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="Turl_Tdate" runat="server" visible="False" id="divtdate">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Last date of tender submission</label>
                                                    <asp:TextBox runat="server" ID="txttendordate" type="date" TabIndex="29" class="form-control inputbox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Tender URL</label>
                                                    <asp:TextBox runat="server" ID="txttendorurl" TabIndex="30" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                                                <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail" class="form-control" TabIndex="31" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="contactFormRow" runat="server" id="contactpanel1" visible="False">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Employee Code</label>
                                                    <asp:TextBox runat="server" ID="txtempcode" name="" Enabled="false" TabIndex="32" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Designation</label>
                                                    <asp:TextBox runat="server" ID="txtDesignation" name="" Enabled="false" TabIndex="33" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>E-Mail ID</label>
                                                    <asp:TextBox runat="server" ID="txtNEmailId" name="" Enabled="false" TabIndex="34" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Mobile Number</label>
                                                    <asp:TextBox runat="server" ID="txtmobnodal" Enabled="false" TabIndex="35" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtNTelephone" name="" Enabled="false" TabIndex="36" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Fax</label>
                                                    <asp:TextBox runat="server" ID="txtNFaxNo" name="" Enabled="false" TabIndex="37" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel" runat="server" id="divnodal2">
                                        <div class="row">
                                            <h4 class="page-header secondary">Contact Detail 2</h4>
                                            <div class="form-group select-box">
                                                <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail2" class="form-control" TabIndex="38" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail2_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="contactFormRow" runat="server" id="contactpanel2" visible="False">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Employee Code</label>
                                                        <asp:TextBox runat="server" ID="txtempcode2" name="" Enabled="false" TabIndex="39" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Designation</label>
                                                        <asp:TextBox runat="server" ID="txtdesignationnodal2" name="" TabIndex="40" Enabled="false" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>E-Mail ID</label>
                                                        <asp:TextBox runat="server" ID="txtNEmailId2" name="" Enabled="false" TabIndex="41" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Mobile Number</label>
                                                        <asp:TextBox runat="server" ID="txtmobnodal2" name="" Enabled="false" TabIndex="42" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Phone Number</label>
                                                        <asp:TextBox runat="server" ID="txtNTelephone2" name="" Enabled="false" TabIndex="43" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Fax</label>
                                                        <asp:TextBox runat="server" ID="txtNFaxNo2" name="" Enabled="false" TabIndex="44" CssClass="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                                                <asp:CheckBox runat="server" ID="chktesting" TabIndex="45" />
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
                                                <asp:TextBox runat="server" ID="txttestingRemarks" TabIndex="46" class="form-control"></asp:TextBox>
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
                                                <asp:CheckBox runat="server" ID="chkcertification" TabIndex="47" />
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
                                                <asp:TextBox runat="server" ID="txtCertificationRemarks" TabIndex="48" class="form-control"></asp:TextBox>
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
                                    <asp:Button runat="server" ID="btnsubmitpanel1" class="btn btn-primary pull-right" TabIndex="50" Text="Save" OnClick="btnsubmitpanel1_Click" OnClientClick="return confirm('Are you sure you want to save this product?');" />
                                    <asp:Button runat="server" ID="btncancelpanel1" class="btn btn-default pull-right" TabIndex="49" Style="margin-right: 10px;" Text="Back" OnClick="btncancelpanel1_Click" />
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
