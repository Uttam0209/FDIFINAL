<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Videos.aspx.cs" Inherits="Videos_Videos" MasterPageFile="~/User/MasterPage.master" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ID="content2" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="row d-flex justify-content-center">
        <div class="col-lg-11">
            <div class="row d-flex">
                <p class=" col-12 card-header alert-info text-center text-capitalize justify-content-end">Video's</p>
                <br />
                <div class="clearfix"></div>
                <div class="col-12 d-flex justify-content-end py-3">
                    <div class="col-sm-6">
                        <p>How to Participate</p>

                        <div class="clearfix"></div>
                        <video src="how%20to%20participate.mp4" controls="controls" height="430px" runat="server" id="video1" autoplay="true" />
                    </div>
                    <div class="col-sm-6">
                        <p>How to Search</p>

                        <div class="clearfix"></div>
                        <video src="how%20to%20search.mp4" controls="controls" autoplay="true" height="430px" runat="server" id="video2" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
