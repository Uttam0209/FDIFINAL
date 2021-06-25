<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/VendorMaster2.master" AutoEventWireup="true" CodeFile="ProfileMigration.aspx.cs" Inherits="Vendor_ProfileMigration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type='text/javascript'>
        function emailValidator(element, alertMsg) {
            debugger;
            var emailvalid = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (element.value.match(emailvalid)) {
                element.style.color = "Green";
                return true;
            } else {
                //alert(alertMsg);
                element.focus();
                element.style.color = "Red";
                return false;
            }
        }
    </script>
<div class="right_col" role="main">
<div class="">
<div class="page-title">
<div class="title_left">
<h3>Profile Migration</h3>
</div>
<div class="title_right">
</div>
</div>
<div class="clearfix"></div>
<div class="row">
<div class="col-md-12 col-sm-12">
<div class="x_panel">
<div class="x_content">
<form>
<span class="section">Profile Migration</span>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Nodal officer Name<span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
    <asp:TextBox ID="NolOffName" runat="server" class="form-control" placeholder="Nodal officer Name *"></asp:TextBox>
</div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Nodal officer email:<span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
 <asp:TextBox ID="VEmailID" runat="server" class="form-control" placeholder="Update Email ID *"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
             ErrorMessage="* Please Enter Correct Email" ControlToValidate="VEmailID"
             SetFocusOnError="True"
             ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
</asp:RegularExpressionValidator>
</div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Nodal office Contact No. <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:TextBox ID="VmobileNO" runat="server" class="form-control" placeholder="Update Mobile NO *" onkeypress="return isNumberKey(event)"></asp:TextBox>

</div>
   
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Nodel Officer Registered Office Address <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:TextBox ID="NolOffADDRE1" runat="server" class="form-control" placeholder="Nodel Officer Registered Office Address *"></asp:TextBox></div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Nodel Officer Registered Office Address 2 <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:TextBox ID="NolOffADDRE2" runat="server" class="form-control" placeholder="Nodel Officer Registered Office Address2 *"></asp:TextBox></div>
</div>

    <div class="field item form-group"style="margin-left: 25%;">
     <div class="col-md-2 col-sm-2">
                                              <p>State</p>
                                            <asp:DropDownList runat="server" ID="ddlstate" Class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"> </asp:DropDownList>

         </div>
                                        <div class="col-md-2 col-sm-2">
                                            <p>City</p>
                                           
                                            <asp:DropDownList runat="server" ID="ddlCity" Class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"> </asp:DropDownList>
                                           </div>
                                        <div class="col-md-2 col-sm-2">
                                             <p>Pin Code</p>
                                           <asp:DropDownList runat="server" ID="ddlPincode" Class="form-control"> </asp:DropDownList>

                        </div>
        </div>

    <%--<a href="#" onclick="showDiv5()" style="margin-left: 63%;"><i class="fa fa-pencil-square-o" style="font-size:18px;color:blue"></i>&nbsp;Suggest Location&nbsp;</a>--%>
    <div class="form-group" id="myID5" style="display: none;">        
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">City <span class="required">*</span></label>
<div class="col-md-6 col-sm-2">
<asp:TextBox ID="sugcity" runat="server" class="form-control" placeholder="City *"></asp:TextBox></div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">State <span class="required">*</span></label>
<div class="col-md-6 col-sm-2">
<asp:TextBox ID="sugstate" runat="server" class="form-control" placeholder="State *"></asp:TextBox></div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Pincode <span class="required">*</span></label>
<div class="col-md-6 col-sm-2">
<asp:TextBox ID="sugpin" runat="server" class="form-control" placeholder="Pincode *" onkeypress="return isNumberKey(event)"></asp:TextBox>
   
</div>
</div>
        </div>     

    <div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Authority letter to be issued from the organization for nodal officer <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:FileUpload ID="FileUpload1" runat="server" class="form-control" /></div>
</div>

    <div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Government ID Card (Showing Name as mentioned in Nodal officer Name). <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:FileUpload ID="FileUpload2" runat="server" class="form-control" /></div>
</div>
    <font color="red" style="margin-left: 45%;"> <B>All File should be pdf formate only * Size less than 1 mb.</B></font>
<div class="ln_solid">
<div class="form-group">
<div class="col-md-6 offset-md-3">
    <asp:Button class="btn btn-primary" ID="Button2" OnClick="profileMigrat" Text="Save" runat="server" OnClientClick= "return validate();"/>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <asp:Button ID="closeBTN" class="btn btn-success" OnClick="closebtn" Text="Resat" runat="server" />

</div>
</div>
</div>
</form>
</div>
</div>
</div>
</div>
</div>

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

       <asp:HiddenField runat="server" ID="PMotp" />
            <div class="modal fade" id="modelotp" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
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
                                <asp:LinkButton runat="server" Text="Resend OTP" ID="lbresendotp" TabIndex="3" CssClass="pull-right mr-1 p-2" OnClick="lbresendotp_Click"></asp:LinkButton>
                                <p>
                                </p>
                            </p>
                        </div>
                    </div>
                </div>
            </div>

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
    <script type="text/javascript">
        function ValidNumeric() {

            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode >= 10 && charCode <= 10) { return true; }
            else { return false; }
        }
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
           function isNumberKey(evt) {
               var charCode = (evt.which) ? evt.which : event.keyCode;
               if (charCode != 46 && charCode > 31
                   && (charCode < 48 || charCode > 57))
                   return false;

               return true;
           }
       </script>
      <script type="text/javascript">

     function validate_disable() {

          }
          function validate() {
              var flag = true;
              if ((document.getElementById("<%=NolOffName.ClientID%>").value.trim() == '') || (document.getElementById("<%=NolOffName.ClientID%>").value.trim() == undefined)) {
                  alert('Nodal officer cannot be empty');
                flag = false;
            }
          else if ((document.getElementById("<%=VEmailID.ClientID%>").value.trim() == '') || (document.getElementById("<%=VEmailID.ClientID%>").value.trim() == undefined)) {
              alert('Please enter emailID');
               flag = false;
           }

            else if ((document.getElementById("<%=VmobileNO.ClientID%>").value.trim() == '') || (document.getElementById("<%=VmobileNO.ClientID%>").value.trim() == undefined)) {
                  alert('Please enter Mobile No.');
                flag = false;
            }

            else if ((document.getElementById("<%=NolOffADDRE1.ClientID%>").value.trim() == '') || (document.getElementById("<%=NolOffADDRE1.ClientID%>").value.trim() == undefined)) {
                  alert('Please enter Address Line1');
                  flag = false;
              }

              return flag;

               else if ((document.getElementById("<%=NolOffADDRE2.ClientID%>").value.trim() == '') || (document.getElementById("<%=NolOffADDRE2.ClientID%>").value.trim() == undefined)) {
                  alert('Please enter Address Line2');
                  flag = false;
              }

              return flag;

          }

      </script>
     <script type='text/javascript' >
         $(function () {
             $('#ddlPincode').ufd({ log: true });
         });
     </script>
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup() {
            $('#ProductCompany').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#modelotp').modal('show');
        }
    </script>
<script type="text/javascript">
    $(function () {
        $("[id$=TxtpincodeT]").autocomplete({
            source: function (request, response) {
                AjaxCall("V_GeneralInfo.aspx/GetPin", request.term, 0, response)
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
                AjaxCall("V_GeneralInfo.aspx/GetCities", request.term, $("[id$=hfPincode]").val(), response)
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
                        url: '<%=ResolveUrl("ProfileMigration.aspx/GetPinCodeService") %>',

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
                            url: '<%=ResolveUrl("ProfileMigration.aspx/GetCityService") %>',
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
</asp:Content>


