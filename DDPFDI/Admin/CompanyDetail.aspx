<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyDetail.aspx.cs" Inherits="Admin_CompanyDetail" MasterPageFile="~/Admin/MasterPage.master" %>

    <asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
        <script>
            function isNumber(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            };
        </script>
    </asp:Content>
    <asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <div class="content oem-content crearfix">
            <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
            <div class="sideBg">
                <div class="col-mod-12">
                    <ul class="breadcrumb">
                        <li class="active">
                            <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label>
                        </li>
                    </ul>
                </div>
                <label for="activityname" class="control-label pull-right">(<span class="mandatory">*</span>) are manadatory field</label>
                <div class="clearfix"></div>

                <div class="row">
                    <div class="col-sm-4">
                        <asp:Label runat="server" ID="lblSelectCompany"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:Label runat="server" ID="lblselectdivison" Visible="False">Select Division/Plant</asp:Label>
                        <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:Label runat="server" ID="lblselectunit" Visible="False">Select Unit</asp:Label>
                        <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="tabing-section" style="margin-top:20px;">
                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#comp">Company</a></li>
                        <li><a data-toggle="tab" href="#sr">Statutory Registration</a></li>
                        <li><a data-toggle="tab" href="#sm">Social Media</a></li>
                        <li><a data-toggle="tab" href="#officer">Officer</a></li>
                        <li><a data-toggle="tab" href="#cc">Customise Category</a></li>
                    </ul>
                </div>
                <div class="tab-content">
                    <div id="comp" class="tab-pane fade in active">
                        <asp:HiddenField ID="hfid" runat="server" />
                        <div id="demo" runat="server">
                            <div class="addfdi">
                                <div class="col-md-12 col-mod-12">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="fdi-add-content">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group" id="DivJointVenture" runat="server">
                                                            <label for="activityname" class="control-label">Joint Venture <span class="mandatory">*</span></label>
                                                            <asp:DropDownList runat="server" ID="seljvventure" required="" name="seljvventure" class="form-control form-cascade-control">
                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                <asp:ListItem Value="N">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">

                                                    <div class="col-md-6">
                                                        <div class="form-group" id="DivActivities" runat="server">
                                                            <label for="companyengaged" class="control-label">Company engaged in Defence Activities</label>
                                                            <asp:DropDownList runat="server" ID="companyengaged" required="" name="companyengaged" class="form-control form-cascade-control">
                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                <asp:ListItem Value="N">No</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label for="tcompanyname" class=" control-label">
                                                            <asp:Label ID="lblCName" runat="server" Text="lblCName"></asp:Label>
                                                            <span class="mandatory">*</span></label>
                                                            <asp:TextBox runat="server" ID="tcompanyname" name="tcompanyname" required="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label for="activityname" class="control-label">Address </label>
                                                            <asp:TextBox runat="server" ID="taddress" name="taddress" TextMode="MultiLine" Height="40px" class="form-control form-cascade-control "
                                                                placeholder=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group" id="DivState" runat="server">
                                                            <label for="selstate" class="control-label">State </label>
                                                            <asp:DropDownList ID="selstate" runat="server" class="form-control form-cascade-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label for="tpincode" class="control-label">Pin Code </label>
                                                            <asp:TextBox runat="server" ID="tpincode" name="tpincode" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">

                                                        <div class="form-group" id="DivCEOName" runat="server">
                                                            <label for="CeoName" class="control-label">CEO Name </label>
                                                            <asp:TextBox runat="server" ID="txtceoname" name="CEO Name" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" id="DivCEOEmail" runat="server">
                                                            <label for="tceoname" class="control-label">CEO Email ID </label>
                                                            <asp:TextBox runat="server" ID="txtCEOEmailId" name="tceoemailid" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                         <div class="form-group">
                                                        <label for="activityname" class="control-label">latitude </label>
                                                        <asp:TextBox runat="server" ID="txtlatitude" name="txtlatitude" class="form-control form-cascade-control " placeholder=""></asp:TextBox>
                                                    </div>
                                                    
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                        <label for="activityname" class="control-label">longitude</label>
                                                        <asp:TextBox runat="server" ID="txtlongitude" name="txtlongitude" class="form-control form-cascade-control " placeholder=""></asp:TextBox>
                                                    </div>
                                                    </div>
                                                </div>




                                                <div class="indiacompanydetails">


                                                    <div class="form-group" id="divmName" runat="server">
                                                        <label for="tcompanyname" class=" control-label">
                                                            <asp:Label ID="lblMName" runat="server" Text=""></asp:Label>
                                                            <span class="mandatory">*</span></label>
                                                        <asp:TextBox runat="server" ID="txtMName" name="txtMName" ReadOnly="true" required="" class="form-control form-cascade-control"
                                                            placeholder=""></asp:TextBox>

                                                    </div>
                                                    <div class="form-group" id="divmcName" runat="server">
                                                        <label for="tcompanyname" class=" control-label">
                                                            <asp:Label ID="lblMCName" runat="server" Text=""></asp:Label>
                                                            <span class="mandatory">*</span></label>
                                                        <asp:TextBox runat="server" ID="txtMCName" name="txtMCName" ReadOnly="true" required="" class="form-control form-cascade-control"
                                                            placeholder=""></asp:TextBox>

                                                    </div>



                                                   

                                                    <div id="Div1" class="form-group" runat="server" visible="false">
                                                        <label for="seldistrict" class="control-label">District </label>
                                                        <asp:TextBox runat="server" ID="seldistrict" name="tdistrict" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                    </div>
                                                    <div id="Div2" class="form-group" runat="server" visible="false">
                                                        <label for="seldistrict" class="control-label">Location </label>
                                                        <asp:TextBox runat="server" ID="TextBox3" name="tdistrict" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                                    </div>



                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div id="sr" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="activityname" class="control-label">Telephone No</label>
                                    <asp:TextBox runat="server" ID="txtTelephone" name="txtTelephone" class="form-control form-cascade-control " placeholder=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="activityname" class="control-label">Fax No</label>
                                    <asp:TextBox runat="server" ID="TextBox1" name="txtTelephone" class="form-control form-cascade-control " placeholder=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Email ID</label>
                                    <input type="text" class="form-control">
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="activityname" class="control-label">Webistie url</label>
                                    <asp:TextBox runat="server" ID="TextBox2" name="txtTelephone" class="form-control form-cascade-control " placeholder=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group" id="DivGST" runat="server">
                                    <label for="tgstno" class="control-label">GST No</label>
                                    <asp:TextBox runat="server" ID="tgstno" name="tgstno" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group" id="DivCIN" runat="server">
                                    <label for="cinno" class=" control-label">CIN </label>
                                    <asp:TextBox runat="server" ID="tcinno" name="tcinno" required="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group" id="DivPAN" runat="server">
                                    <label for="tpanno" class="control-label">PAN </label>
                                    <asp:TextBox runat="server" ID="tpanno" name="tpanno" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group" id="DivHSNO" runat="server">
                                    <label for="tgstno" class="control-label">IE Code </label>
                                    <asp:TextBox runat="server" ID="thssnono" name="thssnono" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="sm" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Facebook</label>
                                    <input type="text" class="form-control">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Twitter</label>
                                     <input type="text" class="form-control">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Linkedin</label>
                                     <input type="text" class="form-control">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="officer" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="tpersonname" class="control-label">Officer's Name </label>
                                    <asp:TextBox runat="server" ID="tpersonname" name="tpersonname" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                </div>


                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="temailid" class=" control-label">Officer's Email ID <span class="mandatory">*</span></label>
                                    <asp:TextBox runat="server" ID="temailid" name="temailid" AutoCompleteType="Email" required="" class="form-control form-cascade-control"
                                        placeholder=""></asp:TextBox>
                                    <p class="note">*Note: will be used as username </p>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="tcontactno" class="control-label">Officer's  Mobile</label>
                                    <asp:TextBox runat="server" ID="tcontactno" name="tcontactno" MaxLength="16" onkeypress="return isNumber(event)" class="form-control form-cascade-control"
                                        placeholder=""></asp:TextBox>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="cc" class="tab-pane fade">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">

                                    <label>
                                        <input type="checkbox">
                                     Mechanical</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">

                                    <label>
                                        <input type="checkbox">
                                            Electrical
                                    </label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">

                                    <label>
                                    <input type="checkbox">Civil</label>
                                </div>
                            </div>
                            <div class="col-md-4">

                                <div class="form-group">

                                    <label>
                                    <input type="checkbox">Electronic</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">

                                    <label>
                                    <input type="checkbox">
                                    Chemical</label>
                                </div>

                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>
                                        <input type="checkbox">
                                   Software Developement</label>
                                </div>
                            </div>


                        </div>
                        <div class="row">
                            <div class="UserInnerpage">
                                <div class="resitered">
                                    <form>
                                        <div class="col-md-12">
                                             <div class="form-group">
                                            <label>
                                                 Are You resitered with Start up India ?
                                            <label style="margin-right: 20px; margin-left: 10px;">
                                            <input type="checkbox" name="startup">Yes</label>
                                            <label><input type="checkbox" name="startup">No</label>
                                            </label>
                                        </div>
                                        </div>
                                       
                                    </form>


                                </div>

                                <div class="clearfix"></div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="UserInnerpage">
                                <div class="resitered">
                                    <form>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                 <input type="checkbox" name="startup">Are you registerd with MSME as a micro or small Enterprise</label>
                                            </div>
                                        </div>

                                    </form>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                             <label>VAM</label>
                                              <input type="text" class="form-control">
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                         <div class="form-group">
                                            <label>Aadhar /Mobile</label>
                                            <input type="text" class="form-control">
                                         </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">

                            <div class="form-group">

                                <asp:LinkButton ID="btndemofirst" runat="server" CssClass="btn btn-danger pull-right" style="margin-left: 10px;" Text="Edit" OnClick="btndemofirst_Click"
                                    Visible="false" />
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-primary pull-right" Text="Delete" Visible="false" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>



        <div class="footer">� 2019 <a href="#">Department of Defence Production</a> </div>
        </div>
        </div>
        </div>

    </asp:Content>