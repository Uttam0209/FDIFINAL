<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Participate.aspx.cs" Inherits="User_Participate" MasterPageFile="~/User/MasterPage.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #more {
            display: none;
        }
    </style>

    <script> 
        var vid = document.getElementById("myVideo");

        function playVid() {
            vid.play();
        }

        function pauseVid() {
            vid.pause();
        }
    </script>

</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-12">
        <div class="py-2">
            <asp:Button ID="btnvideo" runat="server" Text="view video" CssClass="btn btn-primary btn-sm pull-right" OnClick="btnvideo_Click" />
        </div>
    </div>
    <div class="clearfix">
         <div class="row">
        <div class="container py-4" runat="server" id="divvideo" visible="false" style="text-align:center;">
            <video width="100%" height="350" autoplay controls>
                <source src="../Videos/how%20to%20participate.mp4" autoplay="1" type="video/mp4">
            </video>
        </div>
             </div>
        <div class="row">
            <div class="col-12">
                <h5 class="py-2">How to participate in 4 steps</h5>
                <p>(Note: It does not require any registration. DPSU LOGIN is for internal purpose.)</p>
                <p>
                    <b>Step 1</b>: Identify the product(s) of your interest with the help of search facility available in the left column.<span id="dots">...</span><span id="more">
                  Search/filter can be done company wise, NATO Group classification. Use more details below each product to understand the product which includes specification, business opportunity, contact details, etc.</span> <a style="text-decoration: underline!important; color: blue!important; font-weight: 500; cursor: pointer" onclick="myFunction()" id="myBtn">Read More</a>
                </p>
                <p>
                    <b>Step 2</b>: Click the tab <b>Add to show interest</b> as available below each product.
                </p>
                <p>
                    <b>Step 3</b>: Click the tab show interest as available on the top right corner.
                </p>
                <p>
                    <b>Step 4</b>: Fill the Show interest section. You will receive OTP. Based on OTP authentication you will receive mail that you have shown interest.
                </p>
                <p>DPSUs/OFB/SHQs will interact with you based on their requirement of products and as per their guidelines & procedures.</p>
            </div>
        </div>

        <div class="row">
            <div class="col-12 mt-2">
                <p style="color: blue;"><b>Flow chart of how to participate</b></p>
                <img src="Uassets/Images/Presentation5.jpg" />

            </div>
        </div>
    </div>
    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <script>
        function myFunction() {
            var dots = document.getElementById("dots");
            var moreText = document.getElementById("more");
            var btnText = document.getElementById("myBtn");

            if (dots.style.display === "none") {
                dots.style.display = "inline";
                btnText.innerHTML = "Read more";
                moreText.style.display = "none";
            } else {
                dots.style.display = "none";
                btnText.innerHTML = "Read less";
                moreText.style.display = "inline";
            }
        }
    </script>
</asp:Content>
