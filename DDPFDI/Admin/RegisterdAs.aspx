<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterdAs.aspx.cs" Inherits="Admin_RegisterdAs" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="col-mod-12">
                <h3 class="page-header"><i class="fas fa-folder-plus"></i>Registerd As</h3>
                <div class="UserInnerpage">
                        <div class="resitered">
                            <form>
                                <div class="form-group">
                                    <input type="checkbox">
                                    <label>FDI Received</label>
                                </div>
                                <div class="form-group">
                                    <input type="checkbox">
                                    <label>Indigenisation</label>
                                </div>
                                <div class="form-group">
                                     <input type="checkbox">
                                      <label>Defence Export</label>
                                </div>
                                <div class="form-group">
                                    <input type="checkbox">
                                    <label>Defence Production</label>
                                </div>
                                <div class="form-group">
                                    <input type="checkbox">
                                    <label>Export Lead</label>
                                </div>

                            </form>
                        </div>
                </div>

            </div>
            <div class="clearfix"></div>
        </div>
        <div class="footer">� 2019 <a href="#">Department of Defence Production</a> </div>
    </div>
</asp:Content>