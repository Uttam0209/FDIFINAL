<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="Admin_AddProduct" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">

    <script src="../assets/js/jquery-1.7.1.min.js"></script>
    <script src="../assets/js/jquery-ui-1.8.17.custom.min.js"></script>
    <link href="../assets/js/jquery-ui-1.8.17.custom.css" rel="stylesheet" />
    <style>
        .thumb {
            width: 100px;
            height: 100px;
            margin: 0.2em -0.7em 0 0;
            border: 1px solid #ccc;
        }

        .remove_img_preview {
            position: relative;
            top: -42px;
            right: 0px;
            background: black;
            color: white;
            border-radius: 50px;
            font-size: 0.9em;
            padding: 0 0.3em 0;
            text-align: center;
            cursor: pointer;
        }

            .remove_img_preview:before {
                content: "×";
            }
    </style>

</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="tabing-section">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#pd">Product Description</a></li>
                    <li><a data-toggle="tab" href="#pimg">Product Image</a></li>
                    <li><a data-toggle="tab" href="#spd">Support Provided by DPSU</a></li>
                    <li><a data-toggle="tab" href="#qpt">Quantity Required</a></li>
                    <li><a data-toggle="tab" href="#cd">Contact Details</a></li>
                </ul>
                <div class="tab-content">
                    <asp:HiddenField runat="server" ID="hfprodid" />
                    <asp:HiddenField runat="server" ID="hfcomprefno" />
                    <div id="pd" class="tab-pane fade in active">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="add-profile">
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>OEM Part Number</label>
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
                                                    <label>ERP Reference No</label>
                                                    <asp:TextBox runat="server" ID="txterprefno" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Nomenclature of main system </label>
                                                    <asp:DropDownList runat="server" ID="ddlnomnclature" class="form-control" />
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Product Level 1</label>
                                                    <asp:DropDownList runat="server" ID="ddlmastercategory" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>

                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Product Level 2</label>
                                                    <asp:DropDownList runat="server" ID="ddlsubcategory" class="form-control"></asp:DropDownList>

                                                </div>
                                            </div>
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

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Technology Level 1 </label>
                                                    <asp:DropDownList runat="server" ID="ddltechnologycat" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddltechnologycat_SelectedIndexChanged"></asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Technology Level 2</label>
                                                    <asp:DropDownList runat="server" ID="ddlsubtech" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>End User</label>
                                                    <asp:DropDownList runat="server" ID="ddlenduser" class="form-control">
                                                        <asp:ListItem Text="static item 1" Value="1" />
                                                        <asp:ListItem Text="static item 2" Value="2" />
                                                        <asp:ListItem Text="static item 3" Value="3" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Platform :</label>
                                                    <asp:DropDownList runat="server" ID="ddlplatform" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlplatform_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Purpose of Procurement :</label>
                                                    <asp:DropDownList runat="server" ID="ddlplatformsubcat" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Product Requirement :</label>
                                                    <asp:DropDownList runat="server" ID="ddlprodreqir" class="form-control"></asp:DropDownList>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="section-pannel">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Search Keywords :</label>
                                                    <asp:TextBox runat="server" ID="txtsearchkeyword" class="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-md-6">

                                                <div class="form-group">
                                                    <label class="live-status-box">
                                                        Product Already Indeginized :
                                                               <asp:RadioButtonList runat="server" ID="rbisindinised" RepeatColumns="2" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbisindinised_CheckedChanged ">
                                                                   <asp:ListItem Value="Y" class="yes">Yes</asp:ListItem>
                                                                   <asp:ListItem Value="N">No</asp:ListItem>
                                                               </asp:RadioButtonList>
                                                        <span>(<strong>NOTE:</strong> If Yes, please give manufacturer name)</span>

                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtmanufacturename" class="form-control Turl_Tdate" Style="display: none"></asp:TextBox>
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
                    <div id="pimg" class="tab-pane fade in">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Product Image</label>
                                        <p class="nameinput">
                                            <asp:FileUpload ID="files" runat="server" type="file" class="inputtype2 form-control" name="image_file_arr[]"
                                                Multiple="Multiple" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                            

                            <div class="fr nameinput">
                                <asp:Panel ID="panphoto" runat="server">
                                    <div runat="server" id="list">
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="fr nameinput">
                                <p>
                                    <asp:Label ID="lbmes" runat="server" Text=""></asp:Label>
                                </p>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnprodimagesave" class="btn btn-primary pull-right" Text="Save" OnClick="btnprodimagesave_Click" />
                                    <asp:Button runat="server" ID="btnprodback" class="btn btn-default pull-right" Text="Back" Style="margin-right: 10px;" OnClick="btnprodback_Click" />
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
                                        <asp:TemplateField HeaderText="Services">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblservices" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                <asp:HiddenField runat="server" ID="hfservicesid" Value='<%#Eval("SCategoryId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CheckBox">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chk" />
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnsavepanel3" class="btn btn-primary pull-right" Text="Save" OnClick="btnsavepanel3_Click" />
                                    <asp:Button runat="server" ID="btnbackpanel3" class="btn btn-default pull-right" Text="Back" Style="margin-right: 10px;" OnClick="btnbackpanel3_Click" />

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="qpt" class="tab-pane fade">
                        <div class="section-pannel">
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
                                <div class="col-md-4">
                                    <div class="form-group ">
                                        <label>
                                            Tender Status</label>
                                        <asp:DropDownList runat="server" ID="ddltendorstatus" class="form-control" AutoPostBack="True">
                                            <asp:ListItem Value="Live">Live</asp:ListItem>
                                            <asp:ListItem Value="Archive">Archive</asp:ListItem>
                                            <asp:ListItem Value="Not Floated">Not Floated</asp:ListItem>
                                            <asp:ListItem Value="To be Floated shortly">To be Floated shortly</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="form-group live-status-box">
                                        <label>
                                            <strong>Note:</strong> If live, please fill last date of tender submission. 
                                            <span class="checkbox-box">
                                                <asp:RadioButtonList runat="server" ID="rbyesno" RepeatDirection="Horizontal" RepeatColumns="2" RepeatLayout="Flow">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </span>
                                        </label>


                                    </div>
                                </div>
                                <div class="Turl_Tdate" style="display: none">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Tender Date</label>
                                            <asp:TextBox runat="server" ID="txttendordate" class="form-control inputbox"></asp:TextBox>
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
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnsavepanel4" class="btn btn-primary pull-right" Text="Save" OnClick="btnsavepanel4_Click" />
                                    <asp:Button runat="server" ID="btnbackpanel4" class="btn btn-default pull-right" Text="Back" Style="margin-right: 10px;" OnClick="btnbackpanel4_Click" />
                                </div>
                            </div>
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
                                    <div class="contactFormRow" style="display: none">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Employee Code</label>
                                                    <asp:TextBox runat="server" ID="txtempcode" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Designation</label>
                                                    <asp:TextBox runat="server" ID="txtDesignation" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Department</label>
                                                    <input type="text" readonly="readonly" value="Company >> Division >>Unit" class="form-control">
                                                    <label>E-Mail ID</label>
                                                    <asp:TextBox runat="server" ID="txtNEmailId" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Mobile Number</label>
                                                    <input type="text" class="form-control" />

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtNTelephone" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Fax</label>
                                                    <asp:TextBox runat="server" ID="txtNFaxNo" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label runat="server" ID="lblcomapnyNodal" Text=""></asp:Label>
                                                <asp:Label runat="server" ID="lblfactortnodal" Text=""></asp:Label>
                                                <asp:Label runat="server" ID="lblunitnodal" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="showMore">
                                            <a href="javascript:void(0)" class="showMoreLink">Show Details</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="section-pannel" runat="server" id="divnodal2">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 class="page-header secondary">Contact Detail 2</h4>
                                            <div class="form-group select-box">
                                                <asp:DropDownList runat="server" ID="ddlNodalOfficerEmail2" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlNodalOfficerEmail2_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="contactFormRow" style="display: none">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Employee Code</label>
                                                    <asp:TextBox runat="server" ID="txtempcode2" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Designation</label>
                                                    <asp:TextBox runat="server" ID="TextBox7" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Department</label>
                                                    <input type="text" readonly="readonly" value="Company >> Division >>Unit" class="form-control">
                                                    <label>E-Mail ID</label>
                                                    <asp:TextBox runat="server" ID="txtNEmailId2" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Mobile Number</label>
                                                    <input type="text" class="form-control" />

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtNTelephone2" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Fax</label>
                                                    <asp:TextBox runat="server" ID="txtNFaxNo2" name="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label runat="server" ID="lblcompanynodal2" Text=""></asp:Label>
                                                <asp:Label runat="server" ID="lblfactorynodal2" Text=""></asp:Label>
                                                <asp:Label runat="server" ID="lblunitnodal2" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="showMore">
                                            <a href="javascript:void(0)" class="showMoreLink">Show Details</a>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnsavepanel5" class="btn btn-primary pull-right" Text="Save" />
                                                <asp:Button runat="server" ID="btnbackpanel5" class="btn btn-default pull-right" Text="Back" Style="margin-right: 10px;" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        var count = 0;
        function handleFileSelect(evt) {
            var $fileUpload = $("input#files[type='file']");
            count = count + parseInt($fileUpload.get(0).files.length);

            if (parseInt($fileUpload.get(0).files.length) > 8 || count > 9) {
                alert("You can only upload a maximum of 4 files");
                count = count - parseInt($fileUpload.get(0).files.length);
                evt.preventDefault();
                evt.stopPropagation();
                return false;
            }
            var files = evt.target.files;
            for (var i = 0, f; f = files[i]; i++) {
                if (!f.type.match('image.*')) {
                    continue;
                }
                var reader = new FileReader();
                reader.onload = (function (theFile) {
                    return function (e) {
                        var span = document.createElement('span');
                        span.innerHTML = ['<img class="thumb" src="', e.target.result, '" title="', escape(theFile.name), '"/><span class="remove_img_preview"></span>'].join('');
                        document.getElementById('list').insertBefore(span, null);
                    };
                })(f);

                reader.readAsDataURL(f);
            }
        }
        $('#files').change(function (evt) {
            handleFileSelect(evt);
        });

        $('#list').on('click', '.remove_img_preview', function () {
            $(this).parent('span').remove();
            //           parseInt($fileUpload.get(0).files.length - 1;
        });
    </script>
</asp:Content>

