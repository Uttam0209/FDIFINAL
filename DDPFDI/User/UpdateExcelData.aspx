<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateExcelData.aspx.cs" Inherits="User_UpdateExcelData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                            <h4>Upload Product Excel</h4>
                        </div>
                        <hr />
                   <%--     <div class="form-group row">
                            <label class="col-sm-3 form-control-label">
                                Select File Type</label>
                            <div class="col-sm-9">
                                <asp:RadioButtonList ID="rbtypefile" runat="server" CssClass="" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="1">Excel Format</asp:ListItem>
                                    <asp:ListItem Value="2" style="margin-left: 10px;">CSV Format</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>--%>
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
                                Text="Upload Product" OnClick="btnexcelProduct_Click" />
                        </div>
                        <div class="clearfix mt10"></div>
                        <div>

                            <p>1. Company/Division/Unit name should be correct and as per registration.</p>
                            <p>2. All mandatory dropdown like (NSN GROUP,NSN GROUP CLASS,ITEM CODE,HS CODE etc) should be correct and as per master given data in website.</p>
                            <p>
                                3. Please download sample before upload product excel file &nbsp;                           
                                <a href="http://srijandefence.gov.in/Upload/Sample%20Format.xlsx" target="_blank" class="fa fa-download"></a>
                            </p>
                            <p>4. Headers are given in sample excel, please add rows as per your data.</p>
                            <p>
                                5.Always check excel sheet name should be Sheet1.
                            </p>
                            <div class="clearfix mt10"></div>
                            
                        </div>
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
        </div>
    </div>
</asp:Content>

