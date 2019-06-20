<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUploadExcel.aspx.cs" Inherits="frmUploadExcel"
    MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
                                    
    <table width="50%" align="Center">
        <tr>
            <td>
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
                        <div class="col-sm-6">
                       <div>L1 : <asp:TextBox ID="txtL1" runat="server" ></asp:TextBox></div> 
                         <div>L2 : <asp:TextBox ID="txtL2" runat="server" ></asp:TextBox></div>

                        </div>
                        <div class="row">
                         <div class="col-sm-6">
                            <asp:Button ID="btnexcel3510" runat="server" class="btn btn-primary btn-sm btn-block"
                                Text="Upload Excel Sheet Iteam Code File" OnClick="btnexcel3510_Click" />
                        </div>
                        <div class="col-sm-6">
                            <asp:Button ID="btnexcel" runat="server" Visible="false" class="btn btn-primary btn-sm btn-block"
                                Text="Upload Excel Sheet NSN File" OnClick="btnexcel_Click" />
                        </div>

                        </div>
                    </div>
                    <div class="Clearfix pb5">
                    </div>
                    <div runat="server" id="diverror">
                    </div>
                </div>
            </div></td>
        </tr>
      </table>                                      
                                  

</asp:Content>
