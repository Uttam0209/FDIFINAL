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
                                <label>Select Company</label>
                                <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-cascade-control form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblselectdivison">
                            <div class="form-group">
                                <label>Select Division/Palnt</label>
                                <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-cascade-control form-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" id="lblselectunit">
                            <div class="form-group">
                                <label>Select Unit</label>
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
                                                    <label>OEM Part Number <span class="red">*</span></label>
                                                    <asp:TextBox runat="server" ID="txtoempartnumber" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>DPSU Part Number</label>
                                                    <asp:TextBox runat="server" ID="txtdpsupartnumber" class="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>End User Part Number</label>
                                                    <asp:TextBox runat="server" ID="txtenduserpartnumber" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>HSN Code</label>
                                                    <asp:TextBox runat="server" ID="txthsncode" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>NATO Code</label>
                                                    <asp:TextBox runat="server" ID="txtnatocode" class="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>DPSU Reference No</label>
                                                    <asp:TextBox runat="server" ID="txterprefno" class="form-control"></asp:TextBox>
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
                                                            <label>Product Level 1</label>
                                                            <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>

                                                        </div>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Product Level 2</label>
                                                            <asp:DropDownList runat="server" ID="ddlsubcategory" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged"></asp:DropDownList>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Product Level 3</label>
                                                            <asp:DropDownList runat="server" ID="ddllevel3product" class="form-control"></asp:DropDownList>

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
                                                    <asp:TextBox runat="server" ID="txtproductdescription" TextMode="MultiLine" class="form-control"></asp:TextBox>
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
                                                            <label>Technology Level 1 </label>
                                                            <asp:DropDownList runat="server" ID="ddltechnologycat" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddltechnologycat_SelectedIndexChanged"></asp:DropDownList>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Technology Level 2</label>
                                                            <asp:DropDownList runat="server" ID="ddlsubtech" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlsubtech_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Technology Level 3</label>
                                                            <asp:DropDownList runat="server" ID="ddltechlevel3" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblNomenclature" Text="Nomenclature of main system"></asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddlnomnclature" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblenduser" Text="End User"></asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddlenduser" class="form-control">
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
                                                            <label>Platform</label>
                                                            <asp:DropDownList runat="server" ID="ddlplatform" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlplatform_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Purpose of Procurement</label>
                                                            <asp:DropDownList runat="server" ID="ddlplatformsubcat" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblprodrequir" CssClass="form-label" Text="Procurement Time Frame"></asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddlprodreqir" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Search Keywords</label>
                                                    <asp:TextBox runat="server" ID="txtsearchkeyword" class="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                            <asp:UpdatePanel runat="server" ID="upindwginized">
                                                <ContentTemplate>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="live-status-box productalreadylabel ">
                                                                Product Already Indeginized :
                                                               <asp:RadioButtonList runat="server" ID="rbisindinised" RepeatColumns="2" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbisindinised_CheckedChanged ">
                                                                   <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                                   <asp:ListItem Value="Y" class="yes">Yes</asp:ListItem>
                                                               </asp:RadioButtonList>
                                                            </label>
                                                            <asp:TextBox runat="server" ID="txtmanufacturename" placeholder="Enter Manufacturer name" Visible="False" class="form-control Turl_Tdate"></asp:TextBox>
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
                                            <asp:FileUpload ID="files" runat="server" CssClass="uploadimage form-control" type="file" name="image_file_arr[]" Multiple="Multiple" />
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
                                                <asp:CheckBox runat="server" ID="chk" />
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
                                                <asp:TextBox runat="server" ID="txtRemarks" class="form-control"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="txtestimatequantity" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Estimated Price / LLP</label>
                                                <asp:TextBox runat="server" ID="txtestimateprice" class="form-control"></asp:TextBox>
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
                                                <asp:DropDownList runat="server" ID="ddltendorstatus" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddltendorstatus_SelectedIndexChanged">
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
                                                <asp:RadioButtonList runat="server" ID="rbtendordateyesno" RepeatDirection="Horizontal" AutoPostBack="True" RepeatColumns="2" RepeatLayout="Flow" OnSelectedIndexChanged="rbtendordateyesno_CheckedChanged">
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
                                                    <asp:TextBox runat="server" ID="txttendordate" type="date" class="form-control inputbox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Tender URL</label>
                                                    <asp:TextBox runat="server" ID="txttendorurl" class="form-control"></asp:TextBox>
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
                                                <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail_SelectedIndexChanged"></asp:DropDownList>
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
                                    <div class="section-pannel" runat="server" id="divnodal2">
                                        <div class="row">
                                            <h4 class="page-header secondary">Contact Detail 2</h4>
                                            <div class="form-group select-box">
                                                <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail2" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail2_SelectedIndexChanged"></asp:DropDownList>
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
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="test" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>User Testing</label>
                                                    <asp:DropDownList runat="server" ID="ddlusertesting" class="form-control">
                                                        <asp:ListItem runat="server" Value="Select">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Field Testing</label>
                                                    <asp:DropDownList runat="server" ID="ddlfieldtesting" class="form-control">
                                                        <asp:ListItem runat="server" Value="Select">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Trail Testing</label>
                                                    <asp:DropDownList runat="server" ID="ddltrail" class="form-control">
                                                        <asp:ListItem runat="server" Value="Select">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Machnical Testing</label>
                                                    <asp:DropDownList runat="server" ID="ddlmechnicaltesting" class="form-control">
                                                        <asp:ListItem runat="server" Value="Select">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>MET</label>
                                                    <asp:DropDownList runat="server" ID="ddlmettesting" class="form-control">
                                                        <asp:ListItem runat="server" Value="Select">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>EMIC</label>
                                                    <asp:DropDownList runat="server" ID="ddlemictesting" class="form-control">
                                                        <asp:ListItem runat="server" Value="Select">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="cer" class="tab-pane fade">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Certification Image</label>
                                        <div class="fr">
                                            <asp:FileUpload ID="fucertifiimage" runat="server" CssClass="uploadimage form-control" type="file" name="image_file_arr[]" Multiple="Multiple" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!-------uplode photo----------->
                            <div class="gallery"></div>
                            <div class="clearfix" style="margin-top: 8px;"></div>
                            <span>Only .pdf file of size 1 Mb can be uploaded</span>
                            <br />
                            <div runat="server" id="div1" visible="False">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:DataList runat="server" ID="DataList1" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemCommand="dlimage_ItemCommand">
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
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnsubmitpanel1" class="btn btn-primary pull-right" Text="Save" OnClick="btnsubmitpanel1_Click" />
                                <asp:Button runat="server" ID="btncancelpanel1" class="btn btn-default pull-right" Style="margin-right: 10px;" Text="Back" OnClick="btncancelpanel1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        
    </script>
</asp:Content>
