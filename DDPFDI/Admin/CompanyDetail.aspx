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
    <div class="content oem-content">
        <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
        <div class="sideBg">
            <div class="col-mod-12">
                <ul class="breadcrumb">
                    <li class="active">
                        <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></li>
                </ul>
            </div>

            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-4">
                        <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="fdi-tab add-inflow-tab">
                    <ul>
                        <li class="active"><a href="#fdistep1" data-toggle="tab">Company Detail</a></li>
                        <li><a href="#fdistep2" data-toggle="tab">Nodal Detail</a></li>
                        <%--<li><a href="#fdistep3" data-toggle="tab">Specilization</a></li>
                        <li><a href="#fdistep5" data-toggle="tab">MSME</a></li>
                        <li><a href="#fdistep6" data-toggle="tab">Startup</a></li>--%>
                    </ul>
                </div>

                <asp:HiddenField ID="hfid" runat="server" />
                <div id="demo" runat="server">
                    <div class="addfdi">
                        <div class="col-md-12 col-mod-12">
                            <div class="row">
                                <label for="activityname" class="control-label pull-right">Mark with (<span class="mandatory">*</span>) are manadatory field</label>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <div class="fdi-add-content">
                                        <div class="indiacompanydetails fade in active" id="fdistep1">
                                            <div class="form-group" id="DivJointVenture" runat="server">
                                                <label for="activityname" class="control-label">Joint Venture <span class="mandatory">*</span></label>
                                                <asp:DropDownList runat="server" ID="seljvventure" required="" name="seljvventure" class="form-control form-cascade-control">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group" id="DivActivities" runat="server">
                                                <label for="companyengaged" class="control-label">Company engaged in Defence Activities</label>
                                                <asp:DropDownList runat="server" ID="companyengaged" required="" name="companyengaged" class="form-control form-cascade-control">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="tcompanyname" class=" control-label">Company Name <span class="mandatory">*</span></label>
                                                <asp:TextBox runat="server" ID="tcompanyname" name="tcompanyname" required="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="activityname" class="control-label">Address </label>
                                                <asp:TextBox runat="server" ID="taddress" name="taddress" TextMode="MultiLine" Height="98px" class="form-control form-cascade-control " placeholder=""></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <label for="tpincode" class="control-label">Pin Code </label>
                                                <asp:TextBox runat="server" ID="tpincode" name="tpincode" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>
                                            <div id="Div1" class="form-group" runat="server" visible="false">
                                                <label for="seldistrict" class="control-label">District </label>
                                                <asp:TextBox runat="server" ID="seldistrict" name="tdistrict" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>
                                            <div class="form-group" id="DivState" runat="server">
                                                <label for="selstate" class="control-label">State </label>
                                                <asp:DropDownList ID="selstate" runat="server" class="form-control form-cascade-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group" id="DivGST" runat="server">
                                                <label for="tgstno" class="control-label">GST No</label>
                                                <asp:TextBox runat="server" ID="tgstno" name="tgstno" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>
                                            <div class="form-group" id="DivCIN" runat="server">
                                                <label for="cinno" class=" control-label">CIN No <span class="mandatory">*</span> </label>
                                                <asp:TextBox runat="server" ID="tcinno" name="tcinno" required="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>
                                            <div class="form-group" id="DivPAN" runat="server">
                                                <label for="tpanno" class="control-label">PAN No </label>
                                                <asp:TextBox runat="server" ID="tpanno" name="tpanno" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>
                                            <div class="form-group" id="DivHSNO" runat="server">
                                                <label for="tgstno" class="control-label">HSNO </label>
                                                <asp:TextBox runat="server" ID="thssnono" name="thssnono" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="indiacompanydetails fade" id="fdistep2">


                                            <div class="form-group" id="DivCEOName" runat="server">
                                                <label for="CeoName" class="control-label">CEO Name </label>
                                                <asp:TextBox runat="server" ID="txtceoname" name="CEO Name" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>
                                            <div class="form-group" id="DivCEOEmail" runat="server">
                                                <label for="tceoname" class="control-label">CEO Email ID </label>
                                                <asp:TextBox runat="server" ID="txtCEOEmailId" name="tceoemailid" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="tpersonname" class="control-label">Nodal Person  Name </label>
                                                <asp:TextBox runat="server" ID="tpersonname" name="tpersonname" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="temailid" class=" control-label">Nodal Person Email ID <span class="mandatory">*</span></label>
                                                <asp:TextBox runat="server" ID="temailid" name="temailid" AutoCompleteType="Email" required="" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="tcontactno" class="control-label">Nodal Person Contact No</label>
                                                <asp:TextBox runat="server" ID="tcontactno" name="tcontactno" MaxLength="16" onkeypress="return isNumber(event)" class="form-control form-cascade-control" placeholder=""></asp:TextBox>
                                            </div>


                                            <div class="clearfix"></div>
                                            <br />
                                        </div>

                                    </div>
                                </div>
                                <div class="col-sm-12">

                                    <div class="form-group">

                                        <asp:LinkButton ID="btndemofirst" runat="server" CssClass="btn btn-danger pull-right" Text="Edit" OnClick="btndemofirst_Click" Visible="false" />
                                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-primary pull-right" Text="Delete" Visible="false" />
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="clearfix"></div>
        </div>
        <div class="footer">� 2019 <a href="#">Department of Defence Production</a> </div>
    </div>
</asp:Content>
