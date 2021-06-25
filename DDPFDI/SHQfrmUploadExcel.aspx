<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SHQfrmUploadExcel.aspx.cs" MasterPageFile="~/Admin/MasterPage.master" Inherits="SHQfrmUploadExcel" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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
            <asp:UpdatePanel runat="server" ID="up">
                <ContentTemplate>
                    <div class="section-pannel">
                        <div class="row">
                            <div class="col-md-12">
                                <div>
                                    <h4>Upload Product Excel</h4>
                                </div>
                                <hr />

                                     <div class="clearfix"></div>
                                <asp:HiddenField runat="server" ID="hidType" />
                                <asp:HiddenField runat="server" ID="hfcomprefno" />
                                <div>
                                      <label class="form-control-label" id="msg" runat="server">
                                                You can not access this page . </label>&nbsp;
                                </div>
                                <div class="row">
                                    <asp:Label runat="server" Visible="false" ID="lblnsccode"></asp:Label>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <label class="form-control-label">
                                                Select File Type</label>&nbsp;
                                <asp:RadioButtonList ID="rbtypefile" runat="server" CssClass="" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="1">Excel Format</asp:ListItem>
                                 
                                </asp:RadioButtonList>
                                        </div>
                                    </div>              
                                </div>
                                <div class="clearfix pb15"></div>
                                <div class="form-group row">
                                    <label class="col-sm-3 form-control-label">
                                        Select Excel File</label>
                                    <div class="col-sm-9">
                                        <asp:FileUpload runat="server" class="form-control" ID="fuexcel" />
                                    </div>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="col-sm-12">
                                    <asp:Button ID="btnProduct" runat="server" class="btn btn-primary btn-sm pull-right"
                                        Text="Upload Product" OnClick="btnProduct_Click" Visible="false" />
                                    <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-sm pull-right"
                                        Text="Upload Product" OnClick="Button1_Click" />
                                     <asp:Button ID="Button2" Visible="false" runat="server" class="btn btn-primary btn-sm pull-right"
                                        Text="Generate Excel Format" OnClick="Button2_Click"/>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div>

                                    <p>1. Company/Division/Unit name should be correct and as per registration.</p>
                                    <%--<p>2. All mandatory dropdown like (NSN GROUP,NSN GROUP CLASS,ITEM CODE,HS CODE etc) should be correct and as per master given data in website.</p>--%>
                                    <p>
                                        2. Please download sample before upload product excel file &nbsp;                           
                                <a href="http://srijandefence.gov.in/Upload/Sample%20Format.xlsx" target="_blank" class="fa fa-download"></a>
                                    </p>
                                    <p>3. Headers are given in sample excel, please add rows as per your data.</p>
                                    <p>
                                        4.Always check excel sheet name should be Sheet1.
                                    </p>
                                 
                                <div class="clearfix mt10"></div>
                                <div class="col-sm-12">
                                    <label class="form-control-label">
                                        <h4>
                                            <asp:Label ID="lblpathname" runat="server" class="label label-info"></asp:Label>
                                        </h4>
                                    </label>
                                    <label class="form-control-label">
                                        <h4>
                                            <asp:Label ID="lblRowCount" runat="server" class="label label-info"></asp:Label>
                                        </h4>
                                    </label>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div runat="server" id="diverror">
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="form-group row">
                                    <div class="clearfix"></div>
                                    <div id="Div1" class="col-sm-8" runat="server" visible="false">
                                        <div class="col-sm-5 row">
                                            L1 :
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtL1" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="clearfix mt10"></div>
                                        <div class="col-sm-5 row">
                                            L2 :
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtL2" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="col-sm-12">
                                            <div runat="server" id="Div2"></div>
                                            <asp:Button ID="btnexcel3510" runat="server" Visible="false" class="btn btn-primary btn-sm pull-right"
                                                Text="Upload Excel Sheet Iteam Code File" OnClick="btnexcel3510_Click" />
                                        </div>

                                        <div class="col-sm-12">
                                            <div runat="server" id="myhtmldiv"></div>
                                            <asp:Button ID="btnexcel" runat="server" Visible="false" class="btn btn-primary btn-sm pull-right"
                                                Text="Upload Excel Sheet NSN File" OnClick="btnexcel_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Button1" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>