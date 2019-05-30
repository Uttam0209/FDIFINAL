<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditHeadDropdown.aspx.cs" Inherits="Admin_EditHeadDropdown" MasterPageFile="MasterPage.master" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <form method="post" class="addfdi">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <asp:Button runat="server" ID="btncomp" Text="Company Edit" OnClick="btncomp_Click" />
                        </div>
                        <div class="col-md-4" runat="server" id="divlblselectdivison">
                            <asp:Button runat="server" ID="btndivision" Text="Division Edit" OnClick="btndivision_Click" />
                        </div>
                        <div class="col-md-4" runat="server" id="divlblselectunit">
                            <asp:Button runat="server" ID="btnunit" Text="Unit Edit" OnClick="btnunit_Click" />
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>

</asp:Content>
