<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/VendorMaster2.master" AutoEventWireup="true" CodeFile="Profile_Management.aspx.cs" Inherits="Vendor_Profile_Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
    <div class="right_col" role="main">
        <div class="">
            <div class="page-title">
                <div class="title_left">
                    <h3>Profile Management</h3>
                </div>
                <div class="title_right">
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="row">
                <div class="x_panel">
                    <div class="x_content">
                        <div class="clearfix"></div>
                        <div class="col-md-6 col-sm-4  profile_details">
                            <div class="well profile_view">
                                <div class="col-sm-12">
                                    <% if (usertype == "User")
                                        { %>
                                    <h4 class="brief"><i> User Information </i></h4>
                                    <%} %>
                                    <%else
    { %>
                                      <h4 class="brief"><i> Nodal Officer Information </i></h4>
                                    <%} %>
                                    <div class="left col-md-7 col-sm-7">
                                        <h2>Mr.<asp:Label runat="server" ID="NolOffName"></asp:Label></h2>
                                        <ul class="list-unstyled">
                                            <li>
                                                <p>
                                                    <i class="fa fa-envelope"></i>&nbsp;Email : <strong>
                                                        <asp:Label ID="lblusername" runat="server"></asp:Label></strong>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <i class="fa fa-phone"></i>&nbsp;Phone : <strong>
                                                        <asp:Label runat="server" ID="MobNo"></asp:Label></strong>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <i class="fa fa-building"></i>&nbsp;Address : <strong>
                                                        <asp:Label runat="server" ID="NolOffADDRE" />
                                                    </strong>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <i class="fa fa-map-signs"></i>&nbsp;City : <strong>
                                                        <asp:Label runat="server" ID="NolOffCity" Visible="false"  />
                                                        <%=City%>
                                                       </strong>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <i class="fa fa-map-marker"></i>&nbsp;State : <strong>
                                                        <asp:Label runat="server" ID="AB" />
                                                        <%=State%>
                                                    </strong>
                                                </p>
                                            </li>
                                            <li>
                                                <p>
                                                    <i class="fa fa-map-pin"></i>&nbsp;Postal Code : <strong>
                                                        <asp:Label runat="server" ID="AC" />
                                                        <%=ZipCode%></strong>
                                                </p>
                                            </li>

                                        </ul>
                                    </div>
                                    <div class="right col-md-5 col-sm-5 text-center">
                                        <img src="vendor/images/img.jpg" alt="" class="img-circle img-fluid">
                                    </div>
                                </div>
                                <div class=" profile-bottom text-center">
                                    <div class=" col-sm-6 emphasis">
                                        <p class="ratings">
                                        </p>
                                    </div>
                                   <%-- <div class=" col-sm-6 emphasis">
                                        <button type="button" class="btn btn-success btn-sm" data-toggle="tooltip" data-placement="left" title="Identity Card">
                                            <a href="Uploaded/<%=identi%>" style="color: white;"><i class="fa fa-user"></i></a>
                                        </button>
                                        <button type="button" class="btn btn-success btn-sm" data-toggle="tooltip" data-placement="left" title="Authrization Latter "><a href="Uploaded/<%=authri%>" style="color: white;"><i class="fa fa-comments-o"></i></a></button>
                                    </div>--%>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6 ">
                            <div class="x_panel">
                                <div class="x_title">
                                    <% if (usertype == "User")
                                        { %>
                                    <h2><small>Edit User Details</small></h2>
                                    <%} %>
                                    <%else
    { %>
                                    <h2><small>Edit Nodal Officer Details</small></h2>
                                    <%} %>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <br />
                                    <form class="form-horizontal form-label-left">
                                        <div class="form-group row ">
                                            <% if (usertype == "User")
                                                { %>
                                            <label class="control-label col-md-3 col-sm-3 ">User Name</label>
                                            <%} %>
                                            <%else
    { %>
                                            <label class="control-label col-md-3 col-sm-3 ">Nodal officer Name</label>
                                            <%} %>
                                            <div class="col-md-9 col-sm-9 ">
                                             <h5>Mr/Mrs &nbsp;<asp:Label ID="NolOffName1" runat="server" Visible="true"></asp:Label></h5>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                             <% if (usertype == "User")
                                                 { %>
                                             <label class="control-label col-md-3 col-sm-3 ">User email: </label>
                                            <%} %>
                                            <%else
    { %>
                                            <label class="control-label col-md-3 col-sm-3 ">Nodal officer email: </label>
                                            <%} %>
                                            <div class="col-md-9 col-sm-9 ">
                                                <asp:TextBox ID="VEmailID" runat="server" class="form-control" placeholder="Update Email ID *"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                    ErrorMessage="* Please Enter Correct Email" ControlToValidate="VEmailID"
                                                    SetFocusOnError="True"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                             <% if (usertype == "User")
                                                 { %>
                                            <label class="control-label col-md-3 col-sm-3 ">User Contact No.</label>
                                            <%} %>
                                            <%else
    { %>
                                            <label class="control-label col-md-3 col-sm-3 ">Nodal office Contact No.</label>
                                            <%} %>
                                            <div class="col-md-9 col-sm-9 ">
                                                <asp:TextBox ID="VmobileNO" runat="server" class="form-control" placeholder="Update Mobile NO *" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <% if (usertype == "User")
                                                { %>
                                            <label class="control-label col-md-3 col-sm-3 ">User Registered Office Address Line1</label>
                                            <%} %>
                                            <%else
    { %>
                                            <label class="control-label col-md-3 col-sm-3 ">Nodel Officer Registered Office Address Line1</label>
                                            <%} %>
                                            <div class="col-md-9 col-sm-9 ">
                                                <asp:TextBox ID="NolOffADDRE1" runat="server" class="form-control" placeholder="Address line 1"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                             <% if (usertype == "User")
                                                 { %>
                                            <label class="control-label col-md-3 col-sm-3 ">User Registered Office Address Line2</label>
                                            <%} %>
                                            <%else
    { %>
                                            <label class="control-label col-md-3 col-sm-3 ">Nodel Officer Registered Office Address Line2</label>
                                            <%} %>
                                            <div class="col-md-9 col-sm-9 ">
                                                <asp:TextBox ID="NolOffADDRE2" runat="server" class="form-control" placeholder="Address line 2"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-md-3 col-sm-3 ">State</label>
                                            <div class="col-md-9 col-sm-9 ">
                                                <%--<asp:TextBox ID="Pincode1" runat="server" class="form-control" placeholder="Pincode"></asp:TextBox>--%>
                                                <asp:DropDownList runat="server" ID="ddlstate" CssClass="form-cascade-control form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-md-3 col-sm-3 ">City</label>
                                            <div class="col-md-9 col-sm-9 ">
                                                <%--<asp:TextBox ID="City1" runat="server" class="form-control" placeholder="City"></asp:TextBox>--%>
                                                <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-cascade-control form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-md-3 col-sm-3 ">Pincode</label>
                                            <div class="col-md-9 col-sm-9 ">
                                                <asp:DropDownList runat="server" ID="ddlPincode" CssClass="form-cascade-control form-control"></asp:DropDownList>
                                                <%--<asp:TextBox ID="State1" runat="server" class="form-control" placeholder="State"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div visible="false" >
                                         <a href="#" onclick="showDiv5()" visible="false"></a>
                                            <div class="form-group row" id="myID5" style="display: none;">
                                            <label class="control-label col-md-3 col-sm-3 ">Please Suggest Your Location</label>
                                            <div class="col-md-9 col-sm-9 ">
                                                <asp:TextBox ID="Npincode1" runat="server" class="form-control" placeholder="Pin code"></asp:TextBox><br />
                                                <asp:TextBox ID="NCity" runat="server" class="form-control" placeholder="City"></asp:TextBox><br />
                                                <asp:TextBox ID="NState" runat="server" class="form-control" placeholder="State"></asp:TextBox>
                                            </div>
                                        </div></div>

                                        <div class="ln_solid"></div>
                                        <div class="form-group">
                                            <div class="col-md-9 col-sm-9  offset-md-3">
                                                <asp:Button ID="closeBTN" class="btn btn-success" OnClick="closebtn" Text="Resat" runat="server" />
                                                <asp:Button class="btn btn-success" ID="Button1" Text="Save" runat="server" OnClick="updateDetails" />
                                                <%--<asp:LinkButton class="btn btn-primary go-class" Text="Verify OTP" id="LinkButton1" OnClick="LinkButton1_Click"  runat="server"/>--%>
                                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>

                          <div class="col-md-6 col-sm-12 ">
                            <div class="x_panel" style="margin-top: -355px;">
                                <div class="x_title">
                                    <h2>Documents <small></small></h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <form class="form-horizontal form-label-left">
                                        <div class="form-group row">
                                            <div class="col-sm-9">
                                                <div class="input-group"> 
                                                     <% if (usertype == "User")
                                                         { %>
                                                     <span class="input-group-btn" data-toggle="tooltip" data-placement="left" title="Company ID">
                                                      <a href="Upload/<%=authri%>" class="btn btn-primary" style="color: white;"> Vied Company ID</a>
                                                    </span>
                                            
                                            <%} %>
<%else
    { %>
                                                    <span class="input-group-btn" data-toggle="tooltip" data-placement="left" title="Vied Authorization Letter">
                                                      <a href="Upload/<%=authri%>" class="btn btn-primary" style="color: white;"> Vied Authorization Letter</a>
                                                    </span>
                                                    <%} %>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="divider-dashed"></div>
                                        <div class="form-group row">
                                            <div class="col-sm-9">
                                                <div class="input-group">
                                                    <% if (usertype == "User")
                                                        { %>
                                                    <span class="input-group-btn" data-toggle="tooltip" data-placement="left" title="Govt. ID Card">
                                                       <a href="Upload/<%=identi%>" class="btn btn-primary" style="color: white;"> View Govt. ID</a>
                                                      </span>
                                                    <%} %>
                                                    <%else
    { %>
                                                    <span class="input-group-btn" data-toggle="tooltip" data-placement="left" title="Identity Card">
                                                       <a href="Upload/<%=identi%>" class="btn btn-primary" style="color: white;"> View Identity Card</a>
                                                      </span>
                                                    <%} %>
                                                </div>
                                            </div>
                                        </div>
                                        
                                    </form>
                                </div>
                            </div>
                        </div>
                   <%-- <div class="col-md-6 col-sm-12 ">
                            <div class="x_panel" style="margin-top: -355px;">
                                <div class="x_title">
                                    <h2>Upload <small>Authorization Letter/Identity Card</small></h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <form class="form-horizontal form-label-left">
                                        <div class="form-group row">
                                            <div class="col-sm-9">
                                                <div class="input-group">    <asp:FileUpload ID="FileUpload1" runat="server" class="form-control"/>
                                                    <span class="input-group-btn">
                                                       <asp:LinkButton class="btn btn-primary go-class" ID="Btn_bt" OnClick="uploadAuthrizationL" Text="Authorization Letter" runat="server"></asp:LinkButton>
                                                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                    </span>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="divider-dashed"></div>
                                        <div class="form-group row">
                                            <div class="col-sm-9">
                                                <div class="input-group">
                                                    <asp:FileUpload ID="FileUpload2" runat="server" class="form-control" />
                                                    <span class="input-group-btn">
                                                        <asp:Button class="btn btn-primary" ID="Button6" OnClick="uploadidentity" Text="Identity Card" runat="server" />
                                                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        
                                    </form>
                                </div>
                            </div>
                        </div>--%>
                            <!--/.col-->

                       <%-- <div id="MYotpPM" runat="server">
                            <asp:TextBox ID="txtotpVery" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:Button ID="btnon" CssClass="form-control" runat="server" OnClick="btn_VOTP_Click" />
                            <asp:Label ID="mylable" runat="server" Text=""></asp:Label>
                        </div>--%>

                        <div class="col-md-6 col-sm-12" id="MYotpPM" style="margin-top: -35%; color: white; background-color: #032d5a; margin-left: 24%;" runat="server" visible="false" >
                      <asp:Button ID="btnclose" runat="server" OnClick="closebtn" text="X" style="color: red;"/>
                            <div class="x_panel" style="background-color: #094c94;">
                                <div class="x_title">
                                    <h2 style="margin-left: 10%;">Plesae enter OTP we have sent you in your email ID</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <form class="form-horizontal form-label-left">
                                        <div class="form-group row">
                                            <div class="col-sm-9" style="margin-left: 12%;">
                                                <div class="input-group">
                                                     <asp:TextBox runat="server" ID="txtotpVery" placeholder="OTP (6 Digit)" Class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                   <span class="input-group-btn">
                                                      
                                                        <asp:LinkButton class="btn btn-primary go-class" Text="Verify OTP" id="btn_VOTP" OnClick="btn_VOTP_Click" OnClientClick= "return validate();" runat="server"/>
                                                       <asp:Label ID="mylable" runat="server" Text=""></asp:Label>
                                                    </span>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="divider-dashed"></div>
                                    </form>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            
        </div>

        <!-- .content -->
        <asp:HiddenField runat="server" ID="PMotp" />
        <%--<div class="modal fade" id="modelotpPM" runat="server">--%>
             <div class="modal fade" id="modelotpPM" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-dialog-centered" role="document" style="max-width: 600px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <ul class="nav nav-tabs card-header-tabs" role="tablist">
                            <li class="nav-item"><a class="nav-link active" href="#" data-toggle="tab" role="tab"
                                aria-selected="true">OTP</a></li>
                        </ul>
                        <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body tab-content py-4">
                        <p class="text-justify">
                            <asp:TextBox runat="server" ID="txtotp" placeholder="OTP (6 Digit)" TabIndex="1" CssClass="form-control"></asp:TextBox>
                            <div class="clearfix mt-1">
                            </div>
                            <span>Please enter otp received on your given email id.</span>
                            <div class="clearfix mt-1">
                            </div>
                            <asp:LinkButton ID="lbsubmit" runat="server" CssClass="btn btn-primary btn-shadow pull-right" TabIndex="2" Text="Submit"
                                ToolTip="Thank you for showing intrest in these product mail will send to admin and also you will recieved a copy." OnClick="lbsubmit_Click"></asp:LinkButton>
                            <asp:LinkButton runat="server" Text="Resend OTP" ID="lbresendotp" TabIndex="3" CssClass="pull-right mr-1 p-2" OnClick="lbresendotp_Click" ></asp:LinkButton>
                            <asp:Label ID="lblmssg" runat="server"></asp:Label>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
            </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger ControlID="lbsubmit" />
               <asp:PostBackTrigger ControlID="btn_VOTP" />
        </Triggers>
        </asp:UpdatePanel>

    
      
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
        function showPopup() {
            $('#ProductCompany').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openmypopup() {
            $('#modelotpPM').modal('show');
        }
    </script>
    <script type='text/javascript'>
        $(function () {
            $('#ddlstate').ufd({ log: true });
        });
    </script>

    <script type='text/javascript' >
        $(function () {
            $('#ddlPincode').ufd({ log: true });
        });
    </script>
     <script type="text/javascript">
         $(function () {
             $("[id$=TxtpincodeT]").autocomplete({
                 source: function (request, response) {
                     AjaxCall("Profile_Management.aspx/GetPin", request.term, 0, response)
                 },
                 select: function (e, i) {
                     $("[id$=hfPincode]").val(i.item.val);
                     // $("[id$=txtState]").removeAttr("disabled");
                     // $("[id$=txtState]").focus();
                 },
                 minLength: 1
             });



             $("[id$=txtCityT]").autocomplete({
                 source: function (request, response) {
                     AjaxCall("Profile_Management.aspx/GetCities", request.term, $("[id$=hfPincode]").val(), response)
                 },
                 select: function (e, i) {
                     $("[id$=hfstateId]").val(i.item.val);
                 },
                 minLength: 1
             });
         });

         function AjaxCall(url, prefix, parentId, response) {
             $.ajax({
                 url: url,
                 data: "{ 'prefix': '" + prefix + "', parentId: " + parentId + "}",
                 dataType: "json",
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 success: function (r) {
                     response($.map(r.d, function (item) {
                         return {
                             label: item.split('-')[0],
                             val: item.split('-')[1]
                         }
                     }))
                 },
                 error: function (r) {
                     alert(r.responseText);
                 },
                 failure: function (r) {
                     alert(r.responseText);
                 }
             });
         }
     </script>


    <script type="text/javascript">
        $(function () {
            $("[id$=TxtpincodeT]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("Profile_Management.aspx/GetPinCodeService") %>',

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
                select: function (e, i) {
                    $("[id$=hfPincode]").val(i.item.val);
                    // $("[id$=TxtCity]").focus();
                },
                minLength: 1
            });


        });
    </script>
     <script type="text/javascript">
         $(function () {
             $("[id$=TxtCityT]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("Profile_Management.aspx/GetCityService") %>',
                            data: "{ 'prefix': '" + request.term + " , parentId: " + $("[id$=hfPincode]").val() + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('-')[0]
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
        function showDiv5() {
            // document.getElementById('myID2').style.display = "block";

            var y = document.getElementById("myID5");
            if (y.style.display === "block") {
                y.style.display = "none";
            } else {
                y.style.display = "block";
            }
        }

    </script>

    <script type="text/javascript">
function validate_disable() {
         }
        function validate() {
            var flag = true;
            if ((document.getElementById("<%=txtotpVery.ClientID%>").value.trim() == '') || (document.getElementById("<%=txtotpVery.ClientID%>").value.trim() == undefined)) {
                alert('Please enter OTP');
                flag = false;
            }

            return flag;
        }

    </script>
</asp:Content>
