<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_GeneralInfo.aspx.cs" Inherits="Vendor_V_GeneralInfo" MasterPageFile="~/Vendor/VendorMaster.master" %>

<asp:Content ID="ConHead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Innercontent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="container">
                <div class="cacade-forms">
                    <div class="clearfix mt10"></div>
                    <div id="pd" class="tab-pane fade in active">
                        <asp:UpdatePanel ID="UpOne" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="panstep1" runat="server">
                                    <p>Please provide all required details to register your business with us</p>
                                    <div class="clearfix mt10"></div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            REGISTRATION CATEGORY <span class="mandatory">*</span>
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlregiscategory" runat="server" CssClass="form-control"
                                                required="required">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="MANUFACTURER">MANUFACTURER</asp:ListItem>
                                                <asp:ListItem Value="SERVICE SUB CONTRACTOR">SERVICE SUB CONTRACTOR</asp:ListItem>
                                                <asp:ListItem Value="AUTHORISED AGENT">AUTHORISED AGENT</asp:ListItem>
                                                <asp:ListItem Value="TRADER">TRADER</asp:ListItem>
                                                <asp:ListItem Value="OEM">OEM</asp:ListItem>
                                                <asp:ListItem Value="Stockist">Stockist</asp:ListItem>
                                                <asp:ListItem Value="Contractor">Contractor</asp:ListItem>
                                                <asp:ListItem Value="Distributor">Distributor</asp:ListItem>
                                                <asp:ListItem Value="Consortium">Consortium</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Type of OwnerShip
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddltypeofbusiness" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddltypeofbusiness_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <div class="clearfix mt10"></div>
                                            <div runat="server" id="divothersdetails" visible="false">
                                                <asp:TextBox ID="txtothersdetails" runat="server" CssClass="form-control" placeholder="Please sepcifiy"></asp:TextBox>
                                            </div>
                                            <div runat="server" id="divmsmetypeofbuisness" visible="false">
                                                <p>Scale of buisness</p>
                                                <asp:DropDownList ID="ddlscaleofbuisness" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlscaleofbuisness_SelectedIndexChanged" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Small</asp:ListItem>
                                                    <asp:ListItem Value="2">Medium</asp:ListItem>
                                                    <asp:ListItem Value="3">Micro</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="clearfix mt10"></div>
                                                <div class="col-sm-7">
                                                    <p>Ownership</p>
                                                    <asp:CheckBoxList ID="chkownership" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkownership_SelectedIndexChanged" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem Value="1">SC/ST</asp:ListItem>
                                                        <asp:ListItem Value="2">General</asp:ListItem>
                                                        <asp:ListItem Value="3">Women Organization</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                                <div class="col-sm-5">
                                                    <div runat="server" id="per1" visible="false">
                                                        <asp:TextBox ID="txtpercent1" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>Percentage of ownership</p>
                                                    </div>
                                                    <div class="clearfix mt10"></div>
                                                    <div runat="server" id="per2" visible="false">
                                                        <asp:TextBox ID="txtpercent2" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <p>Percentage of ownership</p>
                                                    </div>
                                                </div>
                                                <div class="clearfix mt10"></div>
                                                <div runat="server" id="cermsme" visible="false">
                                                    <p>
                                                        MSME certificate issued by competent authorities (NSIC/ DIC/ KVIC/KVIB/ Coir Board, Directorate of Handicraft & Handlooms)
                                                    </p>
                                                    <asp:FileUpload runat="server" ID="fun" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Business Sector
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlbusinesssector" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Date of Incorporation of the Company
                                        </div>
                                        <div class="col-sm-7">

                                            <div class="input-append date" id="datePicker" data-date="12-02-2012" data-date-format="dd/mm/yyyy">
                                                <span class="add-on"><i class="icon-th"></i></span>
                                                <asp:TextBox ID="txtdateofincorofthecompany" runat="server" CssClass="form-control datePicker"  data-date-format="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Landline No
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtstdcode" runat="server" required="" onkeypress="return isNumberKey(event)" MaxLength="5" CssClass="form-control"></asp:TextBox>

                                            <p>STD Code</p>
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtphoneno" runat="server" required="" onkeypress="return isNumberKey(event)" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                            <p>Phone Number</p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Fax No
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtfaxstdcode" runat="server" required="" onkeypress="return isNumberKey(event)" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                            <p>STD Code</p>
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtfaxphoneno" runat="server" required="" onkeypress="return isNumberKey(event)" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                            <p>Phone Number</p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <p>Details of company officials</p>
                                        <asp:GridView ID="gridNameof" runat="server" AutoGenerateColumns="false" class="table table-responsive" ShowFooter="true" OnRowCreated="gridNameof_RowCreated">
                                            <Columns>
                                                <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
                                                <asp:TemplateField HeaderText="Enter Name of">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlenternameof" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                            <asp:ListItem Value="Proprietor">Proprietor</asp:ListItem>
                                                            <asp:ListItem Value="Managing Director">Managing Director</asp:ListItem>
                                                            <asp:ListItem Value="Partner">Partner</asp:ListItem>
                                                            <asp:ListItem Value="Director">Director</asp:ListItem>
                                                            <asp:ListItem Value="Holder of Power of Attorney">Holder of Power of Attorney</asp:ListItem>
                                                            <asp:ListItem Value="Promoter">Promoter</asp:ListItem>
                                                            <asp:ListItem Value="Company secretary">Company secretary</asp:ListItem>
                                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEnterNameof" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DIN No">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtdinno" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtmobno" runat="server" onkeypress="return isNumberKey(event)"  CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="ButtonAddEnterNameof" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="ButtonAddEnterNameof_Click"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="fa fa-times"
                                                            OnClick="LinkButton1_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="gvgridNameof" runat="server" AutoGenerateColumns="false" class="table table-responsive" OnRowCommand="gvgridNameof_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="EnterName" HeaderText="Enter Name of" />
                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                <asp:BoundField DataField="DinNo" HeaderText="DIN No" />
                                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbAdd" runat="server" Class="fa fa-plus ml5" CommandName="Add" CommandArgument='<%#Eval("RowNumber") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="lbUpdate" runat="server" Class="fa fa-edit ml5"
                                                            CommandName="Upda" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                        <asp:HiddenField ID="hfmid" runat="server" Value='<%#Eval("RowNumber") %>' />
                                                        <asp:LinkButton ID="lbDelete" runat="server" Class="fa fa-trash ml5"
                                                            CommandName="Del" CommandArgument='<%#Eval("RowNumber") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <div class="clearfix pb15"></div>
                                <asp:LinkButton ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right" OnClick="btnsubmit_Click"></asp:LinkButton>
                                <asp:LinkButton ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-primary pull-right mr15" OnClick="btncancel_Click"></asp:LinkButton>
                                <div class="clearfix pb15"></div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnsubmit" />
                                <%--<asp:PostBackTrigger ControlID="gridNameof" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="up" runat="server" AssociatedUpdatePanelID="UpOne">
                            <ProgressTemplate>
                                <div class="overlay-progress">
                                    <div class="custom-progress-bar blue stripes">
                                        <span></span>
                                        <p>Processing</p>
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="changePass" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="p1">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Details of company officials</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="hfGenInfoID" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Enter Name of
                                    </label>
                                    <asp:DropDownList runat="server" ID="ddlenternameedit" class="form-control" TabIndex="1" ToolTip="Select Type of Name"
                                        required="">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="Proprietor">Proprietor</asp:ListItem>
                                        <asp:ListItem Value="Managing Director">Managing Director</asp:ListItem>
                                        <asp:ListItem Value="Partner">Partner</asp:ListItem>
                                        <asp:ListItem Value="Director">Director</asp:ListItem>
                                        <asp:ListItem Value="Holder of Power of Attorney">Holder of Power of Attorney</asp:ListItem>
                                        <asp:ListItem Value="Promoter">Promoter</asp:ListItem>
                                        <asp:ListItem Value="Company secretary">Company secretary</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Name
                                    </label>
                                    <asp:TextBox runat="server" ID="txtnameedit" class="form-control" required="" TabIndex="2" ToolTip="Enter Name"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Designation
                                    </label>
                                    <asp:TextBox runat="server" ID="txtdesignationedit" class="form-control" required="" TabIndex="3" ToolTip="Enter Designation"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        DIN No
                                    </label>
                                    <asp:TextBox runat="server" ID="txtdinnoedit" class="form-control" required="" TabIndex="4" ToolTip="Enter Din No"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Mobile No
                                    </label>
                                    <asp:TextBox runat="server" ID="txtmobnoedit" class="form-control" required="" onkeypress="return isNumberKey(event)" TabIndex="5" ToolTip="Mobile No (123456789) Only Number"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton ID="btnupdate" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="btnupdate_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show', function () {
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
</asp:Content>

