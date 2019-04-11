<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAddFDI.aspx.cs" Inherits="Admin_frmAddFDI" MasterPageFile="~/Admin/MasterPage.master" %>

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
    <div class="content oem-content">
        <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
        <div class="sideBg">
            <div class="col-mod-12">
                <ul class="breadcrumb">
                    <li><a href="home">Dashboard</a></li>
                    <li class="active">Add FDI Inflow</li>
                </ul>
            </div>
            <div class="col-sm-12">
                <asp:UpdatePanel ID="up" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfid" runat="server" />
                        <asp:HiddenField ID="hfCustomerId" runat="server" />
                        <div id="demo1" runat="server">
                            <div class="addfdi">
                                <div class="col-md-12 col-mod-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="businesscode" class="control-label">Select Company </label>
                                                <asp:TextBox ID="txtcomp" runat="server" class="form-control form-cascade-control"></asp:TextBox>
                                                <asp:Label ID="lbl" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="fdi-tab add-inflow-tab">
                                                <ul>
                                                    <li class="active"><a href="#fdistep1" data-toggle="tab">FDI Value</a></li>
                                                    <li><a href="#fdistep2" data-toggle="tab">FDI Route</a></li>
                                                    <li><a href="#fdistep3" data-toggle="tab">Foreign Investor Details</a></li>
                                                    <li><a href="#fdistep4" data-toggle="tab">Source of Information</a></li>
                                                </ul>
                                            </div>
                                            <div class="fdi-add-content">
                                                <ul>
                                                    <li id="fdistep1" class="tab-pane fade in active">
                                                        <!-- NIC Details -->
                                                        <div class="codeofbusiness">
                                                            <div class="form-group">
                                                                <label for="businesscode" class=" control-label">FDI reporting </label>

                                                                <asp:DropDownList ID="nstate" runat="server" name="companyengaged" class="form-control form-cascade-control">
                                                                    <asp:ListItem Value="">FDI Reporting</asp:ListItem>
                                                                    <asp:ListItem Value="Actual">Actual</asp:ListItem>
                                                                    <asp:ListItem Value="Proposed">Proposed</asp:ListItem>
                                                                    <asp:ListItem Value="Revised FDI Value">Revised FDI Value</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label for="businesscode" class=" control-label">Period of reporting </label>

                                                            <asp:DropDownList ID="Select2" runat="server" name="companyengaged" AutoPostBack="true" class="form-control form-cascade-control" OnSelectedIndexChanged="Select2_SelectedIndexChanged">
                                                                <asp:ListItem Value="">Period of reporting</asp:ListItem>
                                                                <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                                                <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                                                <asp:ListItem Value="Annual">Annual</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group" runat="server" id="divhalfyear" visible="false">
                                                            <label for="businesscode" class=" control-label">Period of Half yearly </label>
                                                            <asp:DropDownList ID="ddlhalfyearly" runat="server" name="companyengaged" class="form-control form-cascade-control">
                                                                <asp:ListItem Value="">Select Period of Half-Yearly</asp:ListItem>
                                                                <asp:ListItem Value="H1">April-September</asp:ListItem>
                                                                <asp:ListItem Value="H2">October-March</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group" runat="server" id="periodofquater" visible="false">
                                                            <label for="businesscode" class=" control-label">Period of Quarterly </label>
                                                            <asp:DropDownList ID="ddlquater" runat="server" name="companyengaged" class="form-control form-cascade-control">
                                                                <asp:ListItem Value="">Select Period of Quarterly</asp:ListItem>
                                                                <asp:ListItem Value="Q1">Q1</asp:ListItem>
                                                                <asp:ListItem Value="Q2">Q2</asp:ListItem>
                                                                <asp:ListItem Value="Q3">Q3</asp:ListItem>
                                                                <asp:ListItem Value="Q4">Q4</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group" runat="server" id="year" visible="false">
                                                            <label for="businesscode" class=" control-label">Financial Year</label>
                                                            <asp:DropDownList ID="ddlyear" runat="server" name="companyengaged" class="form-control form-cascade-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="businesscode" class="control-label">Currency </label>

                                                            <asp:DropDownList ID="Select3" runat="server" name="companyengaged" class="form-control form-cascade-control">
                                                                <asp:ListItem Value="">Select Currency</asp:ListItem>
                                                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                                                <asp:ListItem Value="EURO">EURO</asp:ListItem>
                                                                <asp:ListItem Value="Pound">Pound</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="form-group">
                                                            <label for="fdiinflow" class="control-label">FDI Inflow</label>
                                                            <asp:TextBox runat="server" type="text" ID="fdiinflow" name="fdiinflow" onkeypress="return isNumber(event)" class="form-control form-cascade-control"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="exchangerate" class="control-label">Average Exchange Rate of RBI</label>
                                                            <asp:TextBox runat="server" type="text" ID="exchangerate" name="exchangerate" AutoPostBack="true" Text="1" class="form-control form-cascade-control" OnTextChanged="exchangerate_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="afterexchnagerate" class="control-label">Equivalent INR</label>
                                                            <asp:TextBox runat="server" type="text" ID="afterexchnagerate" ReadOnly="true" name="afterexchnagerate" class="form-control form-cascade-control"></asp:TextBox>
                                                        </div>
                                                    </li>


                                                    <li id="fdistep2" class="tab-pane fade">
                                                        <div class="govermentengage">
                                                            <h3 class="hhead">FDI Route</h3>
                                                            <div class="form-group">
                                                                <label for="casetype" class="control-label"></label>
                                                                <asp:DropDownList runat="server" ID="casetype" name="casetype" AutoPostBack="true" required class="form-control form-cascade-control" OnSelectedIndexChanged="casetype_SelectedIndexChanged">
                                                                    <asp:ListItem Value="Government">Government</asp:ListItem>
                                                                    <asp:ListItem Value="Automatic">Automatic</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group" runat="server" id="divapproval">
                                                                <label for="tapprovalno" class=" control-label">Approval No</label>
                                                                <asp:TextBox runat="server" type="text" ID="tapprovalno" name="tapprovalno" class="form-control form-cascade-control"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group" runat="server" id="divapprovaldate">
                                                                <label for="tapprovaldate" class=" control-label">Approval Date</label>
                                                                <asp:TextBox runat="server" ID="tapprovaldate" type="date" name="tapprovaldate" class="form-control form-cascade-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="codeofbusiness">
                                                            <h3 class="hhead">National Industrial Classification(NIC)</h3>
                                                            <div class="form-group">
                                                                <label for="businesscode" class="control-label">NIC code of business</label>
                                                                <asp:TextBox runat="server" ID="businesscode" name="businesscode" class="form-control form-cascade-control" AutoPostBack="true" OnTextChanged="businesscode_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="tbdescription" class=" control-label">Description</label>
                                                                <asp:TextBox runat="server" ID="tbdescription" name="tbdescription" class="form-control form-cascade-control"></asp:TextBox>
                                                            </div>
                                                        </div>


                                                    </li>

                                                    <li id="fdistep3" class="tab-pane fade">
                                                        <div class="foreignvender">
                                                            <h3 class="hhead">Foreign Investor Details</h3>
                                                            <div class="form-group">
                                                                <label for="tcountry" class="control-label">Select Country. </label>
                                                                <asp:TextBox ID="txtcount" runat="server" CssClass="form-control form-cascade-control"></asp:TextBox>

                                                            </div>
                                                            <div class="form-group">
                                                                <label for="tforeignname" class="control-label">Foreign Company Name.</label>

                                                                <asp:TextBox runat="server" type="text" ID="tforeignname" name="tforeignname" class="form-control form-cascade-control"></asp:TextBox>
                                                            </div>

                                                            <div class="form-group">
                                                                <label for="tforeignaddress" class="control-label">Address. </label>

                                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="tforeignaddress" name="tforeignaddress" class="form-control form-cascade-control"></asp:TextBox>

                                                            </div>

                                                            <div class="form-group">
                                                                <label for="tzipcode" class="control-label">Zip Code. </label>

                                                                <asp:TextBox runat="server" type="text" ID="tzipcode" name="tzipcode" class="form-control form-cascade-control" pattern="[0-9]{6}"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="activityname" class="control-label">Foreign investor engaged in Defence Activities .</label>
                                                                <asp:DropDownList ID="Select1" runat="server" name="companyengaged" class="form-control form-cascade-control">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </li>

                                                    <li id="fdistep4" class="tab-pane fade">
                                                        <div class="sourceofinformation">
                                                            <h3 class="hhead">Source of Information</h3>
                                                            <div class="form-group">
                                                                <label for="selsource" class="control-label">Select Source of Information</label>
                                                                <asp:DropDownList ID="selsource" runat="server" name="selsource" class="form-control form-cascade-control">
                                                                    <asp:ListItem Value="">Select Source</asp:ListItem>
                                                                    <asp:ListItem Value="RBI-DIPP">RBI-DIPP</asp:ListItem>
                                                                    <asp:ListItem Value="Meeting">Meeting</asp:ListItem>
                                                                    <asp:ListItem Value="Reported by company">Reported by company</asp:ListItem>
                                                                    <asp:ListItem Value="Informally gathered">Informally gathered</asp:ListItem>
                                                                    <asp:ListItem Value="Any other source">Any other source</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                            <!-- source of -->




                                                            <div class="form-group">
                                                                <label for="tdateofreceiving" class="control-label">Date of receiving information</label>

                                                                <asp:TextBox runat="server" type="date" ID="tdateofreceiving" name="tdateofreceiving" class="form-control form-cascade-control"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="selsource" class="control-label">Authenticity of Information </label>
                                                                <asp:DropDownList ID="selcolour" runat="server" name="selsource" class="form-control form-cascade-control">
                                                                    <asp:ListItem Value="">Authenticity of Information</asp:ListItem>
                                                                    <asp:ListItem Value="Green">Green</asp:ListItem>
                                                                    <asp:ListItem Value="Yellow">Yellow</asp:ListItem>
                                                                    <asp:ListItem Value="Red">Red</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group">
                                                                <label for="tremarks" class="control-label">Remarks </label>
                                                                <asp:TextBox runat="server" ID="tremarks" name="tremarks" TextMode="MultiLine" Height="77px" class="form-control form-cascade-control"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="selsource" class="control-label">Document attached if any (Email/Letter etc in pdf format only.) </label>
                                                                <asp:FileUpload runat="server" type="file" ID="attachfile" name="attachfile" class="form-control form-cascade-control" />
                                                                <asp:Label ID="lblfuupdate" runat="server"></asp:Label>
                                                            </div>

                                                            <asp:LinkButton ID="btnsub" runat="server" Text="Save" class="buttonBg pull-right col-lg-offset-2" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';" OnClick="btnsub_Click"></asp:LinkButton>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnsub" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="clearfix"></div>
        </div>
        <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
    </div>

</asp:Content>
