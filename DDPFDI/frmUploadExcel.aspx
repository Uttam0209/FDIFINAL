<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUploadExcel.aspx.cs" Inherits="frmUploadExcel"
    MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="right_col" role="main">
        <div class="">
            <div class="page-title">
                <div class="title_left">
                    <h3>Upload Excel</h3>
                </div>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="form-group row">
                        <label class="col-sm-3 form-control-label">
                            Select Excel File</label>
                        <div class="col-sm-9">
                            <asp:FileUpload runat="server" class="form-control" ID="fuexcel" />
                        </div>
                    </div>
                    <div class="Clearfix pb5">
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-9 form-control-label">
                            <h4>
                                <asp:Label ID="lblpathname" runat="server" class="label label-info"></asp:Label>
                                <asp:Label ID="lblRowCount" runat="server" class="label label-info"></asp:Label>
                            </h4>
                        </label>
                        <div class="col-sm-3">
                            <asp:Button ID="btnexcel" runat="server" class="btn btn-primary btn-sm btn-block"
                                Text="Upload Excel Sheet File" OnClick="btnexcel_Click" />
                        </div>
                    </div>
                    <div class="Clearfix pb5">
                    </div>
                    <div runat="server" id="diverror">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
