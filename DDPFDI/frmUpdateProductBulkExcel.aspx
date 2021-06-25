<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUpdateProductBulkExcel.aspx.cs" Inherits="Admin_frmUpdateProductBulkExcel" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel runat="server" ID="up">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                        <div class="col-md-12">
                            <div class="clearfix"></div>
                            <div style="margin-top: 5px;">
                                <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <div class="section-pannel">
                        <div class="row">
                            <div class="col-md-12">
                                <div>
                                    <h4>Update Product through Excel</h4>
                                </div>
                                <hr />
                                <asp:Label runat="server" ID="lblRowCount" CssClass="tetLable label-danger"></asp:Label>
                                 <div class="clearfix mt10"></div>
                                <div class="alert alert-info" id="diverror" runat="server"></div>
                                <div class="clearfix"></div>
                                <asp:HiddenField runat="server" ID="hidType" />
                                <asp:HiddenField runat="server" ID="hfcomprefno" />
                                <div class="col-sm-3">
                                    <div class="form-group row">
                                        Company Name
                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged">
                            </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="divlblselectdivison">
                                    <div class="form-group row">
                                        Division/Factory Name
                            <asp:DropDownList ID="ddldivision" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged">
                            </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="divlblselectunit">
                                    <div class="form-group row">
                                        Unit Name
                            <asp:DropDownList ID="ddlunit" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged">
                            </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div1">
                                    <div class="form-group row">
                                        <label>Click on button to download your product excel</label>
                                        <asp:LinkButton runat="server" ID="btndownloadTempexcel" CssClass="btn btn-sm btn-primary" OnClick="btndownloadTempexcel_Click">
                                            <i class="fa fa-download"></i>Download</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="clearfix pb15"></div>
                                <div runat="server" id="divuploadsection">
                                    <div>
                                        <div class="col-sm-9">
                                            <div class="form-group row">
                                                <label class="form-control-label">
                                                    Select Excel File</label>
                                                <asp:FileUpload runat="server" class="form-control" ID="fuexcel" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group row">
                                                <asp:Button ID="btnProduct" runat="server" Style="margin-top: 23px" class="btn btn-primary btn-sm pull-right btn-block"
                                                    Text="Update Product" OnClick="btnProduct_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix pb15"></div>
                                    <div>
                                        <p>1. Company/Division/Unit name should be correct and as per registration.</p>
                                        <p>2. All mandatory dropdown like (NSN GROUP,NSN GROUP CLASS,ITEM CODE,HS CODE etc) should be correct and as per master given data in website.</p>

                                        <p>3. Headers are given in sample excel, please add rows as per your data.</p>
                                        <p>
                                            4.Always check excel sheet name should be Sheet1.
                                        </p>
                                    </div>
                                </div>
                                <div class="clearfix mt10"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btndownloadTempexcel" />
            <asp:PostBackTrigger ControlID="btnProduct" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
